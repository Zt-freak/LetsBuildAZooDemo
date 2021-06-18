// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.AnimalFoodInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_StoreRoom
{
  internal class AnimalFoodInfo
  {
    public Rectangle DrawRect;
    public int DeliveryTime;
    public int Cost;
    public int ShelfLife;

    public AnimalFoodInfo(Rectangle _DrawRect, int _DeliveryTime, int _ShelfLife, int _Cost)
    {
      this.Cost = _Cost;
      this.DrawRect = _DrawRect;
      this.DeliveryTime = _DeliveryTime;
      this.ShelfLife = _ShelfLife;
    }
  }
}
