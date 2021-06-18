// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment.DynamicEnrichmentItem
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_AnimalsAndPeople.PenNav;
using TinyZoo.Z_AnimalsAndPeople.PenNav.CurrentPens;
using TinyZoo.Z_DayNight;

namespace TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment
{
  internal class DynamicEnrichmentItem
  {
    public WalkingPerson walkingPerson_RefFromCustomerManager;
    private PenNavigator pennavigation;
    private AnimatedGameObject EnrichmentObject;
    private Texture2D drawWIthThis;
    private Vector2Int StartLocation;
    private ENRICHMENTBEHAVIOUR enrchmentbehaviour;
    private AttachItemToAnimal attachemnttoanimal;
    private TrampolineManager trampolinemanager;
    private EnrichmentPerch enrichmentperch;
    private Vector2Int CurrentTileInWorldSpace;
    private List<Vector2Int> AccessPoints;
    public int UID;
    public PenItem Ref_PenItem;
    private int YOrigin;
    private int XOrigin;
    private int XWidth;
    private int YHeight;
    public bool SelectedInEditMenu;

    public DynamicEnrichmentItem(
      TileRenderer tilerenderer,
      ZooBuildingTopRenderer toprenderer,
      ENRICHMENTBEHAVIOUR _enrchmentbehaviour,
      PrisonZone prisonzone,
      PenItem penitem)
    {
      this.SelectedInEditMenu = false;
      this.Ref_PenItem = penitem;
      this.enrchmentbehaviour = _enrchmentbehaviour;
      if (_enrchmentbehaviour == ENRICHMENTBEHAVIOUR.Trampoline || this.enrchmentbehaviour == ENRICHMENTBEHAVIOUR.Perch)
      {
        this.YOrigin = tilerenderer.YOrigin;
        this.XOrigin = tilerenderer.XOrigin;
        this.XWidth = tilerenderer.XWidth;
        this.YHeight = tilerenderer.YHeight;
        this.BlockOrUnblockCollision(tilerenderer.TileLocation, prisonzone.FloorTiles, prisonzone.Cell_UID);
      }
      this.UID = Z_GameFlags.GetEnrichmentUID();
      if (toprenderer != null)
      {
        this.EnrichmentObject = new AnimatedGameObject((GameObject) toprenderer);
        this.drawWIthThis = toprenderer.DrawWIthThis;
      }
      else
      {
        this.EnrichmentObject = new AnimatedGameObject((GameObject) tilerenderer);
        this.drawWIthThis = tilerenderer.drawWIthThis;
      }
      this.StartLocation = new Vector2Int(tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y);
      this.EnrichmentObject.vLocation = TileMath.GetTileToWorldSpace(this.StartLocation);
      this.EnrichmentObject.scale = 1f;
      this.EnrichmentObject.SetAlpha(1f);
      float num = (float) this.EnrichmentObject.DrawRect.Height - this.EnrichmentObject.DrawOrigin.Y - 2f;
      this.EnrichmentObject.DrawOrigin.Y += num;
      this.EnrichmentObject.vLocation.Y += num * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.CurrentTileInWorldSpace = new Vector2Int(tilerenderer.TileLocation);
      if (this.enrchmentbehaviour == ENRICHMENTBEHAVIOUR.AnimalAttachment)
        this.attachemnttoanimal = new AttachItemToAnimal(penitem.tiletype);
      else if (this.enrchmentbehaviour == ENRICHMENTBEHAVIOUR.Trampoline)
      {
        this.trampolinemanager = new TrampolineManager(tilerenderer.TileLocation, tilerenderer);
      }
      else
      {
        if (this.enrchmentbehaviour != ENRICHMENTBEHAVIOUR.Perch)
          return;
        this.enrichmentperch = new EnrichmentPerch(tilerenderer);
      }
    }

    public void ForceDetachAnimal(AnimalRenderMan animal)
    {
      if (this.enrchmentbehaviour == ENRICHMENTBEHAVIOUR.AnimalAttachment)
        return;
      if (this.enrchmentbehaviour == ENRICHMENTBEHAVIOUR.Trampoline)
      {
        this.trampolinemanager.ForceDetach(animal, (GameObject) this.EnrichmentObject);
      }
      else
      {
        if (this.enrchmentbehaviour != ENRICHMENTBEHAVIOUR.Perch)
          return;
        this.enrichmentperch.ForceDetach(animal);
      }
    }

    public bool TryAndDestroy(Player player, int PenUID)
    {
      bool flag = false;
      switch (this.enrchmentbehaviour)
      {
        case ENRICHMENTBEHAVIOUR.Trampoline:
          flag = true;
          this.trampolinemanager.ForceDetatch();
          break;
        case ENRICHMENTBEHAVIOUR.Perch:
          flag = true;
          this.enrichmentperch.ForceDetatch();
          break;
      }
      if (flag)
        this.BlockOrUnblockCollision(this.CurrentTileInWorldSpace, player.prisonlayout.GetThisCellBlock(PenUID).FloorTiles, PenUID, true);
      return true;
    }

    public void SetWalkingPerson(WalkingPerson _walkingPerson) => this.walkingPerson_RefFromCustomerManager = _walkingPerson;

    public void SetForIrregularEnclosure(List<Vector2Int> FloorLocations, int PenUID)
    {
      if (this.enrchmentbehaviour == ENRICHMENTBEHAVIOUR.Trampoline)
        return;
      this.pennavigation = new PenNavigator(FloorLocations, PenUID, AnimalType.None);
      if (this.enrchmentbehaviour == ENRICHMENTBEHAVIOUR.Ball || this.enrchmentbehaviour == ENRICHMENTBEHAVIOUR.AnimalAttachment || this.enrchmentbehaviour == ENRICHMENTBEHAVIOUR.Perch)
      {
        this.pennavigation.StartNavigating(ref this.EnrichmentObject.vLocation, this.EnrichmentObject.DrawRect, _navigationtype: NavigationType.RollingBall);
        this.CurrentTileInWorldSpace = new Vector2Int(this.pennavigation.CurrentWorldSpaceLocation);
      }
      else
      {
        this.pennavigation.StartNavigating(ref this.EnrichmentObject.vLocation, this.EnrichmentObject.DrawRect);
        this.CurrentTileInWorldSpace = new Vector2Int(this.pennavigation.CurrentWorldSpaceLocation);
        this.attachemnttoanimal.SetWorldSpaceLocation(this.CurrentTileInWorldSpace);
      }
    }

    private void BlockOrUnblockCollision(
      Vector2Int _TR_TileLocation,
      List<Vector2Int> FloorTiles,
      int Cell_UID,
      bool IsUnblock = false)
    {
      PenNavCollection thisPenNav = StaticPenNavPool.GetThisPenNav(FloorTiles, Cell_UID);
      this.AccessPoints = new List<Vector2Int>();
      for (int index1 = _TR_TileLocation.X - this.XOrigin; index1 < _TR_TileLocation.X - this.XOrigin + this.XWidth; ++index1)
      {
        for (int index2 = _TR_TileLocation.Y - this.YOrigin; index2 < _TR_TileLocation.Y - this.YOrigin + this.YHeight; ++index2)
        {
          if (index1 == _TR_TileLocation.X - this.XOrigin && !IsUnblock && thisPenNav.HasThisFloorTile(index1 - 1, index2))
            this.AccessPoints.Add(new Vector2Int(index1 - 1, index2));
          if (index1 == _TR_TileLocation.X - this.XOrigin + this.XWidth - 1 && !IsUnblock && thisPenNav.HasThisFloorTile(index1 + 1, index2))
            this.AccessPoints.Add(new Vector2Int(index1 + 1, index2));
          if (index2 == _TR_TileLocation.Y - this.YOrigin && !IsUnblock && thisPenNav.HasThisFloorTile(index1, index2 - 1))
            this.AccessPoints.Add(new Vector2Int(index1, index2 - 1));
          if (index2 == _TR_TileLocation.Y - this.YOrigin + this.YHeight - 1 && !IsUnblock && thisPenNav.HasThisFloorTile(index1, index2 + 1))
            this.AccessPoints.Add(new Vector2Int(index1, index2 + 1));
          if (IsUnblock)
          {
            thisPenNav.UnblockThisTile(index1, index2);
            Z_GameFlags.pathfinder.UnblockTile(index1, index2, true);
          }
          else
          {
            thisPenNav.BlockThisTile(index1, index2);
            Z_GameFlags.pathfinder.BlockTile(index1, index2, true);
          }
        }
      }
    }

    public void UpdateEnrichmentItem(
      float DeltaTime,
      Vector2Int CurrentWorldSpaceLocationOfAnAnimal,
      AnimalRenderMan animalrenderer_Untrustworthy)
    {
      switch (this.enrchmentbehaviour)
      {
        case ENRICHMENTBEHAVIOUR.Ball:
          if (CurrentWorldSpaceLocationOfAnAnimal != null && CurrentWorldSpaceLocationOfAnAnimal.CompareMatches(this.pennavigation.CurrentWorldSpaceLocation))
          {
            this.pennavigation.frictionhandler.KnockBalls();
            break;
          }
          break;
        case ENRICHMENTBEHAVIOUR.AnimalAttachment:
          if (CurrentWorldSpaceLocationOfAnAnimal != null && !animalrenderer_Untrustworthy.IsBeingEnriched && (double) animalrenderer_Untrustworthy.EnrchmentDelay <= 0.0)
          {
            if (!this.attachemnttoanimal.GetCanAttach())
            {
              this.attachemnttoanimal.UpdateAttachItemToAnimal(DeltaTime, ref this.EnrichmentObject, this.pennavigation);
              break;
            }
            if (!animalrenderer_Untrustworthy.HoldingToy)
            {
              if (CurrentWorldSpaceLocationOfAnAnimal.CompareMatches(this.pennavigation.CurrentWorldSpaceLocation))
              {
                if (this.UID != animalrenderer_Untrustworthy.LastInteractionPoint_UID)
                {
                  this.attachemnttoanimal.AttachToThisAnimal(animalrenderer_Untrustworthy, this.UID);
                  break;
                }
                break;
              }
              if (animalrenderer_Untrustworthy.LastInteractionPoint_UID == this.UID)
              {
                animalrenderer_Untrustworthy.LastInteractionPoint_UID = -1;
                break;
              }
              break;
            }
            break;
          }
          break;
        case ENRICHMENTBEHAVIOUR.Trampoline:
          if (CurrentWorldSpaceLocationOfAnAnimal != null && animalrenderer_Untrustworthy != null && (!animalrenderer_Untrustworthy.IsBeingEnriched && (double) animalrenderer_Untrustworthy.EnrchmentDelay <= 0.0))
          {
            bool flag = false;
            for (int index = 0; index < this.AccessPoints.Count; ++index)
            {
              if (CurrentWorldSpaceLocationOfAnAnimal.CompareMatches(this.AccessPoints[index]))
                flag = true;
            }
            if (flag)
            {
              if (this.UID != animalrenderer_Untrustworthy.LastInteractionPoint_UID)
                this.trampolinemanager.AttachAnimalToTrampoline(animalrenderer_Untrustworthy, this.UID);
            }
            else if (animalrenderer_Untrustworthy.LastInteractionPoint_UID == this.UID)
              animalrenderer_Untrustworthy.LastInteractionPoint_UID = -1;
          }
          this.trampolinemanager.UpdateTrampolineManager(DeltaTime, (GameObject) this.EnrichmentObject);
          break;
        case ENRICHMENTBEHAVIOUR.Perch:
          this.enrichmentperch.UpdateEnrichmentPerch(DeltaTime, this.EnrichmentObject);
          if (!animalrenderer_Untrustworthy.IsBeingEnriched && (double) animalrenderer_Untrustworthy.EnrchmentDelay <= 0.0 && (CurrentWorldSpaceLocationOfAnAnimal != null && animalrenderer_Untrustworthy != null))
          {
            bool flag = false;
            for (int index = 0; index < this.AccessPoints.Count; ++index)
            {
              if (CurrentWorldSpaceLocationOfAnAnimal.CompareMatches(this.AccessPoints[index]))
                flag = true;
            }
            if (flag)
            {
              if (animalrenderer_Untrustworthy.LastInteractionPoint_UID != this.UID && !animalrenderer_Untrustworthy.IsBeingEnriched && this.enrichmentperch.GetCanPerch())
              {
                this.enrichmentperch.AttachAnimalToPerch(animalrenderer_Untrustworthy, this.UID, this.EnrichmentObject);
                break;
              }
              break;
            }
            if (animalrenderer_Untrustworthy.LastInteractionPoint_UID == this.UID)
            {
              animalrenderer_Untrustworthy.LastInteractionPoint_UID = -1;
              break;
            }
            break;
          }
          break;
      }
      if (this.pennavigation != null)
        this.pennavigation.UpdatePenNavigator(ref this.EnrichmentObject.vLocation, DeltaTime, (EnemyRenderer) null);
      this.walkingPerson_RefFromCustomerManager.vLocation = this.EnrichmentObject.vLocation;
    }

    public void DrawEnrichmentItem()
    {
      this.EnrichmentObject.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
      if (this.enrchmentbehaviour == ENRICHMENTBEHAVIOUR.Trampoline)
        this.trampolinemanager.DrawTrampolineManager(true);
      else if (this.enrchmentbehaviour == ENRICHMENTBEHAVIOUR.Perch)
        this.enrichmentperch.DrawEnrichmentPerch(true);
      if (this.enrchmentbehaviour == ENRICHMENTBEHAVIOUR.AnimalAttachment)
        this.attachemnttoanimal.DrawAttachmentToAnimal(this.EnrichmentObject, this.drawWIthThis);
      if (this.enrchmentbehaviour != ENRICHMENTBEHAVIOUR.AnimalAttachment)
        this.EnrichmentObject.WorldOffsetDraw(AssetContainer.pointspritebatch01, this.drawWIthThis);
      if (this.Ref_PenItem.IsSelectedInEditView)
      {
        this.Ref_PenItem.IsSelectedInEditView = false;
        this.EnrichmentObject.scale = 1f;
        this.EnrichmentObject.WorldOffsetDraw(AssetContainer.PointBlendBatch02, this.drawWIthThis);
        this.EnrichmentObject.scale = 1f;
      }
      if (this.enrchmentbehaviour == ENRICHMENTBEHAVIOUR.Trampoline)
      {
        this.trampolinemanager.DrawTrampolineManager(false);
      }
      else
      {
        if (this.enrchmentbehaviour != ENRICHMENTBEHAVIOUR.Perch)
          return;
        this.enrichmentperch.DrawEnrichmentPerch(false);
      }
    }
  }
}
