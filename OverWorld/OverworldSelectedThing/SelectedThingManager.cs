// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverworldSelectedThing.SelectedThingManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.Audio;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.PeopleAndBeams;
using TinyZoo.OverWorld.OverworldSelectedThing.SelectedPersonUI;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Tutorials;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components;

namespace TinyZoo.OverWorld.OverworldSelectedThing
{
  internal class SelectedThingManager
  {
    private SelectedPersonUIManager selectedpersonmanger;
    private SellUIManager sellUImanager;

    public SelectedThingManager()
    {
      this.selectedpersonmanger = new SelectedPersonUIManager();
      this.sellUImanager = new SellUIManager();
    }

    public bool CheckMouseOver(Player player) => this.sellUImanager.CheckMouseOver(player);

    public bool UpdateSelectedThingManager(
      Player player,
      float DeltaTime,
      OverWorldEnvironmentManager overworldenvironment,
      out bool ChangedState)
    {
      ChangedState = false;
      if (this.selectedpersonmanger.UpdateSelectedPersonUIManager(DeltaTime, player))
      {
        player.inputmap.ClearAllInput(player);
        this.selectedpersonmanger.Deselect();
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
      }
      this.sellUImanager.UpdateSellUIManager(DeltaTime, player, overworldenvironment);
      AnimalsInPens.AnimalUID = -1;
      AnimalsInPens.MouseIsOverAnimal = false;
      overworldenvironment.animalsinpens.CheckPeopleForCollisions(RenderMath.TranslateScreenSpaceToWorldSpace(player.inputmap.PointerLocation), true);
      if ((!Z_GameFlags.MouseIsOverAPanel || Z_GameFlags.ForceClickSelectionFromSmartCursor != null) && ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 || Z_GameFlags.ForceClickSelectionFromSmartCursor != null))
      {
        if (Z_GameFlags.ForceClickSelectionFromSmartCursor != null)
        {
          player.player.touchinput.ReleaseTapArray[0] = RenderMath.TranslateWorldSpaceToScreenSpace(TileMath.GetTileToWorldSpace(Z_GameFlags.ForceClickSelectionFromSmartCursor));
          Z_GameFlags.ForceClickSelectionFromSmartCursor = (Vector2Int) null;
        }
        if ((double) player.player.touchinput.ReleaseTapArray[0].X >= 0.0)
        {
          Vector2 worldSpace = RenderMath.TranslateScreenSpaceToWorldSpace(player.player.touchinput.ReleaseTapArray[0]);
          AnimalRenderMan animalRenderMan = overworldenvironment.animalsinpens.CheckPeopleForCollisions(worldSpace, false);
          if (animalRenderMan != null)
          {
            if (this.selectedpersonmanger != null && this.selectedpersonmanger.intakeperson == animalRenderMan.refperson)
            {
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
              this.selectedpersonmanger.Deselect();
            }
            else if (TutorialManager.currenttutorial == TUTORIALTYPE.None)
            {
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
              OverWorldManager.zoopopupHolder.CreateZooPopUps(animalRenderMan.REF_prisonerinfo, player);
            }
            this.sellUImanager.Deactivate();
          }
          else if (!OverWorldManager.IsGameIntro && !WalkingPerson.SkipSmartCuror)
          {
            Vector2Int _location = TileMath.GetScreenSPaceToTileLocation(player.player.touchinput.ReleaseTapArray[0]);
            if (TileMath.TileIsInWorld(_location.X, _location.Y))
            {
              if (Z_GameFlags.HasCollidedWithAnimalsOnOrderSign(_location.X, _location.Y))
              {
                ChangedState = true;
                AnimalsOnOrderSign animalsOnOrderSign = Z_GameFlags.GetCollidedWithAnimalsOnOrderSign(_location.X, _location.Y);
                OverWorldManager.zoopopupHolder.CreateZooPopUps(player, false, player.prisonlayout.cellblockcontainer.GetThisCellBlock(animalsOnOrderSign.PrisonUID), POPUPSTATE.AnimalDelivery);
              }
              else
              {
                LayoutEntry thisDungeonTile = player.prisonlayout.GetThisDungeonTile(_location.X, _location.Y);
                if (thisDungeonTile.tiletype != TILETYPE.None)
                {
                  if (thisDungeonTile.isChild())
                  {
                    _location = new Vector2Int(thisDungeonTile.GetParentLocation());
                    thisDungeonTile = player.prisonlayout.GetThisDungeonTile(thisDungeonTile.GetParentLocation().X, thisDungeonTile.GetParentLocation().Y);
                  }
                  if (thisDungeonTile.tiletype != TILETYPE.None)
                  {
                    ChangedState = true;
                    this.sellUImanager.SelectedThisTile(thisDungeonTile, _location, player);
                    if (thisDungeonTile.tiletype != TILETYPE.GraveYard_FloorGraveStone)
                      this.selectedpersonmanger.Deselect();
                  }
                }
                else
                {
                  this.sellUImanager.SelectedThisTile(new LayoutEntry(TILETYPE.Moon), _location, player);
                  ChangedState = true;
                }
                if (thisDungeonTile.tiletype == TILETYPE.GraveYard_FloorGraveStone)
                  throw new Exception("NO GRAVES IN GAME");
              }
            }
          }
        }
      }
      return false;
    }

    public void SelectedThisTile(LayoutEntry selected, Vector2Int Loccc, Player player) => this.sellUImanager.SelectedThisTile(new LayoutEntry(TILETYPE.Moon), Loccc, player);

    public void DrawSelectedThingManager(Player player)
    {
      if (TinyZoo.Game1.gamestate == GAMESTATE.ManageShop)
        return;
      this.sellUImanager.DrawSellUIManager(player);
      this.selectedpersonmanger.DrawSelectedPersonUIManager();
    }
  }
}
