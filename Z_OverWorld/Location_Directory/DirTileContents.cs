// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.Location_Directory.DirTileContents
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.Z_OverWorld.AvatarUI.Selection;
using TinyZoo.Z_TrashSystem;

namespace TinyZoo.Z_OverWorld.Location_Directory
{
  internal class DirTileContents
  {
    private List<TrashDrop> trashdrops;
    private List<TrashDrop> Poops;

    public void AddTrash(TrashDrop trashdrop)
    {
      if (this.trashdrops == null)
        this.trashdrops = new List<TrashDrop>();
      this.trashdrops.Add(trashdrop);
    }

    public void AddPoop(TrashDrop trashdrop)
    {
      if (this.Poops == null)
        this.Poops = new List<TrashDrop>();
      this.Poops.Add(trashdrop);
    }

    public void RemoveTrash(TrashDrop trashdrop)
    {
      if (this.trashdrops != null)
        this.trashdrops.Remove(trashdrop);
      if (this.trashdrops == null || this.trashdrops.Count != 0)
        return;
      this.trashdrops = (List<TrashDrop>) null;
    }

    public CURSOR_ACTIONTYPE GetCursorThing() => this.trashdrops != null && this.trashdrops.Count > 0 ? CURSOR_ACTIONTYPE.CollectTrash : CURSOR_ACTIONTYPE.Count;
  }
}
