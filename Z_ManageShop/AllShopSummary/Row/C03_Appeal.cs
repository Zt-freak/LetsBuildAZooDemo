// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.AllShopSummary.Row.C03_Appeal
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood;
using TinyZoo.Z_ManageShop.Shop_Data;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManageShop.AllShopSummary.Row
{
  internal class C03_Appeal
  {
    private TopTextMini toptextmini;
    private TopTextMini toptextminiTwo;
    private StarBar starbar;
    public Vector2 Location;

    public C03_Appeal(
      float HeightForText,
      ShopEntry shopentry,
      float MidTextHeight,
      float BaseScale,
      Player player)
    {
      float popularity = ShopData.GetPopularity(shopentry.tiletype, player);
      this.toptextmini = new TopTextMini("Popularity", BaseScale, HeightForText, false, true);
      this.toptextmini.CenterJustify = true;
      this.starbar = new StarBar(popularity * 5f, 0.5f);
      this.starbar.Location.Y = MidTextHeight;
      this.toptextminiTwo = new TopTextMini(string.Concat((object) Math.Round((double) popularity * 100.0)), BaseScale, MidTextHeight);
      this.toptextminiTwo.CenterJustify = true;
    }

    public void DrawColumn(Vector2 Offset)
    {
      Offset += this.Location;
      this.toptextmini.DrawTopTextMini(Offset);
      this.starbar.DrawStarBar(Offset, AssetContainer.pointspritebatchTop05);
    }
  }
}
