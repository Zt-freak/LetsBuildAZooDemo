// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Bounty
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Objects;
using SEngine.Time;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.Tile_Data;

namespace TinyZoo.PlayerDir
{
  internal class Bounty
  {
    public DailyCheckIn bountyevent;
    public AnimalType enemy;
    public NumberObfiscatorV1 reward;
    public NumberObfiscatorV1 CAAP;
    internal static float BeamSpeed;
    internal static Vector3 InnerCLR;
    internal static Vector3 OuterCLR;
    internal static int LastRNDSeed = 0;
    internal static TILETYPE FloorType;
    internal static TILETYPE FloorInnerCorner;
    internal static TILETYPE FloorInnerEdge;
    internal static TILETYPE WallEdge = TILETYPE.Rockwall_WallSide;
    internal static TILETYPE WallCorner = TILETYPE.Rockwall_WallCorner;
    private List<IntakePerson> people;

    public Bounty()
    {
      this.bountyevent = new DailyCheckIn();
      this.reward = new NumberObfiscatorV1();
      this.CAAP = new NumberObfiscatorV1();
      this.CAAP.ForceSetNewValue(10000);
    }

    internal static TILETYPE GetWall(bool IsCorner) => IsCorner ? Bounty.WallCorner : Bounty.WallEdge;

    internal static TILETYPE GetInnerWall(bool IsCorner) => IsCorner ? Bounty.FloorInnerCorner : Bounty.FloorInnerEdge;

    public void SetReward(Player player)
    {
    }

    public List<IntakePerson> GetPeople(Player player)
    {
      this.people = new List<IntakePerson>();
      Bounty.LastRNDSeed = this.bountyevent.GetDayOfThisCheckIn_UseForSeed(player.Stats.datetimemanager);
      Random random = new Random(Bounty.LastRNDSeed);
      int num = random.Next(3, 12);
      if (random.Next(0, 3) == 0)
        num = random.Next(3, 8);
      if (random.Next(0, 3) == 0)
      {
        if (player.Stats.research.AnimalsResearchedLength() < 3)
        {
          this.people.Add(new IntakePerson((AnimalType) random.Next(0, 5)));
        }
        else
        {
          for (int index = 0; index < num; ++index)
            this.people.Add(new IntakePerson(player.Stats.research.GetResearchedAnimalByIndex(random.Next(0, player.Stats.research.AnimalsResearchedLength()))));
        }
      }
      else
      {
        AnimalType _enemytype = player.Stats.research.GetResearchedAnimalByIndex(random.Next(0, player.Stats.research.AnimalsResearchedLength()));
        if (player.Stats.research.AnimalsResearchedLength() < 3)
          _enemytype = (AnimalType) random.Next(0, 5);
        for (int index = 0; index < num; ++index)
          this.people.Add(new IntakePerson(_enemytype));
      }
      return this.people;
    }

    internal static Rectangle GetBlakOut()
    {
      Rectangle anyRectangle = TileData.GetTileInfo(Bounty.WallCorner).GetAnyRectangle();
      return new Rectangle(anyRectangle.X, anyRectangle.Y, 1, 1);
    }

    public PrisonZone GetZone(Player player)
    {
      Random Rnd = new Random(this.bountyevent.GetDayOfThisCheckIn_UseForSeed(player.Stats.datetimemanager));
      Bounty.BeamSpeed = (float) Rnd.Next(100, 200);
      int num1 = 10;
      Bounty.InnerCLR = Vector3.One;
      Bounty.OuterCLR = Vector3.One;
      Bounty.FloorInnerCorner = TILETYPE.Rockwall_InnerWallCorner;
      Bounty.FloorInnerEdge = TILETYPE.Rockwall_InnerWallSide;
      Bounty.FloorType = TILETYPE.Rockwall_Floor;
      Bounty.WallCorner = TILETYPE.Rockwall_WallCorner;
      Bounty.WallEdge = TILETYPE.Rockwall_WallSide;
      switch (Rnd.Next(0, 3))
      {
        case 0:
          Bounty.WallCorner = TILETYPE.Mudwall_WallCorner;
          Bounty.WallEdge = TILETYPE.Mudwall_WallSide;
          break;
        case 1:
          Bounty.WallCorner = TILETYPE.GrassWall_WallCorner;
          Bounty.WallEdge = TILETYPE.GrassWall_WallSide;
          break;
      }
      switch (Rnd.Next(0, 3))
      {
        case 0:
          Bounty.FloorInnerCorner = TILETYPE.Mudwall_InnerWallCorner;
          Bounty.FloorInnerEdge = TILETYPE.Mudwall_InnerWallSide;
          Bounty.FloorType = TILETYPE.Mudwall_Floor;
          break;
        case 1:
          Bounty.FloorInnerCorner = TILETYPE.GrassWall_InnerWallCorner;
          Bounty.FloorInnerEdge = TILETYPE.GrassWall_InnerWallSide;
          Bounty.FloorType = TILETYPE.GrassWall_Floor;
          break;
      }
      if (Rnd.Next(0, 3) == 0)
        Bounty.InnerCLR = GetRandomColour.GetRandomBrightColour(Rnd);
      if (Rnd.Next(0, 3) == 0)
        Bounty.OuterCLR = GetRandomColour.GetRandomBrightColour(Rnd);
      if (this.people != null)
        num1 = this.people.Count;
      int num2;
      int num3;
      if (num1 > 7)
      {
        int num4 = Rnd.Next(0, 6);
        if (num4 < 2)
        {
          num2 = Rnd.Next(4, 10);
          num3 = Rnd.Next(11, 22);
        }
        else if (num4 == 3)
        {
          num3 = Rnd.Next(3, 10);
          num2 = Rnd.Next(10, 21);
        }
        else
        {
          num3 = Rnd.Next(11, 25);
          num2 = Rnd.Next(11, 22);
        }
      }
      else if (num1 > 5)
      {
        int num4 = Rnd.Next(0, 6);
        if (num4 < 2)
        {
          num2 = Rnd.Next(4, 10);
          num3 = Rnd.Next(11, 20);
        }
        else if (num4 == 3)
        {
          num3 = Rnd.Next(4, 10);
          num2 = Rnd.Next(10, 19);
        }
        else
        {
          num3 = Rnd.Next(11, 23);
          num2 = Rnd.Next(11, 18);
        }
      }
      else
      {
        int num4 = Rnd.Next(0, 6);
        if (num4 < 2)
        {
          num2 = Rnd.Next(4, 10);
          num3 = Rnd.Next(8, 10);
        }
        else if (num4 == 3)
        {
          num3 = Rnd.Next(4, 10);
          num2 = Rnd.Next(8, 10);
        }
        else
        {
          num3 = Rnd.Next(4, 10);
          num2 = Rnd.Next(4, 10);
        }
      }
      new LayoutData().AddNewCellBlock(new Vector2Int(1, 1), new Vector2Int(num3, num2), true, (WallsAndFloorsManager) null, CellBlockType.Mountain);
      return new PrisonZone(1, 1, num3, num2, 0, CellBlockType.Mountain, new Vector2Int(-1, -1))
      {
        prisonercontainer = new PrisonerContainer()
      };
    }

    public void SaveBounty(Writer writer)
    {
      this.bountyevent.SaveDailyCheckIn(writer);
      writer.WriteInt("b", (int) this.enemy);
      this.CAAP.SaveObfiscator(writer);
    }

    public Bounty(Reader reader)
    {
      this.bountyevent = new DailyCheckIn(reader);
      int _out = 0;
      int num = (int) reader.ReadInt("b", ref _out);
      this.enemy = (AnimalType) _out;
      this.CAAP = new NumberObfiscatorV1(reader);
    }
  }
}
