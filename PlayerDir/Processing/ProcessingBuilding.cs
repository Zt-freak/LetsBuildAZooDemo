// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Processing.ProcessingBuilding
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Employees.WorkZonePane;

namespace TinyZoo.PlayerDir.Processing
{
  internal class ProcessingBuilding
  {
    public List<CurrentProcess> ActiveProcesses;
    public List<StoredAnimal> storeddeadthings;
    public int UID;
    public WorkZoneInfo workzoneinfo;

    public ProcessingBuilding(int _UID)
    {
      this.storeddeadthings = new List<StoredAnimal>();
      this.ActiveProcesses = new List<CurrentProcess>();
      this.UID = _UID;
      this.workzoneinfo = new WorkZoneInfo();
      this.workzoneinfo.workzonetype = WorkZoneType.Pens;
      this.workzoneinfo.Enabled = false;
    }

    public void SoldThisBuilding(Player player)
    {
    }

    public void AddDeadAnimalToBuilding(AnimalType animaltype, int DaysSinceDeath) => this.storeddeadthings.Add(new StoredAnimal(animaltype, DaysSinceDeath));

    public void SaveProcessingBuilding(Writer writer)
    {
      writer.WriteInt("a", this.UID);
      writer.WriteInt("a", this.ActiveProcesses.Count);
      for (int index = 0; index < this.ActiveProcesses.Count; ++index)
        this.ActiveProcesses[index].SaveActiveProcesses(writer);
      writer.WriteInt("a", this.storeddeadthings.Count);
      for (int index = 0; index < this.storeddeadthings.Count; ++index)
        this.storeddeadthings[index].SaveStoredAnimal(writer);
      this.workzoneinfo.SaveWorkZoneInfo(writer);
    }

    public ProcessingBuilding(Reader reader, int VersionForLoad)
    {
      int num1 = (int) reader.ReadInt("a", ref this.UID);
      int _out = 0;
      int num2 = (int) reader.ReadInt("a", ref _out);
      this.ActiveProcesses = new List<CurrentProcess>();
      for (int index = 0; index < _out; ++index)
        this.ActiveProcesses.Add(new CurrentProcess(reader));
      int num3 = (int) reader.ReadInt("a", ref _out);
      this.storeddeadthings = new List<StoredAnimal>();
      for (int index = 0; index < _out; ++index)
        this.storeddeadthings.Add(new StoredAnimal(reader));
      this.workzoneinfo = new WorkZoneInfo(reader, VersionForLoad);
    }
  }
}
