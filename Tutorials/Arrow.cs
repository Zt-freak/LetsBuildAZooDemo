// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.Arrow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Objects;

namespace TinyZoo.Tutorials
{
  internal class Arrow : GameObject
  {
    private SinSocillator oscilator;
    private bool WillOscillateUpAndDown;
    private Vector2 VSCALE;

    public Arrow(bool _WillOscillateUpAndDown = false)
    {
      this.WillOscillateUpAndDown = _WillOscillateUpAndDown;
      this.DrawRect = new Rectangle(562, 342, 27, 28);
      this.SetDrawOriginToPoint(DrawOriginPosition.CenterRight);
      this.scale = 1.2f;
      this.oscilator = new SinSocillator(0.5f);
      this.VSCALE = new Vector2(1f, 1f);
      this.SetAlpha(false, 0.3f, 0.0f, 1f);
    }

    public void SetVertical(bool PointsDown)
    {
      if (!PointsDown)
        return;
      this.Rotation = 1.570796f;
    }

    public void InvertPointer() => this.VSCALE.X = -1f;

    public void UpdateArrow(float DeltaTime)
    {
      this.UpdateColours(DeltaTime);
      this.oscilator.UpdateSinOscillator(DeltaTime);
    }

    public void DrawArrow(Vector2 Offset)
    {
      if (this.WillOscillateUpAndDown)
        Offset += new Vector2(0.0f, this.oscilator.CurrentOffset * 3f);
      else
        Offset += new Vector2(this.oscilator.CurrentOffset * 3f, 0.0f);
      this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VSCALE * this.scale * Sengine.ScreenRatioUpwardsMultiplier);
    }
  }
}
