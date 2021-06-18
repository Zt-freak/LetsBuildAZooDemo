// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.CRISPR.HybridAnimal
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir.CRISPR
{
  internal class HybridAnimal
  {
    public AnimalType HeadAnimalType;
    public int BodyVariant;
    public int HeadVariant;
    public int DayUnlocked;

    public HybridAnimal(
      AnimalType _HeadAnimalType,
      int _BodyVariant,
      int _HeadVariant,
      int _DayUnlocked)
    {
      this.HeadAnimalType = _HeadAnimalType;
      this.BodyVariant = _BodyVariant;
      this.HeadVariant = _HeadVariant;
      this.DayUnlocked = _DayUnlocked;
    }

    public void SaveHybridAnimal(Writer writer)
    {
      writer.WriteInt("c", (int) this.HeadAnimalType);
      writer.WriteInt("c", this.BodyVariant);
      writer.WriteInt("c", this.HeadVariant);
      writer.WriteInt("c", this.DayUnlocked);
    }

    public HybridAnimal(Reader reader)
    {
      int _out = 0;
      int num1 = (int) reader.ReadInt("c", ref _out);
      this.HeadAnimalType = (AnimalType) _out;
      int num2 = (int) reader.ReadInt("c", ref this.BodyVariant);
      int num3 = (int) reader.ReadInt("c", ref this.HeadVariant);
      int num4 = (int) reader.ReadInt("c", ref this.DayUnlocked);
    }
  }
}
