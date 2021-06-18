// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ActiveProcess.EndProduct
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir._Factories;
using TinyZoo.PlayerDir.Incinerator;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuildingInfo.Z_Processing.Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Processing;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_StoreRoom.Shelves;
using TinyZoo.Z_SummaryPopUps.People;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ActiveProcess
{
  internal class EndProduct
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private SimpleTextHandler header;
    private List<AnimalFoodIcon> foodIcon;
    private LittleSummaryButton infoIcon;
    private ZGenericText stockText;
    private SimpleTextHandler avgTimeText;
    private SimpleTextHandler stockExplanation;
    private float BaseScale;
    private Vector2 size;
    private UIScaleHelper scaleHelper;
    private Vector2 buffer;
    private bool ViewStockExplanation;
    private FactoryProductionLine reffactoryproduction;
    private TILETYPE refTileType;

    public EndProduct(
      float _BaseScale,
      float forcedHeight,
      TILETYPE tileType,
      FactoryProductionLine factoryproduction,
      Player player)
    {
      this.reffactoryproduction = factoryproduction;
      this.refTileType = tileType;
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.buffer = this.scaleHelper.DefaultBuffer;
      this.size.Y += this.buffer.Y;
      float num1 = this.scaleHelper.ScaleX(100f);
      string TextToWrite = "";
      List<AnimalFoodType> animalFoodTypeList1 = new List<AnimalFoodType>();
      List<AnimalFoodType> animalFoodTypeList2 = PcessedMeatData.GetProductOutputFromFactory(tileType);
      if (animalFoodTypeList2.Count > 0)
      {
        for (int index = 0; index < animalFoodTypeList2.Count; ++index)
        {
          if (index > 0)
            TextToWrite += "/";
          TextToWrite += AnimalFoodData.GetAnimalFoodTypeToString(animalFoodTypeList2[index]);
        }
      }
      else if (TileData.IsAnIncinerator(tileType))
      {
        TextToWrite = "Disposal";
      }
      else
      {
        if (!TileData.IsAMeatProcessingPlant(tileType) && !TileData.IsAVegetableProcessingPlant(tileType))
          throw new Exception("not handled");
        TextToWrite = "Product";
        DeadAnimal manufacturingDeadAnimal = factoryproduction.GetCurrentlyManufacturingDeadAnimal();
        if (manufacturingDeadAnimal != null)
        {
          animalFoodTypeList2 = !TileData.IsAVegetableProcessingPlant(tileType) ? ((IEnumerable<AnimalFoodType>) PcessedMeatData.GetAnmalToBaseMeatType(manufacturingDeadAnimal.animalType).animalfoodtypes).ToList<AnimalFoodType>() : ((IEnumerable<AnimalFoodType>) ProcessedVeg.GetVegetableToOutput(manufacturingDeadAnimal.cropType).animalfoodtypes).ToList<AnimalFoodType>();
          for (int index = animalFoodTypeList2.Count - 1; index > -1; --index)
          {
            if (!PcessedMeatData.CanProcessThisProduce(animalFoodTypeList2[index], player))
              animalFoodTypeList2.RemoveAt(index);
          }
        }
      }
      this.header = new SimpleTextHandler(TextToWrite, num1, true, this.BaseScale, true, true);
      this.header.SetAllColours(ColourData.Z_Cream);
      this.header.Location.Y = this.size.Y;
      this.header.Location.Y += this.header.GetSize().Y * 0.5f;
      this.size.Y += this.header.GetSize().Y + this.buffer.Y;
      float num2 = (num1 - this.buffer.X * 4f) / (float) animalFoodTypeList2.Count;
      this.foodIcon = new List<AnimalFoodIcon>();
      for (int index = 0; index < animalFoodTypeList2.Count; ++index)
      {
        if (index == 0)
          this.size.Y += this.scaleHelper.ScaleY(13f);
        AnimalFoodIcon animalFoodIcon = new AnimalFoodIcon(animalFoodTypeList2[index], 1f, this.BaseScale);
        animalFoodIcon.vLocation.Y = this.size.Y;
        animalFoodIcon.vLocation.X += (float) index * num2;
        animalFoodIcon.vLocation.X -= (float) ((double) num2 * (double) (animalFoodTypeList2.Count - 1) * 0.5);
        if (index == animalFoodTypeList2.Count - 1)
          this.size.Y += this.scaleHelper.ScaleY(13f);
        this.foodIcon.Add(animalFoodIcon);
      }
      if (TileData.IsAFactory(tileType))
      {
        this.size.Y += this.buffer.Y * 0.5f;
        this.stockText = new ZGenericText("Stock: XX", this.BaseScale, _UseOnePointFiveFont: true);
        this.stockText.vLocation.Y = this.size.Y;
        this.stockText.vLocation.Y += this.stockText.GetSize().Y * 0.5f;
        this.size.Y += this.stockText.GetSize().Y;
        this.infoIcon = new LittleSummaryButton(LittleSummaryButtonType.BlueInfoCircle, _BaseScale: this.BaseScale);
        this.infoIcon.vLocation.Y = this.foodIcon[0].vLocation.Y - this.foodIcon[0].GetSize_IconOnly().Y * 0.5f;
        this.infoIcon.vLocation.X = this.scaleHelper.ScaleX(84f);
      }
      this.avgTimeText = new SimpleTextHandler("Avg Processing Time: XXX", num1 - this.buffer.X, true, this.BaseScale, AutoComplete: true);
      if (animalFoodTypeList2.Count == 0)
        this.size.Y += (float) (((double) forcedHeight - (double) this.size.Y) * 0.5 - (double) this.avgTimeText.GetHeightOfParagraph() - (double) this.avgTimeText.GetHeightOfOneLine() * 0.5);
      this.size.Y += this.buffer.Y * 0.5f;
      this.avgTimeText.SetAllColours(ColourData.Z_Cream);
      this.avgTimeText.Location.Y = this.size.Y;
      this.avgTimeText.Location.Y += this.avgTimeText.GetHeightOfOneLine() * 0.5f;
      this.customerFrame = new CustomerFrame(new Vector2(num1, forcedHeight), CustomerFrameColors.DarkBrown, this.BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.header.Location.Y += vector2.Y;
      if (this.stockText != null)
        this.stockText.vLocation.Y += vector2.Y;
      for (int index = 0; index < this.foodIcon.Count; ++index)
        this.foodIcon[index].vLocation.Y += vector2.Y;
      if (this.infoIcon != null)
      {
        LittleSummaryButton infoIcon = this.infoIcon;
        infoIcon.vLocation = infoIcon.vLocation + vector2;
      }
      this.avgTimeText.Location.Y += vector2.Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateEndProduct(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.stockText != null)
      {
        int completedProductCount = this.reffactoryproduction.GetCompletedProductCount();
        for (int index = 1; index < this.foodIcon.Count; ++index)
        {
          if (this.reffactoryproduction.additionfactoryproducts[(int) this.foodIcon[index].refFoodType] != null)
            completedProductCount += (int) Math.Floor((double) this.reffactoryproduction.additionfactoryproducts[(int) this.foodIcon[index].refFoodType].TotalCompletedProductsHeld);
        }
        this.stockText.textToWrite = "Stock: " + (object) completedProductCount;
      }
      if (this.infoIcon != null && this.infoIcon.UpdateLittleSummaryButton(DeltaTime, player, offset))
      {
        this.ViewStockExplanation = !this.ViewStockExplanation;
        if (this.ViewStockExplanation)
        {
          this.stockExplanation = new SimpleTextHandler(string.Format("Stock: ~You currently have {0} items waiting to be collected.", (object) this.reffactoryproduction.GetCompletedProductCount()), this.customerFrame.VSCale.X - this.buffer.X, true, this.BaseScale, AutoComplete: true);
          this.stockExplanation.SetAllColours(ColourData.Z_Cream);
        }
      }
      return false;
    }

    public void DrawEndProduct(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      if (this.header != null)
        this.header.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      if (this.infoIcon != null)
        this.infoIcon.DrawLittleSummaryButton(offset, spriteBatch);
      if (this.ViewStockExplanation)
      {
        this.stockExplanation.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      }
      else
      {
        for (int index = this.foodIcon.Count - 1; index > -1; --index)
          this.foodIcon[index].DrawAnimalFoodIcon(offset, false, spriteBatch);
        if (this.avgTimeText != null)
          this.avgTimeText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
        if (this.stockText == null)
          return;
        this.stockText.DrawZGenericText(offset, spriteBatch);
      }
    }
  }
}
