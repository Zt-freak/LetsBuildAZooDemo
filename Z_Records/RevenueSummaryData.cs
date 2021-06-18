// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Records.RevenueSummaryData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.PlayerDir.FinancialHistory;
using TinyZoo.Z_SummaryPopUps.Generic;

namespace TinyZoo.Z_Records
{
  internal class RevenueSummaryData
  {
    public RevenueEntry[] revenueentries;
    public bool NoData;

    public RevenueSummaryData(TimeSegmentType timesegment, Player player, bool PreviousSegment)
    {
      this.revenueentries = new RevenueEntry[7];
      for (int index = 0; index < this.revenueentries.Length; ++index)
        this.revenueentries[index] = new RevenueEntry((RevenueOrigin) index);
      List<DayStats> daystatsByTimeSegment = Player.financialrecords.GetDaystatsByTimeSegment(timesegment, out this.NoData, PreviousSegment);
      for (int index = 0; index < daystatsByTimeSegment.Count; ++index)
      {
        this.revenueentries[0].DollarValue += daystatsByTimeSegment[index].MoneyFromTicketSales;
        this.revenueentries[1].DollarValue += daystatsByTimeSegment[index].MoneyFromFood;
        this.revenueentries[2].DollarValue += daystatsByTimeSegment[index].MoneyFromDrinks;
        this.revenueentries[3].DollarValue += daystatsByTimeSegment[index].MoneyFromSouvenirs;
        this.revenueentries[4].DollarValue += daystatsByTimeSegment[index].MoneyFromDonations;
        this.revenueentries[6].DollarValue += daystatsByTimeSegment[index].MonsyFromBlackMarket;
        this.revenueentries[5].DollarValue += daystatsByTimeSegment[index].MoneyFromCommodities;
      }
    }

    public int GetTotalRevenue()
    {
      int num = 0;
      for (int index = 0; index < this.revenueentries.Length; ++index)
        num += this.revenueentries[index].DollarValue;
      return num;
    }
  }
}
