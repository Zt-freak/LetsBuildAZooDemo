// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items.Poop
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items
{
  internal class Poop
  {
    public List<Poo> poo;

    public Poop() => this.poo = new List<Poo>();

    public void AddPoop(AnimalType animaltype) => this.poo.Add(new Poo(animaltype));

    public void StartNewDay()
    {
      for (int index = 0; index < this.poo.Count; ++index)
        ++this.poo[index].DaysInPen;
    }

    public void CleanPoop()
    {
    }

    public Poop(Reader reader)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("p", ref _out);
      this.poo = new List<Poo>();
      for (int index = 0; index < _out; ++index)
        this.poo.Add(new Poo(reader));
    }

    public void SavePoop(Writer writer)
    {
      writer.WriteInt("p", this.poo.Count);
      for (int index = 0; index < this.poo.Count; ++index)
        this.poo[index].SavePoo(writer);
    }
  }
}
