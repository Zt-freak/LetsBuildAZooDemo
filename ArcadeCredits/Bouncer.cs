// Decompiled with JetBrains decompiler
// Type: TinyZoo.ArcadeCredits.Bouncer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.ArcadeCredits
{
  internal class Bouncer
  {
    private EnemyRenderer enemyrenderer;
    private float Speed;

    public Bouncer(AnimalType enemy)
    {
      this.enemyrenderer = new EnemyRenderer(enemy, 0);
      this.enemyrenderer.vLocation = new Vector2((float) TinyZoo.Game1.Rnd.Next(20, 320), (float) TinyZoo.Game1.Rnd.Next(0, 768));
      if (TinyZoo.Game1.Rnd.Next(0, 2) == 0)
        this.enemyrenderer.vLocation.X = 1024f - this.enemyrenderer.vLocation.X;
      this.enemyrenderer.scale = RenderMath.GetPixelSizeBestMatch(3f);
      this.enemyrenderer.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.Speed = (float) TinyZoo.Game1.Rnd.Next(30, 55);
    }

    public void UpdateBouncer(float DeltaTime)
    {
      this.enemyrenderer.vLocation.Y += DeltaTime * this.Speed;
      this.enemyrenderer.UpdateAnimation(DeltaTime);
      if ((double) this.enemyrenderer.vLocation.Y <= 768.0)
        return;
      this.enemyrenderer.vLocation.Y = -100f;
    }

    public void DrawBouncer() => this.enemyrenderer.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
  }
}
