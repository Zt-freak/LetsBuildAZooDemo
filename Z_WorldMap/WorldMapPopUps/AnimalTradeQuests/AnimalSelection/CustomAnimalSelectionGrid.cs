// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.CustomAnimalSelectionGrid
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.CollectionScreen;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BalanceSystems.Animals.SellCosts;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.SelectionGrid;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection
{
  internal class CustomAnimalSelectionGrid
  {
    public Vector2 location;
    private List<AlienEntry> alienEntries;
    private List<CustomAnimalSelectionEntry> refAnimals;
    private SimpleArrowPageButtons pageArrows;
    private Vector2 sizeOfOneEntry;
    private float Xbuffer;
    private float Ybuffer;
    private int numberPerRow;
    private int maxRows;
    private int lastSelectedIndex;
    private int maxPageCount;
    private int currentPageIndex;
    private int startIndex;
    private int endIndex;
    private float BaseScale;
    private bool AddSellPrice;
    private Vector2 extraFrameSizeForCostDisplay_raw;
    private CustomerFrameMouseOverBox mouseOverBox;
    private int lastMouseOverIndex;

    public CustomAnimalSelectionGrid(
      List<CustomAnimalSelectionEntry> animals,
      Player player,
      float _BaseScale,
      int _numberPerRow = 10,
      int _maxRows = -1,
      bool _AddSellPrice = false)
    {
      this.BaseScale = _BaseScale;
      this.AddSellPrice = _AddSellPrice;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      this.Xbuffer = uiScaleHelper.GetDefaultXBuffer();
      this.Ybuffer = uiScaleHelper.GetDefaultYBuffer();
      this.refAnimals = animals;
      this.numberPerRow = _numberPerRow;
      this.maxRows = _maxRows;
      this.alienEntries = new List<AlienEntry>();
      this.lastSelectedIndex = -1;
      this.lastMouseOverIndex = -1;
      AlienEntry alienEntry = new AlienEntry(AnimalType.Rabbit, true, true, 0, this.BaseScale);
      if (this.AddSellPrice)
      {
        this.extraFrameSizeForCostDisplay_raw = new Vector2(14f, 22f);
        alienEntry.AddExtraFrame(this.extraFrameSizeForCostDisplay_raw, ColourData.Z_FrameMidBrown);
      }
      this.sizeOfOneEntry = new Vector2(alienEntry.GetWidth(), alienEntry.GetHeight());
      this.maxPageCount = (int) Math.Ceiling((double) this.refAnimals.Count / (double) (this.maxRows * this.numberPerRow));
      if (this.maxRows != -1)
      {
        this.pageArrows = new SimpleArrowPageButtons(this.BaseScale, _DoNotDrawFrame: true);
        this.pageArrows.Location = this.GetSize(true, true, false);
        this.pageArrows.Location.X -= this.pageArrows.GetSize(true).X * 0.5f;
        this.pageArrows.Location.Y += this.pageArrows.GetSize(true).Y * 0.5f + this.Ybuffer;
      }
      this.GoToThisPage(0);
    }

    private void GoToThisPage(int pageIndex)
    {
      this.startIndex = pageIndex * (this.maxRows * this.numberPerRow);
      this.endIndex = this.refAnimals.Count;
      if (this.maxRows != -1)
        this.endIndex = this.startIndex + this.maxRows * this.numberPerRow;
      this.endIndex = Math.Min(this.endIndex, this.refAnimals.Count);
      if (this.alienEntries.Count >= this.endIndex)
      {
        for (int startIndex = this.startIndex; startIndex < this.endIndex; ++startIndex)
          this.alienEntries[startIndex].LerpIn();
      }
      else
      {
        for (int startIndex = this.startIndex; startIndex < this.endIndex; ++startIndex)
        {
          AlienEntry alienEntry1 = new AlienEntry(this.refAnimals[startIndex].refPrisonerInfo.GetAnimalPainted(), !this.refAnimals[startIndex].Darken_NotAvailable, true, this.refAnimals[startIndex].refPrisonerInfo.intakeperson.CLIndex, this.BaseScale, this.refAnimals[startIndex].refPrisonerInfo.intakeperson.HeadType, this.refAnimals[startIndex].refPrisonerInfo.intakeperson.HeadVariant);
          alienEntry1.vLocation.X = (this.sizeOfOneEntry.X + this.Xbuffer) * (float) ((startIndex - this.startIndex) % this.numberPerRow);
          alienEntry1.vLocation.Y = (this.sizeOfOneEntry.Y + this.Ybuffer) * (float) ((startIndex - this.startIndex) / this.numberPerRow);
          AlienEntry alienEntry2 = alienEntry1;
          alienEntry2.vLocation = alienEntry2.vLocation + this.sizeOfOneEntry * 0.5f;
          if (this.AddSellPrice)
          {
            alienEntry1.AddExtraFrame(this.extraFrameSizeForCostDisplay_raw, ColourData.Z_FrameMidBrown);
            alienEntry1.AddStringBelow_NEW("$" + (object) AnimalSellCostCalculator.GetSellCostOfPlayerAnimal(this.refAnimals[startIndex].refPrisonerInfo), ColourData.Z_Cream, this.BaseScale, AssetContainer.SpringFontX1AndHalf, RecreateIfExists_ElseUpdateString: false);
          }
          if (this.refAnimals[startIndex].Darken_NotAvailable)
          {
            alienEntry1.SetLock();
            alienEntry1.SetDiscovered();
          }
          this.alienEntries.Add(alienEntry1);
        }
      }
      this.currentPageIndex = pageIndex;
      if (this.pageArrows == null)
        return;
      if (this.currentPageIndex == 0)
        this.pageArrows.SetAsDisabled(true, true);
      else
        this.pageArrows.SetAsDisabled(true, false);
      if (this.currentPageIndex >= this.maxPageCount - 1)
        this.pageArrows.SetAsDisabled(false, true);
      else
        this.pageArrows.SetAsDisabled(false, false);
    }

    public Vector2 GetSize(
      bool GetMaxExpectedWidth = false,
      bool GetMaxExpectedHeight = false,
      bool IncludeArrows = true)
    {
      int num1 = 0;
      if (GetMaxExpectedWidth)
        num1 = this.numberPerRow;
      else if (this.alienEntries.Count != 0)
        num1 = Math.Min(this.alienEntries.Count, this.numberPerRow);
      double num2 = (double) this.sizeOfOneEntry.X * (double) num1 + (double) (num1 - 1) * (double) this.Xbuffer;
      int num3 = this.alienEntries.Count / this.numberPerRow;
      if (this.maxRows != -1 & GetMaxExpectedHeight)
      {
        num3 = this.maxRows;
      }
      else
      {
        int num4 = GetMaxExpectedHeight ? 1 : 0;
      }
      float num5 = (float) ((double) this.sizeOfOneEntry.Y * (double) num3 + (double) (num3 - 1) * (double) this.Ybuffer);
      if (IncludeArrows && this.pageArrows != null)
        num5 += this.Ybuffer + this.pageArrows.GetSize().Y;
      double num6 = (double) num5;
      return new Vector2((float) num2, (float) num6);
    }

    private void ChangePage(int forwardOrBack)
    {
      int pageIndex = MathHelper.Clamp(this.currentPageIndex + forwardOrBack, 0, this.maxPageCount - 1);
      if (pageIndex == this.currentPageIndex)
        return;
      this.GoToThisPage(pageIndex);
    }

    public PrisonerInfo UpdateCustomAnimalSelectionGrid(
      Player player,
      float DeltaTime,
      Vector2 offset)
    {
      offset += this.location;
      int forwardOrBack = this.pageArrows.UpdateSimpleArrowPageButtons(DeltaTime, player, offset);
      if (forwardOrBack != 0)
        this.ChangePage(forwardOrBack);
      if (this.mouseOverBox != null)
        this.mouseOverBox.Active = false;
      for (int startIndex = this.startIndex; startIndex < this.endIndex; ++startIndex)
      {
        if (this.alienEntries[startIndex].UpdateAlienEntry(offset, DeltaTime, player) && this.alienEntries[startIndex].IsUnlocked)
        {
          this.lastSelectedIndex = startIndex;
          return this.refAnimals[startIndex].refPrisonerInfo;
        }
        if (this.alienEntries[startIndex].isMouseOver)
        {
          if (this.lastMouseOverIndex == -1 && this.lastMouseOverIndex != startIndex)
            this.CreateMouseOverBox(this.refAnimals[startIndex].MouseOverText, this.alienEntries[startIndex]);
          if (this.mouseOverBox != null && !string.IsNullOrEmpty(this.refAnimals[startIndex].MouseOverText))
            this.mouseOverBox.Active = true;
        }
      }
      return (PrisonerInfo) null;
    }

    private void CreateMouseOverBox(string text, AlienEntry entry)
    {
      if (string.IsNullOrEmpty(text))
        return;
      this.mouseOverBox = new CustomerFrameMouseOverBox(this.BaseScale, text, 250f * this.BaseScale);
      this.mouseOverBox.location = entry.vLocation;
      this.mouseOverBox.location.Y -= entry.GetSize().Y * 0.5f;
    }

    public void TickLastSelectedEntry(bool isRemoved = false) => this.alienEntries[this.lastSelectedIndex].AddTick(isRemoved);

    public void DrawCustomAnimalSelectionGrid(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int startIndex = this.startIndex; startIndex < this.endIndex; ++startIndex)
        this.alienEntries[startIndex].DrawAlienEntry(offset, spriteBatch);
      if (this.pageArrows != null)
        this.pageArrows.DrawSimpleArrowPageButtons(offset, spriteBatch);
      if (this.mouseOverBox == null)
        return;
      this.mouseOverBox.DrawCustomerFrameMouseOverBoc(offset, spriteBatch);
    }
  }
}
