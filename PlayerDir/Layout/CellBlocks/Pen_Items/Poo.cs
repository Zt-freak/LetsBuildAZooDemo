// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items.Poo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items
{
  internal class Poo
  {
    public AnimalType animal;
    public int DaysInPen;
    private Vector2 WorldLocation;

    public Poo(AnimalType _animal)
    {
      this.animal = _animal;
      this.DaysInPen = 0;
    }

    public void SavePoo(Writer writer)
    {
      writer.WriteInt("p", (int) this.animal);
      writer.WriteInt("p", this.DaysInPen);
    }

    public Poo(Reader reader)
    {
      int _out = 0;
      int num1 = (int) reader.ReadInt("p", ref _out);
      this.animal = (AnimalType) _out;
      int num2 = (int) reader.ReadInt("p", ref this.DaysInPen);
    }
  }
}
