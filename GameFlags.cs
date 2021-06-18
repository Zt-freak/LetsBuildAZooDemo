// Decompiled with JetBrains decompiler
// Type: TinyZoo.GameFlags
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;

namespace TinyZoo
{
  internal class GameFlags
  {
    internal static bool HasPassedSplash = false;
    internal static int TotalArcadeLevels = 30;
    internal static bool TempDisableCamer = false;
    internal static bool IsBreakOut = false;
    internal static bool BountyMode = false;
    internal static bool CollisionChanged = false;
    internal static bool IsDay = false;
    internal static bool HasCompletedEnoughQuestsToStartDay = false;
    internal static bool HasDoneForceDraw = false;
    internal static bool HasNotch = true;
    internal static float NotchSize = 40f;
    internal static bool MobileUIScale = true;
    internal static bool IsConsoleVersion = true;
    internal static ControllerType SelectedControllerType = ControllerType.Xbone;
    internal static bool IsUsingMouse;
    internal static float PixelZoom = 4f;
    internal static int CrrentStage = 0;
    internal static Decimal FullZoneSize;
    internal static Decimal CurrentReclamedZones;
    internal static float TargetPercent;
    internal static int BeamInventoryAtStart;
    internal static int CurrentBeamInventory;
    internal static int EnemyCount;
    internal static int EnemyCountAtStart;
    internal static int BeamsLockedOrDead;
    internal static bool BlockOverWorldCamera = false;
    internal static bool AllowMouseControl = true;
    internal static float RefDeltaTime;
    internal static bool StatsBarIsOnScreen = false;
    internal static TILETYPE SelectedBuildTILETYPE = TILETYPE.None;
    internal static bool ForceExitBuildNow = false;
    internal static bool JustShuffled = false;
    internal static bool IsConstructingMapOnLoad = false;
    internal static bool prisonersJustChangedInHoldingCell = false;
    internal static bool CellBlockContentsChanged = false;
    internal static int MaxHoldngCell = 8;
    internal static bool DifficultyIsEasy = true;
    internal static int GraveYardMaxSize = 48;
    internal static int GraveYardMinimumSize = 16;
    internal static bool GraveYardUpdated = false;
    internal static bool NoStrobe = false;
    internal static float CycleSpeed = 0.1f;
    internal static bool IsUsingController = false;
    internal static bool Mobile_NoMomentum = true;
    internal static bool GamePaused = false;
    internal static bool IsArcadeMode;
    internal static int ArcadeLevel;
    internal static PathSet pathset;
    internal static PathSet Water_PathSet;

    internal static bool PhotoMode => Game1.gamestate == GAMESTATE.PhotoMode;

    internal static float GetTRCButtonScale() => Z_GameFlags.GetBaseScaleForUI();

    internal static ControllerButton GetCorrectButtonFace(ButtonPressed btn)
    {
      switch (btn)
      {
        case ButtonPressed.Confirm:
          return FlagSettings.SwapButtonsForSwitch ? ControllerButton.XboxB : ControllerButton.XboxA;
        case ButtonPressed.Back:
          return FlagSettings.SwapButtonsForSwitch ? ControllerButton.XboxA : ControllerButton.XboxB;
        default:
          return ControllerButton.XboxA;
      }
    }

    internal static float GetSmallTextScale() => DebugFlags.IsPCVersion ? 2f * Sengine.ScreenRationReductionMultiplier.Y : 3f;
  }
}
