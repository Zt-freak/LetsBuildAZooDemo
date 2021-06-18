// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.Sub.TopBuilider
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Z_DayNight;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.Sub
{
  internal class TopBuilider
  {
    private GameObject renderer;
    private int TargetRectHeight;
    private float TimePerPixel;
    private float SubCounter;
    private Texture2D DrawWIthThis;
    private bool UseChunks;
    private int NextPixelHeight;
    private bool UseInverseBuild;
    private float DrawOriginYFromBottom;
    private int OriginalY;
    private float OriginalDrawOriginY;

    public TopBuilider(ZooBuildingTopRenderer toprend, int ArrayIndex, int FullFootPrintWidth)
    {
      bool flag = false;
      int num1 = ArrayIndex;
      this.renderer = new GameObject((GameObject) toprend);
      if (toprend.FlipRender)
      {
        flag = true;
        this.renderer.FlipRender = true;
        ArrayIndex = FullFootPrintWidth - 1 - ArrayIndex;
      }
      int num2 = toprend.DrawRect.X + toprend.DrawRect.Width;
      this.renderer.DrawRect.X += ArrayIndex * 16;
      this.renderer.DrawRect.Width = 16;
      this.DrawOriginYFromBottom = (float) toprend.DrawRect.Height - toprend.DrawOrigin.Y;
      this.OriginalY = toprend.DrawRect.Y;
      int num3 = 0;
      this.OriginalDrawOriginY = toprend.DrawOrigin.Y;
      Vector2 drawOrigin = toprend.DrawOrigin;
      Rectangle drawRect = toprend.DrawRect;
      if (flag)
        num3 = toprend.DrawRect.Width - 16 * FullFootPrintWidth;
      else if (this.renderer.DrawRect.X + 16 > num2)
        this.renderer.DrawRect.Width = num2 - this.renderer.DrawRect.X;
      if (ArrayIndex == FullFootPrintWidth - 1 && this.renderer.DrawRect.X + 16 < num2)
        this.renderer.DrawRect.Width = num2 - this.renderer.DrawRect.X;
      this.TargetRectHeight = this.renderer.DrawRect.Height;
      this.DrawWIthThis = toprend.DrawWIthThis;
      this.renderer.vLocation.X += (float) (16 * num1);
      this.TimePerPixel = (float) TinyZoo.Game1.Rnd.Next(10, 20);
      this.TimePerPixel *= 0.1f;
      this.TimePerPixel /= (float) this.TargetRectHeight;
      this.UseInverseBuild = TinyZoo.Game1.Rnd.Next(0, 2) == 0;
      if (!this.UseInverseBuild)
      {
        this.renderer.DrawOrigin.Y -= (float) this.TargetRectHeight;
        this.renderer.DrawRect.Height = 0;
      }
      else
      {
        this.renderer.DrawRect.Height = 0;
        this.renderer.DrawRect.Y += this.TargetRectHeight;
      }
      this.UseChunks = true;
      this.NextPixelHeight = !this.UseChunks ? 1 : TinyZoo.Game1.Rnd.Next(4, 10);
      this.renderer.DrawOrigin.X += (float) num3;
    }

    public bool UpdateTopBuilider(float DeltaTime)
    {
      if (this.renderer.DrawRect.Height < this.TargetRectHeight)
      {
        this.SubCounter += DeltaTime;
        if ((double) this.SubCounter > (double) this.TimePerPixel)
        {
          while ((double) this.SubCounter > (double) this.TimePerPixel * (double) this.NextPixelHeight && this.renderer.DrawRect.Height < this.TargetRectHeight)
          {
            if (this.UseChunks)
            {
              this.SubCounter -= this.TimePerPixel * (float) this.NextPixelHeight;
              this.renderer.DrawRect.Height += this.NextPixelHeight;
              if (this.UseInverseBuild)
              {
                this.renderer.DrawRect.Y -= this.NextPixelHeight;
                this.renderer.DrawOrigin.Y = (float) this.renderer.DrawRect.Height - this.DrawOriginYFromBottom;
              }
              else
                this.renderer.DrawOrigin.Y += (float) this.NextPixelHeight;
              this.NextPixelHeight = TinyZoo.Game1.Rnd.Next(4, 10);
              if (this.renderer.DrawRect.Height >= this.TargetRectHeight)
              {
                this.renderer.DrawOrigin.Y = this.OriginalDrawOriginY;
                this.renderer.DrawRect.Height = this.TargetRectHeight;
                this.renderer.DrawRect.Y = this.OriginalY;
              }
            }
            else
            {
              this.SubCounter -= this.TimePerPixel;
              ++this.renderer.DrawRect.Height;
              ++this.renderer.DrawOrigin.Y;
            }
          }
        }
      }
      return this.renderer.DrawRect.Height != this.TargetRectHeight;
    }

    public void DrawTopBuilider()
    {
      this.renderer.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
      this.renderer.WorldOffsetDraw(AssetContainer.pointspritebatch01, this.DrawWIthThis);
    }
  }
}
