// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.RecipeView.RecipeViewManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_ManageShop.RecipeView.FoodSlidr;
using TinyZoo.Z_ManageShop.RecipeView.RecipeCost;
using TinyZoo.Z_ManageShop.RecipeView.StockStatus;
using TinyZoo.Z_ManageShop.Shop_Data;

namespace TinyZoo.Z_ManageShop.RecipeView
{
  internal class RecipeViewManager
  {
    private RecipeViewManagerV2 recpeviewerNew;
    private bool Exiting;
    private List<FoodSlideManager> foodsliders;
    private LerpHandler_Float lerper;
    private RecipeCostManager recipecostmanager;
    private StockStatusManager stockstatis;
    private ShopStatEntry Ref_shopstat;

    public RecipeViewManager(
      ShopStatEntry shopstat,
      ShopStockStatus playershopstockstatus,
      float BaseScale)
    {
      this.recpeviewerNew = new RecipeViewManagerV2(shopstat, playershopstockstatus, BaseScale);
    }

    public void Exit() => this.recpeviewerNew.Exit();

    public bool UpdateRecipeViewManager(float DeltaTime, Player player, Vector2 Offset) => this.recpeviewerNew.UpdateRecipeViewManagerV2(DeltaTime, player, Offset);

    public void DrawRecipeViewManager(Vector2 Offset) => this.recpeviewerNew.DrawRecipeViewManagerV2(Offset);
  }
}
