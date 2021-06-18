// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralitySummary.MoralityScoreDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;

namespace TinyZoo.Z_MoralitySummary
{
  internal class MoralityScoreDisplay
  {
    private static Rectangle goodNineSliceRect = new Rectangle(970, 568, 21, 21);
    private static Rectangle evilNineSliceRect = new Rectangle(970, 590, 21, 21);
    private static Rectangle whiteNineSliceRect = new Rectangle(948, 484, 21, 21);
    private static Rectangle goodTinyRect = new Rectangle(68, 262, 11, 10);
    private static Rectangle evilTinyRect = new Rectangle(185, 245, 10, 10);
    private static string goodstring = "GOOD";
    private static string evilstring = "EVIL";
    private static string neutralstring = "NEUTRAL";
    private static Color scoreColour = new Color(ColourData.Z_Cream.X, ColourData.Z_Cream.Y, ColourData.Z_Cream.Z);
    private GoodEvilIcon moralityicon;
    private GameObjectNineSlice goodnineslice;
    private GameObjectNineSlice evilnineslice;
    private GameObjectNineSlice neutralnineslice;
    private GameObjectNineSlice currnineslice;
    private string currstring;
    public Vector2 location;
    public float basescale;
    private float scoreScale = 1f;
    private float textScale = 1f;
    private float iconScale = 1f;
    public float score;
    private Vector2 textoffset;
    private Vector2 scoreOffset;
    private float framescale = 40f;

    public MoralityScoreDisplay(float basescale_ = 1f)
    {
      this.basescale = basescale_;
      this.goodnineslice = new GameObjectNineSlice(MoralityScoreDisplay.goodNineSliceRect, 7);
      this.evilnineslice = new GameObjectNineSlice(MoralityScoreDisplay.evilNineSliceRect, 7);
      this.neutralnineslice = new GameObjectNineSlice(MoralityScoreDisplay.whiteNineSliceRect, 7);
      this.neutralnineslice.SetAllColours(ColourData.ACPaleBlue);
      this.goodnineslice.scale = this.basescale;
      this.evilnineslice.scale = this.basescale;
      this.neutralnineslice.scale = this.basescale;
      this.textoffset = new Vector2(-15f, -15f) * this.basescale * Sengine.ScreenRatioUpwardsMultiplier;
      this.scoreOffset = new Vector2(0.0f, 3f) * this.basescale * Sengine.ScreenRatioUpwardsMultiplier;
      if ((double) this.score < 0.0)
      {
        this.currnineslice = this.evilnineslice;
        this.currstring = MoralityScoreDisplay.evilstring;
        this.moralityicon = new GoodEvilIcon(false);
      }
      else
      {
        this.currnineslice = this.goodnineslice;
        this.currstring = MoralityScoreDisplay.goodstring;
        this.moralityicon = new GoodEvilIcon(true);
      }
      this.moralityicon.vLocation.X = 11f * Sengine.ScreenRatioUpwardsMultiplier.X * this.basescale;
      this.moralityicon.vLocation.Y = -11f * Sengine.ScreenRatioUpwardsMultiplier.Y * this.basescale;
      this.moralityicon.scale = this.iconScale * this.basescale;
      this.moralityicon.SetDrawOriginToCentre();
    }

    public void SetScore(float score_)
    {
      this.score = score_;
      this.UpdateAppearanceBasedOnScore();
    }

    private void UpdateAppearanceBasedOnScore()
    {
      Vector2 vLocation = this.moralityicon.vLocation;
      if ((double) this.score < 0.0)
      {
        this.currnineslice = this.evilnineslice;
        this.currstring = MoralityScoreDisplay.evilstring;
        this.moralityicon.SetAlignment(false);
      }
      else if ((double) this.score > 0.0)
      {
        this.currnineslice = this.goodnineslice;
        this.currstring = MoralityScoreDisplay.goodstring;
        this.moralityicon.SetAlignment(true);
      }
      else
      {
        this.currnineslice = this.neutralnineslice;
        this.currstring = MoralityScoreDisplay.neutralstring;
        this.moralityicon.SetAlignment(true);
      }
    }

    public void DrawMoralityScoreDisplay(Vector2 offset)
    {
      offset += this.location;
      this.currnineslice.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, offset, this.framescale * Vector2.One * this.basescale * Sengine.ScreenRatioUpwardsMultiplier);
      TextFunctions.DrawJustifiedText(Math.Abs(this.score).ToString("F1"), this.scoreScale * this.basescale, offset + this.scoreOffset, MoralityScoreDisplay.scoreColour, 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch03);
      TextFunctions.DrawTextWithDropShadow(this.currstring, this.textScale * this.basescale, offset + this.textoffset, MoralityScoreDisplay.scoreColour, 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03, false);
      if ((double) this.score <= 0.0500000007450581 && (double) this.score >= -0.0500000007450581)
        return;
      this.moralityicon.DrawGoodEvilIcon(offset, AssetContainer.pointspritebatch03);
    }

    public Vector2 GetSize(bool noScreenRatioMult = false)
    {
      Vector2 vector2 = this.framescale * Vector2.One * this.basescale;
      if (!noScreenRatioMult)
        vector2 *= Sengine.ScreenRatioUpwardsMultiplier;
      return vector2;
    }
  }
}
