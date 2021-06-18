// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.IAPInfo.IAPStatusManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Objects;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverworldSelectedThing.SelectedPersonUI.SelectionPanel;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.Tutorials;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.OverWorld.IAPInfo
{
  internal class IAPStatusManager
  {
    private static GameObject Goat;
    private static GameObject Vortex;
    private static GameObject Flower;
    private LerpHandler_Float lerper;
    private GameObject COllectionBut;
    private LerpHandler_Float Collectionlerper;
    private TRC_ButtonDisplay controllerButton;
    private GameObject RankBut;
    private GameObject Medal;
    private GameObject EventButton;

    public IAPStatusManager(Player player)
    {
      IAPStatusManager.Goat = new GameObject();
      IAPStatusManager.Goat.DrawRect = new Rectangle(179, 70, 20, 17);
      IAPStatusManager.Goat.SetDrawOriginToCentre();
      this.Collectionlerper = new LerpHandler_Float();
      IAPStatusManager.Vortex = new GameObject();
      IAPStatusManager.Vortex.DrawRect = new Rectangle(158, 70, 20, 17);
      IAPStatusManager.Vortex.SetDrawOriginToCentre();
      IAPStatusManager.Goat.bActive = player.Stats.ADisabled(true, player);
      IAPStatusManager.Flower = new GameObject();
      IAPStatusManager.Flower.DrawRect = new Rectangle(252, 69, 20, 17);
      IAPStatusManager.Flower.SetDrawOriginToCentre();
      IAPStatusManager.Flower.bActive = player.Stats.GetFlower();
      IAPStatusManager.Vortex.bActive = player.Stats.Vortex();
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, -1f, -1f, 3f);
      IAPStatusManager.Vortex.scale = 2f;
      IAPStatusManager.Goat.scale = 2f;
      IAPStatusManager.Flower.scale = 2f;
      this.COllectionBut = new GameObject();
      this.COllectionBut.DrawRect = new Rectangle(269, 187, 24, 26);
      this.COllectionBut.SetDrawOriginToCentre();
      float pixelSizeBestMatch = RenderMath.GetPixelSizeBestMatch(2f);
      if (TinyZoo.GameFlags.MobileUIScale)
        pixelSizeBestMatch = RenderMath.GetPixelSizeBestMatch(2f * Sengine.UltraWideSreenDownardsMultiplier);
      float num = pixelSizeBestMatch;
      if (DebugFlags.IsPCVersion)
        num = RenderMath.GetPixelSizeBestMatch(1f);
      this.COllectionBut.vLocation = new Vector2(40f, 13f * num * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.COllectionBut.vLocation.Y += 4f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.COllectionBut.scale = num;
      this.controllerButton = new TRC_ButtonDisplay(2f);
      this.controllerButton.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, ControllerButton.XboxY);
      this.Collectionlerper = new LerpHandler_Float();
      this.RankBut = new GameObject();
      this.RankBut.DrawRect = new Rectangle(280, 101, 25, 26);
      this.RankBut.vLocation = new Vector2(80f, 13f * pixelSizeBestMatch * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.RankBut.scale = this.COllectionBut.scale;
      this.RankBut.SetDrawOriginToCentre();
      this.Medal = IAPStatusManager.GetMedalIcon(player);
      this.EventButton = new GameObject(this.RankBut);
      this.EventButton.DrawRect = new Rectangle(302, 128, 25, 26);
      this.EventButton.vLocation.X = 200f;
      player.Stats.research.HasThisAnimalBeenResearched(AnimalType.Walrus);
    }

    internal static GameObject GetMedalIcon(Player player)
    {
      WardenRank currentRank = ProfitLadderData.GetCurrentRank(player, out float _, out float _, out int _);
      GameObject gameObject = new GameObject();
      gameObject.DrawRect = ProfitLadderData.GetRankData(currentRank).DrawRectForIcon;
      gameObject.SetDrawOriginToCentre();
      return gameObject;
    }

    public void UpdateIAPStatusManager(float DeltaTime, Player player)
    {
      this.Collectionlerper.UpdateLerpHandler(DeltaTime);
      if (TinyZoo.GameFlags.IsUsingController && SellUIManager.selectedtileandsell != null || SelectedPersonInfo.SelectedPersonPanelUpdatedThisFrame)
      {
        if ((double) this.Collectionlerper.TargetValue != -1.0)
          this.Collectionlerper.SetLerp(false, 0.0f, -1f, 3f, true);
      }
      else if ((double) this.Collectionlerper.TargetValue != 0.0)
        this.Collectionlerper.SetLerp(false, 0.0f, 0.0f, 3f, true);
      SelectedPersonInfo.SelectedPersonPanelUpdatedThisFrame = false;
      if (OverWorldManager.overworldstate == OverWOrldState.MainMenu && TutorialManager.currenttutorial == TUTORIALTYPE.None)
      {
        if ((double) this.lerper.TargetValue != 0.0)
          this.lerper.SetLerp(false, 0.0f, 0.0f, 3f, true);
      }
      else if ((double) this.lerper.TargetValue != -1.0)
        this.lerper.SetLerp(false, -1f, -1f, 3f, true);
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value != 0.0 || (double) this.Collectionlerper.Value != 0.0 || (OWHUDManager.transferselection != null || OWHUDManager.reseachsummary != null) || (OWHUDManager.popuppanel != null || !player.livestats.HasActiveEvent()))
        return;
      if ((double) FlashingAlpha.Medium.fAlpha > 0.5)
        this.EventButton.DrawRect = new Rectangle(302, 128, 25, 26);
      else
        this.EventButton.DrawRect = new Rectangle(332, 101, 25, 26);
    }

    public void DRawIAPStatusManager()
    {
      Vector2 vector2 = new Vector2(0.0f, this.lerper.Value * 150f);
      if (!TinyZoo.GameFlags.IsConsoleVersion)
      {
        if (IAPStatusManager.Vortex.bActive)
        {
          IAPStatusManager.Vortex.scale = 1f;
          IAPStatusManager.Vortex.vLocation = new Vector2(970f, 20f * Sengine.ScreenRatioUpwardsMultiplier.Y) + vector2;
          IAPStatusManager.Vortex.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
        }
        if (IAPStatusManager.Goat.bActive)
        {
          IAPStatusManager.Goat.scale = 1f;
          IAPStatusManager.Goat.vLocation = new Vector2(940f, 20f * Sengine.ScreenRatioUpwardsMultiplier.Y) + vector2;
          IAPStatusManager.Goat.Draw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
        }
        if (IAPStatusManager.Flower.bActive)
        {
          IAPStatusManager.Flower.scale = 1f;
          IAPStatusManager.Flower.vLocation = new Vector2(910f, 20f * Sengine.ScreenRatioUpwardsMultiplier.Y) + vector2;
          IAPStatusManager.Flower.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
        }
      }
      vector2.Y += this.Collectionlerper.Value * 400f;
    }
  }
}
