// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.HUD.StatusArea.HintParticle
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Lerp;

namespace TinyZoo.GamePlay.HUD.StatusArea
{
  internal class HintParticle : GameObject
  {
    private Vector2 StartLocation;
    private SinLerper sinlerp;
    private bool Active;

    public void Create(Vector2 TopLeft, Vector2 BottomRight)
    {
      this.SetAlpha(1f);
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.Active = true;
      this.SetDrawOriginToCentre();
      this.StartLocation = new Vector2(MathStuff.getRandomFloat(TopLeft.X, BottomRight.X), MathStuff.getRandomFloat(TopLeft.Y, BottomRight.Y));
      this.sinlerp = new SinLerper();
      this.sinlerp.SetLerp(SinCurveType.EaseIn, MathStuff.getRandomFloat(0.6f, 1f), 0.0f, 1f, true);
      this.StartLocation = RenderMath.TranslateWorldSpaceToScreenSpace(this.StartLocation);
    }

    public bool IsActive() => this.Active;

    public void UpdateHintParticle(float DeltaTime)
    {
      if (!this.Active)
        return;
      this.sinlerp.UpdateSinLerper(DeltaTime);
      if ((double) this.sinlerp.CurrentValue == 1.0 && (double) this.fTargetAlpha != 0.0)
        this.SetAlpha(false, 0.2f, 1f, 0.0f);
      this.UpdateColours(DeltaTime);
      if ((double) this.fAlpha != 0.0)
        return;
      this.Active = false;
    }

    public void DrawHintParticle(Vector2 BarLocation)
    {
      if (!this.Active)
        return;
      this.vLocation = (this.StartLocation - BarLocation) * (1f - this.sinlerp.CurrentValue);
      this.vLocation = this.vLocation + BarLocation;
      this.scale = 3f;
      this.SetAllColours(1f, 1f, 1f);
      this.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
      this.scale = 6f;
      this.SetAllColours(0.2f, 1f, 0.2f);
      this.Draw(AssetContainer.PointBlendBatch04, AssetContainer.SpriteSheet, Vector2.Zero, this.fAlpha * 0.5f);
    }
  }
}
