// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.ManageEmployeeDisplayData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_ManageEmployees.EmployeeView;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable;

namespace TinyZoo.Z_ManageEmployees
{
  internal class ManageEmployeeDisplayData
  {
    public static EmployeeDisplayType GetTileTypeToEmployeeDisplayType(
      TILETYPE tileType,
      Vector2Int location,
      Player player,
      out ShopEntry shopEntry)
    {
      shopEntry = (ShopEntry) null;
      if (TileData.IsThisAShopWithShopStats(tileType))
      {
        shopEntry = player.shopstatus.GetThisShop(location, tileType);
        return EmployeeDisplayType.RetailShop;
      }
      if (TileData.IsAnArchitectOffice(tileType))
      {
        shopEntry = player.shopstatus.GetThisArchitectsOffice(location);
        return EmployeeDisplayType.Research;
      }
      if (TileData.IsAFactory(tileType) || TileData.IsAnIncinerator(tileType) || (TileData.IsAMeatProcessingPlant(tileType) || TileData.IsAVegetableProcessingPlant(tileType)))
      {
        shopEntry = player.shopstatus.GetThisFacility(location);
        return EmployeeDisplayType.Factory;
      }
      if (!TileData.IsACRISPRBuilding(tileType) && !TileData.IsABreedingRoom(tileType) && (tileType != TILETYPE.VetOffice && !TileData.IsThisAFacility(tileType)))
        return EmployeeDisplayType.Count;
      shopEntry = player.shopstatus.GetThisFacility(location);
      return EmployeeDisplayType.Facility;
    }

    public static List<PerformanceColumn> GetTableColumnTypeFromDisplayType(
      EmployeeDisplayType displayType)
    {
      List<PerformanceColumn> performanceColumnList = new List<PerformanceColumn>();
      performanceColumnList.Add(PerformanceColumn.Employee);
      switch (displayType)
      {
        case EmployeeDisplayType.RetailShop:
        case EmployeeDisplayType.Research:
        case EmployeeDisplayType.Facility:
          performanceColumnList.Add(PerformanceColumn.Efficiency);
          performanceColumnList.Add(PerformanceColumn.Satisfaction);
          performanceColumnList.Add(PerformanceColumn.Salary);
          break;
        default:
          performanceColumnList.Add(PerformanceColumn.Efficiency);
          performanceColumnList.Add(PerformanceColumn.Satisfaction);
          performanceColumnList.Add(PerformanceColumn.Salary);
          break;
      }
      return performanceColumnList;
    }

    public static BuildingSummaryData GetBuildingSummaryDataForThis(
      EmployeeDisplayType displayType,
      Player player,
      ShopEntry shopEntry,
      out bool HasData,
      EmployeeType RoamingEmployeeType = EmployeeType.None)
    {
      HasData = true;
      List<Employee> employeesInThisShop = RoamingEmployeeType == EmployeeType.None ? player.employees.GetEmployeesInThisSpecificShop(shopEntry.ShopUID) : player.employees.GetEmployeesOfThisType(RoamingEmployeeType);
      BuildingSummaryData buildingSummaryData = new BuildingSummaryData();
      if (RoamingEmployeeType == EmployeeType.None && shopEntry != null)
        buildingSummaryData.tileType = shopEntry.tiletype;
      switch (displayType)
      {
        case EmployeeDisplayType.RetailShop:
          buildingSummaryData.totalDailySalaries = ManageEmployeeDisplayData.CalculateTotalDailySalaries(employeesInThisShop);
          buildingSummaryData.totalShopEarnings = 0;
          buildingSummaryData.totalRevenue = buildingSummaryData.totalShopEarnings - buildingSummaryData.totalDailySalaries;
          break;
        case EmployeeDisplayType.Research:
          buildingSummaryData.totalDailySalaries = ManageEmployeeDisplayData.CalculateTotalDailySalaries(employeesInThisShop);
          break;
        case EmployeeDisplayType.Factory:
          buildingSummaryData.totalDailySalaries = ManageEmployeeDisplayData.CalculateTotalDailySalaries(employeesInThisShop);
          buildingSummaryData.productsGenerated = 0;
          break;
        case EmployeeDisplayType.Facility:
          buildingSummaryData.totalDailySalaries = ManageEmployeeDisplayData.CalculateTotalDailySalaries(employeesInThisShop);
          break;
      }
      if (buildingSummaryData.totalDailySalaries == 0)
        HasData = false;
      return buildingSummaryData;
    }

    private static int CalculateTotalDailySalaries(List<Employee> employeesInThisShop)
    {
      int num = 0;
      for (int index = 0; index < employeesInThisShop.Count; ++index)
        num = (num + employeesInThisShop[index].quickemplyeedescription.CurrentSalary) / 7;
      return num;
    }

    public static PerformanceSummaryData GetEmployeePerformanceDataForThis(
      EmployeeDisplayType displayType,
      EmployeeFilterType filterType,
      Player player,
      ShopEntry refShop,
      EmployeeType RoamingEmplyeeType = EmployeeType.None,
      bool GetDataForYesterday = false)
    {
      PerformanceSummaryData performanceSummaryData = new PerformanceSummaryData();
      List<Employee> employeeList = new List<Employee>();
      if (filterType == EmployeeFilterType.ThisBranch && RoamingEmplyeeType != EmployeeType.None)
        filterType = EmployeeFilterType.RoamingEmployee;
      switch (filterType)
      {
        case EmployeeFilterType.ThisBranch:
          employeeList = player.employees.GetEmployeesInThisSpecificShop(refShop.ShopUID);
          break;
        case EmployeeFilterType.SameShopBranches:
          employeeList = player.employees.GetEmployeesInThisTileType(refShop.tiletype);
          break;
        case EmployeeFilterType.RoamingEmployee:
          employeeList = player.employees.GetEmployeesOfThisType(RoamingEmplyeeType);
          break;
        case EmployeeFilterType.AllEmployees:
          employeeList = player.employees.employees;
          break;
      }
      switch (displayType)
      {
        case EmployeeDisplayType.RetailShop:
        case EmployeeDisplayType.Research:
        case EmployeeDisplayType.Facility:
          float num1 = 0.0f;
          int num2 = 0;
          float num3 = 0.0f;
          for (int index = 0; index < employeeList.Count; ++index)
          {
            num1 += 0.1f;
            num2 += employeeList[index].quickemplyeedescription.CurrentSalary / 7;
            num3 += 0.1f;
          }
          if (employeeList.Count > 0)
          {
            performanceSummaryData.AverageEfficiency = num1 / (float) employeeList.Count;
            performanceSummaryData.AverageDailySalary = num2 / employeeList.Count;
            performanceSummaryData.AverageSatisfaction = num3 / (float) employeeList.Count;
            break;
          }
          performanceSummaryData.AverageEfficiency = 0.0f;
          performanceSummaryData.AverageSatisfaction = 0.0f;
          performanceSummaryData.AverageDailySalary = 0;
          break;
      }
      return performanceSummaryData;
    }
  }
}
