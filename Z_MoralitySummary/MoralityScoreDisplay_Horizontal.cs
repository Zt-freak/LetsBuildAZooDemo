// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralitySummary.MoralityScoreDisplay_Horizontal
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_MoralitySummary
{
  internal class MoralityScoreDisplay_Horizontal
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
    private float scoreScale = 1f;
    private float iconScale = 1f;
    public float score;
    private Vector2 scoreOffset;
    private Vector2 scaleMult = new Vector2(120f, 20f);
    private Vector2 drawScale;
    private UIScaleHelper scalehelper;
    private Vector2 framescale;
    private float basescale;

    public MoralityScoreDisplay_Horizontal(float basescale_ = 1f)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.framescale = this.scalehelper.ScaleVector2(this.scaleMult);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.goodnineslice = new GameObjectNineSlice(MoralityScoreDisplay_Horizontal.goodNineSliceRect, 7);
      this.evilnineslice = new GameObjectNineSlice(MoralityScoreDisplay_Horizontal.evilNineSliceRect, 7);
      this.neutralnineslice = new GameObjectNineSlice(MoralityScoreDisplay_Horizontal.whiteNineSliceRect, 7);
      this.neutralnineslice.SetAllColours(ColourData.ACPaleBlue);
      this.goodnineslice.scale = this.basescale;
      this.evilnineslice.scale = this.basescale;
      this.neutralnineslice.scale = this.basescale;
      this.currnineslice = this.neutralnineslice;
      this.currstring = MoralityScoreDisplay_Horizontal.neutralstring;
      this.moralityicon = new GoodEvilIcon(true, true);
      this.moralityicon.SetDrawOriginToCentre();
      this.moralityicon.scale = this.iconScale * this.basescale;
      Vector2 vector2 = -0.5f * this.framescale;
      vector2.X += 0.5f * defaultBuffer.X;
      this.moralityicon.vLocation.X = vector2.X + 0.5f * this.moralityicon.GetSize().X;
      this.moralityicon.vLocation.Y = 0.0f;
      vector2.X += this.moralityicon.GetSize().X + 0.5f * defaultBuffer.X;
      this.scoreOffset = new Vector2();
      this.scoreOffset.X = vector2.X;
      this.scoreOffset.Y += -0.5f * AssetContainer.SpringFontX1AndHalf.MeasureString("SomeThing ygX").Y;
      this.drawScale = this.scaleMult * this.basescale * Sengine.ScreenRatioUpwardsMultiplier;
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
        this.currstring = MoralityScoreDisplay_Horizontal.evilstring;
        this.moralityicon.SetAlignment(false);
      }
      else if ((double) this.score > 0.0)
      {
        this.currnineslice = this.goodnineslice;
        this.currstring = MoralityScoreDisplay_Horizontal.goodstring;
        this.moralityicon.SetAlignment(true);
      }
      else
      {
        this.currnineslice = this.neutralnineslice;
        this.currstring = MoralityScoreDisplay_Horizontal.neutralstring;
        this.moralityicon.SetAlignment(true);
      }
    }

    public void DrawMoralityScoreDisplay_Horizontal(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.currnineslice.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, offset, this.drawScale);
      TextFunctions.DrawTextWithDropShadow(this.currstring + " " + Math.Abs(this.score).ToString("F1"), this.scoreScale * this.basescale, offset + this.scoreOffset, MoralityScoreDisplay_Horizontal.scoreColour, 1f, AssetContainer.SpringFontX1AndHalf, spritebatch, false);
      if ((double) this.score <= 0.0500000007450581 && (double) this.score >= -0.0500000007450581)
        return;
      this.moralityicon.DrawGoodEvilIcon(offset, spritebatch);
    }

    public Vector2 GetSize(bool noScreenRatioMult = false)
    {
      Vector2 vector2 = this.scaleMult * new Vector2(this.basescale);
      if (!noScreenRatioMult)
        vector2 *= Sengine.ScreenRatioUpwardsMultiplier;
      return vector2;
    }
  }
}
