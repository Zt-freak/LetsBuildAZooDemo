// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.BreedChambers.ActiveIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Objects;

namespace TinyZoo.Z_BreedScreen.BreedChambers
{
  internal class ActiveIcon : GameObject
  {
    private static Rectangle smallTick = new Rectangle(333, 115, 12, 12);
    private static Rectangle smallExcl = new Rectangle(321, 115, 12, 12);
    private GameObject gobject;
    private bool useSmall;
    public bool WillFlash;

    public ActiveIcon(bool isATick, float BaseScale, bool useSmall_ = false)
    {
      this.useSmall = useSmall_;
      if (this.useSmall)
      {
        if (isATick)
          this.DrawRect = ActiveIcon.smallTick;
        else
          this.DrawRect = ActiveIcon.smallExcl;
      }
      else if (!isATick)
      {
        this.DrawRect = new Rectangle(596, 381, 32, 32);
        this.SetAllColours(new Vector3(0.9254902f, 0.5215687f, 0.5215687f));
      }
      else
      {
        this.DrawRect = new Rectangle(594, 414, 32, 32);
        this.SetAllColours(new Vector3(0.4431373f, 0.7843137f, 0.5921569f));
      }
      this.SetDrawOriginToCentre();
      this.scale = BaseScale;
      if (this.useSmall)
        return;
      this.gobject = new GameObject((GameObject) this);
      if (!isATick)
        this.gobject.SetAllColours(new Vector3(0.8509804f, 0.3568628f, 0.3568628f));
      else
        this.gobject.SetAllColours(new Vector3(0.1882353f, 0.5647059f, 0.3254902f));
      this.gobject.DrawOrigin.Y -= 2f;
      this.gobject.SetAlpha(0.5f);
      this.gobject.scale = BaseScale;
    }

    public Vector2 GetSize(bool withoutScreenResMult = false)
    {
      Vector2 vector2 = new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale;
      if (!withoutScreenResMult)
        vector2 *= Sengine.ScreenRatioUpwardsMultiplier;
      return vector2;
    }

    public void DrawActiveIcon(SpriteBatch spritebatch, Vector2 Offset)
    {
      if (this.WillFlash && (double) FlashingAlpha.Medium.fAlpha < 0.300000011920929)
        return;
      if (!this.useSmall)
        this.gobject.Draw(spritebatch, AssetContainer.SpriteSheet, Offset + this.vLocation);
      this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
    }
  }
}
