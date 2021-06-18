// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.LeftANimalSummary.MiniAnimalSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.LeftANimalSummary
{
  internal class MiniAnimalSummary
  {
    private CustomerFrame customerframe;
    public Vector2 Location;
    private AnimalAndCount animalandcount;

    public MiniAnimalSummary(
      Vector2 MasterScale,
      PrisonZone SelectedEnclosure,
      AnimalType animal,
      int _TotalBabies,
      int _TotalAdults,
      float BaseScale = -1f)
    {
      if ((double) BaseScale == -1.0)
        return;
      this.animalandcount = new AnimalAndCount(animal, _TotalAdults, _TotalBabies, new Vector2(150f * BaseScale, 150f * BaseScale), BaseScale);
      this.customerframe = new CustomerFrame(new Vector2(BaseScale * 150f, BaseScale * 190f * Sengine.ScreenRatioUpwardsMultiplier.Y), true, BaseScale);
    }

    public void SetSatiation(float SatiationValue) => this.animalandcount.SetSatiation(SatiationValue);

    public void setNutrition(float NutritionValue) => this.animalandcount.setNutrition(NutritionValue);

    public void UpdateMiniAnimalSummary()
    {
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void DrawMiniAnimalSummary(Vector2 Offset)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, AssetContainer.pointspritebatchTop05);
      Offset += this.customerframe.frame.vLocation;
      Offset.Y -= this.customerframe.VSCale.Y * 0.5f;
      this.animalandcount.DrawAnimalAndCount(Offset);
    }
  }
}
