// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_EditZone.ZoneEditManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TinyZoo.Z_EditZone.EditUI;
using TinyZoo.Z_EditZone.SelectedZones;
using TinyZoo.Z_Employees.WorkZonePane;

namespace TinyZoo.Z_EditZone
{
  internal class ZoneEditManager
  {
    private WorkZoneInfo workzoneinfo;
    private SelectedZoneManager selectedzonemanager;
    private UI_ZoneEditorPanel uizoneedit;
    private EmployeeType employeetype;

    public ZoneEditManager(
      WorkZoneInfo _workzoneinfo,
      Player player,
      TILETYPE buildingtype = TILETYPE.Count,
      SelectedAndSellManager _selectedtileandsell = null,
      EmployeeType _employeetype = EmployeeType.None)
    {
      this.employeetype = _employeetype;
      this.workzoneinfo = _workzoneinfo;
      this.uizoneedit = new UI_ZoneEditorPanel(this.workzoneinfo, Z_GameFlags.GetBaseScaleForUI(), buildingtype, _selectedtileandsell);
      this.selectedzonemanager = new SelectedZoneManager(_workzoneinfo, player);
    }

    public void UpdateZoneEditManager(Player player, float DeltaTime)
    {
      if (!this.uizoneedit.CheckMouseOver(player, Vector2.Zero))
      {
        bool MakeRed;
        this.selectedzonemanager.UpdateSelectedZoneManager(player, DeltaTime, out MakeRed);
        if (MakeRed)
          this.uizoneedit.SetRed();
      }
      this.uizoneedit.UpdateUI_ZoneEditorPanel(player, DeltaTime, Vector2.Zero);
    }

    public void DrawZoneEditManager()
    {
      this.selectedzonemanager.DrawSelectedZoneManager();
      this.uizoneedit.DrawUI_ZoneEditorPanel(AssetContainer.pointspritebatchTop05, Vector2.Zero);
    }
  }
}
