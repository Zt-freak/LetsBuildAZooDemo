// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.IC.D_TextInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.IC
{
  internal class D_TextInfo : GameObject
  {
    private string Text;
    private string Subtext;
    private GameObject SubSTring;
    private SimpleTextHandler SmallText;
    private Vector2 Location;
    private SatisfactionBar researchprogress;

    public D_TextInfo(float BaseScale, DiseaseType diseasetype, bool IsJournal, TinyZoo.Z_Diseases.Disease disease)
    {
      this.SetAllColours(ColourData.Z_Cream);
      this.scale = BaseScale;
      switch (diseasetype)
      {
        case DiseaseType.KnownDisease:
          this.Text = "Curable";
          this.Subtext = "Disease Name Goes Here";
          break;
        case DiseaseType.UnknownDisease:
          this.Text = "Researching";
          this.Subtext = "Invest in learning more about this disease with the hope of finding a cure.";
          break;
        case DiseaseType.SuspectedDisease:
          this.Text = "Possible Outbreak";
          this.Subtext = "There is an untracked infection somewhere in the zoo";
          break;
      }
      if (IsJournal)
      {
        this.Text = "Cured";
        this.Subtext = "Pink Eye";
      }
      this.SubSTring = new GameObject((GameObject) this);
      this.SubSTring.vLocation.Y = 10f * BaseScale;
      this.SmallText = new SimpleTextHandler(this.Subtext, 140f * BaseScale, _Scale: BaseScale, AutoComplete: true);
      this.SmallText.SetAllColours(ColourData.Z_Cream);
      this.SmallText.Location.Y = 20f * BaseScale;
      this.Location = new Vector2(-45f, -30f) * BaseScale;
      if (IsJournal)
        return;
      float FullPercent = 1f;
      if (!disease.IsResearched)
        FullPercent = disease.diseaseresearch.TotalProgress;
      this.researchprogress = new SatisfactionBar(FullPercent, BaseScale);
      this.researchprogress.vLocation = new Vector2(0.0f, 35f * BaseScale);
    }

    public void DrawD_TextInfo(Vector2 Offset, SpriteBatch spritebatch)
    {
      if (this.researchprogress != null)
        this.researchprogress.DrawSatisfactionBar(Offset, spritebatch);
      Offset += this.Location;
      TextFunctions.DrawTextWithDropShadow(this.Text, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.SpringFontX1AndHalf, spritebatch, false);
      this.SmallText.DrawSimpleTextHandler(Offset, 1f, spritebatch);
    }
  }
}
