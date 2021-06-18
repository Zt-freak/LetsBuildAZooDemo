// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Time.RealTime_TimeToResult
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;

namespace TinyZoo.PlayerDir.Time
{
  internal class RealTime_TimeToResult
  {
    public bool ThingClaimed;
    private Decimal TimeInReminingInSecondsForResearch;
    private Decimal OriginalTime;
    public string TimeLeftString;

    public RealTime_TimeToResult(int TimeInMinutes)
    {
      this.TimeInReminingInSecondsForResearch = (Decimal) TimeInMinutes * 60M;
      this.TimeInReminingInSecondsForResearch *= 0.25M;
      this.OriginalTime = this.TimeInReminingInSecondsForResearch;
    }

    public void RealTimeUpdateOnConsole(float DeltaTime, int TotalResearch)
    {
      this.TimeInReminingInSecondsForResearch -= (Decimal) DeltaTime * (Decimal) TotalResearch;
      if (!(this.TimeInReminingInSecondsForResearch < 0M))
        return;
      this.TimeInReminingInSecondsForResearch = 0M;
    }

    public float GetDateTimePercent(
      bool SetTimeLeftString,
      int TotalTimeNeededInMinutes,
      int TotalResearch)
    {
      double num = (double) (float) ((this.OriginalTime - this.TimeInReminingInSecondsForResearch) / this.OriginalTime);
      if (!SetTimeLeftString)
        return (float) num;
      if (this.TimeInReminingInSecondsForResearch > 0M)
      {
        this.TimeLeftString = TimeSpan.FromSeconds((double) (Decimal) (long) (this.TimeInReminingInSecondsForResearch / (Decimal) TotalResearch)).ToString("hh\\:mm\\:ss");
        return (float) num;
      }
      this.TimeLeftString = SEngine.Localization.Localization.GetText(71);
      return (float) num;
    }

    public void ClaimReward() => this.ThingClaimed = true;

    public bool IsComplete() => this.TimeInReminingInSecondsForResearch <= 0M;

    public void SaveRealTime_TimeToResult(Writer writer)
    {
      writer.WriteDecimal("i", this.TimeInReminingInSecondsForResearch);
      writer.WriteDecimal("i", this.OriginalTime);
    }

    public RealTime_TimeToResult(Reader reader)
    {
      int num1 = (int) reader.ReadDecimal("i", ref this.TimeInReminingInSecondsForResearch);
      int num2 = (int) reader.ReadDecimal("i", ref this.OriginalTime);
    }
  }
}
