// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.StoreRoom.AnimalStuff.QuickOrder.QuickOrderDisplayString
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine.Objects;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_BalanceSystems.FoodDayCalc;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_SummaryPopUps.People;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_HUD.StoreRoom.AnimalStuff.QuickOrder
{
  internal class QuickOrderDisplayString
  {
    public Vector2 location;
    private int LastBasketCount;
    private SimpleTextHandler paragraph_stock;
    private SimpleTextHandler paragraph_foodused;
    private SimpleTextHandler DeliveryTimeAlert;
    private SimpleTextHandler ShelfLifeWarning;
    private SatisfactionBar bar;
    private float InStock;
    private float UsedPerDay;
    private float BarTarget;
    private int ShelfLife;
    private LittleSummaryButton Infor;
    private LittleSummaryButton Infor2;
    private LittleSummaryButton Infor3;
    private float DaysOfStock;
    private float DaysOfStock_barFloat;
    private float DaysOfOnOrder_barFloat;
    private SimpleTextHandler DeliveryTimeInfo;
    private SimpleTextHandler ShelfLifeInfo;
    private bool ShowingDeliveryTime;
    private bool ShowingShelfLifeInfo;

    public QuickOrderDisplayString(TempAnimalInfo animalinfo, float BaseScale, Player player)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.InStock = player.storerooms.GetTotalStockOfThis(animalinfo.CriticalFood);
      int totalOfTheseOnOrder = player.storerooms.GetTotalOfTheseOnOrder(animalinfo.CriticalFood);
      this.paragraph_stock = new SimpleTextHandler("In Stock: " + (object) Math.Round((double) this.InStock, 1) + "~On Order: " + (object) totalOfTheseOnOrder, true, 0.1f * BaseScale, BaseScale, AutoComplete: true);
      this.paragraph_stock.SetAllColours(ColourData.Z_Cream);
      this.paragraph_foodused = new SimpleTextHandler("You are using " + (object) Math.Round((double) FoodDaysRemainingCalculator.FoodByTypeUsedPerDay[(int) animalinfo.CriticalFood], 2) + " units of this per day", true, 0.1f * BaseScale, BaseScale, AutoComplete: true);
      this.paragraph_foodused.SetAllColours(ColourData.Z_Cream);
      this.bar = new SatisfactionBar(0.0f, BaseScale);
      this.bar.AddNewBar(0.0f, ColourData.Z_BarBabyGreen, 2);
      this.bar.AddNewBar(0.0f, ColourData.Z_BarYellow, 1);
      this.bar.SetBarColours(ColourData.LogGreen);
      this.ShelfLife = AnimalFoodData.GetAnimalFoodInfo(animalinfo.CriticalFood).ShelfLife;
      this.UsedPerDay = FoodDaysRemainingCalculator.FoodByTypeUsedPerDay[(int) animalinfo.CriticalFood];
      this.BarTarget = (float) Math.Min(30, this.ShelfLife);
      this.DaysOfStock = this.InStock / this.UsedPerDay;
      this.DaysOfStock_barFloat = this.DaysOfStock / this.BarTarget;
      this.DaysOfOnOrder_barFloat = (float) totalOfTheseOnOrder / this.UsedPerDay / this.BarTarget;
      this.DeliveryTimeAlert = new SimpleTextHandler("Your stocks will run out before this is delivered.", true, 0.1f * BaseScale, BaseScale, AutoComplete: true);
      this.DeliveryTimeAlert.AutoCompleteParagraph();
      this.DeliveryTimeAlert.SetAllColours(ColourData.Z_Cream);
      this.Infor = new LittleSummaryButton(LittleSummaryButtonType.BlueInfoCircle, _BaseScale: BaseScale);
      this.Infor.vLocation = this.DeliveryTimeAlert.Location;
      this.ShelfLifeWarning = new SimpleTextHandler("May expire before used.", true, 0.1f * BaseScale, BaseScale, AutoComplete: true);
      this.ShelfLifeWarning.AutoCompleteParagraph();
      this.ShelfLifeWarning.SetAllColours(ColourData.Z_Cream);
      this.ShelfLifeWarning.Location = this.DeliveryTimeAlert.Location;
      this.Infor2 = new LittleSummaryButton(LittleSummaryButtonType.BlueInfoCircle, _BaseScale: BaseScale);
      if ((double) this.DaysOfStock >= (double) AnimalFoodData.GetAnimalFoodInfo(animalinfo.CriticalFood).DeliveryTime)
        this.DeliveryTimeAlert = (SimpleTextHandler) null;
      string str1 = "DELIVERY TIME (" + (object) AnimalFoodData.GetAnimalFoodInfo(animalinfo.CriticalFood).DeliveryTime + " days):~~All orders take time to be delivered, ";
      string str2;
      if ((double) this.InStock == 0.0)
        str2 = str1 + " you will have no stock of this item until " + (object) AnimalFoodData.GetAnimalFoodInfo(animalinfo.CriticalFood).DeliveryTime + " days have passed.";
      else
        str2 = str1 + " At you current rate of consumption, this will reun out before it is delivered";
      this.DeliveryTimeInfo = new SimpleTextHandler(str2 + "~~You could change your animal's diet to stop them going hungry", true, 0.1f * BaseScale, BaseScale, AutoComplete: true);
      this.DeliveryTimeInfo.AutoCompleteParagraph();
      this.DeliveryTimeInfo.SetAllColours(ColourData.Z_Cream);
      this.ShelfLifeInfo = new SimpleTextHandler("SHELF LIFE (" + (object) this.ShelfLife + " days):~~At your current rate of consumption, this product will expire before it is used.~~Expired food is automatically destroyed, try to manage your inventory to avoid waste.", true, 0.1f * BaseScale, BaseScale, AutoComplete: true);
      this.ShelfLifeInfo.AutoCompleteParagraph();
      this.ShelfLifeInfo.SetAllColours(ColourData.Z_Cream);
      this.Infor3 = new LittleSummaryButton(LittleSummaryButtonType.RedCloseCircle, _BaseScale: BaseScale);
      Vector2 zero = Vector2.Zero;
      this.paragraph_stock.Location.Y = zero.Y;
      this.paragraph_stock.Location.Y += this.paragraph_stock.GetHeightOfOneLine() * 0.5f;
      zero.Y += this.paragraph_stock.GetHeightOfParagraph();
      zero.Y += defaultBuffer.Y;
      this.bar.vLocation.Y = zero.Y;
      this.bar.vLocation.Y += this.bar.GetSize().Y * 0.5f;
      zero.Y += this.bar.GetSize().Y;
      zero.Y += defaultBuffer.Y;
      this.paragraph_foodused.Location.Y = zero.Y;
      this.paragraph_foodused.Location.Y += this.paragraph_foodused.GetHeightOfOneLine() * 0.5f;
      zero.Y += this.paragraph_foodused.GetHeightOfParagraph();
      zero.Y += defaultBuffer.Y;
      zero.Y += defaultBuffer.Y;
      float num = 55f * BaseScale;
      if (this.DeliveryTimeAlert != null)
      {
        this.Infor.vLocation.X += num;
        this.Infor.vLocation.Y = zero.Y;
        this.Infor.vLocation.Y += this.Infor.GetSize().Y * 0.5f;
        this.DeliveryTimeAlert.Location.X -= 10f * BaseScale;
        this.DeliveryTimeAlert.Location.Y = zero.Y;
        this.DeliveryTimeAlert.Location.Y += this.DeliveryTimeAlert.GetHeightOfOneLine() * 0.5f;
      }
      this.ShelfLifeWarning.Location.Y += zero.Y;
      this.Infor2.vLocation.Y = zero.Y;
      this.Infor2.vLocation.X = num;
      this.DeliveryTimeInfo.Location.Y = defaultBuffer.Y;
      this.DeliveryTimeInfo.Location.X -= defaultBuffer.X;
      this.ShelfLifeInfo.Location = this.DeliveryTimeInfo.Location;
      this.Infor3.vLocation.X = num;
      this.Infor3.vLocation.Y = defaultBuffer.Y;
      this.SetBar();
    }

    public bool BlockBuy() => this.ShowingShelfLifeInfo || this.ShowingDeliveryTime;

    public void UpdateQuickOrderDisplayString(
      int BasketCount,
      Vector2 Offset,
      Player player,
      float DeltaTime)
    {
      Offset += this.location;
      if ((this.ShowingDeliveryTime || this.ShowingShelfLifeInfo) && this.Infor3.UpdateLittleSummaryButton(DeltaTime, player, Offset))
      {
        this.ShowingDeliveryTime = false;
        this.ShowingShelfLifeInfo = false;
      }
      if (this.DeliveryTimeAlert != null && this.Infor.UpdateLittleSummaryButton(DeltaTime, player, Offset))
        this.ShowingDeliveryTime = true;
      else if ((double) this.DaysOfStock > (double) this.ShelfLife && this.Infor2.UpdateLittleSummaryButton(DeltaTime, player, Offset))
      {
        this.ShowingShelfLifeInfo = true;
      }
      else
      {
        if (this.LastBasketCount == BasketCount)
          return;
        this.LastBasketCount = BasketCount;
        this.SetBar();
      }
    }

    private void SetBar()
    {
      float num = (float) this.LastBasketCount / this.UsedPerDay / this.BarTarget;
      this.bar.SetFullness(this.DaysOfStock_barFloat, 2);
      this.bar.SetFullness(this.DaysOfStock_barFloat + this.DaysOfOnOrder_barFloat, 1);
      this.bar.SetFullness(this.DaysOfStock_barFloat + this.DaysOfOnOrder_barFloat + num);
    }

    public void DrawQuickOrderDisplayString(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.location;
      if (this.ShowingDeliveryTime)
      {
        this.Infor3.DrawLittleSummaryButton(Offset, spritebatch);
        this.DeliveryTimeInfo.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      }
      else if (this.ShowingShelfLifeInfo)
      {
        this.Infor3.DrawLittleSummaryButton(Offset, spritebatch);
        this.ShelfLifeInfo.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      }
      else
      {
        this.paragraph_stock.DrawSimpleTextHandler(Offset, 1f, spritebatch);
        this.paragraph_foodused.DrawSimpleTextHandler(Offset, 1f, spritebatch);
        this.bar.DrawSatisfactionBar(Offset, spritebatch);
        if (this.DeliveryTimeAlert != null)
        {
          this.Infor.DrawLittleSummaryButton(Offset, spritebatch);
          if ((double) FlashingAlpha.Medium.fAlpha < 0.800000011920929)
            this.DeliveryTimeAlert.DrawSimpleTextHandler(Offset, 1f, spritebatch);
        }
        if ((double) this.DaysOfStock_barFloat <= (double) this.ShelfLife)
          return;
        this.Infor2.DrawLittleSummaryButton(Offset, spritebatch);
        if ((double) FlashingAlpha.Medium.fAlpha >= 0.800000011920929)
          return;
        this.ShelfLifeWarning.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      }
    }
  }
}
