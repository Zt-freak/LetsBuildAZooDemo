// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.RecipeView.NewLayout.FoodAndName
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_ManageShop.AllShopSummary.Row;
using TinyZoo.Z_ManageShop.FoodIcon;

namespace TinyZoo.Z_ManageShop.RecipeView.NewLayout
{
  internal class FoodAndName
  {
    private MiniFoodIcon foodicon;
    public Vector2 Location;
    private string Name;
    private GameObject TextObject;
    private SimpleTextHandler headerparagraph;

    public FoodAndName(
      FOODTYPE foodtype,
      Vector3 TextColour,
      float BaseScale,
      float TextScaleMult = 1f)
    {
      string foodTypeToString = FoodIconData.GetFoodTypeToString(foodtype);
      this.foodicon = new MiniFoodIcon(foodtype);
      this.foodicon.scale = BaseScale;
      bool flag = false;
      for (int index = 0; index < foodTypeToString.Length; ++index)
      {
        if (foodTypeToString[index] == ' ')
        {
          this.Name += "~";
          flag = true;
        }
        else
          this.Name += foodTypeToString[index].ToString();
      }
      this.headerparagraph = new SimpleTextHandler(this.Name, true, 0.5f, BaseScale, true, true);
      this.headerparagraph.SetAllColours(TextColour);
      this.TextObject = new GameObject();
      this.TextObject.SetAllColours(TextColour);
      this.TextObject.scale = BaseScale * TextScaleMult;
      this.TextObject.vLocation = new Vector2(0.0f, -35f * BaseScale);
      this.headerparagraph.Location = new Vector2(0.0f, -35f * BaseScale);
      if (!flag)
        return;
      this.headerparagraph.Location.Y -= 11f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public void DrawFoodAndName(Vector2 Offset)
    {
      Offset += this.Location;
      this.foodicon.DrawMiniFoodIcon(Offset);
      this.headerparagraph.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}
