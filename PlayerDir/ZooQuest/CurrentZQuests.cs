// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.ZooQuest.CurrentZQuests
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Quests;

namespace TinyZoo.PlayerDir.ZooQuest
{
  internal class CurrentZQuests
  {
    private int CompletedQuests;
    public int[] ProgressByCity;

    public CurrentZQuests()
    {
      this.Create();
      this.CheckQuests();
    }

    public void CheckQuests(bool Increment = true)
    {
      for (int index = 0; index < this.ProgressByCity.Length; ++index)
      {
        if (this.ProgressByCity[index] == -1 && QuestData.GetUnlockForThisQuest((CityName) index) <= this.CompletedQuests)
          this.ProgressByCity[index] = 0;
      }
    }

    public int GetQuestsCompleteInThisCity(CityName city) => this.ProgressByCity[(int) city];

    public int GetQuestsCompletedWithThisZooKeeper(AnimalType animaltype) => animaltype == AnimalType.AvatarsCount ? this.CompletedQuests : this.ProgressByCity[(int) QuestData.GetZooKeeperToCity(animaltype)];

    public QuestPack GetActiveQuestFromThisCity(CityName city) => this.ProgressByCity[(int) city] > -1 ? QuestData.GetQuest(city).GetCurrentQuest(this.ProgressByCity[(int) city]) : (QuestPack) null;

    public void CompletedQuest(CityName city)
    {
      if (this.ProgressByCity[(int) city] >= 0)
      {
        ++this.ProgressByCity[(int) city];
        ++this.CompletedQuests;
        this.CheckQuests();
      }
      else
        this.CheckQuests();
    }

    public void DoTradeAndCompleteQuest(List<PrisonerInfo> animals, Player player, QuestPack quest)
    {
      if (quest.trades_ListOnlyOne[0].Total.GetUnvallidatedValue() != 0 && !player.prisonlayout.RemoveSpecificAnimals_TradeOrSell(animals, player))
        return;
      player.worldhistory.GotNewAnimal(quest.GetThisAnimal, 0);
      player.worldhistory.GotNewAnimal(quest.GetThisAnimal, 0);
      player.zquests.CompletedQuest(quest.city);
      Player.financialrecords.CompletedTrade();
      QuestScrubber.ScrubOnCompletingTrade(player);
      player.livestats.AnimalsJustTraded = new WaveInfo(new IntakeInfo()
      {
        CameFromHere = quest.city,
        People = {
          new IntakePerson(quest.GetThisAnimal),
          new IntakePerson(quest.GetThisAnimal, _IsAGirl: true)
        }
      });
    }

    private void Create()
    {
      this.ProgressByCity = new int[15];
      for (int index = 0; index < this.ProgressByCity.Length; ++index)
        this.ProgressByCity[index] = -1;
    }

    public void SaveCurrentZQuests(Writer writer)
    {
      if (TinyZoo.Game1.VersionNumber < 17)
        return;
      writer.WriteInt("q", this.ProgressByCity.Length);
      for (int index = 0; index < this.ProgressByCity.Length; ++index)
      {
        writer.WriteInt("q", this.ProgressByCity[index]);
        if (this.ProgressByCity[index] > -1)
          this.CompletedQuests += this.ProgressByCity[index];
      }
    }

    public CurrentZQuests(Reader reader, int VersionForLoad)
    {
      this.Create();
      int _out1 = 0;
      if (VersionForLoad > 16)
      {
        int num1 = (int) reader.ReadInt("q", ref _out1);
        for (int index = 0; index < _out1; ++index)
        {
          int num2 = (int) reader.ReadInt("q", ref this.ProgressByCity[index]);
        }
      }
      else
      {
        List<ZQuest> zquestList = new List<ZQuest>();
        int num1 = (int) reader.ReadInt("q", ref _out1);
        for (int index = 0; index < _out1; ++index)
          zquestList.Add(new ZQuest(reader));
        int _out2 = 0;
        int num2 = (int) reader.ReadInt("q", ref _out2);
        int num3 = (int) reader.ReadInt("q", ref this.CompletedQuests);
      }
      List<ZQuest> zquestList1 = new List<ZQuest>();
      this.CheckQuests(false);
    }
  }
}
