// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.AvatarUI.SmartCursor
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Buttons;
using TinyZoo.OverWorld;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverworldSelectedThing;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BarMenu.Land;
using TinyZoo.Z_CharacterSelect;
using TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components;
using TinyZoo.Z_OverWorld.AvatarUI.Selection;

namespace TinyZoo.Z_OverWorld.AvatarUI
{
  internal class SmartCursor
  {
    private MicroTimerBar microtimerbar;
    private SelectionFrame SelectionFrame_Controller;
    private Vector2Int LastLoc;
    private Vector2 WorldPosition;
    private UnlockSectorPreview unlocksectorpreview;
    private SmartCursorState cursorstate;
    private Vector2 CharacterWorldPosition;
    private Vector2Int CharacterTileLocation;
    private DirectionPressed CharacterFacingThisWay;
    private float SelectionWidth = 16f;
    private float SelectionHeight = 16f;
    private MicroIconManager microiconmanager;
    private Vector2 CursorOffsetForBuilding;
    private bool IsInMouseRange;
    private CURSOR_ACTIONTYPE LastSetCursor;

    public SmartCursor()
    {
      this.microtimerbar = new MicroTimerBar();
      this.microiconmanager = new MicroIconManager();
      this.unlocksectorpreview = new UnlockSectorPreview();
      this.SelectionFrame_Controller = new SelectionFrame(16, 16, UseWhite: true);
      this.LastLoc = new Vector2Int(-1, -1);
    }

    public void SetNewCharacterLocation(
      WalkingPerson Person,
      PathNavigator pathnavigator,
      Player player,
      bool SetNewTilePos)
    {
      if (GameFlags.IsUsingController)
      {
        if (!SetNewTilePos && this.CharacterFacingThisWay == Person.directionmoving)
          return;
        this.CharacterFacingThisWay = Person.directionmoving;
        this.CharacterWorldPosition = Person.vLocation;
        this.CharacterTileLocation = pathnavigator.CurrentTile;
        this.CheckCursorOnControllerMove(player);
      }
      else
      {
        if (!(this.CharacterWorldPosition != Person.vLocation) && this.CharacterFacingThisWay == Person.directionmoving)
          return;
        this.CharacterFacingThisWay = Person.directionmoving;
        this.CharacterWorldPosition = Person.vLocation;
        this.CharacterTileLocation = pathnavigator.CurrentTile;
      }
    }

    private void CheckCursorOnControllerMove(Player player)
    {
      int num1 = 0;
      int num2 = 0;
      int x = this.CharacterTileLocation.X;
      int y = this.CharacterTileLocation.Y;
      int num3 = 0;
      int num4 = 0;
      Vector2 percentageThroughTile = TileMath.GetPercentageThroughTile(this.CharacterWorldPosition);
      switch (this.CharacterFacingThisWay)
      {
        case DirectionPressed.Up:
          num2 = -1;
          --y;
          num3 = (double) percentageThroughTile.X >= 0.0 ? 1 : -1;
          break;
        case DirectionPressed.Right:
          num1 = 1;
          ++x;
          num4 = (double) percentageThroughTile.Y >= 0.0 ? -1 : 1;
          break;
        case DirectionPressed.Down:
          num2 = 1;
          ++y;
          num3 = (double) percentageThroughTile.X >= 0.0 ? 1 : -1;
          break;
        case DirectionPressed.Left:
          num1 = 1;
          --x;
          num4 = (double) percentageThroughTile.Y >= 0.0 ? -1 : 1;
          break;
      }
      for (int index = 0; index < 2; ++index)
      {
        CURSOR_ACTIONTYPE cursorThing = Z_GameFlags.Location_Directory.GetCursorThing(x + num1 * index, y + num2 * index, player);
        if (cursorThing != CURSOR_ACTIONTYPE.Count)
        {
          this.LastLoc.X = x + num1 * index;
          this.LastLoc.Y = y + num2 * index;
          this.SetCursorPositionWithController(cursorThing, player);
          return;
        }
      }
      for (int index = 0; index < 2; ++index)
      {
        CURSOR_ACTIONTYPE cursorThing = Z_GameFlags.Location_Directory.GetCursorThing(x + num3 + num1 * index, y + num4 + num2 * index, player);
        if (cursorThing != CURSOR_ACTIONTYPE.Count)
        {
          this.LastLoc.X = x + num3 + num1 * index;
          this.LastLoc.Y = y + num4 + num2 * index;
          this.SetCursorPositionWithController(cursorThing, player);
          return;
        }
      }
      this.LastLoc.X = x;
      this.LastLoc.Y = y;
      this.SetCursorPositionWithController(CURSOR_ACTIONTYPE.Count, player);
      this.microiconmanager.bActive = false;
    }

    private void SetCursorPositionWithController(CURSOR_ACTIONTYPE cursor, Player player)
    {
      this.LastSetCursor = cursor;
      this.SelectionWidth = 16f;
      this.SelectionHeight = 16f;
      this.WorldPosition = TileMath.GetTileToWorldSpace(this.LastLoc);
      this.CursorOffsetForBuilding = Vector2.Zero;
      this.unlocksectorpreview.bActive = cursor == CURSOR_ACTIONTYPE.LockedSector;
      switch (cursor)
      {
        case CURSOR_ACTIONTYPE.SelectedBuilding:
          this.SetUpSelectedBuilding(player, this.LastLoc);
          break;
        case CURSOR_ACTIONTYPE.SelectedPen:
          this.SetUpSelectedPen(player, this.LastLoc);
          break;
        case CURSOR_ACTIONTYPE.LockedSector:
          this.SetUpLockedSector(player, this.LastLoc);
          break;
        case CURSOR_ACTIONTYPE.InspectAnimalsOnOrder:
          this.SetUpInspectAnimalsOnOrder(player, this.LastLoc);
          break;
      }
      this.microiconmanager.SetIcon(cursor, player, this.LastLoc);
    }

    private void SetUpSelectedPen(Player player, Vector2Int location)
    {
      PrisonZone prisonZone = player.farms.GetThisField(location, false) ?? player.prisonlayout.cellblockcontainer.GetThisCellBlock(location);
      prisonZone.TrySetTopeLeftAndWidthHeight();
      this.SelectionWidth = (float) ((prisonZone.BottomRight.X - prisonZone.TopLeft.X) * 16);
      this.SelectionHeight = (float) ((prisonZone.BottomRight.Y - prisonZone.TopLeft.Y) * 16);
      this.SelectionWidth += 16f;
      this.SelectionHeight += 16f;
      this.CursorOffsetForBuilding.X = this.SelectionWidth * 0.5f;
      this.CursorOffsetForBuilding.Y = this.SelectionHeight * 0.5f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.CursorOffsetForBuilding.Y -= (float) ((location.Y - prisonZone.TopLeft.Y) * 16) * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.CursorOffsetForBuilding.Y -= 8f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.CursorOffsetForBuilding.X -= (float) ((location.X - prisonZone.TopLeft.X) * 16);
      this.CursorOffsetForBuilding.X -= 8f;
    }

    private void SetUpLockedSector(Player player, Vector2Int location)
    {
      Vector2Int locationToSector = TileMath.GetLocationToSector(location.X, location.Y);
      this.SelectionWidth = (float) (TileMath.SectorSize * 16);
      this.SelectionHeight = (float) (TileMath.SectorSize * 16);
      Vector2Int vector2Int = new Vector2Int(location);
      this.CursorOffsetForBuilding.X = (float) (16 * TileMath.SectorSize);
      this.CursorOffsetForBuilding.Y = (float) (16 * TileMath.SectorSize) * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.CursorOffsetForBuilding *= 0.5f;
      this.CursorOffsetForBuilding.X -= 8f;
      this.CursorOffsetForBuilding.Y -= 8f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.CursorOffsetForBuilding.X -= (float) ((location.X - locationToSector.X * TileMath.SectorSize) * 16);
      this.CursorOffsetForBuilding.Y -= (float) ((location.Y - locationToSector.Y * TileMath.SectorSize) * 16) * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    private void SetUpInspectAnimalsOnOrder(Player player, Vector2Int location)
    {
      AnimalsOnOrderSign animalsOnOrderSign = Z_GameFlags.GetCollidedWithAnimalsOnOrderSign(location.X, location.Y);
      this.SelectionWidth = 16f;
      this.SelectionHeight = 32f;
      this.CursorOffsetForBuilding.X = 0.0f;
      this.CursorOffsetForBuilding.Y = 0.0f;
      if (location.Y != animalsOnOrderSign.Location.Y)
      {
        this.CursorOffsetForBuilding.Y = 8f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.CursorOffsetForBuilding.X = 0.0f;
      }
      else
        this.CursorOffsetForBuilding.Y = -8f;
    }

    private void SetUpSelectedBuilding(Player player, Vector2Int location)
    {
      TileInfo tileInfo = TileData.GetTileInfo(player.prisonlayout.layout.BaseTileTypes[location.X, location.Y].tiletype);
      Vector2Int vector2Int = new Vector2Int(location);
      int rotationClockWise;
      if (player.prisonlayout.layout.BaseTileTypes[location.X, location.Y].isChild())
      {
        vector2Int = new Vector2Int(player.prisonlayout.layout.BaseTileTypes[location.X, location.Y].ParentLocation);
        rotationClockWise = player.prisonlayout.layout.BaseTileTypes[vector2Int.X, vector2Int.Y].RotationClockWise;
      }
      else
        rotationClockWise = player.prisonlayout.layout.BaseTileTypes[location.X, location.Y].RotationClockWise;
      this.SelectionWidth = (float) (tileInfo.GetTileWidth(rotationClockWise) * 16);
      this.SelectionHeight = (float) (tileInfo.GetTileHeight(rotationClockWise) * 16);
      this.CursorOffsetForBuilding.X = (float) ((vector2Int.X - location.X) * 16);
      this.CursorOffsetForBuilding.Y = (float) ((vector2Int.Y - location.Y) * 16) * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.CursorOffsetForBuilding.Y -= (float) ((tileInfo.GetTileHeight(rotationClockWise) - 1) * 8) * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.CursorOffsetForBuilding.Y += (float) ((tileInfo.GetTileHeight(rotationClockWise) - 1 - tileInfo.GetYTileOrigin(rotationClockWise)) * 16) * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public bool CheckNewSelections(
      Player player,
      SelectedThingManager selectedthingmanager,
      bool ForceCheckSelection = false)
    {
      if (OverWorldManager.IsGameIntro || OverwoldMainButtons.MouseIsOverAButton || Z_GameFlags.MouseIsOverAPanel || OverWorldManager.overworldstate == OverWOrldState.CustomizePen && OverWorldManager.customizepenmanager != null && OverWorldManager.customizepenmanager.IsMouseOverButton(player) || (SellUIManager.selectedtileandsell != null && SellUIManager.selectedtileandsell.IsMouseOverButton(player) || !(player.inputmap.ReleasedThisFrame[0] | ForceCheckSelection) && (double) player.player.touchinput.ReleaseTapArray[0].X <= 0.0 || (this.LastSetCursor != CURSOR_ACTIONTYPE.LockedSector || OverWorldManager.overworldstate != OverWOrldState.MainMenu && OverWorldManager.overworldstate != OverWOrldState.BuyMoreLand)) || (FeatureFlags.BlockBuyLand || TileMath.TileIsInBuildablePartOfWorld(this.LastLoc.X, this.LastLoc.Y) || (!TileMath.TileIsInWorld(this.LastLoc.X, this.LastLoc.Y) || TileMath.IsBelowPark(this.LastLoc.Y)) || (OverwoldMainButtons.MouseIsOverAButton || OverWorldManager.z_daynightmanager.MouseIsOverStartDayButton(player))))
        return false;
      OverWorldManager.overworldstate = OverWOrldState.BuyMoreLand;
      OverWorldManager.buymoreland = new Z_BuyLand(player, this.LastLoc);
      if (OverWorldManager.zoopopupHolder.ScrubOnBuyNewLand(player))
        OverWorldManager.zoopopupHolder.SetNull();
      return true;
    }

    public bool UpdateSmartCursor(
      Player player,
      float DeltaTime,
      SelectedThingManager selectedthingmanager,
      ref OverWOrldState OverrideOverworldState)
    {
      bool IsScrolling = false;
      bool ForceCheckSelection = false;
      if (OverWorldManager.overworldstate != OverWOrldState.PlayingAsAvatar)
      {
        if (!Z_GameFlags.IsWaitingToReturnToControllerWalk)
        {
          this.IsInMouseRange = false;
          Vector2Int worldSpaceToTile = TileMath.GetWorldSpaceToTile(RenderMath.TranslateScreenSpaceToWorldSpace(player.inputmap.PointerLocation));
          if (!this.LastLoc.CompareMatches(worldSpaceToTile))
          {
            this.LastLoc = worldSpaceToTile;
            this.SetCursorPositionWithController(Z_GameFlags.Location_Directory.GetCursorThing(worldSpaceToTile.X, worldSpaceToTile.Y, player), player);
          }
          if (OverWorldManager.overworldstate == OverWOrldState.PlayingAsAvatar)
          {
            if (this.LastSetCursor == CURSOR_ACTIONTYPE.SelectedBuilding || this.LastSetCursor == CURSOR_ACTIONTYPE.SelectedPen)
            {
              OverrideOverworldState = OverWOrldState.MainMenu;
              Z_GameFlags.ForceClickSelectionFromSmartCursor = new Vector2Int(this.LastLoc.X, this.LastLoc.Y);
            }
            else
              IsScrolling = (double) player.player.touchinput.MultiTouchTouchLocations[0].X > 0.0;
          }
        }
      }
      else if (this.LastSetCursor == CURSOR_ACTIONTYPE.SelectedBuilding || this.LastSetCursor == CURSOR_ACTIONTYPE.SelectedPen)
      {
        if (player.inputmap.ReleasedThisFrame[29])
        {
          OverrideOverworldState = OverWOrldState.MainMenu;
          Z_GameFlags.ForceClickSelectionFromSmartCursor = new Vector2Int(this.LastLoc.X, this.LastLoc.Y);
        }
        if (IsScrolling)
          ForceCheckSelection = false;
      }
      else
        IsScrolling = player.inputmap.HeldButtons[29];
      if (!this.CheckNewSelections(player, selectedthingmanager, ForceCheckSelection))
      {
        if (IsScrolling && this.cursorstate == SmartCursorState.None)
        {
          if (this.microiconmanager.GetHasAction())
          {
            switch (this.microiconmanager.GetPrimaryAction())
            {
              case CURSOR_ACTIONTYPE.CollectTrash:
                this.microtimerbar.Activate(2f);
                this.cursorstate = SmartCursorState.CollectingTrash;
                break;
              case CURSOR_ACTIONTYPE.SelectedBuilding:
                Vector2Int Loccc = new Vector2Int(this.LastLoc);
                LayoutEntry thisDungeonTile = player.prisonlayout.GetThisDungeonTile(Loccc.X, Loccc.Y);
                if (thisDungeonTile.isChild())
                {
                  Loccc = new Vector2Int(thisDungeonTile.GetParentLocation());
                  player.prisonlayout.GetThisDungeonTile(thisDungeonTile.GetParentLocation().X, thisDungeonTile.GetParentLocation().Y);
                }
                selectedthingmanager.SelectedThisTile(new LayoutEntry(player.prisonlayout.layout.BaseTileTypes[this.LastLoc.X, this.LastLoc.Y].tiletype), Loccc, player);
                break;
            }
          }
          else if (this.LastSetCursor != CURSOR_ACTIONTYPE.SelectedBuilding)
          {
            int lastSetCursor = (int) this.LastSetCursor;
          }
        }
        if (this.cursorstate != SmartCursorState.None)
        {
          switch (this.cursorstate)
          {
            case SmartCursorState.CollectingTrash:
              if (this.microtimerbar.UpdateMicroTimerBar(DeltaTime, IsScrolling) && OverWorldManager.trashmanager.TryToPickUpTrash(this.LastLoc))
              {
                if (GameFlags.IsUsingController)
                  this.CheckCursorOnControllerMove(player);
                this.cursorstate = SmartCursorState.None;
                return true;
              }
              if (!IsScrolling)
              {
                this.cursorstate = SmartCursorState.None;
                break;
              }
              break;
          }
        }
        else
          this.microtimerbar.Deactivate();
      }
      return false;
    }

    public void DrawSmartCursor(SpriteBatch spriteBatch)
    {
      if (OverWorldManager.IsGameIntro || SellUIManager.selectedtileandsell != null || Z_GameFlags.MouseIsOverAPanel)
        return;
      Vector2 screenSpace = RenderMath.TranslateWorldSpaceToScreenSpace(this.WorldPosition + this.CursorOffsetForBuilding);
      if (this.unlocksectorpreview.bActive && OverWorldManager.overworldstate == OverWOrldState.MainMenu && !FeatureFlags.BlockBuyLand)
        this.unlocksectorpreview.DrawSectorPreview(screenSpace, spriteBatch);
      this.SelectionFrame_Controller.Width = this.SelectionWidth * Sengine.WorldOriginandScale.Z;
      this.SelectionFrame_Controller.Corners[0].scale = Sengine.WorldOriginandScale.Z / 2f;
      this.SelectionFrame_Controller.Height = this.SelectionHeight * Sengine.WorldOriginandScale.Z * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.SelectionFrame_Controller.DrawSelectionFrame(screenSpace, spriteBatch);
      if (OverWorldManager.overworldstate != OverWOrldState.PlayingAsAvatar)
        return;
      this.microiconmanager.DrawMicroIconManager(screenSpace + new Vector2((float) ((double) this.SelectionFrame_Controller.Width * 0.5 + (double) this.SelectionFrame_Controller.Corners[0].scale * 3.0), this.SelectionFrame_Controller.Height * -0.5f), spriteBatch);
      this.microtimerbar.DrawMicroTimerBar(screenSpace, this.SelectionFrame_Controller.Height * 0.5f, spriteBatch);
    }
  }
}
