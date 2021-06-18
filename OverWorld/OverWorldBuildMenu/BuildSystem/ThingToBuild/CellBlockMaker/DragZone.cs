// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.CellBlockMaker.DragZone
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.Audio;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.CellBlockMaker
{
  internal class DragZone
  {
    private Vector2Int TopLeft;
    private Vector2Int BottomRight;
    private Vector2Int BottomLeft;
    private Vector2Int TopRight;
    private DragTile fingertile;
    private Vector2 ControllerStartLocation;
    private DragTile WallTile;
    private int TouchTollerance = 3;
    public bool IsDraggingging;
    private TRC_ButtonDisplay ControllerButton;
    private bool IsDraggingTopLeft;
    private bool IsDraggingBottomRight;
    private bool IsDraggingBottomLeft;
    private bool IsDraggingTopRight;
    private bool IsEither;

    public DragZone(Player player)
    {
      this.ControllerButton = new TRC_ButtonDisplay(2f);
      this.ControllerButton.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, SEngine.ControllerButton.XboxX);
      this.fingertile = new DragTile();
      this.fingertile.SetAlpha(0.7f);
      this.fingertile.SetAllColours(1f, 1f, 1f);
      this.WallTile = new DragTile();
      this.ControllerStartLocation = new Vector2(Sengine.WorldOriginandScale.X, Sengine.WorldOriginandScale.Y);
      this.TopLeft = TileMath.GetScreenCenter();
      this.BottomRight = new Vector2Int(this.TopLeft);
      this.TopRight = new Vector2Int(this.TopLeft);
      this.BottomLeft = new Vector2Int(this.TopLeft);
    }

    public int GetVolume() => this.GetWidth() * this.GetHeight();

    public void UpdateDragZone(float DeltaTime, Player player)
    {
      if (player.player.touchinput.TouchCount > 1)
        return;
      this.TouchTollerance = 2;
      this.fingertile.bActive = false;
      if (!this.IsDraggingging)
      {
        if ((double) player.player.touchinput.MultiTouchTapArray[0].X < 0.0)
          return;
        this.IsDraggingTopLeft = false;
        this.IsDraggingBottomRight = false;
        this.IsDraggingBottomLeft = false;
        this.IsDraggingTopRight = false;
        Vector2Int spaceToTileLocation = TileMath.GetScreenSPaceToTileLocation(player.player.touchinput.MultiTouchTapArray[0]);
        if (Math.Abs(spaceToTileLocation.X - this.TopLeft.X) < this.TouchTollerance && Math.Abs(spaceToTileLocation.Y - this.TopLeft.Y) < this.TouchTollerance)
        {
          this.IsDraggingTopLeft = true;
          this.IsDraggingging = true;
        }
        else if (Math.Abs(spaceToTileLocation.X - this.BottomRight.X) < this.TouchTollerance && Math.Abs(spaceToTileLocation.Y - this.BottomRight.Y) < this.TouchTollerance)
        {
          this.IsDraggingBottomRight = true;
          this.IsDraggingging = true;
        }
        else if (Math.Abs(spaceToTileLocation.X - this.BottomLeft.X) < this.TouchTollerance && Math.Abs(spaceToTileLocation.Y - this.BottomLeft.Y) < this.TouchTollerance)
        {
          this.IsDraggingBottomLeft = true;
          this.IsDraggingging = true;
        }
        else if (Math.Abs(spaceToTileLocation.X - this.TopRight.X) < this.TouchTollerance && Math.Abs(spaceToTileLocation.Y - this.TopRight.Y) < this.TouchTollerance)
        {
          this.IsDraggingTopRight = true;
          this.IsDraggingging = true;
        }
        if (!this.IsDraggingging)
        {
          this.CreateNewBox(player);
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.LaunchBeam, 0.3f, 1f);
        }
        else
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.LaunchBeam, 0.3f, 1f);
      }
      else
      {
        int num = TinyZoo.GameFlags.IsUsingController ? 1 : 0;
        if ((double) player.player.touchinput.MultiTouchTouchLocations[0].X < 0.0)
        {
          this.IsDraggingging = false;
        }
        else
        {
          this.fingertile.bActive = true;
          this.fingertile.vLocation = RenderMath.TranslateScreenSpaceToWorldSpace(player.player.touchinput.MultiTouchTouchLocations[0]);
          Vector2Int spaceToTileLocation = TileMath.GetScreenSPaceToTileLocation(player.player.touchinput.MultiTouchTouchLocations[0]);
          if (this.IsDraggingTopLeft)
            this.TopLeft = spaceToTileLocation;
          else if (this.IsDraggingTopRight)
            this.TopRight = spaceToTileLocation;
          else if (this.IsDraggingBottomRight)
            this.BottomRight = spaceToTileLocation;
          else
            this.BottomLeft = spaceToTileLocation;
          if (this.IsDraggingTopLeft)
          {
            bool flag = false;
            if (this.TopLeft.X > this.BottomRight.X)
            {
              int x = this.TopLeft.X;
              this.TopLeft.X = this.BottomRight.X;
              this.BottomLeft.X = this.TopLeft.X;
              this.BottomRight.X = x;
              this.TopRight.X = x;
              this.IsDraggingTopRight = true;
              this.IsDraggingTopLeft = false;
              flag = true;
            }
            if (this.TopLeft.Y > this.BottomRight.Y)
            {
              int y = this.TopLeft.Y;
              this.TopLeft.Y = this.BottomRight.Y;
              this.BottomRight.Y = y;
              this.BottomLeft.Y = y;
              if (flag)
              {
                this.IsDraggingTopRight = false;
                this.IsDraggingBottomRight = true;
              }
              else
                this.IsDraggingBottomLeft = true;
              this.IsDraggingTopLeft = false;
            }
            this.BottomLeft.X = this.TopLeft.X;
            this.TopRight.Y = this.TopLeft.Y;
          }
          else if (this.IsDraggingTopRight)
          {
            bool flag = false;
            if (this.TopRight.X < this.TopLeft.X)
            {
              int x = this.TopRight.X;
              this.TopRight.X = this.TopLeft.X;
              this.TopLeft.X = x;
              this.BottomLeft.X = x;
              this.IsDraggingTopLeft = true;
              this.IsDraggingTopRight = false;
              flag = true;
            }
            if (this.TopRight.Y > this.BottomLeft.Y)
            {
              int y = this.TopRight.Y;
              this.TopRight.Y = this.BottomLeft.Y;
              this.TopLeft.Y = this.BottomLeft.Y;
              this.BottomLeft.Y = y;
              this.BottomRight.Y = y;
              if (flag)
              {
                this.IsDraggingTopLeft = false;
                this.IsDraggingBottomLeft = true;
              }
              else
              {
                this.IsDraggingTopRight = false;
                this.IsDraggingBottomRight = true;
              }
            }
            this.BottomRight.X = this.TopRight.X;
            this.TopLeft.Y = this.TopRight.Y;
          }
          else if (this.IsDraggingBottomRight)
          {
            bool flag = false;
            if (this.BottomRight.X < this.TopLeft.X)
            {
              int x = this.BottomRight.X;
              this.BottomRight.X = this.TopLeft.X;
              this.TopRight.X = this.TopLeft.X;
              this.TopLeft.X = x;
              this.BottomLeft.X = x;
              this.IsDraggingBottomLeft = true;
              this.IsDraggingBottomRight = false;
              flag = true;
            }
            if (this.BottomRight.Y < this.TopLeft.Y)
            {
              int y = this.TopLeft.Y;
              this.TopLeft.Y = this.BottomRight.Y;
              this.BottomLeft.Y = y;
              this.BottomRight.Y = y;
              if (flag)
              {
                this.IsDraggingTopLeft = false;
                this.IsDraggingTopLeft = true;
              }
              else
                this.IsDraggingTopRight = true;
              this.IsDraggingBottomRight = false;
            }
            this.TopRight.X = this.BottomRight.X;
            this.BottomLeft.Y = this.BottomRight.Y;
          }
          else if (this.IsDraggingBottomLeft)
          {
            bool flag = false;
            if (this.BottomLeft.X > this.BottomRight.X)
            {
              int x = this.BottomLeft.X;
              this.BottomLeft.X = this.BottomRight.X;
              this.TopLeft.X = this.BottomRight.X;
              this.BottomRight.X = x;
              this.TopRight.X = x;
              this.TopLeft.X = x;
              this.IsDraggingBottomLeft = false;
              this.IsDraggingBottomRight = true;
              flag = true;
            }
            if (this.BottomLeft.Y < this.TopLeft.Y)
            {
              int y = this.BottomLeft.Y;
              this.BottomLeft.Y = this.TopLeft.Y;
              this.BottomRight.Y = this.TopLeft.Y;
              this.TopLeft.Y = y;
              this.TopRight.Y = y;
              if (flag)
              {
                this.IsDraggingBottomRight = false;
                this.IsDraggingTopRight = true;
              }
              else
                this.IsDraggingTopLeft = true;
              this.IsDraggingBottomLeft = false;
            }
            this.TopLeft.X = this.BottomLeft.X;
            this.BottomRight.Y = this.BottomLeft.Y;
          }
          if (this.TopLeft.X < 0)
          {
            this.TopLeft.X = 0;
            this.BottomLeft.X = 0;
          }
          if (this.TopRight.X < 0)
          {
            this.TopRight.X = 0;
            this.BottomRight.X = 0;
          }
          if (this.TopRight.X > TileMath.GetOverWorldMapSize_XDefault() - 2)
          {
            this.TopRight.X = TileMath.GetOverWorldMapSize_XDefault() - 1;
            this.BottomRight.X = TileMath.GetOverWorldMapSize_XDefault() - 1;
          }
          if (this.TopLeft.X > TileMath.GetOverWorldMapSize_XDefault() - 2)
          {
            this.TopLeft.X = TileMath.GetOverWorldMapSize_XDefault() - 1;
            this.BottomLeft.X = TileMath.GetOverWorldMapSize_XDefault() - 1;
          }
          if (this.TopLeft.Y < 0)
          {
            this.TopLeft.Y = 0;
            this.TopRight.Y = 0;
          }
          if (this.BottomLeft.Y < 0)
          {
            this.BottomLeft.Y = 0;
            this.BottomLeft.Y = 0;
          }
          if (this.BottomLeft.Y > TileMath.GetOverWorldMapSize_XDefault() - 2)
          {
            this.BottomLeft.Y = TileMath.GetOverWorldMapSize_XDefault() - 1;
            this.BottomRight.Y = TileMath.GetOverWorldMapSize_XDefault() - 1;
          }
          if (this.TopLeft.Y <= TileMath.GetOverWorldMapSize_XDefault() - 2)
            return;
          this.TopLeft.Y = TileMath.GetOverWorldMapSize_XDefault() - 1;
          this.TopRight.Y = TileMath.GetOverWorldMapSize_XDefault() - 1;
        }
      }
    }

    private void CreateNewBox(Player player)
    {
      this.TopLeft = TileMath.GetScreenSPaceToTileLocation(player.player.touchinput.MultiTouchTapArray[0]);
      if (!TileMath.TileIsInWorld(this.TopLeft.X, this.TopLeft.Y))
      {
        if (this.TopLeft.X < 0)
          this.TopLeft.X = 0;
        else if (this.TopLeft.X > TileMath.GetOverWorldMapSize_XDefault() - 2)
          this.TopLeft.X = TileMath.GetOverWorldMapSize_XDefault() - 1;
        if (this.TopLeft.Y < 0)
          this.TopLeft.Y = 0;
        else if (this.TopLeft.Y > TileMath.GetOverWorldMapSize_XDefault() - 2)
          this.TopLeft.Y = TileMath.GetOverWorldMapSize_XDefault() - 1;
      }
      this.BottomRight = new Vector2Int(this.TopLeft);
      this.TopRight = new Vector2Int(this.TopLeft);
      this.BottomLeft = new Vector2Int(this.TopLeft);
      this.IsDraggingging = true;
      this.IsDraggingBottomLeft = true;
    }

    public Vector2Int GetTopLeft() => this.TopLeft;

    public int GetHeight() => this.BottomLeft.Y + 1 - this.TopLeft.Y;

    public int GetWidth() => this.BottomRight.X + 1 - this.TopLeft.X;

    public bool GetThisIsnextToSomething(TileRenderer[,] tilesasarray) => WallsAndFloorsManager.GetThisIsnextToSomething(this.TopLeft.X - 1, this.TopLeft.Y - 1, this.BottomRight.X + 1, this.BottomRight.Y + 1, tilesasarray);

    public bool GetIsBlocked(TileRenderer[,] tilesasarray)
    {
      Vector2Int Location = new Vector2Int(this.TopLeft);
      for (int x = this.TopLeft.X; x < this.BottomRight.X + 1; ++x)
      {
        Location.X = x;
        Location.Y = this.TopLeft.Y;
        if (TileRenderer.GetIsBlocked(Location, tilesasarray))
          return true;
        Location.Y = this.BottomRight.Y;
        if (TileRenderer.GetIsBlocked(Location, tilesasarray))
          return true;
      }
      if (this.TopLeft.Y + 1 < this.BottomRight.Y)
      {
        for (int index = this.TopLeft.Y + 1; index < this.BottomRight.Y + 1; ++index)
        {
          Location.X = this.TopLeft.X;
          Location.Y = index;
          if (TileRenderer.GetIsBlocked(Location, tilesasarray))
            return true;
          Location.X = this.BottomRight.X;
          if (TileRenderer.GetIsBlocked(Location, tilesasarray))
            return true;
        }
      }
      if (this.TopLeft.X + 1 < this.BottomRight.X)
      {
        for (int index1 = this.TopLeft.X + 1; index1 < this.BottomRight.X; ++index1)
        {
          if (this.TopLeft.Y + 1 < this.BottomRight.Y)
          {
            for (int index2 = this.TopLeft.Y + 1; index2 < this.BottomRight.Y + 1; ++index2)
            {
              Location.X = index1;
              Location.Y = index2;
              if (TileRenderer.GetIsBlocked(Location, tilesasarray))
                return true;
            }
          }
        }
      }
      return false;
    }

    public void DrawDragZone(TileRenderer[,] tilesasarray)
    {
      Vector2Int Location = new Vector2Int(this.TopLeft);
      this.WallTile.fAlpha = 1f;
      for (int x = this.TopLeft.X; x < this.BottomRight.X + 1; ++x)
      {
        Location.X = x;
        Location.Y = this.TopLeft.Y;
        this.WallTile.vLocation = TileMath.GetTileToWorldSpace(Location);
        if (TileRenderer.GetIsBlocked(Location, tilesasarray))
          this.WallTile.SetRed();
        else
          this.WallTile.SetGreen();
        this.WallTile.DrawDragTile();
        Location.Y = this.BottomRight.Y;
        this.WallTile.vLocation = TileMath.GetTileToWorldSpace(Location);
        if (TileRenderer.GetIsBlocked(Location, tilesasarray))
          this.WallTile.SetRed();
        else
          this.WallTile.SetGreen();
        this.WallTile.DrawDragTile();
      }
      if (this.TopLeft.Y + 1 < this.BottomRight.Y)
      {
        for (int index = this.TopLeft.Y + 1; index < this.BottomRight.Y + 1; ++index)
        {
          Location.X = this.TopLeft.X;
          Location.Y = index;
          this.WallTile.vLocation = TileMath.GetTileToWorldSpace(Location);
          if (TileRenderer.GetIsBlocked(Location, tilesasarray))
            this.WallTile.SetRed();
          else
            this.WallTile.SetGreen();
          this.WallTile.DrawDragTile();
          Location.X = this.BottomRight.X;
          this.WallTile.vLocation = TileMath.GetTileToWorldSpace(Location);
          if (TileRenderer.GetIsBlocked(Location, tilesasarray))
            this.WallTile.SetRed();
          else
            this.WallTile.SetGreen();
          this.WallTile.DrawDragTile();
        }
      }
      this.WallTile.fAlpha = 0.5f;
      if (this.TopLeft.X + 1 < this.BottomRight.X)
      {
        for (int index1 = this.TopLeft.X + 1; index1 < this.BottomRight.X; ++index1)
        {
          if (this.TopLeft.Y + 1 < this.BottomRight.Y)
          {
            for (int index2 = this.TopLeft.Y + 1; index2 < this.BottomRight.Y + 1; ++index2)
            {
              Location.X = index1;
              Location.Y = index2;
              this.WallTile.vLocation = TileMath.GetTileToWorldSpace(Location);
              if (TileRenderer.GetIsBlocked(Location, tilesasarray))
                this.WallTile.SetRed();
              else
                this.WallTile.SetGreen();
              this.WallTile.DrawDragTile();
            }
          }
        }
      }
      if (this.fingertile.bActive)
        this.fingertile.DrawDragTile();
      if (this.IsDraggingging || !TinyZoo.GameFlags.IsUsingController)
        return;
      Vector2 screenSpace1 = RenderMath.TranslateWorldSpaceToScreenSpace(TileMath.GetTileToWorldSpace(this.TopLeft));
      this.ControllerButton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.TRC_Sprites, screenSpace1);
      Vector2 screenSpace2 = RenderMath.TranslateWorldSpaceToScreenSpace(TileMath.GetTileToWorldSpace(this.BottomRight));
      this.ControllerButton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.TRC_Sprites, screenSpace2);
      Vector2 screenSpace3 = RenderMath.TranslateWorldSpaceToScreenSpace(TileMath.GetTileToWorldSpace(this.BottomLeft));
      this.ControllerButton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.TRC_Sprites, screenSpace3);
      Vector2 screenSpace4 = RenderMath.TranslateWorldSpaceToScreenSpace(TileMath.GetTileToWorldSpace(this.TopRight));
      this.ControllerButton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.TRC_Sprites, screenSpace4);
    }
  }
}
