// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.AnimalSelectionManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BalanceSystems.Animals.SellCosts;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Quests;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.InfoBox;
using TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.SelectionGrid;
using TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.TradeList;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection
{
  internal class AnimalSelectionManager
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private CustomAnimalSelectionGridFrame animalSelectionGridFrame;
    private AnimalLocationInfoBox animalLocationInfoBox;
    private AnimalInfoBox animalInfoBox;
    private SimpleTextHandler noAnimalsText;
    private TradeListFrame tradeListFrame;
    private AnimalSelectionUIType UIType;

    public QuestPack refQuest { get; private set; }

    public AnimalSelectionManager(
      QuestPack quest,
      List<AnimalRenderDescriptor> animalsNeeded,
      Player player,
      float BaseScale,
      Vector2 ForcedSize,
      int frameCount_X = 7,
      int frameCount_Y = 5)
    {
      this.refQuest = quest;
      this.UIType = AnimalSelectionUIType.TradeQuest;
      this.SetUp(this.GetEntriesForPlayerTrade(quest, player), animalsNeeded, player, BaseScale, ForcedSize, frameCount_X, frameCount_Y);
    }

    public AnimalSelectionManager(
      AnimalSelectionUIType _UIType,
      Player player,
      float BaseScale,
      Vector2 forcedSize,
      AnimalType viewThisAnimalTypeOnly = AnimalType.None,
      int frameCount_X = 7,
      int frameCount_Y = 5)
    {
      this.UIType = _UIType;
      List<PrisonerInfo> listOfPlayerAnimals = AnimalSelectionManager.GetListOfPlayerAnimals(player, viewThisAnimalTypeOnly);
      List<CustomAnimalSelectionEntry> animalsToTrade = new List<CustomAnimalSelectionEntry>();
      for (int index = 0; index < listOfPlayerAnimals.Count; ++index)
        animalsToTrade.Add(new CustomAnimalSelectionEntry(listOfPlayerAnimals[index]));
      this.SetUp(animalsToTrade, (List<AnimalRenderDescriptor>) null, player, BaseScale, forcedSize, frameCount_X, frameCount_Y);
    }

    private void SetUp(
      List<CustomAnimalSelectionEntry> animalsToTrade,
      List<AnimalRenderDescriptor> animalsNeeded,
      Player player,
      float BaseScale,
      Vector2 ForcedSize,
      int frameCount_X = 7,
      int frameCount_Y = 5)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      this.customerFrame = new CustomerFrame(ForcedSize, CustomerFrameColors.Brown, BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      float x1 = defaultXbuffer + vector2.X;
      float y1 = defaultYbuffer + vector2.Y;
      this.animalSelectionGridFrame = new CustomAnimalSelectionGridFrame(animalsToTrade, player, BaseScale, frameCount_X, frameCount_Y, this.UIType == AnimalSelectionUIType.BlackMarket);
      Vector2 size1 = this.animalSelectionGridFrame.GetSize();
      this.animalSelectionGridFrame.location = new Vector2(x1, y1) + size1 * 0.5f;
      float x2 = (float) ((double) ForcedSize.X - ((double) size1.X + (double) defaultXbuffer) - (double) defaultXbuffer * 2.0);
      this.animalInfoBox = new AnimalInfoBox(BaseScale, new Vector2(x2, size1.Y), this.UIType);
      Vector2 size2 = this.animalInfoBox.GetSize();
      this.animalInfoBox.location = new Vector2(x1, y1) + size2 * 0.5f;
      float x3 = x1 + (size1.X + defaultXbuffer);
      this.animalInfoBox.location = new Vector2(x3, y1) + this.animalInfoBox.GetSize() * 0.5f;
      float num = x3 + (size2.X + defaultXbuffer);
      if (animalsToTrade.Count == 0)
      {
        string TextToWrite = "You do not have any animals that can be used for this trade.";
        if (this.UIType == AnimalSelectionUIType.Donation)
          TextToWrite = "You do not have any animals to donate.";
        this.noAnimalsText = new SimpleTextHandler(TextToWrite, true, (float) ((double) size1.X / 1024.0 * 0.899999976158142), BaseScale, AutoComplete: true);
        this.noAnimalsText.SetAllColours(ColourData.Z_Cream);
        this.noAnimalsText.Location = this.animalSelectionGridFrame.location;
      }
      float y2 = y1 + (this.animalSelectionGridFrame.GetSize().Y + defaultYbuffer);
      float x4 = defaultXbuffer + vector2.X;
      float y3 = ForcedSize.Y - defaultYbuffer * 2f - size1.Y - defaultYbuffer;
      if (this.UIType == AnimalSelectionUIType.TradeQuest)
      {
        this.animalLocationInfoBox = new AnimalLocationInfoBox(animalsNeeded[0].bodyAnimalType, animalsNeeded[0].variant, player, BaseScale);
        Vector2 size3 = this.animalLocationInfoBox.GetSize();
        this.animalLocationInfoBox.location = new Vector2(x4, y2) + size3 * 0.5f;
        x4 += size3.X + defaultXbuffer;
      }
      float x5 = ForcedSize.X - (x4 - vector2.X) - defaultXbuffer;
      this.tradeListFrame = new TradeListFrame(animalsNeeded, BaseScale, new Vector2(x5, y3), this.UIType);
      this.tradeListFrame.location = new Vector2(x4, y2) + this.tradeListFrame.GetSize() * 0.5f;
    }

    private List<CustomAnimalSelectionEntry> GetEntriesForPlayerTrade(
      QuestPack quest,
      Player player)
    {
      List<PrisonerInfo> sameSpeciesButWrongVariant;
      List<PrisonerInfo> beIncludedInTrade = AnimalSelectionManager.GetListOfPlayersAnimalsThatCanBeIncludedInTrade(quest, player, out sameSpeciesButWrongVariant);
      List<CustomAnimalSelectionEntry> animalSelectionEntryList = new List<CustomAnimalSelectionEntry>();
      for (int index = 0; index < beIncludedInTrade.Count; ++index)
        animalSelectionEntryList.Add(new CustomAnimalSelectionEntry(beIncludedInTrade[index]));
      for (int index = 0; index < sameSpeciesButWrongVariant.Count; ++index)
        animalSelectionEntryList.Add(new CustomAnimalSelectionEntry(sameSpeciesButWrongVariant[index], true, "This animal does not meet the trading requirements!"));
      return animalSelectionEntryList;
    }

    public static List<PrisonerInfo> GetListOfPlayersAnimalsThatCanBeIncludedInTrade(
      QuestPack quest,
      Player player)
    {
      return AnimalSelectionManager.GetListOfPlayersAnimalsThatCanBeIncludedInTrade(quest, player, out List<PrisonerInfo> _);
    }

    public static bool HasEnoughAnimalsToDoTrade(QuestPack quest, Player player) => AnimalSelectionManager.GetListOfPlayersAnimalsThatCanBeIncludedInTrade(quest, player, out List<PrisonerInfo> _, true).Count >= quest.trades_ListOnlyOne[0].Total.GetUnvallidatedValue();

    public static List<PrisonerInfo> GetListOfPlayersAnimalsThatCanBeIncludedInTrade(
      QuestPack quest,
      Player player,
      out List<PrisonerInfo> sameSpeciesButWrongVariant,
      bool IsQuickCheckForBooleanOnly = false)
    {
      List<PrisonerInfo> prisonerInfoList = new List<PrisonerInfo>();
      sameSpeciesButWrongVariant = new List<PrisonerInfo>();
      List<PrisonZone> prisonzones = player.prisonlayout.cellblockcontainer.prisonzones;
      for (int index = 0; index < prisonzones.Count; ++index)
      {
        foreach (PrisonerInfo prisoner in prisonzones[index].prisonercontainer.prisoners)
        {
          if (!prisoner.IsDead)
          {
            foreach (TradeInfo tradeInfo in quest.trades_ListOnlyOne)
            {
              if (tradeInfo.animal == prisoner.intakeperson.animaltype)
              {
                if (tradeInfo.VariantIndex == -1 || tradeInfo.VariantIndex == 0 || tradeInfo.VariantIndex == prisoner.intakeperson.CLIndex)
                {
                  prisonerInfoList.Add(prisoner);
                  if (IsQuickCheckForBooleanOnly)
                  {
                    if (prisonerInfoList.Count >= tradeInfo.Total.GetUnvallidatedValue())
                      break;
                  }
                }
                else
                  sameSpeciesButWrongVariant.Add(prisoner);
              }
            }
          }
        }
      }
      return prisonerInfoList;
    }

    public static List<PrisonerInfo> GetListOfPlayerHybridAnimals(Player player)
    {
      List<PrisonerInfo> prisonerInfoList = new List<PrisonerInfo>();
      List<PrisonZone> prisonzones = player.prisonlayout.cellblockcontainer.prisonzones;
      for (int index = 0; index < prisonzones.Count; ++index)
      {
        foreach (PrisonerInfo prisoner in prisonzones[index].prisonercontainer.prisoners)
        {
          if (!prisoner.IsDead && prisoner.intakeperson.HeadType != AnimalType.None && prisoner.intakeperson.animaltype != prisoner.intakeperson.HeadType)
            prisonerInfoList.Add(prisoner);
        }
      }
      return prisonerInfoList;
    }

    public static List<PrisonerInfo> GetListOfPlayerAnimals(
      Player player,
      AnimalType filterThisAnimalType = AnimalType.None)
    {
      List<PrisonerInfo> prisonerInfoList = new List<PrisonerInfo>();
      List<PrisonZone> prisonzones = player.prisonlayout.cellblockcontainer.prisonzones;
      for (int index = 0; index < prisonzones.Count; ++index)
      {
        foreach (PrisonerInfo prisoner in prisonzones[index].prisonercontainer.prisoners)
        {
          if (!prisoner.IsDead)
          {
            if (filterThisAnimalType != AnimalType.None)
            {
              if (prisoner.intakeperson.animaltype == filterThisAnimalType)
                prisonerInfoList.Add(prisoner);
            }
            else
              prisonerInfoList.Add(prisoner);
          }
        }
      }
      return prisonerInfoList;
    }

    private int GetBlackMarketSellPriceOfThisAnimal(PrisonerInfo animal) => this.UIType == AnimalSelectionUIType.BlackMarket ? AnimalSellCostCalculator.GetSellCostOfPlayerAnimal(animal) : -1;

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateAnimalSelectionManager(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out List<PrisonerInfo> animalsSelected)
    {
      animalsSelected = (List<PrisonerInfo>) null;
      offset += this.location;
      PrisonerInfo animal1 = this.animalSelectionGridFrame.UpdateCustomAnimalSelectionGridFrame(player, DeltaTime, offset);
      bool flag = this.tradeListFrame.ReadyForTrade;
      if (this.UIType == AnimalSelectionUIType.BlackMarket || this.UIType == AnimalSelectionUIType.Donation)
        flag = false;
      if (animal1 != null)
        this.animalInfoBox.SetInfoForThisAnimal(animal1, this.tradeListFrame.tradeList.Contains(animal1), flag, this.GetBlackMarketSellPriceOfThisAnimal(animal1));
      PrisonerInfo animal2 = this.animalInfoBox.UpdateAnimalInfoBox(player, DeltaTime, offset);
      if (animal2 != null)
      {
        bool selectThis = this.tradeListFrame.AddOrRemoveThisFromTradeList(animal2);
        if (flag && !selectThis)
          flag = false;
        this.animalInfoBox.SetSelection(selectThis, flag);
        this.animalSelectionGridFrame.AddTickToSelectedEntry(!selectThis);
      }
      if (this.animalLocationInfoBox != null)
        this.animalLocationInfoBox.UpdateAnimalLocationInfoBox(player, DeltaTime, offset);
      if (!this.tradeListFrame.UpdateTradeListFrame(player, DeltaTime, offset))
        return false;
      animalsSelected = this.tradeListFrame.tradeList;
      return true;
    }

    public void DrawAnimalSelectionManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.animalSelectionGridFrame.DrawCustomAnimalSelectionGridFrame(offset, spriteBatch);
      this.animalInfoBox.DrawAnimalInfoBox(offset, spriteBatch);
      if (this.noAnimalsText != null)
        this.noAnimalsText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.tradeListFrame.DrawTradeListFrame(offset, spriteBatch);
      if (this.animalLocationInfoBox == null)
        return;
      this.animalLocationInfoBox.DrawAnimalLocationInfoBox(offset, spriteBatch);
    }
  }
}
