// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Ani_MAll.ShopTop.ShopTopManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_StoreRoom.Ani_MAll.ShopTop
{
  internal class ShopTopManager
  {
    private GameObject Bar;
    private Vector2 BARVSCALE;
    private BackButton close;

    public ShopTopManager()
    {
      this.Bar = new GameObject();
      this.Bar.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.BARVSCALE = new Vector2(1024f, 50f);
      this.Bar.SetAllColours(0.8392157f, 0.3568628f, 0.2980392f);
      this.close = new BackButton();
    }

    public bool UpdateShopTopManager(Player player, float DeltaTime) => this.close.UpdateBackButton(player, DeltaTime);

    public void DrawShopTopManager(Vector2 Offset)
    {
      this.Bar.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.BARVSCALE);
      TextFunctions.DrawTextWithDropShadow("aniMALL", 1.5f * Sengine.ScreenRationReductionMultiplier.Y, new Vector2(20f, 10f), new Color(38, 62, 132), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03, false);
      TextFunctions.DrawTextWithDropShadow("the #1 animal food supplier", 0.8f * Sengine.ScreenRationReductionMultiplier.Y, new Vector2(200f * Sengine.ScreenRationReductionMultiplier.Y, 20f), Color.White, 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03, false);
      this.close.DrawBackButton(Offset);
    }
  }
}
