// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.IC.DiseaseDetailView.DiseaseDetailViewManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.DiseaseDetailView;
using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.DiseaseDetailView.DiseaseInformation;
using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.DiseaseDetailView.InfectedAnimals;
using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.DiseaseDetailView.ResearchProgress;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.IC.DiseaseDetailView
{
  internal class DiseaseDetailViewManager
  {
    private Vector2 InternalLocation;
    private AnimalsImpacted animalsimpacted;
    private DiseaseInfomanager diseaseinfo;
    private Vector2 FullSize;
    private ResearchInvestmentManager researchIvestment;
    private ResearchProgressManager researchprogress;
    private CurrentInfectionsManager currentinfections;

    public DiseaseDetailViewManager(TinyZoo.Z_Diseases.Disease disease, float BaseScale, Player player)
    {
      this.diseaseinfo = new DiseaseInfomanager(disease, BaseScale);
      this.diseaseinfo.Location.Y = this.diseaseinfo.GetSize().Y * 0.5f;
      this.diseaseinfo.Location.X = this.diseaseinfo.GetSize().X * -0.5f;
      this.diseaseinfo.Location.X -= BaseScale * 5f;
      this.animalsimpacted = new AnimalsImpacted(disease, BaseScale);
      this.animalsimpacted.location = this.diseaseinfo.Location;
      this.animalsimpacted.location.Y += this.diseaseinfo.GetSize().Y * 0.5f;
      this.animalsimpacted.location.Y += this.animalsimpacted.GetSize().Y * 0.5f;
      this.animalsimpacted.location.Y += 5f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      Vector2 vector2_1 = new Vector2(this.animalsimpacted.GetSize().X, this.animalsimpacted.location.Y + this.animalsimpacted.GetSize().Y * 0.5f);
      this.researchprogress = new ResearchProgressManager(BaseScale, disease);
      this.researchprogress.Location.X = this.researchprogress.GetSize().X * 0.5f;
      this.researchprogress.Location.Y = this.researchprogress.GetSize().Y * 0.5f;
      this.researchIvestment = new ResearchInvestmentManager(BaseScale, disease);
      this.researchIvestment.Location = this.researchprogress.Location;
      this.researchIvestment.Location.Y += this.researchprogress.GetSize().Y * 0.5f;
      this.researchIvestment.Location.Y += 5f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.researchIvestment.Location.Y += this.researchIvestment.GetSize().Y * 0.5f;
      this.currentinfections = new CurrentInfectionsManager(disease, player, BaseScale);
      this.currentinfections.Location = this.researchIvestment.Location;
      this.currentinfections.Location.Y += this.researchIvestment.GetSize().Y * 0.5f;
      this.currentinfections.Location.Y += 5f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.currentinfections.Location.Y += this.currentinfections.GetSize().Y * 0.5f;
      Vector2 vector2_2 = new Vector2(this.currentinfections.GetSize().X, this.currentinfections.Location.Y + this.currentinfections.GetSize().Y * 0.5f);
      this.FullSize = new Vector2((float) ((double) vector2_2.X + (double) vector2_1.X + (double) BaseScale * 10.0), Math.Max(vector2_2.Y, vector2_1.Y));
      this.InternalLocation.Y = this.FullSize.Y * -0.5f;
    }

    public Vector2 GetSize() => this.FullSize;

    public void UpdateDiseaseDetailViewManager(Player player, Vector2 Offset, float DeltaTime)
    {
      Offset += this.InternalLocation;
      this.animalsimpacted.UpdateAnimalsImpacted(player, DeltaTime, Offset);
      this.researchIvestment.UpdateResearchInvestmentManager(Offset, player, DeltaTime);
      this.currentinfections.UpdateCurrentInfectionsManager(Offset, DeltaTime, player);
    }

    public void DrawDiseaseDetailViewManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.InternalLocation;
      this.diseaseinfo.DrawDiseaseInfomanager(Offset, spritebatch);
      this.animalsimpacted.DrawAnimalsImpacted(Offset, spritebatch);
      this.researchIvestment.DrawResearchInvestmentManager(Offset, spritebatch);
      this.researchprogress.DrawResearchProgressManager(Offset, spritebatch);
      this.currentinfections.DrawCurrentInfectionsManager(Offset, spritebatch);
    }
  }
}
