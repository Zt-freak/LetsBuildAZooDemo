// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_HeroQuests_Pins.PinNotification.PinIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_HUD.PointAtThings;

namespace TinyZoo.Z_HUD.Z_HeroQuests_Pins.PinNotification
{
  internal class PinIcon
  {
    public Vector2 location;
    private GameObject icon;
    private Texture2D texture;

    public PinIcon(float BaseScale, OffscreenPointerType offscreenpointertype)
    {
      this.icon = new GameObject();
      this.icon.scale = BaseScale;
      this.icon.DrawRect = EventPointer.GetRectangleForThisPointerType(offscreenpointertype, out this.texture);
      this.icon.SetDrawOriginToCentre();
    }

    public Vector2 GetSize() => new Vector2((float) this.icon.DrawRect.Width, (float) this.icon.DrawRect.Height) * this.icon.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawPinIcon(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.icon.Draw(spriteBatch, this.texture, offset);
    }
  }
}
