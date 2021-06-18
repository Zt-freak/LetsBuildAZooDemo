// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.ChildArray
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir
{
  internal class ChildArray
  {
    public int[] GirlsBorn;
    public int[] BoysBorn;

    public ChildArray(AnimalType Animal) => this.Create();

    public bool HasThisVariantBeenBredEver(int Variant, bool IsAGirl) => IsAGirl ? this.GirlsBorn[Variant] >= 0 : this.BoysBorn[Variant] >= 0;

    public bool AddChild(int Variant = -1, int Total = -1, bool IsAGirl = false)
    {
      bool flag = false;
      if (IsAGirl)
      {
        if (this.GirlsBorn[Variant] == -1)
        {
          this.GirlsBorn[Variant] = 0;
          flag = true;
        }
        this.GirlsBorn[Variant] += Total;
      }
      else
      {
        if (this.BoysBorn[Variant] == -1)
        {
          this.BoysBorn[Variant] = 0;
          flag = true;
        }
        this.BoysBorn[Variant] += Total;
      }
      return flag;
    }

    private void Create()
    {
      this.GirlsBorn = new int[10];
      this.BoysBorn = new int[10];
      for (int index = 0; index < this.GirlsBorn.Length; ++index)
      {
        this.GirlsBorn[index] = -1;
        this.BoysBorn[index] = -1;
      }
    }

    public ChildArray(Reader reader)
    {
      this.Create();
      int _out = 0;
      int num1 = (int) reader.ReadInt("c", ref _out);
      for (int index = 0; index < _out; ++index)
      {
        int num2 = (int) reader.ReadInt("c", ref this.GirlsBorn[index]);
        int num3 = (int) reader.ReadInt("c", ref this.BoysBorn[index]);
      }
    }

    public void SaveChildArray(Writer writer)
    {
      writer.WriteInt("c", this.GirlsBorn.Length);
      for (int index = 0; index < this.GirlsBorn.Length; ++index)
      {
        writer.WriteInt("c", this.GirlsBorn[index]);
        writer.WriteInt("c", this.BoysBorn[index]);
      }
    }
  }
}
