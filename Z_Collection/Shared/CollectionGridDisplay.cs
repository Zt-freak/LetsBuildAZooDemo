// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Shared.CollectionGridDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Collection.Animals;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_Farms;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Collection.Shared
{
  internal class CollectionGridDisplay
  {
    public Vector2 location;
    private List<AnimalGridEntryFrame> animalEntries;
    private UIScaleHelper scaleHelper;
    private int numberPerRow;
    private float maxHeightForDisplay_DoDrag = -1f;
    private Vector2 entryFrameSize;
    private Vector2 refCachedSize;
    private float YscrollOffset;
    private bool DragWithinGridZone;

    public int selectedEntryIndex { get; private set; }

    public CollectionGridDisplay(
      CollectionType collectionType,
      Player player,
      float BaseScale,
      int _numberPerRow = -1,
      int buildingUID = -1)
    {
      this.scaleHelper = new UIScaleHelper(BaseScale);
      if (_numberPerRow != -1)
        this.numberPerRow = _numberPerRow;
      else
        this.numberPerRow = 4;
    }

    public void Create(
      CollectionType collectionType,
      Player player,
      float BaseScale,
      int buildingUID = -1)
    {
      this.animalEntries = new List<AnimalGridEntryFrame>();
      switch (collectionType)
      {
        case CollectionType.Animals:
          for (int index = 0; index < 56; ++index)
            this.animalEntries.Add(new AnimalGridEntryFrame(collectionType, player, BaseScale, (AnimalType) index));
          break;
        case CollectionType.EmployeesJobs:
          bool IsAGirl;
          for (int index1 = 1; index1 < 20; ++index1)
          {
            if (index1 == 9)
            {
              List<TILETYPE> buildingWithEmployees = EmployeeData.GetBuildingWithEmployees();
              for (int index2 = 0; index2 < buildingWithEmployees.Count; ++index2)
              {
                if (TileData.IsThisAShopWithShopStats(buildingWithEmployees[index2]))
                {
                  AnimalType buildingtoEmployee = EmployeeData.GetBuildingtoEmployee(buildingWithEmployees[index2], out IsAGirl, 0);
                  this.animalEntries.Add(new AnimalGridEntryFrame(collectionType, player, BaseScale, buildingtoEmployee, (EmployeeType) index1, buildingWithEmployees[index2]));
                }
              }
            }
            else if (index1 != 15)
            {
              AnimalType employee = Employees.GetEmployee((EmployeeType) index1, out IsAGirl);
              this.animalEntries.Add(new AnimalGridEntryFrame(collectionType, player, BaseScale, employee, (EmployeeType) index1));
            }
          }
          break;
        case CollectionType.QuarantineAnimals:
          List<PrisonerInfo> quarantinedAnimals = player.animalquarantine.GetThisQuarantineBuilding(buildingUID).GetListOfQuarantinedAnimals(player);
          for (int index = 0; index < quarantinedAnimals.Count; ++index)
            this.animalEntries.Add(new AnimalGridEntryFrame(collectionType, player, BaseScale, quarantinedAnimals[index]));
          break;
        case CollectionType.Seeds:
          List<CROPTYPE> displayListOfCroPs = CropData.GetDisplayListOfCROPs();
          for (int index = 0; index < displayListOfCroPs.Count; ++index)
            this.animalEntries.Add(new AnimalGridEntryFrame(collectionType, player, BaseScale, AnimalType.None, croptype: displayListOfCroPs[index]));
          break;
      }
      for (int index = 0; index < this.animalEntries.Count; ++index)
      {
        if (this.entryFrameSize == Vector2.Zero)
          this.entryFrameSize = this.animalEntries[index].GetSize();
        this.animalEntries[index].location.X = (this.entryFrameSize.X + this.scaleHelper.GetDefaultXBuffer()) * (float) (index % this.numberPerRow);
        this.animalEntries[index].location.Y = (this.entryFrameSize.Y + this.scaleHelper.GetDefaultYBuffer()) * (float) (index / this.numberPerRow);
        this.animalEntries[index].location += this.entryFrameSize * 0.5f;
      }
      this.selectedEntryIndex = -1;
      if (this.animalEntries.Count <= 0)
        return;
      this.ClickedNewEntry(0);
    }

    public void AddDrag(float _maxHeightForDisplay_DoDrag, bool _DragWithinGridZone = true)
    {
      this.maxHeightForDisplay_DoDrag = _maxHeightForDisplay_DoDrag;
      this.refCachedSize = this.GetSize();
      this.DragWithinGridZone = _DragWithinGridZone;
    }

    public Vector2 GetSize()
    {
      if (this.animalEntries.Count == 0)
        return Vector2.Zero;
      Vector2 size = this.animalEntries[0].GetSize();
      float y = this.animalEntries[this.animalEntries.Count - 1].location.Y - this.animalEntries[0].location.Y + size.Y;
      return new Vector2(this.animalEntries[Math.Min(this.animalEntries.Count, this.numberPerRow) - 1].location.X - this.animalEntries[0].location.X + size.X, y);
    }

    public AnimalGridEntryFrame GetCurrentlySelectedEntry() => this.selectedEntryIndex == -1 ? (AnimalGridEntryFrame) null : this.animalEntries[this.selectedEntryIndex];

    public int GetNumberOfEntries() => this.animalEntries.Count;

    public List<AnimalGridEntryFrame> GetEntries() => this.animalEntries;

    public AnimalGridEntryFrame UpdateCollectionGridDisplay(
      Player player,
      float DeltaTime,
      Vector2 offset)
    {
      offset += this.location;
      bool flag = true;
      if ((double) this.maxHeightForDisplay_DoDrag != -1.0 && (double) this.refCachedSize.Y > (double) this.maxHeightForDisplay_DoDrag)
      {
        if (this.DragWithinGridZone)
          flag = MathStuff.CheckPointCollision(false, offset, 1f, this.refCachedSize.X, this.maxHeightForDisplay_DoDrag, player.inputmap.PointerLocation);
        if (flag)
        {
          this.YscrollOffset += player.inputmap.momentumwheel.MovementThisFrame * 0.25f;
          this.YscrollOffset = Math.Min(this.YscrollOffset, 0.0f);
          this.YscrollOffset = Math.Max(this.YscrollOffset, -this.refCachedSize.Y + this.maxHeightForDisplay_DoDrag);
        }
      }
      offset.Y += this.YscrollOffset;
      for (int index = 0; index < this.animalEntries.Count; ++index)
      {
        if (this.animalEntries[index].UpdateAnimalGridEntryFrame(player, DeltaTime, offset, !flag))
        {
          this.ClickedNewEntry(index);
          return this.animalEntries[index];
        }
      }
      return (AnimalGridEntryFrame) null;
    }

    private void ClickedNewEntry(int indexClicked)
    {
      if (this.selectedEntryIndex != -1)
        this.animalEntries[this.selectedEntryIndex].SetSelected(false);
      this.selectedEntryIndex = indexClicked;
      this.animalEntries[indexClicked].SetSelected(true);
    }

    public void ToggleTickOnEntry(bool Remove, int entryIndex) => this.animalEntries[entryIndex].SetTick(Remove);

    public void ToggleTickOnEntry(bool Remove, AnimalGridEntryFrame entry) => entry.SetTick(Remove);

    public void DrawCollectionGridDisplay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      offset.Y += this.YscrollOffset;
      for (int index = 0; index < this.animalEntries.Count; ++index)
      {
        int refAnimalType = (int) this.animalEntries[index].refAnimalType;
        bool flag = false;
        if ((double) this.maxHeightForDisplay_DoDrag != -1.0 && ((double) this.animalEntries[index].location.Y + (double) this.YscrollOffset + (double) this.entryFrameSize.Y < 0.0 || (double) this.animalEntries[index].location.Y + (double) this.YscrollOffset - (double) this.entryFrameSize.Y * 0.5 > (double) this.maxHeightForDisplay_DoDrag))
          flag = true;
        if (!flag)
          this.animalEntries[index].DrawAnimalGridEntryFrame(offset, spriteBatch);
      }
    }
  }
}
