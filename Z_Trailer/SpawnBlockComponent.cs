// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Trailer.SpawnBlockComponent
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Tile_Data;
using TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components;

namespace TinyZoo.Z_Trailer
{
  internal class SpawnBlockComponent
  {
    private static Random RND = new Random(65);
    private float Delay = -1f;
    private bool Blocked = true;
    public TILETYPE tiletypeonconstruct;
    private bool IsPen;
    private bool SpawnDude;
    private Vector2Int Location;
    internal static Player player;

    public SpawnBlockComponent(
      int XL,
      int YL,
      bool IsFloor,
      bool IsUnderfloor,
      TILETYPE _tiletypeonconstruct)
    {
      this.Location = new Vector2Int(XL, YL);
      this.tiletypeonconstruct = _tiletypeonconstruct;
      SpawnBlockArray.AddToBlockArray(this, XL, YL, IsFloor, IsUnderfloor);
      if (!TrailerDemoFlags.CustomPenSpawn)
        return;
      if (TileData.IsThisAPenBoundary(_tiletypeonconstruct) || TileData.IsThisAPenFloor(_tiletypeonconstruct))
      {
        for (int index = 0; index < TrailerDemoFlags.PenRevealIndexes.Count; ++index)
        {
          if (SpawnBlockComponent.player.prisonlayout.GetThisCellBlock(TrailerDemoFlags.PenRevealIndexes[index]).IsThisLocationInThisDungeon(new Vector2Int(XL, YL)))
          {
            this.IsPen = true;
            return;
          }
        }
      }
      if (!CategoryData.IsThisAFloor(_tiletypeonconstruct) || _tiletypeonconstruct == TILETYPE.Volume_Grass || _tiletypeonconstruct != TILETYPE.Floor_GreyBricks && _tiletypeonconstruct != TILETYPE.Floor_GreyBricks && (_tiletypeonconstruct != TILETYPE.Floor_WoodenBoards && _tiletypeonconstruct != TILETYPE.Floor_MetalDecoTile) && (_tiletypeonconstruct != TILETYPE.Floor_ColorfulBrickTile && _tiletypeonconstruct != TILETYPE.Floor_OrangeTiles && (_tiletypeonconstruct != TILETYPE.Floor_PinkSmallTiles && _tiletypeonconstruct != TILETYPE.Floor_BrownOctoTile)) && (_tiletypeonconstruct != TILETYPE.Floor_BrownSquareTile && _tiletypeonconstruct != TILETYPE.Volume_WoodenBoards && (_tiletypeonconstruct != TILETYPE.Volume_RedPathway && _tiletypeonconstruct != TILETYPE.Floor_Cobblestone) && (_tiletypeonconstruct != TILETYPE.Floor_Dirt && _tiletypeonconstruct != TILETYPE.EMPTY_DIRT_WALKABLE_TILE)))
        return;
      this.SpawnDude = true;
    }

    public void TryAddTopSpawner(ZooBuildingTopRenderer toperenderer, TileRenderer _parent)
    {
      for (int index = 0; index < _parent.rendercomponent.Count; ++index)
      {
        if (_parent.rendercomponent[index].componenttype == RenderComponentType.SpawnAnimBuilding)
        {
          _parent.RefTopRenderer = toperenderer;
          TileInfo tileInfo = TileData.GetTileInfo(_parent.tiletypeonconstruct);
          int tileWidth = tileInfo.GetTileWidth(_parent.RotationOnConstruct);
          (_parent.rendercomponent[index] as SpawnAnimBuilding).AddTopRenderer(_parent, tileInfo, tileWidth);
        }
      }
    }

    public bool UpdateSpawnBlocker(float DeltaTime)
    {
      if (!this.Blocked)
      {
        if ((double) this.Delay > 0.0)
        {
          if (SpawnBlockArray.DoingBigRing)
            DeltaTime *= SpawnBlockArray.BigRingMult;
          this.Delay -= DeltaTime;
          if ((double) this.Delay > 0.0)
            return false;
          this.SpawnDudeNow();
          return true;
        }
        this.SpawnDudeNow();
        return true;
      }
      int tiletypeonconstruct = (int) this.tiletypeonconstruct;
      return !this.Blocked;
    }

    private void SpawnDudeNow()
    {
      int num = this.SpawnDude ? 1 : 0;
    }

    public void Unblock(float _Delay = 0.0f, bool AllowUnblockPen = false)
    {
      if (this.IsPen)
        return;
      if ((double) _Delay < (double) this.Delay || (double) this.Delay == -1.0)
        this.Delay = _Delay;
      int tiletypeonconstruct = (int) this.tiletypeonconstruct;
      this.Blocked = false;
    }
  }
}
