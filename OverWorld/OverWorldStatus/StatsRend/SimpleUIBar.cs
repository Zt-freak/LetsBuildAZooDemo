// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldStatus.StatsRend.SimpleUIBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.OverWorld.OverWorldStatus.StatsRend
{
  internal class SimpleUIBar : GameObject
  {
    private Vector2 VSCale;
    private Vector2 VSCaleFilling;
    private GameObject Green;
    private GameObject Red;
    private Vector2 RedScale;
    private Vector2 GreenScale;
    private float GreenFullNess;
    private float RedFullness;
    private bool DrawRedFirst;
    private GameObject BGG;

    public SimpleUIBar(float Width = 100f)
    {
      float num1 = 3f;
      float y = 18f;
      this.VSCale = new Vector2(Width, y);
      float num2 = 0.9f;
      this.SetAllColours(num2, num2, num2);
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      this.Green = new GameObject((GameObject) this);
      this.Red = new GameObject((GameObject) this);
      this.Red.SetAllColours(1f, 0.0f, 0.0f);
      this.Green.SetAllColours(0.0f, 0.772549f, 0.3215686f);
      this.VSCaleFilling = new Vector2(Width - num1, y - num1 * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.Green.vLocation.X = num1 * 0.5f;
      this.Red.vLocation.X = num1 * 0.5f;
      this.RedScale = new Vector2(this.RedFullness, 1f);
      this.GreenScale = new Vector2(this.GreenFullNess, 1f);
      this.BGG = new GameObject((GameObject) this);
      float num3 = 0.0f;
      this.BGG.SetAllColours(num3, num3, num3);
      this.BGG.vLocation.X = num1 * 0.5f;
    }

    public void SetFullness(float REd, float Green, bool _DrawRedFirst)
    {
      this.DrawRedFirst = _DrawRedFirst;
      this.RedScale.X = REd;
      this.GreenScale.X = Green;
    }

    public void UpdateSimpleUIBar()
    {
    }

    public void DrawSimpleUIBar(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset.X -= this.VSCale.X * 0.5f;
      this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset, this.VSCale * Sengine.ScreenRatioUpwardsMultiplier);
      this.BGG.Draw(spritebatch, AssetContainer.SpriteSheet, Offset, this.VSCaleFilling * Sengine.ScreenRatioUpwardsMultiplier);
      if (this.DrawRedFirst)
      {
        this.Red.Draw(spritebatch, AssetContainer.SpriteSheet, Offset, this.VSCaleFilling * this.RedScale * Sengine.ScreenRatioUpwardsMultiplier);
        this.Green.Draw(spritebatch, AssetContainer.SpriteSheet, Offset, this.VSCaleFilling * this.GreenScale * Sengine.ScreenRatioUpwardsMultiplier);
      }
      else
      {
        this.Green.Draw(spritebatch, AssetContainer.SpriteSheet, Offset, this.VSCaleFilling * this.GreenScale * Sengine.ScreenRatioUpwardsMultiplier);
        this.Red.Draw(spritebatch, AssetContainer.SpriteSheet, Offset, this.VSCaleFilling * this.RedScale * Sengine.ScreenRatioUpwardsMultiplier);
      }
    }
  }
}
