// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.Maps.MapLocations.MapMarkers.RedLight
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Objects;

namespace TinyZoo.Z_WorldMap.Maps.MapLocations.MapMarkers
{
  internal class RedLight : GameObject
  {
    private bool IsFlashing;
    private bool ISSmall;

    public RedLight(bool _IsFlashing, bool IsGreen = false, float BaseScale = -1f)
    {
      this.IsFlashing = _IsFlashing;
      this.DrawRect = new Rectangle(0, 811, 16, 16);
      if (IsGreen)
        this.DrawRect = new Rectangle(0, 828, 16, 16);
      this.SetDrawOriginToCentre();
      this.scale = 1.5f;
      if (!this.IsFlashing)
        this.SetAllColours(0.4f, 0.4f, 0.4f);
      if (DebugFlags.IsPCVersion)
        this.scale = 1f;
      if ((double) BaseScale == -1.0)
        return;
      this.scale = BaseScale;
    }

    public void SetSmall()
    {
      this.scale *= 0.7f;
      this.ISSmall = true;
      this.IsFlashing = false;
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawRedLight(SpriteBatch spriteBatch, Vector2 Offset)
    {
      SpriteBatch blendBatch = AssetContainer.PointBlendBatch04;
      if (spriteBatch == AssetContainer.pointspritebatch03)
        blendBatch = AssetContainer.PointBlendBatch04;
      else if (spriteBatch == AssetContainer.pointspritebatch01)
      {
        blendBatch = AssetContainer.PointBlendBatch02;
      }
      else
      {
        SpriteBatch pointspritebatchTop05 = AssetContainer.pointspritebatchTop05;
      }
      this.DrawRedLight(spriteBatch, blendBatch, Offset);
    }

    public void DrawRedLight(
      SpriteBatch spriteBatch,
      SpriteBatch blendBatch,
      Vector2 Offset,
      bool IsNotBlendBatch = false)
    {
      this.Draw(spriteBatch, AssetContainer.SpriteSheet, Offset);
      if (!this.IsFlashing || (double) FlashingAlpha.Medium.fAlpha <= 0.400000005960464)
        return;
      if (IsNotBlendBatch)
        this.Draw(blendBatch, AssetContainer.SpriteSheet, Offset, this.scale * 1.4f, 0.4f);
      else
        this.Draw(blendBatch, AssetContainer.SpriteSheet, Offset, this.scale * 1.4f, 1f);
    }
  }
}
