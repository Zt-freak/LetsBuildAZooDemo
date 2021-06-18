// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.DiseaseViewManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.DiseaseCat;
using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.MedicalJournal;
using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager;
using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.VisitSummary;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease
{
  internal class DiseaseViewManager
  {
    private DiseeaseCatSelectManager categoryselectmanager;
    private MedicalJournalManager medicalJournalManager;
    private DiseaseResearchManager diseaseResearchManager;
    private PenVisitSummary penVisitSummary;
    private DiseaseViewManager.VetHouseView screenstate;

    public DiseaseViewManager()
    {
      this.categoryselectmanager = new DiseeaseCatSelectManager();
      this.screenstate = DiseaseViewManager.VetHouseView.CatSelect;
    }

    public void UpdateDiseaseViewManager()
    {
      int screenstate1 = (int) this.screenstate;
      int screenstate2 = (int) this.screenstate;
      int screenstate3 = (int) this.screenstate;
      int screenstate4 = (int) this.screenstate;
    }

    public void DrawDiseaseViewManager()
    {
      int screenstate1 = (int) this.screenstate;
      int screenstate2 = (int) this.screenstate;
      int screenstate3 = (int) this.screenstate;
      int screenstate4 = (int) this.screenstate;
    }

    private enum VetHouseView
    {
      CatSelect,
      Journal,
      DiseaseResearch,
      Visitation,
      Count,
    }
  }
}
