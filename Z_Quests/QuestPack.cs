// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.QuestPack
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.Objects;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_Quests
{
  internal class QuestPack
  {
    public AnimalType GetThisAnimal;
    public List<TradeInfo> trades_ListOnlyOne;
    public NumberObfiscatorV1 Shipping;
    public CityName city;

    public QuestPack(
      AnimalType _GetThisAnimal,
      CityName _city,
      TradeInfo tradeinfo,
      int ShippingCost)
    {
      this.city = _city;
      this.trades_ListOnlyOne = new List<TradeInfo>();
      this.trades_ListOnlyOne.Add(tradeinfo);
      this.GetThisAnimal = _GetThisAnimal;
      this.Shipping = new NumberObfiscatorV1();
      this.Shipping.ForceSetNewValue(ShippingCost);
    }

    public string GetQuestObjectiveDescription()
    {
      string str1 = "";
      if (this.trades_ListOnlyOne[0].Total.GetUnvallidatedValue() == 0)
        str1 = "Claim this gift now!";
      string str2 = str1 + "~";
      return this.Shipping.GetUnvallidatedValue() <= 0 ? str2 + "Shipping is free!" : str2 + "International shipping fee: $" + (object) this.Shipping.GetUnvallidatedValue();
    }
  }
}
