// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.BeamLayout
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout.CellBlocks;

namespace TinyZoo.PlayerDir.Layout
{
  internal class BeamLayout
  {
    public List<BeamEntry> beamentries;

    public BeamLayout() => this.beamentries = new List<BeamEntry>();

    public void SaveBeamLayout(Writer writer)
    {
      writer.WriteInt("e", this.beamentries.Count);
      for (int index = 0; index < this.beamentries.Count; ++index)
        this.beamentries[index].SaveBeamEntry(writer);
    }

    public BeamLayout(Reader reader)
    {
      int _out = 0;
      this.beamentries = new List<BeamEntry>();
      int num = (int) reader.ReadInt("e", ref _out);
      for (int index = 0; index < _out; ++index)
        this.beamentries.Add(new BeamEntry(reader));
    }
  }
}
