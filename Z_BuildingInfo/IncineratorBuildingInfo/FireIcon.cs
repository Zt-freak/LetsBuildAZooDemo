// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.IncineratorBuildingInfo.FireIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_BuildingInfo.IncineratorBuildingInfo
{
  internal class FireIcon : GameObject
  {
    public bool Active;

    public FireIcon(float BaseScale)
    {
      this.DrawRect = new Rectangle(155, 487, 14, 16);
      this.scale = BaseScale;
      this.SetDrawOriginToCentre();
      this.Active = true;
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawFireIcon(Vector2 offset, SpriteBatch spriteBatch)
    {
      if (!this.Active)
        return;
      this.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
    }
  }
}
