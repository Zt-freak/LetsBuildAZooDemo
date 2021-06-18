// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Diseases.DiseaseResearch
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.Z_BalanceSystems.Diseases;

namespace TinyZoo.PlayerDir.Diseases
{
  internal class DiseaseResearch
  {
    public bool[] Progress;
    public float TotalProgress;
    public int AnimalTypesProgress;
    public int SpendPerDay;
    public bool HasBeenDiscovered;
    private float TotalResearchDays;
    private int DaysSpendResearching;

    public DiseaseResearch()
    {
      this.SpendPerDay = DCost.MinSpend;
      this.Progress = new bool[7];
      this.TotalResearchDays = (float) TinyZoo.Game1.Rnd.Next(5, 15);
    }

    public void SetSpendPerDayFromPercentage(float Percent)
    {
      this.SpendPerDay = DCost.MinSpend;
      this.SpendPerDay += (int) ((double) (DCost.MaxSpend - DCost.MinSpend) * (double) Percent);
    }

    public float GetInvestmentPercentage() => (float) (this.SpendPerDay - DCost.MinSpend) / (float) (DCost.MaxSpend - DCost.MinSpend);

    public string GetDaysResearched() => string.Concat((object) this.DaysSpendResearching);

    public void StartNewDay(Player player)
    {
      if (!this.HasBeenDiscovered)
        return;
      ++this.DaysSpendResearching;
      if (player.Stats.GetCashHeld() < this.SpendPerDay)
        return;
      player.Stats.SpendCash(this.SpendPerDay, SpendingCashOnThis.DiseaseResearch, player);
      this.TotalProgress += (float) (1.0 + (double) (((float) this.SpendPerDay - (float) DCost.MinSpend) / ((float) DCost.MaxSpend - (float) DCost.MinSpend)) * 9.0) / this.TotalResearchDays;
      if ((double) this.TotalProgress > 1.0)
      {
        this.TotalProgress = 1f;
        for (int index = 0; index < this.Progress.Length; ++index)
          this.Progress[index] = true;
      }
      int num1 = 0;
      for (int index = 0; index < this.Progress.Length; ++index)
      {
        if (this.Progress[index])
          ++num1;
      }
      int num2 = (int) ((double) this.TotalProgress * 7.0);
      for (int index1 = 0; num2 > 0 && index1 < 10; ++index1)
      {
        int index2 = TinyZoo.Game1.Rnd.Next(1, 7);
        if (!this.Progress[index2])
        {
          this.Progress[index2] = true;
          --num2;
        }
      }
    }

    public DiseaseResearch(Reader reader, int VersionForLoad)
    {
      this.Progress = new bool[7];
      if (VersionForLoad <= 5)
        return;
      int _out = 0;
      int num1 = (int) reader.ReadInt("g", ref _out);
      for (int index = 0; index < _out; ++index)
      {
        int num2 = (int) reader.ReadBool("g", ref this.Progress[index]);
      }
      int num3 = (int) reader.ReadFloat("g", ref this.TotalProgress);
      int num4 = (int) reader.ReadInt("g", ref this.SpendPerDay);
      int num5 = (int) reader.ReadBool("g", ref this.HasBeenDiscovered);
      int num6 = (int) reader.ReadInt("g", ref this.AnimalTypesProgress);
      int num7 = (int) reader.ReadInt("g", ref this.DaysSpendResearching);
    }

    public void SaveDiseaseResearch(Writer writer)
    {
      writer.WriteInt("g", this.Progress.Length);
      for (int index = 0; index < this.Progress.Length; ++index)
        writer.WriteBool("g", this.Progress[index]);
      writer.WriteFloat("g", this.TotalProgress);
      writer.WriteInt("g", this.SpendPerDay);
      writer.WriteBool("g", this.HasBeenDiscovered);
      writer.WriteInt("g", this.AnimalTypesProgress);
      writer.WriteInt("g", this.DaysSpendResearching);
    }
  }
}
