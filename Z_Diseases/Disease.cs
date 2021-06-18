// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Diseases.Disease
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SpringSocial.NameGenerator;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Diseases;

namespace TinyZoo.Z_Diseases
{
  internal class Disease
  {
    public int UID;
    public List<AnimalType> CanInfectThese;
    public int ProbabilityOfInfection;
    public int IncubationPeriodMin_QuarterDays;
    public int IncubationPeriodMax;
    public int TimeCanSurviveInQuarterDays;
    public string Name;
    public int MortallityRate;
    public int DaysToDeathMinInQuarterDays;
    public int DaysToDeathMax;
    public int DaysToHealMinQuarterDay;
    public int DaysToHealMax;
    public Vector2Int StartLoction;
    public int RollRegularityPerAnimal;
    private bool Airbourne;
    private int AirborneRange;
    public DiseaseResearch diseaseresearch;
    public int KnownInfections;
    public int Deaths;
    public int SuspectedDayOfFirstOutbreak;
    public int TotalOutbreaks;
    public bool IsResearched;

    public Disease()
    {
      this.IsResearched = false;
      this.CanInfectThese = new List<AnimalType>();
      this.diseaseresearch = new DiseaseResearch();
    }

    public void MakeWaterDisease(int DirtValue) => this.DoBase(DirtValue);

    public void StartNewDay(Player player)
    {
      if (this.IsResearched)
        return;
      this.diseaseresearch.StartNewDay(player);
    }

    public void MakeCorpseDisease(int DirtValue) => this.DoBase(DirtValue);

    public void MakePoopDisease(int DirtValue) => this.DoBase(DirtValue);

    private void DoBase(int DirtValue)
    {
      this.ProbabilityOfInfection = TinyZoo.Game1.Rnd.Next(0, DirtValue + 5);
      int maxValue = DirtValue / 10;
      if (maxValue < 3)
        maxValue = 3;
      this.IncubationPeriodMin_QuarterDays = TinyZoo.Game1.Rnd.Next(2, maxValue);
      this.IncubationPeriodMax = TinyZoo.Game1.Rnd.Next(this.IncubationPeriodMin_QuarterDays, this.IncubationPeriodMin_QuarterDays * 2);
      this.IncubationPeriodMin_QuarterDays *= 4;
      this.IncubationPeriodMax *= 4;
      this.TimeCanSurviveInQuarterDays = TinyZoo.Game1.Rnd.Next(2, Math.Max(DirtValue / 2, 3));
      this.MortallityRate = TinyZoo.Game1.Rnd.Next(0, DirtValue);
      this.RollRegularityPerAnimal = TinyZoo.Game1.Rnd.Next(1, 16);
      if (100 - DirtValue > 2)
      {
        this.DaysToDeathMinInQuarterDays = TinyZoo.Game1.Rnd.Next(2, 100 - DirtValue);
        this.DaysToDeathMax = TinyZoo.Game1.Rnd.Next(this.DaysToDeathMinInQuarterDays, 100 - DirtValue + 1);
        if (this.DaysToDeathMax > this.DaysToDeathMinInQuarterDays)
          this.DaysToDeathMax = this.DaysToDeathMinInQuarterDays + 1;
      }
      else
      {
        this.DaysToDeathMinInQuarterDays = 20;
        this.DaysToDeathMax = 30;
      }
      if (100 - DirtValue > 4)
      {
        this.DaysToHealMinQuarterDay = TinyZoo.Game1.Rnd.Next(4, 100 - DirtValue);
        this.DaysToHealMax = TinyZoo.Game1.Rnd.Next(this.DaysToHealMinQuarterDay, 100 - DirtValue + 1);
      }
      else
      {
        this.DaysToHealMinQuarterDay = 4;
        this.DaysToHealMax = 12;
      }
      this.Name = "Virus " + (object) TinyZoo.Game1.Rnd.Next(0, 10000);
      switch (TinyZoo.Game1.Rnd.Next(0, 5))
      {
        case 0:
          this.Name = NameGeneratorData.GetAdjective(TinyZoo.Game1.Rnd) + " Disease";
          break;
        case 1:
          this.Name = NameGeneratorData.GetAdjective(TinyZoo.Game1.Rnd) + " Worm";
          break;
        case 2:
          this.Name = NameGeneratorData.GetAdjective(TinyZoo.Game1.Rnd) + " Peritonitis";
          break;
        case 3:
          this.Name = NameGeneratorData.GetAdjective(TinyZoo.Game1.Rnd) + " Virus";
          break;
        case 4:
          this.Name = NameGeneratorData.GetAdjective(TinyZoo.Game1.Rnd) + "pox";
          break;
      }
    }

    public string GetIncubationPeriodString()
    {
      float periodMinQuarterDays = (float) this.IncubationPeriodMin_QuarterDays;
      return Math.Round((double) ((float) (((double) this.IncubationPeriodMax - (double) periodMinQuarterDays) * 0.5) + periodMinQuarterDays), 1).ToString() + " Days";
    }

    public string GetSterilizationTimeString()
    {
      float surviveInQuarterDays = (float) this.TimeCanSurviveInQuarterDays;
      return Math.Round((double) ((float) (((double) (this.TimeCanSurviveInQuarterDays + 2) - (double) surviveInQuarterDays) * 0.5) + surviveInQuarterDays), 1).ToString() + " Days";
    }

    public string GetRecoveryTimeString()
    {
      float healMinQuarterDay = (float) this.DaysToHealMinQuarterDay;
      return Math.Round((double) ((float) (((double) this.DaysToHealMax - (double) healMinQuarterDay) * 0.5) + healMinQuarterDay), 1).ToString() + " Days";
    }

    public string GetRangeString() => this.Airbourne ? "Airborne, range " + (object) (this.AirborneRange * 5) + " meters" : "Direct Contact Disease";

    public void SaveDisease(Writer writer)
    {
      writer.WriteInt("d", this.UID);
      writer.WriteInt("d", this.ProbabilityOfInfection);
      writer.WriteInt("d", this.IncubationPeriodMin_QuarterDays);
      writer.WriteInt("d", this.IncubationPeriodMax);
      writer.WriteInt("d", this.TimeCanSurviveInQuarterDays);
      if (this.Name == null || this.Name.Length == 0)
      {
        this.Name = "Unknown";
        throw new Exception("khsdf");
      }
      writer.WriteString("d", this.Name);
      writer.WriteInt("d", this.MortallityRate);
      writer.WriteInt("d", this.DaysToDeathMinInQuarterDays);
      writer.WriteInt("d", this.DaysToDeathMax);
      writer.WriteInt("d", this.DaysToHealMinQuarterDay);
      writer.WriteInt("d", this.DaysToHealMax);
      this.StartLoction.SaveVector2Int(writer);
      writer.WriteInt("d", this.RollRegularityPerAnimal);
      writer.WriteInt("d", this.CanInfectThese.Count);
      for (int index = 0; index < this.CanInfectThese.Count; ++index)
        writer.WriteInt("d", (int) this.CanInfectThese[index]);
      writer.WriteBool("d", this.IsResearched);
      if (!this.IsResearched)
        this.diseaseresearch.SaveDiseaseResearch(writer);
      writer.WriteInt("d", this.KnownInfections);
      writer.WriteInt("d", this.Deaths);
      writer.WriteInt("d", this.SuspectedDayOfFirstOutbreak);
      writer.WriteInt("d", this.TotalOutbreaks);
    }

    public Disease(Reader reader, int VersionForLoad)
    {
      int num1 = (int) reader.ReadInt("d", ref this.UID);
      int num2 = (int) reader.ReadInt("d", ref this.ProbabilityOfInfection);
      int num3 = (int) reader.ReadInt("d", ref this.IncubationPeriodMin_QuarterDays);
      int num4 = (int) reader.ReadInt("d", ref this.IncubationPeriodMax);
      int num5 = (int) reader.ReadInt("d", ref this.TimeCanSurviveInQuarterDays);
      this.Name = "";
      if (VersionForLoad > 19)
      {
        int num6 = (int) reader.ReadString("d", ref this.Name);
      }
      int num7 = (int) reader.ReadInt("d", ref this.MortallityRate);
      int num8 = (int) reader.ReadInt("d", ref this.DaysToDeathMinInQuarterDays);
      int num9 = (int) reader.ReadInt("d", ref this.DaysToDeathMax);
      int num10 = (int) reader.ReadInt("d", ref this.DaysToHealMinQuarterDay);
      int num11 = (int) reader.ReadInt("d", ref this.DaysToHealMax);
      this.StartLoction = new Vector2Int(reader);
      int num12 = (int) reader.ReadInt("d", ref this.RollRegularityPerAnimal);
      int _out1 = 0;
      int num13 = (int) reader.ReadInt("d", ref _out1);
      this.CanInfectThese = new List<AnimalType>();
      for (int index = 0; index < _out1; ++index)
      {
        int _out2 = 0;
        int num14 = (int) reader.ReadInt("d", ref _out2);
        this.CanInfectThese.Add((AnimalType) _out2);
      }
      if (VersionForLoad <= 4)
        return;
      int num15 = (int) reader.ReadBool("d", ref this.IsResearched);
      if (!this.IsResearched)
        this.diseaseresearch = new DiseaseResearch(reader, VersionForLoad);
      int num16 = (int) reader.ReadInt("d", ref this.KnownInfections);
      int num17 = (int) reader.ReadInt("d", ref this.Deaths);
      int num18 = (int) reader.ReadInt("d", ref this.SuspectedDayOfFirstOutbreak);
      int num19 = (int) reader.ReadInt("d", ref this.TotalOutbreaks);
    }
  }
}
