// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.ShopManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.Tile_Data;
using TinyZoo.Z_ManageShop.AllShopSummary;
using TinyZoo.Z_ManageShop.RecipeView;

namespace TinyZoo.Z_ManageShop
{
  internal class ShopManager
  {
    private ShopMainPage shopmainpage;
    private StoreBGManager storeBGManager;
    private BackButton backbutton;
    private ScreenHeading screenheading;
    private ScreenHeading ThisShop;
    private AllShopSummaryManager allshopmanager;
    private ShopScreenState shopscreenstate;
    private BlackOut blackout;
    private FoodViewManager foodviewmanager;
    private bool WillSkipSummaryAndExit;
    private ShopManagerScaled shopmamanagerscaled;

    public ShopManager(Player player)
    {
      this.shopmamanagerscaled = new ShopManagerScaled(Z_GameFlags.GetBaseScaleForUI(), player);
      this.blackout = new BlackOut();
      this.blackout.SetAlpha(0.5f);
      this.storeBGManager = new StoreBGManager(IsAutumnal: true);
      this.backbutton = new BackButton();
      this.screenheading = new ScreenHeading("Manage Shop".ToUpper());
      this.shopmainpage = new ShopMainPage(player);
      this.ThisShop = new ScreenHeading(TileData.GetTileStats(player.livestats.SelectedSHop.tiletype).Name.ToUpper(), 100f);
      this.ThisShop.header.vLocation = new Vector2(512f, 50f);
      this.allshopmanager = new AllShopSummaryManager(player);
      if (Z_GameFlags.ForceToNewScreen == ForceToNewScreen.GoSTraightToSpecificShopInfo)
      {
        this.WillSkipSummaryAndExit = true;
        Z_GameFlags.ForceToNewScreen = ForceToNewScreen.None;
        int num = -1;
        for (int index = 0; index < player.shopstatus.shopentries.Count; ++index)
        {
          if (player.livestats.SelectedSHop.Location.CompareMatches(player.shopstatus.shopentries[index].LocationOfThisShop) && player.livestats.SelectedSHop.tiletype == player.shopstatus.shopentries[index].tiletype)
            num = index;
        }
        this.foodviewmanager = new FoodViewManager(player, (float) num);
        this.shopscreenstate = ShopScreenState.OneShop;
        this.allshopmanager.SetShopName(TileData.GetTileStats(player.livestats.SelectedSHop.tiletype).Name.ToUpper());
      }
      this.foodviewmanager = new FoodViewManager(player, Z_GameFlags.GetBaseScaleForUI());
    }

    public void UpdateShopManager(float DeltaTime, Player player)
    {
      float SimulationTime = 0.0f;
      GameStateManager.tutorialmanager.UpdateTutorialManager(ref DeltaTime, ref SimulationTime, player);
      if (this.shopmamanagerscaled.UpdateShopManagerScaled(player, DeltaTime))
      {
        if (this.shopscreenstate == ShopScreenState.OneShop)
        {
          if (this.WillSkipSummaryAndExit)
          {
            if (Game1.GetNextGameState() != GAMESTATE.OverWorld)
            {
              this.shopmamanagerscaled.CheckAddToShopLedgerOnExit(player);
              Game1.ForceSwitchToNextGameState = true;
              Game1.SetNextGameState(GAMESTATE.OverWorld);
            }
          }
          else if (this.foodviewmanager != null)
          {
            this.foodviewmanager = (FoodViewManager) null;
            this.shopscreenstate = ShopScreenState.AllShops;
          }
          else if (this.shopmainpage.recipeviewmanaer != null)
          {
            this.shopmainpage.recipeviewmanaer.Exit();
            this.shopmainpage.LerpBackOn();
            this.backbutton.LerpOn();
          }
          else
            this.shopscreenstate = ShopScreenState.AllShops;
        }
        else
        {
          Game1.SetNextGameState(GAMESTATE.OverWorld);
          Game1.screenfade.BeginFade(true);
        }
      }
    }

    public void DrawShopManager(OverWorldManager overworldmanager, Player player)
    {
      overworldmanager.DrawOverWorldManager(player);
      this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatch03);
      this.shopmamanagerscaled.DrawShopManagerScaled();
    }
  }
}
