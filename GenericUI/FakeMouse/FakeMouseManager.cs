// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.FakeMouse.FakeMouseManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.PlayerDir;
using TinyZoo.Tutorials;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.GenericUI.FakeMouse
{
  internal class FakeMouseManager
  {
    private GameObject mousepointer;
    private List<MHIST> history;
    private float TIMER;
    private TRC_ButtonDisplay ControllerButton;
    private Vector2 mouseloc;
    private GameObject mousepointerWhite;
    private bool LocalDragVarialble;
    private DroneAI droneAI;

    public FakeMouseManager()
    {
      this.droneAI = new DroneAI();
      this.mouseloc = new Vector2(512f, 384f);
      this.mousepointer = new GameObject();
      this.mousepointer.DrawRect = new Rectangle(148, 35, 18, 16);
      this.history = new List<MHIST>();
      this.mousepointer.scale = 2f;
      this.mousepointer.SetDrawOriginToCentre();
      this.ControllerButton = new TRC_ButtonDisplay(3.5f);
      this.ControllerButton.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, SEngine.ControllerButton.XboxX);
      this.mousepointerWhite = new GameObject(this.mousepointer);
      this.mousepointerWhite.DrawRect.X = 139;
      this.mousepointerWhite.DrawRect.Y = 70;
      this.mousepointer.DrawRect = new Rectangle(200, 155, 21, 21);
      this.mousepointer.SetDrawOriginToCentre();
    }

    public void UpdateFakeMouseManager(
      float DeltaTime,
      Player player,
      OverWorldEnvironmentManager overworldmanager)
    {
      this.LocalDragVarialble = LiveStats.DraggingDragZone;
      if (TinyZoo.Game1.gamestate == GAMESTATE.OverWorld && OverWorldManager.overworldstate == OverWOrldState.CellSelect)
      {
        this.TIMER -= DeltaTime;
        if ((double) this.TIMER < 0.0)
        {
          this.TIMER = 0.0125f;
          this.history.Add(new MHIST(Sengine.ReferenceScreenRes * 0.5f));
        }
        for (int index = this.history.Count - 1; index > -1; --index)
        {
          this.history[index].Alpha -= DeltaTime;
          if ((double) this.history[index].Alpha <= 0.0)
            this.history.RemoveAt(index);
        }
      }
      else if (this.IsShowingMouse() || OverWorldManager.overworldstate == OverWOrldState.Build && ThingToBuildManager.placetype == PlaceType.PlacingCellBlock)
      {
        if (TinyZoo.GameFlags.IsUsingController)
        {
          if (player.inputmap.PressedThisFrame[15])
          {
            if (OverWorldManager.overworldstate == OverWOrldState.GraveYard)
              player.player.touchinput.MultiTouchTapArray[0] = this.mouseloc;
            else
              player.player.touchinput.ReleaseTapArray[0] = this.mouseloc;
            player.player.touchinput.MultiTouchTapArray[0] = this.mouseloc;
          }
          if (player.inputmap.HeldButtons[15])
            player.player.touchinput.MultiTouchTouchLocations[0] = this.mouseloc;
        }
        float num = 300f;
        if (player.inputmap.HeldButtons[25])
          num = 900f;
        this.mouseloc += player.inputmap.Movementstick * Sengine.ScreenRatioUpwardsMultiplier * DeltaTime * num;
        this.mouseloc.X = MathHelper.Clamp(this.mouseloc.X, 0.0f, 1024f);
        this.mouseloc.Y = MathHelper.Clamp(this.mouseloc.Y, 0.0f, 768f);
        this.TIMER -= DeltaTime;
        if ((double) this.TIMER < 0.0)
        {
          this.TIMER = 0.0125f;
          this.history.Add(new MHIST(this.mouseloc));
        }
        for (int index = this.history.Count - 1; index > -1; --index)
        {
          this.history[index].Alpha -= DeltaTime;
          if ((double) this.history[index].Alpha <= 0.0)
            this.history.RemoveAt(index);
        }
      }
      else if (this.history.Count > 0)
        this.history = new List<MHIST>();
      LiveStats.DraggingDragZone = false;
      this.droneAI.UpdateDroneAI(DeltaTime, overworldmanager);
    }

    private bool IsShowingMouse()
    {
      if (!OverWorldManager.IsGameIntro && OWHUDManager.reseachsummary == null && (OWHUDManager.transferselection == null && !TinyZoo.GameFlags.IsUsingMouse) && (!OWHUDManager.OWPOPUpIsActive() && TinyZoo.GameFlags.IsUsingController && TutorialManager.currenttutorial == TUTORIALTYPE.None))
      {
        switch (OverWorldManager.overworldstate)
        {
          case OverWOrldState.MainMenu:
          case OverWOrldState.GraveYard:
            if (!this.LocalDragVarialble)
              return true;
            break;
          case OverWOrldState.Build:
            if (ThingToBuildManager.placetype != PlaceType.PlacingCellBlock)
              break;
            goto case OverWOrldState.MainMenu;
        }
      }
      return false;
    }

    public void DrawFakeMouseManager(Player player)
    {
      if (TinyZoo.GameFlags.PhotoMode)
        return;
      bool flag = false;
      if (TinyZoo.GameFlags.IsUsingController)
      {
        if (TinyZoo.Game1.gamestate == GAMESTATE.OverWorld && OverWorldManager.overworldstate == OverWOrldState.CellSelect)
        {
          for (int index = this.history.Count - 1; index > -1; --index)
          {
            this.mousepointerWhite.scale = 2f * this.history[index].Alpha;
            if (this.history[index].LocationInWorldSpace != RenderMath.TranslateScreenSpaceToWorldSpace(this.mouseloc))
            {
              this.mousepointerWhite.Rotation = this.history[index].Alpha * 3f;
              this.mousepointerWhite.scale *= 0.5f * Sengine.WorldOriginandScale.Z;
              this.mousepointerWhite.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, RenderMath.TranslateWorldSpaceToScreenSpace(this.history[index].LocationInWorldSpace), this.history[index].Alpha);
            }
          }
          flag = true;
          this.mousepointer.scale = Sengine.WorldOriginandScale.Z;
          this.mousepointer.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Sengine.ReferenceScreenRes * 0.5f);
          DroneAI.DroneLocation = RenderMath.TranslateScreenSpaceToWorldSpace(this.mouseloc);
        }
        if (this.IsShowingMouse())
        {
          for (int index = this.history.Count - 1; index > -1; --index)
          {
            this.mousepointerWhite.scale = 2f * this.history[index].Alpha;
            if (this.history[index].LocationInWorldSpace != RenderMath.TranslateScreenSpaceToWorldSpace(this.mouseloc))
            {
              this.mousepointerWhite.Rotation = this.history[index].Alpha * 3f;
              this.mousepointerWhite.scale *= 0.5f * Sengine.WorldOriginandScale.Z;
              this.mousepointerWhite.Draw(AssetContainer.PointBlendBatch04, AssetContainer.SpriteSheet, RenderMath.TranslateWorldSpaceToScreenSpace(this.history[index].LocationInWorldSpace), this.history[index].Alpha);
            }
          }
          flag = true;
          this.mousepointer.scale = Sengine.WorldOriginandScale.Z;
          this.mousepointer.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, this.mouseloc);
          DroneAI.DroneLocation = RenderMath.TranslateScreenSpaceToWorldSpace(this.mouseloc);
          if (SellUIManager.selectedtileandsell == null && OverWorldManager.overworldstate != OverWOrldState.Build && OverWorldEnvironmentManager.IsOverBuilding(this.mouseloc, player))
            this.ControllerButton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.TRC_Sprites, this.mouseloc + new Vector2(30f, 30f));
        }
        LiveStats.DraggingDragZone = false;
      }
      if (flag)
        return;
      this.mousepointer.scale = Sengine.WorldOriginandScale.Z;
      this.mousepointer.Draw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet, RenderMath.TranslateWorldSpaceToScreenSpace(DroneAI.DroneLocation));
    }
  }
}
