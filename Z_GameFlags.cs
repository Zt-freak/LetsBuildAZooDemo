// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GameFlags
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.PeopleAndBeams;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_BalanceSystems.Customers.NewCustomers;
using TinyZoo.Z_BalanceSystems.ProductionLines;
using TinyZoo.Z_DayNight;
using TinyZoo.Z_HeatMaps;
using TinyZoo.Z_HUD.ControlHint;
using TinyZoo.Z_Manage.MainButtons;
using TinyZoo.Z_OverWorld;
using TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components;
using TinyZoo.Z_OverWorld.PathFinding_Nodes;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo
{
  internal class Z_GameFlags
  {
    internal static int TotalLivingAnimalsInZoo = 0;
    internal static bool IsMovingSomething;
    internal static bool ScrubForSaleSigns = false;
    internal static bool HoldTheClock = false;
    internal static bool ForceResolutionNextFrame = false;
    internal static bool DeselectPenInPenSelect = false;
    internal static bool DidLoadSave = false;
    internal static bool TopBarIsBlockedForTutorial = true;
    internal static int TotalGarbageTrucksToComeToday = 0;
    internal static bool ScrubOtherHeroQuest;
    internal static bool UsingCustomMouse = true;
    internal static bool HasStartedFirstDay = false;
    internal static bool QuestToOpenZooStarted = false;
    internal static bool HasViewedTasks = false;
    internal static bool IsWaitingToReturnToControllerWalk = false;
    internal static bool ForceControllerMode = false;
    internal static bool JustloadedGame = false;
    internal static bool SettingWasFromTitleScreen = false;
    internal static bool HasBuiltStoreRoom = false;
    internal static bool BusStartsOnSCreenEdge = false;
    internal static int MustForceStartThisManyBreakOutsAfterLoad = 0;
    internal static float DefaultMouseOverAlphaValue = 0.3f;
    internal static bool DidSomethingWithWater = false;
    internal static bool HasCompleteQuestsToView = false;
    internal static bool RecheckZooKeeperZones = false;
    internal static bool MouseIsOverAPanel;
    internal static bool MouseIsOverAPanel_SoBlockZoom;
    internal static bool RecalculatedMorality;
    internal static bool MustRebuildWaterMap;
    internal static bool MustRebuildPrivacyMap;
    internal static bool MustRebuildHygieneMap;
    internal static HeatMapType DRAW_heatmaptype = HeatMapType.None;
    internal static bool HasUsedAControllerThisSession;
    internal static bool BlockPointer = true;
    internal static LocationDirectory Location_Directory;
    internal static bool UseNewPathFndingSystem = true;
    internal static PathFindingManager pathfinder = new PathFindingManager();
    internal static int SelectedPrisonZoneUID;
    internal static bool SelectedPrisonZoneisFarm = false;
    internal static List<int> JUSTMovedTheseEnclosures = new List<int>();
    internal static ManageButtonType ForeToManageState = ManageButtonType.Count;
    private static ForceToNewScreen _ForceToNewScreen = ForceToNewScreen.None;
    internal static int LookAtThisAnimal_UID;
    internal static float SecondsInDay = 720f;
    internal static float SecondsZooOpenPerDay = 240f;
    internal static bool HasPassedMidnight;
    internal static float ZooOpenTime_InSeconds = 270f;
    internal static float SecondsInQuarterDay = 180f;
    internal static float DaysInOneYear = 30f;
    internal static float NightFall = 570f;
    internal static float SunRise = 210f;
    internal static float DayTimer;
    internal static ControllerHintSummary ForceControllerHintsToThe = ControllerHintSummary.Count;
    internal static int TrashUID;
    internal static List<VIP_Entry> SpecialPeopleOnBus = new List<VIP_Entry>();
    internal static PenItem SelectedPenItem;
    internal static int BuildOrder_DebugTrailer = 0;
    internal static bool IsDay = false;
    internal static Vector2Int ForceClickSelectionFromSmartCursor;
    internal static bool CheckDeaths = false;
    internal static bool CameToStoreRoomFromManagePen = false;
    internal static AnimalFoodType StoreRoomGoToThisFood = AnimalFoodType.Count;
    internal static bool ForceToShopOnEnteringStoreRoom = false;
    internal static bool SaveNextFrame = false;
    internal static bool LoadNextFrame = false;
    internal static bool ForceRightMouseDrag = false;
    internal static bool ForceRightAndLeftMouseDrag = false;
    internal static AvatarController avatarcontroller;
    internal static List<Vector3Int> WaterTroughStates;
    internal static bool PurchasedOrSoldABus;
    internal static List<AnimalsOnOrderSign> animalsonordersigns;
    internal static List<FarmSign> farmsigns;
    internal static PrisonZone SelectedCellInfo;
    internal static int SelectedSaveSlot = 0;
    internal static bool JustExitedWorldMap_CheckAnimalExistsInAnimalPanel = true;
    internal static int ShopID = 0;
    private static int EnrichmentUID = 0;
    internal static List<AnimalFinderSummary> breakoutAnimals = new List<AnimalFinderSummary>();
    private static float SecondsPerHour;
    private static float SecondsPerMinute;
    private static DateTime start = new DateTime(2018, 1, 1);
    internal static bool TempBlockVirtualMouse;
    internal static List<Vector2Int> PenFloorTilesJustBlocked;

    internal static float GetBaseScaleForUI()
    {
      if ((double) PlayerStats.UXMult <= 0.0)
        PlayerStats.UXMult = PlayerStats.UXMult = (float) UIScaleSettings.GetDefaultUIScale(out int _);
      return RenderMath.GetPixelZoomOneToOne() * (PlayerStats.UXMult * 0.5f);
    }

    internal static int GetFloatToRating(float Value)
    {
      if ((double) Value >= 0.899999976158142)
        return 0;
      if ((double) Value >= 0.699999988079071)
        return 1;
      return (double) Value >= 0.449999988079071 ? 2 : 3;
    }

    internal static float GetBaseScaleForCustomFont(float Scale)
    {
      if ((double) PlayerStats.UXMult == -1.0)
      {
        double baseScaleForUi = (double) Z_GameFlags.GetBaseScaleForUI();
      }
      return (double) PlayerStats.UXMult % 2.0 == 1.0 ? RenderMath.GetPixelZoomOneToOne() * (float) (((double) PlayerStats.UXMult + 1.0) * 0.5) : Scale;
    }

    internal static SpringFont GetSmallFont(ref float Scale)
    {
      Scale = Z_GameFlags.GetBaseScaleForCustomFont(Scale);
      return (double) PlayerStats.UXMult % 2.0 == 0.0 ? AssetContainer.SinglePixelFontX1AndHalf : AssetContainer.springFont;
    }

    internal static bool UseMipMap() => (double) Sengine.WorldOriginandScale.Z < (double) RenderMath.GetPixelZoomOneToOne();

    internal static int GetShopUID()
    {
      ++Z_GameFlags.ShopID;
      return Z_GameFlags.ShopID - 1;
    }

    internal static int GetEnrichmentUID()
    {
      ++Z_GameFlags.EnrichmentUID;
      return Z_GameFlags.EnrichmentUID - 1;
    }

    internal static void TryAndRemoveOnOrderSign(int PrisonUID)
    {
      for (int index = Z_GameFlags.animalsonordersigns.Count - 1; index > -1; --index)
      {
        if (Z_GameFlags.animalsonordersigns[index].PrisonUID == PrisonUID)
          Z_GameFlags.animalsonordersigns.RemoveAt(index);
      }
    }

    internal static bool GetAllowTopBar() => !OverWorldManager.IsGameIntro && !Z_GameFlags.TopBarIsBlockedForTutorial;

    internal static AnimalsOnOrderSign GetCollidedWithAnimalsOnOrderSign(
      int XLoc,
      int YLoc)
    {
      for (int index = 0; index < Z_GameFlags.animalsonordersigns.Count; ++index)
      {
        if (Z_GameFlags.animalsonordersigns[index].Location.X == XLoc && (Z_GameFlags.animalsonordersigns[index].Location.Y == YLoc || Z_GameFlags.animalsonordersigns[index].Location.Y - 1 == YLoc))
          return Z_GameFlags.animalsonordersigns[index];
      }
      return (AnimalsOnOrderSign) null;
    }

    internal static bool HasCollidedWithAnimalsOnOrderSign(int XLoc, int YLoc)
    {
      for (int index = 0; index < Z_GameFlags.animalsonordersigns.Count; ++index)
      {
        if (Z_GameFlags.animalsonordersigns[index].Location.X == XLoc && (Z_GameFlags.animalsonordersigns[index].Location.Y == YLoc || Z_GameFlags.animalsonordersigns[index].Location.Y - 1 == YLoc))
          return true;
      }
      return false;
    }

    internal static void SetAnimalInBreakOut(Vector2Int Location, PrisonerInfo prisonerinfo)
    {
      for (int index = 0; index < Z_GameFlags.breakoutAnimals.Count; ++index)
      {
        if (Z_GameFlags.breakoutAnimals[index].Ref_prisoninfo == prisonerinfo)
        {
          Z_GameFlags.breakoutAnimals[index].Location.X = Location.X;
          Z_GameFlags.breakoutAnimals[index].Location.Y = Location.Y;
          return;
        }
      }
      Z_GameFlags.breakoutAnimals.Add(new AnimalFinderSummary(Location, prisonerinfo));
    }

    internal static void SetDefaults()
    {
      Z_GameFlags.animalsonordersigns = new List<AnimalsOnOrderSign>();
      Z_GameFlags.SecondsPerHour = Z_GameFlags.SecondsInDay / 24f;
      Z_GameFlags.SecondsPerMinute = Z_GameFlags.SecondsPerHour / 60f;
    }

    internal static int GetTimeUntilThisInHours(float SecondInDay) => (int) ((double) SecondInDay / (double) Z_GameFlags.SecondsInDay * 24.0) - (int) ((double) Z_GameFlags.DayTimer / (double) Z_GameFlags.SecondsInDay * 24.0);

    internal static string GetTimeAsStringFomMinutes(int Minutes)
    {
      int num = Minutes / 60;
      if (num <= 0)
        return Minutes.ToString() + " m";
      Minutes -= num * 60;
      return num.ToString() + "h : " + (object) Minutes + " m";
    }

    internal static int GetTimeUntilThisInMinutesFromMinutes(float MinuteInDay)
    {
      float num = (float) ((double) Z_GameFlags.DayTimer / (double) Z_GameFlags.SecondsPerHour * 60.0);
      return (double) num < (double) MinuteInDay ? (int) ((double) MinuteInDay - (double) num) : 0;
    }

    internal static int GetAbstractTimerToMinutes(float SecondInDay) => (int) ((double) SecondInDay / (double) Z_GameFlags.SecondsPerHour * 60.0);

    internal static int GetTimeUntilThisInMinutes(float SecondInDay)
    {
      SecondInDay -= Z_GameFlags.DayTimer;
      return (int) ((double) SecondInDay / (double) Z_GameFlags.SecondsPerHour * 60.0);
    }

    internal static void ControlFastNight(float SimulationTime, Player player)
    {
      if (Z_GameFlags.HoldTheClock)
        return;
      if ((double) Z_GameFlags.DayTimer > (double) Z_GameFlags.ZooOpenTime_InSeconds)
      {
        Z_GameFlags.DayTimer += SimulationTime * 100f;
        if ((double) Z_GameFlags.DayTimer <= (double) Z_GameFlags.SecondsInDay)
          return;
        Z_GameFlags.DayTimer = 0.0f;
        DayNightManager.CommitFincncialsAtMidnight(player);
        Z_GameFlags.HasPassedMidnight = true;
      }
      else
      {
        Z_GameFlags.DayTimer += SimulationTime * 100f;
        if ((double) Z_GameFlags.DayTimer <= (double) Z_GameFlags.ZooOpenTime_InSeconds)
          return;
        Z_GameFlags.DayTimer = Z_GameFlags.ZooOpenTime_InSeconds;
      }
    }

    internal static float GetTimeThatParkOpensInMorning_Seconds() => Z_GameFlags.ZooOpenTime_InSeconds;

    internal static float GetInGameSecondsPerHour() => Z_GameFlags.SecondsPerHour;

    internal static float GetInGameSecondsToMinutes(float SECS) => SECS / Z_GameFlags.SecondsPerMinute;

    internal static string GetTimeAsStringFromMinutes(int TotalMinutes) => Z_GameFlags.FormatFloatToTime((float) TotalMinutes * Z_GameFlags.SecondsPerMinute, true);

    internal static string GetTimeAsString(float Overridevalue = -1f) => (double) Overridevalue > -1.0 ? Z_GameFlags.FormatFloatToTime(Overridevalue / Z_GameFlags.SecondsInDay, true) : Z_GameFlags.FormatFloatToTime(Z_GameFlags.DayTimer / Z_GameFlags.SecondsInDay, true);

    internal static void HandleNullingselectedtileandsell()
    {
      if (Z_GameFlags.IsWaitingToReturnToControllerWalk)
      {
        Z_GameFlags.IsWaitingToReturnToControllerWalk = false;
        OverWorldManager.overworldstate = OverWOrldState.PlayingAsAvatar;
      }
      SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
    }

    internal static string FormatFloatToTime(float Value, bool SimpleReturn = false)
    {
      int num1 = (int) ((double) Value * 24.0);
      int num2 = (int) Math.Floor(((double) Value * 24.0 - Math.Floor((double) Value * 24.0)) * 60.0);
      string str = string.Concat((object) num2);
      if (num2 < 10)
        str = "0" + (object) num2;
      if (!SimpleReturn)
        return num1.ToString() + "h " + str + "m";
      if (num1 >= 12)
      {
        int num3 = num1 - 12;
        if (num3 == 0)
          num3 = 12;
        return num3.ToString() + ":" + str + "pm";
      }
      return num1.ToString() + ":" + str + "am";
    }

    internal static string GetGameDateToday_AsString()
    {
      DateTime dateTime = Z_GameFlags.start.AddDays((double) Player.financialrecords.GetDaysPassed());
      return dateTime.ToString("dd MMM").TrimStart('0') + ", Year " + (object) (1 + dateTime.Year - Z_GameFlags.start.Year);
    }

    internal static float GetMinutesToSecondsInDay(float Minutes) => Z_GameFlags.SecondsPerMinute * Minutes;

    internal static void ReinitializeGameOnLoadNewSave() => ProductionLineCalc.ReinitialzeOnGameStart();

    internal static float GetPercentageThroughDay() => Z_GameFlags.DayTimer / Z_GameFlags.SecondsInDay;

    internal static float GetClosingTime() => Z_GameFlags.ZooOpenTime_InSeconds + Z_GameFlags.SecondsZooOpenPerDay;

    internal static float GetClosingTimeInMinutes() => (float) (((double) Z_GameFlags.ZooOpenTime_InSeconds + (double) Z_GameFlags.SecondsZooOpenPerDay) / (double) Z_GameFlags.SecondsPerHour * 60.0);

    internal static bool ParkIsOpen() => (double) Z_GameFlags.DayTimer >= (double) Z_GameFlags.ZooOpenTime_InSeconds && (double) Z_GameFlags.DayTimer <= (double) Z_GameFlags.ZooOpenTime_InSeconds + (double) Z_GameFlags.SecondsZooOpenPerDay;

    internal static ForceToNewScreen ForceToNewScreen
    {
      get => Z_GameFlags._ForceToNewScreen;
      set => Z_GameFlags._ForceToNewScreen = value;
    }

    internal static bool UseBuildCam() => OverWorldManager.overworldstate == OverWOrldState.Build && Game1.gamestate == GAMESTATE.OverWorld;

    internal static int GetSalaryThisWeekUntilNow(int Salary, Player player, int DaysEmployed = 7)
    {
      int num = (int) Player.financialrecords.GetDaysPassed() % 7;
      if (DaysEmployed < num)
        num = DaysEmployed;
      float percentageThroughDay = Z_GameFlags.GetPercentageThroughDay();
      if (num < 0)
        num = 0;
      return num * Salary + (int) ((double) percentageThroughDay * (double) Salary);
    }

    internal static void SetHeatMapType(HeatMapType heatmap) => Z_GameFlags.DRAW_heatmaptype = heatmap;

    internal static bool BlockVirtualMouse()
    {
      if (Z_GameFlags.ForceControllerMode && OverWorldManager.overworldstate != OverWOrldState.MoveBuilding && OverWorldManager.overworldstate != OverWOrldState.Build || Game1.gamestate == GAMESTATE.ArchitectResearch)
        return true;
      return OverWorldManager.overworldstate == OverWOrldState.PlayingAsAvatar && Game1.gamestate == GAMESTATE.OverWorld;
    }

    internal static void SetPenFloorTilesOnCollsionsChanged(List<Vector2Int> floors)
    {
      if (Z_GameFlags.PenFloorTilesJustBlocked == null)
        Z_GameFlags.PenFloorTilesJustBlocked = new List<Vector2Int>();
      for (int index = 0; index < floors.Count; ++index)
        Z_GameFlags.PenFloorTilesJustBlocked.Add(floors[index]);
    }

    internal static bool IsTheBlockedByNewPenFloor(int Xloc, int YLoc)
    {
      if (Z_GameFlags.PenFloorTilesJustBlocked == null)
        return false;
      for (int index = 0; index < Z_GameFlags.PenFloorTilesJustBlocked.Count; ++index)
      {
        if (Z_GameFlags.PenFloorTilesJustBlocked[index].X == Xloc && Z_GameFlags.PenFloorTilesJustBlocked[index].Y == YLoc)
          return true;
      }
      return false;
    }

    internal static bool SkipDrawSmartCursor()
    {
      if (OverWorldManager.overworldstate == OverWOrldState.Build || WalkingPerson.SkipSmartCuror || (AnimalsInPens.MouseIsOverAnimal || TrailerDemoFlags.HasTrailerFlag))
        return true;
      if (OverWorldManager.overworldstate != OverWOrldState.Manage && !OverwoldMainButtons.MouseIsOverAButton)
        return false;
      OverwoldMainButtons.MouseIsOverAButton = false;
      return true;
    }

    internal static string GetCostAsDOllarsAndCentsFromInt(int COSTIncludingCents)
    {
      string str1 = string.Concat((object) COSTIncludingCents);
      if (str1.Length == 1)
        return "0.0" + str1;
      if (str1.Length == 2)
        return "0." + str1;
      string str2 = "";
      for (int index = 0; index < str1.Length; ++index)
      {
        if (index == str1.Length - 2)
          str2 += ".";
        str2 += str1[index].ToString();
      }
      return str2;
    }

    internal static string GetCostAsDOllarsAndCents(float COST)
    {
      string str = string.Concat((object) Math.Round((double) COST, 2));
      bool flag = false;
      int num = 0;
      for (int index = 0; index < str.Length; ++index)
      {
        if (str[index] == '.')
          flag = true;
        else if (flag)
          ++num;
      }
      if (!flag)
        str += ".";
      for (int index = 0; index < 2 - num; ++index)
        str += "0";
      return str;
    }

    internal static void ResetCollisionBlocks()
    {
      GameFlags.CollisionChanged = false;
      Z_GameFlags.PenFloorTilesJustBlocked = (List<Vector2Int>) null;
    }
  }
}
