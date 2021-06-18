// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.ThingToBuildFootPrint
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_CharacterSelect;

namespace TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild
{
  internal class ThingToBuildFootPrint
  {
    private GameObject[,] tiles;
    private GameObject[,] WaterBlocks;
    public List<EntranceArrow> Entrances;
    public List<Vector2Int> EntrancePositions;
    public bool HasAccessToWater;
    private int TotalFloorSize;
    private SelectionFrame smartcursorfrane;
    public bool SomethingIsBlocked;
    private bool IncludeWaterSupply;

    public ThingToBuildFootPrint(
      int With,
      int Height,
      TileRenderer renderer,
      TileInfo info,
      PrisonZone _decoratethisprisonzone = null,
      bool IsDestroy = false)
    {
      this.SomethingIsBlocked = false;
      this.smartcursorfrane = new SelectionFrame(With * 16, Height * 16, UseWhite: true);
      this.TotalFloorSize = 0;
      this.tiles = new GameObject[With, Height];
      this.WaterBlocks = new GameObject[With, Height];
      for (int index1 = 0; index1 < With; ++index1)
      {
        for (int index2 = 0; index2 < Height; ++index2)
        {
          ++this.TotalFloorSize;
          this.tiles[index1, index2] = new GameObject();
          this.tiles[index1, index2].DrawRect = EntranceArrow.BlockRect;
          if (IsDestroy)
            this.tiles[index1, index2].DrawRect = EntranceArrow.DestroyRect;
          this.tiles[index1, index2].SetDrawOriginToCentre();
          this.tiles[index1, index2].vLocation = new Vector2((float) ((index1 - renderer.XOrigin) * 16), (float) (16 * (index2 - renderer.YOrigin)) * Sengine.ScreenRatioUpwardsMultiplier.Y);
        }
      }
      this.Entrances = new List<EntranceArrow>();
      this.EntrancePositions = info.GetEntrances(renderer.RotationOnConstruct);
      if (this.EntrancePositions != null)
      {
        for (int index = 0; index < this.EntrancePositions.Count; ++index)
        {
          int rotationOnConstruct = renderer.RotationOnConstruct;
          if (renderer.RotationOnConstruct == 0)
          {
            if (this.EntrancePositions[index].Y <= 0)
            {
              if (this.EntrancePositions[index].Y <= -Height)
                rotationOnConstruct += 2;
              else if (this.EntrancePositions[index].X > 0)
                --rotationOnConstruct;
              else
                ++rotationOnConstruct;
            }
          }
          else
          {
            switch (rotationOnConstruct)
            {
              case 1:
                if (this.EntrancePositions[index].Y <= -Height)
                {
                  ++rotationOnConstruct;
                  break;
                }
                if (this.EntrancePositions[index].X > 0)
                {
                  rotationOnConstruct += 2;
                  break;
                }
                if (this.EntrancePositions[index].Y > 0)
                {
                  --rotationOnConstruct;
                  break;
                }
                break;
              case 2:
                if (this.EntrancePositions[index].Y > -Height)
                {
                  if (this.EntrancePositions[index].Y > 0)
                  {
                    rotationOnConstruct -= 2;
                    break;
                  }
                  if (this.EntrancePositions[index].X > 0)
                  {
                    ++rotationOnConstruct;
                    break;
                  }
                  if (this.EntrancePositions[index].X < 0)
                  {
                    --rotationOnConstruct;
                    break;
                  }
                  break;
                }
                break;
              case 3:
                if (this.EntrancePositions[index].Y <= -Height)
                {
                  --rotationOnConstruct;
                  break;
                }
                if (this.EntrancePositions[index].Y > 0)
                {
                  ++rotationOnConstruct;
                  break;
                }
                if (this.EntrancePositions[index].X <= 0 && this.EntrancePositions[index].X < 0)
                {
                  rotationOnConstruct -= 2;
                  break;
                }
                break;
            }
          }
          if (rotationOnConstruct > 3)
            rotationOnConstruct -= 4;
          else if (rotationOnConstruct < 0)
            rotationOnConstruct += 4;
          this.Entrances.Add(new EntranceArrow(rotationOnConstruct));
          this.Entrances[index].vLocation = new Vector2((float) this.EntrancePositions[index].X * TileMath.TileSize, (float) this.EntrancePositions[index].Y * TileMath.TileSize * Sengine.ScreenRatioUpwardsMultiplier.Y);
          this.Entrances[index].TempWorldLocation = this.EntrancePositions[index];
        }
      }
      if (!DebugFlags.DrawBuildingOrigins)
        return;
      this.Entrances.Add(new EntranceArrow(-1));
    }

    public void SetBlocks(
      BlockInfo block,
      TileRenderer renderer,
      bool _IncludeWaterSupply = false,
      PrisonZone decoratethisprisonzone = null)
    {
      this.SomethingIsBlocked = false;
      this.IncludeWaterSupply = _IncludeWaterSupply;
      int messageType = (int) block.GetMessageType();
      this.HasAccessToWater = true;
      for (int index1 = 0; index1 < this.tiles.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.tiles.GetLength(1); ++index2)
        {
          this.tiles[index1, index2].SetAlpha(0.0f);
          if (this.WaterBlocks[index1, index2] != null)
            this.WaterBlocks[index1, index2].SetAlpha(0.0f);
        }
      }
      if (block.blocks != null)
      {
        if (renderer.Is17PixFloor)
        {
          this.tiles[0, 0].SetAllColours(new Vector3(1f, 1f, 1f));
          this.tiles[0, 0].SetAlpha(1f);
          this.SomethingIsBlocked = true;
        }
        else
        {
          for (int index1 = 0; index1 < block.blocks.Count; ++index1)
          {
            int index2 = block.blocks[index1].X - renderer.TileLocation.X + renderer.XOrigin;
            int index3 = block.blocks[index1].Y - renderer.TileLocation.Y + renderer.YOrigin;
            this.tiles[index2, index3].SetAllColours(new Vector3(1f, 1f, 1f));
            this.tiles[index2, index3].SetAlpha(1f);
            this.SomethingIsBlocked = true;
          }
        }
      }
      if (this.IncludeWaterSupply && block.NonWaterTiles != null)
      {
        int num = 0;
        for (int index1 = 0; index1 < block.NonWaterTiles.Count; ++index1)
        {
          int index2 = block.NonWaterTiles[index1].X - renderer.TileLocation.X + renderer.XOrigin;
          int index3 = block.NonWaterTiles[index1].Y - renderer.TileLocation.Y + renderer.YOrigin;
          if (index2 > -1 && index3 > -1 && (index2 < this.WaterBlocks.GetLength(0) && index3 < this.WaterBlocks.GetLength(1)))
          {
            if (this.WaterBlocks[index2, index3] == null)
            {
              this.WaterBlocks[index2, index3] = new GameObject(this.tiles[index2, index3]);
              this.WaterBlocks[index2, index3].DrawRect = new Rectangle(46, 24, 16, 16);
            }
            ++num;
            this.WaterBlocks[index2, index3].SetAllColours(new Vector3(1f, 1f, 1f));
            this.WaterBlocks[index2, index3].SetAlpha(1f);
          }
        }
        this.HasAccessToWater = num < this.TotalFloorSize;
        Console.WriteLine("ACCES TO WATER" + this.HasAccessToWater.ToString() + "WB" + (object) num + "CNT" + (object) this.TotalFloorSize);
      }
      for (int index1 = 0; index1 < this.Entrances.Count; ++index1)
      {
        this.Entrances[index1].UnblockEntrance();
        if (block.blockedEntrances != null)
        {
          for (int index2 = 0; index2 < block.blockedEntrances.Count; ++index2)
          {
            if (block.blockedEntrances[index2].X - renderer.TileLocation.X == this.Entrances[index1].TempWorldLocation.X && block.blockedEntrances[index2].Y - renderer.TileLocation.Y == this.Entrances[index1].TempWorldLocation.Y)
            {
              bool _WillBlockDraw = false;
              if (decoratethisprisonzone != null && !decoratethisprisonzone.ThisIsInThisEnclosure(block.blockedEntrances[index2].X, block.blockedEntrances[index2].Y))
                _WillBlockDraw = true;
              this.Entrances[index1].BlockEntrance(_WillBlockDraw);
            }
          }
        }
      }
    }

    public void DrawThingToBuildFootPrint(
      Vector2 Offset,
      SpriteBatch spritebatch,
      bool DrawEntrances = true)
    {
      for (int index1 = 0; index1 < this.tiles.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.tiles.GetLength(1); ++index2)
        {
          if ((double) this.tiles[index1, index2].fAlpha > 0.0)
            this.tiles[index1, index2].WorldOffsetDraw(spritebatch, AssetContainer.SpriteSheet, this.tiles[index1, index2].vLocation + Offset, this.tiles[index1, index2].scale, 0.0f);
          if (this.WaterBlocks[index1, index2] != null && (double) this.WaterBlocks[index1, index2].fAlpha > 0.0)
            this.WaterBlocks[index1, index2].WorldOffsetDraw(spritebatch, AssetContainer.SpriteSheet, this.tiles[index1, index2].vLocation + Offset, this.tiles[index1, index2].scale, 0.0f);
        }
      }
      this.smartcursorfrane.ScreenSpaceDrawInBuild(RenderMath.TranslateWorldSpaceToScreenSpace(this.tiles[0, 0].vLocation + Offset + new Vector2((float) (this.tiles.GetLength(0) - 1) * 8f, (float) (this.tiles.GetLength(1) - 1) * 8f * Sengine.ScreenRatioUpwardsMultiplier.Y)), AssetContainer.pointspritebatch03);
      if (!DrawEntrances)
        return;
      for (int index = 0; index < this.Entrances.Count; ++index)
      {
        this.Entrances[index].fAlpha = 0.7f;
        this.Entrances[index].DrawEntrance(Offset, spritebatch);
      }
    }
  }
}
