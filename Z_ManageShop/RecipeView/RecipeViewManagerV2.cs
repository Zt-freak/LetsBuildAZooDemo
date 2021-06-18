// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.RecipeView.RecipeViewManagerV2
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_ManageShop.RecipeView.FoodSlidr;
using TinyZoo.Z_ManageShop.Shop_Data;

namespace TinyZoo.Z_ManageShop.RecipeView
{
  internal class RecipeViewManagerV2
  {
    private ShopStatEntry Ref_shopstat;
    private List<FoodSlideManager> foodsliders;
    private LerpHandler_Float lerper;
    private bool Exiting;

    public RecipeViewManagerV2(
      ShopStatEntry shopstat,
      ShopStockStatus playershopstockstatus,
      float BaseScale)
    {
      this.Ref_shopstat = shopstat;
      this.foodsliders = new List<FoodSlideManager>();
      for (int index = 0; index < shopstat.recipe.recipies.Count; ++index)
      {
        this.foodsliders.Add(new FoodSlideManager(shopstat.recipe.recipies[index], Sengine.ScreenRationReductionMultiplier.Y * 0.5f, playershopstockstatus, index, BaseScale));
        this.foodsliders[index].Position = new Vector2(256f, (float) (400 + index * 100));
      }
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
    }

    public void Exit()
    {
      if (this.Exiting)
        return;
      this.Exiting = true;
      this.lerper.SetLerp(false, 0.0f, 1f, 3f);
    }

    public bool UpdateRecipeViewManagerV2(float DeltaTime, Player player, Vector2 Offset)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      Offset.X += this.lerper.Value * 1024f;
      for (int index = 0; index < this.foodsliders.Count; ++index)
      {
        if (this.foodsliders[index].UpdateFoodSlideManager(player, DeltaTime, Offset))
          throw new Exception("LOG HAS CHNAGED - IF IS CHANGING SETTINGS SO YOU CAN ADD TO SHOP RECORD");
      }
      float minStockCost = (float) this.Ref_shopstat.MinStockCost;
      float currentDragPercent = this.foodsliders[0].dragandbar.CurrentDragPercent;
      float num = (double) currentDragPercent <= 0.5 ? (float) ((double) currentDragPercent * 2.0 * (double) minStockCost * 0.75 + (double) minStockCost * 0.25) : minStockCost + (float) (((double) currentDragPercent - 0.5) * 4.0) * minStockCost;
      return this.Exiting && (double) this.lerper.Value == 1.0;
    }

    public void DrawRecipeViewManagerV2(Vector2 Offset)
    {
      Offset.X += this.lerper.Value * 1024f;
      for (int index = 0; index < this.foodsliders.Count; ++index)
        this.foodsliders[index].DrawFoodSlideManager(Offset);
    }
  }
}
