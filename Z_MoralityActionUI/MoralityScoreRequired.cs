// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralityActionUI.MoralityScoreRequired
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_MoralitySummary;

namespace TinyZoo.Z_MoralityActionUI
{
  internal class MoralityScoreRequired
  {
    private float basescale;
    public Vector2 location;
    private GoodEvilIcon moralityIcon;
    private string scoreStr;
    private float score;
    private bool isGood;
    private UIScaleHelper uiScale;
    private Vector2 textCustomOffset;
    private Vector2 scoreLoc;
    private Vector2 scoreSize;

    public MoralityScoreRequired(bool isGood_, float scoreNeeded, float basescale_)
    {
      this.basescale = basescale_;
      this.score = scoreNeeded;
      this.isGood = isGood_;
      this.uiScale = new UIScaleHelper(this.basescale);
      this.textCustomOffset = this.uiScale.ScaleVector2(new Vector2(0.0f, 2f));
      this.scoreStr = this.score.ToString("F0");
      this.moralityIcon = new GoodEvilIcon(this.isGood);
      this.moralityIcon.scale = this.basescale;
      this.moralityIcon.SetDrawOriginToCentre();
      this.scoreSize = this.uiScale.ScaleVector2(AssetContainer.springFont.MeasureString(this.scoreStr));
      this.moralityIcon.vLocation.X = (float) (0.5 * (double) this.scoreSize.X + 0.5 * (double) this.moralityIcon.GetSize().X);
      this.scoreLoc = Vector2.Zero;
      this.scoreLoc.X -= 0.5f * this.moralityIcon.GetSize().X;
      this.moralityIcon.vLocation.X -= 0.5f * this.moralityIcon.GetSize().X;
    }

    public void SetNewValue(float value)
    {
      this.score = value;
      this.scoreStr = this.score.ToString("F0");
    }

    public Vector2 GetSize()
    {
      Vector2 scoreSize = this.scoreSize;
      scoreSize.X += this.moralityIcon.GetSize().X;
      return scoreSize;
    }

    public void DrawMoralityScoreRequired(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.moralityIcon.DrawGoodEvilIcon(offset, spritebatch);
      TextFunctions.DrawJustifiedText(this.scoreStr, this.basescale, offset + this.scoreLoc + this.textCustomOffset, new Color(ColourData.Z_Cream), 1f, AssetContainer.springFont, spritebatch);
    }
  }
}
