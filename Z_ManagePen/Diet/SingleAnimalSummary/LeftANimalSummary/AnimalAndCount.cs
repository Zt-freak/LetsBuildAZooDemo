// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.LeftANimalSummary.AnimalAndCount
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared.FoodBars;

namespace TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.LeftANimalSummary
{
  internal class AnimalAndCount
  {
    private AnimalInFrame animalinframe;
    private GameObject StatusTExt;
    public Vector2 Location;
    private Vector2 InternalOffset;
    private string Animal;
    private Vector2 LeftLocation;
    private int TotalInPen;
    private int TotalBabies;
    private NutritionBar nutrition;
    private NutritionBar satiation;
    private float BaseScale;
    private string dietString;

    public AnimalAndCount(
      AnimalType animal,
      int _TotalInPen,
      int _TotalBabies,
      Vector2 SubFrameSize,
      float _BaseScale = -1f)
    {
      this.BaseScale = _BaseScale;
      this.TotalInPen = _TotalInPen;
      this.TotalBabies = _TotalBabies;
      this.StatusTExt = new GameObject();
      this.StatusTExt.scale = this.BaseScale * 2f;
      this.StatusTExt.SetAllColours(ColourData.Z_Cream);
      this.StatusTExt.vLocation.X = -10f * this.BaseScale;
      this.StatusTExt.vLocation.Y = -20f * this.BaseScale;
      float TargetSize = 40f * this.BaseScale;
      this.animalinframe = new AnimalInFrame(animal, AnimalType.None, TargetSize: TargetSize, FrameEdgeBuffer: (6f * this.BaseScale), BaseScale: this.BaseScale);
      this.animalinframe.Location.X = SubFrameSize.X * -0.5f;
      this.animalinframe.Location.X += (float) ((double) TargetSize * 0.5 + 10.0 * (double) this.BaseScale);
      this.Animal = EnemyData.GetEnemyTypeName(animal);
      this.InternalOffset.Y = (float) ((double) TargetSize * 0.5 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y + 10.0 * (double) this.BaseScale * (double) Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.LeftLocation = new Vector2((float) ((double) SubFrameSize.X * -0.5 + 10.0 * (double) this.BaseScale), (float) ((double) TargetSize * 0.5 + 10.0 * (double) this.BaseScale));
      this.nutrition = new NutritionBar(true, this.BaseScale);
      this.satiation = new NutritionBar(false, this.BaseScale);
      this.nutrition.vLocation.X = SubFrameSize.X * 0.5f;
      this.satiation.vLocation.X = SubFrameSize.X * 0.5f;
      this.dietString = AnimalData.GetDietTypeToString(AnimalData.GetAnimalStat(animal).diettype);
    }

    public void SetSatiation(float SatiationValue) => this.satiation.SetFullness(SatiationValue);

    public void setNutrition(float NutritionValue) => this.nutrition.SetFullness(NutritionValue);

    public void DrawAnimalAndCount(Vector2 Offset)
    {
      Offset += this.Location;
      Offset += this.InternalOffset;
      this.animalinframe.DrawAnimalInFrame(Offset);
      TextFunctions.DrawTextWithDropShadow("ANIMAL", this.StatusTExt.scale * 0.5f, this.StatusTExt.vLocation + Offset, this.StatusTExt.GetColour(), this.StatusTExt.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false);
      TextFunctions.DrawTextWithDropShadow(this.Animal, this.StatusTExt.scale * 0.5f, this.StatusTExt.vLocation + Offset + new Vector2(0.0f, 15f * this.BaseScale), this.StatusTExt.GetColour(), this.StatusTExt.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05, false);
      TextFunctions.DrawTextWithDropShadow("Total adults: ", this.StatusTExt.scale * 0.5f, this.LeftLocation + Offset + new Vector2(0.0f, 5f * this.BaseScale), this.StatusTExt.GetColour(), this.StatusTExt.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05, false);
      TextFunctions.DrawTextWithDropShadow(string.Concat((object) this.TotalInPen), this.StatusTExt.scale, new Vector2(55f, this.LeftLocation.Y) + Offset, this.StatusTExt.GetColour(), this.StatusTExt.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false, true);
      TextFunctions.DrawTextWithDropShadow("Total juveniles: ", this.StatusTExt.scale * 0.5f, this.LeftLocation + Offset + new Vector2(0.0f, 20f * this.BaseScale), this.StatusTExt.GetColour(), this.StatusTExt.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05, false);
      TextFunctions.DrawTextWithDropShadow(string.Concat((object) this.TotalBabies), this.StatusTExt.scale, new Vector2(55f, this.LeftLocation.Y) + new Vector2(0.0f, 20f * this.BaseScale) + Offset, this.StatusTExt.GetColour(), this.StatusTExt.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false, true);
      Offset.Y += 50f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      TextFunctions.DrawTextWithDropShadow("Satiation: ", this.StatusTExt.scale * 0.5f, this.LeftLocation + Offset, this.StatusTExt.GetColour(), this.StatusTExt.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05, false);
      this.satiation.DrawNutritionBar(this.LeftLocation + Offset + new Vector2(0.0f, 0.0f * this.BaseScale));
      Offset.Y += 20f * Sengine.ScreenRatioUpwardsMultiplier.Y * this.BaseScale;
      TextFunctions.DrawTextWithDropShadow("Nutrition: ", this.StatusTExt.scale * 0.5f, this.LeftLocation + Offset, this.StatusTExt.GetColour(), this.StatusTExt.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05, false);
      this.nutrition.DrawNutritionBar(this.LeftLocation + Offset + new Vector2(0.0f, 0.0f * this.BaseScale));
      Offset.Y += 30f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y * this.BaseScale;
      TextFunctions.DrawTextWithDropShadow("TYPE", RenderMath.GetPixelSizeBestMatch(1f), this.LeftLocation + Offset, this.StatusTExt.GetColour(), this.StatusTExt.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false);
      Offset.Y += 10f * Sengine.ScreenRatioUpwardsMultiplier.Y * this.BaseScale;
      TextFunctions.DrawTextWithDropShadow(this.dietString, RenderMath.GetPixelSizeBestMatch(1f), this.LeftLocation + Offset, this.StatusTExt.GetColour(), this.StatusTExt.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05, false);
    }
  }
}
