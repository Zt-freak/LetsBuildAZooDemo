// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.ProcessingByCategoryView
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir._Factories;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.AnimalView;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.KeepSellView;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.MeatView;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.QueueView;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.WarehouseMarketView;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.WarehouseStockView;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView
{
  internal class ProcessingByCategoryView
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private ProcessingAnimalInfoView animalView;
    private ProcessingMeatInfoView meatView;
    private KeepSellEditView keepSellView;
    private DeadAnimalQueueView queueView;
    private WarehouseMarketViewFrame warehouseMarketView;
    private WarehouseStockViewFrame warehouseStockView;
    private float BaseScale;
    private ProcessingViewType currentViewType;
    private FactoryProductionLine reffactoryproduction;
    private bool IsCrops;
    private float refMaskHeight;

    public ProcessingByCategoryView(
      FactoryProductionLine factoryproduction,
      Player player,
      float _BaseScale,
      bool _IsCrops,
      float maskHeight,
      float ForcedHeight = -1f,
      bool IsWarehouse = false)
    {
      this.BaseScale = _BaseScale;
      this.reffactoryproduction = factoryproduction;
      this.IsCrops = _IsCrops;
      this.refMaskHeight = maskHeight;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      Vector2 zero = Vector2.Zero;
      zero.Y = uiScaleHelper.ScaleY(150f);
      if ((double) ForcedHeight != -1.0)
        zero.Y = ForcedHeight;
      if (IsWarehouse)
      {
        this.warehouseStockView = new WarehouseStockViewFrame(player, this.BaseScale);
        zero.X += this.warehouseStockView.GetSize().X;
      }
      else
      {
        this.animalView = new ProcessingAnimalInfoView(player, this.BaseScale, this.IsCrops);
        zero.X = this.animalView.GetSize().X;
      }
      this.customerFrame = new CustomerFrame(zero, CustomerFrameColors.DarkBrown, this.BaseScale);
      Vector2 vector2 = -zero * 0.5f;
      if (this.animalView != null)
      {
        this.animalView.location += vector2;
        this.animalView.AddScroll(zero.Y);
      }
      else
      {
        if (this.warehouseStockView == null)
          return;
        this.warehouseStockView.location += vector2;
        this.warehouseStockView.AddScroll(zero.Y);
      }
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public Vector2 GetContentsSize()
    {
      switch (this.currentViewType)
      {
        case ProcessingViewType.Queue:
          return this.queueView.GetSize();
        case ProcessingViewType.AnimalsOrCrops:
          return this.animalView.GetSize();
        case ProcessingViewType.Products:
          return this.meatView.GetSize();
        case ProcessingViewType.KeepSell:
          return this.keepSellView.GetSize();
        case ProcessingViewType.WarehouseMarket:
          return this.warehouseMarketView.GetSize();
        case ProcessingViewType.WarehouseStock:
          return this.warehouseStockView.GetSize();
        default:
          return Vector2.Zero;
      }
    }

    public void SetCategory(ProcessingViewType viewType, Player player)
    {
      this.currentViewType = viewType;
      switch (viewType)
      {
        case ProcessingViewType.Queue:
          if (this.queueView != null)
            break;
          this.queueView = new DeadAnimalQueueView(this.reffactoryproduction, this.BaseScale, this.customerFrame.VSCale.X, this.customerFrame.VSCale.Y);
          this.queueView.location -= this.customerFrame.VSCale * 0.5f;
          this.queueView.location += this.queueView.GetSize() * 0.5f;
          break;
        case ProcessingViewType.AnimalsOrCrops:
          if (this.animalView != null)
            break;
          this.animalView = new ProcessingAnimalInfoView(player, this.BaseScale, this.IsCrops);
          this.animalView.location.Y -= this.customerFrame.VSCale.Y * 0.5f;
          this.animalView.AddScroll(this.customerFrame.VSCale.Y);
          break;
        case ProcessingViewType.Products:
          if (this.meatView != null)
            break;
          this.meatView = new ProcessingMeatInfoView(player, this.BaseScale, this.customerFrame.VSCale.X, this.IsCrops);
          this.meatView.location.Y -= this.customerFrame.VSCale.Y * 0.5f;
          this.meatView.AddScroll(this.customerFrame.VSCale.Y);
          break;
        case ProcessingViewType.KeepSell:
          if (this.keepSellView != null)
            break;
          this.keepSellView = new KeepSellEditView(!this.IsCrops, player, this.BaseScale, this.customerFrame.VSCale.X);
          this.keepSellView.location -= this.customerFrame.VSCale * 0.5f;
          this.keepSellView.AddScroll(this.customerFrame.VSCale.Y);
          break;
        case ProcessingViewType.WarehouseMarket:
          if (this.warehouseMarketView != null)
            break;
          this.warehouseMarketView = new WarehouseMarketViewFrame(player, this.BaseScale);
          this.warehouseMarketView.location -= this.customerFrame.VSCale * 0.5f;
          this.warehouseMarketView.AddScroll(this.customerFrame.VSCale.Y);
          break;
        case ProcessingViewType.WarehouseStock:
          if (this.warehouseStockView != null)
            break;
          this.warehouseStockView = new WarehouseStockViewFrame(player, this.BaseScale);
          this.warehouseStockView.location -= this.customerFrame.VSCale * 0.5f;
          this.warehouseStockView.AddScroll(this.customerFrame.VSCale.Y);
          break;
      }
    }

    public void UpdateProcessingByCategoryViewFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.currentViewType == ProcessingViewType.AnimalsOrCrops)
        this.animalView.UpdateProcessingAnimalInfoView(player, DeltaTime, offset);
      else if (this.currentViewType == ProcessingViewType.Products)
        this.meatView.UpdateProcessingMeatInfoView(player, offset);
      else if (this.currentViewType == ProcessingViewType.KeepSell)
        this.keepSellView.UpdateKeepSellEditView(player, DeltaTime, offset);
      else if (this.currentViewType == ProcessingViewType.Queue)
        this.queueView.UpdateDeadAnimalQueueView(player, offset);
      else if (this.currentViewType == ProcessingViewType.WarehouseMarket)
      {
        this.warehouseMarketView.UpdateWarehouseMarketViewFrame(player, DeltaTime, offset);
      }
      else
      {
        if (this.currentViewType != ProcessingViewType.WarehouseStock)
          return;
        this.warehouseStockView.UpdateWarehouseStockViewFrame(player, DeltaTime, offset);
      }
    }

    public void DrawProcessingByCategoryViewFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.currentViewType == ProcessingViewType.AnimalsOrCrops)
        this.animalView.DrawProcessingAnimalInfoView(offset, spriteBatch);
      else if (this.currentViewType == ProcessingViewType.Products)
        this.meatView.DrawProcessingMeatInfoView(offset, spriteBatch);
      else if (this.currentViewType == ProcessingViewType.KeepSell)
        this.keepSellView.DrawKeepSellEditView(offset, spriteBatch);
      else if (this.currentViewType == ProcessingViewType.Queue)
        this.queueView.DrawDeadAnimalQueueView(offset, spriteBatch);
      else if (this.currentViewType == ProcessingViewType.WarehouseMarket)
      {
        this.warehouseMarketView.DrawWarehouseMarketViewFrame(offset, spriteBatch);
      }
      else
      {
        if (this.currentViewType != ProcessingViewType.WarehouseStock)
          return;
        this.warehouseStockView.DrawWarehouseStockViewFrame(offset, spriteBatch);
      }
    }

    public void PostDrawProcessingByCategoryViewFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.currentViewType == ProcessingViewType.AnimalsOrCrops)
        this.animalView.PostDrawProcessingAnimalInfoView(offset, spriteBatch);
      else if (this.currentViewType == ProcessingViewType.WarehouseStock)
      {
        this.warehouseStockView.PostDrawWarehouseStockViewFrame(offset, spriteBatch);
      }
      else
      {
        if (this.currentViewType != ProcessingViewType.WarehouseMarket)
          return;
        this.warehouseMarketView.PostDrawWarehouseMarketViewFrame(offset, spriteBatch);
      }
    }
  }
}
