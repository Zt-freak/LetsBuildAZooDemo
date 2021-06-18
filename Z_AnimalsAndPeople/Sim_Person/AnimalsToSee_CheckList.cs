// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.AnimalsToSee_CheckList
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.Z_BalanceSystems.CustomerStats;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class AnimalsToSee_CheckList
  {
    private List<int> CELLUIDsVisited;
    private List<int> CELLUIDsTOVISIT;
    public bool HasFavouriteAnimal;

    public AnimalsToSee_CheckList(
      CellblockMananger cellblockcontainer,
      AnimalType FavouriteAnimal,
      Player player)
    {
      this.CELLUIDsVisited = new List<int>();
      this.CELLUIDsTOVISIT = new List<int>();
      if (TrailerDemoFlags.HasTrailerFlag || cellblockcontainer == null)
        return;
      if (cellblockcontainer.prisonzones.Count > 1)
      {
        int maxValue = Math.Min(20, cellblockcontainer.prisonzones.Count);
        int num1 = Game1.Rnd.Next(1, maxValue);
        int prizoneZonePopularity = cellblockcontainer.TEMP_TotalPrizoneZonePopularity;
        while (num1 > 0)
        {
          int num2 = Game1.Rnd.Next(0, prizoneZonePopularity);
          for (int index = 0; index < cellblockcontainer.prisonzones.Count; ++index)
          {
            num2 -= cellblockcontainer.prisonzones[index].Temp_Popularity;
            if (!this.CELLUIDsTOVISIT.Contains(cellblockcontainer.prisonzones[index].Cell_UID) && num2 <= 0)
            {
              this.CELLUIDsTOVISIT.Add(cellblockcontainer.prisonzones[index].Cell_UID);
              --num1;
              break;
            }
          }
        }
        int uidWithThisAnimal = CalculateStat.GetCellUIDWithThisAnimal(player, FavouriteAnimal);
        if (uidWithThisAnimal <= -1)
          return;
        this.HasFavouriteAnimal = true;
        this.CELLUIDsTOVISIT.Add(uidWithThisAnimal);
      }
      else
      {
        if (cellblockcontainer.prisonzones.Count <= 0)
          return;
        this.CELLUIDsTOVISIT.Add(cellblockcontainer.prisonzones[0].Cell_UID);
      }
    }

    public bool CheckWantsToVisitAnimal() => this.CELLUIDsTOVISIT.Count > 0;

    public void ReachedAnimal(int CellUID)
    {
      this.CELLUIDsVisited.Add(CellUID);
      this.CELLUIDsTOVISIT.Remove(CellUID);
    }

    public int GetNextPenUID(out bool NothingToSee)
    {
      NothingToSee = false;
      if (this.CELLUIDsTOVISIT.Count > 0)
        return this.CELLUIDsTOVISIT[0];
      NothingToSee = true;
      return -1;
    }

    public void RemoveAnimal(int CellUID) => this.CELLUIDsTOVISIT.Remove(CellUID);
  }
}
