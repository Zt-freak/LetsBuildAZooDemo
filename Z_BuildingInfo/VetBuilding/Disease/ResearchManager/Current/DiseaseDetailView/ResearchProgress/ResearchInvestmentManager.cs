// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.DiseaseDetailView.ResearchProgress.ResearchInvestmentManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.DiseaseDetailView.DiseaseInformation;
using TinyZoo.Z_Employees.Emp_Summary.Hiring;
using TinyZoo.Z_ManageShop.RecipeView.FoodSlidr;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.DiseaseDetailView.ResearchProgress
{
  internal class ResearchInvestmentManager
  {
    private CustomerFrame customerframe;
    private CustomerFrame Inner;
    private float BaseScale;
    private Microscope microsope;
    private SimpleTextHandler texty;
    private MiniHeading header;
    public Vector2 Location;
    private SalarySliderBarWithText paySlider;
    private DragAndBar dragandbar;
    private GameObject MoneyText;
    private Vector2 ExtraOffset;
    private TinyZoo.Z_Diseases.Disease REF_Disease;
    private float PERC;

    public ResearchInvestmentManager(float BaseScale, TinyZoo.Z_Diseases.Disease disease)
    {
      this.REF_Disease = disease;
      this.customerframe = new CustomerFrame(new Vector2(300f * BaseScale + 10f * BaseScale, 150f * BaseScale), true, BaseScale, true);
      this.microsope = new Microscope(BaseScale);
      this.texty = new SimpleTextHandler("Increase your daily investment to discover a cure more quickly", 200f * BaseScale, _Scale: BaseScale, AutoComplete: true);
      this.texty.Location = new Vector2(-80f * BaseScale, -55f * BaseScale);
      this.header = new MiniHeading(this.customerframe.VSCale, "Research Investment:", 1f, BaseScale);
      this.microsope.vLocation = new Vector2(this.customerframe.VSCale.X * -0.5f, 0.0f);
      this.microsope.vLocation.X += BaseScale * 15f;
      this.microsope.vLocation.X += (float) ((double) this.microsope.DrawRect.Width * (double) this.microsope.scale * 0.5);
      this.microsope.vLocation.Y -= BaseScale * 15f;
      this.PERC = this.REF_Disease.diseaseresearch.GetInvestmentPercentage();
      this.dragandbar = new DragAndBar(false, this.PERC, 100f, BaseScale);
      this.dragandbar.LeftText = "";
      this.MoneyText = new GameObject();
      this.MoneyText.SetAllColours(ColourData.Z_Cream);
      this.MoneyText.scale = BaseScale;
      this.MoneyText.vLocation.Y = 18f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.ExtraOffset.Y = 20f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.Inner = new CustomerFrame(this.customerframe.VSCale + new Vector2((float) (10.0 * -(double) BaseScale), (float) (30.0 * -(double) BaseScale) * Sengine.ScreenRatioUpwardsMultiplier.Y), BaseScale: BaseScale, UseTinyRect: true);
      this.Inner.location.Y = 7.5f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void UpdateResearchInvestmentManager(Vector2 Offset, Player player, float DeltaTime)
    {
      Offset += this.Location;
      Offset += this.ExtraOffset;
      this.dragandbar.UpdateDragAndBar(player, DeltaTime, Offset);
      if ((double) this.PERC == (double) this.dragandbar.CurrentDragPercent)
        return;
      this.PERC = this.dragandbar.CurrentDragPercent;
      this.REF_Disease.diseaseresearch.SetSpendPerDayFromPercentage(this.PERC);
    }

    public void DrawResearchInvestmentManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.Inner.DrawCustomerFrame(Offset, spritebatch);
      this.header.DrawMiniHeading(Offset, spritebatch);
      Offset += this.ExtraOffset;
      this.microsope.DrawMicroscope(Offset, spritebatch);
      this.dragandbar.DrawDragAndBar(spritebatch, Offset);
      this.texty.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      TextFunctions.DrawJustifiedText("$" + (object) this.REF_Disease.diseaseresearch.SpendPerDay, this.MoneyText.scale, this.MoneyText.vLocation * -1f + Offset + this.dragandbar.Location, this.MoneyText.GetColour(), this.MoneyText.fAlpha, AssetContainer.SpringFontX1AndHalf, spritebatch);
      TextFunctions.DrawJustifiedText("Daily Investment", this.MoneyText.scale, this.MoneyText.vLocation + Offset + this.dragandbar.Location, this.MoneyText.GetColour(), this.MoneyText.fAlpha, AssetContainer.SpringFontX1AndHalf, spritebatch);
    }
  }
}
