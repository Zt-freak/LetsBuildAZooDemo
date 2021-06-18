// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Shelves.StoreRoomDataEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.Z_StoreRoom.Shelves
{
  internal class StoreRoomDataEntry
  {
    public AnimalFoodType animalfood;
    public float PercentUsed;
    public int ShelfLife;

    public StoreRoomDataEntry(AnimalFoodType _animalfood, float _TotalStock, int _ShelfLife)
    {
      this.ShelfLife = _ShelfLife;
      this.animalfood = _animalfood;
      this.PercentUsed = _TotalStock;
      if ((double) this.PercentUsed <= 1.0)
        return;
      this.PercentUsed = 1f;
    }
  }
}
