// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ActiveProcess.CurrentlyMaking
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir._Factories;
using TinyZoo.PlayerDir.Incinerator;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuildingInfo.IncineratorBuildingInfo;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Processing;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_StoreRoom.Shelves;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ActiveProcess
{
  internal class CurrentlyMaking
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private SimpleTextHandler header;
    private AnimalFoodIcon startProduct;
    private AnimalInFrame startAnimalProduct;
    private AnimalFoodIcon finalProduct;
    private SatisfactionBar bar;
    private ZGenericText timeLeftTop;
    private ZGenericText timeLeftBottom;
    private ZGenericText pausedText;
    private float BaseScale;
    private Vector2 size;
    private Vector2 buffer;
    private UIScaleHelper scaleHelper;
    private bool IsManufacturing;
    private bool IsPaused;
    private FactoryProductionLine reffactoryproduction;
    private TILETYPE reftileType;
    private DeadAnimal current;
    private FireIcon fireIcon;

    public CurrentlyMaking(
      float _BaseScale,
      float forcedHeight,
      TILETYPE tileType,
      FactoryProductionLine factoryproduction)
    {
      this.BaseScale = _BaseScale;
      this.reffactoryproduction = factoryproduction;
      this.reftileType = tileType;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.buffer = this.scaleHelper.DefaultBuffer;
      this.customerFrame = new CustomerFrame(new Vector2(this.scaleHelper.ScaleX(110f), forcedHeight), CustomerFrameColors.DarkBrown, this.BaseScale);
      this.SetProgressAndTime(true);
    }

    private void SetUp()
    {
      this.startProduct = (AnimalFoodIcon) null;
      this.startAnimalProduct = (AnimalInFrame) null;
      this.finalProduct = (AnimalFoodIcon) null;
      this.size = Vector2.Zero;
      this.size.Y += this.buffer.Y;
      string TextToWrite = "Currently~Making";
      if (TileData.IsAnIncinerator(this.reftileType))
        TextToWrite = "Currently~Burning";
      this.header = new SimpleTextHandler(TextToWrite, this.scaleHelper.ScaleX(90f), true, this.BaseScale, true, true);
      this.header.Location.Y = this.size.Y;
      this.header.Location.Y += this.header.GetHeightOfOneLine() * 0.5f;
      this.header.SetAllColours(ColourData.Z_Cream);
      this.size.Y += this.header.GetSize().Y;
      this.size.Y += this.buffer.Y;
      this.bar = new SatisfactionBar(1f, this.BaseScale);
      this.size.Y += this.scaleHelper.ScaleY(17f);
      this.bar.vLocation.Y = this.size.Y;
      this.bar.vLocation.Y += this.bar.GetSize().Y * 0.5f;
      if (!this.IsManufacturing)
        this.bar.Darken();
      float x1 = this.bar.vLocation.X - this.bar.GetSize().X * 0.5f;
      float x2 = this.bar.vLocation.X + this.bar.GetSize().X * 0.5f;
      float y = this.bar.vLocation.Y - this.bar.GetSize().Y * 0.5f;
      this.current = this.reffactoryproduction.GetCurrentlyManufacturingDeadAnimal();
      AnimalFoodType productInputForFactory = PcessedMeatData.GetProductInputForFactory(this.reftileType);
      List<AnimalFoodType> outputFromFactory = PcessedMeatData.GetProductOutputFromFactory(this.reftileType);
      if (TileData.IsAMeatProcessingPlant(this.reftileType) && this.current != null)
      {
        AnimalFoodType[] animalfoodtypes = PcessedMeatData.GetAnmalToBaseMeatType(this.current.animalType).animalfoodtypes;
        outputFromFactory.Add(animalfoodtypes[0]);
      }
      if (productInputForFactory != AnimalFoodType.Count)
      {
        this.startProduct = new AnimalFoodIcon(productInputForFactory, 1f, this.BaseScale * 0.5f);
        this.startProduct.vLocation = new Vector2(x1, y);
        this.startProduct.vLocation.Y -= this.startProduct.GetSize_IconOnly().Y * 0.5f;
        this.startProduct.vLocation.Y -= this.buffer.Y;
        if (!this.IsManufacturing)
          this.startProduct.SetIsInActive(false);
      }
      else if (this.current != null)
      {
        this.startAnimalProduct = new AnimalInFrame(this.current.animalType, this.current.headType, this.current.variant, 25f * this.BaseScale, 6f * this.BaseScale, this.BaseScale, this.current.headVariant, this.current.cropType, true);
        this.startAnimalProduct.Location = new Vector2(x1, y);
        this.startAnimalProduct.Location.Y -= this.buffer.Y * 1.5f;
        if (!this.IsManufacturing)
          this.startAnimalProduct.Darken(true);
      }
      if (outputFromFactory.Count > 0)
      {
        this.finalProduct = new AnimalFoodIcon(outputFromFactory[0], 1f, this.BaseScale * 0.5f);
        this.finalProduct.vLocation = new Vector2(x2, y);
        this.finalProduct.vLocation.Y -= this.finalProduct.GetSize_IconOnly().Y * 0.5f;
        this.finalProduct.vLocation.Y -= this.buffer.Y;
        if (!this.IsManufacturing)
          this.finalProduct.SetIsInActive(false);
      }
      this.size.Y += this.bar.GetSize().Y;
      this.size.Y += this.buffer.Y * 0.5f;
      this.pausedText = new ZGenericText("PAUSED", this.BaseScale);
      this.pausedText.vLocation.Y = this.size.Y;
      this.pausedText.vLocation.Y += this.pausedText.GetSize().Y * 0.5f;
      this.fireIcon = new FireIcon(this.BaseScale);
      this.fireIcon.vLocation.Y = this.size.Y;
      this.fireIcon.vLocation.Y += this.fireIcon.GetSize().Y * 0.5f;
      this.fireIcon.vLocation.Y -= this.fireIcon.GetSize().Y - this.pausedText.GetSize().Y;
      this.size.Y += this.pausedText.GetSize().Y;
      this.timeLeftTop = new ZGenericText("Time Remaining:", this.BaseScale);
      this.timeLeftTop.vLocation.Y = this.size.Y;
      this.timeLeftTop.vLocation.Y += this.timeLeftTop.GetSize().Y * 0.5f;
      this.size.Y += this.timeLeftTop.GetSize().Y;
      if (!this.IsManufacturing && !this.IsPaused)
        this.timeLeftTop.textToWrite = "";
      this.timeLeftBottom = new ZGenericText("XXX", this.BaseScale);
      this.timeLeftBottom.vLocation.Y = this.size.Y;
      this.timeLeftBottom.vLocation.Y += this.timeLeftBottom.GetSize().Y * 0.5f;
      this.size.Y += this.timeLeftBottom.GetSize().Y;
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.header.Location.Y += vector2.Y;
      this.bar.vLocation.Y += vector2.Y;
      this.pausedText.vLocation.Y += vector2.Y;
      this.timeLeftTop.vLocation.Y += vector2.Y;
      this.timeLeftBottom.vLocation.Y += vector2.Y;
      this.fireIcon.vLocation.Y += vector2.Y;
      if (this.startProduct != null)
        this.startProduct.vLocation.Y += vector2.Y;
      if (this.finalProduct != null)
        this.finalProduct.vLocation.Y += vector2.Y;
      if (this.startAnimalProduct != null)
        this.startAnimalProduct.Location.Y += vector2.Y;
      Console.WriteLine("Constructing Currently Making UI");
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    private void SetProgressAndTime(bool isInit = false)
    {
      string TimeLeft;
      float percentComplete = this.reffactoryproduction.GetPercentComplete(out TimeLeft, out this.IsPaused);
      bool flag = this.reffactoryproduction.IsCurrentlyManufacturing();
      if (isInit || this.IsManufacturing != flag)
      {
        this.IsManufacturing = flag;
        this.SetUp();
      }
      this.bar.SetFullness(percentComplete);
      this.timeLeftBottom.textToWrite = TimeLeft;
      if (!this.IsPaused && this.IsManufacturing)
      {
        this.pausedText.textToWrite = "";
        this.fireIcon.Active = true;
      }
      else
      {
        this.pausedText.textToWrite = "PAUSED";
        this.fireIcon.Active = false;
      }
    }

    public void UpdateCurrentlyMaking() => this.SetProgressAndTime();

    public void DrawCurrentlyMaking(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.header.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.pausedText.DrawZGenericText(offset, spriteBatch);
      this.timeLeftTop.DrawZGenericText(offset, spriteBatch);
      this.timeLeftBottom.DrawZGenericText(offset, spriteBatch);
      if (this.startProduct != null)
        this.startProduct.DrawAnimalFoodIcon(offset, false, spriteBatch);
      if (this.finalProduct != null)
        this.finalProduct.DrawAnimalFoodIcon(offset, false, spriteBatch);
      if (this.startAnimalProduct != null)
        this.startAnimalProduct.JustDrawAnimal(offset, spriteBatch);
      this.bar.DrawSatisfactionBar(offset, spriteBatch);
      this.fireIcon.DrawFireIcon(offset, spriteBatch);
    }
  }
}
