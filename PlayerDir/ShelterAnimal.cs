// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.ShelterAnimal
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir
{
  internal class ShelterAnimal
  {
    public AnimalType animal;
    public int NameId;
    public bool IsAGirl;
    public float LifeProgress;
    public int variant;

    public ShelterAnimal(Player player)
    {
      this.animal = (AnimalType) TinyZoo.Game1.Rnd.Next(0, 3);
      this.LifeProgress = MathStuff.getRandomFloat(0.0f, 1f);
      if (Z_DebugFlags.IsBetaVersion)
      {
        List<AnimalType> animalTypeList = new List<AnimalType>();
        animalTypeList.Add(AnimalType.Rabbit);
        animalTypeList.Add(AnimalType.Pig);
        if (player.Stats.variantsfound.GetTotalVaiantsFound(AnimalType.Horse) > 0)
          animalTypeList.Add(AnimalType.Horse);
        if (player.Stats.variantsfound.GetTotalVaiantsFound(AnimalType.Capybara) > 0)
        {
          animalTypeList.Add(AnimalType.Capybara);
          animalTypeList.Add(AnimalType.Goose);
        }
        if (player.Stats.variantsfound.GetTotalVaiantsFound(AnimalType.Snake) > 0)
        {
          animalTypeList.Add(AnimalType.Snake);
          animalTypeList.Add(AnimalType.Alpaca);
        }
        if (player.Stats.variantsfound.GetTotalVaiantsFound(AnimalType.Wolf) > 0)
        {
          animalTypeList.Add(AnimalType.Wolf);
          animalTypeList.Add(AnimalType.Armadillo);
        }
        if (player.Stats.variantsfound.GetTotalVaiantsFound(AnimalType.Hippopotamus) > 0)
        {
          animalTypeList.Add(AnimalType.Hippopotamus);
          animalTypeList.Add(AnimalType.Tortoise);
        }
        this.animal = animalTypeList[TinyZoo.Game1.Rnd.Next(0, animalTypeList.Count)];
      }
      this.variant = TinyZoo.Game1.Rnd.Next(0, 3);
    }

    public ShelterAnimal(Reader reader)
    {
      int _out = 0;
      int num1 = (int) reader.ReadInt("f", ref _out);
      this.animal = (AnimalType) _out;
      int num2 = (int) reader.ReadBool("f", ref this.IsAGirl);
      int num3 = (int) reader.ReadFloat("f", ref this.LifeProgress);
      int num4 = (int) reader.ReadInt("f", ref this.NameId);
      int num5 = (int) reader.ReadInt("f", ref this.variant);
    }

    public void SaveShelterAnimal(Writer writer)
    {
      writer.WriteInt("f", (int) this.animal);
      writer.WriteBool("f", this.IsAGirl);
      writer.WriteFloat("f", this.LifeProgress);
      writer.WriteInt("f", this.NameId);
      writer.WriteInt("f", this.variant);
    }
  }
}
