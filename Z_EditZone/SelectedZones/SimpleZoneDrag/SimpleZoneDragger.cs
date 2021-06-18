// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_EditZone.SelectedZones.SimpleZoneDrag.SimpleZoneDragger
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_Employees.WorkZonePane;

namespace TinyZoo.Z_EditZone.SelectedZones.SimpleZoneDrag
{
  internal class SimpleZoneDragger
  {
    private bool WasDraging;
    private ZonePainter CurrentZone;
    private List<ZonePainter> zonepainter;
    private int MaxZones;

    public SimpleZoneDragger(WorkZoneInfo _workzoneinfo, int Max)
    {
      this.MaxZones = Max;
      this.zonepainter = new List<ZonePainter>();
      for (int index = 0; index < _workzoneinfo.workzones.Count; ++index)
        this.zonepainter.Add(new ZonePainter(_workzoneinfo.workzones[index]));
    }

    public int GetNumberOfZones() => this.zonepainter.Count;

    public void UpdateSimpleZoneDragger(
      Player player,
      float DeltaTime,
      WorkZoneInfo REF_workzoneinfo,
      out bool MakeRed)
    {
      MakeRed = false;
      Z_GameFlags.ForceRightMouseDrag = true;
      if ((double) player.player.touchinput.MultiTouchTouchLocations[0].X >= 0.0)
      {
        if (!this.WasDraging)
        {
          this.WasDraging = true;
          bool flag = false;
          for (int index = 0; index < this.zonepainter.Count; ++index)
          {
            if (this.zonepainter[index].Overlaps(RenderMath.TranslateScreenSpaceToWorldSpace(player.player.touchinput.MultiTouchTouchLocations[0])) && !flag)
            {
              flag = true;
              this.CurrentZone = new ZonePainter(player);
              this.zonepainter[index] = this.CurrentZone;
            }
          }
          if (!flag)
          {
            if (this.zonepainter.Count < this.MaxZones)
            {
              this.CurrentZone = new ZonePainter(player);
              this.zonepainter.Add(this.CurrentZone);
              this.Remake(REF_workzoneinfo);
            }
            else
            {
              MakeRed = true;
              this.CurrentZone = (ZonePainter) null;
            }
          }
        }
        if (this.CurrentZone == null)
          return;
        this.CurrentZone.UpdateZonePainter(player, DeltaTime);
      }
      else
      {
        bool flag = player.inputmap.RightMousReleaseClick || player.inputmap.ReleasedThisFrame[13];
        if (this.WasDraging)
        {
          int count = this.zonepainter.Count;
          int maxZones = this.MaxZones;
          this.Remake(REF_workzoneinfo);
          this.WasDraging = false;
        }
        for (int index = this.zonepainter.Count - 1; index > -1; --index)
        {
          this.zonepainter[index].UpdateMouseOver(player.inputmap.PointerLocation);
          if (this.zonepainter[index].MouseOver & flag)
          {
            flag = false;
            this.zonepainter.RemoveAt(index);
            this.Remake(REF_workzoneinfo);
            if (this.zonepainter.Count >= this.MaxZones)
              MakeRed = true;
          }
        }
      }
    }

    private void Remake(WorkZoneInfo REF_workzoneinfo)
    {
      REF_workzoneinfo.workzones = new List<WorkZone>();
      for (int index = 0; index < this.zonepainter.Count; ++index)
        REF_workzoneinfo.workzones.Add(new WorkZone(this.zonepainter[index]));
    }

    public void DrawSimpleZoneDragger()
    {
      if (this.zonepainter == null)
        return;
      for (int index = 0; index < this.zonepainter.Count; ++index)
        this.zonepainter[index].DrawZonePainter();
    }
  }
}
