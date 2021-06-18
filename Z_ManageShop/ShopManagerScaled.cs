// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.ShopManagerScaled
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageShop.BaseScaled;
using TinyZoo.Z_ManageShop.SalesManager;

namespace TinyZoo.Z_ManageShop
{
  internal class ShopManagerScaled
  {
    private BigBrownPanel BigFrame;
    private Vector2 Location;
    private FoodBar_New foodbarnew;
    private ShopSalesSummary shopsalessummary;

    public ShopManagerScaled(float BaseScale, Player player)
    {
      this.BigFrame = new BigBrownPanel(Vector2.Zero, true, "Manage Shop", BaseScale);
      this.Location = new Vector2(512f, 400f);
      this.foodbarnew = new FoodBar_New(player, BaseScale);
      this.shopsalessummary = new ShopSalesSummary(player, BaseScale);
      Vector2 size = this.foodbarnew.GetSize();
      this.foodbarnew.Position.Y = size.Y * 0.5f;
      this.shopsalessummary.Location.Y = this.foodbarnew.Position.Y;
      this.shopsalessummary.Location.Y += (float) ((double) size.Y * 0.5 + 10.0 * (double) BaseScale);
      this.shopsalessummary.Location.Y += this.shopsalessummary.GetSize().Y * 0.5f;
      size.Y += 10f * BaseScale;
      size.Y += this.shopsalessummary.GetSize().Y;
      this.shopsalessummary.Location.Y -= size.Y * 0.5f;
      this.foodbarnew.Position.Y -= size.Y * 0.5f;
      this.BigFrame.Finalize(size);
      if (this.foodbarnew.foodviewstuff.extraingredient == null)
        return;
      this.shopsalessummary.SetExtraIngredient(this.foodbarnew.foodviewstuff.extraingredient.foodslider.dragandbar.CurrentDragPercent);
    }

    public void CheckAddToShopLedgerOnExit(Player player) => this.foodbarnew.CheckAddToShopLedgerOnExit(player);

    public bool UpdateShopManagerScaled(Player player, float DeltaTime)
    {
      this.foodbarnew.UpdateFoodBar_New(player, this.Location, DeltaTime, out bool _);
      if (this.foodbarnew.foodviewstuff.ExtraIngredientChanged)
      {
        this.foodbarnew.foodviewstuff.ExtraIngredientChanged = false;
        this.shopsalessummary.SetExtraIngredient(this.foodbarnew.foodviewstuff.extraingredient.foodslider.dragandbar.CurrentDragPercent);
      }
      return this.BigFrame.UpdatePanelCloseButton(player, DeltaTime, this.Location);
    }

    public void DrawShopManagerScaled()
    {
      this.BigFrame.DrawBigBrownPanel(this.Location);
      this.foodbarnew.DrawFoorBar_New(this.Location);
      this.shopsalessummary.DrawSalesSummary(this.Location);
    }
  }
}
