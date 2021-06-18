// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Quarantine.AnimalQuarantine
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.PlayerDir.Quarantine
{
  internal class AnimalQuarantine
  {
    private List<QuarantineBuilding> quarantineBuildings;
    private QuarantinePeriod QuarantinePeriod_BlackMarket;
    private QuarantinePeriod QuarantinePeriod_Shelter;
    private QuarantinePeriod QuarantinePeriod_ZooTrades;
    public static int MaxSlotsPerQuarantineBuidling = 20;

    public AnimalQuarantine()
    {
      this.quarantineBuildings = new List<QuarantineBuilding>();
      this.QuarantinePeriod_BlackMarket = QuarantinePeriod.None;
      this.QuarantinePeriod_Shelter = QuarantinePeriod.None;
      this.QuarantinePeriod_ZooTrades = QuarantinePeriod.None;
    }

    public void StartNewDay(Player player)
    {
      for (int index = 0; index < this.quarantineBuildings.Count; ++index)
        this.quarantineBuildings[index].StartNewDay(player);
    }

    public void AddQuaratineBuilding(int UID) => this.quarantineBuildings.Add(new QuarantineBuilding(UID));

    public QuarantineBuilding GetThisQuarantineBuilding(int UID)
    {
      for (int index = 0; index < this.quarantineBuildings.Count; ++index)
      {
        if (this.quarantineBuildings[index].UID == UID)
          return this.quarantineBuildings[index];
      }
      return (QuarantineBuilding) null;
    }

    public bool SoldQuarantineBuilding(int UID)
    {
      for (int index = 0; index < this.quarantineBuildings.Count; ++index)
      {
        if (this.quarantineBuildings[index].UID == UID)
        {
          this.quarantineBuildings.RemoveAt(index);
          return true;
        }
      }
      return false;
    }

    public bool TryAddAnimalToAQuarantineBuilding(
      PrisonerInfo prisonerInfo,
      Player player,
      bool TransferredFromPen,
      CityName cityName = CityName.Count)
    {
      QuarantineBuilding freeSlotForAnimals = this.FindABuildingWithFreeSlotForAnimals();
      if (freeSlotForAnimals == null)
        return false;
      freeSlotForAnimals.AddNewQuaratinedAnimal(prisonerInfo, player, TransferredFromPen, cityName);
      return true;
    }

    public QuarantineBuilding FindABuildingWithFreeSlotForAnimals()
    {
      for (int index = 0; index < this.quarantineBuildings.Count; ++index)
      {
        if (this.quarantineBuildings[index].GetListOfQuarantinedAnimals().Count < AnimalQuarantine.MaxSlotsPerQuarantineBuidling)
          return this.quarantineBuildings[index];
      }
      return (QuarantineBuilding) null;
    }

    public int GetNumberOfBuildings() => this.quarantineBuildings.Count;

    public int GetNumberOfDaysForQuarantine(QuarantinePeriod period)
    {
      switch (period)
      {
        case QuarantinePeriod.None:
          return 0;
        case QuarantinePeriod.OneWeek:
          return 7;
        case QuarantinePeriod.TwoWeek:
          return 14;
        case QuarantinePeriod.Infinite:
          return -1;
        default:
          return -1;
      }
    }

    public int GetNumberOfDaysForQuarantine(bool TransferredFromPen, CityName cityName)
    {
      if (TransferredFromPen)
        return -1;
      if (cityName == CityName.BlackMarket)
        return this.GetNumberOfDaysForQuarantine(this.QuarantinePeriod_BlackMarket);
      return cityName == CityName.Shelter ? this.GetNumberOfDaysForQuarantine(this.QuarantinePeriod_Shelter) : this.GetNumberOfDaysForQuarantine(this.QuarantinePeriod_ZooTrades);
    }

    public QuarantinePeriod GetQuarantinePeriodSetting(ImportSource importSource)
    {
      switch (importSource)
      {
        case ImportSource.BlackMarket:
          return this.QuarantinePeriod_BlackMarket;
        case ImportSource.Shelter:
          return this.QuarantinePeriod_Shelter;
        case ImportSource.ZooTrades:
          return this.QuarantinePeriod_ZooTrades;
        default:
          return QuarantinePeriod.Infinite;
      }
    }

    public void SetQuarantinePeriodSetting(ImportSource importSource, QuarantinePeriod setToThis)
    {
      switch (importSource)
      {
        case ImportSource.BlackMarket:
          this.QuarantinePeriod_BlackMarket = setToThis;
          break;
        case ImportSource.Shelter:
          this.QuarantinePeriod_Shelter = setToThis;
          break;
        case ImportSource.ZooTrades:
          this.QuarantinePeriod_ZooTrades = setToThis;
          break;
      }
    }

    public static string GetQuarantinePeriodOptionToString(QuarantinePeriod period)
    {
      switch (period)
      {
        case QuarantinePeriod.None:
          return "None";
        case QuarantinePeriod.OneWeek:
          return "1 Week";
        case QuarantinePeriod.TwoWeek:
          return "2 Weeks";
        case QuarantinePeriod.Infinite:
          return "Infinite";
        default:
          return "NA_" + (object) period;
      }
    }

    public void SaveAnimalQuarantine(Writer writer)
    {
      writer.WriteInt("q", this.quarantineBuildings.Count);
      for (int index = 0; index < this.quarantineBuildings.Count; ++index)
        this.quarantineBuildings[index].SaveQuarantineBuilding(writer);
      writer.WriteInt("q", (int) this.QuarantinePeriod_BlackMarket);
      writer.WriteInt("q", (int) this.QuarantinePeriod_Shelter);
      writer.WriteInt("q", (int) this.QuarantinePeriod_ZooTrades);
    }

    public AnimalQuarantine(Reader reader)
    {
      int _out = 0;
      int num1 = (int) reader.ReadInt("q", ref _out);
      this.quarantineBuildings = new List<QuarantineBuilding>();
      for (int index = 0; index < _out; ++index)
        this.quarantineBuildings.Add(new QuarantineBuilding(reader));
      int num2 = (int) reader.ReadInt("q", ref _out);
      this.QuarantinePeriod_BlackMarket = (QuarantinePeriod) _out;
      int num3 = (int) reader.ReadInt("q", ref _out);
      this.QuarantinePeriod_Shelter = (QuarantinePeriod) _out;
      int num4 = (int) reader.ReadInt("q", ref _out);
      this.QuarantinePeriod_ZooTrades = (QuarantinePeriod) _out;
    }
  }
}
