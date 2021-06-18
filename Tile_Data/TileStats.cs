// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tile_Data.TileStats
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;

namespace TinyZoo.Tile_Data
{
  internal class TileStats
  {
    private StringID __Name;
    private StringID __Description;
    public int[] Productions;
    public int[] Consumptions;

    public TileStats(StringID _Name, StringID _Description)
    {
      this.__Name = _Name;
      this.__Description = _Description;
    }

    public string Name
    {
      get => SEngine.Localization.Localization.GetText((int) this.__Name);
      set
      {
      }
    }

    public string Description
    {
      get => SEngine.Localization.Localization.GetText((int) this.__Description);
      set
      {
      }
    }

    public static Rectangle GetBuildingIconRectangle(TILETYPE productiontype)
    {
      switch (productiontype)
      {
        case TILETYPE.Floor_GreenGrass:
          return new Rectangle(445, 2, 16, 16);
        case TILETYPE.Floor_Dirt:
          return new Rectangle(360, 2, 16, 16);
        case TILETYPE.Floor_RedCircles:
          return new Rectangle(513, 2, 16, 16);
        case TILETYPE.Floor_GreyBricks:
          return new Rectangle(530, 2, 16, 16);
        case TILETYPE.PowerStation:
          return new Rectangle(632, 19, 16, 16);
        case TILETYPE.KitchenZone:
          return new Rectangle(615, 19, 16, 16);
        case TILETYPE.Research_PrisonPlanet:
          return new Rectangle(598, 19, 16, 16);
        case TILETYPE.Medical:
          return new Rectangle(581, 19, 16, 16);
        case TILETYPE.Security:
          return new Rectangle(615, 36, 16, 16);
        case TILETYPE.Bank:
          return new Rectangle(598, 36, 16, 16);
        case TILETYPE.Solitary:
          return new Rectangle(581, 36, 16, 16);
        case TILETYPE.Janitor:
          return new Rectangle(632, 36, 16, 16);
        case TILETYPE.Farm:
          return new Rectangle(564, 19, 16, 16);
        case TILETYPE.Water:
          return new Rectangle(513, 19, 16, 16);
        case TILETYPE.LifeSupport:
          return new Rectangle(530, 19, 16, 16);
        case TILETYPE.HoldingCell:
          return new Rectangle(411, 19, 16, 16);
        case TILETYPE.GrassEnclosure:
        case TILETYPE.CorruptedGrassEnclosure:
        case TILETYPE.GrassEnclosureIcon:
          return new Rectangle(547, 2, 16, 16);
        case TILETYPE.DesertEnclosure:
        case TILETYPE.DesertEnclosureIcon:
          return new Rectangle(598, 2, 16, 16);
        case TILETYPE.MountainEnclosure:
        case TILETYPE.MountainEnclosureIcon:
          return new Rectangle(615, 2, 16, 16);
        case TILETYPE.ArcticEnclosure:
        case TILETYPE.ArcticEnclosureIcon:
          return new Rectangle(734, 19, 16, 16);
        case TILETYPE.TropicalEnclosure:
        case TILETYPE.TropicalEnclosureIcon:
          return new Rectangle(751, 19, 16, 16);
        case TILETYPE.ForestEnclosure:
        case TILETYPE.CorruptedForestEnclosure:
        case TILETYPE.ForestEnclosureIcon:
          return new Rectangle(564, 2, 16, 16);
        case TILETYPE.SavannahEnclosure:
        case TILETYPE.SavannahEnclosureIcon:
          return new Rectangle(581, 2, 16, 16);
        case TILETYPE.FieldPicketFenceEnclosure:
          return new Rectangle(462, 19, 16, 16);
        case TILETYPE.GraveYard:
          return new Rectangle(547, 19, 16, 16);
        case TILETYPE.PinkTreeIAP:
          return new Rectangle(394, 19, 16, 16);
        case TILETYPE.BlueTreeIAP:
          return new Rectangle(564, 36, 16, 16);
        case TILETYPE.GoatIAP:
          return new Rectangle(547, 36, 16, 16);
        case TILETYPE.ResearchIAP:
          return new Rectangle(530, 36, 16, 16);
        case TILETYPE.TrashCompactorIAP:
          return new Rectangle(513, 36, 16, 16);
        case TILETYPE.FlowerIAP:
          return new Rectangle(479, 36, 16, 16);
        case TILETYPE.SmallGiftShop:
          return new Rectangle(632, 19, 16, 16);
        case TILETYPE.LionHotDogShop:
          return new Rectangle(530, 19, 16, 16);
        case TILETYPE.ElephantGiftShop:
          return new Rectangle(598, 19, 16, 16);
        case TILETYPE.IceCreamTruck:
          return new Rectangle(547, 36, 16, 16);
        case TILETYPE.BigIceCreamShop:
          return new Rectangle(530, 36, 16, 16);
        case TILETYPE.CoconutShop:
          return new Rectangle(479, 36, 16, 16);
        case TILETYPE.DrinksVendingMachine:
          return new Rectangle(513, 19, 16, 16);
        case TILETYPE.SnacksVendingMachine:
          return new Rectangle(581, 19, 16, 16);
        case TILETYPE.SignboardFront:
          return new Rectangle(632, 36, 16, 16);
        case TILETYPE.Lamppost:
          return new Rectangle(615, 36, 16, 16);
        case TILETYPE.GreenDustbin:
          return new Rectangle(581, 36, 16, 16);
        case TILETYPE.WhiteDustbin:
          return new Rectangle(598, 36, 16, 16);
        case TILETYPE.PlantPot:
          return new Rectangle(564, 19, 16, 16);
        case TILETYPE.RedFlag:
          return new Rectangle(547, 19, 16, 16);
        case TILETYPE.RoundFountain:
          return new Rectangle(564, 36, 16, 16);
        case TILETYPE.PandaBurgerShop:
          return new Rectangle(462, 36, 16, 16);
        case TILETYPE.BalloonShop:
          return new Rectangle(445, 36, 16, 16);
        case TILETYPE.FlowerPatch:
          return new Rectangle(394, 2, 16, 16);
        case TILETYPE.DangerSign:
          return new Rectangle(428, 2, 16, 16);
        case TILETYPE.SmallDesertRockDeco:
          return new Rectangle(462, 2, 16, 16);
        case TILETYPE.DesertRockDeco:
          return new Rectangle(479, 2, 16, 16);
        case TILETYPE.DesertCactusDeco:
          return new Rectangle(496, 2, 16, 16);
        case TILETYPE.KangarooPizzaShop:
          return new Rectangle(479, 19, 16, 16);
        case TILETYPE.CottonCandyShop:
          return new Rectangle(496, 19, 16, 16);
        case TILETYPE.SlushieShop:
          return new Rectangle(428, 36, 16, 16);
        case TILETYPE.ChurroShop:
          return new Rectangle(377, 19, 16, 16);
        case TILETYPE.WoodenToilet:
          return new Rectangle(394, 19, 16, 16);
        case TILETYPE.SnakeHuggingBooth:
          return new Rectangle(411, 19, 16, 16);
        case TILETYPE.TreeExhibit:
          return new Rectangle(428, 19, 16, 16);
        case TILETYPE.InfoBooth:
          return new Rectangle(445, 19, 16, 16);
        case TILETYPE.BusStop:
          return new Rectangle(462, 19, 16, 16);
        case TILETYPE.BarSignboard:
          return new Rectangle(666, 19, 16, 16);
        case TILETYPE.Umbrella:
          return new Rectangle(683, 2, 16, 16);
        case TILETYPE.BigTree:
          return new Rectangle(377, 2, 16, 16);
        case TILETYPE.TikiShelter:
          return new Rectangle(683, 19, 16, 16);
        case TILETYPE.ZooMap:
          return new Rectangle(700, 2, 16, 16);
        case TILETYPE.BrownBench:
          return new Rectangle(615, 19, 16, 16);
        case TILETYPE.WhiteBench:
          return new Rectangle(649, 19, 16, 16);
        case TILETYPE.NoSwimmingSign:
          return new Rectangle(734, 2, 16, 16);
        case TILETYPE.PottedPlant:
          return new Rectangle(666, 2, 16, 16);
        case TILETYPE.ThickSignboard:
          return new Rectangle(717, 2, 16, 16);
        case TILETYPE.ElephantFountain:
          return new Rectangle(717, 19, 16, 16);
        case TILETYPE.FloorLight:
          return new Rectangle(649, 2, 16, 16);
        case TILETYPE.RustyKegShop:
          return new Rectangle(751, 2, 16, 16);
        case TILETYPE.PopcornWeaselShop:
          return new Rectangle(768, 2, 16, 16);
        case TILETYPE.Nursery:
          return new Rectangle(785, 2, 16, 16);
        case TILETYPE.QuarantineOffice:
          return new Rectangle(802, 2, 16, 16);
        case TILETYPE.VetOffice:
          return new Rectangle(819, 2, 16, 16);
        case TILETYPE.ElephantMarbleFountain:
          return new Rectangle(768, 19, 16, 16);
        case TILETYPE.FlamingoHedge:
          return new Rectangle(785, 19, 16, 16);
        case TILETYPE.GiraffeHedge:
          return new Rectangle(802, 19, 16, 16);
        case TILETYPE.ElephantHedge:
          return new Rectangle(819, 19, 16, 16);
        case TILETYPE.MonkeyStatue:
          return new Rectangle(836, 19, 16, 16);
        case TILETYPE.GiantSunFlower:
          return new Rectangle(853, 19, 16, 16);
        case TILETYPE.ZooStandee:
          return new Rectangle(870, 19, 16, 16);
        case TILETYPE.PenguinStandee:
          return new Rectangle(887, 19, 16, 16);
        case TILETYPE.BearStandee:
          return new Rectangle(904, 19, 16, 16);
        case TILETYPE.SnakeBench:
          return new Rectangle(836, 2, 16, 16);
        case TILETYPE.ZooTree:
          return new Rectangle(853, 2, 16, 16);
        case TILETYPE.SnakeSignpost:
          return new Rectangle(870, 2, 16, 16);
        case TILETYPE.UmbrellaBench:
          return new Rectangle(887, 2, 16, 16);
        case TILETYPE.MonkeyBanner:
          return new Rectangle(904, 2, 16, 16);
        case TILETYPE.SakuraTree:
          return new Rectangle(386, 636, 16, 16);
        case TILETYPE.LionPlayground:
          return new Rectangle(403, 636, 16, 16);
        case TILETYPE.SpringChicken:
          return new Rectangle(420, 636, 16, 16);
        case TILETYPE.SpringHorse:
          return new Rectangle(437, 636, 16, 16);
        case TILETYPE.RockElephant:
          return new Rectangle(454, 636, 16, 16);
        case TILETYPE.YellowBush:
          return new Rectangle(471, 636, 16, 16);
        case TILETYPE.RedFlower:
          return new Rectangle(488, 636, 16, 16);
        case TILETYPE.WhiteFlower:
          return new Rectangle(505, 636, 16, 16);
        case TILETYPE.PurpleFlower:
          return new Rectangle(522, 636, 16, 16);
        case TILETYPE.PurpleFlowerPatch:
          return new Rectangle(539, 636, 16, 16);
        case TILETYPE.PenguinTrashbin:
          return new Rectangle(556, 636, 16, 16);
        case TILETYPE.SealStandee:
          return new Rectangle(573, 636, 16, 16);
        case TILETYPE.RedShelter:
          return new Rectangle(0, 188, 16, 16);
        case TILETYPE.CrocCrossingSign:
          return new Rectangle(17, 188, 16, 16);
        case TILETYPE.MenuSign:
          return new Rectangle(34, 188, 16, 16);
        case TILETYPE.SmallBarTable:
          return new Rectangle(51, 188, 16, 16);
        case TILETYPE.NoPhotoSign:
          return new Rectangle(68, 188, 16, 16);
        case TILETYPE.OwlClock:
          return new Rectangle(85, 188, 16, 16);
        case TILETYPE.HeShee:
          return new Rectangle(102, 188, 16, 16);
        case TILETYPE.Totem:
          return new Rectangle(119, 188, 16, 16);
        case TILETYPE.AztecSign:
          return new Rectangle(136, 188, 16, 16);
        case TILETYPE.AztecTorch:
          return new Rectangle(153, 188, 16, 16);
        case TILETYPE.AztecPlant:
          return new Rectangle(170, 188, 16, 16);
        case TILETYPE.WoodenTotem:
          return new Rectangle(187, 188, 16, 16);
        case TILETYPE.StoreRoom:
          return new Rectangle(411, 2, 16, 16);
        case TILETYPE.ArchitectOffice:
          return new Rectangle(360, 19, 16, 16);
        case TILETYPE.LargeArchitectOffice:
          return new Rectangle(326, 2, 16, 16);
        case TILETYPE.GlueFactory:
          return new Rectangle(343, 19, 16, 16);
        case TILETYPE.BuffaloWingFactory:
          return new Rectangle(343, 2, 16, 16);
        case TILETYPE.BaconFactory:
          return new Rectangle(326, 19, 16, 16);
        case TILETYPE.Barn:
          return new Rectangle(292, 2, 16, 16);
        case TILETYPE.EmptySoilPatch:
          return new Rectangle(81, 239, 16, 16);
        case TILETYPE.Wheat_SmallGrown:
          return new Rectangle(309, 2, 16, 16);
        case TILETYPE.Wheat_HalfGrown:
          return new Rectangle(309, 2, 16, 16);
        case TILETYPE.Wheat_FullGrown:
          return new Rectangle(309, 2, 16, 16);
        case TILETYPE.Silo:
          return new Rectangle(292, 19, 16, 16);
        case TILETYPE.Floor_OrangeTiles:
          return new Rectangle(149, 239, 16, 16);
        case TILETYPE.Floor_ColorfulBrickTile:
          return new Rectangle(138, 290, 16, 16);
        case TILETYPE.Floor_BlueCircleTiles:
          return new Rectangle(98, 256, 16, 16);
        case TILETYPE.Floor_WoodenBoards:
          return new Rectangle(98, 239, 16, 16);
        case TILETYPE.Floor_MetalDecoTile:
          return new Rectangle(115, 239, 16, 16);
        case TILETYPE.Floor_PawDecoTile:
          return new Rectangle(132, 239, 16, 16);
        case TILETYPE.PalmTree:
          return new Rectangle(275, 2, 16, 16);
        case TILETYPE.GreenTree:
          return new Rectangle(275, 19, 16, 16);
        case TILETYPE.LongGrass:
          return new Rectangle(81, 205, 16, 16);
        case TILETYPE.IrisPlantPot:
          return new Rectangle(98, 205, 16, 16);
        case TILETYPE.YellowPlantPot:
          return new Rectangle(115, 205, 16, 16);
        case TILETYPE.BonsaiPlantPot:
          return new Rectangle(149, 222, 16, 16);
        case TILETYPE.Ferns:
          return new Rectangle(132, 205, 16, 16);
        case TILETYPE.SmallRock:
          return new Rectangle(149, 205, 16, 16);
        case TILETYPE.MediumRock:
          return new Rectangle(166, 205, 16, 16);
        case TILETYPE.LargeMossyRock:
          return new Rectangle(183, 205, 16, 16);
        case TILETYPE.TigerPhoto:
          return new Rectangle(309, 19, 16, 16);
        case TILETYPE.NoticeBoard:
          return new Rectangle(81, 222, 16, 16);
        case TILETYPE.BeetleGlassExhibit:
          return new Rectangle(98, 222, 16, 16);
        case TILETYPE.ButterflyGlassExhibit:
          return new Rectangle(115, 222, 16, 16);
        case TILETYPE.GlassExhibit:
          return new Rectangle(132, 222, 16, 16);
        case TILETYPE.MiniFountain:
          return new Rectangle(132, 256, 16, 16);
        case TILETYPE.ElegantTallFountain:
          return new Rectangle(115, 256, 16, 16);
        case TILETYPE.HedgeArchYellowFlowers:
          return new Rectangle(149, 256, 16, 16);
        case TILETYPE.HedgeArchWhiteFlowers:
          return new Rectangle(0, 262, 16, 16);
        case TILETYPE.WhiteMetalVineArch:
          return new Rectangle(17, 262, 16, 16);
        case TILETYPE.BlackMetalRoseArch:
          return new Rectangle(34, 262, 16, 16);
        case TILETYPE.PeacockBush:
          return new Rectangle(51, 262, 16, 16);
        case TILETYPE.Binoculars:
          return new Rectangle(275, 36, 16, 16);
        case TILETYPE.TwinLamppost:
          return new Rectangle(292, 36, 16, 16);
        case TILETYPE.CurlLampPost:
          return new Rectangle(309, 36, 16, 16);
        case TILETYPE.TwinCurlsLampPost:
          return new Rectangle(326, 36, 16, 16);
        case TILETYPE.WoodenLampPost:
          return new Rectangle(343, 36, 16, 16);
        case TILETYPE.TripletsLampPost:
          return new Rectangle(360, 36, 16, 16);
        case TILETYPE.ClassicLampPost:
          return new Rectangle(275, 53, 16, 16);
        case TILETYPE.WhiteClassicLampPost:
          return new Rectangle(292, 53, 16, 16);
        case TILETYPE.FlamingoLampPost:
          return new Rectangle(309, 53, 16, 16);
        case TILETYPE.SealLampPost:
          return new Rectangle(326, 53, 16, 16);
        case TILETYPE.BallLampPost:
          return new Rectangle(343, 53, 16, 16);
        case TILETYPE.SwingingBench:
          return new Rectangle(360, 53, 16, 16);
        case TILETYPE.GreenGardenBench:
          return new Rectangle(275, 70, 16, 16);
        case TILETYPE.GreenChair:
          return new Rectangle(292, 70, 16, 16);
        case TILETYPE.WoodenChair:
          return new Rectangle(309, 70, 16, 16);
        case TILETYPE.LongWoodenBench:
          return new Rectangle(326, 70, 16, 16);
        case TILETYPE.CamelChair:
          return new Rectangle(343, 70, 16, 16);
        case TILETYPE.Greenhouse:
          return new Rectangle(360, 70, 16, 16);
        case TILETYPE.PandaChair:
          return new Rectangle(81, 273, 16, 16);
        case TILETYPE.ATMMachine:
          return new Rectangle(98, 273, 16, 16);
        case TILETYPE.SmallSpeaker:
          return new Rectangle(115, 273, 16, 16);
        case TILETYPE.LargeSpeaker:
          return new Rectangle(166, 256, 16, 16);
        case TILETYPE.RockSpeaker:
          return new Rectangle(132, 273, 16, 16);
        case TILETYPE.IceSpeaker:
          return new Rectangle(149, 273, 16, 16);
        case TILETYPE.KatCoffeeShop:
          return new Rectangle(183, 256, 16, 16);
        case TILETYPE.ShellShackShop:
          return new Rectangle(166, 273, 16, 16);
        case TILETYPE.TacoTruck:
          return new Rectangle(183, 273, 16, 16);
        case TILETYPE.PretzelShop:
          return new Rectangle(121, 290, 16, 16);
        case TILETYPE.Slaughterhouse:
          return new Rectangle(200, 273, 16, 16);
        case TILETYPE.IglooToilet:
          return new Rectangle(200, 256, 16, 16);
        case TILETYPE.LionTrashbin:
          return new Rectangle(87, 290, 16, 16);
        case TILETYPE.BearTrashbin:
          return new Rectangle(104, 290, 16, 16);
        case TILETYPE.Floor_Cobblestone:
          return new Rectangle(155, 290, 16, 16);
        case TILETYPE.Floor_PinkSmallTiles:
          return new Rectangle(172, 290, 16, 16);
        case TILETYPE.Floor_BrownOctoTile:
          return new Rectangle(189, 290, 16, 16);
        case TILETYPE.Floor_BrownOctoTile2:
          return new Rectangle(206, 290, 16, 16);
        case TILETYPE.Floor_GreenAndBlueDiamondTile:
          return new Rectangle(87, 307, 16, 16);
        case TILETYPE.Floor_BrownSquareTile:
          return new Rectangle(104, 307, 16, 16);
        case TILETYPE.PineTree:
          return new Rectangle(121, 307, 16, 16);
        case TILETYPE.BrickToilet:
          return new Rectangle(206, 307, 16, 16);
        case TILETYPE.EggBatteryFarm:
          return new Rectangle(189, 307, 16, 16);
        case TILETYPE.ColoredTree:
          return new Rectangle(138, 307, 16, 16);
        case TILETYPE.DarkBush:
          return new Rectangle(155, 307, 16, 16);
        case TILETYPE.Cactus:
          return new Rectangle(172, 307, 16, 16);
        case TILETYPE.Floor_Snow:
          return new Rectangle(206, 239, 16, 16);
        case TILETYPE.PenguinSignboard:
          return new Rectangle(217, 256, 16, 16);
        case TILETYPE.IceArch:
          return new Rectangle(234, 256, 16, 16);
        case TILETYPE.SmallIceRocks:
          return new Rectangle(217, 273, 16, 16);
        case TILETYPE.ThinIceRocks:
          return new Rectangle(234, 273, 16, 16);
        case TILETYPE.IceCrystals:
          return new Rectangle(223, 290, 16, 16);
        case TILETYPE.Snowman:
          return new Rectangle(240, 290, 16, 16);
        case TILETYPE.IcyTree:
          return new Rectangle(223, 307, 16, 16);
        case TILETYPE.LionSnowSculpture:
          return new Rectangle(240, 307, 16, 16);
        case TILETYPE.SealIceSculpture:
          return new Rectangle(223, 239, 16, 16);
        case TILETYPE.DeerIceSculpture:
          return new Rectangle(240, 239, 16, 16);
        case TILETYPE.BearIceSculpture:
          return new Rectangle(228, 222, 16, 16);
        case TILETYPE.BirdIceSculpture:
          return new Rectangle(245, 222, 16, 16);
        case TILETYPE.GiraffeIceSculpture:
          return new Rectangle(257, 239, 16, 16);
        case TILETYPE.BlueGrass:
          return new Rectangle(251, 256, 16, 16);
        case TILETYPE.IceShelter:
          return new Rectangle(268, 256, 16, 16);
        case TILETYPE.ZooIceSign:
          return new Rectangle(251, 273, 16, 16);
        case TILETYPE.IceCastle:
          return new Rectangle(268, 273, 16, 16);
        case TILETYPE.IceChair:
          return new Rectangle(257, 290, 16, 16);
        case TILETYPE.GiantTree:
          return new Rectangle(274, 290, 16, 16);
        case TILETYPE.ZooRockSign:
          return new Rectangle(257, 307, 16, 16);
        case TILETYPE.GiantPigBalloon:
          return new Rectangle(274, 307, 16, 16);
        case TILETYPE.Bamboo:
          return new Rectangle(262, 222, 16, 16);
        case TILETYPE.Volume_RedPathway:
          return new Rectangle(513, 2, 16, 16);
        case TILETYPE.WaterTower:
          return new Rectangle(279, 222, 16, 16);
        case TILETYPE.Cart:
          return new Rectangle(296, 222, 16, 16);
        case TILETYPE.SmallBush:
          return new Rectangle(313, 222, 16, 16);
        case TILETYPE.DeadTree:
          return new Rectangle(274, 239, 16, 16);
        case TILETYPE.WantedPoster:
          return new Rectangle(291, 239, 16, 16);
        case TILETYPE.BigRocks:
          return new Rectangle(308, 239, 16, 16);
        case TILETYPE.Pyramid:
          return new Rectangle(285, 256, 16, 16);
        case TILETYPE.SandTower:
          return new Rectangle(302, 256, 16, 16);
        case TILETYPE.SmallPyramid:
          return new Rectangle(285, 273, 16, 16);
        case TILETYPE.Sphinx:
          return new Rectangle(302, 273, 16, 16);
        case TILETYPE.PalmTreesTall:
          return new Rectangle(291, 290, 16, 16);
        case TILETYPE.OldWestShelter:
          return new Rectangle(308, 290, 16, 16);
        case TILETYPE.Anubis:
          return new Rectangle(291, 307, 16, 16);
        case TILETYPE.WesternWindmill:
          return new Rectangle(308, 307, 16, 16);
        case TILETYPE.Volume_Grass:
          return new Rectangle(445, 2, 16, 16);
        case TILETYPE.Volume_WoodenBoards:
          return new Rectangle(98, 239, 16, 16);
        case TILETYPE.JungleToliet:
          return new Rectangle(170, 324, 16, 16);
        case TILETYPE.TreeLogBench:
          return new Rectangle(187, 324, 16, 16);
        case TILETYPE.TreeStump:
          return new Rectangle(204, 324, 16, 16);
        case TILETYPE.GlowingPlant:
          return new Rectangle(221, 324, 16, 16);
        case TILETYPE.RedMushrooms:
          return new Rectangle(238, 324, 16, 16);
        case TILETYPE.BrownMushrooms:
          return new Rectangle((int) byte.MaxValue, 324, 16, 16);
        case TILETYPE.LeafyFern:
          return new Rectangle(271, 324, 16, 16);
        case TILETYPE.LargeFern:
          return new Rectangle(289, 324, 16, 16);
        case TILETYPE.TreeWithVines:
          return new Rectangle(170, 341, 16, 16);
        case TILETYPE.TreeSwing:
          return new Rectangle(187, 341, 16, 16);
        case TILETYPE.JungleArch:
          return new Rectangle(204, 341, 16, 16);
        case TILETYPE.JungleWaterfall:
          return new Rectangle(221, 341, 16, 16);
        case TILETYPE.MushroomShelter:
          return new Rectangle(238, 341, 16, 16);
        case TILETYPE.Floor_WoodenPlanks:
          return new Rectangle((int) byte.MaxValue, 341, 16, 16);
        case TILETYPE.Floor_WoodenTrunk:
          return new Rectangle(272, 341, 16, 16);
        case TILETYPE.Floor_StonePebbles:
          return new Rectangle(289, 341, 16, 16);
        case TILETYPE.Volume_WhiteSnow:
          return new Rectangle(206, 239, 16, 16);
        case TILETYPE.Volume_Water:
          return new Rectangle(306, 324, 16, 16);
        case TILETYPE.Volume_WoodenBridge:
          return new Rectangle(306, 341, 16, 16);
        case TILETYPE.WaterTrough_Metal:
          return new Rectangle(325, 307, 16, 16);
        case TILETYPE.WaterTrough_Metal_Single:
          return new Rectangle(319, 273, 16, 16);
        case TILETYPE.WaterTrough_Wooden:
          return new Rectangle(325, 290, 16, 16);
        case TILETYPE.WaterTrough_Wooden_Single:
          return new Rectangle(319, 256, 16, 16);
        case TILETYPE.RockyWaterfall:
          return new Rectangle(325, 239, 16, 16);
        case TILETYPE.ChocolateVendingMachine:
          return new Rectangle(330, 222, 16, 16);
        case TILETYPE.Water_SmallLilyPads:
          return new Rectangle(336, 256, 16, 16);
        case TILETYPE.Water_LargeLilyPads:
          return new Rectangle(336, 273, 16, 16);
        case TILETYPE.Water_Reeds:
          return new Rectangle(342, 290, 16, 16);
        case TILETYPE.Water_LotusFlower:
          return new Rectangle(342, 307, 16, 16);
        case TILETYPE.Water_Rock:
          return new Rectangle(323, 324, 16, 16);
        case TILETYPE.Water_FlatRock:
          return new Rectangle(340, 324, 16, 16);
        case TILETYPE.Water_WoodenBoards:
          return new Rectangle(323, 341, 16, 16);
        case TILETYPE.Water_Lanturn:
          return new Rectangle(340, 341, 16, 16);
        case TILETYPE.Water_LightBall:
          return new Rectangle(347, 222, 16, 16);
        case TILETYPE.Water_BirdStatue:
          return new Rectangle(342, 239, 16, 16);
        case TILETYPE.Water_FlappingBirdStatue:
          return new Rectangle(359, 239, 16, 16);
        case TILETYPE.Water_FishFountain:
          return new Rectangle(353, 256, 16, 16);
        case TILETYPE.Water_WaterJarFountain:
          return new Rectangle(353, 273, 16, 16);
        case TILETYPE.Water_Fountain:
          return new Rectangle(359, 290, 16, 16);
        case TILETYPE.Water_FloatingCrate:
          return new Rectangle(359, 307, 16, 16);
        case TILETYPE.Water_FloatingBarrel:
          return new Rectangle(357, 324, 16, 16);
        case TILETYPE.Water_WaterJet:
          return new Rectangle(357, 341, 16, 16);
        case TILETYPE.Water_TreasureChest:
          return new Rectangle(364, 222, 16, 16);
        case TILETYPE.Water_SunkenShip:
          return new Rectangle(376, 239, 16, 16);
        case TILETYPE.Water_IceRocks:
          return new Rectangle(370, 273, 16, 16);
        case TILETYPE.Water_FlatIce:
          return new Rectangle(370, 256, 16, 16);
        case TILETYPE.Water_IceBoulders:
          return new Rectangle(376, 290, 16, 16);
        case TILETYPE.Water_RockBoulders:
          return new Rectangle(376, 307, 16, 16);
        case TILETYPE.Water_FloatHouse:
          return new Rectangle(376, 307, 16, 16);
        case TILETYPE.WaterTrough_Leaf:
        case TILETYPE.WaterTrough_TreeTrunk:
        case TILETYPE.DarkTreeDeco:
        case TILETYPE.SwingTreeDeco:
        case TILETYPE.EmptyWaterTrough_Metal:
        case TILETYPE.EmptyWaterTrough_Metal_Single:
        case TILETYPE.EmptyWaterTrough_Wooden:
        case TILETYPE.EmptyWaterTrough_Wooden_Single:
        case TILETYPE.EmptyWaterTrough_Leaf:
        case TILETYPE.EmptyWaterTrough_TreeTrunk:
        case TILETYPE.CorruptedDirtTile:
        case TILETYPE.CorruptedTree:
        case TILETYPE.CorruptedDeadTree:
        case TILETYPE.CorruptedBush:
        case TILETYPE.CorruptedBushSmall:
        case TILETYPE.Shelter_MossyRock:
        case TILETYPE.WaterTrough_Rock:
        case TILETYPE.EmptyWaterTrough_Rock:
        case TILETYPE.WaterTrough_IceRock:
        case TILETYPE.EmptyWaterTrough_IceRock:
        case TILETYPE.ManagmentOffice_BrownWood:
        case TILETYPE.ManagmentOffice_SimpleBlack:
        case TILETYPE.ManagmentOffice_BlueCottage:
        case TILETYPE.ManagmentOffice_Rabbit:
        case TILETYPE.Enrichment_HighStriker:
          return new Rectangle(376, 307, 16, 16);
        case TILETYPE.Enrichment_WaterSprinklers:
          return new Rectangle(36, 290, 16, 16);
        case TILETYPE.Enrichment_BlueTrampoline:
          return new Rectangle(53, 290, 16, 16);
        case TILETYPE.Enrichment_ScratchingPostWood:
          return new Rectangle(51, 324, 16, 16);
        case TILETYPE.Enrichment_LargeRedBall:
          return new Rectangle(119, 358, 16, 16);
        case TILETYPE.Enrichment_SmallBlueBall:
          return new Rectangle(123, 375, 16, 16);
        case TILETYPE.Enrichment_TugRopeToy:
          return new Rectangle(68, 324, 16, 16);
        case TILETYPE.Enrichment_ChewToyPurpleBone:
          return new Rectangle(85, 324, 16, 16);
        case TILETYPE.Enrichment_TunnelGreen:
          return new Rectangle(136, 324, 16, 16);
        case TILETYPE.Enrichment_WoodenLogs:
          return new Rectangle(51, 341, 16, 16);
        case TILETYPE.Enrichment_YellowRockPerch:
          return new Rectangle(58, 392, 16, 16);
        case TILETYPE.Shelter_SmallRockCave:
          return new Rectangle(177, 392, 16, 16);
        case TILETYPE.Shelter_LargeRockCave:
          return new Rectangle(194, 392, 16, 16);
        case TILETYPE.Enrichment_HighPerch:
          return new Rectangle(41, 392, 16, 16);
        case TILETYPE.Shelter_LargeWoodenHouse:
          return new Rectangle(208, 375, 16, 16);
        case TILETYPE.Shelter_Igloo:
          return new Rectangle(70, 307, 16, 16);
        case TILETYPE.WaterPumpStation:
          return new Rectangle(381, 222, 16, 16);
        case TILETYPE.Water_Skull:
          return new Rectangle(374, 324, 16, 16);
        case TILETYPE.Water_CannonballJet:
          return new Rectangle(374, 341, 16, 16);
        case TILETYPE.Water_WaterMill:
          return new Rectangle(387, 273, 16, 16);
        case TILETYPE.Water_FrogFountain:
          return new Rectangle(393, 290, 16, 16);
        case TILETYPE.Water_WaterLanturn:
          return new Rectangle(387, 256, 16, 16);
        case TILETYPE.GiantBearBalloon:
          return new Rectangle(393, 307, 16, 16);
        case TILETYPE.LightHouse:
          return new Rectangle(391, 324, 16, 16);
        case TILETYPE.Water_IceArchRock:
          return new Rectangle(393, 239, 16, 16);
        case TILETYPE.Water_MangroveTree:
          return new Rectangle(398, 222, 16, 16);
        case TILETYPE.Water_SmallMangroveTree:
          return new Rectangle(391, 341, 16, 16);
        case TILETYPE.Water_GrassyRock:
          return new Rectangle(404, 273, 16, 16);
        case TILETYPE.Water_SmallGrassyRock:
          return new Rectangle(404, 256, 16, 16);
        case TILETYPE.Water_DeerScare:
          return new Rectangle(410, 290, 16, 16);
        case TILETYPE.GiraffeAirDancer:
          return new Rectangle(410, 307, 16, 16);
        case TILETYPE.SnakeAirDancer:
          return new Rectangle(408, 324, 16, 16);
        case TILETYPE.Enrichment_HangingCarTire:
          return new Rectangle(85, 341, 16, 16);
        case TILETYPE.Enrichment_HighWoodBeamPerch:
          return new Rectangle(102, 341, 16, 16);
        case TILETYPE.Enrichment_RockCliff:
          return new Rectangle(119, 341, 16, 16);
        case TILETYPE.Enrichment_RockPerch:
          return new Rectangle(136, 341, 16, 16);
        case TILETYPE.Enrichment_SaltBlock:
          return new Rectangle(153, 341, 16, 16);
        case TILETYPE.Enrichment_MirrorRect:
          return new Rectangle(0, 358, 16, 16);
        case TILETYPE.Enrichment_ScentMarkerGrey:
          return new Rectangle(51, 358, 16, 16);
        case TILETYPE.Enrichment_TugBallJollyBallYellow:
          return new Rectangle(34, 358, 16, 16);
        case TILETYPE.Enrichment_LargeCardboardBox:
          return new Rectangle(68, 358, 16, 16);
        case TILETYPE.Enrichment_ScentMarkerGreen:
          return new Rectangle(85, 358, 16, 16);
        case TILETYPE.Enrichment_ScentMarkerBrown:
          return new Rectangle(102, 358, 16, 16);
        case TILETYPE.Enrichment_MirrorRound:
          return new Rectangle(17, 358, 16, 16);
        case TILETYPE.Enrichment_LargeWhiteBall:
          return new Rectangle(136, 358, 16, 16);
        case TILETYPE.Enrichment_LargeGreenBall:
          return new Rectangle(153, 358, 16, 16);
        case TILETYPE.Enrichment_LargeYellowBall:
          return new Rectangle(21, 375, 16, 16);
        case TILETYPE.Enrichment_LargeBlueBall:
          return new Rectangle(38, 376, 16, 16);
        case TILETYPE.Enrichment_TunnelWoodenLog:
          return new Rectangle(153, 324, 16, 16);
        case TILETYPE.Enrichment_PinkTrampoline:
          return new Rectangle(70, 290, 16, 16);
        case TILETYPE.Enrichment_FlatCarTire:
          return new Rectangle(68, 341, 16, 16);
        case TILETYPE.Enrichment_ChewToyBrownBone:
          return new Rectangle(102, 324, 16, 16);
        case TILETYPE.Enrichment_ChewToyRope:
          return new Rectangle(119, 324, 16, 16);
        case TILETYPE.Enrichment_SmallCyanBall:
          return new Rectangle(55, 375, 16, 16);
        case TILETYPE.Enrichment_SmallGreenBall:
          return new Rectangle(72, 375, 16, 16);
        case TILETYPE.Enrichment_SmallRedBall:
          return new Rectangle(89, 375, 16, 16);
        case TILETYPE.Enrichment_SmallPinkBall:
          return new Rectangle(106, 375, 16, 16);
        case TILETYPE.Shelter_IceRocks:
          return new Rectangle(191, 375, 16, 16);
        case TILETYPE.Enrichment_IceCliff:
          return new Rectangle(140, 375, 16, 16);
        case TILETYPE.Enrichment_BrownCliff:
          return new Rectangle(157, 375, 16, 16);
        case TILETYPE.Enrichment_ScratchingPoleTree:
          return new Rectangle(174, 375, 16, 16);
        case TILETYPE.Enrichment_BrownRockPerch:
          return new Rectangle(24, 392, 16, 16);
        case TILETYPE.Enrichment_LogPerch:
          return new Rectangle(75, 392, 16, 16);
        case TILETYPE.Enrichment_NetPerch:
          return new Rectangle(92, 392, 16, 16);
        case TILETYPE.Enrichment_WoodenBeam2:
          return new Rectangle(109, 392, 16, 16);
        case TILETYPE.Enrichment_WoodenBeam3:
          return new Rectangle(126, 392, 16, 16);
        case TILETYPE.Enrichment_CardboardBox2:
          return new Rectangle(143, 392, 16, 16);
        case TILETYPE.Enrichment_TreeHighPerch:
          return new Rectangle(160, 392, 16, 16);
        case TILETYPE.Water_SunkenRocksLarge:
          return new Rectangle(408, 341, 16, 16);
        case TILETYPE.Water_SunkenRocksMed:
          return new Rectangle(415, 222, 16, 16);
        case TILETYPE.Water_SunkenRocksSmall:
          return new Rectangle(410, 239, 16, 16);
        case TILETYPE.WindTurbine:
          return new Rectangle(421, 256, 16, 16);
        case TILETYPE.SolarPanel:
          return new Rectangle(421, 273, 16, 16);
        case TILETYPE.RecyclingBin:
          return new Rectangle(427, 290, 16, 16);
        case TILETYPE.CrossSign:
          return new Rectangle(427, 307, 16, 16);
        case TILETYPE.Volume_DarkGrass:
          return new Rectangle(427, 239, 16, 16);
        case TILETYPE.Volume_YellowGrass:
          return new Rectangle(425, 324, 16, 16);
        case TILETYPE.Volume_Sand:
          return new Rectangle(425, 341, 16, 16);
        case TILETYPE.MilkBatteryFarm:
          return new Rectangle(432, 222, 16, 16);
        case TILETYPE.HelicopterRide:
          return new Rectangle(444, 239, 16, 16);
        case TILETYPE.Caravan:
          return new Rectangle(438, 256, 16, 16);
        case TILETYPE.OldWestToliet:
          return new Rectangle(438, 273, 16, 16);
        case TILETYPE.WestBarrel:
          return new Rectangle(444, 290, 16, 16);
        case TILETYPE.WestWoodenBox:
          return new Rectangle(444, 307, 16, 16);
        case TILETYPE.WoodenTrashbin:
          return new Rectangle(442, 324, 16, 16);
        case TILETYPE.WesternArch:
          return new Rectangle(442, 341, 16, 16);
        case TILETYPE.OrangeStoneArch:
          return new Rectangle(449, 222, 16, 16);
        case TILETYPE.BoneArch:
          return new Rectangle(461, 239, 16, 16);
        case TILETYPE.BoneDeco:
          return new Rectangle(455, 256, 16, 16);
        case TILETYPE.SmallCactus:
          return new Rectangle(455, 273, 16, 16);
        case TILETYPE.ArrowSignboard:
          return new Rectangle(461, 290, 16, 16);
        case TILETYPE.SmallGrass:
          return new Rectangle(461, 307, 16, 16);
        case TILETYPE.WestWoodenBoard:
          return new Rectangle(459, 324, 16, 16);
        case TILETYPE.Volume_SoilPlot:
          return new Rectangle(81, 239, 16, 16);
        case TILETYPE.Carrots_SmallGrown:
        case TILETYPE.Carrots_HalfGrown:
        case TILETYPE.Carrots_FullGrown:
          return new Rectangle(459, 341, 16, 16);
        case TILETYPE.Cabbage_SmallGrown:
        case TILETYPE.Cabbage_HalfGrown:
        case TILETYPE.Cabbage_FullGrown:
          return new Rectangle(476, 341, 16, 16);
        case TILETYPE.Corn_SmallGrown:
        case TILETYPE.Corn_HalfGrown:
        case TILETYPE.Corn_FullGrown:
          return new Rectangle(476, 324, 16, 16);
        case TILETYPE.Potato_SmallGrown:
        case TILETYPE.Potato_HalfGrown:
        case TILETYPE.Potato_FullGrown:
          return new Rectangle(478, 307, 16, 16);
        case TILETYPE.Watermelon_SmallGrown:
        case TILETYPE.Watermelon_HalfGrown:
        case TILETYPE.Watermelon_FullGrown:
          return new Rectangle(478, 290, 16, 16);
        case TILETYPE.Grass_SmallGrown:
        case TILETYPE.Grass_HalfGrown:
        case TILETYPE.Grass_FullGrown:
          return new Rectangle(472, 273, 16, 16);
        case TILETYPE.TreeWithLights:
          return new Rectangle(466, 222, 16, 16);
        case TILETYPE.CarpStreamers:
          return new Rectangle(478, 239, 16, 16);
        case TILETYPE.HorizonValleyZooBalloon:
          return new Rectangle(472, 256, 16, 16);
        case TILETYPE.PineTreeDark:
          return new Rectangle(495, 239, 16, 16);
        case TILETYPE.ArticBush:
          return new Rectangle(489, 256, 16, 16);
        case TILETYPE.DarkSmallPlant:
          return new Rectangle(489, 273, 16, 16);
        case TILETYPE.DeadWinterTree:
          return new Rectangle(495, 290, 16, 16);
        case TILETYPE.TallRoofHouse:
          return new Rectangle(495, 307, 16, 16);
        case TILETYPE.DesertTree:
          return new Rectangle(493, 324, 16, 16);
        case TILETYPE.CactusLong:
          return new Rectangle(493, 341, 16, 16);
        case TILETYPE.AztacTempleFloor:
          return new Rectangle(483, 222, 16, 16);
        case TILETYPE.AztacSnakeStatue:
          return new Rectangle(506, 267, 16, 16);
        case TILETYPE.AztacTemple:
          return new Rectangle(512, 239, 16, 16);
        case TILETYPE.Volume_LightMud:
          return new Rectangle(512, 284, 16, 16);
        case TILETYPE.Volume_LightGrass:
          return new Rectangle(500, 222, 16, 16);
        case TILETYPE.AztacToliet:
          return new Rectangle(534, 320, 16, 16);
        case TILETYPE.AztacMap:
          return new Rectangle(534, 303, 16, 16);
        case TILETYPE.AztacChair:
          return new Rectangle(517, 303, 16, 16);
        case TILETYPE.BigMountainRocks:
          return new Rectangle(510, 337, 16, 16);
        case TILETYPE.OrangeLargeRocks:
          return new Rectangle(517, 320, 16, 16);
        case TILETYPE.AztacTempleGate:
          return new Rectangle(527, 337, 16, 16);
        case TILETYPE.AsianToliet:
          return new Rectangle(334, 358, 16, 16);
        case TILETYPE.AsianGate:
          return new Rectangle(351, 358, 16, 16);
        case TILETYPE.AsianPavillion:
          return new Rectangle(368, 358, 16, 16);
        case TILETYPE.AsianLight:
          return new Rectangle(385, 358, 16, 16);
        case TILETYPE.CoinSquasher:
          return new Rectangle(402, 358, 16, 16);
        case TILETYPE.RockyZooSign:
          return new Rectangle(419, 358, 16, 16);
        case TILETYPE.MediumStones:
          return new Rectangle(436, 358, 16, 16);
        case TILETYPE.LightGreenTree:
          return new Rectangle(453, 358, 16, 16);
        case TILETYPE.ImpossibleBuilding:
          return new Rectangle(575, 602, 16, 16);
        case TILETYPE.DNABuilding:
          return new Rectangle(470, 358, 16, 16);
        case TILETYPE.MeatProcessor:
          return new Rectangle(575, 619, 16, 16);
        case TILETYPE.AnimalColosseum:
          return new Rectangle(510, 354, 16, 16);
        case TILETYPE.Incinerator:
          return new Rectangle(527, 354, 16, 16);
        case TILETYPE.CrocHandbagFactory:
          return new Rectangle(874, 673, 16, 16);
        case TILETYPE.SnakeSkinFactory:
          return new Rectangle(891, 673, 16, 16);
        case TILETYPE.Storeroom_BrownWood:
        case TILETYPE.Storeroom_Victorian:
        case TILETYPE.Storeroom_Monkey:
          return new Rectangle(411, 2, 16, 16);
        case TILETYPE.SurveillanceBuilding:
          return new Rectangle(470, 205, 16, 16);
        case TILETYPE.HotAirBalloonRide:
          return new Rectangle(211, 222, 16, 16);
        case TILETYPE.Farmhouse:
          return new Rectangle(434, 877, 16, 16);
        case TILETYPE.BeerBrewery:
          return new Rectangle(417, 877, 16, 16);
        case TILETYPE.Windmill:
          return new Rectangle(451, 877, 16, 16);
        case TILETYPE.FarmProcessor:
          return new Rectangle(386, 894, 16, 16);
        case TILETYPE.RecyclingCenter:
          return new Rectangle(400, 877, 16, 16);
        case TILETYPE.AnimalRehabilitationBuilding:
          return new Rectangle(81, 763, 16, 16);
        case TILETYPE.Warehouse:
          return new Rectangle(403, 894, 16, 16);
        case TILETYPE.RainCollectionBuilding:
          return new Rectangle(98, 763, 16, 16);
        case TILETYPE.Enrichment_Attachment_Sunglasses:
        case TILETYPE.Enrichment_Attachment_FlowerBlueHat:
        case TILETYPE.Enrichment_Attachment_SmallPinkHat:
        case TILETYPE.Enrichment_Attachment_RedRibbon:
        case TILETYPE.Enrichment_Attachment_FootballHelmet:
        case TILETYPE.Enrichment_Attachment_AngelHalo:
        case TILETYPE.Enrichment_Attachment_RedStripedCap:
        case TILETYPE.Enrichment_Attachment_ColorfulAfroWig:
          return new Rectangle(376, 307, 16, 16);
        default:
          throw new Exception("NO WAY");
      }
    }

    public static string GetProductionTypeToString(ProductionType productiontype)
    {
      switch (productiontype)
      {
        case ProductionType.Power:
          return "Power";
        case ProductionType.LifeSupport:
          return "Life Support";
        case ProductionType.Water:
          return "Water";
        case ProductionType.Farm:
          return "Bio-Farm";
        case ProductionType.FoodKitchen:
          return "Food";
        case ProductionType.Janitor:
          return "Cleanliness";
        case ProductionType.Medicine:
          return "Medicine";
        case ProductionType.Security:
          return "Security";
        case ProductionType.Bank:
          return "Storage";
        case ProductionType.Research:
          return "Research";
        default:
          return "NO NAME";
      }
    }

    public bool ImpactsConsumption() => this.Productions != null || this.Consumptions != null;

    public static Rectangle GetProductionTypeToRectangle(ProductionType productiontype)
    {
      switch (productiontype)
      {
        case ProductionType.Power:
          return new Rectangle(0, 566, 32, 32);
        case ProductionType.LifeSupport:
          return new Rectangle(231, 566, 32, 32);
        case ProductionType.Water:
          return new Rectangle(33, 566, 32, 32);
        case ProductionType.Farm:
          return new Rectangle(99, 566, 32, 32);
        case ProductionType.FoodKitchen:
          return new Rectangle(66, 566, 32, 32);
        case ProductionType.Janitor:
          return new Rectangle(132, 566, 32, 32);
        case ProductionType.Medicine:
          return new Rectangle(198, 566, 32, 32);
        case ProductionType.Security:
          return new Rectangle(297, 566, 32, 32);
        case ProductionType.Bank:
          return new Rectangle(330, 566, 32, 32);
        case ProductionType.Research:
          return new Rectangle(264, 566, 32, 32);
        default:
          return new Rectangle();
      }
    }

    public void SetProduction(ProductionType productiontype, int VolumePerHour)
    {
      if (this.Productions == null)
        this.Productions = new int[10];
      this.Productions[(int) productiontype] = VolumePerHour;
    }

    public void SetConsumption(ProductionType productiontype, int VolumePerHou)
    {
      if (this.Consumptions == null)
        this.Consumptions = new int[10];
      this.Consumptions[(int) productiontype] = VolumePerHou;
    }
  }
}
