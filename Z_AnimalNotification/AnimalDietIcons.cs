// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalNotification.AnimalDietIcons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_AnimalNotification
{
  internal class AnimalDietIcons
  {
    private List<AnimalFoodIconWithFrame> icons = new List<AnimalFoodIconWithFrame>();
    private UIScaleHelper uiScale;
    private float basescale;
    public Vector2 location = Vector2.Zero;
    private float padX10;

    public AnimalDietIcons(
      AnimalType animal,
      float basescale_,
      Player player,
      bool indicateFoodStockAvailability = true,
      PrisonerInfo displayForThisAnimalDiet = null)
    {
      this.basescale = basescale_;
      FoodCollection foodCollection = AnimalFoodData.GetFoodCollection(animal);
      this.uiScale = new UIScaleHelper(basescale_);
      this.padX10 = this.uiScale.ScaleX(10f);
      Vector2 zero = Vector2.Zero;
      FoodSet foodSet = (FoodSet) null;
      if (displayForThisAnimalDiet != null)
      {
        int Cell_UID;
        player.prisonlayout.cellblockcontainer.GetThisAnimal(displayForThisAnimalDiet.intakeperson.UID, out Cell_UID);
        foodSet = player.prisonlayout.GetThisCellBlock(Cell_UID).prisonercontainer.FoodForAnimals.GetThisSet(displayForThisAnimalDiet.intakeperson.animaltype);
      }
      for (int index = 0; index < foodCollection.animalfoodentry.Count; ++index)
      {
        AnimalFoodData.GetAnimalFoodInfo(foodCollection.animalfoodentry[index].foodtype);
        bool isTick = Game1.Rnd.Next(0, 2) == 1;
        bool flag = false;
        if (foodSet != null && (double) foodSet.FoodUnitsPerDay[index] <= 0.0)
        {
          flag = true;
          indicateFoodStockAvailability = false;
        }
        AnimalFoodIconWithFrame foodIconWithFrame = new AnimalFoodIconWithFrame(foodCollection.animalfoodentry[index].foodtype, basescale_, indicateFoodStockAvailability, isTick);
        foodIconWithFrame.location.X = zero.X;
        zero.X += foodIconWithFrame.GetSize().X;
        zero.X += this.padX10;
        if (flag)
          foodIconWithFrame.SetIsGreyedOut();
        this.icons.Add(foodIconWithFrame);
      }
      Vector2 size = this.GetSize();
      float num = (float) (0.5 * ((double) this.icons[0].GetSize().X + (double) this.padX10 - (double) size.X));
      foreach (AnimalFoodIconWithFrame icon in this.icons)
        icon.location.X += num;
    }

    public Vector2 GetSize()
    {
      Vector2 vector2 = new Vector2();
      foreach (AnimalFoodIconWithFrame icon in this.icons)
      {
        vector2.X += icon.GetSize().X;
        vector2.X += this.padX10;
        vector2.Y = Math.Max(vector2.Y, icon.GetSize().Y);
      }
      return vector2;
    }

    public void DrawAnimalDietIcons(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      foreach (AnimalFoodIconWithFrame icon in this.icons)
        icon.DrawAnimalFoodIconWithFrame(offset, spritebatch);
    }
  }
}
