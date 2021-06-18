// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Dungeon.DungoenTileRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TinyZoo.ArcadeMode;
using TinyZoo.BreakOut;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;

namespace TinyZoo.GamePlay.Dungeon
{
  internal class DungoenTileRenderer
  {
    private TileRenderer[,] tilerrenders;

    public DungoenTileRenderer(Player player)
    {
      PrisonZone prisonZone;
      if (GameFlags.IsArcadeMode)
        prisonZone = ArcadeData.GetMap();
      else if (GameFlags.BountyMode)
      {
        player.livestats.waveinfo = new WaveInfo(player.Stats.bounty.GetPeople(player));
        player.livestats.waveinfoFromPrison = new WaveInfo(new IntakeInfo());
        prisonZone = player.Stats.bounty.GetZone(player);
      }
      else if (GameFlags.IsBreakOut)
      {
        player.livestats.waveinfoFromPrison.People = new List<IntakePerson>();
        prisonZone = BreakOutData.GetMap();
      }
      else
        prisonZone = player.prisonlayout.GetThisCellBlock(player.livestats.SelectedPrisonID);
      prisonZone.SetMapLimits();
      Random random = new Random(363);
      this.tilerrenders = new TileRenderer[prisonZone.WidthAndHeight.X + 2, prisonZone.WidthAndHeight.Y + 2];
      for (int index1 = 0; index1 < this.tilerrenders.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.tilerrenders.GetLength(1); ++index2)
        {
          if (GameFlags.IsBreakOut)
          {
            LayoutEntry layourentry = new LayoutEntry(TILETYPE.Grasslands_Floor);
            this.tilerrenders[index1, index2] = new TileRenderer(layourentry, index1 + (prisonZone.TopLeftFloorSpace.X - 1), index2 + (prisonZone.TopLeftFloorSpace.Y - 1), layourentry.isChild());
          }
          else if (GameFlags.BountyMode)
          {
            LayoutEntry layourentry = new LayoutEntry(Bounty.FloorType);
            bool flag = true;
            if (index2 == 0 || index1 == 0 || (index1 == this.tilerrenders.GetLength(0) - 1 || index2 == this.tilerrenders.GetLength(1) - 1))
            {
              flag = false;
              int num = 0;
              if (index2 == 0)
                num = 1;
              else if (index2 == this.tilerrenders.GetLength(1) - 1)
                num = 3;
              else if (index1 == this.tilerrenders.GetLength(0) - 1)
                num = 2;
              bool IsCorner = false;
              if (index2 == 0 && index1 == 0)
              {
                IsCorner = true;
                num = 0;
              }
              else if (index2 == 0 && index1 == this.tilerrenders.GetLength(0) - 1)
              {
                IsCorner = true;
                num = 1;
              }
              else if (index2 == this.tilerrenders.GetLength(1) - 1 && index1 == this.tilerrenders.GetLength(0) - 1)
              {
                num = 2;
                IsCorner = true;
              }
              else if (index2 == this.tilerrenders.GetLength(1) - 1 && index1 == 0)
              {
                num = 3;
                IsCorner = true;
              }
              layourentry = new LayoutEntry(Bounty.GetWall(IsCorner));
              layourentry.RotationClockWise = num;
            }
            else if (Bounty.FloorInnerEdge != Bounty.FloorType && (index2 == 1 || index1 == 1 || (index1 == this.tilerrenders.GetLength(0) - 2 || index2 == this.tilerrenders.GetLength(1) - 2)))
            {
              int num = 0;
              if (index2 == 1)
                num = 1;
              else if (index2 == this.tilerrenders.GetLength(1) - 2)
                num = 3;
              else if (index1 == this.tilerrenders.GetLength(0) - 2)
                num = 2;
              bool IsCorner = false;
              if (index2 == 1 && index1 == 1)
              {
                IsCorner = true;
                num = 0;
              }
              else if (index2 == 1 && index1 == this.tilerrenders.GetLength(0) - 2)
              {
                IsCorner = true;
                num = 1;
              }
              else if (index2 == this.tilerrenders.GetLength(1) - 2 && index1 == this.tilerrenders.GetLength(0) - 2)
              {
                num = 2;
                IsCorner = true;
              }
              else if (index2 == this.tilerrenders.GetLength(1) - 2 && index1 == 1)
              {
                num = 3;
                IsCorner = true;
              }
              layourentry = new LayoutEntry(Bounty.GetInnerWall(IsCorner));
              layourentry.RotationClockWise = num;
            }
            this.tilerrenders[index1, index2] = new TileRenderer(layourentry, index1 + (prisonZone.TopLeftFloorSpace.X - 1), index2 + (prisonZone.TopLeftFloorSpace.Y - 1), layourentry.isChild());
            if (flag)
              this.tilerrenders[index1, index2].SetAllColours(Bounty.InnerCLR);
            else
              this.tilerrenders[index1, index2].SetAllColours(Bounty.OuterCLR);
          }
          else
          {
            LayoutEntry thisDungeonTile = player.prisonlayout.GetThisDungeonTile(index1 + (prisonZone.TopLeftFloorSpace.X - 1), index2 + (prisonZone.TopLeftFloorSpace.Y - 1));
            this.tilerrenders[index1, index2] = new TileRenderer(thisDungeonTile, index1 + (prisonZone.TopLeftFloorSpace.X - 1), index2 + (prisonZone.TopLeftFloorSpace.Y - 1), thisDungeonTile.isChild());
          }
        }
      }
    }

    public void UpdateFloor(float DeltaTime)
    {
      for (int index1 = 0; index1 < this.tilerrenders.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.tilerrenders.GetLength(1); ++index2)
          this.tilerrenders[index1, index2].UpdateTileRenderer(DeltaTime);
      }
    }

    public void DrawFloor(ref Vector2 ThreadLoc, ref Vector2 ThreadScale)
    {
      for (int index1 = 0; index1 < this.tilerrenders.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.tilerrenders.GetLength(1); ++index2)
          this.tilerrenders[index1, index2].DrawTileRenderer(AssetContainer.pointspritebatch01, ref ThreadLoc, ref ThreadScale);
      }
    }
  }
}
