// Decompiled with JetBrains decompiler
// Type: TinyZoo.ArcadeMode.ArcadeData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.SwitchRandom;
using TinyZoo.Tile_Data;

namespace TinyZoo.ArcadeMode
{
  internal class ArcadeData
  {
    internal static LayoutData layout;

    internal static WaveInfo GetWaveInfo()
    {
      List<IntakePerson> _People = new List<IntakePerson>();
      switch (GameFlags.ArcadeLevel)
      {
        case 0:
          for (int index = 0; index < 2; ++index)
            _People.Add(new IntakePerson(AnimalType.Rabbit));
          _People.Add(new IntakePerson(AnimalType.Bear));
          return new WaveInfo(_People);
        case 1:
          _People.Add(new IntakePerson(AnimalType.Rabbit));
          _People.Add(new IntakePerson(AnimalType.Horse));
          _People.Add(new IntakePerson(AnimalType.Camel));
          return new WaveInfo(_People);
        case 2:
          _People.Add(new IntakePerson(AnimalType.Tortoise));
          _People.Add(new IntakePerson(AnimalType.Snake));
          _People.Add(new IntakePerson(AnimalType.Meerkat));
          return new WaveInfo(_People);
        case 3:
          for (int index = 0; index < 2; ++index)
            _People.Add(new IntakePerson(AnimalType.Armadillo));
          _People.Add(new IntakePerson(AnimalType.Goose));
          return new WaveInfo(_People);
        case 4:
          _People.Add(new IntakePerson(AnimalType.Goose));
          _People.Add(new IntakePerson(AnimalType.Bear));
          _People.Add(new IntakePerson(AnimalType.Meerkat));
          _People.Add(new IntakePerson(AnimalType.Meerkat));
          return new WaveInfo(_People);
        case 5:
          _People.Add(new IntakePerson(AnimalType.Horse));
          _People.Add(new IntakePerson(AnimalType.Hyena));
          _People.Add(new IntakePerson(AnimalType.Porcupine));
          _People.Add(new IntakePerson(AnimalType.Tapir));
          return new WaveInfo(_People);
        case 6:
          _People.Add(new IntakePerson(AnimalType.Ostrich));
          for (int index = 0; index < 4; ++index)
            _People.Add(new IntakePerson(AnimalType.Porcupine));
          return new WaveInfo(_People);
        case 7:
          _People.Add(new IntakePerson(AnimalType.Ostrich));
          _People.Add(new IntakePerson(AnimalType.Tapir));
          _People.Add(new IntakePerson(AnimalType.Bear));
          return new WaveInfo(_People);
        case 8:
          _People.Add(new IntakePerson(AnimalType.Camel));
          _People.Add(new IntakePerson(AnimalType.Armadillo));
          _People.Add(new IntakePerson(AnimalType.Cow));
          _People.Add(new IntakePerson(AnimalType.Horse));
          return new WaveInfo(_People);
        case 9:
          _People.Add(new IntakePerson(AnimalType.Chicken));
          _People.Add(new IntakePerson(AnimalType.Orangutan));
          _People.Add(new IntakePerson(AnimalType.Bear));
          _People.Add(new IntakePerson(AnimalType.Bear));
          _People.Add(new IntakePerson(AnimalType.Snake));
          return new WaveInfo(_People);
        case 10:
          _People.Add(new IntakePerson(AnimalType.Penguin));
          _People.Add(new IntakePerson(AnimalType.Armadillo));
          _People.Add(new IntakePerson(AnimalType.Chicken));
          _People.Add(new IntakePerson(AnimalType.Chicken));
          return new WaveInfo(_People);
        case 11:
          _People.Add(new IntakePerson(AnimalType.Antelope));
          _People.Add(new IntakePerson(AnimalType.Chicken));
          _People.Add(new IntakePerson(AnimalType.Armadillo));
          _People.Add(new IntakePerson(AnimalType.Badger));
          return new WaveInfo(_People);
        case 12:
          _People.Add(new IntakePerson(AnimalType.Panther));
          _People.Add(new IntakePerson(AnimalType.Orangutan));
          _People.Add(new IntakePerson(AnimalType.Capybara));
          _People.Add(new IntakePerson(AnimalType.Capybara));
          _People.Add(new IntakePerson(AnimalType.Cow));
          return new WaveInfo(_People);
        case 13:
          _People.Add(new IntakePerson(AnimalType.Pig));
          _People.Add(new IntakePerson(AnimalType.Orangutan));
          _People.Add(new IntakePerson(AnimalType.Orangutan));
          _People.Add(new IntakePerson(AnimalType.Badger));
          return new WaveInfo(_People);
        case 14:
          _People.Add(new IntakePerson(AnimalType.Seal));
          _People.Add(new IntakePerson(AnimalType.Orangutan));
          _People.Add(new IntakePerson(AnimalType.Rabbit));
          return new WaveInfo(_People);
        case 15:
          _People.Add(new IntakePerson(AnimalType.Wolf));
          _People.Add(new IntakePerson(AnimalType.Panther));
          _People.Add(new IntakePerson(AnimalType.Panther));
          _People.Add(new IntakePerson(AnimalType.Porcupine));
          _People.Add(new IntakePerson(AnimalType.Donkey));
          return new WaveInfo(_People);
        case 16:
          _People.Add(new IntakePerson(AnimalType.Lemur));
          _People.Add(new IntakePerson(AnimalType.Wolf));
          _People.Add(new IntakePerson(AnimalType.Lemur));
          _People.Add(new IntakePerson(AnimalType.Wolf));
          _People.Add(new IntakePerson(AnimalType.Lemur));
          _People.Add(new IntakePerson(AnimalType.Wolf));
          _People.Add(new IntakePerson(AnimalType.Lemur));
          _People.Add(new IntakePerson(AnimalType.Wolf));
          return new WaveInfo(_People);
        case 17:
          _People.Add(new IntakePerson(AnimalType.Alpaca));
          _People.Add(new IntakePerson(AnimalType.PolarBear));
          _People.Add(new IntakePerson(AnimalType.Panther));
          _People.Add(new IntakePerson(AnimalType.Ostrich));
          _People.Add(new IntakePerson(AnimalType.Duck));
          return new WaveInfo(_People);
        case 18:
          _People.Add(new IntakePerson(AnimalType.Otter));
          _People.Add(new IntakePerson(AnimalType.Alpaca));
          return new WaveInfo(_People);
        case 19:
          _People.Add(new IntakePerson(AnimalType.KomodoDragon));
          _People.Add(new IntakePerson(AnimalType.Panther));
          _People.Add(new IntakePerson(AnimalType.Ostrich));
          _People.Add(new IntakePerson(AnimalType.Horse));
          _People.Add(new IntakePerson(AnimalType.Hyena));
          _People.Add(new IntakePerson(AnimalType.Walrus));
          return new WaveInfo(_People);
        case 20:
          _People.Add(new IntakePerson(AnimalType.PolarBear));
          _People.Add(new IntakePerson(AnimalType.Otter));
          for (int index = 0; index < 5; ++index)
            _People.Add(new IntakePerson(AnimalType.Duck));
          return new WaveInfo(_People);
        case 21:
          _People.Add(new IntakePerson(AnimalType.Peacock));
          _People.Add(new IntakePerson(AnimalType.Platypus));
          _People.Add(new IntakePerson(AnimalType.KomodoDragon));
          _People.Add(new IntakePerson(AnimalType.Panther));
          _People.Add(new IntakePerson(AnimalType.Tortoise));
          _People.Add(new IntakePerson(AnimalType.Capybara));
          return new WaveInfo(_People);
        case 22:
          for (int index = 0; index < 4; ++index)
            _People.Add(new IntakePerson(AnimalType.WildBoar));
          for (int index = 0; index < 3; ++index)
            _People.Add(new IntakePerson(AnimalType.Cheetah));
          return new WaveInfo(_People);
        case 23:
          _People.Add(new IntakePerson(AnimalType.Crocodile));
          _People.Add(new IntakePerson(AnimalType.Donkey));
          _People.Add(new IntakePerson(AnimalType.Goose));
          return new WaveInfo(_People);
        case 24:
          for (int index = 0; index < 6; ++index)
            _People.Add(new IntakePerson(AnimalType.Crocodile));
          for (int index = 0; index < 2; ++index)
            _People.Add(new IntakePerson(AnimalType.Peacock));
          for (int index = 0; index < 3; ++index)
            _People.Add(new IntakePerson(AnimalType.PolarBear));
          return new WaveInfo(_People);
        case 25:
          _People.Add(new IntakePerson(AnimalType.Deer));
          _People.Add(new IntakePerson(AnimalType.Deer));
          for (int index = 0; index < 2; ++index)
            _People.Add(new IntakePerson(AnimalType.Orangutan));
          for (int index = 0; index < 3; ++index)
            _People.Add(new IntakePerson(AnimalType.Horse));
          return new WaveInfo(_People);
        case 26:
          _People.Add(new IntakePerson(AnimalType.Flamingo));
          for (int index = 0; index < 9; ++index)
            _People.Add(new IntakePerson(AnimalType.Hyena));
          return new WaveInfo(_People);
        case 27:
          _People.Add(new IntakePerson(AnimalType.Tiger));
          _People.Add(new IntakePerson(AnimalType.Horse));
          _People.Add(new IntakePerson(AnimalType.Badger));
          _People.Add(new IntakePerson(AnimalType.Crocodile));
          return new WaveInfo(_People);
        case 28:
          _People.Add(new IntakePerson(AnimalType.Fox));
          return new WaveInfo(_People);
        case 29:
          for (int index = 0; index < 10; ++index)
            _People.Add(new IntakePerson(AnimalType.KomodoDragon));
          return new WaveInfo(_People);
        default:
          return (WaveInfo) null;
      }
    }

    internal static RandomResultContainer GetSeededRandom() => !SEngine.Flags.PlatformIsSwitch ? new RandomResultContainer(new Random(ArcadeData.GetWaveRandomSeed())) : new RandomResultContainer(ArcadeData.GetWaveRandomSeed());

    internal static int GetWaveRandomSeed()
    {
      switch (GameFlags.ArcadeLevel)
      {
        case 2:
          return 8474;
        case 7:
          return 1238;
        case 9:
          return 239;
        case 12:
          return 12321;
        case 14:
          return 12263;
        case 18:
          return 2352;
        case 27:
          return 890;
        default:
          return GameFlags.ArcadeLevel;
      }
    }

    internal static float GetBeamSpeed()
    {
      switch (GameFlags.ArcadeLevel)
      {
        case 0:
          return 60f;
        case 1:
          return 60f;
        case 2:
          return 60f;
        case 3:
          return 60f;
        case 4:
          return 60f;
        case 5:
          return 60f;
        case 6:
          return 100f;
        case 7:
          return 75f;
        case 8:
          return 75f;
        case 9:
          return 75f;
        case 10:
          return 75f;
        case 11:
          return 75f;
        case 12:
          return 75f;
        case 13:
          return 80f;
        case 14:
          return 100f;
        case 15:
          return 90f;
        case 16:
          return 120f;
        case 17:
          return 120f;
        case 18:
          return 100f;
        case 19:
          return 100f;
        case 20:
          return 150f;
        case 21:
          return 150f;
        case 22:
          return 100f;
        case 23:
          return 100f;
        case 24:
          return 1000f;
        case 25:
          return 75f;
        case 26:
          return 100f;
        case 27:
          return 100f;
        case 28:
          return 25f;
        case 29:
          return 300f;
        default:
          return 1000f;
      }
    }

    internal static int GetBeamsForThisLevel()
    {
      switch (GameFlags.ArcadeLevel)
      {
        case 0:
          return 4;
        case 1:
          return 5;
        case 2:
          return 4;
        case 3:
          return 4;
        case 4:
          return 4;
        case 5:
          return 5;
        case 6:
          return 5;
        case 7:
          return 3;
        case 8:
          return 3;
        case 9:
          return 4;
        case 10:
          return 4;
        case 11:
          return 4;
        case 12:
          return 4;
        case 13:
          return 3;
        case 14:
          return 2;
        case 15:
          return 4;
        case 16:
          return 4;
        case 17:
          return 4;
        case 18:
          return 2;
        case 19:
          return 5;
        case 20:
          return 9;
        case 21:
          return 6;
        case 22:
          return 7;
        case 23:
          return 2;
        case 24:
          return 10;
        case 25:
          return 6;
        case 26:
          return 12;
        case 27:
          return 5;
        case 28:
          return 1;
        case 29:
          return 12;
        default:
          return 10;
      }
    }

    internal static PrisonZone GetMap()
    {
      int num1 = 14;
      int num2 = 7;
      CellBlockType cellBlockType = CellBlockType.Grasslands;
      switch (GameFlags.ArcadeLevel)
      {
        case 1:
          num1 = 17;
          break;
        case 2:
          num1 = 18;
          break;
        case 3:
          num2 = 14;
          num1 = 7;
          break;
        case 4:
          num1 = 14;
          break;
        case 5:
          cellBlockType = CellBlockType.Desert;
          num2 = 13;
          num1 = 11;
          break;
        case 6:
          cellBlockType = CellBlockType.Desert;
          num1 = 14;
          num2 = 8;
          break;
        case 7:
          cellBlockType = CellBlockType.Desert;
          num1 = 14;
          num2 = 8;
          break;
        case 8:
          cellBlockType = CellBlockType.Desert;
          num1 = 20;
          break;
        case 9:
          cellBlockType = CellBlockType.Desert;
          num1 = 20;
          break;
        case 10:
          cellBlockType = CellBlockType.Mountain;
          num1 = 14;
          num2 = 10;
          break;
        case 11:
          cellBlockType = CellBlockType.Mountain;
          num1 = 8;
          num2 = 12;
          break;
        case 12:
          cellBlockType = CellBlockType.Mountain;
          num1 = 5;
          num2 = 18;
          break;
        case 13:
          cellBlockType = CellBlockType.Mountain;
          num1 = 17;
          num2 = 4;
          break;
        case 14:
          cellBlockType = CellBlockType.Mountain;
          num1 = 7;
          num2 = 7;
          break;
        case 15:
          cellBlockType = CellBlockType.Savannah;
          num1 = 12;
          num2 = 8;
          break;
        case 16:
          cellBlockType = CellBlockType.Savannah;
          num1 = 18;
          num2 = 18;
          break;
        case 17:
          cellBlockType = CellBlockType.Savannah;
          num1 = 5;
          num2 = 20;
          break;
        case 18:
          cellBlockType = CellBlockType.Savannah;
          num2 = 9;
          num1 = 9;
          break;
        case 19:
          cellBlockType = CellBlockType.Savannah;
          num2 = 6;
          num1 = 10;
          break;
        case 20:
          cellBlockType = CellBlockType.CorruptedForest;
          num1 = 11;
          num2 = 11;
          break;
        case 21:
          cellBlockType = CellBlockType.CorruptedForest;
          break;
        case 22:
          cellBlockType = CellBlockType.CorruptedForest;
          num1 = 20;
          num2 = 20;
          break;
        case 23:
          cellBlockType = CellBlockType.CorruptedForest;
          num1 = 3;
          num2 = 3;
          break;
        case 24:
          cellBlockType = CellBlockType.CorruptedForest;
          num1 = 10;
          num2 = 10;
          break;
        case 25:
          cellBlockType = CellBlockType.GraveYard;
          num1 = 16;
          break;
        case 26:
          cellBlockType = CellBlockType.GraveYard;
          num2 = 4;
          num1 = 18;
          break;
        case 27:
          cellBlockType = CellBlockType.GraveYard;
          num2 = 12;
          num1 = 5;
          break;
        case 28:
          cellBlockType = CellBlockType.CorruptedGrass;
          num2 = 7;
          num1 = 16;
          break;
        case 29:
          cellBlockType = CellBlockType.FieldPicketFence;
          break;
      }
      ArcadeData.layout = new LayoutData();
      ArcadeData.layout.AddNewCellBlock(new Vector2Int(1, 1), new Vector2Int(num1, num2), true, (WallsAndFloorsManager) null, cellBlockType);
      return new PrisonZone(1, 1, num1, num2, 0, cellBlockType, new Vector2Int(-1, -1));
    }
  }
}
