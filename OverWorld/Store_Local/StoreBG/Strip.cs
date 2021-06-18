// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Store_Local.StoreBG.Strip
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.OverWorld.Store_Local.StoreBG
{
  internal class Strip : GameObject
  {
    private Vector2 VSCALE;
    private bool IsTop;
    private float Speed;
    private float DirectionMult;
    private bool IsSpinning;

    public Strip(float ScrollSpeed)
    {
      this.DrawRect = new Rectangle(324, 128, 20, 16);
      this.scale = 4f;
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      this.VSCALE = new Vector2(1024f, 2f);
      this.vLocation.Y = (float) TinyZoo.Game1.Rnd.Next(0, 768);
      this.SetAllColours(ColourData.ACDarkerGreen);
      this.SetAlpha(1f);
      this.Speed = MathStuff.getRandomFloat(10f, 35f);
      this.Speed = ScrollSpeed;
      this.DirectionMult = -1f;
      TinyZoo.Game1.Rnd.Next(0, 2);
    }

    public void SetSpinning()
    {
      this.SetAllColours(ColourData.FernLemon);
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.IsSpinning = true;
      this.vLocation = new Vector2(512f, 384f);
      this.VSCALE = new Vector2(2f, 768f * Sengine.UltraWideSreenUpwardsMultiplier);
      this.Rotation = MathStuff.getRandomFloat(0.0f, 5f);
    }

    public void UpdateStrip(float DeltaTime)
    {
      this.UpdateColours(DeltaTime);
      if (this.IsSpinning)
        this.Rotation += (float) ((double) DeltaTime * (double) this.Speed * (double) this.DirectionMult * 0.100000001490116);
      else if (this.IsTop)
      {
        this.vLocation.X += DeltaTime * this.Speed * this.DirectionMult;
        if ((double) this.DirectionMult < 0.0)
        {
          if ((double) this.vLocation.X > -1.0)
            return;
          this.vLocation.X = 1025f;
        }
        else
        {
          if ((double) this.vLocation.X < 1025.0)
            return;
          this.vLocation.X = -1f;
        }
      }
      else
      {
        this.vLocation.Y += DeltaTime * this.Speed * this.DirectionMult;
        if ((double) this.DirectionMult < 0.0)
        {
          if ((double) this.vLocation.Y > -(double) this.scale * (double) this.DrawRect.Height * (double) Sengine.ScreenRatioUpwardsMultiplier.Y * 0.5)
            return;
          this.vLocation.Y += (float) (769.0 + (double) this.scale * (double) this.DrawRect.Height * (double) Sengine.ScreenRatioUpwardsMultiplier.Y);
        }
        else
        {
          if ((double) this.vLocation.Y < 769.0)
            return;
          this.vLocation.Y = -1f;
        }
      }
    }

    public void DrawStrip(Vector2 Offset, float ALphaMult, SpriteBatch spritebatch) => this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset, this.scale, this.fAlpha * ALphaMult);
  }
}
