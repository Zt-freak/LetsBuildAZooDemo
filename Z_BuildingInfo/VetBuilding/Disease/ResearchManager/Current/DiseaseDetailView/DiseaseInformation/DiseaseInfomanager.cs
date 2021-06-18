// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.DiseaseDetailView.DiseaseInformation.DiseaseInfomanager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.IC.DiseaseDetailView;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.DiseaseDetailView.DiseaseInformation
{
  internal class DiseaseInfomanager
  {
    public Vector2 Location;
    private CustomerFrame customerframe;
    private MiniHeading miniheading;
    private DiseaseInfoDisplay diseaseinfodisplay;

    public DiseaseInfomanager(TinyZoo.Z_Diseases.Disease disease, float BaseScale)
    {
      this.diseaseinfodisplay = new DiseaseInfoDisplay(disease, BaseScale);
      Vector2 size = this.diseaseinfodisplay.GetSize();
      size.X += BaseScale * 10f;
      size.Y += BaseScale * 20f;
      this.customerframe = new CustomerFrame(size, true, BaseScale);
      this.miniheading = new MiniHeading(this.customerframe.VSCale, "Disease overview: " + disease.Name, 1f, BaseScale);
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void UpdateDiseaseInfomanager()
    {
    }

    public void DrawDiseaseInfomanager(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.miniheading.DrawMiniHeading(this.customerframe.location + Offset, spritebatch);
      this.diseaseinfodisplay.DrawDiseaseInfoDisplay(Offset, spritebatch);
    }
  }
}
