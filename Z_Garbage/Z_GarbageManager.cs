// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Garbage.Z_GarbageManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;
using TinyZoo.Z_Bus;

namespace TinyZoo.Z_Garbage
{
  internal class Z_GarbageManager
  {
    private static List<GarbageBin> garbagebins;
    internal static bool CheckBins;
    internal static int BinsEmptiedToday;
    private static float GarbageSTartTime;
    internal static List<int> BinsEmptiedByTruck;
    private static float GapBetweenGarbage;

    public Z_GarbageManager(Player player)
    {
      Z_GarbageManager.garbagebins = new List<GarbageBin>();
      for (int index = 0; index < Player.garbage.TotalBins; ++index)
        Z_GarbageManager.garbagebins.Add(new GarbageBin(Z_GarbageManager.garbagebins.Count));
      Z_GarbageManager.BinsEmptiedByTruck = new List<int>();
    }

    internal static void StartDay()
    {
      Z_GarbageManager.BinsEmptiedToday = 0;
      Z_GarbageManager.GarbageSTartTime = Z_GameFlags.GetTimeThatParkOpensInMorning_Seconds() + Z_GameFlags.GetInGameSecondsPerHour() * 0.5f;
      if (Z_GameFlags.TotalGarbageTrucksToComeToday > 0)
        Z_GarbageManager.GapBetweenGarbage = Z_GameFlags.GetInGameSecondsPerHour() * 6f / (float) Z_GameFlags.TotalGarbageTrucksToComeToday;
      Z_GarbageManager.BinsEmptiedByTruck = new List<int>();
    }

    internal static void CollectedGarbage(int BinIndex)
    {
      if (BinIndex >= Z_GarbageManager.garbagebins.Count)
        return;
      Z_GarbageManager.garbagebins[BinIndex].SetBinState(0);
      int num = 1;
      Player.garbage.RemoveGarbage(1f);
      Player.carbon.DropCarbon(Z_GarbageManager.garbagebins[BinIndex].vLocation, num * 10);
    }

    internal static float GetBinLocation(out int BinIndex)
    {
      BinIndex = -1;
      if (Z_GarbageManager.BinsEmptiedToday >= Z_GarbageManager.garbagebins.Count)
        return 10000f;
      double x = (double) Z_GarbageManager.garbagebins[Z_GarbageManager.BinsEmptiedToday].vLocation.X;
      BinIndex = Z_GarbageManager.BinsEmptiedToday;
      ++Z_GarbageManager.BinsEmptiedToday;
      return (float) (x - 1.0);
    }

    public void UpdateZ_GarbageManager(Player player)
    {
      if (Z_GameFlags.TotalGarbageTrucksToComeToday > 0 && (double) Z_GameFlags.DayTimer >= (double) Z_GarbageManager.GarbageSTartTime)
      {
        Z_GarbageManager.GarbageSTartTime += Z_GarbageManager.GapBetweenGarbage;
        Z_BusManager.AddGarbageTruck();
        --Z_GameFlags.TotalGarbageTrucksToComeToday;
      }
      if (!Z_GarbageManager.CheckBins)
        return;
      Z_GarbageManager.CheckBins = false;
      int num1 = (int) Math.Ceiling((double) Player.garbage.GarbageHeld);
      int num2 = 0;
      if (Math.Floor((double) Player.garbage.GarbageHeld) == Math.Ceiling((double) Player.garbage.GarbageHeld))
        num2 = 2;
      else if ((double) Player.garbage.GarbageHeld - Math.Floor((double) Player.garbage.GarbageHeld) > 0.0)
        num2 = 1;
      while (num1 > Z_GarbageManager.garbagebins.Count)
        Z_GarbageManager.garbagebins.Add(new GarbageBin(Z_GarbageManager.garbagebins.Count));
      for (int index = 0; index < Z_GarbageManager.garbagebins.Count; ++index)
      {
        int BagsInThisBin = 0;
        if (index < num1)
        {
          BagsInThisBin = 2;
          if (index == num1 - 1)
            BagsInThisBin = num2;
        }
        Z_GarbageManager.garbagebins[index].SetBinState(BagsInThisBin);
      }
    }

    public void DrawZ_GarbageManager()
    {
      for (int index = 0; index < Z_GarbageManager.garbagebins.Count; ++index)
        Z_GarbageManager.garbagebins[index].DrawGarbageBin();
    }
  }
}
