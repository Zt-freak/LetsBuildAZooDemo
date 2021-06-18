// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.RatingPopUp.RatingCategoryIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_Morality;

namespace TinyZoo.Z_HUD.TopBar.RatingPopUp
{
  internal class RatingCategoryIcon : GameObject
  {
    public RatingCategoryIcon(RatingCategory category, float BaseScale)
    {
      this.scale = BaseScale;
      switch (category)
      {
        case RatingCategory.Facilities:
          this.DrawRect = new Rectangle(351, 155, 16, 16);
          break;
        case RatingCategory.Animals:
          this.DrawRect = new Rectangle(351, 173, 16, 13);
          break;
        case RatingCategory.Decoration:
          this.DrawRect = new Rectangle(910, 171, 13, 15);
          break;
        case RatingCategory.Publicity:
          this.DrawRect = new Rectangle(902, 276, 18, 13);
          break;
        default:
          this.DrawRect = TinyZoo.Game1.WhitePixelRect;
          this.scale = 16f * BaseScale;
          break;
      }
      this.SetDrawOriginToCentre();
    }

    public RatingCategoryIcon(MoralityCategory category, float BaseScale)
    {
      this.scale = BaseScale;
      switch (category)
      {
        case MoralityCategory.AnimalWelfare:
          this.DrawRect = new Rectangle(351, 173, 16, 13);
          break;
        case MoralityCategory.CustomerTreatment:
          this.DrawRect = new Rectangle(983, 169, 15, 13);
          break;
        case MoralityCategory.EmployeeTreatment:
          this.DrawRect = new Rectangle(999, 168, 13, 16);
          break;
        case MoralityCategory.Industry:
          this.DrawRect = new Rectangle(598, 121, 13, 13);
          break;
        case MoralityCategory.Business:
          this.DrawRect = new Rectangle(585, 121, 12, 14);
          break;
        case MoralityCategory.Pollution:
          this.DrawRect = new Rectangle(870, 492, 15, 15);
          break;
        default:
          this.DrawRect = TinyZoo.Game1.WhitePixelRect;
          this.scale = 16f * BaseScale;
          break;
      }
      this.SetDrawOriginToCentre();
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawRatingCategoryIcon(Vector2 offset, SpriteBatch spriteBatch) => this.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
  }
}
