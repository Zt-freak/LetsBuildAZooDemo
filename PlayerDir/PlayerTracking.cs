// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.PlayerTracking
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GamePlay.Intro;
using TinyZoo.GamePlay.Results;

namespace TinyZoo.PlayerDir
{
  internal class PlayerTracking
  {
    internal static uint TotalAliensKilled;
    private uint TotalGamesStarted;
    public uint TotalGamesCompleted;
    private uint TotalFailsOnFirstLevel;
    private uint TotalOutOfBeams;
    private uint TotalEveryOneDead;
    private uint TotalVictoriesIncludingSomeoneDied;
    private uint TotalVictories_SomeoneDied;
    private uint TotalPlayButtonTaps;
    private uint TotalRetriesFromNoBeams;
    private uint TotalRetriesFromAllDead;
    private uint TotalRetriesWhereContinueAvailable;
    private uint TotalRetriesFromPauseMenu;
    private uint TimesOpenedIAPStore;
    private uint TimesPurchasedAdverts;
    private uint TimesTriedToPurchaseAdverts;
    private uint TimesTriedToPurchaseVortexMind;
    private uint TimesPurchasedVortex;
    private uint TimesPurchasedFlower;
    private uint TimesTriedToPurchaseFlower;
    public uint AliensRevived;
    private uint[] AdvertsWatched = new uint[6];
    private uint TotalAdvertsStarted;

    public PlayerTracking()
    {
    }

    public void StartedGame(Player player)
    {
      if (GameFlags.IsArcadeMode)
        return;
      ++this.TotalGamesStarted;
    }

    public void RevivedAnAlien() => ++this.AliensRevived;

    public void WatchedAdvert(AdvertLocation AdvertOrigin, Player player)
    {
      ++this.AdvertsWatched[(int) AdvertOrigin];
      AnnalyticsEvents.FinishedWatchAdvert(player);
    }

    public void TriedToWatchAdvert(Player player)
    {
      ++this.TotalAdvertsStarted;
      AnnalyticsEvents.StartedWatchAdvert(player);
    }

    public void PressedBuyUnblockedAdverts(Player player)
    {
      ++this.TimesTriedToPurchaseAdverts;
      AnnalyticsEvents.StartedPurchaseAds(player);
    }

    public void UnblockedAdverts(Player player)
    {
      ++this.TimesPurchasedAdverts;
      AnnalyticsEvents.PurchasedAds(player);
    }

    public void PressedBuyVortexMind(Player player)
    {
      ++this.TimesTriedToPurchaseVortexMind;
      AnnalyticsEvents.StartedPurchaseVortexMind(player);
    }

    public void PressedBuyFlower(Player player)
    {
      ++this.TimesTriedToPurchaseFlower;
      AnnalyticsEvents.StartedPurchaseFlower(player);
    }

    public void PurchasedVortex(Player player)
    {
      ++this.TimesPurchasedVortex;
      AnnalyticsEvents.PurchasedVortex(player);
    }

    public void PurchasedFlower(Player player)
    {
      ++this.TimesPurchasedFlower;
      AnnalyticsEvents.PurchasedFlower(player);
    }

    public void OpenedIAPScreen(Player player)
    {
      ++this.TimesOpenedIAPStore;
      if (this.TimesOpenedIAPStore != 1U)
        return;
      AnnalyticsEvents.OpenedShopForFirstTime(player);
    }

    public void TappedPlayButton(Player player)
    {
      ++this.TotalPlayButtonTaps;
      if (this.TotalPlayButtonTaps != 1U)
        return;
      AnnalyticsEvents.TappedPlayButtonFirstTime(player);
    }

    public void ContinuedFromResults() => ++this.TotalGamesCompleted;

    public void RetriedAStage(bool RetriedFromPauseMenu, GameResult resulttt, Player player)
    {
      if (RetriedFromPauseMenu)
      {
        ++this.TotalRetriesFromPauseMenu;
      }
      else
      {
        switch (resulttt)
        {
          case GameResult.PeopleDead:
            ++this.TotalRetriesFromAllDead;
            break;
          case GameResult.NoBombs:
            ++this.TotalRetriesFromNoBeams;
            break;
          case GameResult.Win:
            ++this.TotalRetriesWhereContinueAvailable;
            break;
        }
      }
    }

    public void CompletedRound(
      Player player,
      TYPENAMETHING result,
      bool SomeoneDiedOnWin,
      int DeadPeople)
    {
      if (!player.Stats.TutorialsComplete[14])
        ++this.TotalFailsOnFirstLevel;
      switch (result)
      {
        case TYPENAMETHING.Win:
          ++this.TotalVictoriesIncludingSomeoneDied;
          if (!SomeoneDiedOnWin)
            break;
          ++this.TotalVictories_SomeoneDied;
          break;
        case TYPENAMETHING.Dead:
          ++this.TotalEveryOneDead;
          break;
        case TYPENAMETHING.NoBeam:
          ++this.TotalOutOfBeams;
          break;
      }
    }

    public string[] GetTotalUniqueAliensCaught(Player player)
    {
      int num = 0;
      for (int index = 0; index < 70; ++index)
      {
        if (player.Stats.GetAliensCaptured((AnimalType) index) > 0)
          ++num;
      }
      return new string[2]
      {
        "UniqueAliensCaught",
        string.Concat((object) num)
      };
    }

    public string[] GetTimesAdsTryPurchase() => new string[2]
    {
      "TimesAdsTryPurchase",
      string.Concat((object) this.TimesTriedToPurchaseAdverts)
    };

    public string[] GetTimesAdsPurchased() => new string[2]
    {
      "TimesAdsPurchased",
      string.Concat((object) this.TimesPurchasedAdverts)
    };

    public string[] GetTimesVortexTryPurchase() => new string[2]
    {
      "TimesVortexTryPurchase",
      string.Concat((object) this.TimesTriedToPurchaseVortexMind)
    };

    public string[] GetTimesFlowerPurchased() => new string[2]
    {
      "TimesFlowerPurchased",
      string.Concat((object) this.TimesPurchasedFlower)
    };

    public string[] GetTimesTriedToPurchaseFlower() => new string[2]
    {
      "TimesTriedToPurchaseFlower",
      string.Concat((object) this.TimesTriedToPurchaseFlower)
    };

    public string[] GetTimesVortexPurchased() => new string[2]
    {
      "TimesVortexPurchased",
      string.Concat((object) this.TimesPurchasedVortex)
    };

    public string[] GetTotalUniqueAliensResearched(Player player) => new string[2]
    {
      "AliensResearched",
      string.Concat((object) player.Stats.research.AnimalsResearchedLength())
    };

    public string[] GetHasLinkedTDD(Player player) => new string[2]
    {
      "TDDLInk",
      !player.Stats.TDDLink ? "false" : "true"
    };

    public string[] GetTotalAliensHeldInCells(Player player) => new string[2]
    {
      "TotalInCellBlocks",
      string.Concat((object) player.prisonlayout.cellblockcontainer.GetTotalAliensInCellBlocks())
    };

    public string[] GetTotalAliensInGraves(Player player) => new string[2]
    {
      "TotalInGraveyard",
      string.Concat((object) player.prisonlayout.cellblockcontainer.GetTotalAliensInGraveYards())
    };

    public string[] GetTotalAliensInHoldingCells(Player player) => new string[2]
    {
      "TotalInHoldingCells",
      string.Concat((object) player.prisonlayout.cellblockcontainer.GetTotalAliensInHoldingCells())
    };

    public string[] GetTotalAliensEverCaught(Player player)
    {
      int num = 0;
      for (int index = 0; index < 70; ++index)
      {
        if (player.Stats.GetAliensCaptured((AnimalType) index) > 0)
          num += player.Stats.GetAliensCaptured((AnimalType) index);
      }
      return new string[2]
      {
        "AllTimeAliensCaught",
        string.Concat((object) num)
      };
    }

    public string[] GetTotalAliensCurrentlyHeld(Player player) => new string[2]
    {
      "TotalAliensHeld",
      string.Concat((object) player.prisonlayout.cellblockcontainer.GetTotalAliensHeldIncludingDeadAndHoldingCells())
    };

    public string[] GetTotalRetriesFromNoBeams() => new string[2]
    {
      "RetriesFromNoBeams",
      string.Concat((object) this.TotalRetriesFromNoBeams)
    };

    public string[] GetTotalRetriesFromAllDead() => new string[2]
    {
      "RetriesFromAllDead",
      string.Concat((object) this.TotalRetriesFromAllDead)
    };

    public string[] GetTotalRetriesWhereContinueAvailable() => new string[2]
    {
      "RetriesWhereContinueAvailable",
      string.Concat((object) this.TotalRetriesWhereContinueAvailable)
    };

    public string[] GetTotalRetriesAll() => new string[2]
    {
      "TotalRetries",
      this.TotalRetriesWhereContinueAvailable.ToString() + (object) this.TotalRetriesFromPauseMenu + (object) this.TotalRetriesFromAllDead + (object) this.TotalRetriesFromNoBeams
    };

    public string[] GetTotalRetriesFromPauseMenu() => new string[2]
    {
      "RetriesFromPauseMenu",
      string.Concat((object) this.TotalRetriesFromPauseMenu)
    };

    public string[] GetTotalVictoriesIncludingSomeoneDied() => new string[2]
    {
      "VictoriesIncludingSomeoneDied",
      string.Concat((object) this.TotalVictoriesIncludingSomeoneDied)
    };

    public string[] GetTotalVictories_SomeoneDied() => new string[2]
    {
      "Victories_SomeoneDied",
      string.Concat((object) this.TotalVictories_SomeoneDied)
    };

    public string[] GetTotalFailsOnFirstLevel() => new string[2]
    {
      "FailsOnFirstLevel",
      string.Concat((object) this.TotalFailsOnFirstLevel)
    };

    public string[] GetTotalOutOfBeams() => new string[2]
    {
      "OutOfBeams",
      string.Concat((object) this.TotalOutOfBeams)
    };

    public string[] GetTotalEveryOneDead() => new string[2]
    {
      "TotalEveryOneDead",
      string.Concat((object) this.TotalEveryOneDead)
    };

    public string[] GetTotalGamesStarted() => new string[2]
    {
      "TotalGamesStarted",
      string.Concat((object) this.TotalGamesStarted)
    };

    public string[] GetTotalGamesCompleted() => new string[2]
    {
      "TotalGamesCompleted",
      string.Concat((object) this.TotalGamesCompleted)
    };

    public string[] GetTotalAliensKilled() => new string[2]
    {
      "TotalAliensKilled",
      string.Concat((object) PlayerTracking.TotalAliensKilled)
    };

    public string[] GetTotalAdvertsShuffleIntake() => new string[2]
    {
      "TotalAdsShuffleIntake",
      string.Concat((object) this.AdvertsWatched[0])
    };

    public string[] GetTotalAdvertsSpeedUpResearch() => new string[2]
    {
      "TotalAdsSpeedUpResearch",
      string.Concat((object) this.AdvertsWatched[1])
    };

    public string[] GetTotalAdvertsRetryOnFullDeath() => new string[2]
    {
      "TotalAdsRetryOnFullDeath",
      string.Concat((object) this.AdvertsWatched[2])
    };

    public string[] GetTotalAdvertsRetryOnPartialDeath() => new string[2]
    {
      "TotalAdsRetryOnPartialDeath",
      string.Concat((object) this.AdvertsWatched[3])
    };

    public string[] GetTotalAdvertsRevivePrisoner() => new string[2]
    {
      "TotalAdsRevivePrisoner",
      string.Concat((object) this.AdvertsWatched[4])
    };

    public string[] GetTotalAdvertsActivateSpeedUp() => new string[2]
    {
      "TotalAdsActivateSpeedUp",
      string.Concat((object) this.AdvertsWatched[5])
    };

    public string[] GetTotalAdvertsStarted()
    {
      uint num = 0;
      for (int index = 0; index < this.AdvertsWatched.Length; ++index)
        num += this.AdvertsWatched[index];
      return new string[2]
      {
        "TotalAdsStarted",
        string.Concat((object) this.TotalAdvertsStarted)
      };
    }

    public PlayerTracking(Reader reader)
    {
      int num1 = (int) reader.ReadUInt("t", ref this.TotalRetriesFromNoBeams);
      int num2 = (int) reader.ReadUInt("t", ref this.TotalRetriesFromAllDead);
      int num3 = (int) reader.ReadUInt("t", ref this.TotalRetriesWhereContinueAvailable);
      int num4 = (int) reader.ReadUInt("t", ref this.TotalRetriesFromPauseMenu);
      int num5 = (int) reader.ReadUInt("t", ref this.TotalVictoriesIncludingSomeoneDied);
      int num6 = (int) reader.ReadUInt("t", ref this.TotalVictories_SomeoneDied);
      int num7 = (int) reader.ReadUInt("t", ref this.TotalFailsOnFirstLevel);
      int num8 = (int) reader.ReadUInt("t", ref this.TotalOutOfBeams);
      int num9 = (int) reader.ReadUInt("t", ref this.TotalEveryOneDead);
      int num10 = (int) reader.ReadUInt("t", ref this.TotalGamesStarted);
      int num11 = (int) reader.ReadUInt("t", ref this.TotalGamesCompleted);
      int num12 = (int) reader.ReadUInt("t", ref PlayerTracking.TotalAliensKilled);
      int num13 = (int) reader.ReadUInt("t", ref this.TimesOpenedIAPStore);
      int num14 = (int) reader.ReadUInt("t", ref this.TimesTriedToPurchaseAdverts);
      int num15 = (int) reader.ReadUInt("t", ref this.TimesPurchasedAdverts);
      int num16 = (int) reader.ReadUInt("t", ref this.AliensRevived);
      int num17 = (int) reader.ReadUInt("t", ref this.TotalAdvertsStarted);
      int _out = 0;
      int num18 = (int) reader.ReadInt("t", ref _out);
      this.AdvertsWatched = new uint[6];
      for (int index = 0; index < _out; ++index)
      {
        int num19 = (int) reader.ReadUInt("t", ref this.AdvertsWatched[index]);
      }
      int num20 = (int) reader.ReadUInt("t", ref this.TotalPlayButtonTaps);
      int num21 = (int) reader.ReadUInt("t", ref this.TimesPurchasedVortex);
      int num22 = (int) reader.ReadUInt("t", ref this.TimesTriedToPurchaseVortexMind);
      int num23 = (int) reader.ReadUInt("t", ref this.TimesPurchasedFlower);
      int num24 = (int) reader.ReadUInt("t", ref this.TimesTriedToPurchaseFlower);
    }

    public void SavePlayerTracking(Writer writer)
    {
      writer.WriteUInt("t", this.TotalRetriesFromNoBeams);
      writer.WriteUInt("t", this.TotalRetriesFromAllDead);
      writer.WriteUInt("t", this.TotalRetriesWhereContinueAvailable);
      writer.WriteUInt("t", this.TotalRetriesFromPauseMenu);
      writer.WriteUInt("t", this.TotalVictoriesIncludingSomeoneDied);
      writer.WriteUInt("t", this.TotalVictories_SomeoneDied);
      writer.WriteUInt("t", this.TotalFailsOnFirstLevel);
      writer.WriteUInt("t", this.TotalOutOfBeams);
      writer.WriteUInt("t", this.TotalEveryOneDead);
      writer.WriteUInt("t", this.TotalGamesStarted);
      writer.WriteUInt("t", this.TotalGamesCompleted);
      writer.WriteUInt("t", PlayerTracking.TotalAliensKilled);
      writer.WriteUInt("t", this.TimesOpenedIAPStore);
      writer.WriteUInt("t", this.TimesTriedToPurchaseAdverts);
      writer.WriteUInt("t", this.TimesPurchasedAdverts);
      writer.WriteUInt("t", this.AliensRevived);
      writer.WriteUInt("t", this.TotalAdvertsStarted);
      writer.WriteInt("t", this.AdvertsWatched.Length);
      for (int index = 0; index < this.AdvertsWatched.Length; ++index)
        writer.WriteUInt("t", this.AdvertsWatched[index]);
      writer.WriteUInt("t", this.TotalPlayButtonTaps);
      writer.WriteUInt("t", this.TimesPurchasedVortex);
      writer.WriteUInt("t", this.TimesTriedToPurchaseVortexMind);
      writer.WriteUInt("t", this.TimesPurchasedFlower);
      writer.WriteUInt("t", this.TimesTriedToPurchaseFlower);
    }
  }
}
