// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Revenue.Table.FinanceTableDataHelper
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;
using System.Linq;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Records;

namespace TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Revenue.Table
{
  internal class FinanceTableDataHelper
  {
    public static string[] GetTableHeaderRowStrings(ParkSummaryTableType tableType)
    {
      List<RevenueSummaryColumn> columnsForThisTable = FinanceTableDataHelper.GetColumnsForThisTable(ParkSummaryTableType.ProfitBreakdown);
      string[] strArray = new string[columnsForThisTable.Count];
      for (int index = 0; index < columnsForThisTable.Count; ++index)
      {
        string str = string.Empty;
        switch (columnsForThisTable[index])
        {
          case RevenueSummaryColumn.Category:
            str = "Category";
            break;
          case RevenueSummaryColumn.Revenue:
            switch (tableType)
            {
              case ParkSummaryTableType.Revenue:
                str = "Revenue";
                break;
              case ParkSummaryTableType.Expenditure:
                str = "Expenses";
                break;
              case ParkSummaryTableType.SummaryProfit:
                str = "Cash";
                break;
              case ParkSummaryTableType.ProfitBreakdown:
              case ParkSummaryTableType.Profit_FoodShops:
              case ParkSummaryTableType.Profit_DrinksShops:
              case ParkSummaryTableType.Profit_GiftShops:
              case ParkSummaryTableType.Profit_Commodities:
                str = "Profit";
                break;
              default:
                str = "Cash";
                break;
            }
            break;
          case RevenueSummaryColumn.ProfitPercentageMargin:
            str = "Margin";
            break;
          case RevenueSummaryColumn.LastRevenue:
            str = "Previous";
            break;
          case RevenueSummaryColumn.PercentageComparison:
            str = "Change";
            break;
        }
        strArray[index] = str;
      }
      return strArray;
    }

    public static string GetTableName(ParkSummaryTableType tableType)
    {
      switch (tableType)
      {
        case ParkSummaryTableType.SummaryProfit:
          return "Profit Overview";
        case ParkSummaryTableType.ProfitBreakdown:
          return "Profit Breakdown";
        case ParkSummaryTableType.Profit_OtherRevenue:
          return FinanceTableDataHelper.GetCategoryString(new FinanceTableRowTypeContainer(FinanceTableRowType.ProfitCat_OtherRevenue));
        case ParkSummaryTableType.Profit_FoodShops:
          return FinanceTableDataHelper.GetCategoryString(new FinanceTableRowTypeContainer(FinanceTableRowType.ProfitCat_FoodShops));
        case ParkSummaryTableType.Profit_DrinksShops:
          return FinanceTableDataHelper.GetCategoryString(new FinanceTableRowTypeContainer(FinanceTableRowType.ProfitCat_DrinksShops));
        case ParkSummaryTableType.Profit_GiftShops:
          return FinanceTableDataHelper.GetCategoryString(new FinanceTableRowTypeContainer(FinanceTableRowType.ProfitCat_GiftShops));
        case ParkSummaryTableType.Profit_Animals:
          return FinanceTableDataHelper.GetCategoryString(new FinanceTableRowTypeContainer(FinanceTableRowType.ProfitCat_Animals));
        case ParkSummaryTableType.Profit_ParkRevenue:
          return FinanceTableDataHelper.GetCategoryString(new FinanceTableRowTypeContainer(FinanceTableRowType.ProfitCat_ParkRevenue));
        case ParkSummaryTableType.Profit_Commodities:
          return FinanceTableDataHelper.GetCategoryString(new FinanceTableRowTypeContainer(FinanceTableRowType.ProfitCat_Commodities));
        default:
          return "";
      }
    }

    public static List<RevenueSummaryColumn> GetColumnsForThisTable(
      ParkSummaryTableType tableType)
    {
      List<RevenueSummaryColumn> revenueSummaryColumnList = new List<RevenueSummaryColumn>();
      revenueSummaryColumnList.Add(RevenueSummaryColumn.Category);
      switch (tableType)
      {
        case ParkSummaryTableType.Revenue:
        case ParkSummaryTableType.Expenditure:
        case ParkSummaryTableType.SummaryProfit:
          revenueSummaryColumnList.Add(RevenueSummaryColumn.Revenue);
          revenueSummaryColumnList.Add(RevenueSummaryColumn.LastRevenue);
          revenueSummaryColumnList.Add(RevenueSummaryColumn.PercentageComparison);
          break;
        case ParkSummaryTableType.ProfitBreakdown:
        case ParkSummaryTableType.Profit_FoodShops:
        case ParkSummaryTableType.Profit_DrinksShops:
        case ParkSummaryTableType.Profit_GiftShops:
        case ParkSummaryTableType.Profit_Commodities:
          revenueSummaryColumnList.Add(RevenueSummaryColumn.Revenue);
          revenueSummaryColumnList.Add(RevenueSummaryColumn.ProfitPercentageMargin);
          revenueSummaryColumnList.Add(RevenueSummaryColumn.LastRevenue);
          revenueSummaryColumnList.Add(RevenueSummaryColumn.PercentageComparison);
          break;
        default:
          revenueSummaryColumnList.Add(RevenueSummaryColumn.Revenue);
          revenueSummaryColumnList.Add(RevenueSummaryColumn.LastRevenue);
          revenueSummaryColumnList.Add(RevenueSummaryColumn.PercentageComparison);
          break;
      }
      return revenueSummaryColumnList;
    }

    public static ParkSummaryTableType GetDetailBreakdownTableTypeFromThisRow(
      FinanceTableRowTypeContainer rowType)
    {
      if (rowType == null)
        return ParkSummaryTableType.Count;
      switch (rowType.rowType)
      {
        case FinanceTableRowType.Sum_RevenueIn:
        case FinanceTableRowType.Sum_RunningCosts:
        case FinanceTableRowType.Sum_Purchases:
          return ParkSummaryTableType.ProfitBreakdown;
        case FinanceTableRowType.ProfitCat_FoodShops:
          return ParkSummaryTableType.Profit_FoodShops;
        case FinanceTableRowType.ProfitCat_DrinksShops:
          return ParkSummaryTableType.Profit_DrinksShops;
        case FinanceTableRowType.ProfitCat_GiftShops:
          return ParkSummaryTableType.Profit_GiftShops;
        case FinanceTableRowType.ProfitCat_ParkRevenue:
          return ParkSummaryTableType.Profit_ParkRevenue;
        case FinanceTableRowType.ProfitCat_Commodities:
          return ParkSummaryTableType.Profit_Commodities;
        case FinanceTableRowType.ProfitCat_Animals:
          return ParkSummaryTableType.Profit_Animals;
        case FinanceTableRowType.ProfitCat_OtherRevenue:
          return ParkSummaryTableType.Profit_OtherRevenue;
        default:
          return ParkSummaryTableType.Count;
      }
    }

    public static string GetSummaryRowText(ParkSummaryTableType tableType)
    {
      switch (tableType)
      {
        case ParkSummaryTableType.Revenue:
          return "Total Revenue";
        case ParkSummaryTableType.Expenditure:
          return "Total Expenses";
        case ParkSummaryTableType.SummaryProfit:
          return "Total Profit";
        case ParkSummaryTableType.ProfitBreakdown:
          return "Total Profit";
        case ParkSummaryTableType.Profit_OtherRevenue:
          return FinanceTableDataHelper.GetCategoryString(new FinanceTableRowTypeContainer(FinanceTableRowType.ProfitCat_OtherRevenue));
        case ParkSummaryTableType.Profit_FoodShops:
          return FinanceTableDataHelper.GetCategoryString(new FinanceTableRowTypeContainer(FinanceTableRowType.ProfitCat_FoodShops));
        case ParkSummaryTableType.Profit_DrinksShops:
          return FinanceTableDataHelper.GetCategoryString(new FinanceTableRowTypeContainer(FinanceTableRowType.ProfitCat_DrinksShops));
        case ParkSummaryTableType.Profit_GiftShops:
          return FinanceTableDataHelper.GetCategoryString(new FinanceTableRowTypeContainer(FinanceTableRowType.ProfitCat_GiftShops));
        case ParkSummaryTableType.Profit_Animals:
          return FinanceTableDataHelper.GetCategoryString(new FinanceTableRowTypeContainer(FinanceTableRowType.ProfitCat_Animals));
        case ParkSummaryTableType.Profit_ParkRevenue:
          return FinanceTableDataHelper.GetCategoryString(new FinanceTableRowTypeContainer(FinanceTableRowType.ProfitCat_ParkRevenue));
        case ParkSummaryTableType.Profit_Commodities:
          return FinanceTableDataHelper.GetCategoryString(new FinanceTableRowTypeContainer(FinanceTableRowType.ProfitCat_Commodities));
        default:
          return "NA";
      }
    }

    public static List<FinanceTableRowTypeContainer> GetReOrderedRows(
      ParkSummaryTableType tableType,
      RevenueSummaryData summaryData)
    {
      bool flag = false;
      List<FinanceTableRowTypeContainer> rowTypeContainerList = new List<FinanceTableRowTypeContainer>();
      switch (tableType)
      {
        case ParkSummaryTableType.Revenue:
          for (int index = 0; index < summaryData.revenueentries.Length; ++index)
          {
            if (summaryData.revenueentries[index].DollarValue > 0)
            {
              flag = true;
              break;
            }
          }
          RevenueEntry[] revenueEntryArray = summaryData.revenueentries;
          if (flag)
          {
            RevenueEntry[] array = ((IEnumerable<RevenueEntry>) summaryData.revenueentries).OrderBy<RevenueEntry, int>((Func<RevenueEntry, int>) (x => x.DollarValue)).ToArray<RevenueEntry>();
            Array.Reverse((Array) array);
            revenueEntryArray = array;
          }
          for (int index = 0; index < revenueEntryArray.Length; ++index)
            rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableDataHelper.GetRevenueOriginToRowType(revenueEntryArray[index].revenueorigin)));
          break;
        case ParkSummaryTableType.Expenditure:
          for (int index = 8; index < 15; ++index)
            rowTypeContainerList.Add(new FinanceTableRowTypeContainer((FinanceTableRowType) index));
          break;
        case ParkSummaryTableType.SummaryProfit:
          for (int index = 16; index < 19; ++index)
            rowTypeContainerList.Add(new FinanceTableRowTypeContainer((FinanceTableRowType) index));
          break;
        case ParkSummaryTableType.ProfitBreakdown:
          for (int index = 20; index < 27; ++index)
            rowTypeContainerList.Add(new FinanceTableRowTypeContainer((FinanceTableRowType) index));
          break;
        case ParkSummaryTableType.Profit_OtherRevenue:
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Other_LoansIn));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Other_LoansOut));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Other_SponsersIn));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Others_DonationsIn));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Others_DonationsStaffOut));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Others_FinesOut));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Others_TaskRewardsIn));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Others_FinesOut));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Others_Bribe));
          break;
        case ParkSummaryTableType.Profit_FoodShops:
          List<TILETYPE> list1 = TileData.GetFoodShops().ToList<TILETYPE>();
          for (int index = 0; index < list1.Count; ++index)
            rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.ShopOrFactoryTILETYPE, list1[index]));
          break;
        case ParkSummaryTableType.Profit_DrinksShops:
          List<TILETYPE> list2 = TileData.GetDrinksShops().ToList<TILETYPE>();
          for (int index = 0; index < list2.Count; ++index)
            rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.ShopOrFactoryTILETYPE, list2[index]));
          break;
        case ParkSummaryTableType.Profit_GiftShops:
          List<TILETYPE> list3 = TileData.GetGiftShops().ToList<TILETYPE>();
          for (int index = 0; index < list3.Count; ++index)
            rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.ShopOrFactoryTILETYPE, list3[index]));
          break;
        case ParkSummaryTableType.Profit_Animals:
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Other_LoansIn));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Other_LoansOut));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Other_SponsersIn));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Others_DonationsIn));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Others_DonationsStaffOut));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Others_FinesOut));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Others_TaskRewardsIn));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Others_FinesOut));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Others_Bribe));
          break;
        case ParkSummaryTableType.Profit_ParkRevenue:
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Park_Tickets));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Park_StaffBonus));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Park_GeneralParkStaff));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Park_Utilities));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Park_Decoration));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Park_Infrastructure));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Park_Land));
          rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.Park_Recruitment));
          break;
        case ParkSummaryTableType.Profit_Commodities:
          List<TILETYPE> list4 = TileData.GetFactories().ToList<TILETYPE>();
          list4.Insert(0, TILETYPE.FarmProcessor);
          list4.Insert(0, TILETYPE.MeatProcessor);
          for (int index = 0; index < list4.Count; ++index)
            rowTypeContainerList.Add(new FinanceTableRowTypeContainer(FinanceTableRowType.ShopOrFactoryTILETYPE, list4[index]));
          break;
      }
      return rowTypeContainerList;
    }

    public static string GetCategoryString(FinanceTableRowTypeContainer origin)
    {
      switch (origin.rowType)
      {
        case FinanceTableRowType.In_EntryTicket_DEP:
          return "Ticket";
        case FinanceTableRowType.In_Food_DEP:
          return "Food";
        case FinanceTableRowType.In_Beverage_DEP:
          return "Beverages";
        case FinanceTableRowType.In_Souveniers_DEP:
          return "Souvenirs";
        case FinanceTableRowType.In_Donations_DEP:
          return "Donations";
        case FinanceTableRowType.In_Commodities_DEP:
          return "Commodities";
        case FinanceTableRowType.In_BlackMarket_DEP:
          return "Black Market";
        case FinanceTableRowType.Out_Buildings_DEP:
          return "Building";
        case FinanceTableRowType.Out_Retail_DEP:
          return "Retail";
        case FinanceTableRowType.Out_NonRetailStaff_DEP:
          return "Staff";
        case FinanceTableRowType.Out_AdminMarketing_DEP:
          return "Marketing";
        case FinanceTableRowType.Out_Upgrades_DEP:
          return "Upgrades";
        case FinanceTableRowType.Out_Animals_DEP:
          return "Animals";
        case FinanceTableRowType.Out_Food_DEP:
          return "Animal Food";
        case FinanceTableRowType.Sum_RevenueIn:
          return "Income";
        case FinanceTableRowType.Sum_RunningCosts:
          return "Ongoing Expenses";
        case FinanceTableRowType.Sum_Purchases:
          return "Purchases";
        case FinanceTableRowType.ProfitCat_FoodShops:
          return "Food Shops";
        case FinanceTableRowType.ProfitCat_DrinksShops:
          return "Drinks Shops";
        case FinanceTableRowType.ProfitCat_GiftShops:
          return "Gift Shops";
        case FinanceTableRowType.ProfitCat_ParkRevenue:
          return "Park Revenue";
        case FinanceTableRowType.ProfitCat_Commodities:
          return "Commodities";
        case FinanceTableRowType.ProfitCat_Animals:
          return "Animals";
        case FinanceTableRowType.ProfitCat_OtherRevenue:
          return "Other Revenue";
        case FinanceTableRowType.Animals_Food:
          return "Animal Food";
        case FinanceTableRowType.Animals_Vet:
          return "Vet Wages";
        case FinanceTableRowType.Animals_Zookeeper:
          return "Zookeper Wages";
        case FinanceTableRowType.Animals_ShelterBuy:
          return "Shelter Rescues";
        case FinanceTableRowType.Animals_BlackMarketBuy:
          return "Black Market Purchases";
        case FinanceTableRowType.Animals_BlackMarketSell:
          return "Black Market Sales";
        case FinanceTableRowType.Animals_PenBuild:
          return "Pen Build Costs";
        case FinanceTableRowType.Animals_Enrichment:
          return "Enrichment";
        case FinanceTableRowType.Park_Tickets:
          return "Tickets";
        case FinanceTableRowType.Park_StaffBonus:
          return "Staff Bonuses";
        case FinanceTableRowType.Park_GeneralParkStaff:
          return "General Park Staff";
        case FinanceTableRowType.Park_Utilities:
          return "Utilities";
        case FinanceTableRowType.Park_Decoration:
          return "Decoration";
        case FinanceTableRowType.Park_Infrastructure:
          return "Infrastructure";
        case FinanceTableRowType.Park_Land:
          return "Land";
        case FinanceTableRowType.Park_Recruitment:
          return "Recruitment";
        case FinanceTableRowType.Other_LoansIn:
          return "Loan Income";
        case FinanceTableRowType.Other_LoansOut:
          return "Loan Payment";
        case FinanceTableRowType.Other_SponsersIn:
          return "Sponsors";
        case FinanceTableRowType.Others_DonationsIn:
          return "Donations Received";
        case FinanceTableRowType.Others_DonationsStaffOut:
          return "Donations Given";
        case FinanceTableRowType.Others_Bribe:
          return "Bribes";
        case FinanceTableRowType.Others_TaskRewardsIn:
          return "Task Rewards";
        case FinanceTableRowType.Others_FinesOut:
          return "Fines";
        case FinanceTableRowType.ShopOrFactoryTILETYPE:
          return TileData.GetTileStats(origin.tileType).Name;
        default:
          return "MISSINGSTRING_" + (object) origin.rowType;
      }
    }

    public static FinanceTableRowType GetRevenueOriginToRowType(
      RevenueOrigin origin)
    {
      switch (origin)
      {
        case RevenueOrigin.EntryTicket:
          return FinanceTableRowType.In_EntryTicket_DEP;
        case RevenueOrigin.Food:
          return FinanceTableRowType.In_Food_DEP;
        case RevenueOrigin.Beverage:
          return FinanceTableRowType.In_Beverage_DEP;
        case RevenueOrigin.Souveniers:
          return FinanceTableRowType.In_Souveniers_DEP;
        case RevenueOrigin.Donations:
          return FinanceTableRowType.In_Donations_DEP;
        case RevenueOrigin.Commodities:
          return FinanceTableRowType.In_Commodities_DEP;
        case RevenueOrigin.BlackMarket:
          return FinanceTableRowType.In_BlackMarket_DEP;
        default:
          return FinanceTableRowType.Count;
      }
    }

    public static RevenueOrigin GetRowTypeToRevenueOrigin(FinanceTableRowType rowType)
    {
      switch (rowType)
      {
        case FinanceTableRowType.In_EntryTicket_DEP:
          return RevenueOrigin.EntryTicket;
        case FinanceTableRowType.In_Food_DEP:
          return RevenueOrigin.Food;
        case FinanceTableRowType.In_Beverage_DEP:
          return RevenueOrigin.Beverage;
        case FinanceTableRowType.In_Souveniers_DEP:
          return RevenueOrigin.Souveniers;
        case FinanceTableRowType.In_Donations_DEP:
          return RevenueOrigin.Donations;
        case FinanceTableRowType.In_Commodities_DEP:
          return RevenueOrigin.Commodities;
        case FinanceTableRowType.In_BlackMarket_DEP:
          return RevenueOrigin.BlackMarket;
        default:
          throw new Exception("[GetRowTypeToRevenueOrigin] case not filled: " + (object) rowType);
      }
    }

    public static int GetRevenueOrProfit(
      ParkSummaryTableType tableType,
      FinanceTableRowTypeContainer rowTypeC,
      RevenueSummaryData summaryData,
      bool GetSummaryTotal,
      out float profitMargin)
    {
      profitMargin = 0.0f;
      switch (tableType)
      {
        case ParkSummaryTableType.Revenue:
          return GetSummaryTotal ? summaryData.GetTotalRevenue() : summaryData.revenueentries[(int) FinanceTableDataHelper.GetRowTypeToRevenueOrigin(rowTypeC.rowType)].DollarValue;
        case ParkSummaryTableType.Expenditure:
          int num1 = GetSummaryTotal ? 1 : 0;
          return 0;
        case ParkSummaryTableType.SummaryProfit:
          if (GetSummaryTotal || rowTypeC.rowType == FinanceTableRowType.Sum_RevenueIn)
            return summaryData.GetTotalRevenue();
          if (rowTypeC.rowType == FinanceTableRowType.Sum_RunningCosts)
            return 0;
          int rowType = (int) rowTypeC.rowType;
          return 0;
        case ParkSummaryTableType.ProfitBreakdown:
        case ParkSummaryTableType.Profit_OtherRevenue:
        case ParkSummaryTableType.Profit_FoodShops:
        case ParkSummaryTableType.Profit_DrinksShops:
        case ParkSummaryTableType.Profit_GiftShops:
        case ParkSummaryTableType.Profit_Animals:
        case ParkSummaryTableType.Profit_ParkRevenue:
        case ParkSummaryTableType.Profit_Commodities:
          float num2 = 0.0f;
          float moneyIn = 0.0f;
          float moneyOut = 0.0f;
          List<FinanceTableRowTypeContainer> reOrderedRows;
          if (GetSummaryTotal)
          {
            reOrderedRows = FinanceTableDataHelper.GetReOrderedRows(tableType, summaryData);
          }
          else
          {
            reOrderedRows = FinanceTableDataHelper.GetReOrderedRows(FinanceTableDataHelper.GetDetailBreakdownTableTypeFromThisRow(rowTypeC), summaryData);
            if (reOrderedRows.Count == 0)
              return (int) Math.Round((double) FinanceTableDataHelper.GetAmountSpentOrEarned(rowTypeC, summaryData));
          }
          if (reOrderedRows != null)
          {
            for (int index = 0; index < reOrderedRows.Count; ++index)
            {
              float num3 = 0.0f;
              float num4 = !GetSummaryTotal ? num3 + FinanceTableDataHelper.GetAmountSpentOrEarned(reOrderedRows[index], summaryData) : num3 + (float) FinanceTableDataHelper.GetRevenueOrProfit(tableType, reOrderedRows[index], summaryData, false, out float _);
              num2 += num4;
              if ((double) num4 > 0.0)
                moneyIn += num4;
              else
                moneyOut += num4;
              profitMargin = FinanceTableDataHelper.GetProfitMarginPercentage(moneyIn, moneyOut);
            }
          }
          return (int) Math.Round((double) num2);
        default:
          return -1;
      }
    }

    public static float GetPercentChange(float thisAmount, float lastAmount) => (double) lastAmount != 0.0 || (double) thisAmount != 0.0 ? ((double) lastAmount != 0.0 ? ((double) thisAmount != 0.0 ? (thisAmount - lastAmount) / thisAmount * 100f : -100f) : 100f) : 0.0f;

    public static float GetProfitMarginPercentage(float moneyIn, float moneyOut)
    {
      float num = 0.0f;
      if ((double) moneyIn == 0.0)
      {
        if ((double) moneyOut <= (double) moneyIn)
          ;
      }
      else
        num = (moneyIn - moneyOut) / moneyIn;
      return num * 100f;
    }

    public static float GetAmountSpentOrEarned(
      FinanceTableRowTypeContainer container,
      RevenueSummaryData summaryData)
    {
      switch (container.rowType)
      {
        case FinanceTableRowType.Animals_Food:
          return 0.0f;
        case FinanceTableRowType.Animals_Vet:
          return 0.0f;
        case FinanceTableRowType.Animals_Zookeeper:
          return 0.0f;
        case FinanceTableRowType.Animals_ShelterBuy:
          return 0.0f;
        case FinanceTableRowType.Animals_BlackMarketBuy:
          return 0.0f;
        case FinanceTableRowType.Animals_BlackMarketSell:
          return (float) summaryData.revenueentries[6].DollarValue;
        case FinanceTableRowType.Animals_PenBuild:
          return 0.0f;
        case FinanceTableRowType.Animals_Enrichment:
          return 0.0f;
        case FinanceTableRowType.Park_Tickets:
          return (float) summaryData.revenueentries[0].DollarValue;
        case FinanceTableRowType.Park_StaffBonus:
          return 0.0f;
        case FinanceTableRowType.Park_GeneralParkStaff:
          return 0.0f;
        case FinanceTableRowType.Park_Utilities:
          return 0.0f;
        case FinanceTableRowType.Park_Decoration:
          return 0.0f;
        case FinanceTableRowType.Park_Infrastructure:
          return 0.0f;
        case FinanceTableRowType.Park_Land:
          return 0.0f;
        case FinanceTableRowType.Park_Recruitment:
          return 0.0f;
        case FinanceTableRowType.Other_LoansIn:
          return 0.0f;
        case FinanceTableRowType.Other_LoansOut:
          return 0.0f;
        case FinanceTableRowType.Other_SponsersIn:
          return 0.0f;
        case FinanceTableRowType.Others_DonationsIn:
          return (float) summaryData.revenueentries[4].DollarValue;
        case FinanceTableRowType.Others_DonationsStaffOut:
          return 0.0f;
        case FinanceTableRowType.Others_Bribe:
          return 0.0f;
        case FinanceTableRowType.Others_TaskRewardsIn:
          return 0.0f;
        case FinanceTableRowType.Others_FinesOut:
          return 0.0f;
        case FinanceTableRowType.ShopOrFactoryTILETYPE:
          return 0.0f;
        default:
          return 0.0f;
      }
    }
  }
}
