// Decompiled with JetBrains decompiler
// Type: TinyZoo.ProfitLadderData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using TinyZoo.ProfitLadder.LevelSummary;

namespace TinyZoo
{
  internal class ProfitLadderData
  {
    internal static RandkInfo[] RankData;

    internal static RandkInfo GetRankData(WardenRank rank)
    {
      ProfitLadderData.Check();
      return ProfitLadderData.RankData[(int) rank];
    }

    private static void Check()
    {
      if (ProfitLadderData.RankData != null)
        return;
      ProfitLadderData.RankData = new RandkInfo[20];
      ProfitLadderData.RankData[0] = new RandkInfo(0, new Rectangle(0, 537, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[1] = new RandkInfo(3, new Rectangle(26, 537, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[2] = new RandkInfo(6, new Rectangle(52, 537, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[3] = new RandkInfo(9, new Rectangle(78, 537, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[4] = new RandkInfo(12, new Rectangle(104, 537, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[5] = new RandkInfo(16, new Rectangle(130, 537, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[6] = new RandkInfo(21, new Rectangle(156, 537, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[7] = new RandkInfo(24, new Rectangle(182, 537, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[8] = new RandkInfo(27, new Rectangle(208, 537, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[9] = new RandkInfo(29, new Rectangle(334, 599, 29, 29), StringID.Placeholder);
      ProfitLadderData.RankData[10] = new RandkInfo(31, new Rectangle(395, 509, 37, 31), StringID.Placeholder);
      ProfitLadderData.RankData[11] = new RandkInfo(33, new Rectangle(234, 537, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[12] = new RandkInfo(35, new Rectangle(260, 537, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[13] = new RandkInfo(37, new Rectangle(286, 537, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[14] = new RandkInfo(39, new Rectangle(312, 537, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[15] = new RandkInfo(41, new Rectangle(338, 537, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[16] = new RandkInfo(43, new Rectangle(364, 537, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[17] = new RandkInfo(45, new Rectangle(282, 599, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[18] = new RandkInfo(47, new Rectangle(308, 599, 25, 27), StringID.Placeholder);
      ProfitLadderData.RankData[19] = new RandkInfo(49, new Rectangle(433, 509, 37, 31), StringID.Placeholder);
      for (int RankIndex = 0; RankIndex < ProfitLadderData.RankData.Length; ++RankIndex)
      {
        if (ProfitLadderData.RankData[RankIndex] == null)
          throw new Exception("NOPE");
        ProfitLadderData.RankData[RankIndex].CalculateRank(RankIndex);
      }
    }

    internal static WardenRank GetCurrentRank(
      Player player,
      out float PercentageProgressToNext_Profit,
      out float PercentProgressToNextPeople,
      out int Earnings)
    {
      ProfitLadderData.Check();
      PercentProgressToNextPeople = 0.0f;
      player.prisonlayout.cellblockcontainer.GetTotalAliensInCellBlocks();
      Earnings = player.prisonlayout.GetDailyEanings(true, out int _, out int _, player);
      int num1 = Earnings;
      for (int index = 0; index < ProfitLadderData.RankData.Length; ++index)
      {
        if (num1 < ProfitLadderData.RankData[index].IncomeMin)
        {
          float num2 = (float) (ProfitLadderData.RankData[index].IncomeMin - ProfitLadderData.RankData[index - 1].IncomeMin);
          int num3 = num1 - ProfitLadderData.RankData[index - 1].IncomeMin;
          PercentageProgressToNext_Profit = (float) num3 / num2;
          return (WardenRank) (index - 1);
        }
      }
      PercentageProgressToNext_Profit = 0.0f;
      return WardenRank.HolderOfInfiniteMoneyAndPower;
    }
  }
}
