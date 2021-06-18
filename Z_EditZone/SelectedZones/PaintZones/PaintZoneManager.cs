// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_EditZone.SelectedZones.PaintZones.PaintZoneManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_Employees.WorkZonePane;

namespace TinyZoo.Z_EditZone.SelectedZones.PaintZones
{
  internal class PaintZoneManager
  {
    internal static int PaintZOneSIze = 4;
    private List<PaintZoneObject> paintzones;
    private bool IsPainintingOrDeleting;
    private bool IsDelete;
    private WorkZoneInfo REF_workzone;

    public PaintZoneManager(WorkZoneInfo workzone)
    {
      this.REF_workzone = workzone;
      this.paintzones = new List<PaintZoneObject>();
      for (int index = 0; index < workzone.Paintzones.Count; ++index)
        this.paintzones.Add(new PaintZoneObject(workzone.Paintzones[index], PaintZoneManager.PaintZOneSIze));
    }

    public void UpdatePaintZoneManager(Player player, float DeltaTime)
    {
      Z_GameFlags.ForceRightMouseDrag = true;
      if ((double) player.player.touchinput.MultiTouchTouchLocations[0].X > 0.0)
      {
        if (!this.IsPainintingOrDeleting)
        {
          this.IsPainintingOrDeleting = true;
          int ArrayIndex = this.IsTouchOverlappingATile(player.player.touchinput.MultiTouchTouchLocations[0]);
          if (ArrayIndex > -1)
          {
            this.IsDelete = true;
            this.DeleteTile(ArrayIndex);
          }
          else
          {
            this.IsDelete = false;
            Vector2Int spaceToTileLocation = TileMath.GetScreenSPaceToTileLocation(player.player.touchinput.MultiTouchTouchLocations[0]);
            spaceToTileLocation.X /= PaintZoneManager.PaintZOneSIze;
            spaceToTileLocation.Y /= PaintZoneManager.PaintZOneSIze;
            spaceToTileLocation.X *= PaintZoneManager.PaintZOneSIze;
            spaceToTileLocation.Y *= PaintZoneManager.PaintZOneSIze;
            this.AddTile(spaceToTileLocation);
          }
        }
        else if (this.IsDelete)
        {
          int ArrayIndex = this.IsTouchOverlappingATile(player.player.touchinput.MultiTouchTouchLocations[0]);
          if (ArrayIndex <= -1)
            return;
          this.IsDelete = true;
          this.DeleteTile(ArrayIndex);
        }
        else
        {
          Vector2Int spaceToTileLocation = TileMath.GetScreenSPaceToTileLocation(player.player.touchinput.MultiTouchTouchLocations[0]);
          spaceToTileLocation.X /= PaintZoneManager.PaintZOneSIze;
          spaceToTileLocation.Y /= PaintZoneManager.PaintZOneSIze;
          spaceToTileLocation.X *= PaintZoneManager.PaintZOneSIze;
          spaceToTileLocation.Y *= PaintZoneManager.PaintZOneSIze;
          this.AddTile(spaceToTileLocation);
        }
      }
      else
        this.IsPainintingOrDeleting = false;
    }

    private void DeleteTile(int ArrayIndex)
    {
      this.REF_workzone.Paintzones.RemoveAt(ArrayIndex);
      this.paintzones.RemoveAt(ArrayIndex);
    }

    private void AddTile(Vector2Int Location)
    {
      for (int index = 0; index < this.REF_workzone.Paintzones.Count; ++index)
      {
        if (this.REF_workzone.Paintzones[index].X == Location.X && this.REF_workzone.Paintzones[index].Y == Location.Y)
          return;
      }
      this.REF_workzone.Paintzones.Add(Location);
      this.paintzones.Add(new PaintZoneObject(this.REF_workzone.Paintzones[this.REF_workzone.Paintzones.Count - 1], PaintZoneManager.PaintZOneSIze));
    }

    private int IsTouchOverlappingATile(Vector2 TouchLocation)
    {
      Vector2Int spaceToTileLocation = TileMath.GetScreenSPaceToTileLocation(TouchLocation);
      for (int index = 0; index < this.REF_workzone.Paintzones.Count; ++index)
      {
        if (spaceToTileLocation.X >= this.REF_workzone.Paintzones[index].X && spaceToTileLocation.X < this.REF_workzone.Paintzones[index].X + PaintZoneManager.PaintZOneSIze && (spaceToTileLocation.Y >= this.REF_workzone.Paintzones[index].Y && spaceToTileLocation.Y < this.REF_workzone.Paintzones[index].Y + PaintZoneManager.PaintZOneSIze))
          return index;
      }
      return -1;
    }

    public void DrawPaintZoneManager()
    {
      for (int index = 0; index < this.paintzones.Count; ++index)
        this.paintzones[index].DrawPaintZoneObject();
    }
  }
}
