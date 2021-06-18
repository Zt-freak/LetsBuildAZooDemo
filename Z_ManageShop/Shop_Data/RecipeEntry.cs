// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.Shop_Data.RecipeEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_ManageShop.FoodIcon;

namespace TinyZoo.Z_ManageShop.Shop_Data
{
  internal class RecipeEntry
  {
    public FOODTYPE MinFood_LeftFood;
    public FOODTYPE PremiumName = FOODTYPE.Count;
    public float Cost;
    public float Salt;
    public float Sugar;
    public float ThirstQuenching;
    public float Spicyness;
    public float Cooling;
    public float premiumCost = -1f;
    public float Price;
    public string SetName;

    public RecipeEntry(string _SetName, FOODTYPE _Name, FOODTYPE _PremiumName = FOODTYPE.Count)
    {
      this.SetName = _SetName;
      this.PremiumName = _PremiumName;
      this.MinFood_LeftFood = _Name;
    }
  }
}
