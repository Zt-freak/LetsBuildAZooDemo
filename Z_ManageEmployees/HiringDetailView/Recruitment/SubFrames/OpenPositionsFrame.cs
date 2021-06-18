// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames.OpenPositionsFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_BalanceSystems.Employees;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer;
using TinyZoo.Z_ManageEmployees.ManageEmployeeMain.HiringSummary;
using TinyZoo.Z_ManageShop.Shop_Data;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames
{
  internal class OpenPositionsFrame
  {
    public Vector2 location;
    private MiniHeading miniHeading;
    private CustomerFrame customerFrame;
    private SimpleTextHandler text;
    private PriceAdjuster openPositionsAdjuster;
    private OpenPositions TEMPOPENPOSITIONS;
    private OpenPositions ORIGINALPOSTIIONS;
    private bool AllowOpenPositionsToggling;
    private SpinningProgressIconWithText inProgressText;
    private bool IsActivelySearching;
    private OnOffToggle toggle;

    public OpenPositionsFrame(
      OpenPositions _TEMPOPENPOSITIONS,
      OpenPositions _ORIGINALPOSTIIONS,
      float forceWidth,
      float BaseScale,
      ShopEntry shopEntry,
      Player player,
      EmployeeType RoamingEmployeeType = EmployeeType.None)
    {
      this.TEMPOPENPOSITIONS = _TEMPOPENPOSITIONS;
      this.ORIGINALPOSTIIONS = _ORIGINALPOSTIIONS;
      bool singleOpenPositions = JobApplicants_Calculator.UseSingleOpenPositions;
      int ofPositionsOpened = this.TEMPOPENPOSITIONS.NumberOfPositionsOpened;
      this.AllowOpenPositionsToggling = _TEMPOPENPOSITIONS.GetNumberOfApplicants() == 0;
      if (singleOpenPositions)
        this.AllowOpenPositionsToggling = true;
      this.IsActivelySearching = ofPositionsOpened > 0;
      int Max = JobApplicants_Calculator.GetMaximumOpenPositionsYouCanHave();
      if (JobApplicants_Calculator.LimitSearchingWhenFull && shopEntry != null)
        Max = Math.Max(ShopData.GetMaximumEmployeesForThisShop(shopEntry.tiletype, player) - shopEntry.GetEmplyeeCount(), 0);
      int currentEmployeeCount;
      int maxEmployeeCount;
      JobApplicants_Calculator.IsShopAtMaxCapacity(shopEntry, RoamingEmployeeType, player, out currentEmployeeCount, out maxEmployeeCount);
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      Vector2 vector2_1 = Vector2.One * 10f;
      float num1 = 0.0f;
      float num2 = 0.0f;
      this.miniHeading = new MiniHeading(Vector2.Zero, "Open Job Positions", 1f, BaseScale);
      float num3 = num1 + (float) ((double) this.miniHeading.GetTextHeight(true) + (double) uiScaleHelper.ScaleY(vector2_1.Y) + (double) defaultYbuffer * 0.5);
      float num4 = num2 + uiScaleHelper.ScaleX(vector2_1.X);
      string str = string.Empty;
      if (shopEntry != null)
        str = str + "Available Vacancies: " + (object) (maxEmployeeCount - currentEmployeeCount) + "~";
      string TextToWrite = str + "Opens this job position for applicants to apply.";
      if (this.IsActivelySearching)
        TextToWrite += "~~NOTE: Closing the position will cause the applicants to leave, and the campaign progress to reset.";
      if (!this.AllowOpenPositionsToggling)
        TextToWrite = "You must respond to your current applicants before closing the position.";
      float width_ = forceWidth * 0.6f;
      if (singleOpenPositions)
        width_ = forceWidth - uiScaleHelper.ScaleX(60f);
      this.text = new SimpleTextHandler(TextToWrite, width_, _Scale: BaseScale);
      float heightOfParagraph = this.text.GetHeightOfParagraph();
      this.text.AutoCompleteParagraph();
      this.text.Location.X = num4;
      this.text.Location.Y = num3;
      this.text.SetAllColours(ColourData.Z_Cream);
      float y = num3 + heightOfParagraph + defaultYbuffer;
      if (singleOpenPositions)
      {
        this.toggle = new OnOffToggle(BaseScale, ofPositionsOpened > 0);
        this.toggle.location.X = forceWidth - defaultXbuffer;
        this.toggle.location.Y = y - defaultYbuffer;
        this.toggle.location -= this.toggle.GetSize() * 0.5f;
      }
      else
      {
        this.openPositionsAdjuster = new PriceAdjuster(0, Max, ofPositionsOpened, _BaseScale: BaseScale);
        this.openPositionsAdjuster.SetToString(ofPositionsOpened.ToString(), 40f, true);
      }
      if (this.openPositionsAdjuster != null)
      {
        Vector2 size = this.openPositionsAdjuster.GetSize();
        this.openPositionsAdjuster.Location.X = forceWidth - defaultXbuffer;
        this.openPositionsAdjuster.Location.X -= size.X * 0.5f;
        this.openPositionsAdjuster.Location.Y = y;
        this.openPositionsAdjuster.Location.Y -= size.Y * 0.5f;
        this.openPositionsAdjuster.Location.Y -= defaultYbuffer;
      }
      this.inProgressText = new SpinningProgressIconWithText(BaseScale, "Searching");
      if (this.toggle != null)
      {
        this.inProgressText.location.Y = defaultYbuffer;
        this.inProgressText.location.Y += this.inProgressText.GetSize().Y * 0.5f;
        if (!this.IsActivelySearching)
        {
          this.inProgressText.location.Y = (float) ((double) this.toggle.location.Y - (double) this.toggle.GetSize().Y * 0.5 - (double) this.inProgressText.GetSize().Y * 0.5);
          if (shopEntry != null)
            this.inProgressText.location.Y -= uiScaleHelper.ScaleY(5f);
        }
      }
      else
      {
        if (this.text.paragraph.linemaker.GetNumberOfLines() > 2)
          this.inProgressText.location.Y += defaultYbuffer;
        else
          this.inProgressText.location.Y += uiScaleHelper.ScaleY(4f);
        this.inProgressText.location.Y += this.inProgressText.GetSize().Y * 0.5f;
      }
      if (this.openPositionsAdjuster != null)
      {
        this.inProgressText.location.X = this.openPositionsAdjuster.Location.X;
      }
      else
      {
        this.inProgressText.location.X = forceWidth - defaultXbuffer;
        this.inProgressText.location.X -= this.inProgressText.GetSize().X * 0.5f;
      }
      this.inProgressText.location.X -= this.inProgressText.GetSize().X * 0.5f;
      this.inProgressText.location.X -= uiScaleHelper.ScaleX(2f);
      if (!this.AllowOpenPositionsToggling && this.openPositionsAdjuster != null)
        this.openPositionsAdjuster.SetDisabled(true);
      this.customerFrame = new CustomerFrame(new Vector2(forceWidth, y), BaseScale: BaseScale);
      this.miniHeading.SetTextPosition(this.customerFrame.VSCale, vector2_1.X, vector2_1.Y);
      Vector2 vector2_2 = -this.customerFrame.VSCale * 0.5f;
      this.text.Location += vector2_2;
      if (this.openPositionsAdjuster != null)
        this.openPositionsAdjuster.Location += vector2_2;
      if (this.toggle != null)
        this.toggle.location += vector2_2;
      this.inProgressText.location += vector2_2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateOpenPositionsFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.SetIsActivelySearching();
      if (this.toggle != null)
      {
        if (this.toggle.UpdateOnOffToggle(player, offset, DeltaTime))
        {
          this.TEMPOPENPOSITIONS.NumberOfPositionsOpened = !this.toggle.On ? 0 : 1;
          return true;
        }
      }
      else if (this.openPositionsAdjuster != null && this.openPositionsAdjuster.UpdatePriceAdjuster(player, offset, DeltaTime))
      {
        int currentValue = this.openPositionsAdjuster.CurrentValue;
        this.openPositionsAdjuster.SetToString(currentValue.ToString(), -1f);
        this.TEMPOPENPOSITIONS.NumberOfPositionsOpened = currentValue;
        return true;
      }
      this.inProgressText.UpdateSpinningProgressIconWithText(DeltaTime);
      return false;
    }

    private void SetIsActivelySearching() => this.IsActivelySearching = this.ORIGINALPOSTIIONS.NumberOfPositionsOpened > 0;

    public void DrawOpenPositionsFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.miniHeading.DrawMiniHeading(offset, spriteBatch);
      this.text.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      if (this.toggle != null)
        this.toggle.DrawOnOffToggle(spriteBatch, offset);
      else
        this.openPositionsAdjuster.DrawPriceAdjuster(offset, spriteBatch);
      if (!this.IsActivelySearching)
        return;
      this.inProgressText.DrawSpinningProgressIconWithText(offset, spriteBatch);
    }
  }
}
