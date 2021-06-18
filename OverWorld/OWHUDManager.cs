// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OWHUDManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SpringIAP;
using System;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.OverWorld.PopUps;
using TinyZoo.OverWorld.Research;
using TinyZoo.OverWorld.Transfer.TransferScreen;
using TinyZoo.OverWorld.Transfer.TransferScreen.reanimate;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.HoldingCells;

namespace TinyZoo.OverWorld
{
  internal class OWHUDManager
  {
    internal static PopUpPanel popuppanel;
    internal static ResearchSummary reseachsummary;
    internal static TransferSelectionScreen transferselection;
    private static ReanimatonManager reanimationmanager;
    private LerpHandler_Float OverallLerperForOtherModes;

    public OWHUDManager(Player player)
    {
      OWHUDManager.transferselection = (TransferSelectionScreen) null;
      this.OverallLerperForOtherModes = new LerpHandler_Float();
    }

    internal static void ActivateResearchUI(LayoutEntry layout, Player player) => OWHUDManager.reseachsummary = new ResearchSummary(player);

    internal static bool IsCashOnScreen() => !FeatureFlags.BlockCash;

    internal static void ActivateTransferUI(
      PrisonZone zone,
      HoldingCellInfo holdingcell,
      Player player,
      bool IsReanimate,
      Vector2Int location)
    {
      if (IsReanimate)
        OWHUDManager.reanimationmanager = new ReanimatonManager(player, location);
      else
        OWHUDManager.transferselection = new TransferSelectionScreen(zone, holdingcell, player, IsReanimate, location);
      FeatureFlags.BlockStats = true;
    }

    internal static void DoRevivePopUp(IntakePerson paerson, Player player)
    {
      OWHUDManager.popuppanel = new PopUpPanel("revive this inmate", player);
      OWHUDManager.popuppanel.SetAdvertPopUp(player);
    }

    internal static void DoParolePopUp(IntakePerson paerson, Player player)
    {
      Decimal num1 = (Decimal) (player.Stats.GetTotalDays() - (long) paerson.Birthday);
      Decimal num2 = (Decimal) player.prisonlayout.GetThisAnimal(paerson.UID, out int _).GetimeInZoo();
      int num3 = (int) Math.Ceiling((double) (float) (num2 * (Decimal) paerson.P_PerDay * 0.001M));
      string TextToWrite = string.Format(SEngine.Localization.Localization.GetText(362), (object) EnemyData.GetEnemyTypeName(paerson.animaltype), (object) paerson.Name, (object) num2, (object) num3);
      player.livestats.HCH = num3 * 2;
      Player player1 = player;
      OWHUDManager.popuppanel = new PopUpPanel(TextToWrite, player1, SellOnBlackMarket: true);
    }

    internal static void DoSellBuildingPopUp(
      string Text,
      bool HasTwoButtons,
      bool IsEnclosure,
      Player player)
    {
      OWHUDManager.popuppanel = new PopUpPanel(Text, player, HasTwoButtons, IsSellBuilding: true, IsEnclosure: IsEnclosure);
    }

    internal static void DoPopUp(string Text, bool HasTwoButtons, Player player) => OWHUDManager.popuppanel = new PopUpPanel(Text, player, HasTwoButtons);

    public bool PreUpdateHUD(
      float DeltaTime,
      Player player,
      ref bool GoToCellSelectFromTransfer,
      WallsAndFloorsManager wallsandfloors)
    {
      if (OWHUDManager.popuppanel != null)
      {
        if (!OWHUDManager.popuppanel.UpdatePopUpPanel(DeltaTime, player))
          return true;
        OWHUDManager.popuppanel = (PopUpPanel) null;
      }
      else if (OWHUDManager.reseachsummary != null)
      {
        if (!OWHUDManager.reseachsummary.UpdateResearchSummary(DeltaTime, player))
          return true;
        FeatureFlags.BlockStats = false;
        OWHUDManager.reseachsummary = (ResearchSummary) null;
        if (player.prisonlayout.cellblockcontainer.GetTotalResearch() < 3 && (player.Stats.research.AnimalsResearchedLength() > 2 || player.Stats.research.BuildingsResearched.Count > 2 || player.Stats.research.CellBlocksReseacrhed.Count > 2))
          GameStateManager.tutorialmanager.DoBuildMoreReseacrhPopUp();
      }
      else if (OWHUDManager.reanimationmanager != null)
      {
        if (OWHUDManager.reanimationmanager.UpdateReanimatonManager(DeltaTime, player, wallsandfloors))
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
          OWHUDManager.reanimationmanager = (ReanimatonManager) null;
          FeatureFlags.DemolishEnabled = true;
          FeatureFlags.BlockStats = false;
          OWHUDManager.transferselection = (TransferSelectionScreen) null;
          FeatureFlags.BlockCash = false;
          FeatureFlags.BlockTimer = false;
        }
      }
      else if (OWHUDManager.transferselection != null)
      {
        if (OWHUDManager.transferselection.UpdateTransferSelectionScreen(DeltaTime, player, ref GoToCellSelectFromTransfer))
        {
          FeatureFlags.DemolishEnabled = true;
          FeatureFlags.BlockStats = false;
          OWHUDManager.transferselection = (TransferSelectionScreen) null;
        }
        player.inputmap.ClearAllInput(player);
      }
      return false;
    }

    public void UpdateHUDManager(float DeltaTime, Player player, SpringIAPManager springIAPmanager)
    {
      this.OverallLerperForOtherModes.UpdateLerpHandler(DeltaTime);
      if (OverWorldManager.overworldstate == OverWOrldState.Manage)
      {
        if ((double) this.OverallLerperForOtherModes.TargetValue != -1.0)
          this.OverallLerperForOtherModes.SetLerp(false, 0.0f, -1f, 3f, true);
      }
      else if ((double) this.OverallLerperForOtherModes.TargetValue != 0.0)
        this.OverallLerperForOtherModes.SetLerp(false, 0.0f, 0.0f, 3f, true);
      if (OverWorldManager.IsGameIntro || !player.Stats.NeedsChanged)
        return;
      player.Stats.NeedsChanged = false;
    }

    internal static bool OWPOPUpIsActive() => OWHUDManager.popuppanel != null;

    public void ExitMainMenu()
    {
    }

    public void ReturnToMainMenu()
    {
    }

    public void DrawHUDManager()
    {
      Vector2 vector2 = new Vector2(0.0f, this.OverallLerperForOtherModes.Value * 150f);
      if (OWHUDManager.reseachsummary != null)
        OWHUDManager.reseachsummary.DrawResearchSummary();
      if (OWHUDManager.popuppanel != null)
        OWHUDManager.popuppanel.DrawPopUpPanel();
      if (OWHUDManager.transferselection != null)
        OWHUDManager.transferselection.DrawTransferSelectionScreen();
      if (OWHUDManager.reanimationmanager == null)
        return;
      OWHUDManager.reanimationmanager.DrawReanimatonManager();
    }
  }
}
