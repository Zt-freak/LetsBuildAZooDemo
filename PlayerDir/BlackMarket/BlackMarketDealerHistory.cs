// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.BlackMarket.BlackMarketDealerHistory
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir.BlackMarket
{
  internal class BlackMarketDealerHistory
  {
    public AnimalType animalType;
    public int animalsSoldToThisDealer;
    public int animalsBoughtFromThisDealer;
    public int timeServedInPrison;
    public float relationship_float;

    public BlackMarketDealerHistory(AnimalType _animalType)
    {
      this.animalType = _animalType;
      this.relationship_float = 0.5f;
    }

    public void OnAnimalSold(int soldThisMany) => this.animalsSoldToThisDealer += soldThisMany;

    public void OnAnimalBought() => ++this.animalsBoughtFromThisDealer;

    public BlackMarketDealerHistory(Reader reader)
    {
      int _out = 0;
      int num1 = (int) reader.ReadInt("b", ref _out);
      this.animalType = (AnimalType) _out;
      int num2 = (int) reader.ReadInt("b", ref this.animalsSoldToThisDealer);
      int num3 = (int) reader.ReadInt("b", ref this.animalsBoughtFromThisDealer);
      int num4 = (int) reader.ReadInt("b", ref this.timeServedInPrison);
    }

    public void SaveBlackMarketDealerHistory(Writer writer)
    {
      writer.WriteInt("b", (int) this.animalType);
      writer.WriteInt("b", this.animalsSoldToThisDealer);
      writer.WriteInt("b", this.animalsBoughtFromThisDealer);
      writer.WriteInt("b", this.timeServedInPrison);
    }
  }
}
