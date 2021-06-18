// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.DiseaseDetailView.InfectedAnimals.CurrentInfectionsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.DiseaseDetailView.InfectedAnimals
{
  internal class CurrentInfectionsManager
  {
    public Vector2 Location;
    private CustomerFrame BGframe;
    private AnimalsImpacted animalsimpcated;

    public CurrentInfectionsManager(TinyZoo.Z_Diseases.Disease disease, Player player, float BaseScale)
    {
      List<AnimalType> KnownInfectedAnimals = new List<AnimalType>();
      List<PrisonerInfo> fiagnosedWithThisDisease = player.GetAllAnimalsFiagnosedWithThisDisease(disease.UID);
      for (int index = 0; index < fiagnosedWithThisDisease.Count; ++index)
        KnownInfectedAnimals.Add(fiagnosedWithThisDisease[index].intakeperson.animaltype);
      this.animalsimpcated = new AnimalsImpacted(BaseScale, KnownInfectedAnimals);
    }

    public Vector2 GetSize() => this.animalsimpcated.GetSize();

    public void UpdateCurrentInfectionsManager(Vector2 Offset, float DeltaTime, Player player)
    {
      Offset += this.Location;
      this.animalsimpcated.UpdateAnimalsImpacted(player, DeltaTime, Offset);
    }

    public void DrawCurrentInfectionsManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.animalsimpcated.DrawAnimalsImpacted(Offset, spritebatch);
    }
  }
}
