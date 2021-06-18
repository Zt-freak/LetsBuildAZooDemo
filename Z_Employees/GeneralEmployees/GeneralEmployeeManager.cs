// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.GeneralEmployees.GeneralEmployeeManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.PlayerDir;
using TinyZoo.Z_Employees.GeneralEmployees.NotHere;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Employees.GeneralEmployees
{
  internal class GeneralEmployeeManager
  {
    private BigBrownPanel bigpanel;
    public Vector2 Location;
    private List<EmployeeByTypeSummary> employeesummary;
    private SimpleArrowPageButtons simplearrows;
    private int Page;
    private float BaseScale;
    private ParkStaffScreenState screenstate;
    private float UnmultipliedWidth;
    public EmployeeType SwitchToDetailViewForThis;
    private NotHereCollection notherecollection;
    private int TotalPages = 3;

    public GeneralEmployeeManager(Player player, EmployeeType forceToPageWithThisEmployee = EmployeeType.None)
    {
      this.screenstate = ParkStaffScreenState.Main;
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.BaseScale = baseScaleForUi;
      this.bigpanel = new BigBrownPanel(Vector2.Zero, true, "Park Staff", baseScaleForUi);
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      this.UnmultipliedWidth = 465f;
      this.MakePage(this.UnmultipliedWidth, player);
      this.simplearrows = new SimpleArrowPageButtons(baseScaleForUi);
      Vector2 size = this.employeesummary[0].GetSize();
      size.Y *= (float) this.employeesummary.Count;
      size.Y += 10f * baseScaleForUi * (float) (this.employeesummary.Count - 1);
      size.Y += uiScaleHelper.DefaultBuffer.Y;
      size.Y += this.simplearrows.GetSize(true).Y;
      this.bigpanel.Finalize(size);
      this.PositionEmployeeSummary();
      this.Location = new Vector2(700f, 384f);
      this.simplearrows.Location = size + this.bigpanel.GetFrameOffsetFromTop();
      this.simplearrows.Location -= this.simplearrows.GetSize(true) * 0.5f;
      if (forceToPageWithThisEmployee == EmployeeType.None)
        return;
      for (int index = 0; index < this.TotalPages; ++index)
      {
        if (this.GetListOfEmployeesForThisPage(index).Contains(forceToPageWithThisEmployee))
          this.ChangePage(0, player, index);
      }
    }

    private void MakePage(float UnmultipliedWidth, Player player)
    {
      this.employeesummary = new List<EmployeeByTypeSummary>();
      List<EmployeeType> employeesForThisPage = this.GetListOfEmployeesForThisPage(this.Page);
      for (int index = 0; index < employeesForThisPage.Count; ++index)
        this.employeesummary.Add(new EmployeeByTypeSummary(employeesForThisPage[index], player, this.BaseScale, UnmultipliedWidth));
      this.PositionEmployeeSummary();
    }

    private List<EmployeeType> GetListOfEmployeesForThisPage(int page)
    {
      List<EmployeeType> employeeTypeList = new List<EmployeeType>();
      if (Z_DebugFlags.IsBetaVersion)
      {
        switch (page)
        {
          case 0:
            employeeTypeList.Add(EmployeeType.Keeper);
            employeeTypeList.Add(EmployeeType.Janitor);
            employeeTypeList.Add(EmployeeType.Architect);
            employeeTypeList.Add(EmployeeType.ShopKeeper);
            employeeTypeList.Add(EmployeeType.DNAResearcher);
            break;
          case 1:
            employeeTypeList.Add(EmployeeType.Guide);
            employeeTypeList.Add(EmployeeType.Mascot);
            employeeTypeList.Add(EmployeeType.Mechanic);
            employeeTypeList.Add(EmployeeType.Breeder);
            employeeTypeList.Add(EmployeeType.Vet);
            break;
          case 2:
            employeeTypeList.Add(EmployeeType.Farmer);
            employeeTypeList.Add(EmployeeType.MeatProcessorWorker);
            employeeTypeList.Add(EmployeeType.FactoryWorker);
            employeeTypeList.Add(EmployeeType.VegProcessorWorker);
            employeeTypeList.Add(EmployeeType.WarehouseWorker);
            break;
        }
      }
      else
      {
        switch (page)
        {
          case 0:
            employeeTypeList.Add(EmployeeType.Keeper);
            employeeTypeList.Add(EmployeeType.Janitor);
            employeeTypeList.Add(EmployeeType.Mechanic);
            employeeTypeList.Add(EmployeeType.Mascot);
            employeeTypeList.Add(EmployeeType.ShopKeeper);
            break;
          case 1:
            employeeTypeList.Add(EmployeeType.Guide);
            employeeTypeList.Add(EmployeeType.Architect);
            employeeTypeList.Add(EmployeeType.Breeder);
            employeeTypeList.Add(EmployeeType.Vet);
            employeeTypeList.Add(EmployeeType.DNAResearcher);
            break;
          case 2:
            employeeTypeList.Add(EmployeeType.Farmer);
            employeeTypeList.Add(EmployeeType.MeatProcessorWorker);
            employeeTypeList.Add(EmployeeType.FactoryWorker);
            employeeTypeList.Add(EmployeeType.VegProcessorWorker);
            employeeTypeList.Add(EmployeeType.WarehouseWorker);
            break;
        }
      }
      return employeeTypeList;
    }

    private void PositionEmployeeSummary()
    {
      Vector2 size = this.employeesummary[0].GetSize();
      for (int index = 0; index < this.employeesummary.Count; ++index)
      {
        this.employeesummary[index].Location.Y = size.Y * 0.5f;
        this.employeesummary[index].Location.Y += (size.Y + 10f * this.BaseScale) * (float) index;
        this.employeesummary[index].Location.Y += this.bigpanel.GetFrameOffsetFromTop().Y;
      }
    }

    public bool CheckMouseOver(Player player) => this.bigpanel.CheckMouseOver(player, this.Location);

    private void ChangePage(int PageChange, Player player, int forceToThisPage = -1)
    {
      int max = this.TotalPages - 1;
      if (forceToThisPage != -1)
      {
        this.Page = MathHelper.Clamp(forceToThisPage, 0, max);
        this.MakePage(this.UnmultipliedWidth, player);
      }
      else
      {
        if (PageChange == 0)
          return;
        this.Page += PageChange;
        if (this.Page < 0)
          this.Page = max;
        if (this.Page > max)
          this.Page = 0;
        this.MakePage(this.UnmultipliedWidth, player);
      }
    }

    public bool UpdateGemeralEmployeeManager(
      float DeltaTime,
      Player player,
      out bool SwitchToManage)
    {
      SwitchToManage = false;
      this.bigpanel.UpdateDragger(player, ref this.Location, DeltaTime);
      if (this.bigpanel.UpdatePanelCloseButton(player, DeltaTime, this.Location))
        return true;
      if (this.screenstate == ParkStaffScreenState.Main)
      {
        this.ChangePage(this.simplearrows.UpdateSimpleArrowPageButtons(DeltaTime, player, this.Location), player);
        for (int index = 0; index < this.employeesummary.Count; ++index)
        {
          if (this.employeesummary[index].UpdateEmployeeByTypeSummary(player, DeltaTime, this.Location))
          {
            if (NotHereManager.ShouldDisplayNotHere(this.employeesummary[index].employeetype))
            {
              this.screenstate = ParkStaffScreenState.NotAvilableHere;
              this.notherecollection = new NotHereCollection(player, this.employeesummary[index].employeetype, this.BaseScale, this.UnmultipliedWidth, this.employeesummary[index].animalinframe);
              this.notherecollection.location.Y += this.bigpanel.GetFrameOffsetFromTop().Y;
            }
            else
            {
              this.SwitchToDetailViewForThis = this.employeesummary[index].employeetype;
              SwitchToManage = true;
            }
          }
        }
      }
      if (this.screenstate == ParkStaffScreenState.NotAvilableHere && this.notherecollection.UpdateNotHereCollection(player, DeltaTime, this.Location))
        this.screenstate = ParkStaffScreenState.Main;
      return false;
    }

    public void DrawGemeralEmployeeManager()
    {
      this.bigpanel.DrawBigBrownPanel(this.Location);
      if (this.screenstate == ParkStaffScreenState.NotAvilableHere)
      {
        this.notherecollection.DrawNotHereCollection(this.Location, AssetContainer.pointspritebatchTop05);
      }
      else
      {
        for (int index = 0; index < this.employeesummary.Count; ++index)
          this.employeesummary[index].DrawEmployeeByTypeSummary(this.Location, AssetContainer.pointspritebatchTop05);
        this.simplearrows.DrawSimpleArrowPageButtons(this.Location, AssetContainer.pointspritebatchTop05);
      }
    }
  }
}
