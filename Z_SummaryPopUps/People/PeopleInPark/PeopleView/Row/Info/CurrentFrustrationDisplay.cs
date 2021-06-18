// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info.CurrentFrustrationDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info
{
  internal class CurrentFrustrationDisplay
  {
    public Vector2 location;
    private ZGenericText text;
    private ZGenericText subText;
    private float doubleTextOffsetY;
    private CustomerNeedsIcon icon;
    private SatisfactionType currentFrustration;

    public CurrentFrustrationDisplay(float BaseScale)
    {
      this.currentFrustration = SatisfactionType.Count;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.icon = new CustomerNeedsIcon(BaseScale);
      this.icon.location.X += this.icon.GetSize().X * 0.5f;
      this.text = new ZGenericText("-", BaseScale, false, _UseOnePointFiveFont: true);
      this.text.vLocation.X += this.icon.GetSize().X + uiScaleHelper.GetDefaultXBuffer() * 0.5f;
      this.text.vLocation.Y -= this.text.GetSize().Y * 0.5f;
      this.subText = new ZGenericText("X", BaseScale, false);
      this.subText.vLocation.X = this.text.vLocation.X;
      this.subText.vLocation.Y = this.text.GetSize().Y - this.subText.GetSize().Y * 0.5f - uiScaleHelper.ScaleY(2f);
      this.subText.textToWrite = string.Empty;
      this.doubleTextOffsetY = uiScaleHelper.ScaleY(5f);
    }

    public void Darken()
    {
      this.text.SetInactiveColor();
      this.icon.Deactivate();
    }

    public void UpdateCurrentFrustrationDisplay(WalkingPerson person)
    {
      SatisfactionType frustratedNeedsIfAny = person.simperson.memberofthepublic.customerneeds.GetMostFrustratedNeedsIfAny(out float _);
      if (frustratedNeedsIfAny == this.currentFrustration)
        return;
      if (frustratedNeedsIfAny == SatisfactionType.Count)
      {
        this.text.textToWrite = "-";
        this.subText.textToWrite = string.Empty;
      }
      else
      {
        this.text.textToWrite = SatisfactionBarAndText.GetSatisfactionTypeToString(frustratedNeedsIfAny);
        this.subText.textToWrite = frustratedNeedsIfAny != SatisfactionType.Animals ? string.Empty : "Total Animals & Welfare";
      }
      this.icon.SetSatisfactionType(frustratedNeedsIfAny);
    }

    public void DrawCurrentFrustrationDisplay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.icon.DrawCustomerNeedsIcon(offset, spriteBatch);
      if (!string.IsNullOrEmpty(this.subText.textToWrite))
      {
        offset.Y -= this.doubleTextOffsetY;
        this.subText.DrawZGenericText(offset, spriteBatch);
      }
      this.text.DrawZGenericText(offset, spriteBatch);
    }
  }
}
