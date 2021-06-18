// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralitySummary.MoralityHeader
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_MoralitySummary
{
  internal class MoralityHeader
  {
    public Vector2 location;
    private float basescale;
    private Vector2 framescale;
    private CustomerFrame frame;
    private UIScaleHelper scalehelper;
    private MiniHeading heading;
    private MoralityScoreDisplay_Horizontal scoredisplay;
    private float moralityscore;

    public MoralityHeader(
      float morality_negEvilposGood,
      string text,
      float basescale_,
      float forceToThisWidth = -1f,
      bool displayScoreTag = false)
    {
      this.basescale = basescale_;
      this.moralityscore = morality_negEvilposGood;
      if (displayScoreTag)
      {
        this.scoredisplay = new MoralityScoreDisplay_Horizontal(this.basescale);
        this.scoredisplay.SetScore(this.moralityscore);
      }
      this.heading = new MiniHeading(Vector2.Zero, text, 1f, this.basescale);
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.framescale = 2f * defaultBuffer;
      this.framescale.Y += Math.Max(this.heading.GetSize().Y, 0.0f);
      this.framescale.X += this.heading.GetSize().X;
      this.framescale.X = Math.Max(this.framescale.X, forceToThisWidth);
      this.heading.SetTextPosition(this.framescale);
      this.SetColourBasedOnScore();
      if (this.scoredisplay == null)
        return;
      this.scoredisplay.location.X = (float) (0.5 * (double) this.framescale.X - (double) defaultBuffer.X - 0.5 * (double) this.scoredisplay.GetSize().X);
      this.scoredisplay.location.Y = 0.0f;
    }

    public MoralityHeader(
      string text,
      float basescale_,
      float forceToThisWidth = -1f,
      float moralityscore_ = 0.0f)
    {
      this.basescale = basescale_;
      this.moralityscore = moralityscore_;
      this.scoredisplay = new MoralityScoreDisplay_Horizontal(this.basescale);
      this.scoredisplay.SetScore(this.moralityscore);
      this.heading = new MiniHeading(Vector2.Zero, text, 1f, this.basescale);
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.framescale = 2f * defaultBuffer;
      this.framescale.Y += Math.Max(this.heading.GetSize().Y, 0.0f);
      this.framescale.X += this.heading.GetSize().X + defaultBuffer.X + this.scoredisplay.GetSize().X;
      this.framescale.X = Math.Max(this.framescale.X, forceToThisWidth);
      this.heading.SetTextPosition(this.framescale);
      this.SetColourBasedOnScore();
      this.scoredisplay.location.X = (float) (0.5 * (double) this.framescale.X - (double) defaultBuffer.X - 0.5 * (double) this.scoredisplay.GetSize().X);
      this.scoredisplay.location.Y = 0.0f;
    }

    public Vector2 GetSize() => this.framescale;

    private void SetColourBasedOnScore() => this.frame = new CustomerFrame(this.framescale, (double) this.moralityscore <= 0.0 ? ((double) this.moralityscore >= 0.0 ? CustomerFrameColors.NeutralBlueHeader : CustomerFrameColors.EvilPurpleHeader) : CustomerFrameColors.GoodYellowHeader, this.basescale);

    public void SetScore(float moralityscore_)
    {
      this.moralityscore = moralityscore_;
      if (this.scoredisplay != null)
        this.scoredisplay.SetScore(this.moralityscore);
      this.SetColourBasedOnScore();
    }

    public bool UpdateMoralityHeader(float DeltaTime) => false;

    public void DrawMoralityHeader(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.heading.DrawMiniHeading(offset, spritebatch);
      if (this.scoredisplay == null)
        return;
      this.scoredisplay.DrawMoralityScoreDisplay_Horizontal(offset, spritebatch);
    }
  }
}
