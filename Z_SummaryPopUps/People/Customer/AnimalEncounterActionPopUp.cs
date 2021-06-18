// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.AnimalEncounterActionPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class AnimalEncounterActionPopUp : CustomerActionPopUp
  {
    private AnimalEncounterActionOptions options;
    private AnimalEncounterPenStats penstats;
    private bool hascheckbox;
    private bool applyall;
    private int thisindex;
    private bool inPenPicker;
    private bool penPicked;
    private Vector2 shortframescale;

    public AnimalEncounterActionPopUp(WalkingPerson thisperson, float basescale_)
      : base(basescale_)
    {
      this.inPenPicker = false;
      this.options = new AnimalEncounterActionOptions(thisperson, this.basescale);
      this.penstats = new AnimalEncounterPenStats(this.basescale);
      this.framescale = this.options.GetSize();
      this.framescale.X = Math.Max(this.framescale.X, this.penstats.GetSize().X);
      this.shortframescale = this.framescale;
      this.framescale.Y += this.penstats.GetSize().Y + this.pad.Y;
      this.SizeFrame();
      Vector2 vector2 = -0.5f * this.framescale;
      this.options.location = vector2 + 0.5f * this.options.GetSize();
      vector2.Y += this.options.GetSize().Y + this.pad.Y;
      this.penstats.location = vector2 + 0.5f * this.penstats.GetSize();
      this.penstats.location.X = 0.0f;
      vector2.Y += this.penstats.GetSize().Y;
    }

    public override bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      int num = 0;
      if (!this.options.UpdateAnimalEncounterActionOptions(player, offset, DeltaTime))
        return num != 0;
      this.inPenPicker = !this.inPenPicker;
      this.options.SetMode(this.inPenPicker);
      this.hidemainpanel = !this.hidemainpanel;
      this.penPicked = this.inPenPicker;
      this.penstats.SetMode(this.penPicked);
      return num != 0;
    }

    public override void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.options.DrawAnimalEncounterActionOptions(spritebatch, offset);
      this.penstats.DrawAnimalEncounterPenStats(spritebatch, offset);
    }
  }
}
