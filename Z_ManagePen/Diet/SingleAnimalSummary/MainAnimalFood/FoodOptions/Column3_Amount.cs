// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodOptions.Column3_Amount
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer;

namespace TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodOptions
{
  internal class Column3_Amount
  {
    private TopTextMini toptextmini;
    public Vector2 Location;
    public PriceAdjuster priceadjuster;

    public Column3_Amount(
      float HeightForText,
      float CurrentServing,
      float FullDailyFoodRquirement,
      float BaseScale)
    {
      this.toptextmini = new TopTextMini("% Daily Req", BaseScale, HeightForText);
      this.priceadjuster = new PriceAdjuster(0, 150, (int) ((double) CurrentServing / (double) FullDailyFoodRquirement * 100.0), true, 0.5f, _BaseScale: BaseScale);
      this.priceadjuster.SetSImpleRendererTextColur(ColourData.Z_Cream);
      this.priceadjuster.EndString = "%";
      this.priceadjuster.Location = new Vector2(60f, 5f);
      this.priceadjuster.SetToSpringFont();
      this.priceadjuster.PreString = "";
    }

    public bool UpdateColumn3_Amount(
      Player player,
      float DeltaTime,
      Vector2 Offset,
      bool IsAtMax,
      int CurrentTotal)
    {
      if (IsAtMax)
      {
        this.priceadjuster.LockToMax();
      }
      else
      {
        this.priceadjuster.UnsetMax();
        this.priceadjuster.MaxValue = 150 - CurrentTotal + this.priceadjuster.CurrentValue;
      }
      Offset += this.Location;
      return this.priceadjuster.UpdatePriceAdjuster(player, Offset, DeltaTime);
    }

    public void DrawColumn3_Amount(Vector2 Offset)
    {
      Offset += this.Location;
      this.toptextmini.DrawTopTextMini(Offset);
      this.priceadjuster.DrawPriceAdjuster(Offset);
    }
  }
}
