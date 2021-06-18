// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.SelectCell.Orders.Rows.OrderAssignmentRowContainer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_BalanceSystems.Animals.Cohabitation;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_GenericUI.Z_Scroll;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;

namespace TinyZoo.OverWorld.SelectCell.Orders.Rows
{
  internal class OrderAssignmentRowContainer
  {
    public Vector2 location;
    private OrderInfoHeader headerRow;
    private List<OrderInfoRow> rows;
    private OrderSpaceSummaryRow summaryRow;
    private Z_ScrollHelper scrollHelper;
    private Z_GenericScrollMasks scrollMasks;
    private Vector2 scrollTopLeftPoint;
    private Vector2 size;
    private PrisonZone refPrisonZone;
    private NewAnimalsInCellInfo currentCellInfo;
    private bool DataJustChanged;
    private bool NoAnimalsLeftToAssign;

    public OrderAssignmentRowContainer(float BaseScale, List<IntakePerson> orderList)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      defaultBuffer.Y *= 0.5f;
      float[] widths = new float[5];
      float num1 = uiScaleHelper.ScaleY(35f);
      for (int index = 0; index < 5; ++index)
      {
        float num2 = 0.0f;
        switch (index)
        {
          case 0:
            num2 = 165f;
            break;
          case 1:
          case 2:
            num2 = 50f;
            break;
          case 3:
          case 4:
            num2 = 70f;
            break;
        }
        widths[index] = uiScaleHelper.ScaleX(num2);
      }
      float x = ((IEnumerable<float>) widths).Sum();
      this.size = Vector2.Zero;
      this.headerRow = new OrderInfoHeader(BaseScale, widths);
      this.size.Y += num1 * 0.5f;
      this.headerRow.location.Y = this.size.Y;
      this.size.Y += num1 * 0.5f;
      float num3 = uiScaleHelper.ScaleY(300f);
      float num4 = 0.0f;
      this.rows = new List<OrderInfoRow>();
      for (int index = 0; index < orderList.Count; ++index)
      {
        OrderInfoRow orderInfoRow = new OrderInfoRow(BaseScale, num1, widths, orderList[index]);
        orderInfoRow.location.Y = this.size.Y + num4;
        orderInfoRow.location.Y += orderInfoRow.GetSize().Y * 0.5f;
        num4 += orderInfoRow.GetSize().Y;
        if (index != orderList.Count - 1)
          num4 += defaultBuffer.Y;
        this.rows.Add(orderInfoRow);
      }
      this.scrollMasks = new Z_GenericScrollMasks(BaseScale, ColourData.Z_FrameMidBrown, x + defaultBuffer.X * 2f, num1, Math.Min(num3, num4));
      this.scrollMasks.location.Y = this.size.Y;
      this.scrollTopLeftPoint = new Vector2((float) (-(double) x * 0.5), this.size.Y);
      this.size.Y += Math.Min(num3, num4);
      this.size.Y += defaultBuffer.Y;
      this.scrollHelper = new Z_ScrollHelper(new Vector2(x, num4), num3);
      this.summaryRow = new OrderSpaceSummaryRow(BaseScale, num1, widths);
      this.summaryRow.location.Y = this.size.Y;
      this.summaryRow.location.Y += this.summaryRow.GetSize().Y * 0.5f;
      this.size.Y += this.summaryRow.GetSize().Y;
      this.size.Y += defaultBuffer.Y * 2f;
      this.size.X = this.rows[0].GetSize().X;
      this.ForceTickHeaderRowCheckbox(true);
    }

    public Vector2 GetSize() => this.size;

    internal static Vector2 GetCellColouredMargin() => new Vector2(1f, 2f);

    public void UpdateOrderAssignmentRowContainer(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.headerRow.UpdateOrderInfoHeader(player, DeltaTime, offset))
        this.OnHeaderRowCheckboxClicked();
      this.scrollHelper.UpdateZ_ScrollHelper(player, this.scrollTopLeftPoint + offset);
      offset.Y += this.scrollHelper.YscrollOffset;
      for (int index = 0; index < this.rows.Count; ++index)
      {
        if (this.rows[index].UpdateOrderInfoRow(player, DeltaTime, offset))
        {
          this.ScrubHeaderRowCheckbox(!this.rows[index].IsTicked);
          this.SetData(this.refPrisonZone);
        }
      }
    }

    public List<IntakePerson> GetAnimalListSelected(bool SetAsAssigned = false)
    {
      this.NoAnimalsLeftToAssign = true;
      List<IntakePerson> intakePersonList = new List<IntakePerson>();
      for (int index = 0; index < this.rows.Count; ++index)
      {
        if (this.rows[index].IsActivelySelected)
        {
          intakePersonList.Add(this.rows[index].refIntakePerson);
          if (SetAsAssigned)
            this.rows[index].OrderAssignedFinished = true;
        }
        if (!this.rows[index].OrderAssignedFinished)
          this.NoAnimalsLeftToAssign = false;
      }
      return intakePersonList;
    }

    private void ForceTickHeaderRowCheckbox(bool isTicked)
    {
      this.headerRow.CheckboxTicked = isTicked;
      this.OnHeaderRowCheckboxClicked();
    }

    private void OnHeaderRowCheckboxClicked()
    {
      for (int index = 0; index < this.rows.Count; ++index)
      {
        if (!this.rows[index].OrderAssignedFinished)
          this.rows[index].IsTicked = this.headerRow.CheckboxTicked;
      }
      this.SetData(this.refPrisonZone);
    }

    private void ScrubHeaderRowCheckbox(bool PlayerJustDeselected = false)
    {
      bool flag = true;
      if (PlayerJustDeselected)
      {
        flag = false;
      }
      else
      {
        for (int index = 0; index < this.rows.Count; ++index)
        {
          if (!this.rows[index].OrderAssignedFinished && !this.rows[index].IsTicked)
          {
            flag = false;
            break;
          }
        }
      }
      this.headerRow.CheckboxTicked = flag;
    }

    public void SetData(PrisonZone prisonZone)
    {
      this.refPrisonZone = prisonZone;
      float num1 = -1f;
      float num2 = -1f;
      float num3 = -1f;
      ThreatPackV2[] threatPackV2Array = (ThreatPackV2[]) null;
      if (prisonZone != null)
      {
        num1 = (float) prisonZone.GetFloorSpaceVolume();
        float TerritorySize = 0.0f;
        num3 = prisonZone.GetSpaceRequiredByAniamlsInThisPen(ref TerritorySize);
        num2 = num1 - num3;
        threatPackV2Array = Cohabitation_Calculator.GetThreatForNewAnimalsInPen(prisonZone, this.GetAnimalListSelected());
      }
      float val2_1 = 0.0f;
      float num4 = 0.0f;
      int numberSelected = 0;
      for (int index1 = 0; index1 < this.rows.Count; ++index1)
      {
        if (this.rows[index1].IsActivelySelected)
        {
          float MustHaveAtleastThisMuchSpace = 0.0f;
          float floorSpacePerAnimal = AnimalData.GetRequiredFloorSpacePerAnimal(this.rows[index1].refIntakePerson.animaltype, ref MustHaveAtleastThisMuchSpace);
          val2_1 += floorSpacePerAnimal;
          num4 = Math.Max(MustHaveAtleastThisMuchSpace, num4);
          ++numberSelected;
          bool HasTileSpaceLeftForThisAnimal = (double) num2 - (double) val2_1 >= 0.0;
          bool HasEnclosureSizeForThisAnimal = (double) num1 >= (double) MustHaveAtleastThisMuchSpace;
          NotificationAlertStatus _ThreatStatus = NotificationAlertStatus.Count;
          if (prisonZone != null)
          {
            for (int index2 = 0; index2 < threatPackV2Array.Length; ++index2)
            {
              if (threatPackV2Array[index2].simplethreatpack.animaltype == this.rows[index1].refIntakePerson.animaltype)
              {
                _ThreatStatus = threatPackV2Array[index2].GetAlerStatus();
                break;
              }
            }
          }
          this.rows[index1].SetData(prisonZone, HasTileSpaceLeftForThisAnimal, HasEnclosureSizeForThisAnimal, _ThreatStatus);
        }
        else
          this.rows[index1].SetData(prisonZone);
      }
      float val2_2 = Math.Max(num4, val2_1);
      float differenceInSpace = num1 - val2_2;
      this.summaryRow.SetData(prisonZone, numberSelected, differenceInSpace);
      this.currentCellInfo = new NewAnimalsInCellInfo();
      this.currentCellInfo.NumberOfAnimals = numberSelected;
      this.currentCellInfo.CurrentPenSpace = num1;
      this.currentCellInfo.TotalSpaceNeeded = Math.Max(val2_1 + num3, val2_2);
      this.DataJustChanged = true;
    }

    public NewAnimalsInCellInfo GetDataChanged_ForCellInfo()
    {
      if (this.refPrisonZone == null || this.currentCellInfo == null)
        return (NewAnimalsInCellInfo) null;
      if (!this.DataJustChanged)
        return (NewAnimalsInCellInfo) null;
      this.DataJustChanged = false;
      return this.currentCellInfo;
    }

    public void DrawOrderAssignmentRowContainer(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.rows.Count; ++index)
      {
        Vector2 size = this.rows[index].GetSize();
        float TopLocation = this.rows[index].location.Y - size.Y * 0.5f - this.scrollTopLeftPoint.Y;
        float BottomLocation = TopLocation + size.Y;
        if (this.scrollHelper.CheckIfShouldDrawThis(TopLocation, BottomLocation))
          this.rows[index].DrawOrderInfoRow(offset + new Vector2(0.0f, this.scrollHelper.YscrollOffset), spriteBatch);
      }
      this.scrollMasks.DrawZ_GenericScrollMasks(offset, spriteBatch);
      this.headerRow.DrawOrderInfoHeader(offset, spriteBatch);
      this.summaryRow.DrawOrderSpaceSummaryRow(offset, spriteBatch);
    }
  }
}
