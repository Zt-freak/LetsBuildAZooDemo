// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Desc.PeopleViewerDescFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_BalanceSystems.CustomerStats;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;
using TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info;

namespace TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Desc
{
  internal class PeopleViewerDescFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private SimpleTextHandler paraDesc;
    private ZGenericText text1;
    private float ForcedWidth;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    private PeopleViewInfoType refInfoType;
    private string baseString1;

    public PeopleViewerDescFrame(float _BaseScale, float _ForcedWidth)
    {
      this.ForcedWidth = _ForcedWidth;
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.customerFrame = new CustomerFrame(new Vector2(this.ForcedWidth, this.scaleHelper.ScaleY(40f)), CustomerFrameColors.DarkBrown, this.BaseScale);
      this.text1 = new ZGenericText(this.BaseScale, false, _UseOnePointFiveFont: true);
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void SetInfoType(PeopleViewInfoType infoType)
    {
      this.refInfoType = infoType;
      string TextToWrite = string.Empty;
      this.baseString1 = string.Empty;
      switch (infoType)
      {
        case PeopleViewInfoType.Actions:
          TextToWrite = "View the current actions of customers.";
          this.baseString1 = "Most Prevalent Action: {0}";
          break;
        case PeopleViewInfoType.Frustrations:
          TextToWrite = "View customers' most urgent frustrations.";
          this.baseString1 = "Most Common Frustration: {0}";
          break;
        case PeopleViewInfoType.MoneyHeld:
          TextToWrite = "View the amount of cash customers are holding.";
          this.baseString1 = "Average Cash Held: ${0}";
          break;
        case PeopleViewInfoType.VIP:
          TextToWrite = "VIPs are special visitors that impact your zoo in unique ways.";
          break;
      }
      float y = (float) (-(double) this.customerFrame.VSCale.Y * 0.5) + this.scaleHelper.GetDefaultYBuffer();
      float x = (float) (-(double) this.customerFrame.VSCale.X * 0.5) + this.scaleHelper.GetDefaultXBuffer();
      if (!string.IsNullOrEmpty(TextToWrite))
      {
        this.paraDesc = new SimpleTextHandler(TextToWrite, this.ForcedWidth, _Scale: this.BaseScale, AutoComplete: true);
        this.paraDesc.SetAllColours(ColourData.Z_Cream);
        this.paraDesc.Location = new Vector2(x, y);
        y += this.paraDesc.GetHeightOfParagraph();
      }
      this.text1.textToWrite = this.baseString1;
      this.text1.vLocation = new Vector2(x, y);
      float num = y + this.text1.GetSize().Y;
    }

    public void UpdatePeopleViewerDescFrame(bool NobodyInParkLeft = false)
    {
      if (NobodyInParkLeft)
      {
        if (this.refInfoType == PeopleViewInfoType.VIP)
          return;
        this.text1.textToWrite = string.Format(this.baseString1, (object) "-");
      }
      else
      {
        switch (this.refInfoType)
        {
          case PeopleViewInfoType.Actions:
            this.text1.textToWrite = string.Format(this.baseString1, (object) CurrentActionDisplay.GetCustomerQuestToString(CalculateStat.GetMostInProgressQuest()));
            break;
          case PeopleViewInfoType.Frustrations:
            this.text1.textToWrite = string.Format(this.baseString1, (object) SatisfactionBarAndText.GetSatisfactionTypeToString(CalculateStat.GetHighestFrustration()));
            break;
          case PeopleViewInfoType.MoneyHeld:
            this.text1.textToWrite = string.Format(this.baseString1, (object) CalculateStat.GetAverageCashHeld());
            break;
        }
      }
    }

    public void DrawPeopleViewerDescFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      if (this.paraDesc != null)
        this.paraDesc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.text1.DrawZGenericText(offset, spriteBatch);
    }
  }
}
