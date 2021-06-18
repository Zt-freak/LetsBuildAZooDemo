// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.PeopleAndBeams.AnimalsInPens
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items;
using TinyZoo.PlayerDir.Layout.HoldingCells;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_AnimalsAndPeople.PenNav;
using TinyZoo.Z_Fights;

namespace TinyZoo.OverWorld.OverWorldEnv.PeopleAndBeams
{
  internal class AnimalsInPens
  {
    private List<CellBlockSet> cellblocks;
    internal static int AnimalUID = -1;
    internal static bool MouseIsOverAnimal = false;

    public AnimalsInPens(Player player)
    {
      this.cellblocks = new List<CellBlockSet>();
      for (int index = 0; index < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index)
        this.cellblocks.Add(new CellBlockSet(player.prisonlayout.cellblockcontainer.prisonzones[index], player));
      for (int index = 0; index < player.prisonlayout.cellblockcontainer.holdingcells.Count; ++index)
        this.cellblocks.Add(new CellBlockSet(player.prisonlayout.cellblockcontainer.holdingcells[index]));
    }

    public void MovePen_A_DeleteAllDynamicObjects(
      int Cell_UID,
      Player player,
      PrisonZone prisonzone_ForMove)
    {
      for (int index = 0; index < this.cellblocks.Count; ++index)
      {
        if (this.cellblocks[index].CELLUID == prisonzone_ForMove.Cell_UID)
          this.cellblocks[index].MovePenA_DeleteAll(prisonzone_ForMove, player);
      }
    }

    public void MovePen(
      int Cell_UID,
      Vector2Int MoveThisMuch,
      Player player,
      PrisonZone prisonzone_ForMove)
    {
      for (int index = 0; index < this.cellblocks.Count; ++index)
      {
        if (this.cellblocks[index].CELLUID == prisonzone_ForMove.Cell_UID)
          this.cellblocks[index].MovePen(prisonzone_ForMove, MoveThisMuch, player);
      }
    }

    public void EnrichedAnimalMovedOrDeleted(AnimalRenderMan animal, int CellUID)
    {
      for (int index = 0; index < this.cellblocks.Count; ++index)
      {
        if (this.cellblocks[index].CELLUID == CellUID)
          this.cellblocks[index].EnrichedAnimalMovedOrDeleted(animal);
      }
    }

    public void AddDynamicItemToCellBlock(
      TileRenderer tilerenderer,
      int Cell_UID,
      ZooBuildingTopRenderer toprenderer,
      ENRICHMENTBEHAVIOUR enrchmentbehaviour,
      PenItem penitem)
    {
      for (int index = 0; index < this.cellblocks.Count; ++index)
      {
        if (this.cellblocks[index].CELLUID == Cell_UID)
          this.cellblocks[index].AddDynamicItemToCellBlock(tilerenderer, toprenderer, enrchmentbehaviour, penitem);
      }
    }

    public void RemoveDynamicItemFromCellBlock(PenItem penitem, int PenUID, Player player)
    {
      for (int index = 0; index < this.cellblocks.Count; ++index)
      {
        if (this.cellblocks[index].CELLUID == PenUID)
          this.cellblocks[index].RemoveDynamicItemFromCellBlock(penitem, player, PenUID);
      }
    }

    public AnimalRenderMan GetAnimalRendererByUID(
      int AnimalUID,
      int CellblockUID,
      out int PenID)
    {
      PenID = -1;
      for (int index = 0; index < this.cellblocks.Count; ++index)
      {
        if (CellblockUID == -1 || CellblockUID == this.cellblocks[index].CELLUID)
        {
          AnimalRenderMan characterByUid = this.cellblocks[index].GetCharacterByUID(AnimalUID);
          if (characterByUid != null)
          {
            PenID = this.cellblocks[index].CELLUID;
            return characterByUid;
          }
        }
      }
      return (AnimalRenderMan) null;
    }

    public AnimalRenderMan GetCharacterByUID(int UID)
    {
      for (int index = 0; index < this.cellblocks.Count; ++index)
      {
        if (this.cellblocks[index].GetCharacterByUID(UID) != null)
          return this.cellblocks[index].GetCharacterByUID(UID);
      }
      return (AnimalRenderMan) null;
    }

    public void AddHoldingCellOnTheFly(Player player, Vector2Int Location)
    {
      HoldingCellInfo thisHoldingCell = player.prisonlayout.GetThisHoldingCell(Location);
      bool flag = true;
      for (int index = 0; index < this.cellblocks.Count; ++index)
      {
        if (this.cellblocks[index].holdingcell == thisHoldingCell)
          flag = false;
      }
      if (!flag)
        return;
      this.cellblocks.Add(new CellBlockSet(thisHoldingCell));
    }

    public void AddCellBlockOnTheFly(Player player)
    {
      for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < this.cellblocks.Count; ++index2)
        {
          if (this.cellblocks[index2].CELLUID == player.prisonlayout.cellblockcontainer.prisonzones[index1].Cell_UID)
            flag = true;
        }
        if (!flag)
          this.cellblocks.Add(new CellBlockSet(player.prisonlayout.cellblockcontainer.prisonzones[index1], player));
      }
    }

    public AnimalRenderMan CheckPeopleForCollisions(
      Vector2 LocationInWorldSpace,
      bool SetMouseOver)
    {
      List<AnimalRenderMan> animalRenderManList1 = new List<AnimalRenderMan>();
      for (int index1 = 0; index1 < this.cellblocks.Count; ++index1)
      {
        List<AnimalRenderMan> animalRenderManList2 = this.cellblocks[index1].CheckPeopleForCollisions(LocationInWorldSpace);
        if (animalRenderManList2.Count > 0)
        {
          for (int index2 = 0; index2 < animalRenderManList2.Count; ++index2)
            animalRenderManList1.Add(animalRenderManList2[index2]);
        }
      }
      if (animalRenderManList1.Count <= 0)
        return (AnimalRenderMan) null;
      if (animalRenderManList1.Count == 1)
      {
        if (SetMouseOver)
        {
          AnimalsInPens.AnimalUID = animalRenderManList1[0].refperson.UID;
          AnimalsInPens.MouseIsOverAnimal = true;
        }
        return animalRenderManList1[0];
      }
      int index3 = -1;
      float num = -1f;
      for (int index1 = 0; index1 < animalRenderManList1.Count; ++index1)
      {
        if ((double) num == -1.0 || (double) (animalRenderManList1[index1].enemyrenderere.vLocation - LocationInWorldSpace).Length() < (double) num)
        {
          index3 = index1;
          num = (animalRenderManList1[index1].enemyrenderere.vLocation - LocationInWorldSpace).Length();
        }
      }
      if (SetMouseOver)
      {
        AnimalsInPens.AnimalUID = animalRenderManList1[index3].refperson.UID;
        AnimalsInPens.MouseIsOverAnimal = true;
      }
      return animalRenderManList1[index3];
    }

    public void DeletePeopleAfterSellingPrison(int CellUID)
    {
      for (int index = this.cellblocks.Count - 1; index > -1; --index)
      {
        if (this.cellblocks[index].CELLUID == CellUID)
        {
          this.cellblocks[index].REF_prisonzone.prisonercontainer.RemoveFromOverworldRendering();
          StaticPenNavPool.RemoveThisPen(CellUID);
          this.cellblocks.RemoveAt(index);
        }
      }
    }

    public void AddFight(FightManager thisisthefight, int CellBlockUID)
    {
      for (int index = 0; index < this.cellblocks.Count; ++index)
      {
        if (this.cellblocks[index].CELLUID == CellBlockUID)
          this.cellblocks[index].AddFightManager(thisisthefight);
      }
    }

    public void StartBreakOut(
      int CellBlockUID,
      Vector2Int GateLocation,
      Vector2Int SpaceBehindGate)
    {
      for (int index = 0; index < this.cellblocks.Count; ++index)
      {
        if (this.cellblocks[index].CELLUID == CellBlockUID)
          this.cellblocks[index].StartBreakOut(GateLocation, SpaceBehindGate);
      }
    }

    public void UpdatePeopleAndBeamsManager(float DeltaTime)
    {
      if (GameFlags.CellBlockContentsChanged || GameFlags.prisonersJustChangedInHoldingCell)
      {
        for (int index = 0; index < this.cellblocks.Count; ++index)
        {
          if (this.cellblocks[index].REF_prisonzone != null && this.cellblocks[index].REF_prisonzone.prisonercontainer.ThisWasTehCellBlockThatChanged)
          {
            this.cellblocks[index].REF_prisonzone.prisonercontainer.ThisWasTehCellBlockThatChanged = false;
            this.cellblocks[index].VallidateAnimals();
          }
          else if (this.cellblocks[index].holdingcell != null && this.cellblocks[index].holdingcell.prisonercontainer.ThisWasTehCellBlockThatChanged)
          {
            this.cellblocks[index].holdingcell.prisonercontainer.ThisWasTehCellBlockThatChanged = false;
            this.cellblocks[index].VallidateAnimals();
          }
        }
        GameFlags.prisonersJustChangedInHoldingCell = false;
        GameFlags.CellBlockContentsChanged = false;
      }
      if (Z_GameFlags.CheckDeaths)
      {
        Z_GameFlags.CheckDeaths = false;
        for (int index = 0; index < this.cellblocks.Count; ++index)
          this.cellblocks[index].CheckDeaths();
      }
      for (int index = 0; index < this.cellblocks.Count; ++index)
        this.cellblocks[index].UpdateCellBlockSet(DeltaTime);
    }

    public void StartNewDay()
    {
      for (int index = 0; index < this.cellblocks.Count; ++index)
        this.cellblocks[index].StartNewDay();
    }

    public CellBlockSet GetCellBlockByUID(int CellUID)
    {
      for (int index = 0; index < this.cellblocks.Count; ++index)
      {
        if (this.cellblocks[index].CELLUID == CellUID)
          return this.cellblocks[index];
      }
      return (CellBlockSet) null;
    }

    public Enemy GetRandomPerson() => this.cellblocks.Count < 1 ? (Enemy) null : this.cellblocks[TinyZoo.Game1.Rnd.Next(0, this.cellblocks.Count)].GetRandomPerson();

    public void DRawPeopleAndBeamsManager()
    {
      for (int index = 0; index < this.cellblocks.Count; ++index)
        this.cellblocks[index].DrawCellBlockSet();
    }
  }
}
