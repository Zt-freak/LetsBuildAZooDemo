// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.facilities.Z_Facilities
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_Notification;

namespace TinyZoo.PlayerDir.Layout.facilities
{
  internal class Z_Facilities
  {
    public List<Facility> WaterPumps;

    public Z_Facilities() => this.WaterPumps = new List<Facility>();

    public void RemoveWaterPump(Vector2Int Location, Player player)
    {
      for (int index = this.WaterPumps.Count - 1; index > -1; --index)
      {
        if (this.WaterPumps[index].Location.CompareMatches(Location))
        {
          this.WaterPumps.RemoveAt(index);
          Z_GameFlags.MustRebuildWaterMap = true;
          Z_GameFlags.DidSomethingWithWater = true;
          OverWorldManager.heatmapmanager.DoubleCheckWaterSetUp(player);
          Z_NotificationManager.AddPenIDTorecountWater(-1);
          Z_NotificationManager.RescrubWater = true;
          break;
        }
      }
    }

    public void AddPumpStation(Vector2Int Location, Player player)
    {
      Z_NotificationManager.RescrubWater = true;
      Z_GameFlags.MustRebuildWaterMap = true;
      Z_GameFlags.DidSomethingWithWater = true;
      Z_NotificationManager.AddPenIDTorecountWater(-1);
      this.WaterPumps.Add(new Facility(Location));
      if (player.prisonlayout == null)
        return;
      OverWorldManager.heatmapmanager.DoubleCheckWaterSetUp(player);
    }

    public Z_Facilities(Reader reader)
    {
      Z_GameFlags.MustRebuildWaterMap = true;
      Z_GameFlags.DidSomethingWithWater = true;
      int _out = 0;
      int num = (int) reader.ReadInt("x", ref _out);
      this.WaterPumps = new List<Facility>();
      for (int index = 0; index < _out; ++index)
        this.WaterPumps.Add(new Facility(reader));
    }

    public void SaveZ_Facilities(Writer writer)
    {
      writer.WriteInt("x", this.WaterPumps.Count);
      for (int index = 0; index < this.WaterPumps.Count; ++index)
        this.WaterPumps[index].SaveWaterPump(writer);
    }
  }
}
