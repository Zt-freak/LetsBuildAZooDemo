// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Processing.AnimalProductionList
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_Processing
{
  internal class AnimalProductionList
  {
    public AnimalFoodType[] animalfoodtypes;
    public float[] Yield;

    public AnimalProductionList(params AnimalFoodType[] _animalfoodtypes)
    {
      this.animalfoodtypes = _animalfoodtypes;
      this.Yield = new float[this.animalfoodtypes.Length];
      for (int index = 0; index < this.Yield.Length; ++index)
        this.Yield[index] = 1f;
    }

    public void SetYield(params float[] _Yields) => this.Yield = _Yields;
  }
}
