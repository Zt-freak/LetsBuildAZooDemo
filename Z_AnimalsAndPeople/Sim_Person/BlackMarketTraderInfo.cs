// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.BlackMarketTraderInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class BlackMarketTraderInfo
  {
    public AnimalType Body;
    public AnimalType Head;
    public int HeadVariant;
    public int BodyVariant;
    public bool Purchased;

    public BlackMarketTraderInfo()
    {
      this.Body = (AnimalType) Game1.Rnd.Next(0, 56);
      this.Head = this.Body;
      while (this.Head == this.Body)
        this.Head = (AnimalType) Game1.Rnd.Next(0, 56);
      if (!Z_DebugFlags.IsBetaVersion)
        return;
      List<AnimalType> animalTypeList = new List<AnimalType>();
      animalTypeList.Add(AnimalType.Rabbit);
      animalTypeList.Add(AnimalType.Horse);
      animalTypeList.Add(AnimalType.Capybara);
      animalTypeList.Add(AnimalType.Alpaca);
      animalTypeList.Add(AnimalType.Hippopotamus);
      animalTypeList.Add(AnimalType.Armadillo);
      animalTypeList.Add(AnimalType.Snake);
      animalTypeList.Add(AnimalType.Goose);
      animalTypeList.Add(AnimalType.Pig);
      animalTypeList.Add(AnimalType.Tortoise);
      animalTypeList.Add(AnimalType.Wolf);
      this.Body = animalTypeList[Game1.Rnd.Next(0, animalTypeList.Count)];
      this.Head = this.Body;
      while (this.Head == this.Body)
        this.Head = animalTypeList[Game1.Rnd.Next(0, animalTypeList.Count)];
    }
  }
}
