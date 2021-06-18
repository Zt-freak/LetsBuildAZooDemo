// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.StockAdjuster
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_Manage.Hiring;
using TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer;
using TinyZoo.Z_ManageShop.FoodIcon;
using TinyZoo.Z_ManageShop.Shop_Data;
using TinyZoo.Z_ManageShop.StockAdjustBits;

namespace TinyZoo.Z_ManageShop
{
  internal class StockAdjuster
  {
    private GameObjectNineSlice box;
    private string StateName;
    private Vector2 VSCALE;
    public Vector2 Location;
    private StockStatus stockstatus;
    private Vector3 SecondaryColour;
    private GameObject TextColourer;
    private PriceAdjuster Price;
    private TextButton managetextbutton;
    private BigPersonFrame foodframe;
    private bool Locked;
    private ShopStockStatus REF_playershopstockstatus;

    public StockAdjuster(ShopStatEntry shopstat, ShopStockStatus playershopstockstatus)
    {
      this.REF_playershopstockstatus = playershopstockstatus;
      Rectangle frameColourRect = StringInBox.GetFrameColourRect(BTNColour.Cream, out this.SecondaryColour);
      this.StateName = FoodIconData.GetFoodTypeToString(shopstat.MainItemForSale).ToUpper();
      if (!playershopstockstatus.Unlocked)
      {
        this.Locked = true;
        this.StateName = "LOCKED";
        frameColourRect = StringInBox.GetFrameColourRect(BTNColour.Grey, out Vector3 _);
      }
      else
      {
        this.Locked = false;
        this.managetextbutton = new TextButton("MANAGE", 50f, OverAllMultiplier: 1.3f);
        this.managetextbutton.vLocation = new Vector2(258f, 20f);
        this.stockstatus = new StockStatus(playershopstockstatus.CurrentStock, playershopstockstatus.MaximumStock, playershopstockstatus.GetCurrentPrice(), shopstat);
        this.Price = new PriceAdjuster(0, 100, playershopstockstatus.GetCurrentPrice(), true, Sengine.ScreenRationReductionMultiplier.Y);
        this.Price.SetSImpleRendererTextColur(this.SecondaryColour);
      }
      this.box = new GameObjectNineSlice(frameColourRect, 7);
      this.box.scale = 3f * Sengine.ScreenRationReductionMultiplier.Y;
      this.VSCALE = new Vector2(900f, 180f * Sengine.ScreenRationReductionMultiplier.Y);
      this.TextColourer = new GameObject();
      this.TextColourer.SetAllColours(this.SecondaryColour);
      this.foodframe = new BigPersonFrame(shopstat.MainItemForSale, false, " ", Sengine.ScreenRationReductionMultiplier.Y);
      if (!this.Locked)
        return;
      this.foodframe.GreyOut();
    }

    public void UpdateStockAdjuster(
      Vector2 Offset,
      Player player,
      float DeltaTime,
      out bool GoToStockView)
    {
      GoToStockView = false;
      bool PressedPrice;
      if (this.stockstatus != null && this.stockstatus.UpdateStockStatus(player, DeltaTime, Offset, out PressedPrice) && PressedPrice)
        throw new Exception("CostWasChanged = true");
      if (this.Price != null)
      {
        this.Price.UpdatePriceAdjuster(player, Offset + this.Location, DeltaTime);
        this.REF_playershopstockstatus.SetCurrentPrice(this.Price.CurrentValue);
      }
      if (this.managetextbutton == null || !this.managetextbutton.UpdateTextButton(player, Offset + this.Location, DeltaTime))
        return;
      GoToStockView = true;
    }

    public void DrawStockAdjuster(Vector2 Offset)
    {
      Offset += this.Location;
      this.box.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VSCALE);
      TextFunctions.DrawTextWithDropShadow(this.StateName, 1.3f * Sengine.ScreenRationReductionMultiplier.Y, Offset + new Vector2(-280f, -60f * Sengine.ScreenRationReductionMultiplier.Y), this.TextColourer.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03, false);
      StockStatus stockstatus = this.stockstatus;
      if (this.Price != null)
      {
        this.Price.Location = new Vector2(-60f, 20f);
        TextFunctions.DrawTextWithDropShadow("PRICE:", 1f * Sengine.ScreenRationReductionMultiplier.Y, this.Price.Location + Offset + new Vector2(-220f, -10f * Sengine.ScreenRationReductionMultiplier.Y), this.TextColourer.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, false);
        this.Price.DrawPriceAdjuster(Offset);
        TextFunctions.DrawJustifiedText("EXPENSIVE!", 0.7f * Sengine.ScreenRationReductionMultiplier.Y, this.Price.Location + Offset + new Vector2(0.0f, 40f * Sengine.ScreenRationReductionMultiplier.Y), this.TextColourer.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05);
      }
      if (this.foodframe != null)
        this.foodframe.DrawBigPersonFrame(Offset + new Vector2(-360f, 0.0f), AssetContainer.pointspritebatchTop05, !this.Locked);
      if (this.managetextbutton == null)
        return;
      TextFunctions.DrawTextWithDropShadow("STOCK:", 1f * Sengine.ScreenRationReductionMultiplier.Y, Offset + new Vector2(250f, -55f * Sengine.ScreenRationReductionMultiplier.Y), this.TextColourer.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03, false, true);
      TextFunctions.DrawTextWithDropShadow("12/19", 1.3f * Sengine.ScreenRationReductionMultiplier.Y, Offset + new Vector2(265f, -60f * Sengine.ScreenRationReductionMultiplier.Y), this.TextColourer.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03, false);
      this.managetextbutton.DrawTextButton(Offset);
    }
  }
}
