// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Animals.Hybrid.HybridFullDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_BarInfo.MainBar.Scroll;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Collection.Animals.Hybrid
{
  internal class HybridFullDisplay
  {
    public Vector2 location;
    private BackButton backArrow;
    private AnimalInFrameGrid animalGrid;
    private List<AnimalRenderDescriptor> HybridsDiscovered;
    private ZGenericText header;
    private Z_ScrollButton leftArrow;
    private Z_ScrollButton rightArrow;
    private UIScaleHelper scaleHelper;
    private float BaseScale;
    private float Xbuffer;
    private float Ybuffer;
    private int currentPageIndex;
    private int numberOfPages;
    private int numberPerRow;
    private int numberOfRowsPerPage;
    private Vector2 FixedLocationForGrid;
    private Vector2 refFrameSize;

    public HybridFullDisplay(
      AnimalType animalType,
      Player player,
      float _BaseScale,
      Vector2 frameSize)
    {
      this.BaseScale = _BaseScale;
      this.refFrameSize = frameSize;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.Xbuffer = this.scaleHelper.GetDefaultXBuffer();
      this.Ybuffer = this.scaleHelper.GetDefaultYBuffer();
      float xbuffer = this.Xbuffer;
      float ybuffer = this.Ybuffer;
      this.HybridsDiscovered = player.unlocks.GetAllHybridsDiscoveredForThisAnimalType(animalType);
      this.numberPerRow = 12;
      this.numberOfRowsPerPage = 2;
      this.numberOfPages = this.HybridsDiscovered.Count / (this.numberOfRowsPerPage * this.numberPerRow);
      this.currentPageIndex = 0;
      this.header = new ZGenericText("Dominant Hybrids Discovered: " + (object) this.HybridsDiscovered.Count, this.BaseScale, false, _UseOnePointFiveFont: true);
      this.header.vLocation = new Vector2(xbuffer, ybuffer);
      this.backArrow = new BackButton(true, BaseScale: this.BaseScale, _IsPrevious: true);
      this.backArrow.vLocation = Vector2.Zero;
      Vector2 size = this.backArrow.GetSize();
      BackButton backArrow1 = this.backArrow;
      backArrow1.vLocation = backArrow1.vLocation + size * 0.5f;
      BackButton backArrow2 = this.backArrow;
      backArrow2.vLocation = backArrow2.vLocation + new Vector2(this.Xbuffer, this.Ybuffer);
      this.RefreshPage();
    }

    private void RefreshPage()
    {
      List<AnimalRenderDescriptor> animals = new List<AnimalRenderDescriptor>();
      int num1 = this.currentPageIndex * (this.numberOfRowsPerPage * this.numberPerRow);
      int num2 = Math.Min(num1 + this.numberOfRowsPerPage * this.numberPerRow, this.HybridsDiscovered.Count);
      for (int index = num1; index < num2; ++index)
        animals.Add(this.HybridsDiscovered[index]);
      this.animalGrid = new AnimalInFrameGrid(this.BaseScale, this.numberPerRow, this.Xbuffer, this.Ybuffer, animals);
      if (this.currentPageIndex == 0)
      {
        this.animalGrid.location.Y += (float) ((double) this.header.vLocation.Y + (double) this.header.GetSize().Y + (double) this.Ybuffer * 0.5);
        this.animalGrid.location.X = (float) ((double) this.refFrameSize.X * 0.5 - (double) this.animalGrid.GetSize().X * 0.5);
        this.FixedLocationForGrid = this.animalGrid.location;
      }
      else
        this.animalGrid.location = this.FixedLocationForGrid;
      this.header.vLocation.X = this.animalGrid.location.X;
      if (this.currentPageIndex > 0)
      {
        if (this.leftArrow == null)
        {
          this.leftArrow = new Z_ScrollButton(true, this.BaseScale);
          this.leftArrow.vLocation.X = this.animalGrid.location.X;
          this.leftArrow.vLocation.Y = this.refFrameSize.Y * 0.5f;
          Z_ScrollButton leftArrow = this.leftArrow;
          leftArrow.vLocation = leftArrow.vLocation + (new Vector2(-this.leftArrow.GetSize().X, this.leftArrow.GetSize().Y) * 0.5f + new Vector2(-this.Xbuffer, 0.0f));
        }
      }
      else
        this.leftArrow = (Z_ScrollButton) null;
      if (num2 != this.HybridsDiscovered.Count)
      {
        if (this.rightArrow != null)
          return;
        this.rightArrow = new Z_ScrollButton(false, this.BaseScale);
        this.rightArrow.vLocation.X = this.animalGrid.location.X + this.animalGrid.GetSize().X;
        this.rightArrow.vLocation.Y = this.refFrameSize.Y * 0.5f;
        Z_ScrollButton rightArrow = this.rightArrow;
        rightArrow.vLocation = rightArrow.vLocation + (this.rightArrow.GetSize() * 0.5f + new Vector2(this.Xbuffer, 0.0f));
      }
      else
        this.rightArrow = (Z_ScrollButton) null;
    }

    public bool UpdateHybridFullDisplay(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.leftArrow != null && this.leftArrow.UpdateZ_ScrollButton(offset, player, DeltaTime, true))
      {
        --this.currentPageIndex;
        if (this.currentPageIndex < 0)
          this.currentPageIndex = this.numberOfPages;
        this.RefreshPage();
      }
      if (this.rightArrow != null && this.rightArrow.UpdateZ_ScrollButton(offset, player, DeltaTime, true))
      {
        ++this.currentPageIndex;
        if (this.currentPageIndex > this.numberOfPages)
          this.currentPageIndex = 0;
        this.RefreshPage();
      }
      return this.backArrow.UpdateBackButton(player, DeltaTime, offset);
    }

    public void DrawHybridFullDisplay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.header.DrawZGenericText(offset, spriteBatch);
      this.animalGrid.DrawAnimalInFrameGrid(offset, spriteBatch);
      this.backArrow.DrawBackButton(offset, spriteBatch);
      if (this.leftArrow != null)
        this.leftArrow.DrawZ_ScrollButton(offset, spriteBatch);
      if (this.rightArrow == null)
        return;
      this.rightArrow.DrawZ_ScrollButton(offset, spriteBatch);
    }
  }
}
