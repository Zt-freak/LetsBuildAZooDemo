// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.Quests.QuestViewManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.NewDiscoveryScreen;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Tutorials;
using TinyZoo.Z_Quests;
using TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade;

namespace TinyZoo.Z_WorldMap.Quests
{
  internal class QuestViewManager
  {
    private BlackOut blackout;
    private BlackOut HardFade;
    private BlackOut background;
    private BlackOut whiteout;
    private SmartCharacterBox charactertextbox;
    private QuestPack Ref_questpack;
    public bool Active;
    private BackButton close;
    private bool claimed;
    private TextButton Claim;
    private SimpleTextBox textbox;
    private newThingRenderer newthingget;
    public bool Claimed;
    private Z_MiniMap zmap;
    private AnimalTradeView animaltradeview;
    private bool IsFirstQuest;
    private QuestSet questpack;
    private CityName city;

    public QuestViewManager(CityName _city, Player player)
    {
      this.city = _city;
      this.blackout = new BlackOut();
      this.blackout.SetAllColours(0.2f, 0.2f, 0.0f);
      this.questpack = QuestData.GetQuest(this.city);
      this.Ref_questpack = this.questpack.GetCurrentQuest(player.zquests.ProgressByCity[(int) this.city]);
      this.IsFirstQuest = false;
      if (this.city == CityName.Sydney && player.zquests.GetQuestsCompleteInThisCity(this.city) == 0)
        this.IsFirstQuest = true;
      this.Claimed = false;
      this.close = new BackButton();
      this.Create(player, false);
      float smallTextScale = GameFlags.GetSmallTextScale();
      this.textbox = new SimpleTextBox(this.questpack.GetQuestObjectiveDescription(player.zquests.ProgressByCity[(int) this.city]), 500f, textScale: smallTextScale);
      this.Claim = new TextButton("Get!");
      this.Claim.vLocation = new Vector2(512f, 700f);
      this.background = new BlackOut();
      this.background.SetAllColours(new Vector3(0.2f, 0.2f, 0.0f));
      this.whiteout = new BlackOut();
      this.whiteout.SetAllColours(new Vector3(1f, 1f, 1f));
      this.HardFade = new BlackOut();
      this.HardFade.SetAlpha(0.0f);
      this.zmap = new Z_MiniMap(this.Ref_questpack, _city);
    }

    private void Create(Player player, bool Complete)
    {
      AnimalType TalkingHead;
      this.charactertextbox = new SmartCharacterBox(QuestStories.GetStory(this.Ref_questpack, this.city, player, out string _, out TalkingHead, Complete), TalkingHead, Height: 200f, ShortenForCloseButton: true);
      if (Complete)
        this.Claim = (TextButton) null;
      this.charactertextbox.ForceEndLerp();
    }

    public bool UpdateQuestViewManager(Player player, float DeltaTime, bool AllowUpdate)
    {
      if (this.animaltradeview != null)
      {
        bool WillDoClaim;
        if (this.animaltradeview.UpdateAnimalTradeView(player, DeltaTime, out WillDoClaim))
          this.animaltradeview = (AnimalTradeView) null;
        if (WillDoClaim)
          this.DoClaim(player, this.Ref_questpack.city);
        return false;
      }
      this.HardFade.UpdateColours(DeltaTime);
      this.zmap.UpdateZ_MiniMap(DeltaTime);
      if (this.newthingget != null)
      {
        this.background.UpdateColours(DeltaTime);
        this.whiteout.UpdateColours(DeltaTime);
        if (this.newthingget.UpdatenewThingRenderer(DeltaTime, player) && (double) this.HardFade.fTargetAlpha != 1.0)
          this.HardFade.SetAlpha(false, 0.4f, 0.0f, 1f);
        if ((double) this.HardFade.fTargetAlpha == 1.0 && (double) this.HardFade.fAlpha == 1.0)
        {
          this.HardFade.SetAlpha(false, 0.4f, 1f, 0.0f);
          this.newthingget = (newThingRenderer) null;
          this.Create(player, true);
        }
      }
      this.textbox.UpdateSimpleTextBox(DeltaTime, player);
      this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true);
      if (AllowUpdate && this.Claim != null && (this.Claim.UpdateTextButton(player, Vector2.Zero, DeltaTime) || player.inputmap.PressedThisFrame[0]) && !this.claimed)
      {
        if (!this.IsFirstQuest)
        {
          this.animaltradeview = new AnimalTradeView(player, this.Ref_questpack);
          return false;
        }
        this.DoClaim(player, this.Ref_questpack.city);
        return false;
      }
      if ((this.Claimed || TutorialManager.currenttutorial != TUTORIALTYPE.WelcomeToTheZoo) && this.close.UpdateBackButton(player, DeltaTime) & AllowUpdate)
      {
        if (!this.Claimed)
          return true;
        if (Game1.GetNextGameState() != GAMESTATE.OverWorldSetUp)
        {
          TileMath.SetOverWorldMapSize_XDefault(65);
          TileMath.SetOverWorldMapSize_YSize(56);
          Game1.screenfade.BeginFade(true);
          Game1.SetNextGameState(GAMESTATE.OverWorldSetUp);
          ParkGate.Reset();
        }
      }
      return false;
    }

    private void DoClaim(Player player, CityName city)
    {
      this.claimed = true;
      int BoysUsed;
      player.prisonlayout.HasTheseAnimals(this.Ref_questpack, out bool _, out BoysUsed);
      if (this.Ref_questpack.GetThisAnimal != AnimalType.Rabbit)
        player.prisonlayout.RemoveTheseAnimalsOnTrade(this.Ref_questpack.trades_ListOnlyOne[0].Total.SmartGetValue(false, 100), this.Ref_questpack.trades_ListOnlyOne[0].animal, this.Ref_questpack.trades_ListOnlyOne[0].VariantIndex, BoysUsed);
      player.worldhistory.GotNewAnimal(this.Ref_questpack.GetThisAnimal, 0);
      player.worldhistory.GotNewAnimal(this.Ref_questpack.GetThisAnimal, 0);
      this.newthingget = new newThingRenderer(this.Ref_questpack.GetThisAnimal, WasBreedingPair: true, _WasNew: true);
      this.newthingget.UpdatenewThingRenderer(0.0f, player);
      this.background.SetColours(false, 1f, new Vector3(0.7f, 0.7f, 0.7f), new Vector3(0.8f, 0.8f, 0.0f));
      this.whiteout.SetAlpha(false, 2f, 0.6f, 0.0f);
      player.Stats.research.AddAnimalToResearchComplete(this.Ref_questpack.GetThisAnimal, 0, 1, true);
      player.Stats.research.AddAnimalToResearchComplete(this.Ref_questpack.GetThisAnimal, 0, 1);
      player.zquests.CompletedQuest(this.Ref_questpack.city);
      player.livestats.AnimalsJustTraded = new WaveInfo(new IntakeInfo()
      {
        CameFromHere = city,
        People = {
          new IntakePerson(this.Ref_questpack.GetThisAnimal),
          new IntakePerson(this.Ref_questpack.GetThisAnimal, _IsAGirl: true)
        }
      });
      if (player.Stats.GetAliensCaptured(this.Ref_questpack.GetThisAnimal) > 0)
        throw new Exception("sf");
      player.Stats.AnimalBredOrFound(this.Ref_questpack.GetThisAnimal, 0, true);
      player.Stats.AnimalBredOrFound(this.Ref_questpack.GetThisAnimal, 0, false);
      this.Claimed = true;
      Player.financialrecords.CompletedTrade();
      QuestScrubber.ScrubOnCompletingTrade(player);
    }

    public void DrawQuestViewManager()
    {
      this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatch03);
      if (this.newthingget != null)
      {
        this.background.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatch03);
        this.newthingget.DrawnewThingRenderer(false);
        this.whiteout.DrawBlackOut(Vector2.Zero, AssetContainer.PointBlendBatch04);
        this.HardFade.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      }
      else
      {
        this.zmap.DrawZ_MiniMap(Vector2.Zero, this.Claimed);
        if (!this.Claimed && TutorialManager.currenttutorial == TUTORIALTYPE.WelcomeToTheZoo)
        {
          this.charactertextbox.DrawSmartCharacterBox(new Vector2(0.0f, 50f));
        }
        else
        {
          this.charactertextbox.DrawSmartCharacterBox(new Vector2(0.0f, 50f));
          this.close.DrawBackButton(Vector2.Zero);
        }
        if (this.Claim != null)
        {
          this.textbox.DrawSimpleTextBox(new Vector2(512f, 600f));
          this.Claim.DrawTextButton(Vector2.Zero);
        }
        if (this.animaltradeview != null)
          this.animaltradeview.DrawAnimalTradeView();
        this.HardFade.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      }
    }
  }
}
