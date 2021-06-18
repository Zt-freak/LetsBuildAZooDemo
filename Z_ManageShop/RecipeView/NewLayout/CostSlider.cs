// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.RecipeView.NewLayout.CostSlider
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer;

namespace TinyZoo.Z_ManageShop.RecipeView.NewLayout
{
  internal class CostSlider
  {
    public PriceAdjuster Price;
    private GameObject TextColourer;
    public Vector2 Location;
    private bool IsBuy;
    private string TopString;
    private ShopStockStatus REF_playershopstockstatus;
    private float TextScale;
    private bool Temp_ForceLeft;
    private bool Temp_ForceRight;
    private string SecondSTRING = " ";

    public CostSlider(
      ShopStockStatus playershopstockstatus,
      Vector3 SecondaryColour,
      float BaseScale,
      bool _IsBuy = false)
    {
      this.TextScale = BaseScale;
      this.REF_playershopstockstatus = playershopstockstatus;
      this.Price = new PriceAdjuster(0, playershopstockstatus.REF_shopentry.MaxSellToPublicCost, playershopstockstatus.GetCurrentPrice(), true, 0.4f, true, BaseScale);
      this.Price.SetSImpleRendererTextColur(SecondaryColour);
      this.Price.AddZero = true;
      this.TextColourer = new GameObject();
      this.TextColourer.SetAllColours(SecondaryColour);
      this.IsBuy = _IsBuy;
      this.TopString = "SELL PRICE:";
      if (!this.IsBuy)
        return;
      this.TopString = "COST:";
      this.Price.RemoveButtons();
    }

    public void ForceUpdateSlider(bool ForceLeft, bool ForceRight)
    {
      this.Temp_ForceLeft = ForceLeft;
      this.Temp_ForceRight = ForceRight;
    }

    public bool UpdateCostSlider(Player player, Vector2 Offset, float DeltaTime)
    {
      if (!this.Temp_ForceLeft)
      {
        int num = this.Temp_ForceRight ? 1 : 0;
      }
      Offset += this.Location;
      if (this.Price.UpdatePriceAdjuster(player, Offset, DeltaTime, this.Temp_ForceLeft, this.Temp_ForceRight) && !this.IsBuy)
      {
        this.REF_playershopstockstatus.SetCurrentPrice(this.Price.CurrentValue);
        return true;
      }
      this.Temp_ForceLeft = false;
      this.Temp_ForceRight = false;
      return false;
    }

    public void SetCost(int COST) => this.Price.SetCost(COST);

    public void SetSecondStrring(string _SecondSTRING) => this.SecondSTRING = _SecondSTRING;

    public void DrawCostSlider(Vector2 Offset)
    {
      Offset += this.Location;
      TextFunctions.DrawJustifiedText(this.TopString, this.TextScale, this.Price.Location + Offset + new Vector2(0.0f, -35f * this.TextScale * Sengine.ScreenRationReductionMultiplier.Y), this.TextColourer.GetColour(), 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05);
      this.Price.DrawPriceAdjuster(Offset);
      TextFunctions.DrawJustifiedText(this.SecondSTRING, this.TextScale, this.Price.Location + Offset + new Vector2(0.0f, this.TextScale * 35f * Sengine.ScreenRationReductionMultiplier.Y), this.TextColourer.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
    }
  }
}
