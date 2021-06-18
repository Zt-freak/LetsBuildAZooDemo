// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.RewardPack
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.sponsor;

namespace TinyZoo.Z_Quests
{
  internal class RewardPack
  {
    public REWARDTYPE reward;
    public int TotalReward;
    public int Days;

    public RewardPack(REWARDTYPE rewardtype, int COunnt, int _Days = 0)
    {
      this.Days = _Days;
      this.reward = rewardtype;
      this.TotalReward = COunnt;
    }

    public void ProcessReward(Player player)
    {
      switch (this.reward)
      {
        case REWARDTYPE.Money:
          player.Stats.GiveCash(this.TotalReward, player, true);
          break;
        case REWARDTYPE.ReseacrhPoints:
          player.unlocks.ResearchPoints += this.TotalReward;
          LiveStats.EarnedResearch = true;
          break;
        case REWARDTYPE.BusRoute:
          player.busroutes.UnlockedRoutes[this.TotalReward] = true;
          break;
        case REWARDTYPE.DailySponsorship:
          player.sponsorships.AddSponsorship(new CurrentSponsor(this.TotalReward, this.Days, SponsorshipType.Research));
          break;
        default:
          throw new Exception("NO REWARD Given SET");
      }
    }

    public string GetRewardString()
    {
      switch (this.reward)
      {
        case REWARDTYPE.Money:
          return "$" + (object) this.TotalReward;
        case REWARDTYPE.ReseacrhPoints:
          return this.TotalReward.ToString() + " Research points";
        case REWARDTYPE.DailySponsorship:
          return "$" + (object) this.TotalReward + " per day for " + (object) this.Days + " days";
        default:
          return "NO REWARD STRING SET";
      }
    }
  }
}
