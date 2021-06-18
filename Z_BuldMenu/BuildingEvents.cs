// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.BuildingEvents
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.Z_BuldMenu
{
  internal class BuildingEvents
  {
    internal static void SellStructureAndUpdateRenderer(
      Player player,
      Vector2Int Location,
      OverWorldEnvironmentManager overworldenvironment,
      bool isFloor = false)
    {
      if (isFloor)
        throw new Exception("NOT DONE");
      LayoutEntry _layoutentry = new LayoutEntry(player.prisonlayout.layout.BaseTileTypes[Location.X, Location.Y].tiletype);
      Vector2Int vector2Int = new Vector2Int(Location);
      if (player.prisonlayout.layout.BaseTileTypes[Location.X, Location.Y].isChild())
      {
        vector2Int = player.prisonlayout.layout.BaseTileTypes[Location.X, Location.Y].GetParentLocation();
        _layoutentry.SetChild(vector2Int, player.prisonlayout.layout.BaseTileTypes[Location.X, Location.Y].tiletype);
        _layoutentry.RotationClockWise = player.prisonlayout.layout.BaseTileTypes[vector2Int.X, vector2Int.Y].RotationClockWise;
      }
      else
      {
        _layoutentry.RotationClockWise = player.prisonlayout.layout.BaseTileTypes[Location.X, Location.Y].RotationClockWise;
        _layoutentry.UnsetChild();
      }
      player.prisonlayout.SellStructure(vector2Int, _layoutentry, player.livestats.consumptionstatus, player);
      overworldenvironment.SellStructure(vector2Int, _layoutentry, player.prisonlayout.layout);
    }
  }
}
