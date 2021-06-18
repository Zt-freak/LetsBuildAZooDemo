// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TrashSystem.Z_TrashManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Employees.WorkZonePane;

namespace TinyZoo.Z_TrashSystem
{
  internal class Z_TrashManager
  {
    private static List<TrashDrop> Trash;

    public Z_TrashManager(Player player)
    {
      Z_TrashManager.Trash = new List<TrashDrop>();
      for (int index = 0; index < player.prisonlayout.trashandstuff.trashentries.Count; ++index)
      {
        if (player.prisonlayout.trashandstuff.trashentries[index].trashtype == TrashType.AnimalPoop)
        {
          Z_TrashManager.Trash.Add(new TrashDrop(player.prisonlayout.trashandstuff.trashentries[index].TileLocation, player.prisonlayout.trashandstuff.trashentries[index].trashtype, _PrisonUID: player.prisonlayout.trashandstuff.trashentries[index].PrisonUID));
          Z_GameFlags.Location_Directory.AddPoop(Z_TrashManager.Trash[Z_TrashManager.Trash.Count - 1], player.prisonlayout.trashandstuff.trashentries[index].PrisonUID);
        }
        else
        {
          Z_TrashManager.Trash.Add(new TrashDrop(player.prisonlayout.trashandstuff.trashentries[index].TileLocation, player.prisonlayout.trashandstuff.trashentries[index].trashtype));
          Z_GameFlags.Location_Directory.AddTrash(Z_TrashManager.Trash[Z_TrashManager.Trash.Count - 1]);
        }
        Z_TrashManager.Trash[index].ISNew = false;
      }
    }

    internal static bool HasPoopHere(int PENUID) => Z_GameFlags.Location_Directory.HasPoopHere(PENUID);

    internal static bool TrashLevelNeedsMessage(ref bool BlockFutureChecksToday)
    {
      if (Z_TrashManager.Trash.Count > 50)
      {
        if (TinyZoo.Game1.Rnd.Next(0, 2) == 0)
          return true;
      }
      else if (Z_TrashManager.Trash.Count > 30)
      {
        if (TinyZoo.Game1.Rnd.Next(0, 5) == 0)
          return true;
      }
      else if (Z_TrashManager.Trash.Count > 15)
      {
        if (TinyZoo.Game1.Rnd.Next(0, 10) == 0)
          return true;
      }
      else if (Z_TrashManager.Trash.Count > 9)
      {
        if (TinyZoo.Game1.Rnd.Next(0, 20) == 0)
          return true;
      }
      else
        BlockFutureChecksToday = false;
      return false;
    }

    public bool TryToPickUpPoop(Vector2Int Location, int PenUID)
    {
      for (int index = Z_TrashManager.Trash.Count - 1; index > -1; --index)
      {
        if (Z_TrashManager.Trash[index].bActive && Z_TrashManager.Trash[index].TileLocation.CompareMatches(Location))
        {
          Z_GameFlags.Location_Directory.RemovePoop(Z_TrashManager.Trash[index], PenUID);
          Z_TrashManager.Trash[index].ReadyForPickUp = true;
          return true;
        }
      }
      return false;
    }

    public bool TryToPickUpTrash(Vector2Int Location)
    {
      for (int index = Z_TrashManager.Trash.Count - 1; index > -1; --index)
      {
        if (Z_TrashManager.Trash[index].bActive && Z_TrashManager.Trash[index].TileLocation.CompareMatches(Location))
        {
          Z_GameFlags.Location_Directory.RemoveTrash(Z_TrashManager.Trash[index]);
          Z_TrashManager.Trash[index].ReadyForPickUp = true;
          return true;
        }
      }
      return false;
    }

    public int GetTotalTrash() => Z_TrashManager.Trash.Count;

    public bool IsTrashHere(Vector2Int location)
    {
      for (int index = 0; index < Z_TrashManager.Trash.Count; ++index)
      {
        if (Z_TrashManager.Trash[index].bActive && Z_TrashManager.Trash[index].TileLocation.CompareMatches(location))
          return true;
      }
      return false;
    }

    public Vector2Int GetTrashLocation(WorkZoneInfo workzoneinfo)
    {
      int maxValue = 0;
      List<int> intList = new List<int>();
      for (int index = 0; index < Z_TrashManager.Trash.Count; ++index)
      {
        if (Z_TrashManager.Trash[index].bActive)
        {
          if (workzoneinfo.workzones.Count > 0)
          {
            if (workzoneinfo.workzones[0].IsInWorkZone(Z_TrashManager.Trash[index].TileLocation))
              intList.Add(index);
          }
          else
            ++maxValue;
        }
      }
      if (workzoneinfo.workzones.Count > 0)
      {
        if (intList.Count > 0)
          return Z_TrashManager.Trash[intList[TinyZoo.Game1.Rnd.Next(0, intList.Count)]].TileLocation;
      }
      else if (maxValue > 0)
      {
        int num = TinyZoo.Game1.Rnd.Next(0, maxValue);
        for (int index = 0; index < Z_TrashManager.Trash.Count; ++index)
        {
          if (Z_TrashManager.Trash[index].bActive)
          {
            --num;
            if (num <= 0)
              return Z_TrashManager.Trash[index].TileLocation;
          }
        }
      }
      return (Vector2Int) null;
    }

    internal static void DropTrash(Vector2Int TrashLocation, TrashType dropthis)
    {
      Z_TrashManager.Trash.Add(new TrashDrop(TrashLocation, dropthis));
      Z_GameFlags.Location_Directory.AddTrash(Z_TrashManager.Trash[Z_TrashManager.Trash.Count - 1]);
      Z_TrashManager.Trash.Sort(new Comparison<TrashDrop>(TrashDrop.SortTrash));
    }

    internal static void MakePoop(Vector2Int TrashLocation, AnimalType animal, int PrisonUID)
    {
      Z_TrashManager.Trash.Add(new TrashDrop(TrashLocation, TrashType.AnimalPoop, animal, PrisonUID));
      Z_GameFlags.Location_Directory.AddPoop(Z_TrashManager.Trash[Z_TrashManager.Trash.Count - 1], PrisonUID);
      Z_TrashManager.Trash.Sort(new Comparison<TrashDrop>(TrashDrop.SortTrash));
    }

    public void UpdateZ_TrashManager(float DeltaTime, Player player)
    {
      if (GameFlags.CollisionChanged)
      {
        for (int index = Z_TrashManager.Trash.Count - 1; index > -1; --index)
          Z_TrashManager.Trash[index].UpdateBlocked(player.prisonlayout.trashandstuff);
      }
      for (int index = Z_TrashManager.Trash.Count - 1; index > -1; --index)
      {
        Z_TrashManager.Trash[index].UpdateTrashDrop(DeltaTime);
        if (Z_TrashManager.Trash[index].ISNew)
        {
          player.prisonlayout.trashandstuff.AddTrash(Z_TrashManager.Trash[index], player);
          Z_TrashManager.Trash[index].ISNew = false;
        }
        if (Z_TrashManager.Trash[index].ReadyForPickUp)
        {
          player.prisonlayout.trashandstuff.RemoveTrash(Z_TrashManager.Trash[index], player);
          Z_TrashManager.Trash.RemoveAt(index);
        }
      }
    }

    public void DrawZ_TrashManager()
    {
      for (int index = 0; index < Z_TrashManager.Trash.Count; ++index)
        Z_TrashManager.Trash[index].DrawTrashDrop();
    }
  }
}
