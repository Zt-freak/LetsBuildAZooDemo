// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralitySummary.SubElements.ScoreDisplay.MoralityScoreProgressionDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Morality;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_MoralitySummary.SubElements.ScoreDisplay
{
  internal class MoralityScoreProgressionDisplay
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private GoodEvilIcon goodEvilIcon;
    private ZGenericText scoreProgression;
    private float BaseScale;
    private Vector2 buffer;

    public MoralityScoreProgressionDisplay(float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.buffer = new UIScaleHelper(this.BaseScale).DefaultBuffer;
      this.goodEvilIcon = new GoodEvilIcon(true, useBigIcon: true, basescale_: this.BaseScale);
      this.goodEvilIcon.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      float num = this.goodEvilIcon.GetSize().X + this.buffer.X;
      this.scoreProgression = new ZGenericText("X", this.BaseScale, false, _UseOnePointFiveFont: true);
      this.scoreProgression.vLocation.X = num;
      this.scoreProgression.vLocation.Y -= this.scoreProgression.GetSize().Y * 0.5f;
    }

    public Vector2 GetSize() => new Vector2(this.goodEvilIcon.GetSize().X + this.buffer.X + this.scoreProgression.GetSize().X, Math.Max(this.goodEvilIcon.GetSize().Y, this.scoreProgression.GetSize().Y));

    public void SetScore(
      float currentScore,
      float maxScore,
      bool IsGoodNotEvil_Icon,
      bool RoundToInt = true)
    {
      if (RoundToInt)
      {
        currentScore = (float) (int) Math.Floor((double) currentScore);
        maxScore = (float) (int) Math.Floor((double) maxScore);
      }
      string str = "Good: ";
      if (!IsGoodNotEvil_Icon)
        str = "Evil: ";
      this.scoreProgression.textToWrite = str + string.Format("{0}/{1}", (object) currentScore, (object) maxScore);
      this.goodEvilIcon.SetAlignment(IsGoodNotEvil_Icon);
    }

    public bool SmartSetScoreForBuilding(TILETYPE tileType, Player player)
    {
      int toUseThisBuilding = MoralityUnlocksData.GetNumberOfPointsNeededToUseThisBuilding(tileType);
      bool IsGoodNotEvil_Icon = toUseThisBuilding > 0;
      bool flag = (double) player.livestats.MoralityScore >= 0.0;
      float num = player.livestats.MoralityScore;
      if (IsGoodNotEvil_Icon != flag)
        num = 0.0f;
      this.SetScore(Math.Abs(num), (float) Math.Abs(toUseThisBuilding), IsGoodNotEvil_Icon);
      return IsGoodNotEvil_Icon;
    }

    public void DrawMoralityScoreProgressionDisplay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.scoreProgression.DrawZGenericText(offset, spriteBatch);
      this.goodEvilIcon.DrawGoodEvilIcon(offset, spriteBatch);
    }
  }
}
