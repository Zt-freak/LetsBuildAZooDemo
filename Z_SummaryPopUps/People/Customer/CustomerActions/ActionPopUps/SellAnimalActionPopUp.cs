// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps.SellAnimalActionPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.CollectionScreen;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.Research;
using TinyZoo.PlayerDir.BlackMarket;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BalanceSystems.Animals.SellCosts;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_MoralitySummary;
using TinyZoo.Z_SummaryPopUps.People.Customer.CustomerPanelsManagers;
using TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps
{
  internal class SellAnimalActionPopUp : CustomerActionPopUp
  {
    private AnimalSelectionManager animalSelectionManager;
    private CollectionScreenManager collection;
    private GoodEvilIcon moralityIcon;
    private BlackMarketDealerHistory dealerHistory;
    private bool SoldSomething;
    private UIScaleHelper scaleHelper;
    public static bool RefreshAnimalEntries;

    public SellAnimalActionPopUp(Player player, float BaseScale, WalkingPerson walkingPerson)
      : base(BaseScale)
    {
      this.scaleHelper = new UIScaleHelper(BaseScale);
      this.dealerHistory = player.blackmarketstats.GetDealerHistoryForThisBlackMarketDealer(walkingPerson.thispersontype);
      this.SetUpSpeciesSelect(player);
      this.SoldSomething = false;
      this.SizeFrame();
      this.moralityIcon = new GoodEvilIcon(false, basescale_: this.basescale, addInfoIcon: (!Z_DebugFlags.IsBetaVersion), useXtraBig: true);
      GoodEvilIcon moralityIcon1 = this.moralityIcon;
      moralityIcon1.vLocation = moralityIcon1.vLocation - this.framescale * 0.5f;
      GoodEvilIcon moralityIcon2 = this.moralityIcon;
      moralityIcon2.vLocation = moralityIcon2.vLocation + this.scaleHelper.ScaleVector2(new Vector2(160f, -40f));
    }

    private void SetUpSpeciesSelect(Player player)
    {
      this.collection = new CollectionScreenManager(player, BaseScale: this.basescale, isCustomSelection_addEntriesLater: true);
      List<AlienEntry> _alienEntries = new List<AlienEntry>();
      List<AnimalType> animalTypeList = new List<AnimalType>();
      animalTypeList.Add(AnimalType.Rabbit);
      animalTypeList.AddRange((IEnumerable<AnimalType>) ResearchData.GetAliensReseachedInOrder());
      foreach (AnimalType _enemy in animalTypeList)
      {
        _alienEntries.Add(new AlienEntry(_enemy, false, false, 0, this.basescale));
        _alienEntries[_alienEntries.Count - 1].AddExtraFrame(new Vector2(23f, 29f), ColourData.Z_FrameMidBrown, 11f, 5f);
      }
      this.collection.AddAndPositionEntries(_alienEntries, this.basescale, true);
      this.RefreshEntries(player);
      this.collection.location -= this.collection.GetOffsetFromTopLeft();
      this.framescale = new Vector2(this.collection.GetWidth(), this.collection.GetHeight());
      this.collection.location += -this.framescale * 0.5f;
      this.HasPreviousButton = false;
    }

    private void RefreshEntries(Player player)
    {
      HashSet<AnimalType> animalTypeSet = new HashSet<AnimalType>();
      int[] numArray1 = new int[56];
      int[] numArray2 = new int[56];
      List<PrisonZone> prisonzones = player.prisonlayout.cellblockcontainer.prisonzones;
      for (int index = 0; index < prisonzones.Count; ++index)
      {
        foreach (PrisonerInfo prisoner in prisonzones[index].prisonercontainer.prisoners)
        {
          if (!prisoner.IsDead)
          {
            AnimalType animaltype = prisoner.intakeperson.animaltype;
            animalTypeSet.Add(animaltype);
            ++numArray2[(int) animaltype];
            numArray1[(int) animaltype] += AnimalSellCostCalculator.GetSellCostOfPlayerAnimal(prisoner);
          }
        }
      }
      foreach (AlienEntry allEntry in this.collection.GetAllEntries())
      {
        if (animalTypeSet.Contains(allEntry.anaimaltype))
        {
          allEntry.SetUnlocked();
          int num1 = numArray1[(int) allEntry.anaimaltype];
          int num2 = numArray2[(int) allEntry.anaimaltype];
          allEntry.AddStringBelow_NEW("x" + (object) num2, ColourData.Z_Cream, this.basescale, AssetContainer.springFont, RecreateIfExists_ElseUpdateString: false);
          allEntry.AddStringBelow_NEW("$" + (object) num1, ColourData.Z_Cream, this.basescale, AssetContainer.SpringFontX1AndHalf, 1, false);
        }
        else
          allEntry.SetLock();
      }
      SellAnimalActionPopUp.RefreshAnimalEntries = false;
    }

    private void SetUpAnimalSelect(Player player, AnimalType animaltypeFilter = AnimalType.None)
    {
      this.HasPreviousButton = true;
      this.animalSelectionManager = new AnimalSelectionManager(AnimalSelectionUIType.BlackMarket, player, this.basescale, this.framescale, animaltypeFilter, 5, 4);
    }

    public override void OnPreviousButtonClicked()
    {
      this.animalSelectionManager = (AnimalSelectionManager) null;
      this.HasPreviousButton = false;
    }

    public override Vector2 GetSize() => this.framescale;

    public override bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime)
    {
      if (this.animalSelectionManager != null)
      {
        List<PrisonerInfo> animalsSelected;
        if (this.animalSelectionManager.UpdateAnimalSelectionManager(player, DeltaTime, offset, out animalsSelected))
        {
          this.SellTheseAnimals(animalsSelected, player);
          return true;
        }
      }
      else
      {
        if (SellAnimalActionPopUp.RefreshAnimalEntries)
          this.RefreshEntries(player);
        AnimalType animaltypeFilter = this.collection.UpdateCollectionScreenManager(offset, DeltaTime, player, out bool _, out bool _);
        if (animaltypeFilter != AnimalType.None)
        {
          this.SetUpAnimalSelect(player, animaltypeFilter);
          this.collection.enemy = AnimalType.None;
        }
      }
      if (this.moralityIcon != null)
        this.moralityIcon.UpdateForInfoIcon(player, DeltaTime, offset);
      return false;
    }

    public void SellTheseAnimals(List<PrisonerInfo> animalsToSell, Player player)
    {
      int num = 0;
      for (int index = 0; index < animalsToSell.Count; ++index)
        num += AnimalSellCostCalculator.GetSellCostOfPlayerAnimal(animalsToSell[index]);
      player.prisonlayout.RemoveSpecificAnimals_TradeOrSell(animalsToSell, player);
      Player.financialrecords.PlayerSoldAnimalOnBlackMarket(num);
      player.Stats.GiveCash(num, player);
      this.dealerHistory.OnAnimalSold(animalsToSell.Count);
      BlackMarketPanelsManager.SomethingChanged_Refresh = true;
    }

    public override void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset)
    {
      if (this.animalSelectionManager != null)
        this.animalSelectionManager.DrawAnimalSelectionManager(offset, spritebatch);
      else
        this.collection.DrawCollectionScreenManager(offset, spritebatch);
      if (this.moralityIcon == null)
        return;
      this.moralityIcon.DrawGoodEvilIcon(offset, spritebatch);
    }
  }
}
