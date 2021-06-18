// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Processing.StoredAnimal
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir.Processing
{
  internal class StoredAnimal
  {
    public AnimalType animaltype;
    private int DaysSinceDeath;
    private int CorpseAgeOnCollection;

    public StoredAnimal(AnimalType _animaltype, int _DaysSinceDeath)
    {
      this.CorpseAgeOnCollection = _DaysSinceDeath;
      this.DaysSinceDeath = _DaysSinceDeath;
      this.animaltype = _animaltype;
    }

    public StoredAnimal(Reader reader)
    {
      int _out = 0;
      int num1 = (int) reader.ReadInt("a", ref _out);
      this.animaltype = (AnimalType) _out;
      int num2 = (int) reader.ReadInt("a", ref this.DaysSinceDeath);
      int num3 = (int) reader.ReadInt("a", ref this.CorpseAgeOnCollection);
    }

    public void SaveStoredAnimal(Writer writer)
    {
      writer.WriteInt("a", (int) this.animaltype);
      writer.WriteInt("a", this.DaysSinceDeath);
      writer.WriteInt("a", this.CorpseAgeOnCollection);
    }
  }
}
