// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Animals.AnimalCollectionPageFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Z_Collection.Animals.BottomFrame;
using TinyZoo.Z_Collection.Shared;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Collection.Animals
{
  internal class AnimalCollectionPageFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private SpeciesDetailFrame detailFrame;
    private CollectionGridDisplay gridDisplay;
    private AnimalBottomFrame bottomFrame;
    private RowSegmentRectangle hackyFrameToHideTopScroll;
    private RowSegmentRectangle hackyFrameToHideBottomScroll;
    private SimpleTextHandler noAnimalsDesc;
    private CollectionType collectionType;
    private int buildingUID;
    private int numberPerRow;
    private UIScaleHelper scaleHelper;
    private Vector2 buffer;
    private float BaseScale;
    private float heightForGridDisplay;

    public AnimalCollectionPageFrame(
      CollectionType _collectionType,
      Player player,
      float _BaseScale,
      Vector2 forcedSize,
      int _numberPerRow = -1,
      int _buildingUID = -1)
    {
      this.collectionType = _collectionType;
      this.buildingUID = _buildingUID;
      this.BaseScale = _BaseScale;
      this.numberPerRow = _numberPerRow;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.buffer = this.scaleHelper.DefaultBuffer;
      this.customerFrame = new CustomerFrame(forcedSize, BaseScale: this.BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      float y1 = this.buffer.Y;
      float x = this.buffer.X;
      this.detailFrame = new SpeciesDetailFrame(this.collectionType, this.BaseScale, this.customerFrame.VSCale.X - this.buffer.X * 2f);
      this.hackyFrameToHideTopScroll = new RowSegmentRectangle(this.customerFrame.VSCale.X, 40f, ColourData.Z_FrameMidBrown, 1f);
      this.bottomFrame = new AnimalBottomFrame(this.collectionType, this.BaseScale, this.customerFrame.VSCale.X - this.buffer.X * 2f, this.buildingUID);
      this.gridDisplay = new CollectionGridDisplay(this.collectionType, player, this.BaseScale, this.numberPerRow, this.buildingUID);
      this.hackyFrameToHideBottomScroll = new RowSegmentRectangle(this.customerFrame.VSCale.X, 40f, ColourData.Z_FrameMidBrown, 1f);
      string TextToWrite = string.Empty;
      if (this.collectionType == CollectionType.QuarantineAnimals)
        TextToWrite = "You do not have any quarantined animals. You can quarantine an animal from selecting them from their pens.";
      this.noAnimalsDesc = new SimpleTextHandler(TextToWrite, this.customerFrame.VSCale.X * 0.8f, true, this.BaseScale, true, true);
      this.noAnimalsDesc.SetAllColours(ColourData.Z_Cream);
      this.noAnimalsDesc.Location = new Vector2(x, y1);
      this.noAnimalsDesc.Location.Y += vector2.Y + this.buffer.Y;
      Vector2 size = this.detailFrame.GetSize();
      this.detailFrame.location = new Vector2(x, y1) + size * 0.5f;
      this.detailFrame.location += vector2;
      float y2 = y1 + size.Y + this.buffer.Y;
      this.hackyFrameToHideTopScroll.vLocation.Y = y2 + vector2.Y;
      this.hackyFrameToHideTopScroll.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.heightForGridDisplay = (float) ((double) this.customerFrame.VSCale.Y - (double) y2 - (double) this.buffer.Y * 2.0) - this.bottomFrame.GetSize().Y;
      this.gridDisplay.location = new Vector2(x, y2);
      this.gridDisplay.location += vector2;
      float num = y2 + this.heightForGridDisplay;
      this.hackyFrameToHideBottomScroll.vLocation.Y = num + vector2.Y;
      this.hackyFrameToHideBottomScroll.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.bottomFrame.location.Y = num + this.buffer.Y + vector2.Y;
      this.bottomFrame.location.Y += this.bottomFrame.GetSize().Y * 0.5f;
      this.RefreshGridContents(player);
    }

    public void RefreshGridContents(Player player)
    {
      this.gridDisplay.Create(this.collectionType, player, this.BaseScale, this.buildingUID);
      this.gridDisplay.AddDrag(this.heightForGridDisplay);
      AnimalGridEntryFrame currentlySelectedEntry = this.gridDisplay.GetCurrentlySelectedEntry();
      if (currentlySelectedEntry != null)
        this.ClickedThisEntry(currentlySelectedEntry, player);
      else
        this.detailFrame.SetToNoneSelected();
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateAnimalCollectionPageFrame(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool ForceClosePanel,
      out bool RefreshGridList)
    {
      offset += this.location;
      AnimalGridEntryFrame entryClicked = this.gridDisplay.UpdateCollectionGridDisplay(player, DeltaTime, offset);
      if (entryClicked != null)
        this.ClickedThisEntry(entryClicked, player);
      this.detailFrame.UpdateSpeciesDetailFrame(player, DeltaTime, offset);
      if (this.collectionType == CollectionType.Seeds)
      {
        this.detailFrame.UpdateSeedDetailFrame(player, DeltaTime, offset, out ForceClosePanel, out RefreshGridList);
      }
      else
      {
        if (this.detailFrame.UpdateQuarantinedAnimalsDetailFrame(player, DeltaTime, offset, out ForceClosePanel, out RefreshGridList))
          this.TryToSelect(this.gridDisplay.GetCurrentlySelectedEntry());
        bool SelectAllClicked;
        this.bottomFrame.UpdateAnimalBottomFrame(player, DeltaTime, offset, out ForceClosePanel, out SelectAllClicked);
        if (!SelectAllClicked)
          return;
        List<AnimalGridEntryFrame> entries = this.gridDisplay.GetEntries();
        for (int index = 0; index < entries.Count; ++index)
          this.TryToSelect(entries[index], true);
      }
    }

    private void ClickedThisEntry(AnimalGridEntryFrame entryClicked, Player player)
    {
      switch (this.collectionType)
      {
        case CollectionType.Animals:
          this.detailFrame.SetAnimal(entryClicked.refAnimalType, player, entryClicked.IsUnlocked);
          break;
        case CollectionType.EmployeesJobs:
          this.detailFrame.SetEmployeeTypeToView(entryClicked.refEmployeeType, entryClicked.refAnimalType, entryClicked.refTileType, player, entryClicked.IsUnlocked);
          break;
        case CollectionType.QuarantineAnimals:
          this.detailFrame.SetQuarantinedAnimalToView(entryClicked.refPrisonerInfo, player, this.buildingUID);
          if (!this.bottomFrame.quarantineFrame.animalsSelected.Contains(entryClicked.refPrisonerInfo))
            break;
          this.detailFrame.SetTextButtonState(true);
          break;
        case CollectionType.Seeds:
          this.detailFrame.SetSeedToApplyToField(player, true, entryClicked.refCropType);
          break;
      }
    }

    private bool TryToSelect(AnimalGridEntryFrame entry, bool AddOnly_DoNotRemove = false)
    {
      if (this.collectionType != CollectionType.QuarantineAnimals)
        return false;
      bool quarantineAnimalsList = this.bottomFrame.AddToQuarantineAnimalsList(entry.refPrisonerInfo, AddOnly_DoNotRemove);
      if (!(!quarantineAnimalsList & AddOnly_DoNotRemove))
      {
        this.gridDisplay.ToggleTickOnEntry(!quarantineAnimalsList, entry);
        this.detailFrame.SetTextButtonState(quarantineAnimalsList);
      }
      return quarantineAnimalsList;
    }

    public void DrawAnimalCollectionPageFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      if (this.gridDisplay.GetNumberOfEntries() == 0)
      {
        this.noAnimalsDesc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      }
      else
      {
        this.gridDisplay.DrawCollectionGridDisplay(offset, spriteBatch);
        this.hackyFrameToHideTopScroll.DrawRowSegmentRectangle(offset, spriteBatch);
        this.hackyFrameToHideBottomScroll.DrawRowSegmentRectangle(offset, spriteBatch);
        this.detailFrame.DrawSpeciesDetailFrame(offset, spriteBatch);
        this.bottomFrame.DrawAnimalBottomFrame(offset, spriteBatch);
      }
    }
  }
}
