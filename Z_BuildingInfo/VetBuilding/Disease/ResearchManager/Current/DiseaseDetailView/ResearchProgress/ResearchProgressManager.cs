// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.DiseaseDetailView.ResearchProgress.ResearchProgressManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.DiseaseDetailView.ResearchProgress
{
  internal class ResearchProgressManager
  {
    private CustomerFrame customerframe;
    private CustomerFrame Inner;
    private SatisfactionBar satisfactionbar;
    public Vector2 Location;
    private MiniHeading miniheader;
    private SimpleTextHandler texty;
    private Vector2 InternalOffset;

    public ResearchProgressManager(float BaseScale, TinyZoo.Z_Diseases.Disease disease)
    {
      this.customerframe = new CustomerFrame(new Vector2(300f * BaseScale + 10f * BaseScale, 90f * BaseScale), true, BaseScale, true);
      this.Inner = new CustomerFrame(this.customerframe.VSCale + new Vector2((float) (10.0 * -(double) BaseScale), (float) (30.0 * -(double) BaseScale) * Sengine.ScreenRatioUpwardsMultiplier.Y), BaseScale: BaseScale, UseTinyRect: true);
      float FullPercent = 1f;
      if (!disease.IsResearched)
        FullPercent = disease.diseaseresearch.TotalProgress;
      this.satisfactionbar = new SatisfactionBar(FullPercent, BaseScale);
      this.miniheader = new MiniHeading(this.customerframe.VSCale, "Research Progress", 1f, BaseScale);
      this.texty = new SimpleTextHandler("Researching for " + disease.diseaseresearch.GetDaysResearched() + " days", 200f * BaseScale, _Scale: BaseScale, AutoComplete: true);
      this.texty.Location = new Vector2(-80f * BaseScale, -25f * BaseScale);
      this.texty.SetAllColours(ColourData.Z_Cream);
      this.InternalOffset.Y = 7.5f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.texty.Location.Y += 7f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.satisfactionbar.vLocation.Y += 7f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void UpdateResearchProgressManager(Vector2 Offset, Player player, float DeltaTime) => Offset += this.Location;

    public void DrawResearchProgressManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.miniheader.DrawMiniHeading(Offset);
      Offset += this.InternalOffset;
      this.Inner.DrawCustomerFrame(Offset, spritebatch);
      this.texty.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      this.satisfactionbar.DrawSatisfactionBar(Offset, spritebatch);
    }
  }
}
