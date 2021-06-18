// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.SelectCell.Orders.Rows.OrderInfoRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.RibbonButtons;
using TinyZoo.Z_ManageEmployees.EmployeeView;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.OverWorld.SelectCell.Orders.Rows
{
  internal class OrderInfoRow
  {
    public Vector2 location;
    private PerformanceTableRowFrame frame;
    private ZCheckBox checkBox;
    private AnimalInFrame animal;
    private ZGenericText animalName;
    private NotificationAlertIcon habitatStatus;
    private NotificationAlertIcon threatStatus;
    private ZGenericText territoryText;
    private ZGenericText seperationText;
    public bool OrderAssignedFinished;

    public IntakePerson refIntakePerson { get; private set; }

    public bool IsTicked
    {
      get => this.checkBox.GetIsTicked();
      set => this.checkBox.SetTicked(value);
    }

    public bool IsActivelySelected => this.IsTicked && !this.OrderAssignedFinished;

    public OrderInfoRow(
      float BaseScale,
      float rowHeight,
      float[] widths,
      IntakePerson intakePerson)
    {
      this.SetUp(BaseScale, rowHeight, widths, intakePerson);
    }

    private void SetUp(
      float BaseScale,
      float rowHeight,
      float[] widths,
      IntakePerson intakePerson)
    {
      this.refIntakePerson = intakePerson;
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      defaultBuffer.X *= 0.5f;
      float num1 = (float) (-(double) ((IEnumerable<float>) widths).Sum() * 0.5);
      float MustHaveAtleastThisMuchSpace = 0.0f;
      float floorSpacePerAnimal = AnimalData.GetRequiredFloorSpacePerAnimal(intakePerson.animaltype, ref MustHaveAtleastThisMuchSpace);
      for (int index = 0; index < 5; ++index)
      {
        float num2 = num1 + widths[index] * 0.5f;
        switch (index)
        {
          case 0:
            this.checkBox = new ZCheckBox(BaseScale, true);
            this.animal = new AnimalInFrame(intakePerson.animaltype, intakePerson.HeadType, intakePerson.CLIndex, 25f * BaseScale, 6f * BaseScale, BaseScale, intakePerson.HeadVariant);
            this.animalName = new ZGenericText(EnemyData.GetEnemyTypeName(intakePerson.animaltype, intakePerson.HeadType), BaseScale, false, _UseOnePointFiveFont: true);
            float num3 = num2 - widths[index] * 0.5f + defaultBuffer.X;
            this.checkBox.location.X = num3 + this.checkBox.GetSize().X * 0.5f;
            float num4 = num3 + (this.checkBox.GetSize().X + defaultBuffer.X);
            this.animal.Location.X = num4 + this.animal.GetSize().X * 0.5f;
            float num5 = num4 + (this.animal.GetSize().X + defaultBuffer.X);
            this.animalName.vLocation.X = num5;
            this.animalName.vLocation.Y -= this.animalName.GetSize().Y * 0.5f;
            float num6 = num5 + (this.animalName.GetSize().X + defaultBuffer.X);
            break;
          case 1:
            this.habitatStatus = new NotificationAlertIcon(BaseScale);
            this.habitatStatus.location.X = num2;
            break;
          case 2:
            this.threatStatus = new NotificationAlertIcon(BaseScale);
            this.threatStatus.location.X = num2;
            break;
          case 3:
            this.territoryText = new ZGenericText("XX", BaseScale, _UseOnePointFiveFont: true);
            this.territoryText.textToWrite = MustHaveAtleastThisMuchSpace.ToString();
            this.territoryText.vLocation.X = num2;
            break;
          case 4:
            this.seperationText = new ZGenericText("XX", BaseScale, _UseOnePointFiveFont: true);
            this.seperationText.textToWrite = floorSpacePerAnimal.ToString();
            this.seperationText.vLocation.X = num2;
            break;
        }
        num1 = num2 + widths[index] * 0.5f;
      }
      this.frame = new PerformanceTableRowFrame(BaseScale, rowHeight, CustomerFrameColors.DarkBrown, widths);
      this.SetData((PrisonZone) null);
    }

    public void SetData(
      PrisonZone prisonZone,
      bool HasTileSpaceLeftForThisAnimal = false,
      bool HasEnclosureSizeForThisAnimal = false,
      NotificationAlertStatus _ThreatStatus = NotificationAlertStatus.None)
    {
      if (prisonZone == null)
      {
        for (int columnIndex = 0; columnIndex < 5; ++columnIndex)
          this.frame.RemoveColumnColor(columnIndex);
        this.habitatStatus.ChangeStatus(NotificationAlertStatus.Count);
        this.threatStatus.ChangeStatus(NotificationAlertStatus.Count);
      }
      else
      {
        float x = OrderAssignmentRowContainer.GetCellColouredMargin().X;
        float y = OrderAssignmentRowContainer.GetCellColouredMargin().Y;
        if (this.IsActivelySelected)
        {
          if (HasTileSpaceLeftForThisAnimal)
            this.frame.ColorThisColumnGreen(4, x, y);
          else
            this.frame.ColorThisColumnRed(4, x, y);
          if (HasEnclosureSizeForThisAnimal)
            this.frame.ColorThisColumnGreen(3, x, y);
          else
            this.frame.ColorThisColumnRed(3, x, y);
        }
        this.SetNotificationStatus(OrderInfoColumn.Habitat, AnimalData.GetHabitatAlert(prisonZone.CellBLOCKTYPE, this.refIntakePerson.animaltype), x, y);
        this.SetNotificationStatus(OrderInfoColumn.Threat, _ThreatStatus, x, y);
      }
      this.frame.Active = this.IsActivelySelected;
    }

    private void SetNotificationStatus(
      OrderInfoColumn column,
      NotificationAlertStatus status,
      float marginX,
      float marginY)
    {
      switch (column)
      {
        case OrderInfoColumn.Habitat:
          this.habitatStatus.ChangeStatus(status);
          break;
        case OrderInfoColumn.Threat:
          this.threatStatus.ChangeStatus(status);
          break;
      }
      switch (status)
      {
        case NotificationAlertStatus.Tick:
        case NotificationAlertStatus.Special_Heart:
        case NotificationAlertStatus.Special_Star:
          this.frame.ColorThisColumnGreen((int) column, marginX, marginY);
          break;
        case NotificationAlertStatus.Exclamation:
          this.frame.ColorThisColumn((int) column, ColourData.IconYellow, marginX, marginY);
          break;
        case NotificationAlertStatus.Danger_Worst:
          this.frame.ColorThisColumnRed((int) column, marginX, marginY);
          break;
      }
    }

    public Vector2 GetSize() => this.frame.GetSize();

    public bool UpdateOrderInfoRow(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.frame.UpdateWithoutMouseOver(player, DeltaTime, offset);
      return !this.OrderAssignedFinished && this.checkBox.UpdateCheckBox(player, offset);
    }

    public void DrawOrderInfoRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.frame.DrawPerformanceTableRowFrame(offset, spriteBatch);
      if (this.OrderAssignedFinished)
        this.checkBox.DrawCheckBox(spriteBatch, offset);
      this.animal.DrawAnimalInFrame(offset, spriteBatch);
      this.animalName.DrawZGenericText(offset, spriteBatch);
      this.habitatStatus.DrawNotificationAlertIcon(offset, spriteBatch);
      this.threatStatus.DrawNotificationAlertIcon(offset, spriteBatch);
      this.territoryText.DrawZGenericText(offset, spriteBatch);
      this.seperationText.DrawZGenericText(offset, spriteBatch);
      this.frame.PostDrawPerformanceTableRowFrame(offset, spriteBatch);
      if (this.OrderAssignedFinished)
        return;
      this.checkBox.DrawCheckBox(spriteBatch, offset);
    }
  }
}
