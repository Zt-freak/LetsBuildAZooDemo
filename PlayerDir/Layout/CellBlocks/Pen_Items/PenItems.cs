// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items.PenItems
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.PeopleAndBeams;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_AnimalsAndPeople.PenNav;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;
using TinyZoo.Z_Notification;

namespace TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items
{
  internal class PenItems
  {
    public List<PenItem> items;
    private List<PenItemDuplicateInfo> duplicateinfo;
    public int MaximumWaterUnits;
    public int SuppliedWaterUnits;
    private ENRICHMENTBEHAVIOUR enrchmentbehaviour;

    public PenItems()
    {
      this.duplicateinfo = new List<PenItemDuplicateInfo>();
      this.items = new List<PenItem>();
    }

    public bool HasWaterTrough()
    {
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (TileData.IsThisAWaterTrough(this.items[index].tiletype))
          return true;
      }
      return false;
    }

    public int GetWaterSize(Player player)
    {
      int num = 0;
      bool flag = false;
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (TileData.IsThisAWaterTrough(this.items[index].tiletype))
        {
          if (OverWorldManager.heatmapmanager.GetHasWaterAccess(this.items[index].Location.X, this.items[index].Location.Y))
            num += this.items[index].WaterUnits;
          flag = true;
        }
      }
      if (flag && num == 0 && !Z_NotificationManager.HasThisNotification(Z_NotificationType.A_NoWaterConnection))
        Z_NotificationManager.AddNotificationPackage(new NotificationPackage(Z_NotificationType.A_NoWaterConnection)
        {
          AlertStatus = NotificationAlertStatus.Danger_Worst
        }, player);
      return num;
    }

    public bool HasEnrichment()
    {
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (!TileData.IsThisAWaterTrough(this.items[index].tiletype))
          return true;
      }
      return false;
    }

    public void ReaddPenItemsOnMapBuild(int CellUID, AnimalsInPens animalsinpens)
    {
      for (int index = 0; index < this.items.Count; ++index)
      {
        ENRICHMENTBEHAVIOUR enrchmentbehaviour;
        if (TileData.ThisIsADynamicEnrichmentItem(this.items[index].tiletype, out enrchmentbehaviour) || enrchmentbehaviour == ENRICHMENTBEHAVIOUR.Trampoline || enrchmentbehaviour == ENRICHMENTBEHAVIOUR.Perch)
        {
          TileInfo tileInfo = TileData.GetTileInfo(this.items[index].tiletype);
          TileRenderer tileRenderer = new TileRenderer(new LayoutEntry(this.items[index].tiletype), this.items[index].Location.X, this.items[index].Location.Y, false);
          if (tileInfo.HasBuildingLayer)
            tileRenderer.RefTopRenderer = new ZooBuildingTopRenderer(tileInfo, this.items[index].Location.X, this.items[index].Location.Y, this.items[index].Rotation, tileRenderer);
          animalsinpens.AddDynamicItemToCellBlock(tileRenderer, CellUID, tileRenderer.RefTopRenderer, enrchmentbehaviour, this.items[index]);
        }
      }
    }

    public List<PenItemDuplicateInfo> GetItemsHereByType()
    {
      this.duplicateinfo = new List<PenItemDuplicateInfo>();
      for (int index1 = 0; index1 < this.items.Count; ++index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < this.duplicateinfo.Count; ++index2)
        {
          if (this.duplicateinfo[index2].enrichmentclass == this.items[index1].enrichmentclass)
          {
            flag = true;
            ++this.duplicateinfo[index2].TotalOfThese;
          }
        }
        if (!flag)
          this.duplicateinfo.Add(new PenItemDuplicateInfo(this.items[index1].enrichmentclass));
      }
      return this.duplicateinfo;
    }

    public PenItem AddNewItem(
      TileRenderer tilerenderer,
      out bool IsDynamicItemForPen,
      out ENRICHMENTBEHAVIOUR enrchmentbehaviour)
    {
      enrchmentbehaviour = ENRICHMENTBEHAVIOUR.Count;
      IsDynamicItemForPen = false;
      Z_GameFlags.MustRebuildPrivacyMap = true;
      if (TileData.IsThisAWaterTrough(tilerenderer.tiletypeonconstruct))
        this.MaximumWaterUnits += TileData.GetWaterVolume(tilerenderer.tiletypeonconstruct);
      this.items.Add(new PenItem(tilerenderer.tiletypeonconstruct, tilerenderer.TileLocation, tilerenderer.RotationOnConstruct));
      IsDynamicItemForPen = TileData.ThisIsADynamicEnrichmentItem(tilerenderer.tiletypeonconstruct, out enrchmentbehaviour);
      return this.items[this.items.Count - 1];
    }

    public PenItem GetItemBlockingThisTile(
      int XLoc,
      int YLoc,
      bool GetCollisionBlockingOnly)
    {
      for (int index = this.items.Count - 1; index > -1; --index)
      {
        if (this.items[index].IsBlockingThisWorldSpace(XLoc, YLoc, GetCollisionBlockingOnly))
          return this.items[index];
      }
      return (PenItem) null;
    }

    public void StartDay(Player player) => this.SetUpWater(player);

    public void SetUpWater(Player player)
    {
      this.SuppliedWaterUnits = 0;
      for (int index = this.items.Count - 1; index > -1; --index)
      {
        if (TileData.IsThisAWaterTrough(this.items[index].tiletype))
        {
          if (TileData.GetTileInfo(this.items[index].tiletype).IsSomethingOverlappingWaterPump(player.prisonlayout.layout.BaseTileTypes[this.items[index].Location.X, this.items[index].Location.Y].RotationClockWise, this.items[index].Location.X, this.items[index].Location.Y))
          {
            this.SuppliedWaterUnits += TileData.GetWaterVolume(this.items[index].tiletype);
            player.livestats.SetWaterStatusForTrough(this.items[index].Location.X, this.items[index].Location.Y, 1);
          }
          else
            player.livestats.SetWaterStatusForTrough(this.items[index].Location.X, this.items[index].Location.Y, 0);
        }
      }
    }

    public void RemoveItem(
      Vector2Int TileLoc,
      TILETYPE tiletype,
      int PenUID,
      Player player,
      PenItem penitem = null)
    {
      if (penitem != null)
      {
        for (int index = this.items.Count - 1; index > -1; --index)
        {
          if (this.items[index] == penitem)
          {
            if (this.MaximumWaterUnits > 0 && TileData.IsThisAWaterTrough(this.items[index].tiletype))
              this.MaximumWaterUnits -= TileData.GetWaterVolume(this.items[index].tiletype);
            Z_GameFlags.MustRebuildPrivacyMap = true;
            StaticPenNavPool.GetThisExistingPenNav(PenUID);
            OverWorldManager.overworldenvironment.animalsinpens.RemoveDynamicItemFromCellBlock(this.items[index], PenUID, player);
            this.items.RemoveAt(index);
            return;
          }
        }
      }
      else
      {
        for (int index = this.items.Count - 1; index > -1; --index)
        {
          if (this.items[index].Location.CompareMatches(TileLoc) && this.items[index].tiletype == tiletype)
          {
            if (this.MaximumWaterUnits > 0 && TileData.IsThisAWaterTrough(this.items[index].tiletype))
              this.MaximumWaterUnits -= TileData.GetWaterVolume(this.items[index].tiletype);
            Z_GameFlags.MustRebuildPrivacyMap = true;
            this.items.RemoveAt(index);
            return;
          }
        }
      }
      throw new Exception("DIDNT FIND THIS ENTRY");
    }

    public PenItems(Reader reader)
    {
      this.items = new List<PenItem>();
      int _out = 0;
      int num1 = (int) reader.ReadInt("p", ref _out);
      for (int index = 0; index < _out; ++index)
        this.items.Add(new PenItem(reader));
      int num2 = (int) reader.ReadInt("p", ref _out);
      this.duplicateinfo = new List<PenItemDuplicateInfo>();
      for (int index = 0; index < _out; ++index)
        this.duplicateinfo.Add(new PenItemDuplicateInfo(reader));
      int num3 = (int) reader.ReadInt("p", ref this.MaximumWaterUnits);
      int num4 = (int) reader.ReadInt("p", ref this.SuppliedWaterUnits);
      int num5 = (int) reader.ReadInt("p", ref _out);
      this.enrchmentbehaviour = (ENRICHMENTBEHAVIOUR) _out;
    }

    public void SavePenItems(Writer writer)
    {
      writer.WriteInt("p", this.items.Count);
      for (int index = 0; index < this.items.Count; ++index)
        this.items[index].SavePenItem(writer);
      writer.WriteInt("p", this.duplicateinfo.Count);
      for (int index = 0; index < this.duplicateinfo.Count; ++index)
        this.duplicateinfo[index].SavePenItemDuplicateInfo(writer);
      writer.WriteInt("p", this.MaximumWaterUnits);
      writer.WriteInt("p", this.SuppliedWaterUnits);
      writer.WriteInt("p", (int) this.enrchmentbehaviour);
    }
  }
}
