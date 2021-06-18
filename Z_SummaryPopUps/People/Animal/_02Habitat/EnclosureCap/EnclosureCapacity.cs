// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._02Habitat.EnclosureCap.EnclosureCapacity
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._02Habitat.EnclosureCap
{
  internal class EnclosureCapacity : GameObject
  {
    private PointerAndText pointerandtext;
    public Vector2 Location;

    public EnclosureCapacity(PrisonZone pz, float FloorSpace, float BaseScale)
    {
      this.DrawRect = new Rectangle(654, 591, 230, 10);
      this.scale = BaseScale;
      float TerritorySize = 0.0f;
      float num1 = pz.GetSpaceRequiredByAniamlsInThisPen(ref TerritorySize) + TerritorySize;
      float num2 = 2f;
      float num3 = 73f;
      float num4 = 79f;
      float num5 = 52f;
      float num6 = 22f;
      float num7 = 1f;
      float num8 = 3f;
      string Text;
      float num9;
      if ((double) num1 >= (double) FloorSpace + (double) FloorSpace * (double) num7)
      {
        float num10 = num2 + num3 + num4 + num5;
        Text = "Inhumane";
        float num11 = (num1 - (FloorSpace + FloorSpace * num7)) / (FloorSpace * num8);
        if ((double) num11 > 1.0)
          num11 = 1f;
        num9 = num10 + num6 * num11;
      }
      else if ((double) num1 > (double) FloorSpace)
      {
        float num10 = num2 + num3 + num4;
        Text = "Uncomfortable";
        float num11 = (num1 - FloorSpace) / (FloorSpace * num7);
        num9 = num10 + num5 * num11;
      }
      else
      {
        float num10 = 0.5f;
        if ((double) num1 > (double) FloorSpace * (double) num10)
        {
          Text = "Acceptable";
          num9 = num2 + num3 + num4 * (float) ((double) num1 * (1.0 - (double) num10) / ((double) FloorSpace - (double) num1 * (double) num10));
        }
        else
        {
          Text = "Spacious";
          num9 = num2 + num3 * (num1 / (FloorSpace * num10));
        }
      }
      float x = num9 * this.scale;
      this.pointerandtext = new PointerAndText(Text, BaseScale);
      this.pointerandtext.vLocation = new Vector2(x, this.GetBarSize().Y * 0.5f);
    }

    public Vector2 GetBarSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public Vector2 GetSize()
    {
      Vector2 barSize = this.GetBarSize();
      Vector2 vector2 = barSize;
      vector2.Y += this.pointerandtext.GetLineAndTextHeight() - barSize.Y;
      return vector2;
    }

    public Vector2 GetOffsetFromTopLeft() => new Vector2(0.0f, (float) (((double) this.pointerandtext.GetLineVScale().Y - (double) this.GetBarSize().Y) * 0.5));

    public void DrawEnclosureCapacity(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.Location;
      this.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
      this.pointerandtext.DrawPointerAndText(offset);
    }
  }
}
