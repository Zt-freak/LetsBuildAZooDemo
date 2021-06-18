// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info.CustomerNeedsIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info
{
  internal class CustomerNeedsIcon : GameObject
  {
    public Vector2 location;
    private float BaseScale;

    public CustomerNeedsIcon(SatisfactionType satisfactionType, float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.SetSatisfactionType(satisfactionType);
    }

    public CustomerNeedsIcon(float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.SetSatisfactionType(SatisfactionType.Count);
    }

    public void SetSatisfactionType(SatisfactionType satisfactionType)
    {
      this.scale = this.BaseScale;
      switch (satisfactionType)
      {
        case SatisfactionType.Energy:
          this.DrawRect = new Rectangle(0, 663, 10, 13);
          break;
        case SatisfactionType.Hunger:
          this.DrawRect = new Rectangle(0, 677, 13, 12);
          break;
        case SatisfactionType.Thirst:
          this.DrawRect = new Rectangle(0, 650, 9, 12);
          break;
        case SatisfactionType.Bathroom:
          this.DrawRect = new Rectangle(953, 589, 16, 16);
          break;
        case SatisfactionType.Animals:
          this.DrawRect = new Rectangle(336, 851, 16, 15);
          break;
        case SatisfactionType.Attractions:
          this.DrawRect = new Rectangle(321, 851, 14, 15);
          break;
        default:
          this.DrawRect = TinyZoo.Game1.WhitePixelRect;
          this.scale = this.BaseScale * 16f;
          break;
      }
      this.SetDrawOriginToCentre();
    }

    public void Deactivate()
    {
      this.SetAllColours(Color.LightGray.ToVector3());
      this.SetAlpha(0.8f);
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void UpdateCustomerNeedsIcon()
    {
    }

    public void DrawCustomerNeedsIcon(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
    }
  }
}
