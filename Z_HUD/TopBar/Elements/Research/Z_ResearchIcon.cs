// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.Research.Z_ResearchIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_HUD.TopBar.Elements.Research
{
  internal class Z_ResearchIcon
  {
    public Vector2 location;
    private GameObject icon;
    private IconType iconType;

    public Z_ResearchIcon(float BaseScale, IconType _iconType)
    {
      this.iconType = _iconType;
      this.icon = new GameObject();
      switch (this.iconType)
      {
        case IconType.Research:
          this.icon.DrawRect = new Rectangle(464, 822, 15, 17);
          break;
        case IconType.Customer:
          this.icon.DrawRect = new Rectangle(983, 169, 15, 13);
          break;
      }
      this.icon.scale = BaseScale;
      this.icon.SetDrawOriginToCentre();
    }

    public Vector2 GetSize() => new Vector2((float) this.icon.DrawRect.Width, (float) this.icon.DrawRect.Height) * this.icon.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawResearchIcon(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.icon.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
    }
  }
}
