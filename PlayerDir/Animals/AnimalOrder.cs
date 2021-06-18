// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Animals.AnimalOrder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.IntakeStuff;

namespace TinyZoo.PlayerDir.Animals
{
  internal class AnimalOrder
  {
    public int OrderUID;
    public int PrisonUID;
    public List<IntakePerson> incominganaimals;
    public int DaysToArrival = -1;
    public int SecondInDayOrArrival;
    private int SecondInDayOfPurchase;
    public bool InTransit;
    public bool IsBlackMarket;

    public AnimalOrder(WaveInfo AnimalsJustTraded, int _PrisonUID)
    {
      this.IsBlackMarket = AnimalsJustTraded.CameFromHere == CityName.BlackMarket;
      this.OrderUID = LiveStats.AnimalOrderUID;
      ++LiveStats.AnimalOrderUID;
      this.PrisonUID = _PrisonUID;
      this.incominganaimals = new List<IntakePerson>();
      float travelTime = this.GetTravelTime(AnimalsJustTraded.CameFromHere);
      this.DaysToArrival = (int) travelTime;
      this.SecondInDayOfPurchase = (int) Z_GameFlags.DayTimer;
      this.SecondInDayOrArrival = (int) Z_GameFlags.DayTimer;
      float num = travelTime - (float) this.DaysToArrival;
      for (int index = 0; index < AnimalsJustTraded.People.Count; ++index)
        this.incominganaimals.Add(AnimalsJustTraded.People[index]);
    }

    private float GetTravelTime(CityName cityname)
    {
      switch (cityname)
      {
        case CityName.Sydney:
          return 0.0f;
        case CityName.London:
          return 1f;
        case CityName.Shelter:
          return 1.5f;
        default:
          return 1f;
      }
    }

    public WaveInfo GetAnimalsAsWaveInfo() => new WaveInfo(this.incominganaimals);

    public bool StartNewDay()
    {
      --this.DaysToArrival;
      if (this.DaysToArrival <= 0 && (double) this.SecondInDayOrArrival > (double) Z_GameFlags.GetClosingTime())
      {
        int maxValue = (int) ((double) Z_GameFlags.GetClosingTime() - (double) Z_GameFlags.ZooOpenTime_InSeconds) / 2;
        this.SecondInDayOrArrival = (int) Z_GameFlags.ZooOpenTime_InSeconds + TinyZoo.Game1.Rnd.Next(0, maxValue);
      }
      return this.DaysToArrival <= 0;
    }

    public AnimalOrder(Reader reader, int VersionForLoad)
    {
      int num1 = (int) reader.ReadInt("s", ref this.PrisonUID);
      int num2 = (int) reader.ReadInt("s", ref this.DaysToArrival);
      int num3 = (int) reader.ReadInt("s", ref this.SecondInDayOrArrival);
      int num4 = (int) reader.ReadInt("s", ref this.SecondInDayOfPurchase);
      int _out = 0;
      int num5 = (int) reader.ReadInt("s", ref _out);
      this.incominganaimals = new List<IntakePerson>();
      for (int index = 0; index < _out; ++index)
        this.incominganaimals.Add(new IntakePerson(reader));
      if (VersionForLoad <= 30)
        return;
      int num6 = (int) reader.ReadInt("s", ref this.OrderUID);
    }

    public void SaveAnimalOrder(Writer writer)
    {
      writer.WriteInt("s", this.PrisonUID);
      writer.WriteInt("s", this.DaysToArrival);
      writer.WriteInt("s", this.SecondInDayOrArrival);
      writer.WriteInt("s", this.SecondInDayOfPurchase);
      writer.WriteInt("s", this.incominganaimals.Count);
      for (int index = 0; index < this.incominganaimals.Count; ++index)
        this.incominganaimals[index].SaveIntakePerson(writer);
      writer.WriteInt("s", this.OrderUID);
    }
  }
}
