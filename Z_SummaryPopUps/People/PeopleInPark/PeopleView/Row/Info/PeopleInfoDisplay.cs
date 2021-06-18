// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info.PeopleInfoDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info
{
  internal class PeopleInfoDisplay
  {
    public Vector2 location;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    private CurrentActionDisplay currentActionDisplay;
    private CurrentMoneyHeldDisplay moneyDisplay;
    private CurrentFrustrationDisplay frustrationDisplay;
    private VIPhappinessbar VIPhappinessBar;
    private PeopleViewInfoType infoType;

    public PeopleInfoDisplay(float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.infoType = PeopleViewInfoType.Count;
    }

    public void Darken()
    {
      switch (this.infoType)
      {
        case PeopleViewInfoType.Actions:
          this.currentActionDisplay.Darken();
          break;
        case PeopleViewInfoType.Frustrations:
          this.frustrationDisplay.Darken();
          break;
        case PeopleViewInfoType.MoneyHeld:
          this.moneyDisplay.Darken();
          break;
        case PeopleViewInfoType.VIP:
          this.VIPhappinessBar.Darken();
          break;
      }
    }

    public void SetInfoType(PeopleViewInfoType _infoType)
    {
      if (this.infoType == _infoType)
        return;
      this.infoType = _infoType;
      switch (this.infoType)
      {
        case PeopleViewInfoType.Actions:
          this.currentActionDisplay = new CurrentActionDisplay(this.BaseScale);
          break;
        case PeopleViewInfoType.Frustrations:
          this.frustrationDisplay = new CurrentFrustrationDisplay(this.BaseScale);
          break;
        case PeopleViewInfoType.MoneyHeld:
          this.moneyDisplay = new CurrentMoneyHeldDisplay(this.BaseScale);
          break;
        case PeopleViewInfoType.VIP:
          this.VIPhappinessBar = new VIPhappinessbar(this.BaseScale);
          this.VIPhappinessBar.location.X = this.VIPhappinessBar.GetBarSize().X * 0.5f;
          this.VIPhappinessBar.location.X += this.scaleHelper.ScaleX(10f);
          this.VIPhappinessBar.location.Y += this.VIPhappinessBar.GetExtraTextHeight() * 0.5f;
          break;
      }
    }

    public void UpdatePeopleInfoDisplay(Vector2 offset, WalkingPerson person)
    {
      offset += this.location;
      switch (this.infoType)
      {
        case PeopleViewInfoType.Actions:
          this.currentActionDisplay.UpdateCurrentAction(person.simperson, person);
          break;
        case PeopleViewInfoType.Frustrations:
          this.frustrationDisplay.UpdateCurrentFrustrationDisplay(person);
          break;
        case PeopleViewInfoType.MoneyHeld:
          this.moneyDisplay.UpdateCurrentMoneyHeldDisplay(person, offset);
          break;
        case PeopleViewInfoType.VIP:
          this.VIPhappinessBar.UpdateVIPhappinessbar(person);
          break;
      }
    }

    public void DrawPeopleInfoDisplay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      switch (this.infoType)
      {
        case PeopleViewInfoType.Actions:
          this.currentActionDisplay.DrawCurrentAction(offset, spriteBatch);
          break;
        case PeopleViewInfoType.Frustrations:
          this.frustrationDisplay.DrawCurrentFrustrationDisplay(offset, spriteBatch);
          break;
        case PeopleViewInfoType.MoneyHeld:
          this.moneyDisplay.DrawCurrentMoneyHeldDisplay(offset, spriteBatch);
          break;
        case PeopleViewInfoType.VIP:
          this.VIPhappinessBar.DrawVIPhappinessbar(offset, spriteBatch);
          break;
      }
    }
  }
}
