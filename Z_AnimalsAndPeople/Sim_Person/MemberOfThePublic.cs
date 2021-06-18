// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.MemberOfThePublic
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.Speech;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person.VIPS;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_BalanceSystems.CustomerStats;
using TinyZoo.Z_ManageShop.FoodIcon;
using TinyZoo.Z_Notification;
using TinyZoo.Z_OverWorld;
using TinyZoo.Z_OverWorld._OverWorldEnv.Customers.PeopleAttachments;
using TinyZoo.Z_OverWorld.PathFinding_Nodes;
using TinyZoo.Z_OverWorld.Speech;
using TinyZoo.Z_SummaryPopUps.FeatureUnlock;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;
using TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info;
using TinyZoo.Z_TrashSystem;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class MemberOfThePublic
  {
    public int BribeValue = -1;
    private List<TrashType> TrashHeld;
    private GeneralWellbeing generalwellbeing;
    public int CashHeld;
    public int StartingCash;
    public bool HasSeasonPass;
    public CustomerLedger purchaseledger;
    public bool ThisCustomerDecidedNotToPay;
    public bool LeftParkEarly;
    private ParkLeavingReason _LeftTheParkBecauseOfThis;
    public AnimalsToSee_CheckList animalstosee;
    public bool IsWalkingToShop;
    private Vector2Int TargetShopLocation;
    public string Name;
    private Vector2Int TargetShopRootLocation;
    public CustomerNeeds customerneeds;
    private Vector2Int LastSHopTriedToGoTo_ROOT;
    private List<Vector2Int> ShopsUsed;
    private List<Vector2Int> BenchesUsed;
    private List<Vector2Int> BinsUsed;
    private List<Vector2Int> ToiletsUsed;
    private List<Vector2Int> ATMSUsed;
    public List<int> BenchesUsedUID;
    public List<int> ToiletsUsedUID;
    public List<int> BinsUsedUID;
    public List<int> ATMsUsedUID;
    public List<int> ShopsUsedUID;
    private Vector2Int CurrentQuestTarget;
    private DirectionPressed LookThisWayToInteract;
    public CustomerQuest _CurrentQuest = CustomerQuest.Count;
    public CustomerQuest _NextQuest = CustomerQuest.Count;
    private SatisfactionType nextquestsatisfactiontype;
    public List<PersonAttachementType> UnnequipedAttachments;
    private PersonAttachementType EatingThis = PersonAttachementType.Count;
    private float EatTimer;
    public bool IsAtBusWaiting;
    public float SaltDose;
    public float CornSyrupDose;
    public float CaffieneDose;
    public float SugarDose;
    public float ChilliDose;
    public int QueueFails;
    private float QueuePatience;
    private bool WaitingToPopQueuIcon;
    private int CellUID;
    private Vector2Int RootLocationOfTargetBuilding;
    private Vector2Int InternalBuildingOffset;
    public bool WaitingToUseShop;
    public bool OnTheWayToUseShoporViewAnimal;
    private bool UsingShop;
    private float ServingTime;
    private ShopEntry Ref_WaitingToUseThisShop;
    public int QueueNumber;
    public PathNavigator refpathnavigator;
    public bool IsVIP;
    public bool JustMovedorSoldShop;
    private GroupNavigator GroupNavigator;
    private AnimalType FavouriteAnimal;
    public AnimalWelfareController animalwelfarecontroller;
    public CriticalChoiceVIP criticalchoiceVIP;
    private CustomerQuest _QuestForStringRender = CustomerQuest.Count;
    private bool WillGoOffGridToReachTarget;
    private float ForceSkipQuestForThisLong;
    private CustomerQuest TempOverrideQuest = CustomerQuest.Count;
    private Vector2Int PreOverrideOldTarget;
    private Vector2Int PreOverride_CurrentQuestTarget;
    private Vector2Int PreOverride_TargetShopLocation;
    private Vector2Int PreOverride_RootLocationOfTargetBuilding;
    private CustomerQuest PreOverrideOldQuest = CustomerQuest.Count;
    private ShopEntry PREOVERRIDE_Ref_WaitingToUseThisShop;
    private static Vector2Int ShopVec = new Vector2Int();
    internal static float Energy_LessThat_HuntBench_E_Food = 0.4f;
    internal static float LeavePark_Energy = 0.05f;
    internal static float HungerToLookForFood = 0.6f;
    internal static float LeavePark_HungerValue = 0.999f;
    internal static float ThresholdForWantingBathroom = 0.6f;
    internal static float LeavePark_BathroomValue = 0.95f;
    internal static float LeavePark_ChilliValue = 0.95f;
    internal static float LeavePark_ThirstValue = 0.95f;
    internal static float WelfareLeaveParkThreshold = 0.1f;
    internal static float GlobalHighestCutOff = 0.6f;
    private float DesireForThisQuest;
    private int RandomNavCounter;
    private static List<CustomerQuest> AnythingToEatOrDrink;

    public ParkLeavingReason LeftTheParkBecauseOfThis
    {
      get => this._LeftTheParkBecauseOfThis;
      set => this._LeftTheParkBecauseOfThis = value;
    }

    public MemberOfThePublic(
      AnimalType persontype,
      CellblockMananger cellblockcontainer,
      bool _IsVIP,
      Player player,
      CustomerType customertype,
      WalkingPerson parent)
    {
      this.IsVIP = (uint) customertype > 0U;
      this.UnnequipedAttachments = new List<PersonAttachementType>();
      switch (customertype)
      {
        case CustomerType.AnimalWelfareOfficer:
          this.animalwelfarecontroller = new AnimalWelfareController(player);
          this.customerneeds = new CustomerNeeds(false, true);
          break;
        case CustomerType.ResearchGrantGuy:
        case CustomerType.AnimalArtist:
        case CustomerType.GenomeBetaGiver:
          this.criticalchoiceVIP = new CriticalChoiceVIP(customertype);
          this.customerneeds = new CustomerNeeds(false, true);
          break;
        default:
          this.RandomNavCounter = TinyZoo.Game1.Rnd.Next(0, 10);
          this.FavouriteAnimal = AnimalData.GetFavouriteAnimal();
          this.animalstosee = new AnimalsToSee_CheckList(cellblockcontainer, this.FavouriteAnimal, player);
          this.TrashHeld = new List<TrashType>();
          this.ShopsUsed = new List<Vector2Int>();
          this.BinsUsed = new List<Vector2Int>();
          this.ShopsUsedUID = new List<int>();
          this.BinsUsedUID = new List<int>();
          this.ATMsUsedUID = new List<int>();
          this.ToiletsUsedUID = new List<int>();
          this.ATMSUsed = new List<Vector2Int>();
          this.BenchesUsed = new List<Vector2Int>();
          this.BenchesUsedUID = new List<int>();
          this.ToiletsUsed = new List<Vector2Int>();
          this.generalwellbeing = new GeneralWellbeing();
          this.purchaseledger = new CustomerLedger();
          this.customerneeds = new CustomerNeeds(this.animalstosee.HasFavouriteAnimal, false);
          break;
      }
      this.SetRandomName(persontype);
      CalculateStat.RecalculateCash = true;
    }

    public void SetRandomName(AnimalType persontype)
    {
      int Choice;
      if (persontype == AnimalType.TigerKing)
        this.Name = "Joe";
      else if (persontype == AnimalType.BlackMarketDealer)
        this.Name = "Arnold";
      else if (!EnemyData.IsThisAGirl(persontype))
        this.Name = MaleNames.GetMaleName(out Choice);
      else
        this.Name = FemalNames.GetName(out Choice);
    }

    public void CompletedUsingShop() => this._QuestForStringRender = this.CurrentQuest;

    public void HoldTimeComplete()
    {
      if (this.LeftParkEarly)
      {
        this._QuestForStringRender = CustomerQuest.LeftPark;
      }
      else
      {
        if (this._QuestForStringRender == CustomerQuest.IsBeingServedAtShop && this.UsingShop)
          return;
        this._QuestForStringRender = this.CurrentQuest;
      }
    }

    public string GetCurrentActionDisplayString(
      SimPerson simperson,
      WalkingPerson walkingperson,
      ref string subText)
    {
      if (this._QuestForStringRender == CustomerQuest.LeftPark)
      {
        subText = CurrentActionDisplay.GetLeaveParkReasonToString(simperson.memberofthepublic.LeftTheParkBecauseOfThis);
        return "Leaving Park";
      }
      if (this._QuestForStringRender < CustomerQuest.UsingShop)
      {
        if (this.Ref_WaitingToUseThisShop != null && this.QueueNumber > 0 && walkingperson.pathnavigator.GetPathLength() <= this.QueueNumber)
          subText = "Queue # " + (object) this.QueueNumber;
        return CurrentActionDisplay.GetCustomerQuestToString(this._QuestForStringRender);
      }
      if (this._QuestForStringRender == CustomerQuest.IsBeingServedAtShop)
      {
        if (this.Ref_WaitingToUseThisShop.tiletype == TILETYPE.ChocolateVendingMachine || this.Ref_WaitingToUseThisShop.tiletype == TILETYPE.DrinksVendingMachine || this.Ref_WaitingToUseThisShop.tiletype == TILETYPE.SnacksVendingMachine)
          return "Using Vendor";
        return TileData.IsThisABench(this.Ref_WaitingToUseThisShop.tiletype) ? "Using Bench" : "Using Shop";
      }
      if (this._QuestForStringRender == CustomerQuest.LookingAtAnimal)
        return "Looking at Animal";
      if (this._QuestForStringRender == CustomerQuest.InQueueForShop)
        return "Queuing at Shop";
      if (this.LeftParkEarly)
      {
        subText = CurrentActionDisplay.GetLeaveParkReasonToString(simperson.memberofthepublic.LeftTheParkBecauseOfThis);
        return "Leaving Park";
      }
      if ((double) walkingperson.HoldTime <= 0.0)
        return CurrentActionDisplay.GetCustomerQuestToString(this._CurrentQuest);
      if (this._QuestForStringRender == CustomerQuest.LookingAtAnimal)
        return "Looking at Animal";
      return this.UsingShop ? TileData.GetTileStats(this.Ref_WaitingToUseThisShop.tiletype).Name : "No thing found";
    }

    public CustomerQuest CurrentQuestForText
    {
      get => this._QuestForStringRender;
      set
      {
        if (value != CustomerQuest.Count)
          this._QuestForStringRender = value;
        int questForStringRender = (int) this._QuestForStringRender;
      }
    }

    public CustomerQuest CurrentQuest
    {
      get => this._CurrentQuest;
      set
      {
        this.CurrentQuestForText = value;
        this._CurrentQuest = value;
      }
    }

    public CustomerQuest NextQuest
    {
      get => this._NextQuest;
      set => this._NextQuest = value;
    }

    public void AddTrash(TrashType attachmenttrash) => this.TrashHeld.Add(attachmenttrash);

    public void AddGroupNavigation(GroupNavigator _GroupNavigator) => this.GroupNavigator = _GroupNavigator;

    public void AddCurrentAction(PersonAttachementType personattachmenttype)
    {
      if (PeopleAttachmentData.GetAttachInfo(personattachmenttype).attachmentlocation != AttachmentLocation.LeftHand)
        return;
      this.EatingThis = personattachmenttype;
      this.EatTimer = MathStuff.getRandomFloat(10f, 30f);
    }

    public void CancelActionAndStartReturn(
      ref bool BlockAutoWalk,
      ref bool WalkPaused,
      WalkingPerson parent)
    {
      if (this.Ref_WaitingToUseThisShop == null)
        return;
      this.EndShopQuest(ref BlockAutoWalk, ref WalkPaused, parent);
    }

    public void CancelAllActions(ref bool WalkPaused, ref bool BlockAutoWalk)
    {
      if (this.Ref_WaitingToUseThisShop != null)
      {
        this.Ref_WaitingToUseThisShop.RemoveMeFromAllListsOnReset(this);
        this.Ref_WaitingToUseThisShop = (ShopEntry) null;
      }
      this.WaitingToUseShop = false;
      this.UsingShop = false;
      this.PreOverrideOldQuest = CustomerQuest.Count;
      this.TempOverrideQuest = CustomerQuest.Count;
      this.CurrentQuestForText = CustomerQuest.Count;
      this.TargetShopLocation = (Vector2Int) null;
      this.RootLocationOfTargetBuilding = (Vector2Int) null;
      this.CurrentQuest = CustomerQuest.Count;
      this.NextQuest = CustomerQuest.Count;
      this.CurrentQuestTarget = (Vector2Int) null;
      WalkPaused = false;
      BlockAutoWalk = false;
      this.OnTheWayToUseShoporViewAnimal = false;
    }

    private void EndShopQuest(ref bool BlockAutoWalk, ref bool WalkPaused, WalkingPerson parent)
    {
      this.UsingShop = false;
      this.CurrentQuestTarget = (Vector2Int) null;
      this.Ref_WaitingToUseThisShop.RemoveMeFromAllListsOnReset(this);
      WalkPaused = false;
      this.WaitingToUseShop = false;
      this.CurrentQuest = CustomerQuest.Count;
      this.TargetShopLocation = (Vector2Int) null;
      this.RootLocationOfTargetBuilding = (Vector2Int) null;
      this.CurrentQuest = CustomerQuest.Count;
      this.NextQuest = CustomerQuest.Count;
      this.CurrentQuestTarget = (Vector2Int) null;
      BlockAutoWalk = false;
      this.OnTheWayToUseShoporViewAnimal = false;
      parent.CancelExtraOffset();
      if (this.TempOverrideQuest == CustomerQuest.Count)
        return;
      this.TempOverrideQuest = CustomerQuest.Count;
      this.Ref_WaitingToUseThisShop = (ShopEntry) null;
    }

    public bool BlockWalkFroSpecialCharacter(WalkingPerson parent) => parent.simperson.customertype == CustomerType.AnimalWelfareOfficer && this.animalwelfarecontroller.BlockWalkFroSpecialCharacter();

    public void UpdateMemberOfThePublic(
      WalkingPerson parent,
      float DeltaTime,
      Player player,
      ref bool WalkPaused,
      ref bool BlockAutoWalk,
      ref bool IsPlayingWalkAnimation)
    {
      int tempOverrideQuest = (int) this.TempOverrideQuest;
      if (this.JustMovedorSoldShop)
      {
        this.JustMovedorSoldShop = false;
        if (parent.simperson.customertype != CustomerType.Normal && parent.simperson.customertype == CustomerType.AnimalWelfareOfficer)
          this.animalwelfarecontroller.CancelAllActions();
        this.CancelAllActions(ref WalkPaused, ref BlockAutoWalk);
      }
      else
      {
        if (parent.simperson.ForcePopUpOnEnterPark && OverWorldManager.zoopopupHolder.IsNull())
        {
          if (parent.simperson.customertype == CustomerType.BlackMarket && !player.Stats.TutorialsComplete[31])
          {
            player.Stats.TutorialsComplete[31] = true;
            OverWorldManager.zoopopupHolder.CreateFeatureReveal(FeatureUnlockDisplayType.VIPIntro);
            parent.simperson.ForcePopUpOnEnterPark = false;
          }
          else
          {
            OverWorldManager.zoopopupHolder.CreateZooPopUps(parent, player);
            parent.simperson.ForcePopUpOnEnterPark = false;
          }
        }
        if (!this.IsAtBusWaiting && (this.LeftTheParkBecauseOfThis == ParkLeavingReason.TicketTooExpensive || this.LeftTheParkBecauseOfThis == ParkLeavingReason.VIPEND_EveryoneElseLeft) && (parent.pathnavigator.CurrentTile.CompareMatches(parent.ThisPersonStartLocation) && (parent.pathnavigator.GetCurrentPath() == null || parent.pathnavigator.GetCurrentPath().Count == 0)) && !this.IsAtBusWaiting)
        {
          CalculateStat.RecalculateCash = true;
          this.SetWaitingForBus(parent);
          --CustomerManager.CustomersInPark_NotWaitingForBus;
          if (CustomerManager.IsAVIP(parent.simperson.customertype))
            --CustomerManager.VIP_BlackMarketEtc;
        }
        if (parent.simperson.customertype == CustomerType.Protestor)
          return;
        if (parent.simperson.customertype != CustomerType.Normal)
        {
          if (this.criticalchoiceVIP != null)
            this.criticalchoiceVIP.UpdateCriticalChoiceVIP(parent, player, DeltaTime, ref IsPlayingWalkAnimation, ref BlockAutoWalk);
          else if (parent.simperson.customertype == CustomerType.AnimalWelfareOfficer)
            this.animalwelfarecontroller.UpdateAnimalWelfareController(DeltaTime, ref WalkPaused, parent, player, ref BlockAutoWalk, ref IsPlayingWalkAnimation);
          if (!this.LeftParkEarly || parent.IsOnFinalWalkToBus)
            return;
          IsPlayingWalkAnimation = false;
          parent.TryToReturnToBus();
        }
        else
        {
          if (this.Ref_WaitingToUseThisShop == null && SpeechManager.SomeoneShouldSaySomething && (parent.pathnavigator.CurrentTile.Y < 223 && TinyZoo.Game1.Rnd.Next(0, 100) == 0))
          {
            SpeechManager.LastMessageAutoPop = 0.0f;
            SpeechManager.SomeoneShouldSaySomething = false;
            Z_NotificationManager.TryAndSaySomething(player, walkingperson: parent);
          }
          if (this.Ref_WaitingToUseThisShop != null && this.Ref_WaitingToUseThisShop.people_usingShop.Count > 0)
            this.Ref_WaitingToUseThisShop.people_usingShop.Contains(this);
          if (this.EatingThis != PersonAttachementType.Count && (double) this.EatTimer > 0.0)
          {
            this.EatTimer -= DeltaTime;
            if ((double) this.EatTimer <= 0.0 && !Z_GameFlags.Location_Directory.OneOfTheseIsNearby(POINT_OF_INTEREST.Bin, parent.pathnavigator.CurrentTile))
            {
              Z_TrashManager.DropTrash(parent.pathnavigator.CurrentTile, TrashDrop.GetPersonAttachementTypeToTrash(this.EatingThis));
              parent.RemoveAttachment(this.EatingThis);
              this.EatingThis = PersonAttachementType.Count;
            }
          }
          if (this.OnTheWayToUseShoporViewAnimal)
            this.UpdateGoingToQuest(parent, ref WalkPaused, DeltaTime, ref BlockAutoWalk, ref IsPlayingWalkAnimation);
          else
            this.UpdateQuestAndSwitchToQuest(DeltaTime, player, parent, ref BlockAutoWalk, ref WalkPaused);
        }
      }
    }

    public string GetThought(SimPerson parent)
    {
      if (!parent.IsFootballer)
        return this.customerneeds.GetThought();
      int num = TinyZoo.Game1.Rnd.Next(0, 3);
      if (parent.persontype == AnimalType.FootballCaptain)
      {
        if (num == 0)
          return "What a great game we had this morning. If only people knew how stressful it is being a captain";
        if (num == 1)
          return "The job of a football captain is a tough one, thats why I bring the team here to relax";
      }
      else if (parent.persontype == AnimalType.FootballCaptain)
      {
        if (num == 0)
          return "As a goalkeeper, my job is to always be in defence mode, ready to strike...if one of these animals attacks, I will be there to block it!";
        if (num == 1)
          return "The goal of a goal keeper is to never hear the other team shot the word GOAL! at the top of their voices.";
      }
      switch (TinyZoo.Game1.Rnd.Next(0, 5))
      {
        case 0:
          return "These animals are so elegant, I want to be as graceful as them when I play football";
        case 1:
          return "Animals are so cute, the total opposite of people on the fooitball field!";
        case 2:
          return "These animals make me so relaxed! I dont want to kick anything anymore.";
        case 3:
          return "I love watching videos on ZooTube of animals kicking footballs. Pigs are especially good at it.";
        default:
          return "We come here every sunday after the game. Maybe I just want to play more football!";
      }
    }

    public void SetWaitingForBus(WalkingPerson walkingperson)
    {
      if (this.IsAtBusWaiting)
        return;
      this.IsAtBusWaiting = true;
      if (walkingperson.simperson.customertype != CustomerType.AnimalWelfareOfficer)
        return;
      this.animalwelfarecontroller.FinishJob(walkingperson);
    }

    public void WalkedUsedEnergy(int TileWalked) => this.customerneeds.WalkedUsedEnergy(TileWalked);

    public void UpdateNeedsAndWants(float Cycles, WalkingPerson walkingperson) => this.customerneeds.UpdateNeedsAndWants(Cycles, walkingperson);

    public void SetUpCashHeld(int _TicketPrice, CellblockMananger cellblockcontainer)
    {
      if (this.animalstosee == null)
        throw new Exception("The code below was there before -  but you didnt want to pass the player in, and guessed this woudla actually not matter - because its instantiated above");
      int maxValue = 80 + PlayerStats.LandSize * 5;
      int minValue = 40;
      if (maxValue > 130)
      {
        minValue += (maxValue - 130) / 2;
        if (minValue > 80)
          minValue = 80;
        maxValue = 130 + (maxValue - 130) / 10;
        if (TinyZoo.Game1.Rnd.Next(0, 10) == 0)
          minValue = 40;
      }
      this.CashHeld = TinyZoo.Game1.Rnd.Next(minValue, maxValue);
      TinyZoo.Game1.Rnd.Next(0, 4);
      if (DebugFlags.TrashIsland)
      {
        this.TrashHeld.Add(TrashType.RedCan);
        this.TrashHeld.Add(TrashType.RedCan);
        this.TrashHeld.Add(TrashType.RedCan);
        this.TrashHeld.Add(TrashType.RedCan);
      }
      int cashHeld = this.CashHeld;
      this.CashHeld *= 100;
      this.StartingCash = this.CashHeld;
    }

    public void BuyThing(int Cost, FOODTYPE purchasedthing)
    {
      this.CashHeld -= Cost;
      this.purchaseledger.purchaseledgers.Add(new CustomerPurchaseLedger(Cost, purchasedthing));
    }

    public void TeleportedBackToBus(WalkingPerson walkingperson)
    {
      if (this.IsAtBusWaiting)
        return;
      this.SetWaitingForBus(walkingperson);
      --CustomerManager.CustomersInPark_NotWaitingForBus;
      if (!CustomerManager.IsAVIP(walkingperson.simperson.customertype))
        return;
      --CustomerManager.VIP_BlackMarketEtc;
    }

    public void ReachedOutOfThresholdTarget(Vector2Int CurrentLocation, WalkingPerson parent)
    {
      if (!parent.IsOnFinalWalkToBus || this.IsAtBusWaiting || (!CurrentLocation.CompareMatches(parent.ThisPersonStartLocation) || this.IsAtBusWaiting))
        return;
      this.SetWaitingForBus(parent);
      --CustomerManager.CustomersInPark_NotWaitingForBus;
      if (!CustomerManager.IsAVIP(parent.simperson.customertype))
        return;
      --CustomerManager.VIP_BlackMarketEtc;
    }

    public void ReachedTarget(
      Vector2Int CurrentLocation,
      Player player,
      Vector2 vLocationOfPerson,
      PathNavigator pathnavigator,
      out bool SetNewPath,
      WalkingPerson parent,
      ref bool BlockAutoWalk,
      ref bool IsWalking,
      ref Vector2Int ForceGoHere)
    {
      if (this.WaitingToUseShop)
        throw new Exception("gsf");
      if (parent.IsOnFinalWalkToBus && !this.IsAtBusWaiting && (CurrentLocation.CompareMatches(parent.ThisPersonStartLocation) && !this.IsAtBusWaiting))
      {
        this.SetWaitingForBus(parent);
        --CustomerManager.CustomersInPark_NotWaitingForBus;
      }
      SetNewPath = false;
      if (parent.simperson.customertype != CustomerType.Normal && parent.simperson.customertype != CustomerType.BlackMarket)
      {
        if (this.criticalchoiceVIP != null)
          this.criticalchoiceVIP.ReachedLocation(CurrentLocation, out SetNewPath, player, parent, ref ForceGoHere, ref IsWalking, ref BlockAutoWalk, this);
        else if (parent.simperson.customertype == CustomerType.AnimalWelfareOfficer)
          this.animalwelfarecontroller.ReachedLocation(CurrentLocation, out SetNewPath, player, parent, ref ForceGoHere, ref IsWalking, ref BlockAutoWalk, this);
        int num = SetNewPath ? 1 : 0;
      }
      else
      {
        if (this.CurrentQuestTarget != null && this.CurrentQuestTarget.CompareMatches(CurrentLocation))
        {
          Vector2Int internalBuildingOffset = this.InternalBuildingOffset;
          if (this.CurrentQuest == CustomerQuest.SeekingBin)
          {
            parent.RemoveAttachment(this.EatingThis);
            this.EatingThis = PersonAttachementType.Count;
            if (this.Ref_WaitingToUseThisShop.IsBin)
            {
              this.Ref_WaitingToUseThisShop.AddGarbageToBin(1);
              Player.carbon.DropCarbon(parent.vLocation, 5);
            }
            this.NextQuest = CustomerQuest.Count;
            this.CurrentQuest = CustomerQuest.Count;
            this.CurrentQuestTarget = (Vector2Int) null;
            MoneyRenderer.PopIcon(vLocationOfPerson, IconPopType.Trash);
          }
          else if (this.CurrentQuest == CustomerQuest.SeekingBathroom)
            this.ReachedQuestBuildingRoot(SatisfactionType.Bathroom, ref IsWalking, parent, CurrentLocation, ref BlockAutoWalk);
          else if (this.CurrentQuest == CustomerQuest.WantsToSeeAnimal)
          {
            this.CurrentQuest = CustomerQuest.Count;
            this.animalstosee.ReachedAnimal(this.CellUID);
            PrisonZone thisCellBlock = player.prisonlayout.GetThisCellBlock(this.CellUID);
            if (thisCellBlock != null)
            {
              float welfareAndCleanliness = thisCellBlock.WelfareAndCleanliness;
              bool flag = false;
              this.CurrentQuestForText = CustomerQuest.LookingAtAnimal;
              if (this.customerneeds.VisitedPen(welfareAndCleanliness))
              {
                this.LeftParkEarly = true;
                if (TinyZoo.Game1.Rnd.Next(0, NewDay_ByPen.PoopLeavingReason + NewDay_ByPen.CorpseLeavingReason) < NewDay_ByPen.CorpseLeavingReason)
                {
                  this.LeftTheParkBecauseOfThis = ParkLeavingReason.DeadAnimals;
                  SpeechManager.AddPersonAndComment(parent, SpeechEvent.ISeeDeadThings);
                  MoneyRenderer.PopIcon(vLocationOfPerson, IconPopType.Angry);
                  flag = true;
                }
                else
                {
                  flag = true;
                  this.LeftTheParkBecauseOfThis = ParkLeavingReason.EnclosureFilfth;
                  MoneyRenderer.PopIcon(vLocationOfPerson, IconPopType.Disgusted);
                  SpeechManager.AddPersonAndComment(parent, SpeechEvent.ISeePoop);
                }
              }
              if (!flag)
              {
                if ((double) welfareAndCleanliness >= 1.0)
                {
                  if (thisCellBlock.prisonercontainer.prisoners.Count > 0)
                  {
                    if (thisCellBlock.Temp_Popularity > 500 && TinyZoo.Game1.Rnd.Next(0, 5) == 0)
                      MoneyRenderer.PopIcon(vLocationOfPerson, IconPopType.SeeAnimalsLove);
                    else
                      MoneyRenderer.PopIcon(vLocationOfPerson, IconPopType.SeeAnimalsSmallLove);
                  }
                  else
                    MoneyRenderer.PopIcon(vLocationOfPerson, IconPopType.Bored);
                }
                else
                {
                  if (!flag)
                  {
                    float num = 0.0f;
                    if (thisCellBlock.prisonercontainer.prisoners.Count > 0)
                      num = (float) thisCellBlock.CorpseCount / (float) thisCellBlock.prisonercontainer.prisoners.Count;
                    else if (thisCellBlock.CorpseCount > 0)
                      num = 100f;
                    if ((double) num > (double) this.customerneeds.PersonHorrorForDeath)
                    {
                      flag = true;
                      MoneyRenderer.PopIcon(vLocationOfPerson, IconPopType.Angry);
                      this.customerneeds.AddThought(ThoughtType.Death, (float) (((double) num - (double) this.customerneeds.PersonHorrorForDeath) * 3.0));
                    }
                  }
                  if (!flag)
                  {
                    float num = 0.0f;
                    if (thisCellBlock.prisonercontainer.prisoners.Count > 0)
                      num = (float) thisCellBlock.TotalPoops / (float) thisCellBlock.prisonercontainer.prisoners.Count;
                    if ((double) num > (double) this.customerneeds.PersonHorrorForPoop)
                    {
                      flag = true;
                      this.customerneeds.AddThought(ThoughtType.Death, (float) (((double) num - (double) this.customerneeds.PersonHorrorForPoop) * 1.0));
                      MoneyRenderer.PopIcon(vLocationOfPerson, IconPopType.Disgusted);
                    }
                  }
                  if (!flag)
                  {
                    if (thisCellBlock.prisonercontainer.prisoners.Count == 0)
                      MoneyRenderer.PopIcon(vLocationOfPerson, IconPopType.Bored);
                    else if (thisCellBlock.Temp_Popularity > 500 && TinyZoo.Game1.Rnd.Next(0, 5) == 0)
                      MoneyRenderer.PopIcon(vLocationOfPerson, IconPopType.SeeAnimalsLove);
                    else if (thisCellBlock.Temp_Popularity > 100 && TinyZoo.Game1.Rnd.Next(0, thisCellBlock.Temp_Popularity) > 50)
                      MoneyRenderer.PopIcon(vLocationOfPerson, IconPopType.SeeAnimalsSmallLove);
                    else
                      MoneyRenderer.PopIcon(vLocationOfPerson, IconPopType.Bored);
                  }
                }
              }
              parent.ForceRotationAndHold(this.LookThisWayToInteract, (float) TinyZoo.Game1.Rnd.Next(4, 11));
              if (Player.financialrecords.GetDaysPassed() > 1L)
                Z_NotificationManager.TryAndSaySomething(player, this.CellUID, parent);
            }
            else
              MoneyRenderer.PopIcon(vLocationOfPerson, IconPopType.Confused);
          }
          else if (this.CurrentQuest == CustomerQuest.SeekingBench)
            this.ReachedQuestBuildingRoot(SatisfactionBarAndText.GetQuestToSatisfaction(this.CurrentQuest), ref IsWalking, parent, CurrentLocation, ref BlockAutoWalk);
          else if (this.CurrentQuest == CustomerQuest.SeekingDrink || this.CurrentQuest == CustomerQuest.SeekingFood || (this.CurrentQuest == CustomerQuest.SeekingIceCream || this.CurrentQuest == CustomerQuest.SeekingATM) || (this.CurrentQuest == CustomerQuest.SeekingSouvenier || this.CurrentQuest == CustomerQuest.SeekingBuyingSouvenirsBeforeLeavingPark || (this.CurrentQuest == CustomerQuest.SeekingBench || this.CurrentQuest == CustomerQuest.SeekingBathroom)) || this.CurrentQuest == CustomerQuest.SeekingBin)
          {
            this.ReachedQuestBuildingRoot(SatisfactionBarAndText.GetQuestToSatisfaction(this.CurrentQuest), ref IsWalking, parent, CurrentLocation, ref BlockAutoWalk);
            return;
          }
        }
        if (this.TargetShopLocation == null || !this.TargetShopLocation.CompareMatches(CurrentLocation))
          return;
        player.shopstatus.TryAndUseThisShop(this.generalwellbeing, this.TargetShopRootLocation, ref this.CashHeld, this.purchaseledger, player, vLocationOfPerson, this);
        this.TargetShopLocation = (Vector2Int) null;
      }
    }

    public void StartQueingAtShop(ShopEntry UseThisShop)
    {
      this.OnTheWayToUseShoporViewAnimal = false;
      this.WaitingToUseShop = true;
      this.Ref_WaitingToUseThisShop = UseThisShop;
    }

    private void StartDrinksQuest()
    {
      this.DesireForThisQuest = this.customerneeds.CurrentWantValues[2];
      this.NextQuest = CustomerQuest.SeekingDrink;
      this.nextquestsatisfactiontype = SatisfactionType.Thirst;
    }

    private void StartSouvenierQuest()
    {
      this.NextQuest = CustomerQuest.SeekingSouvenier;
      this.DesireForThisQuest = this.customerneeds.CurrentWantValues[11];
      this.nextquestsatisfactiontype = SatisfactionType.Souvenirs;
    }

    private void StartATMQuest()
    {
      this.NextQuest = CustomerQuest.SeekingATM;
      this.DesireForThisQuest = 1000f;
      this.nextquestsatisfactiontype = SatisfactionType.GetCash;
    }

    private void StartFoodQuest()
    {
      this.NextQuest = CustomerQuest.SeekingFood;
      this.DesireForThisQuest = this.customerneeds.CurrentWantValues[1];
      this.nextquestsatisfactiontype = SatisfactionType.Hunger;
    }

    private bool FindAnyFoodOrDrink(WalkingPerson parent, PathNavigator pathnavigator)
    {
      if (MemberOfThePublic.AnythingToEatOrDrink == null)
      {
        MemberOfThePublic.AnythingToEatOrDrink = new List<CustomerQuest>();
        MemberOfThePublic.AnythingToEatOrDrink.Add(CustomerQuest.SeekingIceCream);
        MemberOfThePublic.AnythingToEatOrDrink.Add(CustomerQuest.SeekingFood);
        MemberOfThePublic.AnythingToEatOrDrink.Add(CustomerQuest.SeekingDrink);
      }
      for (int index = 0; index < MemberOfThePublic.AnythingToEatOrDrink.Count; ++index)
      {
        SatisfactionType shophunt = SatisfactionType.IceCream;
        if (MemberOfThePublic.AnythingToEatOrDrink[index] == CustomerQuest.SeekingDrink)
          shophunt = SatisfactionType.Thirst;
        else if (MemberOfThePublic.AnythingToEatOrDrink[index] == CustomerQuest.SeekingFood)
          shophunt = SatisfactionType.Hunger;
        this.RootLocationOfTargetBuilding = ShopNavigation.TryToGoToNerestSpecificShop(this.CashHeld, shophunt, pathnavigator, this.ShopsUsed, ref this.Ref_WaitingToUseThisShop, this.ShopsUsedUID, ref this.InternalBuildingOffset, out this.WillGoOffGridToReachTarget);
        if (this.RootLocationOfTargetBuilding != null)
        {
          this.SetQueuing(parent);
          this.TargetShopRootLocation = new Vector2Int(this.RootLocationOfTargetBuilding);
          this.CurrentQuest = MemberOfThePublic.AnythingToEatOrDrink[index];
          this.NextQuest = CustomerQuest.Count;
          this.CurrentQuestTarget = pathnavigator.GetPathLength() <= 0 ? new Vector2Int(pathnavigator.CurrentTile) : pathnavigator.GetEndOfCurrentPath();
          return true;
        }
      }
      return false;
    }

    private void ReachedTargetWhenLookingForShop(Player player, ref bool BlockAutoWalk) => throw new Exception("CHECK THIS - DO YOU NEED TO CALL TEH FOLLOWING COMMENTED OUT LINE?");

    private void TryAndStartFinalShopQuestBeforeLeavingTheZoo()
    {
      if (TinyZoo.Game1.Rnd.Next(0, 2) == 0)
      {
        this.NextQuest = CustomerQuest.SeekingBuyingSouvenirsBeforeLeavingPark;
        this.DesireForThisQuest = 0.5f;
      }
      else
      {
        this.NextQuest = CustomerQuest.SeekingAnyFoodOrDrink;
        this.DesireForThisQuest = 0.5f;
      }
    }

    private bool TryToStartQuest(
      SatisfactionType TryToGoHere,
      PathNavigator pathnavigator,
      WalkingPerson parent)
    {
      switch (TryToGoHere)
      {
        case SatisfactionType.Energy:
          this.RootLocationOfTargetBuilding = ShopNavigation.TryToGoToNerestSpecificShop(this.CashHeld, TryToGoHere, pathnavigator, this.BenchesUsed, ref this.Ref_WaitingToUseThisShop, this.BenchesUsedUID, ref this.InternalBuildingOffset, out this.WillGoOffGridToReachTarget);
          if (this.RootLocationOfTargetBuilding != null)
          {
            this.BenchesUsed.Add(this.RootLocationOfTargetBuilding);
            break;
          }
          break;
        case SatisfactionType.Bathroom:
          this.RootLocationOfTargetBuilding = ShopNavigation.TryToGoToNerestSpecificShop(this.CashHeld, TryToGoHere, pathnavigator, this.ToiletsUsed, ref this.Ref_WaitingToUseThisShop, this.ToiletsUsedUID, ref this.InternalBuildingOffset, out this.WillGoOffGridToReachTarget);
          if (this.RootLocationOfTargetBuilding != null)
          {
            this.ToiletsUsed.Add(this.RootLocationOfTargetBuilding);
            break;
          }
          break;
        case SatisfactionType.Bin:
          this.RootLocationOfTargetBuilding = ShopNavigation.TryToGoToNerestSpecificShop(this.CashHeld, TryToGoHere, pathnavigator, this.BinsUsed, ref this.Ref_WaitingToUseThisShop, this.BinsUsedUID, ref this.InternalBuildingOffset, out this.WillGoOffGridToReachTarget);
          if (this.RootLocationOfTargetBuilding != null)
          {
            this.BinsUsed.Add(this.RootLocationOfTargetBuilding);
            break;
          }
          break;
        case SatisfactionType.GetCash:
          this.RootLocationOfTargetBuilding = ShopNavigation.TryToGoToNerestSpecificShop(this.CashHeld, TryToGoHere, pathnavigator, this.ATMSUsed, ref this.Ref_WaitingToUseThisShop, this.ATMsUsedUID, ref this.InternalBuildingOffset, out this.WillGoOffGridToReachTarget);
          if (this.RootLocationOfTargetBuilding != null)
          {
            this.ATMSUsed.Add(this.RootLocationOfTargetBuilding);
            break;
          }
          break;
        default:
          this.RootLocationOfTargetBuilding = ShopNavigation.TryToGoToNerestSpecificShop(this.CashHeld, TryToGoHere, pathnavigator, this.ShopsUsed, ref this.Ref_WaitingToUseThisShop, this.ShopsUsedUID, ref this.InternalBuildingOffset, out this.WillGoOffGridToReachTarget);
          if (this.RootLocationOfTargetBuilding != null)
          {
            this.ShopsUsed.Add(this.RootLocationOfTargetBuilding);
            break;
          }
          break;
      }
      if (this.RootLocationOfTargetBuilding != null && pathnavigator.GetPathLength() < 10)
      {
        if (this.Ref_WaitingToUseThisShop.people_walkingTo_SHOP.Count > 0 && pathnavigator.GetPathLength() < this.Ref_WaitingToUseThisShop.people_walkingTo_SHOP[this.Ref_WaitingToUseThisShop.people_walkingTo_SHOP.Count - 1].refpathnavigator.GetPathLength())
          this.RootLocationOfTargetBuilding = (Vector2Int) null;
        if (this.Ref_WaitingToUseThisShop.people_usingShop.Count > 0 && pathnavigator.GetPathLength() < 1)
          this.RootLocationOfTargetBuilding = (Vector2Int) null;
      }
      if (this.RootLocationOfTargetBuilding == null)
        return false;
      this.SetQueuing(parent);
      this.TargetShopRootLocation = new Vector2Int(this.RootLocationOfTargetBuilding);
      this.CurrentQuest = this.NextQuest;
      this.NextQuest = CustomerQuest.Count;
      this.CurrentQuestTarget = pathnavigator.GetEndOfCurrentPath();
      return true;
    }

    private void UpdateGoingToQuest(
      WalkingPerson parent,
      ref bool WalkPaused,
      float DeltaTime,
      ref bool BlockAutoWalk,
      ref bool IsPlayingWalkAnimation)
    {
      int num = 0;
      bool flag1 = true;
      if (!this.Ref_WaitingToUseThisShop.HasSpaceToBeServed())
        num = 1;
      if (this.QueueNumber + num > parent.pathnavigator.GetPathLength() | flag1)
      {
        PathNode pathTileByIndex1 = parent.pathnavigator.GetPathTileByIndex(1);
        bool flag2 = false;
        if (pathTileByIndex1 != null)
        {
          if (this.Ref_WaitingToUseThisShop.IsSomeoneInfrontOfMe(this.QueueNumber, pathTileByIndex1.XLoc, pathTileByIndex1.YLoc, this))
          {
            if (this.WaitingToPopQueuIcon)
            {
              MoneyRenderer.PopIcon(parent.vLocation, IconPopType.JoinedQueue);
              this.WaitingToPopQueuIcon = false;
            }
            flag2 = true;
          }
        }
        else
        {
          PathNode pathTileByIndex2 = parent.pathnavigator.GetPathTileByIndex();
          if (pathTileByIndex2 != null && this.Ref_WaitingToUseThisShop.IsSomeoneInfrontOfMe(this.QueueNumber, pathTileByIndex2.XLoc, pathTileByIndex2.YLoc, this))
          {
            if (this.WaitingToPopQueuIcon)
            {
              MoneyRenderer.PopIcon(parent.vLocation, IconPopType.JoinedQueue);
              this.WaitingToPopQueuIcon = false;
            }
            flag2 = true;
          }
        }
        if (flag2)
        {
          WalkPaused = true;
          this.QueuePatience -= DeltaTime;
          if ((double) this.QueuePatience >= 0.0)
            return;
          this.Ref_WaitingToUseThisShop.RemoveMeFromAllListsOnReset(this);
          this.OnTheWayToUseShoporViewAnimal = false;
          this.Ref_WaitingToUseThisShop.LeftQueueWithoutPaying();
          this.EndShopQuest(ref BlockAutoWalk, ref WalkPaused, parent);
          ++this.QueueFails;
          this.TargetShopRootLocation = (Vector2Int) null;
          this.CurrentQuestTarget = (Vector2Int) null;
          this.ForceSkipQuestForThisLong = 5f;
          parent.pathnavigator.ForceResetPath();
          parent.pathnavigator.TryToGoHere(parent.pathnavigator.CurrentTile, GameFlags.pathset);
          MoneyRenderer.PopIcon(parent.vLocation, IconPopType.LeftQueue);
          if (parent.pathnavigator.IsNavigating)
            return;
          IsPlayingWalkAnimation = false;
        }
        else
          WalkPaused = false;
      }
      else
      {
        if (!WalkPaused)
          return;
        WalkPaused = false;
      }
    }

    private void ReachedQuestBuildingRoot(
      SatisfactionType shopservesthis,
      ref bool IsWalking,
      WalkingPerson parent,
      Vector2Int CurrentLocation,
      ref bool BlockAutoWalk)
    {
      IsWalking = false;
      this.OnTheWayToUseShoporViewAnimal = false;
      this.WaitingToUseShop = true;
      this.Ref_WaitingToUseThisShop.AddPersonToUseShopList(this);
      BlockAutoWalk = true;
      parent.pathnavigator.TeleportHere(CurrentLocation);
    }

    private void UpdateQuestAndSwitchToQuest(
      float DeltaTime,
      Player player,
      WalkingPerson parent,
      ref bool BlockAutoWalk,
      ref bool WalkPaused)
    {
      if (!this.WaitingToUseShop)
        return;
      if (this.UsingShop)
      {
        this.ServingTime -= DeltaTime;
        if ((double) this.ServingTime > 0.0)
          return;
        if (this.Ref_WaitingToUseThisShop.shoptype == ShopEntryType.Shop)
        {
          if (this.Ref_WaitingToUseThisShop.TryAndUseThisShop(ref this.CashHeld, this.purchaseledger, this.generalwellbeing, player, parent.vLocation, this))
            this.ShopsUsedUID.Add(this.Ref_WaitingToUseThisShop.ShopUID);
          else
            this.ShopsUsedUID.Add(this.Ref_WaitingToUseThisShop.ShopUID);
        }
        else if (this.Ref_WaitingToUseThisShop.shoptype == ShopEntryType.Toilet)
        {
          this.customerneeds.CurrentWantValues[3] = 0.0f;
          this.ToiletsUsed.Add(new Vector2Int(this.RootLocationOfTargetBuilding));
          MoneyRenderer.PopIcon(parent.vLocation, IconPopType.UseToilet);
        }
        else if (this.Ref_WaitingToUseThisShop.shoptype == ShopEntryType.Bench)
        {
          this.customerneeds.CurrentWantValues[0] = 1f;
          MoneyRenderer.PopIcon(parent.vLocation, IconPopType.GetEnergy);
        }
        else if (this.Ref_WaitingToUseThisShop.shoptype == ShopEntryType.ATM && parent.simperson.memberofthepublic.CashHeld < 15000)
        {
          int Money = TinyZoo.Game1.Rnd.Next(10, 110) * 100;
          parent.simperson.memberofthepublic.CashHeld += Money;
          MoneyRenderer.EarnMoney(parent.vLocation, Money, true);
        }
        this.EndShopQuest(ref BlockAutoWalk, ref WalkPaused, parent);
      }
      else if (this.Ref_WaitingToUseThisShop.TryToStartBeingServed(this, ref this.ServingTime))
      {
        if (this.Ref_WaitingToUseThisShop.DirectionCustomerWillFaceUsingAShop != DirectionPressed.None)
          this.LookThisWayToInteract = this.Ref_WaitingToUseThisShop.DirectionCustomerWillFaceUsingAShop;
        parent.ForceRotationAndHold(this.LookThisWayToInteract, this.ServingTime);
        this.OnTheWayToUseShoporViewAnimal = false;
        this.UsingShop = true;
        parent.ForceRotationAndHold(this.LookThisWayToInteract, 0.1f);
        if (this.CurrentQuest != CustomerQuest.SeekingBench)
          return;
        if (this.InternalBuildingOffset != null)
        {
          if (this.InternalBuildingOffset.Y == -1 && this.InternalBuildingOffset.X == 0)
          {
            parent.SetExtraOffset(new Vector2(0.0f, BenchData.GetBenchOffset(this.Ref_WaitingToUseThisShop.tiletype, DirectionPressed.Down) * Sengine.ScreenRatioUpwardsMultiplier.Y), 0);
            parent.ForceRotationAndHold(DirectionPressed.Down, this.ServingTime);
          }
          else if (this.InternalBuildingOffset.Y == 1 && this.InternalBuildingOffset.X == 0)
          {
            parent.SetExtraOffset(new Vector2(0.0f, BenchData.GetBenchOffset(this.Ref_WaitingToUseThisShop.tiletype, DirectionPressed.Up) * Sengine.ScreenRatioUpwardsMultiplier.Y), TileData.GetTileInfo(player.prisonlayout.layout.BaseTileTypes[this.TargetShopRootLocation.X, this.TargetShopRootLocation.Y].tiletype).GetTileHeight(2) + 1);
            parent.ForceRotationAndHold(DirectionPressed.Up, this.ServingTime);
          }
          else if (this.InternalBuildingOffset.Y == 0 && this.InternalBuildingOffset.X == -1)
          {
            parent.SetExtraOffset(new Vector2(BenchData.GetBenchOffset(this.Ref_WaitingToUseThisShop.tiletype, DirectionPressed.Left), 0.0f), TileData.GetTileInfo(player.prisonlayout.layout.BaseTileTypes[this.TargetShopRootLocation.X, this.TargetShopRootLocation.Y].tiletype).GetTileHeight(1) + 1);
            parent.ForceRotationAndHold(DirectionPressed.Left, this.ServingTime);
          }
          else
          {
            parent.SetExtraOffset(new Vector2(BenchData.GetBenchOffset(this.Ref_WaitingToUseThisShop.tiletype, DirectionPressed.Right), 0.0f), TileData.GetTileInfo(player.prisonlayout.layout.BaseTileTypes[this.TargetShopRootLocation.X, this.TargetShopRootLocation.Y].tiletype).GetTileHeight(1) + 1);
            parent.ForceRotationAndHold(DirectionPressed.Right, this.ServingTime);
          }
        }
        else
          parent.ForceRotationAndHold(DirectionPressed.Down, this.ServingTime);
      }
      else
      {
        if (this.CurrentQuestForText != CustomerQuest.InQueueForShop && this.CurrentQuestForText != CustomerQuest.IsBeingServedAtShop)
          return;
        this.CurrentQuestForText = CustomerQuest.Count;
      }
    }

    public void HitPointOfInterest(Vector2Int Location, Player player, WalkingPerson walkingperson)
    {
      if (Z_DebugFlags.IsBetaVersion || walkingperson.simperson.customertype != CustomerType.Normal || (this.Ref_WaitingToUseThisShop != null || this.NextQuest != CustomerQuest.Count) || this.TempOverrideQuest != CustomerQuest.Count)
        return;
      Vector5Int pointOfInterest = PathFindingManager.entranceblockmanager.GetPointOfInterest(Location);
      if (TileData.IsForFood((TILETYPE) pointOfInterest.W) && this.EatingThis == PersonAttachementType.Count)
        this.TempOverrideQuest = CustomerQuest.SeekingFood;
      else if (TileData.IsThisAnATM((TILETYPE) pointOfInterest.W))
        this.TempOverrideQuest = CustomerQuest.SeekingATM;
      else if (TileData.IsForThirst((TILETYPE) pointOfInterest.W) && this.EatingThis == PersonAttachementType.Count)
        this.TempOverrideQuest = CustomerQuest.SeekingDrink;
      else if (TileData.IsForSouvenir((TILETYPE) pointOfInterest.W))
        this.TempOverrideQuest = CustomerQuest.SeekingSouvenier;
      if (this.RootLocationOfTargetBuilding != null)
        this.PreOverride_RootLocationOfTargetBuilding = new Vector2Int(this.RootLocationOfTargetBuilding);
      MemberOfThePublic.ShopVec.X = Location.X;
      MemberOfThePublic.ShopVec.Y = Location.Y;
      if (this.TempOverrideQuest == CustomerQuest.Count)
        return;
      switch (pointOfInterest.Z)
      {
        case 0:
          --MemberOfThePublic.ShopVec.Y;
          break;
        case 1:
          ++MemberOfThePublic.ShopVec.X;
          break;
        case 2:
          ++MemberOfThePublic.ShopVec.Y;
          break;
        case 3:
          --MemberOfThePublic.ShopVec.X;
          break;
      }
      ShopEntry shopEntry;
      if (player.prisonlayout.layout.BaseTileTypes[MemberOfThePublic.ShopVec.X, MemberOfThePublic.ShopVec.Y].GetIsChild())
        shopEntry = this.TempOverrideQuest != CustomerQuest.SeekingATM ? player.shopstatus.GetThisShop(player.prisonlayout.layout.BaseTileTypes[MemberOfThePublic.ShopVec.X, MemberOfThePublic.ShopVec.Y].GetParentLocation(), (TILETYPE) pointOfInterest.W) : player.shopstatus.GetThisATM(player.prisonlayout.layout.BaseTileTypes[MemberOfThePublic.ShopVec.X, MemberOfThePublic.ShopVec.Y].GetParentLocation(), (TILETYPE) pointOfInterest.W);
      else if (this.TempOverrideQuest == CustomerQuest.SeekingATM)
      {
        shopEntry = player.shopstatus.GetThisATM(MemberOfThePublic.ShopVec, (TILETYPE) pointOfInterest.W, true);
        if (shopEntry == null)
          this.TempOverrideQuest = CustomerQuest.Count;
      }
      else
      {
        shopEntry = player.shopstatus.GetThisShop(MemberOfThePublic.ShopVec, (TILETYPE) pointOfInterest.W, true);
        if (shopEntry == null)
          this.TempOverrideQuest = CustomerQuest.Count;
      }
      if (shopEntry != null)
      {
        if (shopEntry.people_usingShop.Count > 0 || shopEntry.people_walkingTo_SHOP.Count > 0)
        {
          this.TempOverrideQuest = CustomerQuest.Count;
        }
        else
        {
          if (this.CurrentQuest == CustomerQuest.Count)
          {
            int nextQuest = (int) this.NextQuest;
          }
          this.PreOverride_RootLocationOfTargetBuilding = (Vector2Int) null;
          this.PreOverride_TargetShopLocation = (Vector2Int) null;
          if (walkingperson.pathnavigator.IsNavigating)
          {
            if (this.TargetShopLocation != null)
              this.PreOverride_TargetShopLocation = new Vector2Int(this.TargetShopLocation);
            if (this.TargetShopRootLocation != null)
              this.PreOverride_RootLocationOfTargetBuilding = new Vector2Int(this.TargetShopRootLocation);
            this.PreOverrideOldQuest = this.CurrentQuest;
            this.PreOverrideOldTarget = new Vector2Int(walkingperson.pathnavigator.GetEndOfCurrentPath());
            if (this.CurrentQuestTarget != null)
              this.PreOverride_CurrentQuestTarget = new Vector2Int(this.CurrentQuestTarget);
          }
          this.CurrentQuestTarget = new Vector2Int(Location);
          this.CurrentQuest = this.TempOverrideQuest;
          walkingperson.pathnavigator.TryToGoHere(Location, GameFlags.pathset);
          this.TargetShopLocation = new Vector2Int(this.CurrentQuestTarget);
          this.PREOVERRIDE_Ref_WaitingToUseThisShop = this.Ref_WaitingToUseThisShop;
          this.Ref_WaitingToUseThisShop = shopEntry;
          this.SetQueuing(walkingperson);
        }
      }
      else
        this.TempOverrideQuest = CustomerQuest.Count;
    }

    public void CheckGoSomewhereSpecific(
      ref Vector2Int TargetLocation,
      Vector2Int CurrentLocation,
      PathNavigator pathnavigator,
      CustomerType customertype,
      WalkingPerson walkingperson)
    {
      if (TrailerDemoFlags.HasTrailerFlag || customertype != CustomerType.Normal || (this.NextQuest != CustomerQuest.Count || this.CurrentQuest != CustomerQuest.Count))
        return;
      if (this.RandomNavCounter > 0 && !walkingperson.IsOnTravelator)
        --this.RandomNavCounter;
      else if ((double) this.EatTimer <= 0.0 && this.EatingThis != PersonAttachementType.Count)
      {
        this.DesireForThisQuest = 0.1f;
        this.NextQuest = CustomerQuest.SeekingBin;
      }
      else
      {
        if ((double) this.EatTimer > 0.0 || this.CurrentQuest != CustomerQuest.Count)
          return;
        if ((double) this.customerneeds.CurrentWantValues[13] >= 1.0 && ((double) this.customerneeds.CurrentWantValues[13] >= 2.0 || TinyZoo.Game1.Rnd.Next(0, 4) == 0))
        {
          this.customerneeds.CurrentWantValues[0] *= 0.5f;
          if ((double) this.customerneeds.CurrentWantValues[17] > 0.0)
            this.customerneeds.CurrentWantValues[17] = 0.0001f;
          Z_TrashManager.DropTrash(CurrentLocation, TrashType.Vomit);
          this.customerneeds.CurrentWantValues[13] = 0.0f;
          MoneyRenderer.PopIcon(walkingperson.vLocation, IconPopType.Disgusted);
        }
        Vector2Int vector2Int = new Vector2Int();
        float num = Math.Max(Math.Max(Math.Max(this.customerneeds.CurrentWantValues[2], this.customerneeds.CurrentWantValues[1]), this.customerneeds.CurrentWantValues[10]), this.customerneeds.CurrentWantValues[3]);
        if ((double) num > (double) MemberOfThePublic.GlobalHighestCutOff && (double) num > 1.0 - (double) this.customerneeds.CurrentWantValues[0])
        {
          if ((double) this.customerneeds.CurrentWantValues[10] == (double) num)
          {
            this.DesireForThisQuest = this.customerneeds.CurrentWantValues[10];
            if (ShopNavigation.shopsbytype[8] != null && ShopNavigation.shopsbytype[8].shops.Count > 0)
            {
              this.DesireForThisQuest = this.customerneeds.CurrentWantValues[8];
              this.NextQuest = CustomerQuest.SeekingIceCream;
              this.nextquestsatisfactiontype = SatisfactionType.IceCream;
              return;
            }
            if ((double) this.customerneeds.CurrentWantValues[10] >= (double) MemberOfThePublic.LeavePark_ChilliValue)
            {
              this.LeftTheParkBecauseOfThis = ParkLeavingReason.NoIcecreamForChilli;
              this.LeftParkEarly = true;
            }
          }
          if ((double) this.customerneeds.CurrentWantValues[2] > (double) this.customerneeds.CurrentWantValues[1])
          {
            if (ShopNavigation.shopsbytype[2] != null && ShopNavigation.shopsbytype[2].shops.Count > 0)
            {
              this.StartDrinksQuest();
              return;
            }
            if ((double) this.customerneeds.CurrentWantValues[2] >= (double) MemberOfThePublic.LeavePark_ThirstValue)
            {
              this.LeftTheParkBecauseOfThis = ParkLeavingReason.NoDrinks;
              this.LeftParkEarly = true;
              SpeechManager.AddPersonAndComment(walkingperson, SpeechEvent.CustomerThirst);
            }
          }
          if ((double) this.customerneeds.CurrentWantValues[3] > (double) MemberOfThePublic.ThresholdForWantingBathroom)
          {
            if (ShopNavigation.shopsbytype[3] != null && ShopNavigation.shopsbytype[3].shops.Count > 0)
            {
              this.NextQuest = CustomerQuest.SeekingBathroom;
              this.DesireForThisQuest = this.customerneeds.CurrentWantValues[3];
              this.nextquestsatisfactiontype = SatisfactionType.Bathroom;
              return;
            }
            if ((double) this.customerneeds.CurrentWantValues[3] >= (double) MemberOfThePublic.LeavePark_BathroomValue)
            {
              this.LeftTheParkBecauseOfThis = ParkLeavingReason.NoToilets;
              this.LeftParkEarly = true;
              SpeechManager.AddPersonAndComment(walkingperson, SpeechEvent.CustomerToilet);
            }
          }
          if ((double) this.customerneeds.CurrentWantValues[1] > (double) MemberOfThePublic.HungerToLookForFood)
          {
            if (ShopNavigation.shopsbytype[1] != null && ShopNavigation.shopsbytype[1].shops.Count > 0)
            {
              this.StartFoodQuest();
              return;
            }
            if ((double) this.customerneeds.CurrentWantValues[1] >= (double) MemberOfThePublic.LeavePark_HungerValue)
            {
              SpeechManager.AddPersonAndComment(walkingperson, SpeechEvent.CustomerHunger);
              this.LeftTheParkBecauseOfThis = ParkLeavingReason.NoFood;
              this.LeftParkEarly = true;
            }
          }
        }
        else if ((double) this.customerneeds.CurrentWantValues[0] < (double) MemberOfThePublic.Energy_LessThat_HuntBench_E_Food)
        {
          if (ShopNavigation.shopsbytype[0] != null && ShopNavigation.shopsbytype[0].shops.Count > 0)
          {
            this.NextQuest = CustomerQuest.SeekingBench;
            this.DesireForThisQuest = 1f - this.customerneeds.CurrentWantValues[0];
            this.nextquestsatisfactiontype = SatisfactionType.Energy;
            return;
          }
          if ((double) this.customerneeds.CurrentWantValues[0] < (double) MemberOfThePublic.LeavePark_Energy)
          {
            this.LeftTheParkBecauseOfThis = ParkLeavingReason.NoBenches;
            SpeechManager.AddPersonAndComment(walkingperson, SpeechEvent.CustomerBench);
            this.LeftParkEarly = true;
          }
        }
        if (this.animalstosee.CheckWantsToVisitAnimal())
        {
          this.NextQuest = CustomerQuest.WantsToSeeAnimal;
        }
        else
        {
          if (this.customerneeds.MaxVistsToATM > 0 && this.CashHeld < this.customerneeds.WillLookForATMWhenLessThanThis)
          {
            if (ShopNavigation.shopsbytype[18] != null && ShopNavigation.shopsbytype[18].shops.Count > 0)
            {
              this.StartATMQuest();
              return;
            }
            if ((double) this.CashHeld < (double) this.customerneeds.WillLookForATMWhenLessThanThis * 0.300000011920929)
            {
              SpeechManager.AddPersonAndComment(walkingperson, SpeechEvent.NoATM_NoMoney);
              this.LeftTheParkBecauseOfThis = ParkLeavingReason.NoATM_NoMoney;
              this.LeftParkEarly = true;
            }
          }
          if (this.CashHeld <= 0)
            return;
          if (TinyZoo.Game1.Rnd.Next(0, 4) > 0)
          {
            this.TryAndStartFinalShopQuestBeforeLeavingTheZoo();
          }
          else
          {
            SpeechManager.AddPersonAndComment(walkingperson, SpeechEvent.NothigLeftToDo);
            this.LeftTheParkBecauseOfThis = ParkLeavingReason.NothingLeftToDo;
            this.LeftParkEarly = true;
          }
        }
      }
    }

    public bool TryToStartNextQuest(
      PathNavigator pathnavigator,
      Player player,
      out ParkLeavingReason leavepark,
      WalkingPerson parent)
    {
      leavepark = ParkLeavingReason.None;
      if (this.NextQuest != CustomerQuest.Count && this.CurrentQuest == CustomerQuest.Count)
      {
        if (this.NextQuest == CustomerQuest.SeekingBin)
        {
          if (this.EatingThis != PersonAttachementType.Count)
          {
            if (this.TryToStartQuest(SatisfactionType.Bin, pathnavigator, parent))
              return true;
            parent.RemoveAttachment(this.EatingThis);
            Z_TrashManager.DropTrash(parent.pathnavigator.CurrentTile, TrashDrop.GetPersonAttachementTypeToTrash(this.EatingThis));
            this.EatingThis = PersonAttachementType.Count;
            this.NextQuest = CustomerQuest.Count;
          }
          return false;
        }
        if (this.NextQuest == CustomerQuest.WantsToSeeAnimal)
        {
          bool NothingToSee;
          this.CellUID = this.animalstosee.GetNextPenUID(out NothingToSee);
          if (!NothingToSee)
          {
            PrisonZone thisCellBlock = player.prisonlayout.cellblockcontainer.GetThisCellBlock(this.CellUID);
            if (thisCellBlock != null)
            {
              if (thisCellBlock.viewinglocations == null)
                OverWorldManager.heatmapmanager.DoubleCheckAnimalPrivacySetUp(player);
              if (thisCellBlock.TryAndGetPathToViewingLocation(pathnavigator, out this.LookThisWayToInteract))
              {
                this.CurrentQuest = this.NextQuest;
                this.NextQuest = CustomerQuest.Count;
                this.CurrentQuestTarget = pathnavigator.GetEndOfCurrentPath();
                return true;
              }
              this.animalstosee.RemoveAnimal(this.CellUID);
              this.NextQuest = CustomerQuest.Count;
            }
            else
            {
              this.NextQuest = CustomerQuest.None;
              MoneyRenderer.PopIcon(parent.vLocation, IconPopType.Confused);
            }
          }
          else
            this.animalstosee.RemoveAnimal(this.CellUID);
        }
        else
        {
          if (this.NextQuest == CustomerQuest.SeekingBuyingSouvenirsBeforeLeavingPark)
          {
            if (this.TryToStartQuest(SatisfactionType.Souvenirs, pathnavigator, parent))
              return true;
            leavepark = ParkLeavingReason.NothingLeftToDo;
            this.NextQuest = CustomerQuest.Count;
            this.CurrentQuest = CustomerQuest.Count;
            return false;
          }
          if (this.NextQuest == CustomerQuest.SeekingAnyFoodOrDrink)
          {
            if (this.FindAnyFoodOrDrink(parent, pathnavigator))
              return true;
            leavepark = ParkLeavingReason.NothingLeftToDo;
            this.NextQuest = CustomerQuest.Count;
            this.CurrentQuest = CustomerQuest.Count;
            return false;
          }
          if (this.NextQuest == CustomerQuest.SeekingBench)
          {
            if (this.TryToStartQuest(SatisfactionType.Energy, pathnavigator, parent))
              return true;
            this.NextQuest = CustomerQuest.Count;
            this.CurrentQuest = CustomerQuest.Count;
          }
          else if (this.NextQuest == CustomerQuest.SeekingBathroom)
          {
            if (this.TryToStartQuest(SatisfactionType.Bathroom, pathnavigator, parent))
              return true;
          }
          else if (this.NextQuest == CustomerQuest.SeekingDrink)
          {
            if (this.TryToStartQuest(SatisfactionType.Thirst, pathnavigator, parent))
              return true;
          }
          else if (this.NextQuest == CustomerQuest.SeekingSouvenier)
          {
            if (this.TryToStartQuest(SatisfactionType.Souvenirs, pathnavigator, parent))
              return true;
          }
          else if (this.NextQuest == CustomerQuest.SeekingFood)
          {
            if (this.TryToStartQuest(SatisfactionType.Hunger, pathnavigator, parent))
              return true;
          }
          else if (this.NextQuest == CustomerQuest.SeekingATM)
          {
            if (this.TryToStartQuest(SatisfactionType.GetCash, pathnavigator, parent))
              return true;
          }
          else if (this.NextQuest != CustomerQuest.None)
            throw new Exception("THIS IS ALL GARBAGE");
        }
      }
      return false;
    }

    public void SetQueuing(WalkingPerson parent)
    {
      this.refpathnavigator = parent.pathnavigator;
      this.OnTheWayToUseShoporViewAnimal = true;
      this.Ref_WaitingToUseThisShop.AddPersonOnWayToShop(this);
      int minValue = 10 + (int) (15.0 * (double) this.DesireForThisQuest);
      int maxValue = (int) (30.0 * (1.0 + (double) this.DesireForThisQuest * 2.0));
      this.QueuePatience = (float) TinyZoo.Game1.Rnd.Next(minValue, maxValue);
      this.WaitingToPopQueuIcon = true;
    }
  }
}
