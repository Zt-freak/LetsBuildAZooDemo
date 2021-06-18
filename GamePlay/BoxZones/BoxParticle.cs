// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.BoxZones.BoxParticle
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Objects;

namespace TinyZoo.GamePlay.BoxZones
{
  internal class BoxParticle : GameObject
  {
    private Vector2 TopLeft;
    private Vector2 BottomRight;
    private float AlphaSpeed;
    private DualSinOscillator oscialltor;
    private Vector2 Loc;

    public BoxParticle(Vector2 _TopLeft, Vector2 _BottomRight)
    {
      this.TopLeft = _TopLeft;
      this.TopLeft += Vector2.One;
      this.BottomRight = _BottomRight;
      this.BottomRight -= Vector2.One;
      float x = this.TopLeft.X;
      float Max1 = this.BottomRight.X;
      if ((double) this.BottomRight.X < (double) this.TopLeft.X)
        Max1 = x;
      float y = this.TopLeft.Y;
      float Max2 = this.BottomRight.Y;
      if ((double) y > (double) Max2)
        Max2 = y;
      this.Loc = new Vector2(MathStuff.getRandomFloat(x, Max1), MathStuff.getRandomFloat(y, Max2));
      this.SetAlpha(0.8f);
      this.SetAllColours(0.9f, 1f, 0.9f);
      this.AlphaSpeed = MathStuff.getRandomFloat(0.6f, 2f);
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToCentre();
      this.scale = (float) TinyZoo.Game1.Rnd.Next(1, 2);
      this.oscialltor = new DualSinOscillator(MathStuff.getRandomFloat(0.5f, 1f), MathStuff.getRandomFloat(0.5f, 1f), MathStuff.getRandomFloat(-3f, 3f), MathStuff.getRandomFloat(-3f, 3f));
    }

    public void UpdateBoxParticle(float DeltaTime)
    {
      this.AlphaCycle(this.AlphaSpeed, 0.9f, 0.1f);
      this.UpdateColours(DeltaTime);
      this.oscialltor.UpdateDualSinOscillator(DeltaTime * 0.3f);
    }

    public void DrawBoxParticle()
    {
      this.vLocation = this.Loc + this.oscialltor.CurrentOffset;
      this.WorldOffsetDraw(AssetContainer.PointBlendBatch02, AssetContainer.SpriteSheet);
    }

    public void BarDrawBoxParticle(Vector2 Location)
    {
      this.scale = 4f;
      this.vLocation = this.Loc + this.oscialltor.CurrentOffset + Location;
      this.Draw(AssetContainer.PointBlendBatch04, AssetContainer.SpriteSheet);
    }
  }
}
