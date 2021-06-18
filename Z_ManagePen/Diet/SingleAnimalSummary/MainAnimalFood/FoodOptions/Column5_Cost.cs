// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodOptions.Column5_Cost
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodOptions
{
  internal class Column5_Cost
  {
    private TopTextMini toptextmini;
    private TopTextMini MidText;
    public Vector2 Location;
    private float CostPerUnit;
    private float TotalAnimals;
    private float TotalBabies;
    public float TotalUnitsPerDay;
    public float COST;

    public Column5_Cost(
      float HeightForText,
      float HeightForMiddleText,
      AnimalFoodType animalfood,
      float CurrentFoodPerAnimal,
      int _TotalAnimals,
      int _TotalBabies,
      float BaseScale)
    {
      this.TotalAnimals = (float) _TotalAnimals;
      this.TotalBabies = (float) _TotalBabies;
      this.CostPerUnit = (float) AnimalFoodData.GetAnimalFoodInfo(animalfood).Cost;
      this.toptextmini = new TopTextMini("Cost", BaseScale, HeightForText);
      this.TotalUnitsPerDay = CurrentFoodPerAnimal * (this.TotalAnimals - this.TotalBabies * 0.5f);
      this.COST = this.CostPerUnit * this.TotalUnitsPerDay;
      this.MidText = new TopTextMini("$" + Z_GameFlags.GetCostAsDOllarsAndCents(this.COST) + " a day", BaseScale, HeightForMiddleText);
      this.MidText.SetAsSplit();
    }

    public void SetNewCost(float CurrentFoodPerAnimal)
    {
      this.TotalUnitsPerDay = CurrentFoodPerAnimal * (this.TotalAnimals - this.TotalBabies * 0.5f);
      this.COST = (float) ((double) this.CostPerUnit * (double) CurrentFoodPerAnimal * ((double) this.TotalAnimals - (double) this.TotalBabies * 0.5));
      this.MidText.SetNewText("$" + Z_GameFlags.GetCostAsDOllarsAndCents(this.COST) + " a day");
      this.MidText.SetAsSplit();
    }

    public void DrawColumn5_Cost(Vector2 Offset)
    {
      Offset += this.Location;
      this.toptextmini.DrawTopTextMini(Offset);
      this.MidText.DrawTopTextMini(Offset);
    }
  }
}
