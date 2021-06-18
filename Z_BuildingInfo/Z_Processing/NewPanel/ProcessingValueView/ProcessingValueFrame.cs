// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.ProcessingValueFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir._Factories;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.AnimalView;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView
{
  internal class ProcessingValueFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private MiniHeading miniHeading;
    private ProduceCategorySelector catSelect;
    private ProcessingByCategoryView view;
    private ProcessingViewType currentViewType;
    private RowSegmentRectangle hackyScrollFrame_Top;
    private RowSegmentRectangle hackyScrollFrame_Bottom;
    private SimpleTextHandler desc;
    private float BaseScale;
    private Vector2 buffer;
    private FactoryProductionLine factoryproduction;
    private bool IsCrops;
    private bool IsWarehouse;

    public ProcessingValueFrame(
      int BuildingUID,
      TILETYPE tileType,
      Player player,
      float _BaseScale,
      float ForcedHeight = -1f)
    {
      this.BaseScale = _BaseScale;
      ShopEntry thisFacility = player.shopstatus.GetThisFacility(BuildingUID);
      this.factoryproduction = thisFacility.factoryproduction;
      if (TileData.IsAVegetableProcessingPlant(thisFacility.tiletype))
        this.IsCrops = true;
      else if (TileData.IsAWarehouse(thisFacility.tiletype))
        this.IsWarehouse = true;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      this.buffer = uiScaleHelper.DefaultBuffer;
      float num = uiScaleHelper.ScaleY(57f);
      ForcedHeight -= num - this.buffer.Y;
      this.currentViewType = ProcessingViewType.Count;
      this.customerFrame = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, this.BaseScale);
      this.miniHeading = new MiniHeading(Vector2.Zero, "BLAH", 1f, this.BaseScale);
      List<ProcessingViewType> _buttonTypes = new List<ProcessingViewType>();
      if (this.IsWarehouse)
      {
        _buttonTypes.Add(ProcessingViewType.WarehouseStock);
        _buttonTypes.Add(ProcessingViewType.WarehouseMarket);
      }
      else
      {
        _buttonTypes.Add(ProcessingViewType.Queue);
        _buttonTypes.Add(ProcessingViewType.AnimalsOrCrops);
        _buttonTypes.Add(ProcessingViewType.Products);
        _buttonTypes.Add(ProcessingViewType.KeepSell);
      }
      this.catSelect = new ProduceCategorySelector(this.BaseScale, _buttonTypes, this.IsCrops);
      Vector2 zero = Vector2.Zero;
      zero.X = this.buffer.X;
      zero.Y += this.buffer.Y;
      zero.Y += this.miniHeading.GetTextHeight(true);
      zero.Y += this.buffer.Y;
      this.catSelect.location = zero;
      this.catSelect.location.Y += this.catSelect.GetSize().Y * 0.5f;
      zero.Y += this.catSelect.GetSize().Y;
      zero.Y += this.buffer.Y;
      this.view = new ProcessingByCategoryView(this.factoryproduction, player, this.BaseScale, this.IsCrops, num, ForcedHeight - zero.Y - this.buffer.Y, this.IsWarehouse);
      this.view.location.Y = zero.Y;
      this.view.location.Y += this.view.GetSize().Y * 0.5f;
      zero += this.view.GetSize();
      zero.Y += num;
      this.customerFrame.Resize(zero);
      this.miniHeading.SetTextPosition(zero);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.catSelect.location += vector2;
      this.view.location.Y += vector2.Y;
      this.hackyScrollFrame_Top = new RowSegmentRectangle(zero.X, num, ColourData.Z_FrameMidBrown, 1f);
      this.hackyScrollFrame_Top.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.hackyScrollFrame_Top.vLocation = this.view.location - new Vector2(0.0f, this.view.GetSize().Y * 0.5f);
      this.hackyScrollFrame_Bottom = new RowSegmentRectangle(zero.X, num, ColourData.Z_FrameMidBrown, 1f);
      this.hackyScrollFrame_Bottom.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.hackyScrollFrame_Bottom.vLocation = this.view.location + new Vector2(0.0f, this.view.GetSize().Y * 0.5f);
      this.OnClickCategory(_buttonTypes[0], player);
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateProcessingValueFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      ProcessingViewType viewType = this.catSelect.UpdateProduceCategorySelector(player, DeltaTime, offset);
      if (viewType != ProcessingViewType.Count)
        this.OnClickCategory(viewType, player);
      this.view.UpdateProcessingByCategoryViewFrame(player, DeltaTime, offset);
    }

    private void OnClickCategory(ProcessingViewType viewType, Player player)
    {
      if (this.currentViewType == viewType)
        return;
      this.view.SetCategory(viewType, player);
      this.currentViewType = viewType;
      string TextToWrite = string.Empty;
      string text = string.Empty;
      switch (this.currentViewType)
      {
        case ProcessingViewType.Queue:
          text = "Building Stock";
          break;
        case ProcessingViewType.AnimalsOrCrops:
          text = !this.IsCrops ? "Animal Value" : "Crops Value";
          TextToWrite = "Value shown is how much it would cost to purchase these items.";
          break;
        case ProcessingViewType.Products:
          text = "Products by Type";
          break;
        case ProcessingViewType.KeepSell:
          TextToWrite = "Select how many of each produce you would like to keep in your store room. Excess will then be sent to your warehouse.";
          text = "Store Room Settings";
          break;
        case ProcessingViewType.WarehouseMarket:
          text = "Current Sell Cost";
          break;
        case ProcessingViewType.WarehouseStock:
          text = "Warehouse Stock";
          TextToWrite = "Total Items In Stock: " + (object) ProcessingAnimalInfoView.TotalWarehouseStockCount;
          break;
      }
      this.desc = new SimpleTextHandler(TextToWrite, this.customerFrame.VSCale.X - this.buffer.X * 2f, _Scale: this.BaseScale, AutoComplete: true);
      this.desc.SetAllColours(ColourData.Z_Cream);
      this.desc.Location.Y = (double) this.view.GetContentsSize().Y >= (double) this.view.GetSize().Y ? (float) ((double) this.view.location.Y + (double) this.view.GetSize().Y * 0.5 + (double) this.hackyScrollFrame_Bottom.GetSize().Y * 0.5) : (float) ((double) this.view.location.Y - (double) this.view.GetSize().Y * 0.5 + (double) this.view.GetContentsSize().Y + (double) this.buffer.Y * 2.0);
      this.desc.Location.X = (float) (-(double) this.customerFrame.VSCale.X * 0.5) + this.buffer.X;
      this.desc.Location.Y -= this.desc.GetHeightOfParagraph() * 0.5f;
      this.miniHeading.SetNewText(text);
    }

    public void DrawProcessingValueFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.view.DrawProcessingByCategoryViewFrame(offset, spriteBatch);
      this.hackyScrollFrame_Top.DrawRowSegmentRectangle(offset, spriteBatch);
      this.hackyScrollFrame_Bottom.DrawRowSegmentRectangle(offset, spriteBatch);
      this.catSelect.DrawProduceCategorySelector(offset, spriteBatch);
      this.miniHeading.DrawMiniHeading(offset, spriteBatch);
      this.view.PostDrawProcessingByCategoryViewFrame(offset, spriteBatch);
      if (this.desc == null)
        return;
      this.desc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
