// Decompiled with JetBrains decompiler
// Type: TinyZoo.ProfitLadder.LevelSummary.CahsProgress
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.ProfitLadder.LevelSummary
{
  internal class CahsProgress
  {
    private GameObject CashGot;
    private GameObject Frame;
    private Vector2 CashGotScale;
    private Vector2 BaseFrameScale;
    private int CashPerDay;
    private GameObject texthead;

    public CahsProgress(int BaseLevel, float PercentThoughThisLevel, int _CashPerDay)
    {
      this.CashPerDay = _CashPerDay;
      this.Frame = new GameObject();
      this.Frame.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.Frame.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      this.CashGot = new GameObject(this.Frame);
      this.Frame.SetAlpha(0.4f);
      this.CashGot.SetAllColours(0.1f, 0.1f, 0.5f);
      this.CashGotScale = new Vector2((float) BaseLevel * LevelIndicator.CurrencyGap, 25f);
      this.CashGotScale.X += PercentThoughThisLevel * LevelIndicator.CurrencyGap;
      this.BaseFrameScale = new Vector2(1030f, 31f);
      this.Frame.vLocation.X = -3f;
      this.texthead = new GameObject();
      this.texthead.SetAllColours(ColourData.FernLemon);
      this.texthead.scale = 3f;
    }

    public void UpdateCahsProgress()
    {
    }

    public void DrawCahsProgress(Vector2 Offset, bool TopDraw = false)
    {
      this.Frame.vLocation.X = Offset.X;
      this.Frame.vLocation.Y = Offset.Y;
      if ((double) this.Frame.vLocation.X < -3.0)
        this.Frame.vLocation.X = -3f;
      if (!TopDraw)
      {
        this.Frame.SetAllColours(1f, 1f, 1f);
        this.Frame.SetAlpha(0.9f);
        this.Frame.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Vector2.Zero, this.BaseFrameScale * Sengine.ScreenRatioUpwardsMultiplier);
        this.Frame.SetAllColours(0.0f, 0.0f, 0.0f);
        this.Frame.SetAlpha(0.3f);
        this.Frame.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, new Vector2(3f, 0.0f), new Vector2(this.BaseFrameScale.X, this.CashGotScale.Y) * Sengine.ScreenRatioUpwardsMultiplier);
        this.CashGot.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.CashGotScale * Sengine.ScreenRatioUpwardsMultiplier);
      }
      else
      {
        this.CashGot.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.CashGotScale * Sengine.ScreenRatioUpwardsMultiplier, 0.5f);
        float num = 0.0f;
        if (GameFlags.HasNotch)
          num = GameFlags.NotchSize;
        TextFunctions.DrawTextWithDropShadow(string.Format(SEngine.Localization.Localization.GetText(85), (object) this.CashPerDay), this.texthead.scale, this.Frame.vLocation + new Vector2(8f + num, -10f * Sengine.ScreenRatioUpwardsMultiplier.Y), this.texthead.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false);
      }
    }
  }
}
