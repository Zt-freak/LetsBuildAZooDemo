// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.AllShopSummary.Row.C07_ExtraFood
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood;
using TinyZoo.Z_ManageShop.FoodIcon;
using TinyZoo.Z_ManageShop.Shop_Data;

namespace TinyZoo.Z_ManageShop.AllShopSummary.Row
{
  internal class C07_ExtraFood
  {
    private TopTextMini toptextmini;
    private TopTextMini toptextminiTwo;
    public Vector2 Location;
    private MiniFoodIcon minifood;
    private string miniTwoBaseString;

    public C07_ExtraFood(
      float HeightForText,
      float TextHeightTwo,
      float BaseScale,
      ShopEntry shopEntry)
    {
      FOODTYPE foodtype = FOODTYPE.Count;
      ShopStatsCollection shopInfo = ShopData.GetShopInfo(shopEntry.tiletype);
      if (shopInfo.Seasoning != null)
        foodtype = shopInfo.Seasoning.MinFood_LeftFood;
      this.toptextmini = new TopTextMini("Extra Ingredient", BaseScale, HeightForText, false, true);
      this.toptextmini.SetAsSplit();
      this.toptextmini.CenterJustify = true;
      string empty = string.Empty;
      string WriteMe;
      if (foodtype != FOODTYPE.Count)
      {
        this.miniTwoBaseString = FoodIconData.GetFoodTypeToString(foodtype);
        WriteMe = this.miniTwoBaseString + " 0%";
      }
      else
        WriteMe = "None";
      this.toptextminiTwo = new TopTextMini(WriteMe, BaseScale, TextHeightTwo, false, true);
      this.toptextminiTwo.CenterJustify = true;
      this.toptextminiTwo.SetAsSplit();
      if (foodtype == FOODTYPE.Count)
        return;
      this.minifood = new MiniFoodIcon(foodtype);
      this.minifood.scale = 0.666666f;
      this.minifood.vLocation = new Vector2(-25f, 15f);
      this.toptextminiTwo.vLocation.X = 10f;
    }

    public void SetExtraIngredient(float Perc)
    {
      this.toptextminiTwo.SetNewText(this.miniTwoBaseString + " " + (object) Math.Round((double) Perc * 100.0) + "%");
      this.toptextminiTwo.CenterJustify = true;
      this.toptextminiTwo.SetAsSplit();
    }

    public void DrawColumn(Vector2 Offset)
    {
      Offset += this.Location;
      this.toptextmini.DrawTopTextMini(Offset);
      if (this.minifood != null)
        this.minifood.DrawMiniFoodIcon(Offset);
      this.toptextminiTwo.DrawTopTextMini(Offset);
    }
  }
}
