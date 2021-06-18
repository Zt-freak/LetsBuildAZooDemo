// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.ChildrenBred
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir
{
  internal class ChildrenBred
  {
    public ChildArray[] children;

    public ChildrenBred() => this.Create();

    public bool HasThisVariantBeenBredEver(AnimalType animaltype, int Variant, bool IsAGirl) => this.children[(int) animaltype].HasThisVariantBeenBredEver(Variant, IsAGirl);

    public bool AddedAChild(AnimalType animaltype, int Variant = -1, int Total = -1, bool IsAGirl = false) => this.children[(int) animaltype].AddChild(Variant, Total, IsAGirl);

    private void Create()
    {
      this.children = new ChildArray[70];
      for (int index = 0; index < this.children.Length; ++index)
        this.children[index] = new ChildArray((AnimalType) index);
    }

    public ChildrenBred(Reader reader)
    {
      this.Create();
      int _out = 0;
      int num = (int) reader.ReadInt("c", ref _out);
      for (int index = 0; index < _out; ++index)
        this.children[index] = new ChildArray(reader);
    }

    public void SaveChildrenBred(Writer writer)
    {
      writer.WriteInt("c", this.children.Length);
      for (int index = 0; index < this.children.Length; ++index)
        this.children[index].SaveChildArray(writer);
    }
  }
}
