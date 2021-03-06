// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.AllShopSummary.Row.MiniFoodIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Z_ManageShop.FoodIcon;

namespace TinyZoo.Z_ManageShop.AllShopSummary.Row
{
  internal class MiniFoodIcon : GameObject
  {
    private GameObject FoodThing;

    public MiniFoodIcon(FOODTYPE foodtype)
    {
      this.DrawRect = FoodIconData.GetFoodRectangle(foodtype);
      this.SetDrawOriginToCentre();
      this.scale = 1f;
    }

    public void DrawMiniFoodIcon(Vector2 Offset) => this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
  }
}
