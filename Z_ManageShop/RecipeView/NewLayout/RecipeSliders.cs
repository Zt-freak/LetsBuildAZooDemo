// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.RecipeView.NewLayout.RecipeSliders
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_ManageShop.RecipeView.FoodSlidr;
using TinyZoo.Z_ManageShop.Shop_Data;

namespace TinyZoo.Z_ManageShop.RecipeView.NewLayout
{
  internal class RecipeSliders
  {
    private List<FoodSlideManager> foodsliders;
    public Vector2 Location;

    public RecipeSliders(
      ShopStatEntry shopstat,
      ShopStockStatus playershopstockstatus,
      float BaseScale)
    {
      this.foodsliders = new List<FoodSlideManager>();
      if (shopstat.recipe == null)
        return;
      for (int index = 0; index < shopstat.recipe.recipies.Count; ++index)
      {
        if (index == 0)
        {
          this.foodsliders.Add(new FoodSlideManager(shopstat.recipe.recipies[index], 0.5f, playershopstockstatus, index, BaseScale));
          this.foodsliders[index].Position = new Vector2(0.0f, (float) (index * 200));
          this.foodsliders[index].Position.Y += 25f;
        }
      }
    }

    public bool UpdateRecipeSliders(Vector2 Offset, Player player, float DeltaTime)
    {
      bool flag = false;
      Offset += this.Location;
      for (int index = 0; index < this.foodsliders.Count; ++index)
        flag |= this.foodsliders[index].UpdateFoodSlideManager(player, DeltaTime, Offset);
      return flag;
    }

    public void OverrideSlider(Player player, float Movement)
    {
      if (this.foodsliders.Count != 1)
        throw new Exception("DOES NOT SUPPORT MULTIPLES");
      this.foodsliders[0].OverrideSlider(player, Movement);
    }

    public void DrawRecipeSliders(Vector2 Offset)
    {
      Offset += this.Location;
      for (int index = 0; index < this.foodsliders.Count; ++index)
        this.foodsliders[index].DrawFoodSlideManager(Offset);
    }
  }
}
