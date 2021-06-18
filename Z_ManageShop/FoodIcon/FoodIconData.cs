// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.FoodIcon.FoodIconData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_ManageShop.FoodIcon
{
  internal class FoodIconData
  {
    internal static string GetExtarInredientDescription(FOODTYPE foodtype)
    {
      switch (foodtype)
      {
        case FOODTYPE.ChilliPowder:
          return "Some people love chilli, some people dont. When added in just the right ammount, all that heat can lead to people seeking out something to cool their mouth down.";
        case FOODTYPE.CornSyrup:
          return "Corn Syrup's increased sweetness offers a medium kick of energy, however, this sweeter taste can easily lead to nausea.";
        case FOODTYPE.Salt:
          return "Adding extra salt can make a dish taste better, adding more salt makes peopl thirsty! Eating too much salt without having anything to drink will lead to nausea.";
        case FOODTYPE.Sugar:
          return "Sugar offers a small kick of energy, but can lead to nausea, Diabetics will be affected however, more experienced staff will better understand their needs.";
        case FOODTYPE.Caffeine:
          return "While this helps maximise profits by getting your visitors to move around the zoo at faster speeds, in some cases it may also lead to heart attacks and other side effects";
        case FOODTYPE.MSG:
          return "Nothing masks bad tasting food quite like MSG, it also contributes to thirst.";
        default:
          return "NOT FOUND";
      }
    }

    internal static string GetFoodTypeToString(FOODTYPE foodtype)
    {
      switch (foodtype)
      {
        case FOODTYPE.ZooTicket:
          return "Entry Ticket";
        case FOODTYPE.HotDog:
          return "Hot Dog";
        case FOODTYPE.CornDog:
          return "Corn Dog";
        case FOODTYPE.VeganDog:
          return "Vegan Dog";
        case FOODTYPE.KeyChain:
          return "Key Chain";
        case FOODTYPE.FridgeMagnet:
          return "Magnet";
        case FOODTYPE.Mug:
          return "Mug";
        case FOODTYPE.Hat:
          return "Hat";
        case FOODTYPE.TShirt:
          return "T-Shirt";
        case FOODTYPE.Plushie:
          return "Plush";
        case FOODTYPE.SingleScoop:
          return "Single Scoop Ice Cream";
        case FOODTYPE.SnowCone:
          return "Snow Cone";
        case FOODTYPE.Popsicle:
          return "Popsicle";
        case FOODTYPE.Sundae:
          return "Sundae";
        case FOODTYPE.BananaSplit:
          return "Banana Split";
        case FOODTYPE.Parfait:
          return "Parfait";
        case FOODTYPE.CoconutJuice:
          return "Coconut Juice";
        case FOODTYPE.FruitPunch:
          return "Hawaiian Fruit Punch";
        case FOODTYPE.Mocktail:
          return "Non-alcoholic Cocktail";
        case FOODTYPE.JuniorBurger:
          return "CheeseBurger";
        case FOODTYPE.VegetarianBurger:
          return "Barbeque Burger";
        case FOODTYPE.CholesterolKingBurger:
          return "Cholesterol King Burger";
        case FOODTYPE.EyesAndTails:
          return "Eyes & Tails";
        case FOODTYPE.PrimeMeat:
          return "Prime Meat";
        case FOODTYPE.ChilliPowder:
          return "Chilli Powder";
        case FOODTYPE.SaturatedMeatOil:
          return "Saturated Meat Oil";
        case FOODTYPE.AvocadoOil:
          return "Avocado Oil";
        case FOODTYPE.CornSyrup:
          return "Corn Syrup";
        case FOODTYPE.Cardboard:
          return "Cardboard";
        case FOODTYPE.Carrot:
          return "Carrot";
        case FOODTYPE.Salt:
          return "Salt";
        case FOODTYPE.Plastic:
          return "Plastic";
        case FOODTYPE.Enamel:
          return "Enamel";
        case FOODTYPE.EggShell:
          return "EggShell";
        case FOODTYPE.Graphene:
          return "Graphene";
        case FOODTYPE.Rubber:
          return "Rubber";
        case FOODTYPE.PlasticKeyChain:
          return "Plastic";
        case FOODTYPE.PigFat:
          return "Pig Fat";
        case FOODTYPE.Cream:
          return "Cream";
        case FOODTYPE.FrozenMineralWater:
          return "Frozen Mineral Water";
        case FOODTYPE.NoFlavouring:
          return "None";
        case FOODTYPE.NaturalJuice:
          return "Natural Juice";
        case FOODTYPE.Rehydrated:
          return "Rehydrated";
        case FOODTYPE.FreshCoconut:
          return "Fresh Coconut";
        case FOODTYPE.DeadZooAnimals:
          return "Zoo Animals";
        case FOODTYPE.DeadFarmAnimals:
          return "Farm Animals";
        case FOODTYPE.Meat:
          return "Meat";
        case FOODTYPE.NotMeat:
          return "Not Meat";
        case FOODTYPE.CandleWax:
          return "Candle Wax";
        case FOODTYPE.ProcessedCheese:
          return "Processed Cheese";
        case FOODTYPE.Cola:
          return "Cola";
        case FOODTYPE.BadChemicals:
          return "Bad";
        case FOODTYPE.LessBadChemicals:
          return "Less Bad";
        case FOODTYPE.Sugar:
          return "Sugar";
        case FOODTYPE.Coffee:
          return "Coffee";
        case FOODTYPE.Dirt:
          return "Dirt";
        case FOODTYPE.ArabicaBeans:
          return "Arabica Beans";
        case FOODTYPE.Caffeine:
          return "Caffeine";
        case FOODTYPE.Crisps:
          return "Crisps";
        case FOODTYPE.Starch:
          return "Starch";
        case FOODTYPE.Potato:
          return "Potato";
        case FOODTYPE.Chocolate:
          return "Chocolate";
        case FOODTYPE.PalmOil:
          return "Palm Oil";
        case FOODTYPE.CocoButter:
          return "Coco Butter";
        case FOODTYPE.Molly:
          return "Molly";
        case FOODTYPE.Slurpz:
          return "Slurpz";
        case FOODTYPE.TapWater:
          return "Tap";
        case FOODTYPE.FrozenSpringWater:
          return "Spring";
        case FOODTYPE.EnergyChemicals:
          return "Energy Chemicals";
        case FOODTYPE.FruitSlush:
          return "Bubble Tea";
        case FOODTYPE.RabbitBalls:
          return "Rabbit Balls";
        case FOODTYPE.TapiocaBalls:
          return "Tapioca Balls";
        case FOODTYPE.Margarita:
          return "Margherita ";
        case FOODTYPE.Lettuce:
          return "Lettuce";
        case FOODTYPE.Basil:
          return "Basil";
        case FOODTYPE.MSG:
          return "MSG";
        case FOODTYPE.Hawaiian:
          return "Hawaiian";
        case FOODTYPE.NoPinapple:
          return "NoPinapple";
        case FOODTYPE.Pinapple:
          return "Pinapple";
        case FOODTYPE.PinkCottonCandy:
          return "Cotton Candy";
        case FOODTYPE.SyntheticDye:
          return "Synthetic";
        case FOODTYPE.NaturalCOlouring:
          return "Natural";
        case FOODTYPE.AnimalCandy:
          return "Cotton Wool";
        case FOODTYPE.Candy:
          return "Candy";
        case FOODTYPE.Cuteness:
          return "Cuteness";
        case FOODTYPE.Balloon:
          return "Balloon";
        case FOODTYPE.Air:
          return "Air";
        case FOODTYPE.Helium:
          return "Helium";
        case FOODTYPE.AnimalBalloon:
          return "Animal Balloon";
        case FOODTYPE.Churros:
          return "Churros";
        case FOODTYPE.Flour:
          return "Flour";
        case FOODTYPE.FrostedChurros:
          return "Frosted Churros";
        case FOODTYPE.Dulcedeleche:
          return "Dulce de leche";
        case FOODTYPE.Placeholder:
          return "PLaceholder";
        case FOODTYPE.PopCorn:
          return "Popcorn";
        case FOODTYPE.Unpopped:
          return "Unpopped";
        case FOODTYPE.Popped:
          return "Popped";
        case FOODTYPE.CraftBeer:
          return "Beer";
        case FOODTYPE.Sewage:
          return "Sewage";
        case FOODTYPE.Hops:
          return "Hops";
        case FOODTYPE.KopiLuwak:
          return "Kopi Luwak";
        case FOODTYPE.Poop:
          return "Poop";
        case FOODTYPE.Luwak:
          return "Luwak";
        case FOODTYPE.FreedomFries:
          return "Freedom Fries";
        case FOODTYPE.Pretzel:
          return "Pretzel";
        case FOODTYPE.MonkeyMilk:
          return "Monkey Milk";
        case FOODTYPE.CowMilk:
          return "Cow Milk";
        case FOODTYPE.BeefTaco:
          return "Beef Taco";
        case FOODTYPE.Worms:
          return "Worms";
        case FOODTYPE.Beef:
          return "Beef";
        case FOODTYPE.Polyester:
          return "Polyester";
        case FOODTYPE.Cotton:
          return "Cotton";
        default:
          return "NOPE";
      }
    }

    public static Rectangle GetFoodRectangle(FOODTYPE foodtype)
    {
      switch (foodtype)
      {
        case FOODTYPE.HotDog:
          return new Rectangle(0, 996, 29, 28);
        case FOODTYPE.CornDog:
          return new Rectangle(0, 961, 30, 34);
        case FOODTYPE.VeganDog:
          return new Rectangle(30, 990, 32, 34);
        case FOODTYPE.KeyChain:
          return new Rectangle(0, 925, 32, 35);
        case FOODTYPE.FridgeMagnet:
          return new Rectangle(63, 993, 34, 31);
        case FOODTYPE.Mug:
          return new Rectangle(31, 960, 29, 29);
        case FOODTYPE.Hat:
          return new Rectangle(33, 926, 35, 33);
        case FOODTYPE.TShirt:
          return new Rectangle(61, 960, 34, 32);
        case FOODTYPE.Plushie:
          return new Rectangle(69, 927, 31, 32);
        case FOODTYPE.SingleScoop:
          return new Rectangle(92, 853, 30, 37);
        case FOODTYPE.SnowCone:
          return new Rectangle(123, 853, 29, 37);
        case FOODTYPE.Popsicle:
          return new Rectangle(170, 957, 35, 35);
        case FOODTYPE.Sundae:
          return new Rectangle(206, 956, 33, 36);
        case FOODTYPE.BananaSplit:
          return new Rectangle(167, 921, 42, 34);
        case FOODTYPE.Parfait:
          return new Rectangle(240, 954, 34, 38);
        case FOODTYPE.CoconutJuice:
          return new Rectangle(210, 924, 33, 31);
        case FOODTYPE.FruitPunch:
          return new Rectangle(17, 815, 34, 33);
        case FOODTYPE.Mocktail:
          return new Rectangle(52, 819, 27, 34);
        case FOODTYPE.JuniorBurger:
          return new Rectangle(140, 891, 30, 29);
        case FOODTYPE.VegetarianBurger:
          return new Rectangle(171, 891, 29, 29);
        case FOODTYPE.CholesterolKingBurger:
          return new Rectangle(80, 818, 31, 34);
        case FOODTYPE.EyesAndTails:
          return new Rectangle(98, 987, 40, 37);
        case FOODTYPE.PrimeMeat:
          return new Rectangle(96, 960, 33, 26);
        case FOODTYPE.ChilliPowder:
          return Game1.Rnd.Next(0, 2) == 0 ? new Rectangle(101, 924, 30, 35) : new Rectangle(14, 777, 34, 36);
        case FOODTYPE.SaturatedMeatOil:
          return new Rectangle(139, 989, 33, 35);
        case FOODTYPE.AvocadoOil:
          return new Rectangle(130, 953, 35, 35);
        case FOODTYPE.CornSyrup:
          return new Rectangle(24, 887, 37, 37);
        case FOODTYPE.Cardboard:
          return new Rectangle(62, 891, 46, 34);
        case FOODTYPE.Carrot:
          return new Rectangle(0, 845, 20, 41);
        case FOODTYPE.Salt:
          return new Rectangle(0, 887, 23, 37);
        case FOODTYPE.Plastic:
          return new Rectangle(132, 921, 34, 31);
        case FOODTYPE.Enamel:
          return new Rectangle(21, 849, 37, 37);
        case FOODTYPE.EggShell:
          return new Rectangle(59, 855, 32, 35);
        case FOODTYPE.Graphene:
          return new Rectangle(173, 993, 41, 31);
        case FOODTYPE.Rubber:
          return new Rectangle(215, 993, 41, 31);
        case FOODTYPE.PlasticKeyChain:
          return new Rectangle(109, 891, 30, 30);
        case FOODTYPE.PigFat:
          return new Rectangle(244, 926, 32, 27);
        case FOODTYPE.Cream:
          return new Rectangle(201, 893, 35, 29);
        case FOODTYPE.GroundSnow:
          return new Rectangle(81, 794, 29, 23);
        case FOODTYPE.FrozenMineralWater:
          return new Rectangle(237, 894, 31, 31);
        case FOODTYPE.NoFlavouring:
          return new Rectangle(153, 855, 35, 35);
        case FOODTYPE.NaturalJuice:
          return new Rectangle(836, 493, 32, 37);
        case FOODTYPE.Rehydrated:
          return new Rectangle(128, 830, 28, 22);
        case FOODTYPE.FreshCoconut:
          return new Rectangle(81, 733, 30, 29);
        case FOODTYPE.DeadZooAnimals:
          return new Rectangle(83, 704, 34, 28);
        case FOODTYPE.DeadFarmAnimals:
          return new Rectangle(83, 675, 34, 28);
        case FOODTYPE.Meat:
          return new Rectangle(96, 960, 33, 26);
        case FOODTYPE.NotMeat:
          return new Rectangle(157, 829, 32, 25);
        case FOODTYPE.CandleWax:
          return new Rectangle(54, 702, 28, 27);
        case FOODTYPE.ProcessedCheese:
          return new Rectangle(82, 648, 35, 26);
        case FOODTYPE.Cola:
          return new Rectangle(190, 857, 35, 34);
        case FOODTYPE.BadChemicals:
          return new Rectangle(52, 666, 31, 36);
        case FOODTYPE.LessBadChemicals:
          return new Rectangle(257, 858, 30, 35);
        case FOODTYPE.Sugar:
          return new Rectangle(49, 787, 31, 31);
        case FOODTYPE.Dirt:
          return new Rectangle(52, 642, 29, 23);
        case FOODTYPE.ArabicaBeans:
          return new Rectangle(16, 744, 33, 32);
        case FOODTYPE.Caffeine:
          return new Rectangle(16, 744, 33, 32);
        case FOODTYPE.Crisps:
          return new Rectangle(128, 774, 37, 36);
        case FOODTYPE.Starch:
          return new Rectangle(190, 832, 35, 24);
        case FOODTYPE.Potato:
          return new Rectangle(288, 993, 30, 31);
        case FOODTYPE.Chocolate:
          return new Rectangle(226, 856, 30, 37);
        case FOODTYPE.PalmOil:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.CocoButter:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.Molly:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.Slurpz:
          return new Rectangle(128, 730, 30, 43);
        case FOODTYPE.TapWater:
          return new Rectangle(159, 733, 34, 32);
        case FOODTYPE.FrozenSpringWater:
          return new Rectangle(237, 894, 31, 31);
        case FOODTYPE.EnergyChemicals:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.FruitSlush:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.RabbitBalls:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.TapiocaBalls:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.Margarita:
          return new Rectangle(226, 825, 27, 30);
        case FOODTYPE.Lettuce:
          return new Rectangle(254, 826, 29, 31);
        case FOODTYPE.Basil:
          return new Rectangle(166, 766, 33, 26);
        case FOODTYPE.MSG:
          return new Rectangle(50, 751, 30, 35);
        case FOODTYPE.Hawaiian:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.NoPinapple:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.Pinapple:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.PinkCottonCandy:
          return new Rectangle(275, 958, 33, 34);
        case FOODTYPE.SyntheticDye:
          return new Rectangle(277, 926, 29, 31);
        case FOODTYPE.NaturalCOlouring:
          return new Rectangle(269, 899, 36, 26);
        case FOODTYPE.AnimalCandy:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.CottonWool:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.Candy:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.Cuteness:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.Balloon:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.Air:
          return new Rectangle(167, 793, 30, 35);
        case FOODTYPE.Helium:
          return new Rectangle(198, 796, 30, 35);
        case FOODTYPE.AnimalBalloon:
          return new Rectangle(288, 859, 32, 39);
        case FOODTYPE.Churros:
          return new Rectangle(319, 990, 33, 34);
        case FOODTYPE.Flour:
          return new Rectangle(309, 964, 40, 25);
        case FOODTYPE.FrostedChurros:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.Dulcedeleche:
          return new Rectangle(128, 811, 38, 17);
        case FOODTYPE.PopCorn:
          return new Rectangle(229, 790, 26, 34);
        case FOODTYPE.Unpopped:
          return new Rectangle(256, 797, 29, 28);
        case FOODTYPE.Popped:
          return new Rectangle(284, 823, 39, 35);
        case FOODTYPE.CraftBeer:
          return new Rectangle(307, 926, 33, 37);
        case FOODTYPE.Sewage:
          return new Rectangle(194, 729, 33, 29);
        case FOODTYPE.Hops:
          return new Rectangle(286, 787, 36, 35);
        case FOODTYPE.KopiLuwak:
          return new Rectangle(200, 759, 29, 36);
        case FOODTYPE.Poop:
          return new Rectangle(15, 677, 36, 29);
        case FOODTYPE.Luwak:
          return new Rectangle(17, 707, 36, 36);
        case FOODTYPE.FreedomFries:
          return new Rectangle(128, 692, 32, 37);
        case FOODTYPE.Pretzel:
          return new Rectangle(11, 642, 40, 34);
        case FOODTYPE.MonkeyMilk:
          return new Rectangle(161, 696, 32, 36);
        case FOODTYPE.CowMilk:
          return new Rectangle(194, 696, 33, 32);
        case FOODTYPE.BeefTaco:
          return new Rectangle(128, 657, 37, 34);
        case FOODTYPE.Worms:
          return new Rectangle(306, 900, 32, 25);
        case FOODTYPE.Beef:
          return new Rectangle(96, 960, 33, 26);
        case FOODTYPE.Polyester:
          return new Rectangle(284, 756, 38, 30);
        case FOODTYPE.Cotton:
          return new Rectangle(166, 658, 36, 37);
        default:
          return new Rectangle(128, 811, 38, 17);
      }
    }
  }
}
