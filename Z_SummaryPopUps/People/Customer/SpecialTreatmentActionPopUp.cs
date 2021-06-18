// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.SpecialTreatmentActionPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv.Customers;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class SpecialTreatmentActionPopUp : CustomerActionPopUp
  {
    private VIPServicesPanel services;
    private TextButton button;

    public SpecialTreatmentActionPopUp(WalkingPerson person, float basescale_)
      : base(basescale_)
    {
      this.services = new VIPServicesPanel(person, this.basescale);
      this.button = new TextButton(this.basescale, SEngine.Localization.Localization.GetText(935), 40f);
      this.framescale = 2f * this.pad;
      this.framescale.X += this.uiscale.ScaleX(180f);
      this.framescale.Y = this.services.GetSize().Y;
      this.framescale.Y += this.button.GetSize_True().Y + this.pad.Y;
      this.SizeFrame();
      Vector2 vector2 = -0.5f * this.framescale;
      this.services.location = vector2 + 0.5f * this.services.GetSize();
      vector2.Y += this.services.GetSize().Y + this.pad.Y;
      this.button.vLocation = vector2;
      TextButton button = this.button;
      button.vLocation = button.vLocation + 0.5f * this.button.GetSize_True();
      this.button.vLocation.X = 0.0f;
    }

    public override bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      this.services.UpdateVIPServicesPanel(player, offset, DeltaTime);
      return (0 | (this.button.UpdateTextButton(player, offset, DeltaTime) ? 1 : 0)) != 0;
    }

    public override void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.services.DrawVIPServicesPanel(spritebatch, offset);
      this.button.DrawTextButton(offset, 1f, spritebatch);
    }
  }
}
