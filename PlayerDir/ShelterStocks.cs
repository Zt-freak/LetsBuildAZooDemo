// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.ShelterStocks
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;

namespace TinyZoo.PlayerDir
{
  internal class ShelterStocks
  {
    public List<ShelterAnimal> shelteredanimal;

    public ShelterStocks(Player player)
    {
      this.shelteredanimal = new List<ShelterAnimal>();
      this.shelteredanimal.Add(new ShelterAnimal(player));
      this.shelteredanimal.Add(new ShelterAnimal(player));
    }

    public void StartNewDay(Player player)
    {
      int num = TinyZoo.Game1.Rnd.Next(0, 2);
      if (this.shelteredanimal.Count > 0 && TinyZoo.Game1.Rnd.Next(0, 5) == 0)
        this.shelteredanimal.RemoveAt(TinyZoo.Game1.Rnd.Next(0, this.shelteredanimal.Count));
      if (TinyZoo.Game1.Rnd.Next(0, 5) == 0)
        ++num;
      for (int index = 0; index < num; ++index)
      {
        if (this.shelteredanimal.Count < 5)
          this.shelteredanimal.Add(new ShelterAnimal(player));
      }
    }

    public ShelterStocks(Reader reader)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("f", ref _out);
      this.shelteredanimal = new List<ShelterAnimal>();
      for (int index = 0; index < _out; ++index)
        this.shelteredanimal.Add(new ShelterAnimal(reader));
    }

    public void SaveShelterStocks(Writer writer)
    {
      writer.WriteInt("f", this.shelteredanimal.Count);
      for (int index = 0; index < this.shelteredanimal.Count; ++index)
        this.shelteredanimal[index].SaveShelterAnimal(writer);
    }
  }
}
