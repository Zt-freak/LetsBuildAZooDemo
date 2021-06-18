// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ActiveProcess.WaitingQueueSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.PlayerDir.Incinerator;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Processing;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_StoreRoom.Ani_MAll.ShopThing;
using TinyZoo.Z_StoreRoom.Shelves;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ActiveProcess
{
  internal class WaitingQueueSummary
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private SimpleTextHandler header;
    private AnimalFoodIcon productIcon;
    private AnimalInFrame deadAnimalIcon;
    private StockNumber number;
    private bool HasStock;
    private int stockNumber;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    private Vector2 size;
    private Vector2 iconSize;

    public WaitingQueueSummary(float _BaseScale, float forcedHeight, TILETYPE tileType)
    {
      this.BaseScale = _BaseScale;
      this.stockNumber = -1;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      Vector2 defaultBuffer = this.scaleHelper.DefaultBuffer;
      this.size.Y += defaultBuffer.Y;
      this.header = new SimpleTextHandler("Raw~Materials", this.scaleHelper.ScaleX(90f), true, this.BaseScale, true, true);
      this.header.Location.Y = this.size.Y;
      this.header.Location.Y += this.header.GetHeightOfOneLine() * 0.5f;
      this.header.SetAllColours(ColourData.Z_Cream);
      this.size.Y += this.header.GetSize().Y;
      this.size.Y += defaultBuffer.Y;
      this.size.Y += this.scaleHelper.ScaleY(5f);
      this.number = new StockNumber(this.BaseScale);
      this.iconSize = this.scaleHelper.ScaleVector2(new Vector2(35f, 35f));
      AnimalFoodType productInputForFactory = PcessedMeatData.GetProductInputForFactory(tileType);
      if (productInputForFactory != AnimalFoodType.Count)
      {
        this.productIcon = new AnimalFoodIcon(productInputForFactory, 1f, this.BaseScale);
        this.productIcon.vLocation.Y = this.size.Y;
        this.productIcon.vLocation.Y += this.productIcon.GetSize_IconOnly().Y * 0.5f;
        this.number.Location = this.productIcon.vLocation + this.productIcon.GetSize_IconOnly() * 0.5f;
      }
      else
      {
        if (TileData.IsAVegetableProcessingPlant(tileType))
        {
          this.deadAnimalIcon = new AnimalInFrame(AnimalType.None, AnimalType.None, TargetSize: this.iconSize.X, FrameEdgeBuffer: (6f * this.BaseScale), BaseScale: this.BaseScale, HeadVariant: 0, croptype: CROPTYPE.Corn, DrawGrownPlant: true);
        }
        else
        {
          this.deadAnimalIcon = new AnimalInFrame(AnimalType.Walrus, AnimalType.None, TargetSize: this.iconSize.X, FrameEdgeBuffer: (6f * this.BaseScale), BaseScale: this.BaseScale);
          this.deadAnimalIcon.SetDead(AnimalType.Walrus, 0);
        }
        this.deadAnimalIcon.Location.Y = this.size.Y;
        this.deadAnimalIcon.Location.Y += this.deadAnimalIcon.GetSize().Y * 0.5f;
        this.deadAnimalIcon.Location.Y -= this.scaleHelper.ScaleY(10f);
        this.number.Location = this.deadAnimalIcon.Location + this.deadAnimalIcon.GetSize() * 0.5f;
      }
      if (this.productIcon != null || this.deadAnimalIcon != null)
        this.number.Location.X += this.scaleHelper.ScaleX(5f);
      this.SetQueueCount(0, true);
      this.customerFrame = new CustomerFrame(new Vector2(this.scaleHelper.ScaleX(90f), forcedHeight), CustomerFrameColors.DarkBrown, this.BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.header.Location.Y += vector2.Y;
      if (this.productIcon != null)
        this.productIcon.vLocation.Y += vector2.Y;
      if (this.deadAnimalIcon != null)
        this.deadAnimalIcon.Location.Y += vector2.Y;
      this.number.Location.Y += vector2.Y;
    }

    public void SetQueueCount(int value, bool ForceSet = false, List<DeadAnimal> deadAnimal = null)
    {
      if (this.stockNumber == value)
        return;
      if (ForceSet)
        this.number.ForceSetValue(value);
      else
        this.number.AddValue(value - this.stockNumber);
      this.stockNumber = value;
      bool flag = value > 0;
      if (flag != this.HasStock | ForceSet)
      {
        this.HasStock = flag;
        if (this.productIcon != null)
          this.productIcon.SetIsInActive(this.HasStock);
      }
      if (deadAnimal == null || deadAnimal.Count <= 1)
        return;
      if (deadAnimal[1].animalType != AnimalType.None)
      {
        this.deadAnimalIcon.SetDead(deadAnimal[1].animalType, deadAnimal[1].variant);
      }
      else
      {
        Vector2 location = this.deadAnimalIcon.Location;
        this.deadAnimalIcon = new AnimalInFrame(AnimalType.None, AnimalType.None, TargetSize: this.iconSize.X, FrameEdgeBuffer: (6f * this.BaseScale), BaseScale: this.BaseScale, HeadVariant: 0, croptype: deadAnimal[1].cropType);
        this.deadAnimalIcon.Location = location;
      }
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateWaitingQueueSummary(float DeltaTime) => this.number.UpdateStockNumber(DeltaTime);

    public void DrawWaitingQueueSummary(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.header.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      if (this.productIcon != null)
        this.productIcon.DrawAnimalFoodIcon(offset, false, spriteBatch);
      if (this.deadAnimalIcon != null)
        this.deadAnimalIcon.JustDrawAnimal(offset, spriteBatch);
      if (this.number == null)
        return;
      this.number.DrawStockNumber(offset, spriteBatch);
    }
  }
}
