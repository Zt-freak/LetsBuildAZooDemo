// Decompiled with JetBrains decompiler
// Type: TinyZoo.ProfitLadder.LevelSummary.LevelIndicator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.ProfitLadder.LevelSummary
{
  internal class LevelIndicator
  {
    public Vector2 Location;
    private GameObject Line;
    private GameObject IConThing;
    private Vector2 VSCale;
    private SimpleTextHandler CostDesc;
    private SimpleTextHandler TGTInc;
    private int TargetIncome;
    internal static float CurrencyGap = 200f;

    public LevelIndicator(
      WardenRank ThisRank,
      bool IsUnlocked,
      bool IsLast,
      float PercentProgressToNext,
      RandkInfo rankdata,
      bool IsOneAboveLast)
    {
      this.IConThing = new GameObject();
      this.IConThing.DrawRect = rankdata.DrawRectForIcon;
      this.IConThing.SetDrawOriginToCentre();
      this.IConThing.scale = 2f;
      this.Location = new Vector2((float) (100.0 + (double) ThisRank * (double) LevelIndicator.CurrencyGap), 350f);
      this.Line = new GameObject();
      this.Line.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.Line.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.TargetIncome = rankdata.IncomeMin;
      float num1 = 200f;
      this.VSCale = new Vector2(5f, 50f);
      this.TGTInc = new SimpleTextHandler("$" + (object) this.TargetIncome, false, 0.2f, 3f * Sengine.UltraWideSreenDownardsMultiplier, false, false);
      if (!IsUnlocked)
      {
        this.Line.SetAlpha(0.3f);
        this.CostDesc = new SimpleTextHandler(SEngine.Localization.Localization.GetText(4), false, 0.2f, 3f * Sengine.UltraWideSreenDownardsMultiplier, false, false);
        int num2 = IsOneAboveLast ? 1 : 0;
        this.IConThing.SetAllColours(0.0f, 0.0f, 0.0f);
      }
      else
        this.CostDesc = new SimpleTextHandler(SEngine.Localization.Localization.GetText((int) rankdata.stringID), false, 0.19f, 3f * Sengine.UltraWideSreenDownardsMultiplier, false, false);
      this.CostDesc.AutoCompleteParagraph();
      Vector2 size = this.CostDesc.paragraph.GetSize(true);
      this.CostDesc.Location.Y = -num1;
      this.CostDesc.Location.X = 3f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.VSCale.Y = num1 - size.Y;
      this.Line.vLocation.Y = -this.VSCale.Y;
      this.TGTInc.AutoCompleteParagraph();
      this.TGTInc.Location.Y = 10f;
    }

    public void UpdateLevelIndicator()
    {
    }

    public void DrawLevelIndicator(Vector2 Offset)
    {
      Offset += this.Location;
      this.IConThing.scale = 1f;
      this.IConThing.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + new Vector2(26f, (float) (-30.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y - 30.0)));
      this.Line.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VSCale);
      this.CostDesc.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatchTop05);
      this.TGTInc.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}
