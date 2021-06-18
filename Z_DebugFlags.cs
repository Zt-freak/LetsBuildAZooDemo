// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_DebugFlags
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo
{
  internal class Z_DebugFlags
  {
    internal static bool IsBetaVersion = true;
    internal static bool ZooTutoriallsDisabled = false;
    internal static bool AutoCompleteAllHeroQuests = false;
    internal static bool QuickCRISPRBreeds = false;
    internal static int ForceNumberOfJobApplicantsEachDay = -1;
    internal static bool DisableMoralityBlocks = true;
    internal static bool LotsOfPoop = false;
    internal static bool WillBlockLoadScreenRes = false;
    internal static bool TempNewBuildingMenu = true;
    internal static bool UnlockAllBuildingsHack = false;
    internal static int[] developerOverrides = new int[16];
    internal static string ForceLoadString = "";
    internal static bool ValidatePaths = true;
    internal static bool EmployeesCannotBeKilledByAnimals = true;
    internal static bool UseNewEndOfWeek = true;
    internal static bool UseRenderThreading = false;
    internal static bool HasSkipDay = false;
    internal static bool DrawWhiteStuffOnPerch = false;
    internal static bool Is4K = false;
    internal static int ForceThisManyCustomers = -1;
    internal static int ForcedPeoplePerDay = -1;
    internal static bool TempBlockMorePeopleSpawning = false;
    internal static bool DrawCollision = false;
    internal static bool DrawDistancesToDirectConnections = false;
    internal static bool DrawReverseConnections = false;
    internal static bool SaveAtEndOfDayOnly = true;
    internal static bool DrawCollisionOnAnimals = false;
    internal static bool HasDebugMenu = true;
    internal static AnimalFoodType AutoFood = AnimalFoodType.Count;
    internal static bool SimulationIsVerbose = false;
    internal static bool HasOnScreenLog = true;
    internal static bool DisplayFPS = false;
    internal static bool AllowMaleAndFemalBreedingPairs = true;
    internal static bool UnlockAllResearchOnGridRenderer = false;
    internal static CustomerQuest ForceToThisQuest = CustomerQuest.Count;
    internal static bool DisableRandomBirths = false;
    internal static bool LockZoomToOne = true;
    internal static bool AllowAvatarDirectControl = false;
  }
}
