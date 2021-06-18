// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralitySummary.MoralityScoreDisplay_Overall
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;

namespace TinyZoo.Z_MoralitySummary
{
  internal class MoralityScoreDisplay_Overall
  {
    private static Rectangle goodNineSliceRect = new Rectangle(970, 568, 21, 21);
    private static Rectangle evilNineSliceRect = new Rectangle(970, 590, 21, 21);
    private static Rectangle goodTinyRect = new Rectangle(68, 262, 11, 10);
    private static Rectangle evilTinyRect = new Rectangle(185, 245, 10, 10);
    private static string goodstring = "GOOD";
    private static string evilstring = "EVIL";
    private static Color scoreColour = new Color(ColourData.Z_Cream.X, ColourData.Z_Cream.Y, ColourData.Z_Cream.Z);
    private GoodEvilIcon moralityicon;
    private GameObjectNineSlice goodnineslice;
    private GameObjectNineSlice evilnineslice;
    private GameObjectNineSlice currnineslice;
    private string currstring;
    public Vector2 location;
    public float scale;
    private float scoreScale = 1f;
    private float iconScale = 2f;
    private Vector2 textOffset1;
    private Vector2 textOffset2;
    private Vector2 scaleMult = new Vector2(170f, 80f);
    private Vector2 drawScale;
    private float scoreLineSeparator;
    private float moralityScore;

    public MoralityScoreDisplay_Overall(float basescale = 1f)
    {
      this.scale = basescale;
      this.goodnineslice = new GameObjectNineSlice(MoralityScoreDisplay_Overall.goodNineSliceRect, 7);
      this.evilnineslice = new GameObjectNineSlice(MoralityScoreDisplay_Overall.evilNineSliceRect, 7);
      this.goodnineslice.scale = basescale;
      this.evilnineslice.scale = basescale;
      this.scoreLineSeparator = 25f * basescale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.textOffset1 = new Vector2((float) (-0.5 * (double) this.scaleMult.X + 50.0), -22f) * basescale * Sengine.ScreenRatioUpwardsMultiplier;
      this.textOffset2 = this.textOffset1;
      this.textOffset2.Y += this.scoreLineSeparator;
      if ((double) this.moralityScore < 0.0)
      {
        this.currnineslice = this.evilnineslice;
        this.currstring = MoralityScoreDisplay_Overall.evilstring;
        this.moralityicon = new GoodEvilIcon(false, true);
      }
      else
      {
        this.currnineslice = this.goodnineslice;
        this.currstring = MoralityScoreDisplay_Overall.goodstring;
        this.moralityicon = new GoodEvilIcon(true, true);
      }
      this.moralityicon.vLocation.X = (float) (0.5 * -(double) this.scaleMult.X + 25.0) * Sengine.ScreenRatioUpwardsMultiplier.X * basescale;
      this.moralityicon.vLocation.Y = 0.0f;
      this.moralityicon.scale = this.iconScale * basescale;
      this.moralityicon.SetDrawOriginToCentre();
      this.drawScale = this.scaleMult * basescale * Sengine.ScreenRatioUpwardsMultiplier;
    }

    public void SetScore(float score_)
    {
      this.moralityScore = score_;
      if ((double) score_ < 0.0)
      {
        this.currnineslice = this.evilnineslice;
        this.currstring = MoralityScoreDisplay_Overall.evilstring;
        this.moralityicon.SetAlignment(false);
      }
      else
      {
        this.currnineslice = this.goodnineslice;
        this.currstring = MoralityScoreDisplay_Overall.goodstring;
        this.moralityicon.SetAlignment(true);
      }
    }

    public void DrawMoralityScoreDisplay_Overall(Vector2 offset)
    {
      offset += this.location;
      this.currnineslice.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, offset, this.drawScale);
      TextFunctions.DrawTextWithDropShadow("Alignment: " + ((double) this.moralityScore >= 0.0 ? "GOOD" : "EVIL"), this.scoreScale * this.scale, offset + this.textOffset1, MoralityScoreDisplay_Overall.scoreColour, 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch03, false);
      TextFunctions.DrawTextWithDropShadow("Score: " + Math.Abs(this.moralityScore).ToString("F1"), this.scoreScale * this.scale, offset + this.textOffset2, MoralityScoreDisplay_Overall.scoreColour, 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch03, false);
      this.moralityicon.DrawGoodEvilIcon(offset, AssetContainer.pointspritebatch03);
    }

    public Vector2 GetSize(bool noScreenRatioMult = false) => this.scaleMult * Vector2.One * this.scale * Sengine.ScreenRatioUpwardsMultiplier;
  }
}
