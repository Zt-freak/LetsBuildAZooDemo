// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.BlackMarket.BlackMarketStats
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir.BlackMarket
{
  internal class BlackMarketStats
  {
    private List<BlackMarketDealerHistory> dealerHistories;

    public BlackMarketStats() => this.dealerHistories = new List<BlackMarketDealerHistory>();

    public BlackMarketDealerHistory GetDealerHistoryForThisBlackMarketDealer(
      AnimalType animalType)
    {
      foreach (BlackMarketDealerHistory dealerHistory in this.dealerHistories)
      {
        if (dealerHistory.animalType == animalType)
          return dealerHistory;
      }
      BlackMarketDealerHistory marketDealerHistory = new BlackMarketDealerHistory(animalType);
      this.dealerHistories.Add(marketDealerHistory);
      return marketDealerHistory;
    }

    public BlackMarketStats(Reader reader)
    {
      this.dealerHistories = new List<BlackMarketDealerHistory>();
      int _out = 0;
      int num = (int) reader.ReadInt("b", ref _out);
      for (int index = 0; index < _out; ++index)
        this.dealerHistories.Add(new BlackMarketDealerHistory(reader));
    }

    public void SaveBlackMarketStats(Writer writer)
    {
      writer.WriteInt("b", this.dealerHistories.Count);
      for (int index = 0; index < this.dealerHistories.Count; ++index)
        this.dealerHistories[index].SaveBlackMarketDealerHistory(writer);
    }
  }
}
