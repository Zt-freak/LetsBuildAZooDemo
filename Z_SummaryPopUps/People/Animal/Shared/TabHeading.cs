// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal.Shared.TabHeading
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame;

namespace TinyZoo.Z_SummaryPopUps.People.Animal.Shared
{
  internal class TabHeading : GameObject
  {
    private string Heading;

    public TabHeading(
      AnimalViewTabType tabtype,
      AnimalType animalselected,
      Vector2 VSCALE,
      float MasterMult)
    {
      this.vLocation.X = VSCALE.X * -0.5f;
      this.vLocation.X += 10f * MasterMult;
      this.vLocation.Y += 16f * MasterMult;
      this.SetAllColours(ColourData.Z_Cream);
      this.Heading = TabHeading.GetHeaderForThisTab(tabtype, animalselected);
      this.scale = 0.7f * MasterMult;
    }

    public static string GetHeaderForThisTab(AnimalViewTabType tabtype, AnimalType animalselected)
    {
      string str = string.Empty;
      switch (tabtype)
      {
        case AnimalViewTabType.Animal:
          str = "Animal";
          break;
        case AnimalViewTabType.Habitat:
          str = "Enclosure Status";
          break;
        case AnimalViewTabType.FamilyTree:
          str = "Family Tree";
          break;
        case AnimalViewTabType.Profitability:
          str = "Profitability";
          break;
        case AnimalViewTabType.Info:
          str = "Species Info";
          break;
        case AnimalViewTabType.LifeTimeStats:
          str = "LifeTime Stats";
          break;
      }
      return str;
    }

    public void UpdateTabHeading()
    {
    }

    public void DrawTabHeading(Vector2 TopCentre) => TextFunctions.DrawTextWithDropShadow(this.Heading, this.scale, this.vLocation + TopCentre, this.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, true);
  }
}
