// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_EditZone.SelectedZones.Patrol_Zones.WorkZoneRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.Graves;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Employees.WorkZonePane;

namespace TinyZoo.Z_EditZone.SelectedZones.Patrol_Zones
{
  internal class WorkZoneRenderer
  {
    private SelectionOutline selectionoutline;
    private DragAndDrawZone dragandrawmanager;

    public WorkZoneRenderer(WorkZone workzone) => this.selectionoutline = new SelectionOutline((Vector2Int) null, (TileInfo) null, (PrisonZone) null, (GraveYardBlockInfo) null, TILETYPE.Count, 0, workzone);

    public bool UpdatePatrolZoneRenderer(Player player, float DeltaTime)
    {
      this.selectionoutline.UpdateSelectionOutline();
      return false;
    }

    public void DrawPatrolZoneRenderer() => this.selectionoutline.DrawSelectionOutline();
  }
}
