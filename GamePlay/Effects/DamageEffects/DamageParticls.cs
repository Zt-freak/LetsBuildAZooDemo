// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Effects.DamageEffects.DamageParticls
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.GamePlay.Effects.DamageEffects
{
  internal class DamageParticls : GameObject
  {
    private float Timer;
    private Vector2 Direction;
    private float Speed;
    private float ScaleSpeed;
    private float Life;

    public DamageParticls(int Index)
    {
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.scale = 50f;
      this.SetDrawOriginToCentre();
      switch (Index % 3)
      {
        case 0:
          this.SetAllColours(0.0f, 0.0f, 1f);
          break;
        case 1:
          this.SetAllColours(0.0f, 1f, 0.0f);
          break;
        case 2:
          this.SetAllColours(1f, 0.0f, 0.0f);
          break;
      }
    }

    public void LaunchParticle(Vector2 Location)
    {
      this.vLocation = Location;
      this.Timer = 0.0f;
      this.scale = 1f;
      this.Speed = MathStuff.getRandomFloat(0.0f, 3f);
      this.Direction = MathStuff.GetRandomVector2(new Vector2(-1f, -1f), new Vector2(1f, 1f), TinyZoo.Game1.Rnd);
      this.SetAlpha(1f);
      this.Life = MathStuff.getRandomFloat(0.4f, 1f);
      this.ScaleSpeed = (float) TinyZoo.Game1.Rnd.Next(1, 5);
    }

    public void UpdateDamageParticls(float DeltaTime)
    {
      if ((double) this.Timer < (double) this.Life)
      {
        this.Timer += DeltaTime;
        if ((double) this.Timer >= (double) this.Life)
          this.SetAlpha(false, 0.5f, 1f, 0.0f);
      }
      this.scale += DeltaTime * this.ScaleSpeed;
      this.UpdateColours(DeltaTime);
      this.vLocation = this.vLocation + this.Direction * DeltaTime * this.Speed;
    }

    public void DrawParticle() => this.WorldOffsetDraw(AssetContainer.PointBlendBatch02, AssetContainer.SpriteSheet);
  }
}
