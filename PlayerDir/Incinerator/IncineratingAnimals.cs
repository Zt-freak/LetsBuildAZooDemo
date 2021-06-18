// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Incinerator.IncineratingAnimals
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir.Incinerator
{
  internal class IncineratingAnimals
  {
    public DeadAnimal deadAnimal;

    public IncineratingAnimals(DeadAnimal _deadAnimal) => this.deadAnimal = _deadAnimal;

    public void StartNewDay()
    {
    }

    public void SaveIncineratingAnimals(Writer writer) => this.deadAnimal.SaveDeadAnimal(writer);

    public IncineratingAnimals(Reader reader, int VersionForLoad)
    {
      if (VersionForLoad > 9)
      {
        this.deadAnimal = new DeadAnimal(reader, VersionForLoad);
      }
      else
      {
        this.deadAnimal = new DeadAnimal();
        int _out1 = 0;
        int num1 = (int) reader.ReadInt("i", ref _out1);
        this.deadAnimal.animalType = (AnimalType) _out1;
        float _out2 = 0.0f;
        int num2 = (int) reader.ReadFloat("i", ref _out2);
        int num3 = (int) reader.ReadFloat("i", ref _out2);
      }
    }
  }
}
