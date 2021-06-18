// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Trailer.SpawnBlockArray
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_Trailer
{
  internal class SpawnBlockArray
  {
    internal static SpawnBlockComponent[,] spawnblocks;
    internal static SpawnBlockComponent[,] Floorspawnblocks;
    internal static SpawnBlockComponent[,] UnderFloorspawnblocks;
    internal static Vector2Int LastSetAvatarLocation;
    internal static float BigRingMult;
    internal static bool DoingBigRing;

    internal static void SetAvatarPostion(Vector2 personlocation)
    {
      SpawnBlockArray.LastSetAvatarLocation = TileMath.GetWorldSpaceToTile(personlocation);
      SpawnBlockArray.DoRing(TrailerDemoFlags.AutoRevealRange, 1f);
    }

    internal static void AddToBlockArray(
      SpawnBlockComponent spawnblock,
      int XL,
      int YL,
      bool IsFloor,
      bool IsUnderFloor)
    {
      if (XL == 164)
        ;
      if (SpawnBlockArray.spawnblocks == null)
      {
        SpawnBlockArray.UnderFloorspawnblocks = new SpawnBlockComponent[TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize()];
        SpawnBlockArray.Floorspawnblocks = new SpawnBlockComponent[TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize()];
        SpawnBlockArray.spawnblocks = new SpawnBlockComponent[TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize()];
      }
      if (IsUnderFloor)
        SpawnBlockArray.UnderFloorspawnblocks[XL, YL] = spawnblock;
      else if (IsFloor)
      {
        SpawnBlockArray.Floorspawnblocks[XL, YL] = spawnblock;
      }
      else
      {
        if (XL == 166)
          ;
        SpawnBlockArray.spawnblocks[XL, YL] = spawnblock;
      }
    }

    internal static void UpdateSpawnBlockArray(Player player, bool ForceRing = false)
    {
      if (!(player.inputmap.PressedThisFrame[12] | ForceRing))
        return;
      SpawnBlockArray.DoingBigRing = true;
      SpawnBlockArray.BigRingMult = 0.5f;
      SpawnBlockArray.DoRing(TrailerDemoFlags.ManualRevealRange, TrailerDemoFlags.ManualRevealWaveDelay);
    }

    private static void DoRing(int Range, float Revealdelay)
    {
      for (int index1 = -Range; index1 < Range; ++index1)
      {
        for (int index2 = -Range; index2 < Range; ++index2)
        {
          if (index1 + SpawnBlockArray.LastSetAvatarLocation.X > -1 && index2 + SpawnBlockArray.LastSetAvatarLocation.Y > -1 && (index1 + SpawnBlockArray.LastSetAvatarLocation.X < SpawnBlockArray.spawnblocks.GetLength(0) && index2 + SpawnBlockArray.LastSetAvatarLocation.Y < SpawnBlockArray.spawnblocks.GetLength(1)))
          {
            float num = new Vector2((float) index1, (float) index2).Length();
            if ((double) num < (double) Range)
            {
              float Delay = (num - (float) TrailerDemoFlags.AutoRevealRange) / (float) (Range - TrailerDemoFlags.AutoRevealRange) * Revealdelay;
              SpawnBlockArray.TryToUnblock(index1 + SpawnBlockArray.LastSetAvatarLocation.X, index2 + SpawnBlockArray.LastSetAvatarLocation.Y, Delay);
            }
          }
        }
      }
    }

    internal static void TryToUnblock(int Xlox, int YLoc, float Delay = 0.0f)
    {
      if (Xlox == 164)
        ;
      if (SpawnBlockArray.spawnblocks == null || Xlox <= -1 || (YLoc <= -1 || Xlox >= SpawnBlockArray.spawnblocks.GetLength(0)) || YLoc >= SpawnBlockArray.spawnblocks.GetLength(1))
        return;
      if (SpawnBlockArray.spawnblocks[Xlox, YLoc] != null)
      {
        int tiletypeonconstruct = (int) SpawnBlockArray.spawnblocks[Xlox, YLoc].tiletypeonconstruct;
        SpawnBlockArray.spawnblocks[Xlox, YLoc].Unblock(Delay);
      }
      if (SpawnBlockArray.UnderFloorspawnblocks[Xlox, YLoc] != null)
        SpawnBlockArray.UnderFloorspawnblocks[Xlox, YLoc].Unblock(Delay);
      if (SpawnBlockArray.Floorspawnblocks[Xlox, YLoc] == null)
        return;
      SpawnBlockArray.Floorspawnblocks[Xlox, YLoc].Unblock(Delay);
    }
  }
}
