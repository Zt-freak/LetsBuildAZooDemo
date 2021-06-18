// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Smear.SmearItem
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;

namespace TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Smear
{
  internal class SmearItem : GameObject
  {
    private float Width;
    private float Height;
    private float Speed;
    private float EX;

    public SmearItem(Random rndd, float _Width, float _Height)
    {
      this.Width = _Width;
      this.Height = _Height;
      this.DrawRect = new Rectangle(0, 0, 1024, 1024);
      this.SetDrawOriginToCentre();
      this.SetAllColours(1f, 0.8f, 0.2f);
      this.SetAlpha(0.35f);
      if (rndd.Next(0, 5) == 0)
      {
        this.SetAllColours(1f, 0.3f, 0.2f);
        this.SetAlpha(0.4f);
      }
      this.scale = MathStuff.getRandomFloat(0.5f, 3f, rndd);
      this.Speed = MathStuff.getRandomFloat(10f, 20f, rndd);
      this.EX = (float) (this.DrawRect.Width / 2) * this.scale;
      this.EX += 5f;
    }

    public void UpdateSmearItem(float DeltaTime)
    {
      this.vLocation.X += DeltaTime * this.Speed;
      if ((double) this.vLocation.X <= (double) this.Width + (double) this.EX)
        return;
      this.vLocation.X = -this.EX;
      this.vLocation.Y = (float) TinyZoo.Game1.Rnd.Next(0, (int) this.Height);
      this.vLocation.Y *= Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.Speed = MathStuff.getRandomFloat(10f, 60f, TinyZoo.Game1.Rnd);
    }

    public void DrawSmear() => this.WorldOffsetDraw(AssetContainer.PointBlendBatch02, AssetContainer.SmearSheet, this.fAlpha);
  }
}
