// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CustomizePen.Pen_SelectedPenManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.OverWorld.OverWorldRenderer;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BarMenu.Pen.AddDeco;
using TinyZoo.Z_BarMenu.Pen.Animals;
using TinyZoo.Z_BarMenu.Pen.EditPen;
using TinyZoo.Z_BarMenu.Pen.Staff;
using TinyZoo.Z_BarMenu.Pen.ViewDeco;
using TinyZoo.Z_PenInfo;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.Z_CustomizePen
{
  internal class Pen_SelectedPenManager
  {
    private PrisonZone prisonzone;
    internal static BuildingManageButton StateToStartOn;
    internal static Vector2Int SelectedTileLocation;
    private PenVideState ScreenState;
    internal static bool Reconstruct;
    private ModifyPenMenu editpen;
    private AnimalsMenu animals;
    private AddDecoManager additems;
    private ViewDecoManager viewdeco;
    private Pen_StaffManager penstaff;

    public Pen_SelectedPenManager(Player player) => this.Create(player);

    public bool IsCurrentlyMovingThisPen(int PenUID) => this.ScreenState == PenVideState.EditPen && this.editpen.IsCurrentlyMovingThisPen(PenUID);

    private void Create(Player player)
    {
      Pen_SelectedPenManager.Reconstruct = false;
      this.prisonzone = !Z_GameFlags.SelectedPrisonZoneisFarm ? player.prisonlayout.GetThisCellBlock(Z_GameFlags.SelectedPrisonZoneUID) : player.farms.GetThisFarmFieldByUID(Z_GameFlags.SelectedPrisonZoneUID);
      switch (Pen_SelectedPenManager.StateToStartOn)
      {
        case BuildingManageButton.Pen_EditPen:
          Z_DebugFlags.TempNewBuildingMenu = false;
          this.editpen = new ModifyPenMenu(player);
          this.ScreenState = PenVideState.EditPen;
          break;
        case BuildingManageButton.Pen_AddItemsToPen:
          this.additems = new AddDecoManager(player);
          Z_DebugFlags.TempNewBuildingMenu = false;
          this.ScreenState = PenVideState.AddItemsToPen;
          break;
        case BuildingManageButton.Pen_ItemsViewEdit:
          this.ScreenState = PenVideState.ViewItems;
          Z_DebugFlags.TempNewBuildingMenu = false;
          this.viewdeco = new ViewDecoManager(player, this.prisonzone);
          break;
        case BuildingManageButton.Pen_Animals:
          this.animals = new AnimalsMenu(player, this.prisonzone);
          this.ScreenState = PenVideState.Animals;
          break;
        case BuildingManageButton.GetStaff:
          this.penstaff = new Pen_StaffManager(player, this.prisonzone);
          this.ScreenState = PenVideState.Staff;
          break;
      }
    }

    public bool IsMouseOverButton(Player player)
    {
      switch (this.ScreenState)
      {
        case PenVideState.EditPen:
          return this.editpen.IsMouseOverButton(player);
        case PenVideState.AddItemsToPen:
          return this.additems.IsMouseOverButton(player);
        case PenVideState.ViewItems:
          return this.viewdeco.IsMouseOverButton(player);
        case PenVideState.Animals:
          return this.animals.IsMouseOverButton(player);
        case PenVideState.Staff:
          return this.penstaff.IsMouseOverButton(player);
        default:
          return false;
      }
    }

    public bool UpdatePen_SelectedPenManager(
      float DeltaTime,
      Player player,
      OverWorldEnvironmentManager overworldenvironment,
      WallsAndFloorsManager wallsandfloors,
      OverworldBuildManager buildmanager)
    {
      if (Pen_SelectedPenManager.Reconstruct)
        this.Create(player);
      bool flag = false;
      switch (this.ScreenState)
      {
        case PenVideState.EditPen:
          flag = this.editpen.UpdateModifyPenMenu(player, DeltaTime, this.prisonzone, overworldenvironment, Pen_SelectedPenManager.SelectedTileLocation);
          break;
        case PenVideState.AddItemsToPen:
          flag = this.additems.UpdateAddDecoManager(player, DeltaTime, wallsandfloors, buildmanager, this.prisonzone);
          break;
        case PenVideState.ViewItems:
          flag = this.viewdeco.UpdateViewDecoManager(player, DeltaTime, this.prisonzone, wallsandfloors, buildmanager, overworldenvironment);
          break;
        case PenVideState.Animals:
          flag = this.animals.UpdateAnimalsMenu(player, DeltaTime, this.prisonzone);
          break;
        case PenVideState.Staff:
          flag = this.penstaff.UpdateStaffManager(player, DeltaTime, this.prisonzone);
          break;
      }
      if (!flag)
        return false;
      PenInfoManager.RemakeThisBarNow = true;
      if (OverWorldManager.overworldstate != OverWOrldState.Build)
        OverWorldManager.overworldstate = OverWOrldState.MainMenu;
      Z_DebugFlags.TempNewBuildingMenu = true;
      return true;
    }

    public void DrawPen_SelectedPenManager(Player player)
    {
      if (Pen_SelectedPenManager.Reconstruct)
        return;
      switch (this.ScreenState)
      {
        case PenVideState.EditPen:
          this.editpen.DrawModifyPenMenu(player);
          break;
        case PenVideState.AddItemsToPen:
          this.additems.DrawAddDecoManager(player);
          break;
        case PenVideState.ViewItems:
          this.viewdeco.DrawViewDecoManager(player);
          break;
        case PenVideState.Animals:
          this.animals.DrawAnimalsMenu(player);
          break;
        case PenVideState.Staff:
          this.penstaff.DrawStaffManager(player);
          break;
      }
    }
  }
}
