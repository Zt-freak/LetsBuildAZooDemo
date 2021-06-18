// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFoodManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodDivisionBar;
using TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodOptions;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_SummaryPopUps.People.Animal;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary
{
  internal class MainAnimalFoodManager
  {
    private CustomerFrame customerframe;
    private MiniHeading miniheading;
    private FoodDivideBarManager fooddividebar;
    public FoodSet REF_foodset;
    private FoodCollection foodcollection;
    private FoodOptionsList foodoptionslist;
    public int TotalAnimals;
    public int TotaBabies;
    private Vector2 Heightffset;
    private Vector2 Location;

    public MainAnimalFoodManager(
      Vector2 MasterScale,
      PrisonZone SelectedEnclosure,
      AnimalType animal,
      Player player,
      float MasterMult,
      float BaseScale)
    {
      float num = 0.7f;
      this.customerframe = new CustomerFrame(new Vector2((float) ((double) MasterScale.X * (double) num - (double) AnimalPopUpManager.VerticalBuffer * 1.5), MasterScale.Y - AnimalPopUpManager.VerticalBuffer * 2f), true);
      this.customerframe.frame.vLocation.X = MasterScale.X * (float) ((1.0 - (double) num) * 0.5);
      this.customerframe.frame.vLocation.Y = MasterScale.Y * 0.5f;
      this.customerframe.frame.vLocation.X -= AnimalPopUpManager.VerticalBuffer * 0.25f;
      for (int index = 0; index < SelectedEnclosure.prisonercontainer.prisoners.Count; ++index)
      {
        if (SelectedEnclosure.prisonercontainer.prisoners[index].intakeperson.animaltype == animal)
        {
          ++this.TotalAnimals;
          if (SelectedEnclosure.prisonercontainer.prisoners[index].GetIsABaby())
            ++this.TotaBabies;
        }
      }
      List<PrisonerInfo> prisonerInfoList = new List<PrisonerInfo>();
      for (int index = 0; index < SelectedEnclosure.prisonercontainer.prisoners.Count; ++index)
      {
        if (SelectedEnclosure.prisonercontainer.prisoners[index].intakeperson.animaltype == animal)
          prisonerInfoList.Add(SelectedEnclosure.prisonercontainer.prisoners[index]);
      }
      this.REF_foodset = SelectedEnclosure.prisonercontainer.FoodForAnimals.GetThisSet(animal);
      this.foodcollection = AnimalFoodData.GetFoodCollection(animal);
      float Height = 0.0f;
      this.fooddividebar = new FoodDivideBarManager(this.REF_foodset, player, this.foodcollection, BaseScale, ref Height);
      this.foodoptionslist = new FoodOptionsList(this.customerframe.VSCale, this.foodcollection, this.REF_foodset, this.TotalAnimals, this.TotaBabies, player, BaseScale, ref Height);
      this.customerframe = new CustomerFrame(new Vector2(470f * BaseScale, Height), true, BaseScale);
      this.miniheading = new MiniHeading(this.customerframe.VSCale, "Food Amount (" + (object) this.TotalAnimals + " animals)", MasterMult, BaseScale);
      this.Heightffset.Y = Height * -0.5f;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void SetPosition(Vector2 CenterLocation) => this.Location = CenterLocation;

    public float GetSatiation() => Math.Min(1f, this.fooddividebar.Satiation);

    public void UpdateMainAnimalFoodManager(
      Vector2 Offset,
      float DeltaTime,
      Player player,
      out bool SetSatiationAndNutrition)
    {
      Offset += this.Location;
      SetSatiationAndNutrition = false;
      Offset += this.Heightffset;
      if (this.foodoptionslist.UpdateFoodOptionsList(Offset, DeltaTime, player, this.TotalAnimals, this.TotaBabies))
      {
        this.fooddividebar.ChangeFoodBarValues(this.REF_foodset, this.foodcollection, player);
        SetSatiationAndNutrition = true;
      }
      this.fooddividebar.UpdateFoodDivideBarManager(DeltaTime);
    }

    public void DrawMainAnimalFoodManager(Vector2 Offset)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, AssetContainer.pointspritebatchTop05);
      this.miniheading.DrawMiniHeading(Offset);
      Offset += this.Heightffset;
      this.fooddividebar.DrawFoodDivideBarManager(Offset);
      this.foodoptionslist.DrawFoodOptionsList(Offset);
    }
  }
}
