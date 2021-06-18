// Decompiled with JetBrains decompiler
// Type: TinyZoo.TrailerDemoFlags
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace TinyZoo
{
  internal class TrailerDemoFlags
  {
    internal static bool PlayBusIntro = false;
    internal static bool AutoReveal = false;
    internal static int AutoRevealRange;
    internal static int ManualRevealRange;
    internal static bool PlayScaffoldAnimations;
    internal static float ScaffoldAnimationSpeedMult = 1f;
    internal static bool ZooKeeperRelative;
    internal static bool SkipDrawFloor;
    internal static bool HasTrailerFlag = false;
    internal static bool FreeCam;
    internal static float ManualRevealWaveDelay;
    internal static bool HideUI;
    internal static bool CustomPenSpawn;
    internal static bool DrawEffectsLayer;
    internal static bool AlwaysNight;
    internal static bool DisableSave;
    private static int FloorR;
    private static int FloorG;
    private static int FloorB;
    internal static Vector3 FloorColour;
    internal static bool RenderRoadAndFences;
    internal static bool DoNotDetachCameraOnR3;
    internal static float ChopperDropSpeed = 1f;
    internal static float ChopperMoveSpeed = 1f;
    internal static int CustomerSpawnRange = 1;
    internal static int CustomersToSpawn = 1;
    internal static List<int> PenRevealIndexes = new List<int>();
    internal static float AutoZoomTarget = 1f;
    internal static float AutoZoomTime = 1f;
    internal static float AutoZoomYTargetOffset;
    internal static float AutoZoomXTargetOffset;

    internal static void LoadTrailerFlags()
    {
    }

    internal static List<int> ConvertStringToListOfInts(string Text)
    {
      List<int> intList = new List<int>();
      string str = "";
      for (int index = 0; index < Text.Length; ++index)
      {
        if (Text[index] != ',')
        {
          str += Text[index].ToString();
        }
        else
        {
          intList.Add(Convert.ToInt32(str));
          str = "";
        }
      }
      return intList;
    }

    internal static void ProcessBool(string T, ref bool BOOOL)
    {
      BOOOL = T == "t";
      if (!BOOOL)
        return;
      TrailerDemoFlags.HasTrailerFlag = true;
    }
  }
}
