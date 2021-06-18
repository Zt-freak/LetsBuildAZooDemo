// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.RatingPopUp.RatingIconAndName
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Morality;

namespace TinyZoo.Z_HUD.TopBar.RatingPopUp
{
  internal class RatingIconAndName
  {
    public Vector2 location;
    private RatingCategoryIcon icon;
    private ZGenericText text;

    public RatingIconAndName(RatingCategory category, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.icon = new RatingCategoryIcon(category, BaseScale);
      this.icon.vLocation.X += this.icon.GetSize().X * 0.5f;
      this.text = new ZGenericText(RatingIconAndName.GetRatingCategoryToString(category), BaseScale, false, _UseOnePointFiveFont: true);
      this.text.vLocation.X += this.icon.vLocation.X + this.icon.GetSize().X * 0.5f + uiScaleHelper.ScaleX(5f);
      this.text.vLocation.Y -= this.text.GetSize().Y * 0.5f;
    }

    public RatingIconAndName(MoralityCategory category, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.icon = new RatingCategoryIcon(category, BaseScale);
      this.icon.vLocation.X += this.icon.GetSize().X * 0.5f;
      this.text = new ZGenericText(MoralityData.GetMoralityCategoryToString(category, true), BaseScale, false, _UseOnePointFiveFont: true);
      this.text.vLocation.X += this.icon.vLocation.X + this.icon.GetSize().X * 0.5f + uiScaleHelper.ScaleX(5f);
      this.text.vLocation.Y -= this.text.GetSize().Y * 0.5f;
    }

    public static string GetRatingCategoryToString(RatingCategory category)
    {
      switch (category)
      {
        case RatingCategory.Facilities:
          return "Facilities";
        case RatingCategory.Animals:
          return "Animals";
        case RatingCategory.Decoration:
          return "Attractiveness";
        case RatingCategory.Publicity:
          return "Publicity";
        default:
          return "NA_" + (object) category;
      }
    }

    public void DrawRatingIconAndName(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.icon.DrawRatingCategoryIcon(offset, spriteBatch);
      this.text.DrawZGenericText(offset, spriteBatch);
    }
  }
}
