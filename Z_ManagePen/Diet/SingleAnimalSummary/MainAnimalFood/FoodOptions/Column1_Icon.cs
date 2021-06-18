// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodOptions.Column1_Icon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_StoreRoom.Shelves;

namespace TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodOptions
{
  internal class Column1_Icon
  {
    private AnimalFoodIcon animalfoodicon;
    private TopTextMini toptextmini;
    public Vector2 Location;

    public Column1_Icon(float HeightForText, int Index, AnimalFoodType foodtype, float BaseScale)
    {
      this.animalfoodicon = new AnimalFoodIcon(foodtype, 1f);
      this.toptextmini = new TopTextMini(string.Concat((object) Index), BaseScale, HeightForText);
      this.animalfoodicon.vLocation.X = (float) this.animalfoodicon.DrawRect.Width * 0.5f * BaseScale;
      this.animalfoodicon.vLocation.Y = 5f;
    }

    public void UpdateColumn1_Icon()
    {
    }

    public void DrawColumn1_Icon(Vector2 Offset)
    {
      Offset += this.Location;
      this.toptextmini.DrawTopTextMini(Offset);
      this.animalfoodicon.DrawAnimalFoodIcon(Offset, false, AssetContainer.pointspritebatchTop05);
    }
  }
}
