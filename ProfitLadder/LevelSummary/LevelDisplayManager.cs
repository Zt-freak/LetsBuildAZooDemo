// Decompiled with JetBrains decompiler
// Type: TinyZoo.ProfitLadder.LevelSummary.LevelDisplayManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.DragHandlers;
using System.Collections.Generic;

namespace TinyZoo.ProfitLadder.LevelSummary
{
  internal class LevelDisplayManager
  {
    private SpringDrag_ZoneManager springdrag;
    private List<LevelIndicator> levelindicators;
    private GameObject BottomBar;
    private CahsProgress cashprogress;
    public WardenRank rank;

    public LevelDisplayManager(Player player)
    {
      this.levelindicators = new List<LevelIndicator>();
      float PercentageProgressToNext_Profit;
      int Earnings;
      this.rank = ProfitLadderData.GetCurrentRank(player, out PercentageProgressToNext_Profit, out float _, out Earnings);
      bool IsOneAboveLast = false;
      for (int index = 0; index < 20; ++index)
      {
        this.levelindicators.Add(new LevelIndicator((WardenRank) index, this.rank >= (WardenRank) index, this.rank == (WardenRank) index, PercentageProgressToNext_Profit, ProfitLadderData.GetRankData((WardenRank) index), IsOneAboveLast));
        IsOneAboveLast = this.rank >= (WardenRank) index;
      }
      this.BottomBar = new GameObject();
      this.BottomBar.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.BottomBar.SetDrawOriginToPoint(DrawOriginPosition.Centre);
      this.BottomBar.vLocation = new Vector2(512f, 350f);
      this.springdrag = new SpringDrag_ZoneManager((float) (((double) this.levelindicators[this.levelindicators.Count - 1].Location.X - 900.0) * -1.0), Vector2.Zero, new Vector2(1024f, 370f), false);
      float num = (float) this.rank * LevelIndicator.CurrencyGap;
      if ((double) num > 512.0)
        this.springdrag.CurrentOffset.X = (float) -((double) num - 512.0);
      this.cashprogress = new CahsProgress((int) this.rank, PercentageProgressToNext_Profit, Earnings);
    }

    public void UpdateLevelDisplayManager(Player player, float DeltaTime) => this.springdrag.UpdateSpringDrag_ZoneManager(player.player.touchinput, 100f);

    public void DrawLevelDisplayManager()
    {
      this.BottomBar.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Vector2.Zero, new Vector2(1024f, 4f * Sengine.ScreenRatioUpwardsMultiplier.Y));
      this.cashprogress.DrawCahsProgress(this.springdrag.CurrentOffset + new Vector2(100f, this.BottomBar.vLocation.Y - 20f * Sengine.UltraWideSreenUpwardsMultiplier));
      for (int index = 0; index < 20; ++index)
        this.levelindicators[index].DrawLevelIndicator(this.springdrag.CurrentOffset);
      this.cashprogress.DrawCahsProgress(this.springdrag.CurrentOffset + new Vector2(100f, this.BottomBar.vLocation.Y - 20f * Sengine.UltraWideSreenUpwardsMultiplier), true);
    }
  }
}
