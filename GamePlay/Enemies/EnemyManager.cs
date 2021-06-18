// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Enemies.EnemyManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.ArcadeMode;
using TinyZoo.Audio;
using TinyZoo.GamePlay.ReclaimedZones;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items;
using TinyZoo.SwitchRandom;
using TinyZoo.Tutorials;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment;
using TinyZoo.Z_Fights;
using TinyZoo.Z_TrashSystem;

namespace TinyZoo.GamePlay.Enemies
{
  internal class EnemyManager
  {
    private List<AnimalRenderMan> animals;
    public int TotalEnemies;
    private int PoopsRemaining;
    private int SpecialAnimalOptimzation;
    private int NextPoopTime;
    private int GapBetweenPoops;
    private int PrisonUID;
    private List<DynamicEnrichmentItem> enrichemntitems;

    public EnemyManager(
      int PrisonIDForRandomSeed,
      BoxZoneManager boxzonemanager,
      WaveInfo waveinfo,
      Player player)
    {
      this.PrisonUID = PrisonIDForRandomSeed;
      this.TotalEnemies = waveinfo.People.Count;
      RandomResultContainer rand = new RandomResultContainer(PrisonIDForRandomSeed);
      if (GameFlags.IsArcadeMode)
        rand = ArcadeData.GetSeededRandom();
      else if (GameFlags.BountyMode)
        rand = new RandomResultContainer(Bounty.LastRNDSeed);
      this.animals = new List<AnimalRenderMan>();
      for (int index = 0; index < this.TotalEnemies; ++index)
        this.animals.Add(new AnimalRenderMan(rand, waveinfo.People[index]));
      if (!GameFlags.IsArcadeMode && player.tracking.TotalGamesCompleted == 0U && TutorialManager.currenttutorial == TUTORIALTYPE.GamePlayIntro)
      {
        this.animals[0].enemyrenderere.vLocation.X = boxzonemanager.zones[0].TopLeft.X + 65f;
        this.animals[0].enemyrenderere.vLocation.Y = boxzonemanager.zones[0].TopLeft.Y + 90f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      }
      for (int index = 0; index < this.animals.Count; ++index)
        this.animals[index].RefreshZones(boxzonemanager);
      GameFlags.EnemyCount = this.TotalEnemies;
      GameFlags.EnemyCountAtStart = GameFlags.EnemyCount;
    }

    public AnimalRenderMan GetCharacterByUID(int UID)
    {
      for (int index = 0; index < this.animals.Count; ++index)
      {
        if (this.animals[index].refperson.UID == UID)
          return this.animals[index];
      }
      return (AnimalRenderMan) null;
    }

    public void AddFightManager(FightManager thisisthefight)
    {
    }

    public void EnrichedAnimalMovedOrDeleted(AnimalRenderMan animal)
    {
      if (this.enrichemntitems == null)
        return;
      for (int index = this.enrichemntitems.Count - 1; index > -1; --index)
        this.enrichemntitems[index].ForceDetachAnimal(animal);
    }

    public void DeleteAllDynamicItemsInThisen(PrisonZone REF_prisonzone, Player player)
    {
      for (int index = 0; index < REF_prisonzone.penItems.items.Count; ++index)
        this.RemoveDynamicItemFromCellBlock(REF_prisonzone.penItems.items[index], player, REF_prisonzone.Cell_UID);
      int num = 0;
      while (num < REF_prisonzone.penItems.items.Count)
        ++num;
    }

    public void AddDynamicItemToCellBlock(
      TileRenderer tilerenderer,
      PrisonZone REF_prisonzone,
      ZooBuildingTopRenderer toprenderer,
      ENRICHMENTBEHAVIOUR enrchmentbehaviour,
      PenItem penitem)
    {
      if (this.enrichemntitems == null)
        this.enrichemntitems = new List<DynamicEnrichmentItem>();
      this.enrichemntitems.Add(new DynamicEnrichmentItem(tilerenderer, toprenderer, enrchmentbehaviour, REF_prisonzone, penitem));
      CustomerManager.AddDynamicEnrichmentItemToPeople(this.enrichemntitems[this.enrichemntitems.Count - 1]);
      this.enrichemntitems[this.enrichemntitems.Count - 1].SetForIrregularEnclosure(REF_prisonzone.FloorTiles, REF_prisonzone.Cell_UID);
    }

    public void RemoveDynamicItemFromCellBlock(PenItem penitem, Player player, int PenUID)
    {
      if (this.enrichemntitems == null)
        return;
      for (int index = this.enrichemntitems.Count - 1; index > -1; --index)
      {
        if (this.enrichemntitems[index].Ref_PenItem == penitem)
        {
          this.enrichemntitems[index].TryAndDestroy(player, PenUID);
          CustomerManager.RemoveDynamicEnrichmentItem(this.enrichemntitems[index]);
          this.enrichemntitems.RemoveAt(index);
          break;
        }
      }
    }

    public void VallidatAnimals(PrisonZone REF_prisonzone, BoxZoneManager boxzonemanager)
    {
      this.PrisonUID = REF_prisonzone.Cell_UID;
      for (int index1 = this.animals.Count - 1; index1 > -1; --index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < REF_prisonzone.prisonercontainer.prisoners.Count; ++index2)
        {
          if (REF_prisonzone.prisonercontainer.prisoners[index2].intakeperson == this.animals[index1].refperson)
            flag = true;
        }
        if (!flag)
        {
          CustomerManager.RemoveAnimal(this.animals[index1], REF_prisonzone.Cell_UID);
          this.EnrichedAnimalMovedOrDeleted(this.animals[index1]);
          this.animals.RemoveAt(index1);
        }
      }
      for (int index1 = 0; index1 < REF_prisonzone.prisonercontainer.prisoners.Count; ++index1)
      {
        bool flag = false;
        for (int index2 = this.animals.Count - 1; index2 > -1; --index2)
        {
          if (REF_prisonzone.prisonercontainer.prisoners[index1].intakeperson == this.animals[index2].refperson)
            flag = true;
        }
        if (!flag)
        {
          this.animals.Add(new AnimalRenderMan(new RandomResultContainer(TinyZoo.Game1.Rnd), REF_prisonzone.prisonercontainer.prisoners[index1].intakeperson, REF_prisonzone.prisonercontainer.prisoners[index1]));
          bool ForceLoc = false;
          if (REF_prisonzone.prisonercontainer.prisoners[index1].UseTempOverrideLocation)
            ForceLoc = true;
          this.animals[this.animals.Count - 1].enemyrenderere.vLocation = REF_prisonzone.prisonercontainer.prisoners[index1].GetLocation();
          CustomerManager.AddAnimalToPeople(this.animals[this.animals.Count - 1]);
          if (REF_prisonzone.IsIerregular)
            this.animals[this.animals.Count - 1].SetForIrregularEnclosure(REF_prisonzone.FloorTiles, REF_prisonzone.Cell_UID, ForceLoc);
          else
            this.animals[this.animals.Count - 1].RefreshZones(boxzonemanager);
          if (REF_prisonzone.GateIsBroken)
            this.animals[this.animals.Count - 1].StartBreakOut(REF_prisonzone.GetGateLocation(), REF_prisonzone.GetSpaceBehindGate(), REF_prisonzone.Cell_UID);
        }
      }
    }

    public void AddEnemiesFromCell(
      int PrisonIDForRandomSeed,
      WaveInfo waveinfo,
      BoxZoneManager boxzonemanager)
    {
      throw new Exception("IS THIS IN THE GAME?");
    }

    public EnemyManager(
      PrisonerContainer prisonercontainer,
      BoxZoneManager boxzonemanager,
      PrisonZone prisonzone)
    {
      this.animals = new List<AnimalRenderMan>();
      if (TrailerDemoFlags.HasTrailerFlag && TrailerDemoFlags.CustomPenSpawn && TrailerDemoFlags.PenRevealIndexes.Contains(prisonzone.Cell_UID))
        return;
      this.PrisonUID = prisonzone.Cell_UID;
      for (int index = 0; index < prisonercontainer.prisoners.Count; ++index)
      {
        this.animals.Add(new AnimalRenderMan(new RandomResultContainer(TinyZoo.Game1.Rnd), prisonercontainer.prisoners[index].intakeperson, prisonercontainer.prisoners[index]));
        CustomerManager.AddAnimalToPeople(this.animals[this.animals.Count - 1]);
        if (prisonzone != null)
        {
          this.animals[this.animals.Count - 1].enemyrenderere.vLocation = prisonzone.GetRandomLocationInCellBlock();
          this.animals[this.animals.Count - 1].SetForIrregularEnclosure(prisonzone.FloorTiles, prisonzone.Cell_UID);
        }
      }
    }

    public void RemoveDeadPeopleFromPrison(Player player)
    {
      for (int index = 0; index < this.animals.Count; ++index)
      {
        if (this.animals[index].REF_prisonerinfo.IsDead)
        {
          if (player.Stats.TutorialsComplete[13] || DebugFlags.DisableTutorials_DEP_KeepTrue)
            player.livestats.AddFreshMeatToCorpseQueue(this.animals[index].refperson);
          player.livestats.waveinfo.RemoveThisPerson(this.animals[index].refperson);
        }
      }
    }

    public void ScrubBoxZones(BoxZoneManager boxzonemanager, bool WasTempTest = false)
    {
      for (int index = 0; index < this.animals.Count; ++index)
        this.animals[index].ScrubBoxZones(boxzonemanager);
      if (WasTempTest)
        boxzonemanager.RevallidateAgainstTemp();
      boxzonemanager.LaunchProgressParticles();
      boxzonemanager.CountProgress();
    }

    public void DoShuffle()
    {
      GameFlags.JustShuffled = true;
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmUpgrade, 0.8f, 1f);
      CameraShake.BeginCameraShake(TinyZoo.Game1.Rnd, 0.5f);
      for (int index = 0; index < this.animals.Count; ++index)
        this.animals[index].Shuffle();
    }

    public void CheckDeaths()
    {
      for (int index = 0; index < this.animals.Count; ++index)
        this.animals[index].CheckDeath();
    }

    public void StartNewDay()
    {
      if (Z_DebugFlags.IsBetaVersion)
        return;
      this.PoopsRemaining = 0;
      for (int index = 0; index < this.animals.Count; ++index)
      {
        this.animals[index].CheckDeath();
        this.PoopsRemaining += (int) Math.Floor((double) this.animals[index].REF_prisonerinfo.PoopNeed);
      }
      this.GapBetweenPoops = (int) ((double) Z_GameFlags.SecondsZooOpenPerDay / ((double) this.PoopsRemaining + 2.0));
      this.NextPoopTime = (int) Z_GameFlags.ZooOpenTime_InSeconds + this.GapBetweenPoops;
    }

    public void UpdateEnemyManager(float DeltaTime, BoxZoneManager boxzonemanager)
    {
      if (FeatureFlags.BlockEnemySpawn)
        DeltaTime = 0.0f;
      if (this.PoopsRemaining > 0 && (double) Z_GameFlags.DayTimer > (double) this.NextPoopTime)
      {
        this.NextPoopTime += this.GapBetweenPoops;
        for (int index = 0; index < this.animals.Count; ++index)
        {
          if ((double) this.animals[index].REF_prisonerinfo.PoopNeed >= 1.0)
          {
            --this.animals[index].REF_prisonerinfo.PoopNeed;
            Z_TrashManager.MakePoop(this.animals[index].pennavigation.CurrentWorldSpaceLocation, this.animals[index].REF_prisonerinfo.intakeperson.animaltype, this.PrisonUID);
          }
        }
      }
      if (this.enrichemntitems != null)
      {
        if (this.animals != null && this.animals.Count > 0)
        {
          if (this.SpecialAnimalOptimzation < this.animals.Count - 1)
            ++this.SpecialAnimalOptimzation;
          else
            this.SpecialAnimalOptimzation = 0;
          for (int index = 0; index < this.enrichemntitems.Count; ++index)
            this.enrichemntitems[index].UpdateEnrichmentItem(DeltaTime, this.animals[this.SpecialAnimalOptimzation].pennavigation.CurrentWorldSpaceLocation, this.animals[this.SpecialAnimalOptimzation]);
        }
        else
        {
          for (int index = 0; index < this.enrichemntitems.Count; ++index)
          {
            if (this.animals.Count > 0)
              this.enrichemntitems[index].UpdateEnrichmentItem(DeltaTime, (Vector2Int) null, this.animals[this.SpecialAnimalOptimzation]);
          }
        }
      }
      bool flag = false;
      for (int index = 0; index < this.animals.Count; ++index)
      {
        if (this.animals[index].UpdateAnimal(DeltaTime))
          flag = true;
      }
      if (!flag)
        return;
      boxzonemanager.SetAllTempHasPersonCopy();
      for (int index = 0; index < this.animals.Count; ++index)
        this.animals[index].RefreshZones(boxzonemanager);
      boxzonemanager.RevallidateAgainstTemp();
      boxzonemanager.CountProgress();
    }

    public void StartBreakOut(Vector2Int GateLocation, Vector2Int SpaceBehindGate, int PrisonUID)
    {
      for (int index = 0; index < this.animals.Count; ++index)
        this.animals[index].StartBreakOut(GateLocation, SpaceBehindGate, PrisonUID);
    }

    public List<AnimalRenderMan> CheckPeopleForCollisions(
      Vector2 LocationInWorldSpace)
    {
      List<AnimalRenderMan> animalRenderManList = new List<AnimalRenderMan>();
      for (int index = 0; index < this.animals.Count; ++index)
      {
        if (this.animals[index].CheckCollision(LocationInWorldSpace))
          animalRenderManList.Add(this.animals[index]);
      }
      return animalRenderManList;
    }

    public Enemy GetRandomPerson() => this.animals.Count > 0 ? (Enemy) this.animals[TinyZoo.Game1.Rnd.Next(0, this.animals.Count)] : (Enemy) null;

    public void DrawEnemyManager()
    {
      this.animals.Sort(new Comparison<AnimalRenderMan>(AnimalRenderMan.SortAnimal));
      for (int index = 0; index < this.animals.Count; ++index)
        this.animals[index].DrawAnimal();
    }
  }
}
