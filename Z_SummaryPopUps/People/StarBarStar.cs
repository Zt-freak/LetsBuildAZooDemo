// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.StarBarStar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_SummaryPopUps.People
{
  internal class StarBarStar : GameObject
  {
    private GameObject StarFrame;

    public StarBarStar(float FullNess, float SCALE)
    {
      this.StarFrame = new GameObject();
      this.StarFrame.DrawRect = new Rectangle(82, 10, 15, 14);
      this.StarFrame.SetDrawOriginToCentre();
      this.StarFrame.scale = SCALE;
      this.DrawRect = new Rectangle(65, 10, 15, 14);
      this.SetDrawOriginToCentre();
      this.scale = SCALE;
      this.bActive = true;
      if ((double) FullNess <= 0.0)
      {
        this.bActive = false;
      }
      else
      {
        if ((double) FullNess >= 1.0)
          return;
        int num = (int) ((double) this.DrawRect.Width * (double) FullNess);
        if (this.DrawRect.Width == num)
          --num;
        else if (num == 0)
          num = 1;
        this.DrawRect.Width = num;
      }
    }

    public Vector2 GetSize() => new Vector2((float) this.StarFrame.DrawRect.Width * this.StarFrame.scale, (float) this.StarFrame.DrawRect.Height * this.StarFrame.scale) * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawStar(SpriteBatch spritebatch, Vector2 Offset)
    {
      this.StarFrame.vLocation = this.vLocation;
      this.StarFrame.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
      if (!this.bActive)
        return;
      this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
    }
  }
}
