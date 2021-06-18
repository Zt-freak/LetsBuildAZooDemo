// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.StarBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class StarBar
  {
    private StarBarStar[] stars;
    public Vector2 Location;

    public StarBar(float FullNess, float Overridescale = -1f)
    {
      this.stars = new StarBarStar[5];
      float SCALE = RenderMath.GetPixelSizeBestMatch(1f);
      if ((double) Overridescale > -1.0)
        SCALE = Overridescale;
      float num = 17f;
      for (int index = 0; index < 5; ++index)
      {
        this.stars[index] = new StarBarStar(MathHelper.Clamp(FullNess - (float) index, 0.0f, 1f), SCALE);
        this.stars[index].vLocation.X = (float) index * SCALE * num;
        this.stars[index].vLocation.X -= (float) ((double) SCALE * (double) num * 2.0);
      }
    }

    public StarBar(float FullNess, float BaseScale, bool DrawFromCenter, int TotalStars = 5)
    {
      this.stars = new StarBarStar[TotalStars];
      float num = 2f * BaseScale;
      for (int index = 0; index < TotalStars; ++index)
      {
        this.stars[index] = new StarBarStar(MathHelper.Clamp(FullNess - (float) index, 0.0f, 1f), BaseScale);
        Vector2 size = this.stars[index].GetSize();
        if (DrawFromCenter)
          this.stars[index].vLocation.X -= (float) (((double) size.X + (double) num) * (double) (TotalStars - 1) * 0.5);
        else
          this.stars[index].vLocation.X += size.X * 0.5f;
        this.stars[index].vLocation.X += (size.X + num) * (float) index;
      }
    }

    public Vector2 GetSize()
    {
      Vector2 size = this.stars[0].GetSize();
      return new Vector2(this.stars[this.stars.Length - 1].vLocation.X - this.stars[0].vLocation.X + size.X, size.Y);
    }

    public void DrawStarBar(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      for (int index = 0; index < this.stars.Length; ++index)
        this.stars[index].DrawStar(spritebatch, Offset);
    }
  }
}
