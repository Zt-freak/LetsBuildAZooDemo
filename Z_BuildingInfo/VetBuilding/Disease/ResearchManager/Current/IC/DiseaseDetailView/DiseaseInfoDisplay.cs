// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.IC.DiseaseDetailView.DiseaseInfoDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.IC.DiseaseDetailView
{
  internal class DiseaseInfoDisplay
  {
    private string[] Left;
    private string[] Right;
    private CustomerFrame customerframe;
    private float BaseScale;
    public Vector2 Location;
    private GameObject Lefty;
    private float RowHeight;

    public DiseaseInfoDisplay(TinyZoo.Z_Diseases.Disease disease, float _BaseScale)
    {
      this.RowHeight = 35f;
      this.BaseScale = _BaseScale;
      this.Left = new string[7];
      this.Right = new string[7];
      this.Left[0] = "Status:";
      this.Left[2] = "Mortality Rate:";
      this.Left[1] = "Infection Rate:";
      this.Left[3] = "Average Incubation Period:";
      this.Left[5] = "Steralization Time:";
      this.Left[4] = "Average Recovery Time:";
      this.Left[6] = "Effective Spread Distance:";
      this.Right[0] = disease.Name;
      this.Right[2] = disease.MortallityRate.ToString() + "%";
      this.Right[1] = disease.ProbabilityOfInfection.ToString() + "%";
      this.Right[3] = disease.GetIncubationPeriodString();
      this.Right[5] = disease.GetSterilizationTimeString();
      this.Right[4] = disease.GetRecoveryTimeString();
      this.Right[6] = disease.GetRangeString();
      if (!disease.IsResearched)
      {
        this.Right[0] = "Research incomplete";
        for (int index = 1; index < this.Right.Length; ++index)
        {
          if (!disease.diseaseresearch.Progress[index])
            this.Right[index] = "Unknown";
        }
      }
      this.customerframe = new CustomerFrame(new Vector2(300f * this.BaseScale, 25f * this.BaseScale), BaseScale: this.BaseScale, UseTinyRect: true);
      this.Lefty = new GameObject();
      this.Lefty.SetAllColours(ColourData.Z_Cream);
      this.Lefty.scale = this.BaseScale;
      this.Lefty.vLocation.X = this.customerframe.VSCale.X * -0.5f;
      this.Lefty.vLocation.X += 10f * this.BaseScale;
      this.Location.Y = (float) this.Right.Length * this.RowHeight * this.BaseScale + this.customerframe.VSCale.Y;
      this.Location.Y *= -0.5f;
      this.Location.Y += this.RowHeight * this.BaseScale;
      this.Location.Y += 10f * this.BaseScale;
    }

    public Vector2 GetSize() => new Vector2(this.customerframe.VSCale.X, (float) this.Right.Length * this.RowHeight * this.BaseScale + this.customerframe.VSCale.Y);

    public void UpdateDiseaseInfoDisplay()
    {
    }

    public void DrawDiseaseInfoDisplay(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      for (int index = 0; index < this.Right.Length; ++index)
      {
        this.customerframe.DrawCustomerFrame(Offset, spritebatch);
        TextFunctions.DrawTextWithDropShadow(this.Left[index], this.Lefty.scale, this.Lefty.vLocation + Offset, this.Lefty.GetColour(), this.Lefty.fAlpha, AssetContainer.springFont, spritebatch, false, false, false, 0.0f, 1);
        TextFunctions.DrawTextWithDropShadow(this.Right[index], this.Lefty.scale, Offset - this.Lefty.vLocation, this.Lefty.GetColour(), this.Lefty.fAlpha, AssetContainer.springFont, spritebatch, false, true, false, 0.0f, 1);
        Offset.Y += this.RowHeight * this.BaseScale;
      }
    }
  }
}
