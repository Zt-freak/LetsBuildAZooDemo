// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.Fog.FogLayer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.PlayerDir;

namespace TinyZoo.OverWorld.OverWorldEnv.Fog
{
  internal class FogLayer : GameObject
  {
    private Vector2 Speed;

    public FogLayer(int Index)
    {
      this.DrawRect = new Rectangle(0, 0, 512, 512);
      switch (Index)
      {
        case 0:
          this.Speed = new Vector2(200f, 125f);
          break;
        case 1:
          this.Speed = new Vector2(-100f, -30f);
          break;
        case 2:
          this.Speed = new Vector2(160f, -140f);
          break;
        case 3:
          this.Speed = new Vector2(110f, 70f);
          break;
        case 4:
          this.Speed = new Vector2(90f, -30f);
          break;
      }
      this.SetAllColours(new Vector3(32f, 112f, 109f) / (float) byte.MaxValue);
      this.SetAlpha(0.5f);
      if (!LiveStats.IsChristmas)
        return;
      if ((double) this.Speed.X < 0.0)
        this.Speed.X *= -1f;
      if ((double) this.Speed.Y >= 0.0)
        return;
      this.Speed.Y *= -1f;
    }

    public void UpdateFogLayer(float DeltaTime)
    {
      this.UpdateColours(DeltaTime);
      this.vLocation = this.vLocation + this.Speed * DeltaTime * 0.5f;
    }

    public void DrawFogLayer(SpriteBatch spritebatch, Texture2D DrawWithThis, float ScaleMult = 1f)
    {
      if ((double) this.fAlpha <= 0.0)
        return;
      this.vLocation = RenderMath.TranslateWorldSpaceToScreenSpace(this.vLocation);
      float num1 = (float) this.DrawRect.Width * Sengine.WorldOriginandScale.Z * ScaleMult;
      if ((double) this.vLocation.X > 0.0)
      {
        while ((double) this.vLocation.X > 0.0)
          this.vLocation.X -= num1;
      }
      else if ((double) this.vLocation.X < -(double) num1)
      {
        while ((double) this.vLocation.X < -(double) num1)
          this.vLocation.X += num1;
      }
      if ((double) this.vLocation.Y > 0.0)
      {
        while ((double) this.vLocation.Y > 0.0)
          this.vLocation.Y -= num1 * Sengine.ScreenRatioUpwardsMultiplier.Y;
      }
      else if ((double) this.vLocation.Y < -(double) num1 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y)
      {
        while ((double) this.vLocation.Y < -(double) num1 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y)
          this.vLocation.Y += num1 * Sengine.ScreenRatioUpwardsMultiplier.Y;
      }
      this.scale = Sengine.WorldOriginandScale.Z * ScaleMult;
      Vector2 vLocation = this.vLocation;
      int num2 = (int) (1024.0 / (double) num1) + 2;
      int num3 = (int) (768.0 / (double) num1) + 2;
      for (int index1 = 0; index1 < num2; ++index1)
      {
        this.vLocation = vLocation;
        this.vLocation.X += num1 * (float) index1;
        for (int index2 = 0; index2 < num3; ++index2)
        {
          this.Draw(spritebatch, DrawWithThis);
          this.vLocation.Y += num1 * Sengine.ScreenRatioUpwardsMultiplier.Y;
        }
      }
      this.vLocation = RenderMath.TranslateScreenSpaceToWorldSpace(vLocation);
    }
  }
}
