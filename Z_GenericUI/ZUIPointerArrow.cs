// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.ZUIPointerArrow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_GenericUI
{
  internal class ZUIPointerArrow : GameObject
  {
    private Vector2 lineVScale;
    private LerpHandler_Float bloop_lerper;
    private bool BloopInY;
    private bool BloopInX;

    public ZUIPointerArrow(
      float BaseScale,
      Vector2 startPoint_ScreenSpace,
      Vector2 endPoint_ScreenSpace,
      bool _BloopInY = true,
      bool _BloopInX = true)
    {
      this.BloopInY = _BloopInY;
      this.BloopInX = _BloopInX;
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetAllColours(ColourData.Z_Cream);
      Vector2 vector2 = endPoint_ScreenSpace - startPoint_ScreenSpace;
      this.Rotation = MathStuff.GetVectorToRadians(vector2 * new Vector2(Sengine.ScreenRatioUpwardsMultiplier.Y, Sengine.ScreenRatioUpwardsMultiplier.X)) - 1.570796f;
      this.lineVScale = new Vector2((vector2 * Sengine.ScreenRationReductionMultiplier).Length(), BaseScale * 4f);
      this.vLocation = startPoint_ScreenSpace;
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      this.bloop_lerper = new LerpHandler_Float();
      this.LerpIn_Bloop();
    }

    public void LerpIn_Bloop() => this.bloop_lerper.SetLerp(true, 0.0f, 1f, 3f);

    public void LerpOut_Bloop(bool bloopOutX = false)
    {
      this.BloopInX = bloopOutX;
      this.bloop_lerper.SetLerp(false, 1f, 0.0f, 3f);
    }

    public bool IsDoneLerpingIn() => (double) this.bloop_lerper.Value == 1.0;

    public void UpdateZUIPointerArrow(float DeltaTime) => this.bloop_lerper.UpdateLerpHandler(DeltaTime);

    public void DrawZUIPointerArrow(Vector2 offset, SpriteBatch spriteBatch)
    {
      Vector2 lineVscale = this.lineVScale;
      if (this.BloopInX)
        lineVscale.X = this.bloop_lerper.Value * this.lineVScale.X;
      if (this.BloopInY)
        lineVscale.Y = this.bloop_lerper.Value * this.lineVScale.Y;
      this.Draw(spriteBatch, AssetContainer.SpriteSheet, offset, lineVscale);
    }
  }
}
