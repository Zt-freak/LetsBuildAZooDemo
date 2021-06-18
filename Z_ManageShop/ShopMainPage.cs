// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.ShopMainPage
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_ManageShop.RecipeView;
using TinyZoo.Z_ManageShop.Shop_Data;

namespace TinyZoo.Z_ManageShop
{
  internal class ShopMainPage
  {
    private StockAdjuster[] stokadjusters;
    private ShopEntry thisshop;
    private ShopStatsCollection thisstore;
    public RecipeViewManager recipeviewmanaer;
    private LerpHandler_Float lerper;

    public ShopMainPage(Player player, int OverrideIndex = -1)
    {
      this.thisshop = player.shopstatus.GetThisShop(player.livestats.SelectedSHop.Location, player.livestats.SelectedSHop.tiletype);
      this.thisstore = ShopData.GetShopInfo(player.livestats.SelectedSHop.tiletype);
      if (OverrideIndex > -1)
      {
        this.thisshop = player.shopstatus.shopentries[OverrideIndex];
        this.thisstore = ShopData.GetShopInfo(this.thisshop.tiletype);
      }
      this.lerper = new LerpHandler_Float();
      this.stokadjusters = new StockAdjuster[this.thisstore.shopstats.Count];
      for (int index = 0; index < this.thisstore.shopstats.Count; ++index)
      {
        this.stokadjusters[index] = new StockAdjuster(this.thisstore.shopstats[index], this.thisshop.shopstockstatus[index]);
        this.stokadjusters[index].Location = new Vector2(512f, (float) (260.0 + (double) index * (200.0 * (double) Sengine.ScreenRationReductionMultiplier.Y)));
      }
    }

    public void LerpBackOn() => this.lerper.SetLerp(true, 1f, 0.0f, 3f, true);

    public void UpdateShopMainPage(Player player, float DeltaTime, out bool SwitchState)
    {
      SwitchState = false;
      if (this.recipeviewmanaer == null && (double) this.lerper.Value == 0.0)
      {
        for (int index = 0; index < this.stokadjusters.Length; ++index)
        {
          bool GoToStockView;
          this.stokadjusters[index].UpdateStockAdjuster(Vector2.Zero, player, DeltaTime, out GoToStockView);
          if (GoToStockView)
          {
            float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
            this.lerper.SetLerp(true, 0.0f, 1f, 3f, true);
            SwitchState = true;
            this.recipeviewmanaer = new RecipeViewManager(this.thisstore.shopstats[index], this.thisshop.shopstockstatus[index], baseScaleForUi);
          }
        }
      }
      if (this.recipeviewmanaer != null && this.recipeviewmanaer.UpdateRecipeViewManager(DeltaTime, player, Vector2.Zero))
        this.recipeviewmanaer = (RecipeViewManager) null;
      this.lerper.UpdateLerpHandler(DeltaTime);
    }

    public void DrawShopMainPage()
    {
      if (this.recipeviewmanaer != null)
        this.recipeviewmanaer.DrawRecipeViewManager(Vector2.Zero);
      for (int index = 0; index < this.stokadjusters.Length; ++index)
        this.stokadjusters[index].DrawStockAdjuster(new Vector2(this.lerper.Value * 1024f, 0.0f));
    }
  }
}
