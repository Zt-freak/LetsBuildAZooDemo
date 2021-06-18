// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_EditZone.SelectedZones.PatrolZones
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_EditZone.SelectedZones.Patrol_Zones;
using TinyZoo.Z_Employees.WorkZonePane;

namespace TinyZoo.Z_EditZone.SelectedZones
{
  internal class PatrolZones
  {
    private bool MouseDown;
    private DragAndDrawZone dragandrawmanager;
    private List<WorkZoneRenderer> workzones;

    public PatrolZones(WorkZoneInfo _workzoneinfo)
    {
      _workzoneinfo.workzones.Add(new WorkZone(new Vector2Int(10, 10), new Vector2Int(50, 50)));
      this.workzones = new List<WorkZoneRenderer>();
      for (int index = 0; index < _workzoneinfo.workzones.Count; ++index)
        this.workzones.Add(new WorkZoneRenderer(_workzoneinfo.workzones[index]));
    }

    public void UdatePatrolZones(Player player, float DeltaTime)
    {
      if (this.dragandrawmanager != null)
        this.dragandrawmanager.UpdateDragAndDrawZone(player);
      bool flag = false;
      for (int index = 0; index < this.workzones.Count; ++index)
        flag = this.workzones[index].UpdatePatrolZoneRenderer(player, DeltaTime);
      if ((double) player.player.touchinput.MultiTouchTouchLocations[0].X <= 0.0)
        return;
      if (!this.MouseDown)
      {
        if (flag || this.dragandrawmanager != null)
          return;
        this.MouseDown = true;
        this.dragandrawmanager = new DragAndDrawZone(player.player.touchinput.MultiTouchTouchLocations[0]);
      }
      else
        this.MouseDown = false;
    }

    public void DrawPatrolZones()
    {
      for (int index = 0; index < this.workzones.Count; ++index)
        this.workzones[index].DrawPatrolZoneRenderer();
      if (this.dragandrawmanager == null)
        return;
      this.dragandrawmanager.DrawDragAndDrawZone();
    }
  }
}
