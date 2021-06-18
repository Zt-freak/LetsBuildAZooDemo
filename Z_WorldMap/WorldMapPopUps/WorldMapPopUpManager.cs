// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.WorldMapPopUpManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests;
using TinyZoo.Z_WorldMap.WorldMapPopUps.Shelter;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps
{
  internal class WorldMapPopUpManager
  {
    public PopUpState popupstate;
    private PopUpState lastPopupstate;
    private AnimalQuestPopupManager animalQuestPopup;
    private AnimalShelterPopUpManager animalShelterPopup;
    private ZUIPointerArrow pointerArrow;
    private float BaseScale;

    public WorldMapPopUpManager()
    {
      this.popupstate = PopUpState.None;
      this.lastPopupstate = PopUpState.None;
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
    }

    public bool CheckMouseOver(Player player) => this.animalQuestPopup != null && (this.animalQuestPopup.CheckMouseOver(player) || !this.animalQuestPopup.AllowExternalClosing()) || this.animalShelterPopup != null && this.animalShelterPopup.CheckMouseOver(player);

    public void SetNewSelection(CityName cityname, Player player, Vector2 worldSpaceLocation)
    {
      Vector2 screenSpace = RenderMath.TranslateWorldSpaceToScreenSpace(worldSpaceLocation);
      Vector2 pointHere = Vector2.Zero;
      if (this.popupstate != PopUpState.None)
        return;
      if (cityname == CityName.Shelter)
      {
        this.popupstate = PopUpState.Shelter;
        this.animalShelterPopup = new AnimalShelterPopUpManager(player);
        this.animalShelterPopup.location = this.GetPositionForPanel(screenSpace, this.animalShelterPopup.GetSize(), this.animalShelterPopup.GetPositionOffset(), out pointHere);
      }
      else
      {
        this.popupstate = PopUpState.Quest;
        this.animalQuestPopup = new AnimalQuestPopupManager(cityname, player);
        this.animalQuestPopup.location = this.GetPositionForPanel(screenSpace, this.animalQuestPopup.GetSize(), this.animalQuestPopup.GetPositionOffset(), out pointHere);
        if (FeatureFlags.BlockBuild && cityname == CityName.Sydney && player.Stats.variantsfound.GetTotalVaiantsFound(AnimalType.Rabbit) == 0)
          FeatureFlags.BlockBuild = false;
      }
      this.pointerArrow = new ZUIPointerArrow(this.BaseScale, screenSpace, pointHere);
    }

    private Vector2 GetPositionForPanel(
      Vector2 cityScreenSpaceLocation,
      Vector2 panelSize,
      Vector2 panelPositionOffset,
      out Vector2 pointHere)
    {
      float num1 = 70f;
      float num2 = 50f;
      Vector2 vector2;
      vector2.X = (double) cityScreenSpaceLocation.X >= 512.0 ? (float) ((double) cityScreenSpaceLocation.X - (double) num1 - (double) panelSize.X * 0.5) : (float) ((double) cityScreenSpaceLocation.X + (double) num1 + (double) panelSize.X * 0.5);
      vector2.Y = (double) cityScreenSpaceLocation.Y >= 384.0 ? (float) ((double) cityScreenSpaceLocation.Y - (double) num2 - (double) panelSize.Y * 0.5) : (float) ((double) cityScreenSpaceLocation.Y + (double) num2 + (double) panelSize.Y * 0.5);
      vector2.X = MathHelper.Clamp(vector2.X, panelSize.X * 0.5f, (float) (1024.0 - (double) panelSize.X * 0.5));
      vector2.Y = MathHelper.Clamp(vector2.Y, panelSize.Y * 0.5f - panelPositionOffset.Y, (float) (768.0 - (double) panelSize.Y * 0.5) - panelPositionOffset.Y);
      float num3 = 2f * this.BaseScale;
      float num4 = (float) (-(double) panelSize.X * 0.5 + (double) panelPositionOffset.X * 0.5) + num3;
      float num5 = (float) (-(double) panelSize.Y * 0.5);
      pointHere = vector2;
      if ((double) vector2.X < (double) cityScreenSpaceLocation.X)
        num4 *= -1f;
      if ((double) vector2.Y < (double) cityScreenSpaceLocation.Y)
        num5 += panelSize.Y * 0.9f;
      pointHere.X += num4;
      pointHere.Y += num5;
      return vector2;
    }

    public void UpdateWorldMapPopUpManager(
      float DeltaTime,
      Player player,
      bool MouseIsOverPopUpPanel)
    {
      if (!MouseIsOverPopUpPanel && (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0)
      {
        this.popupstate = PopUpState.None;
        this.animalQuestPopup = (AnimalQuestPopupManager) null;
        this.animalShelterPopup = (AnimalShelterPopUpManager) null;
      }
      if (this.popupstate != PopUpState.None && this.pointerArrow.IsDoneLerpingIn())
      {
        switch (this.popupstate)
        {
          case PopUpState.Shelter:
            if (this.animalShelterPopup.UpdateAnimalShelterPopUpManager(player, DeltaTime))
            {
              this.animalShelterPopup = (AnimalShelterPopUpManager) null;
              this.popupstate = PopUpState.None;
              if (FeatureFlags.NewAnimalGot)
              {
                this.ExitToOverworld();
                break;
              }
              break;
            }
            break;
          case PopUpState.Quest:
            bool exitToOverworld;
            if (this.animalQuestPopup.UpdateAnimalQuestPopupManager(player, DeltaTime, out exitToOverworld))
            {
              if (exitToOverworld)
              {
                Z_GameFlags.JustExitedWorldMap_CheckAnimalExistsInAnimalPanel = true;
                this.ExitToOverworld();
                break;
              }
              this.animalQuestPopup = (AnimalQuestPopupManager) null;
              this.popupstate = PopUpState.None;
              break;
            }
            break;
        }
      }
      if (this.lastPopupstate != PopUpState.None && this.popupstate == PopUpState.None)
        this.pointerArrow.LerpOut_Bloop();
      if (this.pointerArrow != null)
        this.pointerArrow.UpdateZUIPointerArrow(DeltaTime);
      this.lastPopupstate = this.popupstate;
    }

    public void ExitToOverworld()
    {
      TinyZoo.Game1.screenfade.BeginFade(true);
      TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorld);
      ParkGate.Reset();
    }

    public void DrawWorldMapPopUpManager()
    {
      if (this.popupstate != PopUpState.None && this.pointerArrow.IsDoneLerpingIn())
      {
        switch (this.popupstate)
        {
          case PopUpState.Shelter:
            this.animalShelterPopup.DrawAnimalShelterPopUpManager();
            break;
          case PopUpState.Quest:
            this.animalQuestPopup.DrawAnimalQuestPopupManager();
            break;
        }
      }
      if (this.pointerArrow == null)
        return;
      this.pointerArrow.DrawZUIPointerArrow(Vector2.Zero, AssetContainer.pointspritebatch01);
    }
  }
}
