// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.PlayerSave
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework.Media;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.PlayerDir.Animals;
using TinyZoo.PlayerDir.BlackMarket;
using TinyZoo.PlayerDir.Breeding;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.PlayerDir.Commodities;
using TinyZoo.PlayerDir.CRISPR;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.PlayerDir.FinancialHistory;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.PlayerDir.Incinerator;
using TinyZoo.PlayerDir.Processing;
using TinyZoo.PlayerDir.Quarantine;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.PlayerDir.ZooQuest;
using TinyZoo.Z_Player;

namespace TinyZoo.PlayerDir
{
  internal class PlayerSave
  {
    internal static void LoadPlayer(Reader reader, Player player)
    {
      int _out = 0;
      int num1 = (int) reader.ReadInt("i", ref _out);
      int num2 = (int) reader.ReadFloat("m", ref MusicManager.MusicVol);
      MediaPlayer.Volume = MusicManager.MusicVol;
      int num3 = (int) reader.ReadFloat("m", ref SoundEffectsManager.SFXVolume);
      player.zquests = new CurrentZQuests(reader, _out);
      player.employees = new Employees(reader, _out);
      Player.financialrecords = new FanancialRecords(reader, _out);
      player.breeds = new Breeds(reader, _out);
      player.prisonlayout = new PrisonLayout(reader, player, _out);
      player.Stats = new PlayerStats(reader, _out);
      player.shopstatus = new ShopStatus(reader, _out, player);
      player.unions = new Unions(reader);
      player.heroquestprogress = new HeroQuestProgress(reader);
      player.crisprBreeds = new CRISPR_Breed(reader, _out);
      player.animalProcessing = new AnimalProcessing(reader, _out);
      player.unlocks = new Unlocks(reader);
      player.storerooms = new TinyZoo.PlayerDir.StoreRooms.StoreRooms(reader);
      player.busroutes = new BusInfo(reader);
      player.animalsonorder = new AnimalsOnOrder(reader, _out);
      player.blackmarketstats = _out <= 2 ? new BlackMarketStats() : new BlackMarketStats(reader);
      if (_out > 4)
        player.animalquarantine = new AnimalQuarantine(reader);
      if (_out > 8)
        player.animalincineration = new AnimalIncineration(reader, _out);
      player.farms = _out <= 9 ? new Farms() : new Farms(reader, _out);
      player.warehouse = _out <= 12 ? new Warehouse() : new Warehouse(reader);
      if (_out > 24)
      {
        Player.criticalchoices = new CriticalChoices(reader, _out);
        player.sponsorships = new Sponsorships(reader);
      }
      else
      {
        Player.criticalchoices = new CriticalChoices();
        player.sponsorships = new Sponsorships();
      }
      player.shelterstocks = _out <= 28 ? new ShelterStocks(player) : new ShelterStocks(reader);
      player.shopstatus.populatefromemployees(player.employees);
      player.shopstatus.AddToShopNavigationAfterLoad(player);
      if (player.heroquestprogress.ProgressArray[1].GetCompletetedQuests() > 0)
      {
        FeatureFlags.FlashTrade = false;
        FeatureFlags.FlashBuildFromNotificationTrack = false;
        FeatureFlags.BlockAllUI = false;
      }
      if (player.heroquestprogress.ProgressArray[3].IsUnlocked)
      {
        FeatureFlags.BlockBuyLand = false;
        Z_GameFlags.ScrubForSaleSigns = true;
      }
      Z_GameFlags.TopBarIsBlockedForTutorial = false;
      if (Z_DebugFlags.ZooTutoriallsDisabled)
      {
        FeatureFlags.BlockAllUI = false;
        FeatureFlags.BlockBuyLand = false;
        Z_GameFlags.TopBarIsBlockedForTutorial = false;
        Z_GameFlags.ScrubForSaleSigns = true;
      }
      Player.currentActiveResearchBonuses = new CurrentActiveResearchBonuses();
      Player.currentActiveResearchBonuses.RecountSetsAndCreateUnlocks(player);
      Z_GameFlags.ForceResolutionNextFrame = true;
    }

    internal static void SavePlayer(Writer writer, Player player)
    {
      writer.Write("i", TinyZoo.Game1.VersionNumber);
      writer.WriteFloat("m", MusicManager.MusicVol);
      writer.WriteFloat("m", SoundEffectsManager.SFXVolume);
      player.zquests.SaveCurrentZQuests(writer);
      player.employees.SaveEmployees(writer);
      Player.financialrecords.SaveFanancialRecords(writer);
      player.breeds.SaveBreeds(writer);
      player.prisonlayout.SavePrisonLayout(writer);
      player.Stats.SaveStats(writer);
      player.shopstatus.SaveShopStatus(writer);
      player.unions.SaveUnions(writer);
      player.heroquestprogress.SaveHeroQuestProgress(writer);
      player.crisprBreeds.SaveCRISPR_Breeds(writer);
      player.animalProcessing.SaveAnimalProcessing(writer);
      player.unlocks.SaveUnlocks(writer);
      if (player.storerooms.StorRoomcontents.StoreRoomLocation == null)
        player.storerooms.StorRoomcontents.PullLocation(player);
      player.storerooms.SaveStoreRooms(writer);
      player.busroutes.SaveBusInfo(writer);
      player.animalsonorder.SaveAnimalOrder(writer);
      player.blackmarketstats.SaveBlackMarketStats(writer);
      player.animalquarantine.SaveAnimalQuarantine(writer);
      player.animalincineration.SaveAnimalIncineration(writer);
      player.farms.SaveFarms(writer);
      player.warehouse.SaveWarehouse(writer);
      Player.criticalchoices.SaveCriticalChoices(writer);
      player.sponsorships.SaveSponsorships(writer);
      player.shelterstocks.SaveShelterStocks(writer);
    }
  }
}
