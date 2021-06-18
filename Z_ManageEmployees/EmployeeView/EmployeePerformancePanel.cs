// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.EmployeeView.EmployeePerformancePanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageShop.Shop_Data;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManageEmployees.EmployeeView
{
  internal class EmployeePerformancePanel
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private MiniHeading miniHeading;
    private EmployeePerformanceTable performanceTable;
    private float totalHeight;
    private ShopEntry refShop;
    private EmployeeDisplayType refDisplayType;
    private EmployeePerformanceFilter filter;
    private bool HasEmployeesToShow;

    public EmployeePerformancePanel(
      EmployeeDisplayType displayType,
      ShopEntry shopEntry,
      Player player,
      float BaseScale,
      EmployeeType _RoamingEmployeeType = EmployeeType.None)
    {
      this.refShop = shopEntry;
      this.refDisplayType = displayType;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      this.totalHeight = 0.0f;
      int num = -1;
      List<Employee> employees;
      EmployeeType employeetype;
      if (_RoamingEmployeeType != EmployeeType.None)
      {
        employees = player.employees.GetEmployeesOfThisType(_RoamingEmployeeType);
        employeetype = _RoamingEmployeeType;
      }
      else
      {
        employees = shopEntry.GetListOfEmployeesHere();
        EmployeeData.IsThisAnEmployee(EmployeeData.GetBuildingtoEmployee(shopEntry.tiletype, out bool _), out employeetype);
        num = ShopData.GetMaximumEmployeesForThisShop(shopEntry.tiletype, player);
      }
      Vector2 vector2_1 = Vector2.One * 10f;
      string empty = string.Empty;
      string text = (_RoamingEmployeeType == EmployeeType.None ? (object) (empty + TileData.GetTileStats(shopEntry.tiletype).Name) : (object) (empty + Employees.GetEmployeeThypeToString(_RoamingEmployeeType))).ToString() + ": " + (object) employees.Count;
      if (num != -1)
        text = text + "/" + (object) num;
      this.miniHeading = new MiniHeading(Vector2.Zero, text, 1f, BaseScale);
      this.totalHeight += this.miniHeading.GetTextHeight();
      this.totalHeight += uiScaleHelper.ScaleY(vector2_1.Y);
      this.totalHeight += defaultYbuffer;
      this.filter = new EmployeePerformanceFilter(BaseScale, employeetype, uiScaleHelper.ScaleX(EmployeePerformanceTable.widths[0]));
      this.filter.location.Y = this.totalHeight + uiScaleHelper.ScaleY((float) EmployeePerformanceTable.HeightPerRow_Raw) * 0.5f;
      this.filter.location.X = defaultXbuffer + uiScaleHelper.ScaleX(EmployeePerformanceTable.widths[0]) * 0.5f;
      this.performanceTable = new EmployeePerformanceTable(employees, BaseScale);
      Vector2 size = this.performanceTable.GetSize();
      this.performanceTable.location.Y = this.totalHeight;
      this.totalHeight += size.Y;
      this.totalHeight += defaultYbuffer;
      this.customerFrame = new CustomerFrame(new Vector2(size.X + defaultXbuffer * 2f, this.totalHeight), true, BaseScale);
      this.miniHeading.SetTextPosition(this.customerFrame.VSCale, vector2_1.X, vector2_1.Y);
      Vector2 vector2_2 = -this.customerFrame.VSCale * 0.5f;
      if (this.performanceTable != null)
        this.performanceTable.location.Y += vector2_2.Y;
      this.filter.location += vector2_2;
      this.SetData(this.filter.filterType, player, _RoamingEmployeeType);
      this.SetSummaryData(player, _RoamingEmployeeType);
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateEmployeePerformancePanel(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out Employee ViewThisEmployeeInfo)
    {
      offset += this.location;
      ViewThisEmployeeInfo = (Employee) null;
      if (this.filter.UpdateEmployeePerformanceFilter(player, DeltaTime, offset))
        this.SetData(this.filter.filterType, player);
      return this.performanceTable.UpdateEmployeePerformanceTable(player, DeltaTime, offset, out ViewThisEmployeeInfo);
    }

    public void SetSummaryData(Player player, EmployeeType RoamingEmployeeType) => this.performanceTable.SetSummaryData(ManageEmployeeDisplayData.GetEmployeePerformanceDataForThis(this.refDisplayType, EmployeeFilterType.ThisBranch, player, this.refShop, RoamingEmployeeType));

    public void SetData(
      EmployeeFilterType filterType,
      Player player,
      EmployeeType RoamingEmplyeeType = EmployeeType.None)
    {
      this.performanceTable.SetData(ManageEmployeeDisplayData.GetEmployeePerformanceDataForThis(this.refDisplayType, filterType, player, this.refShop, RoamingEmplyeeType));
    }

    public void DrawEmployeePerformancePanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.miniHeading.DrawMiniHeading(offset, spriteBatch);
      this.performanceTable.DrawEmployeePerformanceTable(offset, spriteBatch);
      this.filter.DrawEmployeePerformanceFilter(offset, spriteBatch);
    }
  }
}
