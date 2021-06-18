// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade.BigAnimalFrame.AnimalTradeGrid
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Z_Quests;

namespace TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade.BigAnimalFrame
{
  internal class AnimalTradeGrid
  {
    private BigAnimalTradeFrame MaleTrade;
    private BigAnimalTradeFrame FemaleTrade;
    private BigAnimalTradeFrame MaleReward;
    private BigAnimalTradeFrame FemaleReward;
    private TradeArrow tradearrow;
    private Vector2 VScale;
    private GameObjectNineSlice gameobjectninsliceLeft;
    private GameObjectNineSlice gameobjectninsliceRight;

    public AnimalTradeGrid(
      Player player,
      bool HasTheseAnimals,
      QuestPack Ref_Zquest,
      bool StillHasBreedingPair,
      int BoysUsed)
    {
      float y1 = (float) (300.0 + 210.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y);
      float y2 = 300f;
      if (DebugFlags.IsPCVersion)
      {
        float num1 = 355f;
        y1 = 460f;
        float num2 = 110f;
        y2 = num1 - num2 * Sengine.ScreenRatioUpwardsMultiplier.Y + num2;
      }
      this.gameobjectninsliceRight = new GameObjectNineSlice(new Rectangle(877, 350, 21, 21), 7);
      this.gameobjectninsliceLeft = new GameObjectNineSlice(new Rectangle(877, 350, 21, 21), 7);
      this.gameobjectninsliceRight.scale = 2f;
      this.gameobjectninsliceLeft.scale = 2f;
      this.VScale = new Vector2(235f, 450f);
      this.FemaleReward = new BigAnimalTradeFrame(Ref_Zquest.GetThisAnimal, true, true);
      this.MaleReward = new BigAnimalTradeFrame(Ref_Zquest.GetThisAnimal, false, true);
      this.MaleReward.SetString("Get 1", new Color(0.05490196f, 0.5882353f, 0.3176471f));
      this.FemaleReward.SetString("Get 1", new Color(0.05490196f, 0.5882353f, 0.3176471f));
      float x = 300f;
      this.FemaleReward.Location = new Vector2(1024f - x, y2);
      this.MaleReward.Location = new Vector2(1024f - x, y1);
      this.gameobjectninsliceRight.vLocation = this.FemaleReward.Location;
      this.gameobjectninsliceRight.vLocation.Y += (float) (((double) this.MaleReward.Location.Y - (double) this.FemaleReward.Location.Y) * 0.5);
      this.FemaleTrade = new BigAnimalTradeFrame(Ref_Zquest.trades_ListOnlyOne[0].animal, true, false, Ref_Zquest.trades_ListOnlyOne[0].VariantIndex);
      this.MaleTrade = new BigAnimalTradeFrame(Ref_Zquest.trades_ListOnlyOne[0].animal, false, false, Ref_Zquest.trades_ListOnlyOne[0].VariantIndex);
      this.FemaleTrade.Location = new Vector2(x, y2);
      this.MaleTrade.Location = new Vector2(x, y1);
      this.gameobjectninsliceLeft.vLocation = this.FemaleTrade.Location;
      this.gameobjectninsliceLeft.vLocation.Y = this.gameobjectninsliceRight.vLocation.Y;
      if (!player.Stats.research.HasThisAnimalBeenResearched(Ref_Zquest.trades_ListOnlyOne[0].animal, Ref_Zquest.trades_ListOnlyOne[0].VariantIndex) && !player.Stats.research.HasThisAnimalBeenResearched(Ref_Zquest.trades_ListOnlyOne[0].animal, Ref_Zquest.trades_ListOnlyOne[0].VariantIndex))
      {
        this.FemaleTrade.DarkenAnimal();
        this.MaleTrade.DarkenAnimal();
      }
      int totalOfThisAlien1 = player.prisonlayout.cellblockcontainer.GetTotalOfThisALien(Ref_Zquest.trades_ListOnlyOne[0].animal, Ref_Zquest.trades_ListOnlyOne[0].VariantIndex, true);
      int totalOfThisAlien2 = player.prisonlayout.cellblockcontainer.GetTotalOfThisALien(Ref_Zquest.trades_ListOnlyOne[0].animal, Ref_Zquest.trades_ListOnlyOne[0].VariantIndex, false);
      int num = Ref_Zquest.trades_ListOnlyOne[0].Total.GetUnvallidatedValue() - BoysUsed;
      Color _StringColour = new Color(ColourData.Z_TextGreen);
      if (!StillHasBreedingPair)
      {
        this.FemaleTrade.SetString(string.Concat((object) totalOfThisAlien1), _StringColour);
        this.FemaleTrade.SetOverAllAlphaMultiplier(0.3f);
        this.MaleTrade.SetString(string.Concat((object) totalOfThisAlien2), _StringColour);
        this.MaleTrade.SetOverAllAlphaMultiplier(0.3f);
      }
      else
      {
        if (totalOfThisAlien1 < num)
          _StringColour = new Color(0.8f, 0.2f, 0.2f);
        this.FemaleTrade.SetString(totalOfThisAlien1.ToString() + "/" + (object) num, _StringColour);
        if (num == 0 || !StillHasBreedingPair)
          this.FemaleTrade.SetOverAllAlphaMultiplier(0.3f);
        _StringColour = new Color(ColourData.Z_TextGreen);
        if (totalOfThisAlien2 < BoysUsed)
          _StringColour = new Color(0.8f, 0.2f, 0.2f);
        this.MaleTrade.SetString(totalOfThisAlien2.ToString() + "/" + (object) BoysUsed, _StringColour);
        if (BoysUsed == 0 || !StillHasBreedingPair)
          this.MaleTrade.SetOverAllAlphaMultiplier(0.3f);
      }
      this.tradearrow = new TradeArrow(totalOfThisAlien1 + totalOfThisAlien2, Ref_Zquest.trades_ListOnlyOne[0].Total.GetUnvallidatedValue(), !HasTheseAnimals && !StillHasBreedingPair);
      this.tradearrow.vLocation = new Vector2(512f, y2 + (float) (((double) y1 - (double) y2) * 0.5));
    }

    public void UpdateAnimalTradeGrid(float DeltaTime, Player player)
    {
      this.FemaleReward.UpdateBigAnimalTradeFrame(DeltaTime, player);
      this.MaleReward.UpdateBigAnimalTradeFrame(DeltaTime, player);
      this.FemaleTrade.UpdateBigAnimalTradeFrame(DeltaTime, player);
      this.MaleTrade.UpdateBigAnimalTradeFrame(DeltaTime, player);
    }

    public void DrawAnimalTradeGrid(Vector2 Offset)
    {
      float num = 1f;
      if (DebugFlags.IsPCVersion)
        num = 0.5f;
      this.gameobjectninsliceRight.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VScale * Sengine.ScreenRatioUpwardsMultiplier * num);
      this.gameobjectninsliceLeft.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VScale * Sengine.ScreenRatioUpwardsMultiplier * num);
      this.FemaleReward.DrawBigAnimalTradeFrame(Offset);
      this.MaleReward.DrawBigAnimalTradeFrame(Offset);
      this.FemaleTrade.DrawBigAnimalTradeFrame(Offset);
      this.MaleTrade.DrawBigAnimalTradeFrame(Offset);
      this.tradearrow.DrawTradeArrow(Offset);
    }
  }
}
