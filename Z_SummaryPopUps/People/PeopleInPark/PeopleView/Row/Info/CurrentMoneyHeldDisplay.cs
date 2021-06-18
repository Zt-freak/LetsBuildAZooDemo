// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info.CurrentMoneyHeldDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info
{
  internal class CurrentMoneyHeldDisplay
  {
    public Vector2 location;
    private ZGenericText text;

    public CurrentMoneyHeldDisplay(float BaseScale)
    {
      this.text = new ZGenericText("-", BaseScale, false, _UseOnePointFiveFont: true);
      this.text.vLocation.Y -= this.text.GetSize().Y * 0.5f;
      this.text.vLocation.X += 26f * BaseScale;
    }

    public void Darken() => this.text.SetInactiveColor();

    public void UpdateCurrentMoneyHeldDisplay(WalkingPerson person, Vector2 offset)
    {
      offset += this.location;
      if (!person.IsAtive)
        this.text.textToWrite = "-";
      else
        this.text.textToWrite = "$" + (object) person.simperson.memberofthepublic.CashHeld;
    }

    public void DrawCurrentMoneyHeldDisplay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.text.DrawZGenericText(offset, spriteBatch);
    }
  }
}
