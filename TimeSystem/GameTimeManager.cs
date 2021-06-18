// Decompiled with JetBrains decompiler
// Type: TinyZoo.TimeSystem.GameTimeManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;

namespace TinyZoo.TimeSystem
{
  internal class GameTimeManager
  {
    private float TimePastToday;
    private float HoursPassed;
    private long totaldays;
    private int DaysInAYear = 100;
    private int HoursInADay = 24;
    private int MinutesInAnHour = 60;
    private float LengthOfAnHour;
    public int HoursNotCollected;

    public GameTimeManager() => this.LengthOfAnHour = Z_GameFlags.SecondsInDay / (float) this.HoursInADay;

    public void UpdateGameTimeManager(float DeltaTime)
    {
      Console.WriteLine("TIME - Depricated");
      this.TimePastToday += DeltaTime;
      this.HoursPassed += DeltaTime;
      if ((double) this.LengthOfAnHour == 0.0)
        this.LengthOfAnHour = Z_GameFlags.SecondsInDay / (float) this.HoursInADay;
      while ((double) this.HoursPassed > (double) this.LengthOfAnHour)
      {
        this.HoursPassed -= this.LengthOfAnHour;
        ++this.HoursNotCollected;
      }
      while ((double) this.TimePastToday > (double) Z_GameFlags.SecondsInDay)
      {
        this.TimePastToday -= Z_GameFlags.SecondsInDay;
        ++this.totaldays;
      }
    }

    public string GetDayDisplay()
    {
      Console.WriteLine("TIME - Depricated");
      this.GetYearsPast();
      this.GetDaysPastThisYEar();
      return this.totaldays.ToString() + "days, " + (object) this.GetHousePastToday() + "h";
    }

    public string GetTimeOfDay()
    {
      Console.WriteLine("TIME - Depricated");
      return this.GetHousePastToday().ToString() + ":" + (object) this.GetMinutesPastTheHour();
    }

    public float GetPercentThroughDay()
    {
      Console.WriteLine("TIME - Depricated");
      return (double) this.TimePastToday < (double) Z_GameFlags.SecondsInDay ? this.TimePastToday / Z_GameFlags.SecondsInDay : 1f;
    }

    private int GetHousePastToday()
    {
      Console.WriteLine("TIME - Depricated");
      return (int) ((double) this.TimePastToday / (double) Z_GameFlags.SecondsInDay * (double) this.HoursInADay);
    }

    private int GetMinutesPastTheHour()
    {
      Console.WriteLine("TIME - Depricated");
      float num = Z_GameFlags.SecondsInDay / (float) this.HoursInADay;
      return (int) (((double) this.TimePastToday - (double) this.GetHousePastToday() * (double) num) / (double) num * 60.0);
    }

    private int GetYearsPast()
    {
      Console.WriteLine("TIME - Depricated");
      return (int) this.totaldays / this.DaysInAYear;
    }

    private int GetDaysPastThisYEar()
    {
      Console.WriteLine("TIME - Depricated");
      return (int) this.totaldays % this.DaysInAYear;
    }

    public long GetTotalDaysPassed()
    {
      Console.WriteLine("TIME - Depricated");
      return this.totaldays;
    }

    public GameTimeManager(Reader reader)
    {
      Console.WriteLine("TIME - Depricated");
      int num1 = (int) reader.ReadLong("p", ref this.totaldays);
      Decimal _out = 0M;
      int num2 = (int) reader.ReadDecimal("p", ref _out);
      Console.WriteLine("REMOVE THIS FROM SAVE");
    }

    public void SaveGameTime(Writer writer)
    {
      Console.WriteLine("TIME - Depricated");
      writer.WriteLong("p", this.totaldays);
      Decimal _x = 0M;
      writer.WriteDecimal("p", _x);
      Console.WriteLine("REMOVE THIS FROM SAVE");
    }
  }
}
