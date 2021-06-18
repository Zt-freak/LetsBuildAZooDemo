// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.CurrentCostPerDay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood
{
  internal class CurrentCostPerDay : GameObject
  {
    private CustomerFrame customerframe;
    private string Text;
    public Vector2 Location;
    public float Height;

    public CurrentCostPerDay(float BaseScale)
    {
      this.customerframe = new CustomerFrame(new Vector2((float) (200.0 * (double) BaseScale - 10.0 * (double) BaseScale), 20f * BaseScale), BaseScale: BaseScale);
      this.customerframe.location.X = 129f * BaseScale;
      this.SetAllColours(ColourData.Z_Cream);
      this.scale = BaseScale;
      this.vLocation.Y = 3f * BaseScale;
      this.Location.Y = this.customerframe.VSCale.Y * 0.5f;
      this.Location.Y += BaseScale * 10f;
      this.Height = this.customerframe.VSCale.Y;
      this.Height += BaseScale * 20f;
    }

    public void SetTotalCost(string COST) => this.Text = "Total Cost Per Day: $" + COST;

    public void UpdateCurrentCostPerDay()
    {
    }

    public void DrawCurrentCostPerDay(Vector2 Offset)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, AssetContainer.pointspritebatchTop05);
      TextFunctions.DrawJustifiedText(this.Text, this.scale, this.customerframe.location + Offset + this.vLocation, this.GetColour(), this.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05);
    }
  }
}
