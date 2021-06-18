// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.BlockInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.Tile_Data;

namespace TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild
{
  internal class BlockInfo
  {
    public bool IsNextToSomething;
    public bool SomethingIsBlocked;
    public bool FloorReplacingFloor;
    public List<Vector2Int> blocks;
    public List<Vector2Int> blockedEntrances;
    public List<Vector2Int> NonWaterTiles;
    private BUILDINGTYPE Tryngtoplacethis;

    public BlockInfo(BUILDINGTYPE _Tryngtoplacethis) => this.Tryngtoplacethis = _Tryngtoplacethis;

    public void AddCantReachWater(Vector2Int location)
    {
      if (this.NonWaterTiles == null)
        this.NonWaterTiles = new List<Vector2Int>();
      this.NonWaterTiles.Add(location);
    }

    public void AddBlock(Vector2Int location, bool IsForEntrance)
    {
      if (this.blocks == null && !IsForEntrance)
        this.blocks = new List<Vector2Int>();
      else if (this.blockedEntrances == null & IsForEntrance)
        this.blockedEntrances = new List<Vector2Int>();
      if (IsForEntrance)
        this.blockedEntrances.Add(location);
      else
        this.blocks.Add(location);
    }

    public BuildMessageType GetMessageType()
    {
      if (this.blocks != null)
        return BuildMessageType.Overlapping;
      if (this.Tryngtoplacethis == BUILDINGTYPE.Building)
      {
        int num = this.IsNextToSomething ? 1 : 0;
      }
      return BuildMessageType.CanBuild;
    }
  }
}
