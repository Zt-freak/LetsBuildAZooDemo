// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodDivisionBar.FoodPointer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_StoreRoom.Shelves;

namespace TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodDivisionBar
{
  internal class FoodPointer : GameObject
  {
    private Vector2 VSCALE;
    public AnimalFoodIcon animalfoodicon;

    public FoodPointer(AnimalFoodEntry animalfoodentry)
    {
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.VSCALE = new Vector2(2f, 24f);
      this.SetAllColours(ColourData.Z_Cream);
      if (animalfoodentry != null)
      {
        this.animalfoodicon = new AnimalFoodIcon(animalfoodentry.foodtype, 1f);
        this.animalfoodicon.vLocation.Y = 15f * this.animalfoodicon.scale;
        this.animalfoodicon.vLocation.Y += this.VSCALE.Y * 0.5f;
        this.animalfoodicon.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      }
      else
      {
        this.VSCALE.Y = 14f;
        this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
        this.SetAllColours(new Vector3(135f, 210f, 55f) / (float) byte.MaxValue);
        this.vLocation.Y += 6f;
      }
    }

    public void DrawFoodPointer(Vector2 Offset)
    {
      this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VSCALE);
      if (this.animalfoodicon == null)
        return;
      this.animalfoodicon.DrawAnimalFoodIcon(Offset + this.vLocation, false, AssetContainer.pointspritebatchTop05);
    }
  }
}
