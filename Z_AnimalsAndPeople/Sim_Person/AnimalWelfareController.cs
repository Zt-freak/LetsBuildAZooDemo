// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.AnimalWelfareController
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person.VIPS;
using TinyZoo.Z_SummaryPopUps.EventReport;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class AnimalWelfareController
  {
    private DeliveryManController deliveryman;
    private List<int> HasVisitedThesePens;
    private float WaitAndLookAtAnimalsTimer;
    private int TotalToVisit;
    private List<ReportResultRank> TheseRanks;
    private bool RatedThis;
    public List<IntakePerson> SawTheseAnimals;
    public List<CellBlockType> pentypesvisited;
    private List<AnimalWelfareList> animalwelfaresummary;
    private bool WasBribed;
    private bool IsTutorial;

    public AnimalWelfareController(Player player)
    {
      this.IsTutorial = !player.Stats.TutorialsComplete[30];
      player.Stats.TutorialsComplete[30] = true;
      this.pentypesvisited = new List<CellBlockType>();
      this.animalwelfaresummary = new List<AnimalWelfareList>();
      this.deliveryman = new DeliveryManController();
      this.HasVisitedThesePens = new List<int>();
      this.TotalToVisit = 0;
      for (int index = 0; index < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index)
      {
        if (player.prisonlayout.cellblockcontainer.prisonzones[index].prisonercontainer.prisoners.Count > 0)
          ++this.TotalToVisit;
      }
      if (this.TotalToVisit > 5)
        this.TotalToVisit = 5;
      this.TheseRanks = new List<ReportResultRank>();
      this.SawTheseAnimals = new List<IntakePerson>();
    }

    public int GetTotalPensThisOfficerWillVisit() => this.TotalToVisit;

    public List<ReportResultRank> GetRanskEarened() => this.TheseRanks;

    public bool BlockWalkFroSpecialCharacter() => this.deliveryman.IsDoingDelivery && this.deliveryman.deliveryguystatus == DeliveryGuyStatus.AtJobWaiting;

    public bool GetIsBribed() => this.WasBribed;

    public int GetBribeValue(out int maxbribe)
    {
      ReportResultRank finalRank = this.GetFinalRank();
      switch (this.TheseRanks.Count)
      {
        case 0:
          maxbribe = 1000;
          return 1000;
        case 1:
          maxbribe = 4000;
          switch (finalRank)
          {
            case ReportResultRank.A:
              return 1000;
            case ReportResultRank.B:
              return 2000;
            case ReportResultRank.C:
              return 3000;
            case ReportResultRank.F:
              return 4000;
          }
          break;
        case 2:
          maxbribe = 8000;
          switch (finalRank)
          {
            case ReportResultRank.A:
              return 1500;
            case ReportResultRank.B:
              return 3000;
            case ReportResultRank.C:
              return 5000;
            case ReportResultRank.F:
              return 8000;
          }
          break;
        case 3:
          maxbribe = 12000;
          switch (finalRank)
          {
            case ReportResultRank.A:
              return 1500;
            case ReportResultRank.B:
              return 5000;
            case ReportResultRank.C:
              return 8000;
            case ReportResultRank.F:
              return 12000;
          }
          break;
        case 4:
          maxbribe = 18000;
          switch (finalRank)
          {
            case ReportResultRank.A:
              return 1500;
            case ReportResultRank.B:
              return 8000;
            case ReportResultRank.C:
              return 12000;
            case ReportResultRank.F:
              return 18000;
          }
          break;
        default:
          maxbribe = 20000;
          switch (finalRank)
          {
            case ReportResultRank.A:
              return 500;
            case ReportResultRank.B:
              return 7000;
            case ReportResultRank.C:
              return 12000;
            case ReportResultRank.F:
              return 20000;
          }
          break;
      }
      return 20000;
    }

    public void ConfirmBribe() => this.WasBribed = true;

    public List<IntakePerson> GetAniamlsSeen() => this.SawTheseAnimals;

    public void FinishJob(WalkingPerson walkingperson) => OverWorldManager.zoopopupHolder.CreateZooPopUps(POPUPSTATE.EventReport, walkingperson);

    public void CancelAllActions() => throw new Exception("IS THIS WHY THE DELIVERY GUY GETS STUCK?");

    public void ReachedLocation(
      Vector2Int CurrentLocation,
      out bool SetNewPath,
      Player player,
      WalkingPerson parent,
      ref Vector2Int ForceGoHere,
      ref bool IsWalking,
      ref bool BlockAutoWalk,
      MemberOfThePublic member)
    {
      SetNewPath = true;
      int ArrayShortCut = 1;
      Console.WriteLine("GUY REACHED TARGET" + (object) Player.financialrecords.GetDaysPassed());
      if (this.deliveryman.IsDoingDelivery)
      {
        this.deliveryman.ReachedTargetLocation(CurrentLocation, ref BlockAutoWalk, ref IsWalking, out bool _);
        int deliveryguystatus = (int) this.deliveryman.deliveryguystatus;
      }
      if (this.deliveryman.IsDoingDelivery)
        return;
      if (player.prisonlayout.cellblockcontainer.prisonzones.Count > 0 && this.HasVisitedThesePens.Count < player.prisonlayout.cellblockcontainer.prisonzones.Count)
      {
        bool flag = false;
        int num = 0;
        while (!flag)
        {
          ++num;
          int index = TinyZoo.Game1.Rnd.Next(0, player.prisonlayout.cellblockcontainer.prisonzones.Count);
          if (!this.HasVisitedThesePens.Contains(player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID) && player.prisonlayout.cellblockcontainer.prisonzones[index].prisonercontainer.prisoners.Count > 0)
          {
            ArrayShortCut = index;
            this.HasVisitedThesePens.Add(player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID);
            break;
          }
          if (num > 10)
          {
            member.LeftParkEarly = true;
            member.LeftTheParkBecauseOfThis = ParkLeavingReason.JobIsDone;
            BlockAutoWalk = false;
            return;
          }
        }
        BlockAutoWalk = this.deliveryman.TryToStartJourneyToPen(player, this.HasVisitedThesePens[this.HasVisitedThesePens.Count - 1], parent, ref ForceGoHere, ArrayShortCut);
        if (BlockAutoWalk)
        {
          if (ForceGoHere == null || !parent.pathnavigator.TryToGoHere(ForceGoHere, GameFlags.pathset, true))
            return;
          this.RatedThis = false;
          SetNewPath = true;
        }
        else
        {
          member.LeftParkEarly = true;
          member.LeftTheParkBecauseOfThis = ParkLeavingReason.JobIsDone;
          BlockAutoWalk = false;
        }
      }
      else
      {
        member.LeftParkEarly = true;
        member.LeftTheParkBecauseOfThis = ParkLeavingReason.JobIsDone;
      }
    }

    public void UpdateAnimalWelfareController(
      float DeltaTime,
      ref bool WalkPaused,
      WalkingPerson parent,
      Player player,
      ref bool BlockAutoWalk,
      ref bool IsPlayingWalkAnimation)
    {
      if (!this.deliveryman.IsDoingDelivery)
        return;
      if (this.deliveryman.deliveryguystatus == DeliveryGuyStatus.AtJobWaiting)
      {
        IsPlayingWalkAnimation = false;
        this.WaitAndLookAtAnimalsTimer += DeltaTime;
        if ((double) this.WaitAndLookAtAnimalsTimer <= 3.0)
          return;
        this.ProcessResult(player);
        this.deliveryman.ExitJobLocation();
        IsPlayingWalkAnimation = true;
      }
      else
      {
        if (!this.deliveryman.UpdateDelivery(DeltaTime, ref IsPlayingWalkAnimation, player, parent, ref BlockAutoWalk, out bool _))
          return;
        WalkPaused = false;
        IsPlayingWalkAnimation = false;
      }
    }

    public ReportResultRank GetFinalRank()
    {
      if (this.WasBribed)
        return ReportResultRank.A;
      if (this.IsTutorial)
        return ReportResultRank.C;
      int num = 0;
      for (int index = 0; index < this.TheseRanks.Count; ++index)
      {
        if (this.TheseRanks[index] > (ReportResultRank) num)
          num = (int) this.TheseRanks[index];
      }
      return this.TheseRanks.Count == 0 ? ReportResultRank.C : (ReportResultRank) num;
    }

    public List<string> GetFinalResultFlavourText()
    {
      if (this.TheseRanks.Count == 0)
        return new List<string>()
        {
          "I saw nothing!",
          "I didnt see any animals, so I cant give you a rating right now. I will be back tomorrow to check again"
        };
      ReportResultRank finalRank = this.GetFinalRank();
      for (int index = 0; index < this.TheseRanks.Count; ++index)
      {
        if (this.TheseRanks[index] == finalRank || this.IsTutorial)
          return this.animalwelfaresummary[index].GetFlavourText(this.WasBribed, this.IsTutorial);
      }
      return new List<string>() { "Error Finding Text" };
    }

    private void ProcessResult(Player player)
    {
      if (this.RatedThis)
        return;
      this.RatedThis = true;
      if (this.HasVisitedThesePens.Count <= 0)
        return;
      PrisonZone thisCellBlock = player.prisonlayout.cellblockcontainer.GetThisCellBlock(this.HasVisitedThesePens[this.HasVisitedThesePens.Count - 1]);
      if (thisCellBlock == null)
        return;
      this.pentypesvisited.Add(thisCellBlock.CellBLOCKTYPE);
      AnimalWelfareList animalWelfareList = new AnimalWelfareList(thisCellBlock.Cell_UID);
      animalWelfareList.HasBuiltStoreRoom = player.storerooms.HasBuiltStoreRoom;
      if (thisCellBlock.prisonercontainer.prisoners.Count <= 0)
        return;
      this.SawTheseAnimals.Add(thisCellBlock.prisonercontainer.prisoners[0].intakeperson);
      animalWelfareList.OverallWelfare = thisCellBlock.WelfareAndCleanliness;
      animalWelfareList.OverallPoop = thisCellBlock.PoopWelfareContribution;
      animalWelfareList.OverallCorpse = thisCellBlock.PoopWelfareContribution;
      animalWelfareList.QualityForSpace = thisCellBlock.LastCalculatedQualityForSpace;
      for (int index = 0; index < thisCellBlock.prisonercontainer.tempAnimalInfo.Count; ++index)
      {
        animalWelfareList.CollectiveCorpseAge += (float) thisCellBlock.prisonercontainer.tempAnimalInfo[index].CollectiveCorpseAge;
        animalWelfareList.StressFromCohabitation += thisCellBlock.prisonercontainer.tempAnimalInfo[index].StressFromCohabitation;
        animalWelfareList.GroupSizeLoneliness += (float) thisCellBlock.prisonercontainer.tempAnimalInfo[index].GroupSizeLoneliness;
        animalWelfareList.LargeGroupStress += (float) thisCellBlock.prisonercontainer.tempAnimalInfo[index].LargeGroupStress;
        animalWelfareList.AnimalHabitatMatch += thisCellBlock.prisonercontainer.tempAnimalInfo[index].AnimalHabitatMatch;
        if ((double) thisCellBlock.prisonercontainer.tempAnimalInfo[index].TotalEnrichmentValue < (double) thisCellBlock.prisonercontainer.tempAnimalInfo[index].RequiredEnrichment)
          animalWelfareList.EnrichmentDefecit += thisCellBlock.prisonercontainer.tempAnimalInfo[index].TotalEnrichmentValue - thisCellBlock.prisonercontainer.tempAnimalInfo[index].RequiredEnrichment;
      }
      for (int index = 0; index < thisCellBlock.prisonercontainer.prisoners.Count; ++index)
      {
        if (!thisCellBlock.prisonercontainer.prisoners[index].IsDead)
        {
          if (thisCellBlock.prisonercontainer.prisoners[index].DaysWithoutFood > 0)
          {
            animalWelfareList.TotalDaysWithoutFood += thisCellBlock.prisonercontainer.prisoners[index].DaysWithoutFood;
            if (animalWelfareList.PeakDaysWithoutFood < thisCellBlock.prisonercontainer.prisoners[index].DaysWithoutFood)
            {
              animalWelfareList.PeakDaysWithoutFood = thisCellBlock.prisonercontainer.prisoners[index].DaysWithoutFood;
              animalWelfareList.TotalPeakNoFood = 1;
            }
            else if (animalWelfareList.PeakDaysWithoutFood == thisCellBlock.prisonercontainer.prisoners[index].DaysWithoutFood)
              ++animalWelfareList.TotalPeakNoFood;
          }
          if (thisCellBlock.prisonercontainer.prisoners[index].DaysWithoutWater > 0)
          {
            animalWelfareList.TotalDaysWithoutWater += thisCellBlock.prisonercontainer.prisoners[index].DaysWithoutWater;
            if (animalWelfareList.PeakDaysWithoutWater < thisCellBlock.prisonercontainer.prisoners[index].DaysWithoutWater)
            {
              animalWelfareList.PeakDaysWithoutWater = thisCellBlock.prisonercontainer.prisoners[index].DaysWithoutWater;
              animalWelfareList.TotalPeakNoWater = 1;
            }
            else if (animalWelfareList.PeakDaysWithoutWater == thisCellBlock.prisonercontainer.prisoners[index].DaysWithoutWater)
              ++animalWelfareList.TotalPeakNoWater;
          }
        }
      }
      this.animalwelfaresummary.Add(animalWelfareList);
      this.TheseRanks.Add(animalWelfareList.GetRank());
    }
  }
}
