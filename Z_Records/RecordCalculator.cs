// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Records.RecordCalculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_SummaryPopUps.Generic;

namespace TinyZoo.Z_Records
{
  internal class RecordCalculator
  {
    internal static float GetPercentageOfPeopleWhoPurchasedNormalTicket(
      TimeSegmentType timesegment,
      out bool NoData,
      out int TotalPeople,
      out int TotalPayingCustomers)
    {
      TotalPeople = 0;
      TotalPayingCustomers = 0;
      NoData = false;
      switch (timesegment)
      {
        case TimeSegmentType.Today:
          if (Player.financialrecords.daystats.Count > 0)
          {
            TotalPeople = Player.financialrecords.daystats[Player.financialrecords.daystats.Count - 1].PeopleWhoWantedToCome;
            if (TotalPeople == 0)
            {
              NoData = true;
              return 0.0f;
            }
            TotalPayingCustomers = Player.financialrecords.daystats[Player.financialrecords.daystats.Count - 1].PeopleWhoCame;
            return (float) Player.financialrecords.daystats[Player.financialrecords.daystats.Count - 1].PeopleWhoCame / (float) Player.financialrecords.daystats[Player.financialrecords.daystats.Count - 1].PeopleWhoWantedToCome;
          }
          NoData = true;
          return 0.0f;
        case TimeSegmentType.Yesterday:
          if (Player.financialrecords.daystats.Count > 1)
          {
            TotalPeople = Player.financialrecords.daystats[Player.financialrecords.daystats.Count - 2].PeopleWhoWantedToCome;
            if (TotalPeople == 0)
            {
              NoData = true;
              return 0.0f;
            }
            TotalPayingCustomers = Player.financialrecords.daystats[Player.financialrecords.daystats.Count - 2].PeopleWhoCame;
            return (float) Player.financialrecords.daystats[Player.financialrecords.daystats.Count - 2].PeopleWhoCame / (float) Player.financialrecords.daystats[Player.financialrecords.daystats.Count - 2].PeopleWhoWantedToCome;
          }
          NoData = true;
          return 0.0f;
        case TimeSegmentType.Last7Days:
        case TimeSegmentType.AllTime:
          int num = 0;
          for (int index = Player.financialrecords.daystats.Count - 1; index > -1; --index)
          {
            TotalPayingCustomers += Player.financialrecords.daystats[index].PeopleWhoCame;
            TotalPeople += Player.financialrecords.daystats[index].PeopleWhoWantedToCome;
            ++num;
            if (timesegment == TimeSegmentType.Last7Days && num >= 7)
            {
              if (TotalPeople > 0)
                return (float) TotalPayingCustomers / (float) TotalPeople;
              NoData = true;
              return 0.0f;
            }
          }
          if (TotalPeople > 0)
            return (float) TotalPayingCustomers / (float) TotalPeople;
          NoData = true;
          return 0.0f;
        default:
          return 0.0f;
      }
    }

    internal static RevenueSummaryData GetRevenueSummary(
      TimeSegmentType timesegment,
      Player player,
      out RevenueSummaryData PreviousSegmentData)
    {
      RevenueSummaryData revenueSummaryData = new RevenueSummaryData(timesegment, player, false);
      PreviousSegmentData = new RevenueSummaryData(timesegment, player, true);
      return revenueSummaryData;
    }
  }
}
