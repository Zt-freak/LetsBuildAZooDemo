// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Inventory
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Objects;
using System;
using TinyZoo.Blance;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.Store_Local.Entries;

namespace TinyZoo.PlayerDir
{
  internal class Inventory
  {
    public int DropZones;
    public int DropZonesRemaining;
    public int DropZonesInMaps;
    public int InstantBeams;
    public int InstantBeamsRemaining = 10;
    public int InstantBeamsBeamsInMaps;
    private NumberObfiscatorV1 TotalDropZones;
    public NumberObfiscatorV1 BeamSpdUpgrades;
    public NumberObfiscatorV1 RightBeamUpgrades;
    private CstBase SpeedCost;
    public bool[] SecretAliensAvailable;

    public Inventory() => this.Create();

    private void Create()
    {
      this.TotalDropZones = new NumberObfiscatorV1();
      this.BeamSpdUpgrades = new NumberObfiscatorV1();
      this.RightBeamUpgrades = new NumberObfiscatorV1();
      this.SecretAliensAvailable = new bool[13];
    }

    internal static SecretAliens GetThisEnemyToSecretAlien(AnimalType enemy)
    {
      switch (enemy)
      {
        case AnimalType.Triclops:
          return SecretAliens.Triclops;
        case AnimalType.Bunsen:
          return SecretAliens.Bunsen;
        case AnimalType.Tribble:
          return SecretAliens.Tribble;
        case AnimalType.EmperorPorcupine:
          return SecretAliens.EmperorPorcupine;
        case AnimalType.Hal9000:
          return SecretAliens.Hal9000;
        case AnimalType.Leeloo:
          return SecretAliens.Leeloo;
        case AnimalType.Riddick:
          return SecretAliens.Riddick;
        case AnimalType.Tardigrade:
          return SecretAliens.Tardigrade;
        case AnimalType.Galaxian:
          return SecretAliens.Galaxian;
        case AnimalType.Bill:
          return SecretAliens.Bill;
        case AnimalType.Gremlin:
          return SecretAliens.Gremlin;
        case AnimalType.Krampus:
          return SecretAliens.Krampus;
        case AnimalType.Grinch:
          return SecretAliens.Grinch;
        default:
          throw new Exception("NOOOOO");
      }
    }

    public AnimalType GetThisSecretAlien(int Index)
    {
      switch (Index)
      {
        case 0:
          return AnimalType.Triclops;
        case 1:
          return AnimalType.Bunsen;
        case 2:
          return AnimalType.Tribble;
        case 3:
          return AnimalType.EmperorPorcupine;
        case 4:
          return AnimalType.Hal9000;
        case 5:
          return AnimalType.Leeloo;
        case 6:
          return AnimalType.Riddick;
        case 7:
          return AnimalType.Tardigrade;
        case 8:
          return AnimalType.Galaxian;
        case 9:
          return AnimalType.Bill;
        case 10:
          return AnimalType.Gremlin;
        case 11:
          return AnimalType.Krampus;
        case 12:
          return AnimalType.Grinch;
        default:
          throw new Exception("NOOOOO");
      }
    }

    public int GetCost(StoreEntryType thisentry, Player player)
    {
      switch (thisentry)
      {
        case StoreEntryType.BasicBeam:
          return 0;
        case StoreEntryType.BeamSpeed:
          if (this.SpeedCost == null)
          {
            this.SpeedCost = new CstBase();
            this.SpeedCost.SetUpBell(250, 2f, 0.91f);
          }
          return this.SpeedCost.GetValue(player.inventory.BeamSpdUpgrades.GetUnvallidatedValue());
        default:
          return 999999999;
      }
    }

    public void PurchasedThis(StoreEntryType storeEntryType)
    {
      switch (storeEntryType)
      {
        case StoreEntryType.BeamSpeed:
          this.BeamSpdUpgrades.SmartAddValue_MinimumThreshold(false, 1, 0);
          break;
        case StoreEntryType.BeamL2:
          this.RightBeamUpgrades.SmartAddValue_MinimumThreshold(false, 1, 0);
          break;
        case StoreEntryType.InstaBeam:
          ++this.InstantBeamsRemaining;
          ++this.InstantBeams;
          break;
        case StoreEntryType.BeamSpeedL2:
          ++this.DropZones;
          ++this.DropZonesRemaining;
          break;
      }
    }

    public void SaveInventory(Writer writer)
    {
      this.TotalDropZones.SaveObfiscator(writer);
      this.BeamSpdUpgrades.SaveObfiscator(writer);
      this.RightBeamUpgrades.SaveObfiscator(writer);
      writer.WriteInt("i", this.SecretAliensAvailable.Length);
      for (int index = 0; index < this.SecretAliensAvailable.Length; ++index)
        writer.WriteBool("i", this.SecretAliensAvailable[index]);
    }

    public Inventory(Reader reader)
    {
      this.Create();
      this.TotalDropZones.LoadObfiscatorValuesFromFile(reader);
      this.BeamSpdUpgrades.LoadObfiscatorValuesFromFile(reader);
      this.RightBeamUpgrades.LoadObfiscatorValuesFromFile(reader);
      int _out = 0;
      int num1 = (int) reader.ReadInt("i", ref _out);
      for (int index = 0; index < _out; ++index)
      {
        int num2 = (int) reader.ReadBool("i", ref this.SecretAliensAvailable[index]);
      }
    }
  }
}
