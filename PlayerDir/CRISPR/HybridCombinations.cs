// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.CRISPR.HybridCombinations
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;

namespace TinyZoo.PlayerDir.CRISPR
{
  internal class HybridCombinations
  {
    public List<HybridAnimal> combinations;

    public HybridCombinations() => this.combinations = new List<HybridAnimal>();

    public void SaveHybridCombinations(Writer writer)
    {
      writer.WriteInt("a", this.combinations.Count);
      for (int index = 0; index < this.combinations.Count; ++index)
        this.combinations[index].SaveHybridAnimal(writer);
    }

    public HybridCombinations(Reader reader)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("a", ref _out);
      this.combinations = new List<HybridAnimal>();
      for (int index = 0; index < _out; ++index)
        this.combinations.Add(new HybridAnimal(reader));
    }
  }
}
