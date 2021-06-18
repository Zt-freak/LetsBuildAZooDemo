// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.AnimalFoodEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.Z_StoreRoom
{
  internal class AnimalFoodEntry
  {
    public int QualityOfLife;
    public float PercentOfDailyIdeal;
    public AnimalFoodType foodtype;
    public float NutritionValue;
    public float CostTemp;
    public float CostNormalized;

    public AnimalFoodEntry(AnimalFoodType _foodtype, float _PercentOfDailyIdeal)
    {
      this.foodtype = _foodtype;
      this.PercentOfDailyIdeal = _PercentOfDailyIdeal;
    }
  }
}
