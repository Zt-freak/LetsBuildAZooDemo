// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Time.PayoutTimer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Time;
using System;

namespace TinyZoo.PlayerDir.Time
{
  internal class PayoutTimer
  {
    private DateTime lastpayout;
    private DateTime lastUNvallidatedpayout;
    private DateTime LastPayoutAtSaveTime;
    private DateTime FirstPayoutAfterSaveMinusPaymentCycle;
    public bool HasGivenTimeAwayPayout;
    private long Debt;
    public int earningsperday_OnLoad;

    public PayoutTimer()
    {
      this.HasGivenTimeAwayPayout = true;
      this.lastpayout = DateTime.UtcNow;
      this.lastUNvallidatedpayout = DateTime.UtcNow;
    }

    public void ForceDoPayout_DeltaTime(
      DateTimeManager datetimemanager,
      TimeSpan lengthperpaycycle,
      out int ThisManyFakeCyclesCaptured)
    {
      ThisManyFakeCyclesCaptured = 0;
      bool WasServerTime;
      DateTime dateTimeNow = datetimemanager.GetDateTimeNow(out WasServerTime);
      if (WasServerTime)
      {
        this.lastpayout = new DateTime(dateTimeNow.Ticks);
        if (this.lastpayout.Ticks < this.lastUNvallidatedpayout.Ticks)
        {
          long num = this.lastUNvallidatedpayout.Ticks - this.lastpayout.Ticks;
          ThisManyFakeCyclesCaptured = (int) ((Decimal) num / (Decimal) lengthperpaycycle.Ticks);
        }
        this.lastUNvallidatedpayout = new DateTime(dateTimeNow.Ticks);
      }
      else
        this.lastUNvallidatedpayout = new DateTime(dateTimeNow.Ticks);
      if (this.FirstPayoutAfterSaveMinusPaymentCycle.Year >= 50)
        return;
      this.FirstPayoutAfterSaveMinusPaymentCycle = new DateTime(dateTimeNow.Ticks - lengthperpaycycle.Ticks);
    }

    public long GetPayoutSinceSave(DateTimeManager datetimemanager, TimeSpan timeperpayout)
    {
      if (!this.HasGivenTimeAwayPayout)
      {
        bool WasServerTime;
        DateTime dateTimeNow = datetimemanager.GetDateTimeNow(out WasServerTime);
        if (WasServerTime)
        {
          long num1 = 0;
          this.HasGivenTimeAwayPayout = true;
          this.lastpayout = new DateTime(dateTimeNow.Ticks);
          this.lastUNvallidatedpayout = new DateTime(dateTimeNow.Ticks);
          if (this.FirstPayoutAfterSaveMinusPaymentCycle.Year > 50)
          {
            if (this.FirstPayoutAfterSaveMinusPaymentCycle.Ticks <= dateTimeNow.Ticks)
            {
              long num2 = this.FirstPayoutAfterSaveMinusPaymentCycle.Ticks - this.LastPayoutAtSaveTime.Ticks;
              if (num2 > 0L)
                num1 = (long) (int) ((Decimal) num2 / (Decimal) timeperpayout.Ticks);
            }
            else
            {
              long num2 = dateTimeNow.Ticks - this.LastPayoutAtSaveTime.Ticks;
              if (num2 > 0L)
                num1 = (long) (int) ((Decimal) num2 / (Decimal) timeperpayout.Ticks);
              long num3 = (long) (int) ((Decimal) (this.FirstPayoutAfterSaveMinusPaymentCycle.Ticks - dateTimeNow.Ticks) / (Decimal) timeperpayout.Ticks);
              num1 -= num3;
              if (num1 < 0L)
              {
                this.Debt += -num1;
                num1 = 0L;
              }
            }
          }
          else
          {
            long num2 = dateTimeNow.Ticks - this.LastPayoutAtSaveTime.Ticks;
            if (num2 > 0L)
              num1 = (long) (int) ((Decimal) num2 / (Decimal) timeperpayout.Ticks);
          }
          if (this.Debt > 0L)
          {
            if (num1 >= this.Debt)
            {
              num1 -= this.Debt;
              this.Debt = 0L;
            }
            else
            {
              this.Debt -= num1;
              num1 = 0L;
            }
          }
          return num1;
        }
      }
      return 0;
    }

    public void SavePayoutTime(Writer writer)
    {
      writer.WriteDateTime("t", this.lastpayout);
      writer.WriteDateTime("t", this.lastUNvallidatedpayout);
      this.LastPayoutAtSaveTime = this.lastpayout.Ticks <= this.lastUNvallidatedpayout.Ticks ? new DateTime(this.lastUNvallidatedpayout.Ticks) : new DateTime(this.lastpayout.Ticks);
      writer.WriteDateTime("t", this.lastpayout);
      writer.WriteLong("t", this.Debt);
    }

    public PayoutTimer(Reader reader)
    {
      this.HasGivenTimeAwayPayout = false;
      this.lastpayout = reader.ReadDateTime("t");
      this.lastUNvallidatedpayout = reader.ReadDateTime("t");
      this.LastPayoutAtSaveTime = reader.ReadDateTime("t");
      int num = (int) reader.ReadLong("t", ref this.Debt);
      this.FirstPayoutAfterSaveMinusPaymentCycle = new DateTime();
    }
  }
}
