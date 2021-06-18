// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BarMenu.Land.Z_BuyLand
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.OverWorld;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BalanceSystems.Costs;
using TinyZoo.Z_BuldMenu.BuyLand;
using TinyZoo.Z_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_OverWorld.AvatarUI.Selection;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.Z_BarMenu.Land
{
  internal class Z_BuyLand
  {
    private MainBarManager barmanager;
    private UnlockSectorPreview unlocksectorpreview;
    private Vector2Int SelectedSector;
    private ConfirmationDialog confirmdialog;
    private float basescale;

    public Z_BuyLand(Player player, Vector2Int SelectedLocation)
    {
      this.basescale = Z_GameFlags.GetBaseScaleForUI();
      this.SelectedSector = new Vector2Int(SelectedLocation.X / TileMath.SectorSize, SelectedLocation.Y / TileMath.SectorSize);
      this.unlocksectorpreview = new UnlockSectorPreview(SelectedLocation);
      this.barmanager = new MainBarManager(BAR_TYPE.BuyMoreLand, TILETYPE.None, false, player);
      SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
    }

    public bool UpdateZ_BuyLand(
      Player player,
      float DeltaTime,
      OverWorldEnvironmentManager overworldManager)
    {
      Z_GameFlags.ForceRightAndLeftMouseDrag = true;
      bool GoBack;
      BuildingManageButton buildingManageButton = this.barmanager.UpdateMainBarManager(DeltaTime, player, out GoBack);
      OverwoldMainButtons.MouseIsOverAButton = this.barmanager.CheckMouseOver(player);
      if (this.confirmdialog == null)
      {
        if (GoBack)
          return true;
        if (buildingManageButton != BuildingManageButton.Count)
        {
          int newLandCost = CostData.GetNewLandCost(player);
          int cashHeld = player.Stats.GetCashHeld();
          this.confirmdialog = newLandCost > cashHeld ? new ConfirmationDialog("Buy Land", "You do not have enough to buy this land for $" + newLandCost.ToString() + ".", this.basescale, true) : new ConfirmationDialog("Buy Land", "Are you sure you want to buy this land for $" + newLandCost.ToString() + "?", this.basescale);
          this.confirmdialog.location = Sengine.HalfReferenceScreenRes;
        }
        else if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && this.barmanager.LerpOnComplete())
          return true;
      }
      else
      {
        int newLandCost = CostData.GetNewLandCost(player);
        int cashHeld = player.Stats.GetCashHeld();
        this.confirmdialog.SetDisabled(newLandCost > cashHeld);
        bool confirmed;
        if (this.confirmdialog.UpdateConfirmationDialog(player, Vector2.Zero, DeltaTime, out confirmed))
        {
          this.confirmdialog = (ConfirmationDialog) null;
          if (!confirmed)
            return false;
          AddForSaleSigns.RemoveSign(player, this.SelectedSector);
          ZMapSetUp.UnlockThisSector(player, this.SelectedSector, overworldManager);
          QuestScrubber.ScrubOnZooSIzeIncreaseBuyLand(player);
          player.Stats.SpendCash(newLandCost, SpendingCashOnThis.BuyLand, player);
          AddForSaleSigns.AddSigns(player);
          return true;
        }
      }
      return false;
    }

    public void DrawZ_BuyLand(Player player)
    {
      this.unlocksectorpreview.DrawSectorPreviewSet();
      this.barmanager.DrawMainBarManager(player);
      if (this.confirmdialog == null)
        return;
      this.confirmdialog.DrawConfirmationDialog(AssetContainer.pointspritebatchTop05, Vector2.Zero);
    }
  }
}
