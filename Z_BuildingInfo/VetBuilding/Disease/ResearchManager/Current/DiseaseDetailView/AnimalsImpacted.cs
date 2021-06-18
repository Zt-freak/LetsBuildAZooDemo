// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.DiseaseDetailView.AnimalsImpacted
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_SummaryPopUps.People;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.DiseaseDetailView
{
  internal class AnimalsImpacted
  {
    private VariableAnimalList ListOfAnimals;
    private CustomerFrame frame;
    private MiniHeading heading;
    public Vector2 location;
    private LittleSummaryButton summary;
    private bool SHowingInfo;
    private SimpleTextHandler simpletext;

    public AnimalsImpacted(TinyZoo.Z_Diseases.Disease disease, float BaseScale) => this.Create(disease.CanInfectThese, BaseScale, "This disease can be contracted by these animal types.", "Transmission");

    public AnimalsImpacted(float BaseScale, List<AnimalType> KnownInfectedAnimals) => this.Create(KnownInfectedAnimals, BaseScale, "Animals known to have this disease in your zoo", "Current Infections");

    private void Create(
      List<AnimalType> animals,
      float BaseScale,
      string BODYTEXT,
      string Headingg)
    {
      this.ListOfAnimals = new VariableAnimalList(animals, BaseScale, 300f * BaseScale, 1);
      this.frame = new CustomerFrame(this.ListOfAnimals.BGframe.VSCale + new Vector2(10f * BaseScale, 50f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y), true);
      this.summary = new LittleSummaryButton(LittleSummaryButtonType.BlueInfoCircle, _BaseScale: BaseScale);
      this.summary.vLocation.X = this.frame.VSCale.X * 0.5f;
      this.summary.vLocation.Y = this.frame.VSCale.Y * -0.5f;
      this.summary.vLocation.X -= (float) ((double) this.summary.scale * (double) this.summary.DrawRect.Width * 0.5);
      this.summary.vLocation.Y += (float) ((double) this.summary.scale * (double) this.summary.DrawRect.Height * 0.5);
      this.summary.vLocation.X -= 10f * BaseScale;
      this.summary.vLocation.Y += 10f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.simpletext = new SimpleTextHandler(BODYTEXT, 100f, _Scale: BaseScale, AutoComplete: true);
      this.simpletext.SetAllColours(ColourData.Z_Cream);
      this.simpletext.Location.X = this.frame.VSCale.X * -0.5f;
      this.simpletext.Location.X += 10f * BaseScale;
      this.heading = new MiniHeading(this.frame.VSCale, Headingg, 1f, BaseScale);
    }

    public Vector2 GetSize() => this.frame.VSCale;

    public void UpdateAnimalsImpacted(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset += this.location;
      if (!this.summary.UpdateLittleSummaryButton(DeltaTime, player, this.frame.location + Offset))
        return;
      this.SHowingInfo = !this.SHowingInfo;
    }

    public void DrawAnimalsImpacted(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.location;
      this.frame.DrawCustomerFrame(Offset, spritebatch);
      this.heading.DrawMiniHeading(this.frame.location + Offset);
      this.summary.DrawLittleSummaryButton(this.frame.location + Offset, spritebatch);
      if (this.SHowingInfo)
        this.simpletext.DrawSimpleTextHandler(this.frame.location + Offset, 1f, spritebatch);
      else
        this.ListOfAnimals.DrawVariableAnimalList(Offset, spritebatch);
    }
  }
}
