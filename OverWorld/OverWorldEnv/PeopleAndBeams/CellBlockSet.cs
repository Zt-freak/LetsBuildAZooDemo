// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.PeopleAndBeams.CellBlockSet
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GamePlay.ReclaimedZones;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items;
using TinyZoo.PlayerDir.Layout.HoldingCells;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras;
using TinyZoo.Z_Fights;
using TinyZoo.Z_OverWorld._OverWorldEnv.PenDecoEnrich;

namespace TinyZoo.OverWorld.OverWorldEnv.PeopleAndBeams
{
  internal class CellBlockSet
  {
    private BoxZoneManager boxzonemanager;
    private EnemyManager enemyrenderer;
    public HoldingCellInfo holdingcell;
    public int CELLUID;
    public PrisonZone REF_prisonzone;
    private PenDecoAndEnrichmentManager penDecoAndEnrichment;

    public CellBlockSet(PrisonZone prisonzone, Player player)
    {
      this.penDecoAndEnrichment = new PenDecoAndEnrichmentManager(prisonzone, player);
      this.REF_prisonzone = prisonzone;
      this.CELLUID = prisonzone.Cell_UID;
      prisonzone.SetMapLimits();
      this.boxzonemanager = new BoxZoneManager();
      this.enemyrenderer = prisonzone.GetEnemyRenderer(this.boxzonemanager);
    }

    public AnimalRenderMan GetCharacterByUID(int UID) => this.enemyrenderer.GetCharacterByUID(UID);

    public CellBlockSet(HoldingCellInfo _holdingcell)
    {
      this.holdingcell = _holdingcell;
      this.holdingcell.SetMapLimits();
      this.boxzonemanager = new BoxZoneManager();
      this.enemyrenderer = this.holdingcell.GetEnemyRenderer(this.boxzonemanager);
    }

    public void MovePenA_DeleteAll(PrisonZone REF_prisonzone, Player player) => this.enemyrenderer.DeleteAllDynamicItemsInThisen(REF_prisonzone, player);

    public void MovePen(PrisonZone REF_prisonzone, Vector2Int MoveThisMuch, Player player) => REF_prisonzone.penItems.ReaddPenItemsOnMapBuild(REF_prisonzone.Cell_UID, OverWorldManager.overworldenvironment.animalsinpens);

    public void EnrichedAnimalMovedOrDeleted(AnimalRenderMan animal) => this.enemyrenderer.EnrichedAnimalMovedOrDeleted(animal);

    public void AddDynamicItemToCellBlock(
      TileRenderer tilerenderer,
      ZooBuildingTopRenderer toprenderer,
      ENRICHMENTBEHAVIOUR enrchmentbehaviour,
      PenItem penitem)
    {
      this.enemyrenderer.AddDynamicItemToCellBlock(tilerenderer, this.REF_prisonzone, toprenderer, enrchmentbehaviour, penitem);
    }

    public void RemoveDynamicItemFromCellBlock(PenItem penitem, Player player, int PenUID) => this.enemyrenderer.RemoveDynamicItemFromCellBlock(penitem, player, PenUID);

    public void AddFightManager(FightManager thisisthefight) => this.enemyrenderer.AddFightManager(thisisthefight);

    public List<AnimalRenderMan> CheckPeopleForCollisions(
      Vector2 LocationInWorldSpace)
    {
      return this.enemyrenderer.CheckPeopleForCollisions(LocationInWorldSpace);
    }

    public void StartBreakOut(Vector2Int GateLocation, Vector2Int SpaceBehindGate) => this.enemyrenderer.StartBreakOut(GateLocation, SpaceBehindGate, this.CELLUID);

    public void VallidateAnimals()
    {
      if (this.REF_prisonzone != null)
        this.enemyrenderer.VallidatAnimals(this.REF_prisonzone, this.boxzonemanager);
      else if (this.holdingcell != null)
        throw new Exception("NO HOLDING CELLS IN GAME");
    }

    public void CheckDeaths() => this.enemyrenderer.CheckDeaths();

    public void StartNewDay() => this.enemyrenderer.StartNewDay();

    public void UpdateCellBlockSet(float DeltaTime)
    {
      if (TrailerDemoFlags.HasTrailerFlag)
        DeltaTime *= 3f;
      this.penDecoAndEnrichment.UpdatePenDecoAndEnrichmentManager(DeltaTime);
      this.enemyrenderer.UpdateEnemyManager(DeltaTime, this.boxzonemanager);
      this.boxzonemanager.UpdateBoxZoneManager(DeltaTime);
    }

    public Enemy GetRandomPerson() => this.enemyrenderer.GetRandomPerson();

    public void Feed(UsedFoodCollection foodused) => this.penDecoAndEnrichment.Feed(foodused);

    public void DrawCellBlockSet()
    {
      this.penDecoAndEnrichment.DrawPenDecoAndEnrichmentManager();
      this.boxzonemanager.DrawBoxZoneManager();
    }
  }
}
