// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.AllShopSummary.Row.C02_Type
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Tile_Data;
using TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood;
using TinyZoo.Z_ManageShop.Shop_Data;

namespace TinyZoo.Z_ManageShop.AllShopSummary.Row
{
  internal class C02_Type
  {
    private TopTextMini toptextmini;
    private TopTextMini toptextminiTwo;
    public Vector2 Location;

    public C02_Type(float HeightForText, TILETYPE tiletype, float MidTextHeight, float BaseScale)
    {
      this.toptextmini = new TopTextMini("Type", BaseScale, HeightForText, false, true);
      this.toptextmini.CenterJustify = true;
      this.toptextminiTwo = new TopTextMini(ShopData.WhatDoesThisShopSell(tiletype), BaseScale, MidTextHeight);
      this.toptextminiTwo.CenterJustify = true;
    }

    public void DrawColumn(Vector2 Offset)
    {
      Offset += this.Location;
      this.toptextmini.DrawTopTextMini(Offset);
      this.toptextminiTwo.DrawTopTextMini(Offset);
    }
  }
}
