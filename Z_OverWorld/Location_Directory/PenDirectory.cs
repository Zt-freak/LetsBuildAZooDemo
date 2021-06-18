// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.Location_Directory.PenDirectory
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;
using TinyZoo.Z_TrashSystem;

namespace TinyZoo.Z_OverWorld.Location_Directory
{
  internal class PenDirectory
  {
    public int PenUID;
    private List<TrashDrop> Poop;

    public PenDirectory(int _PenUID)
    {
      this.PenUID = _PenUID;
      this.Poop = new List<TrashDrop>();
    }

    public void AddPoop(TrashDrop trashdrop)
    {
      this.Poop.Add(trashdrop);
      Console.WriteLine("Poop ADEED NOW: " + (object) this.Poop.Count);
    }

    public void RemovePoop(TrashDrop trashdrop)
    {
      for (int index = this.Poop.Count - 1; index > -1; --index)
      {
        if (this.Poop[index] == trashdrop)
        {
          this.Poop.RemoveAt(index);
          Console.WriteLine("Poop Removed REMAINING" + (object) this.Poop.Count);
          break;
        }
      }
    }

    public bool HasPoopHere() => this.Poop.Count > 0;

    public TrashDrop GetNextPoop(int ListID, ref bool ReachedEndOfList)
    {
      if (ListID < this.Poop.Count)
        return this.Poop[ListID];
      ReachedEndOfList = true;
      return (TrashDrop) null;
    }
  }
}
