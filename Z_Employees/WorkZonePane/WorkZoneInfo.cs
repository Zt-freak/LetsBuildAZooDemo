// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.WorkZonePane.WorkZoneInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;

namespace TinyZoo.Z_Employees.WorkZonePane
{
  internal class WorkZoneInfo
  {
    public int ZoneCap = 1;
    public List<Vector2Int> Paintzones;
    public List<WorkZone> workzones;
    public WorkZoneType workzonetype;
    public List<int> PenUIDs;
    public int LastPenVisitedUID;
    public bool Enabled;

    public WorkZoneInfo(int _ZoneCap = -1)
    {
      this.ZoneCap = _ZoneCap;
      this.Paintzones = new List<Vector2Int>();
      this.workzones = new List<WorkZone>();
      this.PenUIDs = new List<int>();
      this.LastPenVisitedUID = -1;
    }

    public void AddPenID(int PenUID)
    {
      this.Enabled = true;
      this.PenUIDs.Add(PenUID);
    }

    public bool IsThisenInMyWorkZone(int PenUID) => this.PenUIDs.Count > 0 && this.PenUIDs.Contains(PenUID);

    public void RecheckPens(Player player)
    {
      int num = this.Enabled ? 1 : 0;
    }

    public string GetNumericSummary() => this.ZoneCap == -1 ? (this.workzonetype == WorkZoneType.SingleZone ? string.Concat((object) this.workzones.Count) : string.Concat((object) this.PenUIDs.Count)) : (this.workzonetype == WorkZoneType.SingleZone ? this.workzones.Count.ToString() + "/" + (object) this.ZoneCap : this.PenUIDs.Count.ToString() + "/" + (object) this.ZoneCap);

    public int GetNextPen() => this.PenUIDs.Count == 0 || !this.Enabled ? -1 : this.GetPen(this.PenUIDs);

    private int GetPen(List<int> PENIDS)
    {
      for (int index = 0; index < PENIDS.Count; ++index)
      {
        if (PENIDS[index] == this.LastPenVisitedUID)
        {
          if (index < PENIDS.Count - 1)
          {
            this.LastPenVisitedUID = PENIDS[index + 1];
            return this.LastPenVisitedUID;
          }
          this.LastPenVisitedUID = PENIDS[0];
          return this.LastPenVisitedUID;
        }
      }
      this.LastPenVisitedUID = PENIDS[0];
      return this.LastPenVisitedUID;
    }

    public void SaveWorkZoneInfo(Writer writer)
    {
      writer.WriteInt("z", this.ZoneCap);
      writer.WriteInt("z", this.Paintzones.Count);
      for (int index = 0; index < this.Paintzones.Count; ++index)
        this.Paintzones[index].SaveVector2Int(writer);
      writer.WriteInt("z", this.workzones.Count);
      for (int index = 0; index < this.workzones.Count; ++index)
        this.workzones[index].SaveWorkZone(writer);
      writer.WriteInt("z", this.PenUIDs.Count);
      for (int index = 0; index < this.PenUIDs.Count; ++index)
        writer.WriteInt("z", this.PenUIDs[index]);
      writer.WriteInt("z", (int) this.workzonetype);
      if (this.workzonetype != WorkZoneType.Pens)
        return;
      writer.WriteInt("z", this.LastPenVisitedUID);
    }

    public WorkZoneInfo(Reader reader, int VersionForLoad)
    {
      int num1 = (int) reader.ReadInt("z", ref this.ZoneCap);
      int _out1 = 0;
      this.Paintzones = new List<Vector2Int>();
      this.workzones = new List<WorkZone>();
      this.PenUIDs = new List<int>();
      int num2 = (int) reader.ReadInt("z", ref _out1);
      for (int index = 0; index < _out1; ++index)
        this.Paintzones.Add(new Vector2Int(reader));
      int num3 = (int) reader.ReadInt("z", ref _out1);
      for (int index = 0; index < _out1; ++index)
        this.workzones.Add(new WorkZone(reader));
      int num4 = (int) reader.ReadInt("z", ref _out1);
      for (int index = 0; index < _out1; ++index)
      {
        int _out2 = 0;
        int num5 = (int) reader.ReadInt("z", ref _out2);
        this.PenUIDs.Add(_out2);
      }
      int num6 = (int) reader.ReadInt("z", ref _out1);
      if (VersionForLoad > 6)
      {
        this.workzonetype = (WorkZoneType) _out1;
        if (this.workzonetype == WorkZoneType.Pens)
        {
          int num5 = (int) reader.ReadInt("z", ref this.LastPenVisitedUID);
        }
      }
      Z_GameFlags.RecheckZooKeeperZones = true;
    }
  }
}
