// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.AnimalView.ProcessingAnimalInfoView
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.CollectionScreen;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.Research;
using TinyZoo.PlayerDir.Commodities;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.Z_BuildingInfo.Z_Processing.Data;
using TinyZoo.Z_Farms;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.AnimalView
{
  internal class ProcessingAnimalInfoView
  {
    public Vector2 location;
    private CollectionScreenManager collection;
    private Vector2 size;
    private AnimalMeatMouseOver mouseOverFrame;
    private float BaseScale;
    private GameObject testPoint;
    private bool IsWarehouse;
    private bool IsWarehouseStock;

    public static int TotalWarehouseStockCount { get; private set; }

    public ProcessingAnimalInfoView(
      Player player,
      float _BaseScale,
      bool IsCrops,
      bool _IsWarehouse = false,
      bool _IsWarehouseStock = false)
    {
      this.BaseScale = _BaseScale;
      this.IsWarehouse = _IsWarehouse;
      this.IsWarehouseStock = _IsWarehouseStock;
      this.SetUpGrid(player, IsCrops, this.IsWarehouse);
    }

    private void SetUpGrid(Player player, bool IsCrops, bool IsWarehouse)
    {
      this.collection = new CollectionScreenManager(player, BaseScale: this.BaseScale, isCustomSelection_addEntriesLater: true);
      Vector2 vector2 = new Vector2(14f, 22f);
      List<AlienEntry> _alienEntries = new List<AlienEntry>();
      if (IsCrops)
      {
        List<CROPTYPE> displayListOfCroPs = CropData.GetDisplayListOfCROPs();
        for (int index = 0; index < displayListOfCroPs.Count; ++index)
        {
          bool flag = true;
          _alienEntries.Add(new AlienEntry(AnimalType.None, flag, flag, 0, this.BaseScale, _cropType: displayListOfCroPs[index], DrawGrownPlant: true));
          _alienEntries[_alienEntries.Count - 1].AddExtraFrame(new Vector2(14f, 22f), ColourData.Z_FrameDarkBrown);
          _alienEntries[_alienEntries.Count - 1].AddStringBelow_NEW("$??", ColourData.Z_Cream, this.BaseScale, AssetContainer.SpringFontX1AndHalf);
        }
      }
      else if (IsWarehouse || this.IsWarehouseStock)
      {
        List<AnimalFoodType> ofWarehouseItems = WarehouseData.GetDisplayListOfWarehouseItems();
        WarehouseStock[] warehouseStock = player.warehouse.GetWarehouseStock();
        if (this.IsWarehouseStock)
          ProcessingAnimalInfoView.TotalWarehouseStockCount = 0;
        for (int index = 0; index < ofWarehouseItems.Count; ++index)
        {
          bool flag = true;
          string empty = string.Empty;
          int totalStored = warehouseStock[(int) ofWarehouseItems[index]].GetTotalStored();
          if (totalStored == -1)
            flag = false;
          string text;
          if (this.IsWarehouseStock)
          {
            int num = Math.Max(0, totalStored);
            text = num.ToString();
            ProcessingAnimalInfoView.TotalWarehouseStockCount += num;
          }
          else
            text = "$" + (object) warehouseStock[(int) ofWarehouseItems[index]].SellValue;
          _alienEntries.Add(new AlienEntry(AnimalType.None, flag, flag, SCALEs: this.BaseScale, _animalFoodType: ofWarehouseItems[index]));
          _alienEntries[_alienEntries.Count - 1].AddExtraFrame(new Vector2(14f, 22f), ColourData.Z_FrameDarkBrown);
          _alienEntries[_alienEntries.Count - 1].AddStringBelow_NEW(text, ColourData.Z_Cream, this.BaseScale, AssetContainer.SpringFontX1AndHalf);
        }
      }
      else
      {
        List<AnimalType> animalTypeList = new List<AnimalType>();
        animalTypeList.Add(AnimalType.Rabbit);
        animalTypeList.AddRange((IEnumerable<AnimalType>) ResearchData.GetAliensReseachedInOrder());
        foreach (AnimalType _enemy in animalTypeList)
        {
          bool flag = true;
          _alienEntries.Add(new AlienEntry(_enemy, flag, flag, 0, this.BaseScale));
          _alienEntries[_alienEntries.Count - 1].AddExtraFrame(new Vector2(14f, 22f), ColourData.Z_FrameDarkBrown);
          _alienEntries[_alienEntries.Count - 1].AddStringBelow_NEW("$??", ColourData.Z_Cream, this.BaseScale, AssetContainer.SpringFontX1AndHalf);
        }
      }
      this.collection.AddAndPositionEntries(_alienEntries, this.BaseScale, true);
      this.collection.location -= this.collection.GetOffsetFromTopLeft();
      this.size = new Vector2(this.collection.GetWidth(), this.collection.GetHeight());
    }

    public Vector2 GetSize() => this.size;

    public void AddScroll(float _maxHeightForDisplay) => this.collection.AddScroll(_maxHeightForDisplay);

    public void UpdateProcessingAnimalInfoView(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      int num = (int) this.collection.UpdateCollectionScreenManager(offset, DeltaTime, player, out bool _, out bool _);
      AlienEntry mouseOverEntry = this.collection.GetMouseOverEntry();
      if (mouseOverEntry != null)
        this.OnMouseOver(mouseOverEntry.anaimaltype, mouseOverEntry.vLocation - this.collection.GetOffsetFromTopLeft() + new Vector2(0.0f, this.collection.scrollHelper.YscrollOffset), mouseOverEntry.GetSize(), mouseOverEntry.GetOffsetFromCenter(), offset, player, mouseOverEntry.cropType, mouseOverEntry.animalFoodType, mouseOverEntry.IsUnlocked);
      else
        this.mouseOverFrame = (AnimalMeatMouseOver) null;
    }

    private void OnMouseOver(
      AnimalType animalType,
      Vector2 entryLocation,
      Vector2 entrySize,
      Vector2 entryFrameOffset,
      Vector2 offset,
      Player player,
      CROPTYPE cropType,
      AnimalFoodType animalFoodType,
      bool IsDiscovered)
    {
      if (this.mouseOverFrame != null && this.mouseOverFrame.refAnimalType == animalType && (this.mouseOverFrame.refCropType == cropType && this.mouseOverFrame.refFoodType == animalFoodType))
        return;
      this.mouseOverFrame = new AnimalMeatMouseOver(animalType, player, this.BaseScale, cropType, animalFoodType: animalFoodType, HasDiscovered: IsDiscovered);
      this.mouseOverFrame.location = entryLocation;
      this.mouseOverFrame.location.Y -= this.mouseOverFrame.GetSize().Y * 0.5f;
      this.mouseOverFrame.location.Y -= entrySize.Y * 0.5f;
      this.mouseOverFrame.location.Y += entryFrameOffset.Y;
      float num = (float) ((double) this.mouseOverFrame.location.X + (double) offset.X + (double) this.mouseOverFrame.GetSize().X * 0.5 - 1024.0);
      if ((double) num > 0.0)
        this.mouseOverFrame.location.X -= num;
      this.testPoint = new GameObject();
      this.testPoint.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.testPoint.SetAllColours(Color.Cyan.ToVector3());
      this.testPoint.vLocation = this.mouseOverFrame.location;
      this.testPoint.SetDrawOriginToCentre();
      this.testPoint.scale = 5f;
    }

    public void DrawProcessingAnimalInfoView(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.collection.DrawCollectionScreenManager(offset, spriteBatch);
    }

    public void PostDrawProcessingAnimalInfoView(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.mouseOverFrame == null)
        return;
      this.mouseOverFrame.DrawAnimalMeatMouseOver(offset, spriteBatch);
    }
  }
}
