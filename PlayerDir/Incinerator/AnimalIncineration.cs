// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Incinerator.AnimalIncineration
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;

namespace TinyZoo.PlayerDir.Incinerator
{
  internal class AnimalIncineration
  {
    private List<IncineratorBuilding> incinerationBuildings;

    public AnimalIncineration() => this.incinerationBuildings = new List<IncineratorBuilding>();

    public void StartNewDay()
    {
      for (int index = 0; index < this.incinerationBuildings.Count; ++index)
        this.incinerationBuildings[index].StartNewDay();
    }

    public void AddIncinerationBuilding(int UID) => this.incinerationBuildings.Add(new IncineratorBuilding(UID));

    public void RemoveIncinerationBuilding(int UID)
    {
      for (int index = this.incinerationBuildings.Count - 1; index > -1; --index)
      {
        if (this.incinerationBuildings[index].UID == UID)
        {
          this.incinerationBuildings.RemoveAt(index);
          break;
        }
      }
    }

    public IncineratorBuilding GetIncinerationBuilding(int UID)
    {
      for (int index = 0; index < this.incinerationBuildings.Count; ++index)
      {
        if (this.incinerationBuildings[index].UID == UID)
          return this.incinerationBuildings[index];
      }
      return (IncineratorBuilding) null;
    }

    public void SaveAnimalIncineration(Writer writer)
    {
      writer.WriteInt("i", this.incinerationBuildings.Count);
      for (int index = 0; index < this.incinerationBuildings.Count; ++index)
        this.incinerationBuildings[index].SaveIncineratorBuilding(writer);
    }

    public AnimalIncineration(Reader reader, int VersionForLoad)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("i", ref _out);
      this.incinerationBuildings = new List<IncineratorBuilding>();
      for (int index = 0; index < _out; ++index)
        this.incinerationBuildings.Add(new IncineratorBuilding(reader, VersionForLoad));
    }
  }
}
