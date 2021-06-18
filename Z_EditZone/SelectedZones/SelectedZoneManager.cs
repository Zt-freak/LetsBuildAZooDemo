// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_EditZone.SelectedZones.SelectedZoneManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_EditZone.SelectedZones.PaintZones;
using TinyZoo.Z_EditZone.SelectedZones.SimpleZoneDrag;
using TinyZoo.Z_Employees.WorkZonePane;

namespace TinyZoo.Z_EditZone.SelectedZones
{
  internal class SelectedZoneManager
  {
    private SimpleZoneDragger simplezondragger;
    private SelectedPens selectedpens;
    private PatrolZones patrolzones;
    private PaintZoneManager paintzonemanager;
    private WorkZoneInfo REF_workzoneinfo;

    public SelectedZoneManager(WorkZoneInfo _workzoneinfo, Player player)
    {
      this.REF_workzoneinfo = _workzoneinfo;
      if (this.REF_workzoneinfo.workzonetype == WorkZoneType.Pens)
        this.selectedpens = new SelectedPens(_workzoneinfo, player);
      else if (this.REF_workzoneinfo.workzonetype == WorkZoneType.Painted)
        this.paintzonemanager = new PaintZoneManager(_workzoneinfo);
      else
        this.simplezondragger = new SimpleZoneDragger(_workzoneinfo, this.REF_workzoneinfo.ZoneCap);
    }

    public int GetNumberOfZones() => this.REF_workzoneinfo.workzonetype == WorkZoneType.Pens ? this.selectedpens.GetNumberOfZones() : this.simplezondragger.GetNumberOfZones();

    public void UpdateSelectedZoneManager(Player player, float DeltaTime, out bool MakeRed)
    {
      MakeRed = false;
      if (this.REF_workzoneinfo.workzonetype == WorkZoneType.Pens)
        this.selectedpens.UpdateSelectedPens(player);
      else if (this.REF_workzoneinfo.workzonetype == WorkZoneType.Painted)
        this.paintzonemanager.UpdatePaintZoneManager(player, DeltaTime);
      else
        this.simplezondragger.UpdateSimpleZoneDragger(player, DeltaTime, this.REF_workzoneinfo, out MakeRed);
    }

    public void DrawSelectedZoneManager()
    {
      if (this.REF_workzoneinfo.workzonetype == WorkZoneType.Pens)
        this.selectedpens.DrawSelectedPens();
      else if (this.REF_workzoneinfo.workzonetype == WorkZoneType.Painted)
        this.paintzonemanager.DrawPaintZoneManager();
      else
        this.simplezondragger.DrawSimpleZoneDragger();
    }
  }
}
