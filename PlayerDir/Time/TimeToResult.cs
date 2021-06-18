// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Time.TimeToResult
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Objects;
using SEngine.Time;
using System;
using System.Collections.Generic;

namespace TinyZoo.PlayerDir.Time
{
  internal class TimeToResult
  {
    public DateTime UnmodifiedLengthofEvent;
    private DateTime RemainingTime;
    private DateTime LastStartTime;
    private NumberObfiscatorV1 MultiplierSpeed;
    public int MuliplierSpeedPublic;
    private List<UnvallidatedMultiplierEvents> unvallidatedevents;
    private Decimal PercerntLeftAtLastTimeSet;
    public bool ThingClaimed;
    public string TimeLeftString;

    public TimeToResult(DateTime _StartTime, DateTime _LengthOfTimeToObjective, int TotalUnits = 1)
    {
      this.PercerntLeftAtLastTimeSet = 1M;
      this.MultiplierSpeed = new NumberObfiscatorV1();
      this.MultiplierSpeed.ForceSetNewValue(TotalUnits);
      this.unvallidatedevents = new List<UnvallidatedMultiplierEvents>();
      this.LastStartTime = new DateTime(_StartTime.Ticks);
      this.UnmodifiedLengthofEvent = new DateTime(_LengthOfTimeToObjective.Ticks);
      this.RemainingTime = TotalUnits != 0 ? new DateTime(_LengthOfTimeToObjective.Ticks / (long) TotalUnits) : new DateTime(_LengthOfTimeToObjective.Ticks);
      this.MuliplierSpeedPublic = TotalUnits;
      this.ThingClaimed = false;
    }

    public void ForceReduceTimeRemaining(TimeSpan ReduceTime)
    {
      this.RemainingTime.ToShortDateString();
      if (this.RemainingTime.Ticks > ReduceTime.Ticks)
        this.RemainingTime = new DateTime(this.RemainingTime.Ticks - ReduceTime.Ticks);
      else
        this.RemainingTime = new DateTime(0L);
    }

    public void Multiply(Decimal Perc) => this.RemainingTime = new DateTime((long) ((Decimal) this.RemainingTime.Ticks * Perc));

    public void ClaimReward() => this.ThingClaimed = true;

    public void AddorRemoveUnit(DateTimeManager datetime, int AddThisMany)
    {
      bool WasServerTime;
      DateTime dateTimeNow = datetime.GetDateTimeNow(out WasServerTime);
      if (WasServerTime)
      {
        if (this.unvallidatedevents.Count > 0)
        {
          while (this.unvallidatedevents.Count > 0)
          {
            if (this.unvallidatedevents[0].localtimeofevent.Ticks <= dateTimeNow.Ticks && this.unvallidatedevents[0].localtimeofevent.Ticks >= this.LastStartTime.Ticks)
            {
              this.DoMultiplierEvent(this.unvallidatedevents[0].localtimeofevent, this.unvallidatedevents[0].TotalAddedOrRemoved.SmartGetValue(false));
              this.unvallidatedevents.RemoveAt(0);
            }
          }
        }
        this.DoMultiplierEvent(dateTimeNow, AddThisMany);
      }
      else
      {
        this.unvallidatedevents.Add(new UnvallidatedMultiplierEvents(AddThisMany));
        this.MuliplierSpeedPublic += AddThisMany;
      }
    }

    private void DoMultiplierEvent(DateTime now, int AddThisMany)
    {
      long num1 = now.Ticks - this.LastStartTime.Ticks;
      if (num1 >= 0L)
      {
        this.LastStartTime = new DateTime(now.Ticks);
        long ticks1 = this.RemainingTime.Ticks;
        long num2 = ticks1;
        long ticks2 = ticks1 - num1;
        this.PercerntLeftAtLastTimeSet -= (1M - (Decimal) ticks2 / (Decimal) num2) * this.PercerntLeftAtLastTimeSet;
        long num3 = (long) this.MultiplierSpeed.SmartGetValue(false);
        this.MuliplierSpeedPublic = this.MultiplierSpeed.GetUnvallidatedValue();
        this.MultiplierSpeed.SmartAddValue_MinimumThreshold(false, AddThisMany, 0);
        this.MuliplierSpeedPublic += AddThisMany;
        if (this.MuliplierSpeedPublic < 0)
          throw new Exception("TIME TRAVEL BACKWARDS");
        if (ticks2 > 0L)
        {
          if (num3 > 1L)
            ticks2 *= num3;
          if (this.MuliplierSpeedPublic > 1)
            ticks2 = (long) ((Decimal) ticks2 / (Decimal) this.MuliplierSpeedPublic);
          this.RemainingTime = new DateTime(ticks2);
        }
        else
          this.RemainingTime = new DateTime(0L);
      }
      else
        this.RemainingTime = new DateTime(0L);
    }

    public int GetMultiplier()
    {
      this.MuliplierSpeedPublic = this.MultiplierSpeed.GetUnvallidatedValue();
      return this.MuliplierSpeedPublic;
    }

    public bool QuestGetIsComplete(DateTimeManager datetime) => this.RemainingTime.Ticks - (datetime.GetDateTimeNow(out bool _).Ticks - this.LastStartTime.Ticks) <= 0L;

    public bool IsComplete(DateTimeManager datetime, out bool ServerError, bool MustHaveServerTime = true)
    {
      ServerError = false;
      bool WasServerTime;
      DateTime dateTimeNow = datetime.GetDateTimeNow(out WasServerTime);
      if (!WasServerTime)
        datetime.SmartForceGetTimeFromServer();
      if (MustHaveServerTime && !WasServerTime)
      {
        ServerError = true;
        return false;
      }
      return this.RemainingTime.Ticks - (dateTimeNow.Ticks - this.LastStartTime.Ticks) <= 0L;
    }

    public float GetDateTimePercent(DateTimeManager datetime, bool SetTimeLeftString)
    {
      long num1 = datetime.GetDateTimeNow(out bool _).Ticks - this.LastStartTime.Ticks;
      if (num1 <= 0L)
        return 1f;
      long ticks = this.RemainingTime.Ticks - num1;
      float num2 = this.RemainingTime.Ticks <= 0L ? 0.0f : (float) ((Decimal) ticks / (Decimal) this.RemainingTime.Ticks);
      if ((double) num2 > 1.0)
        num2 = 1f;
      else if ((double) num2 < 0.0)
        num2 = 0.0f;
      float num3 = num2 * (float) this.PercerntLeftAtLastTimeSet;
      if (SetTimeLeftString)
        this.TimeLeftString = ticks <= 0L ? SEngine.Localization.Localization.GetText(71) : TimeUtils.GetTimeAsString(new DateTime(ticks).AddSeconds(1.0));
      return 1f - num3;
    }

    public void SaveTimeToResult(Writer writer)
    {
      writer.WriteDateTime("t", this.UnmodifiedLengthofEvent);
      writer.WriteDateTime("t", this.RemainingTime);
      writer.WriteDateTime("t", this.LastStartTime);
      writer.WriteDecimal("t", this.PercerntLeftAtLastTimeSet);
      this.MultiplierSpeed.SaveObfiscator(writer);
      writer.WriteBool("t", this.ThingClaimed);
      writer.WriteInt("t", this.unvallidatedevents.Count);
      for (int index = 0; index < this.unvallidatedevents.Count; ++index)
        this.unvallidatedevents[index].SaveUnvallidatedMultiplierEvents(writer);
    }

    public TimeToResult(Reader reader)
    {
      this.UnmodifiedLengthofEvent = reader.ReadDateTime("t");
      this.RemainingTime = reader.ReadDateTime("t");
      this.LastStartTime = reader.ReadDateTime("t");
      int num1 = (int) reader.ReadDecimal("t", ref this.PercerntLeftAtLastTimeSet);
      this.MultiplierSpeed = new NumberObfiscatorV1(reader);
      int num2 = (int) reader.ReadBool("t", ref this.ThingClaimed);
      int _out = 0;
      int num3 = (int) reader.ReadInt("t", ref _out);
      this.unvallidatedevents = new List<UnvallidatedMultiplierEvents>();
      for (int index = 0; index < _out; ++index)
        this.unvallidatedevents.Add(new UnvallidatedMultiplierEvents(reader));
    }
  }
}
