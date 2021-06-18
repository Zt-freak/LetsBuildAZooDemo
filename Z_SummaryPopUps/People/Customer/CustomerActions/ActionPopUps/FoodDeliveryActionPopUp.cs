// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps.FoodDeliveryActionPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps
{
  internal class FoodDeliveryActionPopUp : CustomerActionPopUp
  {
    private FoodDeliveryOptionsPanel foodoptions;

    public FoodDeliveryActionPopUp(float basescale_)
      : base(basescale_)
    {
      this.foodoptions = new FoodDeliveryOptionsPanel(this.basescale);
      this.framescale = this.foodoptions.GetSize();
    }

    public override bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return (0 | (this.foodoptions.UpdateFoodDeliveryOptionsPanel(player, offset, DeltaTime) ? 1 : 0)) != 0;
    }

    public override void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.foodoptions.DrawFoodDeliveryOptionsPanel(spritebatch, offset);
    }
  }
}
