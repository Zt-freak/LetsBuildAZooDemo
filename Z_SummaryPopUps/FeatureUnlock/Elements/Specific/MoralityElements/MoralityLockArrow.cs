// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements.Specific.MoralityElements.MoralityLockArrow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements.Specific.MoralityElements
{
  internal class MoralityLockArrow
  {
    public Vector2 location;
    private GameObject lockWithArrow;

    public MoralityLockArrow(float BaseScale)
    {
      this.lockWithArrow = new GameObject();
      this.lockWithArrow.DrawRect = new Rectangle(259, 439, 140, 38);
      this.lockWithArrow.scale = BaseScale;
      this.lockWithArrow.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
    }

    public Vector2 GetOffsetToCenterOfMoralityIcons(bool isGood)
    {
      Vector2 vector2 = new Vector2(62f, 30f);
      if (isGood)
        vector2.X *= -1f;
      return vector2 * this.lockWithArrow.scale * Sengine.ScreenRatioUpwardsMultiplier;
    }

    public Vector2 GetSizeOfMoralityIconOnly() => new Vector2(18f, 19f) * this.lockWithArrow.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public Vector2 GetSize() => new Vector2((float) this.lockWithArrow.DrawRect.Width, (float) this.lockWithArrow.DrawRect.Height) * this.lockWithArrow.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawMoralityLockArrow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.lockWithArrow.Draw(spriteBatch, AssetContainer.UISheet, offset);
    }
  }
}
