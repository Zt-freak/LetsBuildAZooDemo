// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.QuickPick.EmployeeHistory
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using TinyZoo.Z_SummaryPopUps.Generic;

namespace TinyZoo.Z_Employees.QuickPick
{
  internal class EmployeeHistory
  {
    public int Day;
    public float PercentServing;
    public float PercentOnBreak;
    public float PercentAtWork;
    public float ShopOpenTime;
    public int CustomersServed;

    public EmployeeHistory(int _Day) => this.Day = _Day;

    private void AddStates(ref Decimal[] totals)
    {
      totals[1] += (Decimal) this.CustomersServed;
      totals[2] += (Decimal) this.PercentServing;
      totals[3] += (Decimal) this.PercentOnBreak;
      totals[4] += (Decimal) this.PercentAtWork;
      totals[0] += (Decimal) this.ShopOpenTime;
    }

    public bool Compares(EmployeeHistory A) => A.Day == this.Day && A.CustomersServed == this.CustomersServed && ((double) A.ShopOpenTime == (double) this.ShopOpenTime && (double) A.PercentOnBreak == (double) this.PercentOnBreak) && (double) A.PercentServing == (double) this.PercentServing;

    internal static float[] GetStates(
      QuickEmployeeDescription employeedec,
      TimeSegmentType SegmentType,
      int DayToday,
      out bool NoData)
    {
      NoData = false;
      int num1 = -1;
      int num2 = 0;
      Decimal[] totals = new Decimal[5];
      float[] numArray = new float[5];
      if (SegmentType == TimeSegmentType.Today)
      {
        int num3 = num2 + 1;
        numArray[1] = (float) employeedec.CustomersServed;
        if ((double) Z_GameFlags.DayTimer > 0.0)
        {
          numArray[3] = employeedec.BreakTime / Z_GameFlags.DayTimer;
          numArray[2] = employeedec.TimeSpentServing / Z_GameFlags.DayTimer;
          numArray[4] = employeedec.TimeAtWork_IncludingBreaks / Z_GameFlags.DayTimer;
        }
        numArray[0] = employeedec.TimeAtWork_IncludingBreaks / Z_GameFlags.SecondsInDay;
        return numArray;
      }
      for (int index = employeedec.employeehistory.Count - 1; index > -1; --index)
      {
        if (SegmentType == TimeSegmentType.SinceLastChange)
        {
          if (num1 != employeedec.employeehistory[index].Day)
          {
            num1 = employeedec.employeehistory[index].Day;
            ++num2;
          }
          else if (employeedec.employeehistory[index].Compares(employeedec.employeehistory[employeedec.employeehistory.Count - 1]))
            employeedec.employeehistory[index].AddStates(ref totals);
          else
            break;
        }
        else if ((SegmentType == TimeSegmentType.AllTime || SegmentType == TimeSegmentType.Today) && employeedec.employeehistory[index].Day == DayToday)
        {
          if (num1 != employeedec.employeehistory[index].Day)
          {
            num1 = employeedec.employeehistory[index].Day;
            ++num2;
          }
          employeedec.employeehistory[index].AddStates(ref totals);
        }
        else if (SegmentType != TimeSegmentType.Today)
        {
          if ((SegmentType == TimeSegmentType.AllTime || SegmentType == TimeSegmentType.Yesterday || SegmentType == TimeSegmentType.Last7Days) && employeedec.employeehistory[index].Day == DayToday - 1)
          {
            if (num1 != employeedec.employeehistory[index].Day)
            {
              num1 = employeedec.employeehistory[index].Day;
              ++num2;
            }
            employeedec.employeehistory[index].AddStates(ref totals);
          }
          else if (SegmentType != TimeSegmentType.Yesterday || employeedec.employeehistory[index].Day >= DayToday - 1)
          {
            if ((SegmentType == TimeSegmentType.AllTime || SegmentType == TimeSegmentType.Last7Days) && employeedec.employeehistory[index].Day == DayToday - 7)
            {
              if (num1 != employeedec.employeehistory[index].Day)
              {
                num1 = employeedec.employeehistory[index].Day;
                ++num2;
              }
              employeedec.employeehistory[index].AddStates(ref totals);
            }
            else if (SegmentType != TimeSegmentType.Last7Days || employeedec.employeehistory[index].Day >= DayToday - 7)
            {
              if (SegmentType == TimeSegmentType.AllTime)
              {
                if (num1 != employeedec.employeehistory[index].Day)
                {
                  num1 = employeedec.employeehistory[index].Day;
                  ++num2;
                }
                employeedec.employeehistory[index].AddStates(ref totals);
              }
            }
            else
              break;
          }
          else
            break;
        }
        else
          break;
      }
      for (int index = 0; index < numArray.Length; ++index)
      {
        if (index == 1)
          numArray[index] = (float) totals[index];
        else if (num2 > 0)
          numArray[index] = (float) (totals[index] / (Decimal) num2);
        else
          NoData = true;
      }
      numArray[0] = numArray[4];
      return numArray;
    }

    public EmployeeHistory(Reader reader)
    {
      int num1 = (int) reader.ReadInt("h", ref this.Day);
      int num2 = (int) reader.ReadFloat("h", ref this.PercentServing);
      int num3 = (int) reader.ReadFloat("h", ref this.PercentOnBreak);
      int num4 = (int) reader.ReadFloat("h", ref this.PercentAtWork);
      int num5 = (int) reader.ReadFloat("h", ref this.ShopOpenTime);
      int num6 = (int) reader.ReadInt("h", ref this.CustomersServed);
    }

    public void SaveEmployeeHistory(Writer writer)
    {
      writer.WriteInt("h", this.Day);
      writer.WriteFloat("h", this.PercentServing);
      writer.WriteFloat("h", this.PercentOnBreak);
      writer.WriteFloat("h", this.PercentAtWork);
      writer.WriteFloat("h", this.ShopOpenTime);
      writer.WriteInt("h", this.CustomersServed);
    }
  }
}
