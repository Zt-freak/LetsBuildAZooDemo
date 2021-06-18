// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info.VIPhappinessbar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info
{
  internal class VIPhappinessbar
  {
    public Vector2 location;
    private ZGenericText text;
    private SatisfactionBar satisfactionBar;
    private float extraTextHeight;

    public VIPhappinessbar(float BaseScale)
    {
      this.satisfactionBar = new SatisfactionBar(1f, BaseScale);
      this.satisfactionBar.SetFullness(MathStuff.getRandomFloat(0.0f, 1f), DoSetColorBasedOnValue: true);
      this.text = new ZGenericText("Happiness", BaseScale);
      this.extraTextHeight = this.text.GetSize().Y;
      this.text.vLocation.Y -= this.extraTextHeight;
    }

    public float GetExtraTextHeight() => this.extraTextHeight;

    public Vector2 GetBarSize() => this.satisfactionBar.GetSize();

    public void Darken()
    {
      this.satisfactionBar.Darken();
      this.text.SetInactiveColor();
    }

    public void UpdateVIPhappinessbar(WalkingPerson person)
    {
    }

    public void DrawVIPhappinessbar(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.text.DrawZGenericText(offset, spriteBatch);
      this.satisfactionBar.DrawSatisfactionBar(offset, spriteBatch);
    }
  }
}
