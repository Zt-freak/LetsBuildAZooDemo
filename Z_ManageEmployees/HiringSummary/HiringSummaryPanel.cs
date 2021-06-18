// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringSummary.HiringSummaryPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_BalanceSystems.Employees;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManageEmployees.HiringSummary
{
  internal class HiringSummaryPanel
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private MiniHeading miniHeading;
    private List<HiringQuickInfoFrame> infoFrames;
    private SimpleTextHandler maxCapacityText;

    public HiringSummaryPanel(
      ShopEntry shopEntry,
      OpenPositions currentOpenPositions,
      Player player,
      float forceWidth,
      float BaseScale,
      EmployeeType _RoamingEmployeeType = EmployeeType.None)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      Vector2 vector2_1 = Vector2.One * 10f;
      float num1 = 0.0f;
      int currentEmployeeCount = 0;
      int maxEmployeeCount = -1;
      bool flag = JobApplicants_Calculator.IsShopAtMaxCapacity(shopEntry, _RoamingEmployeeType, player, out currentEmployeeCount, out maxEmployeeCount);
      string text = "Recruitment";
      if (maxEmployeeCount != -1)
        text = "Recruitment: " + (object) currentEmployeeCount + "/" + (object) maxEmployeeCount;
      this.miniHeading = new MiniHeading(Vector2.Zero, text, 1f, BaseScale);
      float num2 = num1 + (this.miniHeading.GetTextHeight(true) + uiScaleHelper.ScaleY(vector2_1.Y)) + defaultYbuffer;
      float num3;
      if (flag && JobApplicants_Calculator.LimitSearchingWhenFull)
      {
        this.maxCapacityText = new SimpleTextHandler("You are currently at max capacity!", true, (float) ((double) forceWidth / 1024.0 * 0.899999976158142), BaseScale);
        this.maxCapacityText.AutoCompleteParagraph();
        this.maxCapacityText.SetAllColours(ColourData.Z_Cream);
        this.maxCapacityText.Location.Y = num2;
        num3 = num2 + this.maxCapacityText.GetHeightOfParagraph();
      }
      else
      {
        this.infoFrames = new List<HiringQuickInfoFrame>();
        float ForceHeight = -1f;
        float ForceWidth = forceWidth - defaultXbuffer * 2f;
        for (int index = 0; index < 3; ++index)
        {
          HiringQuickInfoFrame hiringQuickInfoFrame = new HiringQuickInfoFrame((RecruitmentInfoType) index, currentOpenPositions, BaseScale, player, ForceHeight, ForceWidth);
          hiringQuickInfoFrame.location.Y = num2;
          hiringQuickInfoFrame.location.Y += hiringQuickInfoFrame.GetSize().Y * 0.5f;
          num2 += hiringQuickInfoFrame.GetSize().Y;
          if (index != 2)
            num2 += defaultYbuffer;
          this.infoFrames.Add(hiringQuickInfoFrame);
        }
        num3 = num2 + ForceHeight;
      }
      float y = num3 + defaultYbuffer;
      this.customerFrame = new CustomerFrame(new Vector2(forceWidth, y), true, BaseScale);
      this.miniHeading.SetTextPosition(this.customerFrame.VSCale, vector2_1.X, vector2_1.Y);
      Vector2 vector2_2 = -this.customerFrame.VSCale * 0.5f;
      if (this.infoFrames != null)
      {
        for (int index = 0; index < this.infoFrames.Count; ++index)
          this.infoFrames[index].location.Y += vector2_2.Y;
      }
      if (this.maxCapacityText == null)
        return;
      this.maxCapacityText.Location.Y += vector2_2.Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public RecruitmentButtonType UpdateHiringSummaryPanel(
      Player player,
      float DeltaTime,
      Vector2 offset)
    {
      offset += this.location;
      if (this.infoFrames != null)
      {
        for (int index = 0; index < this.infoFrames.Count; ++index)
        {
          RecruitmentButtonType recruitmentButtonType = this.infoFrames[index].UpdateHiringQuickInfoFrame(player, DeltaTime, offset);
          if (recruitmentButtonType != RecruitmentButtonType.Count)
            return recruitmentButtonType;
        }
      }
      return RecruitmentButtonType.Count;
    }

    public void DrawHiringSummaryPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.miniHeading.DrawMiniHeading(offset, spriteBatch);
      if (this.infoFrames != null)
      {
        for (int index = 0; index < this.infoFrames.Count; ++index)
          this.infoFrames[index].DrawHiringQuickInfoFrame(offset, spriteBatch);
      }
      if (this.maxCapacityText == null)
        return;
      this.maxCapacityText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
