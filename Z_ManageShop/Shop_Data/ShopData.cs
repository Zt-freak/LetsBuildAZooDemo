// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.Shop_Data.ShopData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.FileInOut;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_ManageShop.FoodIcon;

namespace TinyZoo.Z_ManageShop.Shop_Data
{
  internal class ShopData
  {
    private static NeedFulFillmentEntry[] needfullfilment;
    private static ShopStatsCollection[] shopstats;

    internal static int GetServingCapacity(TILETYPE tiletype) => ShopData.GetShopInfo(tiletype).CustomerCapacity;

    internal static float GetPopularity(TILETYPE tiletype, Player player)
    {
      float num1 = 0.6f;
      switch (tiletype)
      {
        case TILETYPE.SmallGiftShop:
          num1 = 0.5f;
          break;
        case TILETYPE.LionHotDogShop:
          num1 = 0.5f;
          break;
        case TILETYPE.DrinksVendingMachine:
          num1 = 0.3f;
          break;
        case TILETYPE.SnacksVendingMachine:
          num1 = 0.3f;
          break;
        case TILETYPE.ChocolateVendingMachine:
          num1 = 0.2f;
          break;
      }
      List<int> intList = new List<int>();
      for (int index = 0; index < player.shopstatus.shopentries.Count; ++index)
      {
        if (player.shopstatus.shopentries[index].tiletype == tiletype)
          intList.Add(index);
      }
      if (intList.Count > 1)
      {
        float num2 = num1 / (float) intList.Count;
        num1 = num2 + (float) (((double) num1 - (double) num2) * 0.5);
      }
      return num1;
    }

    internal static string WhatDoesThisShopSell(TILETYPE tiletype)
    {
      if (TileData.IsForSouvenir(tiletype))
        return "Souvenirs";
      if (TileData.IsForThirst(tiletype))
        return "Drink";
      return TileData.IsForFood(tiletype) ? "Food" : "NA";
    }

    internal static NeedFulFillmentEntry GetNeedFulfillment(
      FOODTYPE foodtype,
      ShopStockStatus shopstatucpurchased)
    {
      if (ShopData.needfullfilment == null)
        ShopData.needfullfilment = new NeedFulFillmentEntry[121];
      ShopData.needfullfilment[(int) foodtype] = new NeedFulFillmentEntry();
      switch (foodtype)
      {
        case FOODTYPE.HotDog:
        case FOODTYPE.CornDog:
        case FOODTYPE.VeganDog:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[1] -= 0.4f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[3] += 0.15f;
          if (shopstatucpurchased != null && (double) shopstatucpurchased.StockSliderValues[0] < 0.5)
          {
            ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[13] = (float) ((0.5 - (double) shopstatucpurchased.StockSliderValues[0]) * 2.0);
            break;
          }
          break;
        case FOODTYPE.KeyChain:
        case FOODTYPE.FridgeMagnet:
        case FOODTYPE.Mug:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[9] += 0.3f;
          break;
        case FOODTYPE.Hat:
        case FOODTYPE.TShirt:
        case FOODTYPE.Plushie:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[9] += 0.5f;
          break;
        case FOODTYPE.SingleScoop:
        case FOODTYPE.SnowCone:
        case FOODTYPE.Popsicle:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[10] = Math.Min(ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[10] - 0.6f, 0.0f);
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[1] -= 0.1f;
          break;
        case FOODTYPE.Sundae:
        case FOODTYPE.BananaSplit:
        case FOODTYPE.Parfait:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[10] = Math.Min(ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[10] - 0.6f, 0.0f);
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[1] -= 0.15f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[2] -= 0.1f;
          break;
        case FOODTYPE.CoconutJuice:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[2] = -1f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[3] += 0.3f;
          break;
        case FOODTYPE.FruitPunch:
        case FOODTYPE.Mocktail:
        case FOODTYPE.Slurpz:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[2] -= 0.35f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[0] += 0.1f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[3] += 0.3f;
          break;
        case FOODTYPE.JuniorBurger:
        case FOODTYPE.VegetarianBurger:
        case FOODTYPE.CholesterolKingBurger:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[1] -= 0.5f;
          break;
        case FOODTYPE.Cola:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[2] = -1f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[3] += 0.3f;
          if (shopstatucpurchased != null && (double) shopstatucpurchased.StockSliderValues[0] < 0.5)
          {
            ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[13] = (float) ((0.5 - (double) shopstatucpurchased.StockSliderValues[0]) * 1.79999995231628);
            break;
          }
          break;
        case FOODTYPE.Coffee:
        case FOODTYPE.KopiLuwak:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[2] -= 0.5f;
          ++ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[0];
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[3] += 0.15f;
          break;
        case FOODTYPE.Crisps:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[1] -= 0.2f;
          if (shopstatucpurchased != null && (double) shopstatucpurchased.StockSliderValues[0] < 0.5)
          {
            ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[13] = (float) ((0.5 - (double) shopstatucpurchased.StockSliderValues[0]) * 1.60000002384186);
            break;
          }
          break;
        case FOODTYPE.Chocolate:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[1] -= 0.2f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[0] += 0.5f;
          break;
        case FOODTYPE.FruitSlush:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[2] -= 0.5f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[1] -= 0.1f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[0] += 0.5f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[3] += 0.15f;
          break;
        case FOODTYPE.Margarita:
        case FOODTYPE.Hawaiian:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[1] -= 0.4f;
          break;
        case FOODTYPE.PinkCottonCandy:
        case FOODTYPE.AnimalCandy:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[1] -= 0.25f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[0] += 0.3f;
          break;
        case FOODTYPE.Churros:
        case FOODTYPE.FrostedChurros:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[1] -= 0.1f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[0] += 0.3f;
          break;
        case FOODTYPE.PopCorn:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[1] -= 0.2f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[0] += 0.2f;
          break;
        case FOODTYPE.GormetPocorn:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[1] -= 0.2f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[0] += 0.2f;
          break;
        case FOODTYPE.Beer:
        case FOODTYPE.CraftBeer:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[1] += 0.1f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[12] += 0.5f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[3] += 0.45f;
          break;
        case FOODTYPE.Champagne:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[1] -= 0.1f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[12] += 0.5f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[3] += 0.35f;
          break;
        case FOODTYPE.FreedomFries:
        case FOODTYPE.Pretzel:
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[1] -= 0.5f;
          ShopData.needfullfilment[(int) foodtype].SatisfactionModifiers[2] += 0.5f;
          break;
      }
      return ShopData.needfullfilment[(int) foodtype];
    }

    internal static ShopStatsCollection GetShopInfo(TILETYPE shoptype)
    {
      if (ShopData.shopstats == null)
      {
        ShopData.shopstats = new ShopStatsCollection[743];
        CSVFileReader csvFileReader = new CSVFileReader();
        csvFileReader.Read("ShopInventory.csv");
        for (int Row = 1; Row < csvFileReader.GetRowCount(); ++Row)
        {
          TILETYPE stringToShop = ShopData.GetStringToShop(csvFileReader.GetCell(Row, 0));
          if (ShopData.shopstats[(int) stringToShop] == null)
          {
            switch (stringToShop)
            {
              case TILETYPE.SmallGiftShop:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection((RecipeEntry) null);
                ShopData.shopstats[(int) stringToShop].SetUpServing(1);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.FridgeMagnet, 10, 20, "Egg Shell", "Graphene", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetMagnetRecipe();
                break;
              case TILETYPE.LionHotDogShop:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Seasoning", FOODTYPE.MSG));
                ShopData.shopstats[(int) stringToShop].SetUpServing(1);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.HotDog, 1, 4, "Eyes and Tails", "Prime Meat", 65));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetHotDogRecipe();
                break;
              case TILETYPE.ElephantGiftShop:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection((RecipeEntry) null);
                ShopData.shopstats[(int) stringToShop].SetUpServing(2);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.Hat, 10, 20, "Polyester", "Cotton", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetHatRecipe();
                break;
              case TILETYPE.IceCreamTruck:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Seasoning", FOODTYPE.CornSyrup));
                ShopData.shopstats[(int) stringToShop].SetUpServing(1);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.Popsicle, 1, 4, "No Flavoring", "Natural Juice", 65));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetPopsicleRecipe();
                break;
              case TILETYPE.BigIceCreamShop:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Seasoning", FOODTYPE.Sugar));
                ShopData.shopstats[(int) stringToShop].SetUpServing(1);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.Parfait, 1, 4, "Pig Fat", "Cream", 65));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetParfaitRecipe();
                break;
              case TILETYPE.CoconutShop:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Seasoning", FOODTYPE.CornSyrup));
                ShopData.shopstats[(int) stringToShop].SetUpServing(2);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.CoconutJuice, 10, 20, "Rehydrated", "Fresh Coconut", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetCoconutJuiceRecipe();
                break;
              case TILETYPE.DrinksVendingMachine:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Caffeine", FOODTYPE.Caffeine));
                ShopData.shopstats[(int) stringToShop].SetUpServing(1);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.Cola, 10, 5, "Bad Chemicals", "Less Bad Chemicals", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetColaRecipe();
                break;
              case TILETYPE.SnacksVendingMachine:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Seasoning", FOODTYPE.Salt));
                ShopData.shopstats[(int) stringToShop].SetUpServing(1);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.Crisps, 10, 20, "Powdered Starch", "Fresh Potato", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetCrispsRecipe();
                break;
              case TILETYPE.PandaBurgerShop:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Seasoning", FOODTYPE.Salt));
                ShopData.shopstats[(int) stringToShop].SetUpServing(2);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.JuniorBurger, 10, 20, "Dead Zoo Animals", "Dead Farm Animals", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetJuniorBurgerRecipe();
                break;
              case TILETYPE.BalloonShop:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection((RecipeEntry) null);
                ShopData.shopstats[(int) stringToShop].SetUpServing(2);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.AnimalBalloon, 10, 20, "Air", "Helium", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetBalloonRecipe();
                break;
              case TILETYPE.KangarooPizzaShop:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Seasoning", FOODTYPE.ChilliPowder));
                ShopData.shopstats[(int) stringToShop].SetUpServing(2);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.Margarita, 10, 20, "Lettuce", "Basil", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetMargaritaRecipe();
                break;
              case TILETYPE.CottonCandyShop:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Seasoning", FOODTYPE.CornSyrup));
                ShopData.shopstats[(int) stringToShop].SetUpServing(1);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.PinkCottonCandy, 10, 20, "Synthetic Dye", "Natural Coloring", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetPinkCottonCandyRecipe();
                break;
              case TILETYPE.SlushieShop:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Seasoning", FOODTYPE.CornSyrup));
                ShopData.shopstats[(int) stringToShop].SetUpServing(2);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.Slurpz, 10, 20, "Tap Water", "Frozen Spring Water", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetSlurpzRecipe();
                break;
              case TILETYPE.ChurroShop:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Seasoning", FOODTYPE.Sugar));
                ShopData.shopstats[(int) stringToShop].SetUpServing(1);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.Churros, 10, 20, "Starch", "Flour", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetChurrosRecipe();
                break;
              case TILETYPE.RustyKegShop:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection((RecipeEntry) null);
                ShopData.shopstats[(int) stringToShop].SetUpServing(1);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.CraftBeer, 10, 20, "Sewage", "Hops", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetCraftBeerRecipe();
                break;
              case TILETYPE.PopcornWeaselShop:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Salt", FOODTYPE.Salt));
                ShopData.shopstats[(int) stringToShop].SetUpServing(2);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.PopCorn, 10, 20, "Unpopped", "Popped", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetPopcornRecipe();
                break;
              case TILETYPE.KatCoffeeShop:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Seasoning", FOODTYPE.Caffeine));
                ShopData.shopstats[(int) stringToShop].SetUpServing(2);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.KopiLuwak, 10, 20, "Poop", "Luwak", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetKopiRecipe();
                break;
              case TILETYPE.ShellShackShop:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Seasoning", FOODTYPE.Salt));
                ShopData.shopstats[(int) stringToShop].SetUpServing(2);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.FreedomFries, 10, 20, "Powdered Starch", "Potato", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetFreedomFriesRecipe();
                break;
              case TILETYPE.TacoTruck:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Seasoning", FOODTYPE.ChilliPowder));
                ShopData.shopstats[(int) stringToShop].SetUpServing(1);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.BeefTaco, 10, 20, "Worms", "Beef", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetBeefTacoRecipe();
                break;
              case TILETYPE.PretzelShop:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Salt", FOODTYPE.Salt));
                ShopData.shopstats[(int) stringToShop].SetUpServing(2);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.Pretzel, 10, 20, "Monkey Milk", "Cow Milk", 20));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetPretzelRecipe();
                break;
              case TILETYPE.ChocolateVendingMachine:
                ShopData.shopstats[(int) stringToShop] = new ShopStatsCollection(new RecipeEntry("Seasoning", FOODTYPE.Sugar));
                ShopData.shopstats[(int) stringToShop].SetUpServing(1);
                ShopData.shopstats[(int) stringToShop].shopstats.Add(new ShopStatEntry(FOODTYPE.Chocolate, 10, 20, "Palm Oil", "Cocoa Butter", 33));
                ShopData.shopstats[(int) stringToShop].shopstats[0].recipe = ShopData.GetChocolateRecipe();
                break;
            }
          }
          FOODTYPE stringToFood = ShopData.GetSTringToFood(csvFileReader.GetCell(Row, 1));
          if (stringToShop == TILETYPE.RustyKegShop)
          {
            Console.WriteLine("SHOP NOT DONE");
          }
          else
          {
            for (int index = 0; index < ShopData.shopstats[(int) stringToShop].shopstats.Count; ++index)
            {
              if (ShopData.shopstats[(int) stringToShop].shopstats[index].MainItemForSale == stringToFood)
              {
                ShopData.shopstats[(int) stringToShop].shopstats[index].IdealCost = Convert.ToInt32(csvFileReader.GetCell(Row, 2));
                ShopData.shopstats[(int) stringToShop].shopstats[index].MinStockCost = Convert.ToInt32(csvFileReader.GetCell(Row, 3));
                ShopData.shopstats[(int) stringToShop].shopstats[index].MaxSellToPublicCost = Convert.ToInt32(csvFileReader.GetCell(Row, 4));
                ShopData.shopstats[(int) stringToShop].shopstats[index].MinStockCost /= 10;
                ShopData.shopstats[(int) stringToShop].shopstats[index].IdealMin = ShopData.shopstats[(int) stringToShop].shopstats[index].MinStockCost;
                ShopData.shopstats[(int) stringToShop].shopstats[index].IdealMax = ShopData.shopstats[(int) stringToShop].shopstats[index].MaxSellToPublicCost;
              }
            }
          }
        }
        ShopData.MakeRides(TILETYPE.HelicopterRide);
        ShopData.MakeRides(TILETYPE.HotAirBalloonRide);
        ShopData.MakeRides(TILETYPE.CorruptedHotAirBalloonRide);
      }
      return ShopData.shopstats[(int) shoptype];
    }

    private static void MakeRides(TILETYPE thisride)
    {
      ShopData.shopstats[(int) thisride] = new ShopStatsCollection(new RecipeEntry("Excitement!", FOODTYPE.Excitement));
      ShopData.shopstats[(int) thisride].SetUpServing(1);
      ShopData.shopstats[(int) thisride].shopstats.Add(new ShopStatEntry(FOODTYPE.Time, 10, 20, "Length", "Length", 20));
      ShopData.shopstats[(int) thisride].shopstats[0].recipe = ShopData.GetBeefTacoRecipe();
    }

    private static RecipeList GetHotDogRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Meat", FOODTYPE.EyesAndTails, FOODTYPE.PrimeMeat)
        {
          Cost = 0.5f,
          premiumCost = 2f
        }
      }
    };

    private static RecipeList GetCornDogRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Oil", FOODTYPE.SaturatedMeatOil, FOODTYPE.AvocadoOil)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("Seasoning", FOODTYPE.CornSyrup)
      }
    };

    private static RecipeList GetColaRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Chemicals", FOODTYPE.BadChemicals, FOODTYPE.LessBadChemicals)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("Sugar", FOODTYPE.Sugar)
      }
    };

    private static RecipeList GetCoffeeRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Beans", FOODTYPE.Dirt, FOODTYPE.ArabicaBeans)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("Caffiene", FOODTYPE.Caffeine)
      }
    };

    private static RecipeList GetCrispsRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Carbs", FOODTYPE.Starch, FOODTYPE.Potato)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("Salt", FOODTYPE.Salt)
      }
    };

    private static RecipeList GetChocolateRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Goodness", FOODTYPE.PalmOil, FOODTYPE.CocoButter)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("FDA Approved Energy Compound", FOODTYPE.Molly)
      }
    };

    private static RecipeList GetVeganDogRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Meat Substitute", FOODTYPE.Cardboard, FOODTYPE.Carrot)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("Seasoning", FOODTYPE.Salt)
      }
    };

    private static RecipeList GetMagnetRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Material", FOODTYPE.Plastic, FOODTYPE.Enamel)
        {
          Cost = 0.5f,
          premiumCost = 2f
        }
      }
    };

    private static RecipeList GetMugRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Durability", FOODTYPE.EggShell, FOODTYPE.Graphene)
        {
          Cost = 0.5f
        }
      }
    };

    private static RecipeList GetKeyChainRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Material", FOODTYPE.Rubber, FOODTYPE.PlasticKeyChain)
        {
          Cost = 0.5f,
          premiumCost = 2f
        }
      }
    };

    private static RecipeList GetMargaritaRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Ingredients", FOODTYPE.Lettuce, FOODTYPE.Basil)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("MSG", FOODTYPE.MSG)
      }
    };

    private static RecipeList GetHawaiianRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Choice", FOODTYPE.NoPinapple, FOODTYPE.Pinapple)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("MSG", FOODTYPE.MSG)
      }
    };

    private static RecipeList GetPinkCottonCandyRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Food Dye", FOODTYPE.SyntheticDye, FOODTYPE.NaturalCOlouring)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("Energy", FOODTYPE.EnergyChemicals)
      }
    };

    private static RecipeList GetAnimalCandyRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Animal Candy", FOODTYPE.CottonWool, FOODTYPE.Candy)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("Cuteness", FOODTYPE.Cuteness)
      }
    };

    private static RecipeList GetSlurpzRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Water", FOODTYPE.TapWater, FOODTYPE.FrozenSpringWater)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("Energy", FOODTYPE.EnergyChemicals)
      }
    };

    private static RecipeList GetBubbleTeaRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Balls", FOODTYPE.RabbitBalls, FOODTYPE.TapiocaBalls)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("Caffeine", FOODTYPE.Caffeine)
      }
    };

    private static RecipeList GetHatRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Material", FOODTYPE.Polyester, FOODTYPE.Cotton)
        {
          Cost = 0.5f,
          premiumCost = 2f
        }
      }
    };

    private static RecipeList GetSingleScoopRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Cream Substitute", FOODTYPE.PigFat, FOODTYPE.Cream)
        {
          Cost = 0.5f,
          premiumCost = 2f
        }
      }
    };

    private static RecipeList GetSnowConeRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Water", FOODTYPE.GroundSnow, FOODTYPE.FrozenMineralWater)
        {
          Cost = 0.5f,
          premiumCost = 2f
        }
      }
    };

    internal static RecipeList GetPopsicleRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Flavor", FOODTYPE.NoFlavouring, FOODTYPE.NaturalJuice)
        {
          Cost = 0.5f,
          premiumCost = 2f
        }
      }
    };

    private static RecipeList GetJuniorBurgerRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Meat", FOODTYPE.DeadZooAnimals, FOODTYPE.DeadFarmAnimals)
        {
          Cost = 0.5f,
          premiumCost = 2f
        }
      }
    };

    private static RecipeList GetVegeterianBurgerRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Meat Substitute", FOODTYPE.Meat, FOODTYPE.NotMeat)
        {
          Cost = 0.5f,
          premiumCost = 2f
        }
      }
    };

    private static RecipeList GetCholesterolKingBurgerRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Cheese", FOODTYPE.CandleWax, FOODTYPE.ProcessedCheese)
        {
          Cost = 0.5f,
          premiumCost = 2f
        }
      }
    };

    private static RecipeList GetCoconutJuiceRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Freshness", FOODTYPE.Rehydrated, FOODTYPE.FreshCoconut)
        {
          Cost = 0.5f,
          premiumCost = 2f
        }
      }
    };

    private static RecipeList GetParfaitRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Ingredients", FOODTYPE.PigFat, FOODTYPE.Cream)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("Sugar", FOODTYPE.Sugar)
      }
    };

    private static RecipeList GetBalloonRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Filling", FOODTYPE.Air, FOODTYPE.Helium)
        {
          Cost = 0.5f,
          premiumCost = 2f
        }
      }
    };

    private static RecipeList GetChurrosRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Mix", FOODTYPE.Starch, FOODTYPE.Flour)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("Sugar", FOODTYPE.Sugar)
      }
    };

    private static RecipeList GetFrostedChurrosRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Mix", FOODTYPE.PigFat, FOODTYPE.Dulcedeleche)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("FDA Approved Energy Compound", FOODTYPE.Molly)
      }
    };

    private static RecipeList GetPopcornRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Popcorn", FOODTYPE.Unpopped, FOODTYPE.Popped)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("Salt", FOODTYPE.Salt)
      }
    };

    private static RecipeList GetKopiRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Kopi Luwak", FOODTYPE.Poop, FOODTYPE.Luwak)
        {
          Cost = 0.5f,
          premiumCost = 2f
        }
      }
    };

    private static RecipeList GetCraftBeerRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Recipe", FOODTYPE.Sewage, FOODTYPE.Hops)
        {
          Cost = 0.5f,
          premiumCost = 2f
        }
      }
    };

    private static RecipeList GetFreedomFriesRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Freedom Fries", FOODTYPE.Starch, FOODTYPE.Potato)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("Salt", FOODTYPE.Salt)
      }
    };

    private static RecipeList GetPretzelRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Pretzel", FOODTYPE.MonkeyMilk, FOODTYPE.CowMilk)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("Salt", FOODTYPE.Salt)
      }
    };

    private static RecipeList GetBeefTacoRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Beef Taco", FOODTYPE.Worms, FOODTYPE.Beef)
        {
          Cost = 0.5f,
          premiumCost = 2f
        },
        new RecipeEntry("Chilli", FOODTYPE.ChilliPowder)
      }
    };

    private static RecipeList GetPlaceholderRecipe() => new RecipeList()
    {
      recipies = {
        new RecipeEntry("Placeholder", FOODTYPE.Placeholder, FOODTYPE.Placeholder)
        {
          Cost = 0.5f,
          premiumCost = 2f
        }
      }
    };

    internal static TILETYPE GetStringToShop(string ShopName)
    {
      switch (ShopName)
      {
        case "BalloonShop":
          return TILETYPE.BalloonShop;
        case "BigIceCreamShop":
          return TILETYPE.BigIceCreamShop;
        case "BlackVendingMachine":
          return TILETYPE.DrinksVendingMachine;
        case "ChocolateVendingMachine":
          return TILETYPE.ChocolateVendingMachine;
        case "ChurroShop":
          return TILETYPE.ChurroShop;
        case "CoconutShop":
          return TILETYPE.CoconutShop;
        case "CottonCandyShop":
          return TILETYPE.CottonCandyShop;
        case "ElephantGiftShop":
          return TILETYPE.ElephantGiftShop;
        case "HelicopterRide":
          return TILETYPE.HelicopterRide;
        case "HotAirBalloonRide":
          return TILETYPE.HotAirBalloonRide;
        case "IceCreamTruck":
          return TILETYPE.IceCreamTruck;
        case "KangarooPizzaShop":
          return TILETYPE.KangarooPizzaShop;
        case "KatCoffeeShop":
          return TILETYPE.KatCoffeeShop;
        case "LionHotDogShop":
          return TILETYPE.LionHotDogShop;
        case "PandaBurgerShop":
          return TILETYPE.PandaBurgerShop;
        case "PopGoesTheWeasel":
          return TILETYPE.PopcornWeaselShop;
        case "PretzelShop":
          return TILETYPE.PretzelShop;
        case "RedVendingMachine":
          return TILETYPE.SnacksVendingMachine;
        case "ShellShackShop":
          return TILETYPE.ShellShackShop;
        case "SlushieShop":
          return TILETYPE.SlushieShop;
        case "SmallGiftShop":
          return TILETYPE.SmallGiftShop;
        case "TacoTruck":
          return TILETYPE.TacoTruck;
        case "TheRustyKeg":
          return TILETYPE.RustyKegShop;
        default:
          throw new Exception("No Way");
      }
    }

    internal static int GetMaximumEmployeesForThisShop(TILETYPE shoptype, Player player)
    {
      switch (shoptype)
      {
        case TILETYPE.DrinksVendingMachine:
        case TILETYPE.SnacksVendingMachine:
        case TILETYPE.ChocolateVendingMachine:
          return 0;
        case TILETYPE.ArchitectOffice:
          return Player.currentActiveResearchBonuses.TypesOfUpgradeAndLevel[1] > 0 ? 2 : 1;
        default:
          return 1;
      }
    }

    internal static FOODTYPE GetSTringToFood(string Text)
    {
      switch (Text)
      {
        case "AnimalBalloon":
          return FOODTYPE.AnimalBalloon;
        case "AnimalCandy":
          return FOODTYPE.AnimalCandy;
        case "Balloon":
          return FOODTYPE.Balloon;
        case "BananaSplit":
          return FOODTYPE.BananaSplit;
        case "BeefTaco":
          return FOODTYPE.BeefTaco;
        case "Beer":
          return FOODTYPE.Beer;
        case "Champagne":
          return FOODTYPE.Champagne;
        case "ChocolateBar":
          return FOODTYPE.Chocolate;
        case "CholesterolKingBurger":
          return FOODTYPE.CholesterolKingBurger;
        case "Churros":
          return FOODTYPE.Churros;
        case "CoconutJuice":
          return FOODTYPE.CoconutJuice;
        case "Coffee":
          return FOODTYPE.Coffee;
        case "Cola":
          return FOODTYPE.Cola;
        case "CornDog":
          return FOODTYPE.CornDog;
        case "CraftBeer":
          return FOODTYPE.CraftBeer;
        case "FreedomFries":
          return FOODTYPE.FreedomFries;
        case "FridgeMagnet":
          return FOODTYPE.FridgeMagnet;
        case "FrostedChurros":
          return FOODTYPE.FrostedChurros;
        case "FruitPunch":
          return FOODTYPE.FruitPunch;
        case "FruitSlush":
          return FOODTYPE.FruitSlush;
        case "GormetPopcorn":
          return FOODTYPE.GormetPocorn;
        case "Hat":
          return FOODTYPE.Hat;
        case "Hawaiian":
          return FOODTYPE.Hawaiian;
        case "HotDog":
          return FOODTYPE.HotDog;
        case "JuniorBurger":
          return FOODTYPE.JuniorBurger;
        case "KeyChain":
          return FOODTYPE.KeyChain;
        case "KopiLuwak":
          return FOODTYPE.KopiLuwak;
        case "Margarita":
          return FOODTYPE.Margarita;
        case "Mocktail":
          return FOODTYPE.Mocktail;
        case "Mug":
          return FOODTYPE.Mug;
        case "Parfait":
          return FOODTYPE.Parfait;
        case "PinkCottonCandy":
          return FOODTYPE.PinkCottonCandy;
        case "Placeholder":
          return FOODTYPE.Placeholder;
        case "Plushie":
          return FOODTYPE.Plushie;
        case "Popcorn":
          return FOODTYPE.PopCorn;
        case "Popsicle":
          return FOODTYPE.Popsicle;
        case "PotatoChips":
          return FOODTYPE.Crisps;
        case "Pretzel":
          return FOODTYPE.Pretzel;
        case "SingleScoop":
          return FOODTYPE.SingleScoop;
        case "Slurpz":
          return FOODTYPE.Slurpz;
        case "SnowCone":
          return FOODTYPE.SnowCone;
        case "Sundae":
          return FOODTYPE.Sundae;
        case "TShirt":
          return FOODTYPE.TShirt;
        case "VeganDog":
          return FOODTYPE.VeganDog;
        case "VegetarianBurger":
          return FOODTYPE.VegetarianBurger;
        default:
          return FOODTYPE.Coffee;
      }
    }
  }
}
