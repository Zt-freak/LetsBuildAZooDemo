// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ZooValues.EmployeeStats
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;

namespace TinyZoo.Z_ZooValues
{
  internal class EmployeeStats
  {
    public int MinimumWage;
    public int MaximumWage;
    public int Salary;
    public int Seniority;
    public string JobDescription;
    public int Determination;
    public int Greed;

    public EmployeeStats()
    {
      this.Greed = TinyZoo.Game1.Rnd.Next(0, 100);
      this.Determination = TinyZoo.Game1.Rnd.Next(30, 100);
    }

    public int GetMarketAverage() => this.MinimumWage + (this.MaximumWage - this.MinimumWage) / 2;

    public int GetOfferToGreedLevel(int FinancialOffer)
    {
      FinancialOffer -= this.MinimumWage;
      int num = this.MaximumWage - this.MinimumWage;
      return (int) ((double) FinancialOffer / (double) num * 5.0);
    }

    public int GetGreedAnswerIndex()
    {
      int num = this.Greed / 20;
      if (num > 4)
        num = 4;
      return num;
    }

    public EmployeeStats(Reader reader)
    {
      this.JobDescription = "NA";
      int num1 = (int) reader.ReadInt("p", ref this.MinimumWage);
      int num2 = (int) reader.ReadInt("p", ref this.MaximumWage);
      int num3 = (int) reader.ReadInt("p", ref this.Salary);
      int num4 = (int) reader.ReadInt("p", ref this.Seniority);
    }

    public void SaveEmployeeStats(Writer writer)
    {
      writer.WriteInt("p", this.MinimumWage);
      writer.WriteInt("p", this.MaximumWage);
      writer.WriteInt("p", this.Salary);
      writer.WriteInt("p", this.Seniority);
    }
  }
}
