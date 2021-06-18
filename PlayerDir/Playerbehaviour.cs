// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Playerbehaviour
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;

namespace TinyZoo.PlayerDir
{
  internal class Playerbehaviour
  {
    private DateTime DayOfLastCheckIn;
    private uint TotalUniqueDaysAppLaunchedOn;
    private uint TotalTimesAppLaunched;
    private DateTime TimeOfLastSave;
    private TimeSpan TimePlayed;

    public Playerbehaviour()
    {
      this.DayOfLastCheckIn = DateTime.UtcNow;
      this.TimeOfLastSave = DateTime.UtcNow;
      this.TotalUniqueDaysAppLaunchedOn = 1U;
      this.TimePlayed = new TimeSpan();
    }

    public string[] GetTotalUniqueDaysAppLaunchedOn() => new string[2]
    {
      "TotalDaysLaunched",
      string.Concat((object) this.TotalUniqueDaysAppLaunchedOn)
    };

    public int GetTotalUniqueDaysAppLaunchedOnAsInt() => (int) this.TotalUniqueDaysAppLaunchedOn;

    public double GetTotalMinutesPlayed() => this.TimePlayed.TotalMinutes;

    public string[] GetTotalApplicationStartUps(bool Increment)
    {
      string[] strArray = new string[2]
      {
        "TotalTimesLaunched",
        string.Concat((object) this.TotalTimesAppLaunched)
      };
      if (!Increment)
        return strArray;
      ++this.TotalTimesAppLaunched;
      return strArray;
    }

    public string GetTimePlayedAsStringForCloudSave()
    {
      this.TimePlayed = new TimeSpan(this.TimePlayed.Ticks + (DateTime.UtcNow.Ticks - this.TimeOfLastSave.Ticks));
      this.TimeOfLastSave = DateTime.UtcNow;
      int num1 = (int) Math.Floor(this.TimePlayed.TotalMinutes);
      int num2 = num1 / 60;
      return num2.ToString() + " Hours " + (object) (num1 - num2 * 60) + " Minutes";
    }

    public string[] GetTimePlayedAsString()
    {
      this.TimePlayed = new TimeSpan(this.TimePlayed.Ticks + (DateTime.UtcNow.Ticks - this.TimeOfLastSave.Ticks));
      this.TimeOfLastSave = DateTime.UtcNow;
      return new string[2]
      {
        "TimePlayed01",
        Math.Floor(this.TimePlayed.TotalMinutes).ToString() + "." + (object) this.TimePlayed.Seconds
      };
    }

    public void SavePlayerbehaviour(Writer writer)
    {
      writer.WriteLong("l", this.DayOfLastCheckIn.Ticks);
      writer.WriteUInt("l", this.TotalUniqueDaysAppLaunchedOn);
      this.TimePlayed = new TimeSpan(this.TimePlayed.Ticks + (DateTime.UtcNow.Ticks - this.TimeOfLastSave.Ticks));
      this.TimeOfLastSave = DateTime.UtcNow;
      writer.WriteLong("l", this.TimePlayed.Ticks);
      writer.WriteUInt("l", this.TotalTimesAppLaunched);
    }

    public Playerbehaviour(Reader reader)
    {
      long _out = 0;
      int num1 = (int) reader.ReadLong("l", ref _out);
      this.DayOfLastCheckIn = new DateTime(_out);
      int num2 = (int) reader.ReadUInt("l", ref this.TotalUniqueDaysAppLaunchedOn);
      if (this.DayOfLastCheckIn.DayOfYear != DateTime.UtcNow.DayOfYear)
      {
        ++this.TotalUniqueDaysAppLaunchedOn;
        this.DayOfLastCheckIn = DateTime.UtcNow;
      }
      int num3 = (int) reader.ReadLong("l", ref _out);
      this.TimePlayed = new TimeSpan(_out);
      this.TimeOfLastSave = DateTime.UtcNow;
      int num4 = (int) reader.ReadUInt("l", ref this.TotalTimesAppLaunched);
    }
  }
}
