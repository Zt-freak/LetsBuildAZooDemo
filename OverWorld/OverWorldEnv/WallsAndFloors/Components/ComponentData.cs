// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components.ComponentData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.Tile_Data;
using TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components;

namespace TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components
{
  internal class ComponentData
  {
    internal static List<RenderComponent> GetRenderComponent(
      TILETYPE tiletype,
      TileRenderer parent,
      bool IsConstructionPreview)
    {
      if (tiletype == TILETYPE.Volume_Water)
        return new List<RenderComponent>()
        {
          (RenderComponent) new UnifiedWaterAnimator(parent)
        };
      if (TileData.IsThisAWaterTrough(tiletype))
        return new List<RenderComponent>()
        {
          (RenderComponent) new WaterTrough(parent)
        };
      TileInfo tileInfo = TileData.GetTileInfo(tiletype);
      if (tileInfo.BaseFrame > 0)
        return new List<RenderComponent>()
        {
          (RenderComponent) new BuildingAnimator(parent, tileInfo)
        };
      if (TileData.IsThisAPenDecoration(tiletype) || TileData.IsThisAPenDecoration(tiletype) || TileData.IsThisAPenDecoration(tiletype))
        return new List<RenderComponent>()
        {
          (RenderComponent) new EnrichmentSelectionRenderer(parent, tileInfo)
        };
      switch (tiletype)
      {
        case TILETYPE.PowerStation:
        case TILETYPE.KitchenZone:
        case TILETYPE.Medical:
        case TILETYPE.Security:
        case TILETYPE.Bank:
        case TILETYPE.Janitor:
        case TILETYPE.Water:
        case TILETYPE.LifeSupport:
        case TILETYPE.GoatIAP:
        case TILETYPE.ResearchIAP:
        case TILETYPE.TrashCompactorIAP:
        case TILETYPE.SmallGiftShop:
        case TILETYPE.LionHotDogShop:
        case TILETYPE.ElephantGiftShop:
        case TILETYPE.IceCreamTruck:
        case TILETYPE.BigIceCreamShop:
        case TILETYPE.CoconutShop:
        case TILETYPE.DrinksVendingMachine:
        case TILETYPE.SnacksVendingMachine:
        case TILETYPE.SignboardFront:
        case TILETYPE.Lamppost:
        case TILETYPE.GreenDustbin:
        case TILETYPE.WhiteDustbin:
        case TILETYPE.PlantPot:
        case TILETYPE.RedFlag:
        case TILETYPE.RoundFountain:
        case TILETYPE.PandaBurgerShop:
        case TILETYPE.BalloonShop:
        case TILETYPE.FlowerPatch:
        case TILETYPE.DangerSign:
        case TILETYPE.SmallDesertRockDeco:
        case TILETYPE.DesertRockDeco:
        case TILETYPE.DesertCactusDeco:
        case TILETYPE.KangarooPizzaShop:
        case TILETYPE.CottonCandyShop:
        case TILETYPE.SlushieShop:
        case TILETYPE.ChurroShop:
        case TILETYPE.WoodenToilet:
        case TILETYPE.SnakeHuggingBooth:
        case TILETYPE.TreeExhibit:
        case TILETYPE.InfoBooth:
        case TILETYPE.BusStop:
        case TILETYPE.BarSignboard:
        case TILETYPE.Umbrella:
        case TILETYPE.BigTree:
        case TILETYPE.TikiShelter:
        case TILETYPE.ZooMap:
        case TILETYPE.BrownBench:
        case TILETYPE.WhiteBench:
        case TILETYPE.NoSwimmingSign:
        case TILETYPE.PottedPlant:
        case TILETYPE.ThickSignboard:
        case TILETYPE.ElephantFountain:
        case TILETYPE.FloorLight:
        case TILETYPE.RustyKegShop:
        case TILETYPE.PopcornWeaselShop:
        case TILETYPE.ElephantMarbleFountain:
        case TILETYPE.FlamingoHedge:
        case TILETYPE.GiraffeHedge:
        case TILETYPE.ElephantHedge:
        case TILETYPE.MonkeyStatue:
        case TILETYPE.GiantSunFlower:
        case TILETYPE.ZooStandee:
        case TILETYPE.PenguinStandee:
        case TILETYPE.BearStandee:
        case TILETYPE.SnakeBench:
        case TILETYPE.ZooTree:
        case TILETYPE.SnakeSignpost:
        case TILETYPE.UmbrellaBench:
        case TILETYPE.MonkeyBanner:
        case TILETYPE.SakuraTree:
        case TILETYPE.LionPlayground:
        case TILETYPE.SpringChicken:
        case TILETYPE.SpringHorse:
        case TILETYPE.RockElephant:
        case TILETYPE.YellowBush:
        case TILETYPE.RedFlower:
        case TILETYPE.WhiteFlower:
        case TILETYPE.PurpleFlower:
        case TILETYPE.PurpleFlowerPatch:
        case TILETYPE.PenguinTrashbin:
        case TILETYPE.SealStandee:
        case TILETYPE.RedShelter:
        case TILETYPE.CrocCrossingSign:
        case TILETYPE.MenuSign:
        case TILETYPE.SmallBarTable:
        case TILETYPE.NoPhotoSign:
        case TILETYPE.OwlClock:
        case TILETYPE.HeShee:
        case TILETYPE.Totem:
        case TILETYPE.AztecSign:
        case TILETYPE.AztecTorch:
        case TILETYPE.AztecPlant:
        case TILETYPE.WoodenTotem:
        case TILETYPE.ArchitectOffice:
        case TILETYPE.PalmTree:
        case TILETYPE.GreenTree:
        case TILETYPE.LongGrass:
        case TILETYPE.IrisPlantPot:
        case TILETYPE.YellowPlantPot:
        case TILETYPE.BonsaiPlantPot:
        case TILETYPE.Ferns:
        case TILETYPE.SmallRock:
        case TILETYPE.MediumRock:
        case TILETYPE.LargeMossyRock:
        case TILETYPE.TigerPhoto:
        case TILETYPE.NoticeBoard:
        case TILETYPE.BeetleGlassExhibit:
        case TILETYPE.ButterflyGlassExhibit:
        case TILETYPE.GlassExhibit:
          return (List<RenderComponent>) null;
        case TILETYPE.Research_PrisonPlanet:
          List<RenderComponent> renderComponentList = new List<RenderComponent>();
          renderComponentList.Add((RenderComponent) new BuildingAnimator(parent));
          ResearchDone researchDone = new ResearchDone(parent);
          if (IsConstructionPreview)
            researchDone.Disabled = true;
          renderComponentList.Add((RenderComponent) researchDone);
          return renderComponentList;
        case TILETYPE.Farm:
        case TILETYPE.GraveYard_FloorGraveStone:
        case TILETYPE.Logo:
        case TILETYPE.ZooEntrance_Deer:
        case TILETYPE.ZooEntrance_Cliff:
        case TILETYPE.ZooEntrance_Modern:
          return new List<RenderComponent>()
          {
            (RenderComponent) new ParkGate(parent)
          };
        case TILETYPE.FlowerIAP:
          return new List<RenderComponent>()
          {
            (RenderComponent) new BuildingAnimator(parent)
          };
        case TILETYPE.Nursery:
        case TILETYPE.QuarantineOffice:
        case TILETYPE.VetOffice:
        case TILETYPE.StoreRoom:
        case TILETYPE.Barn:
        case TILETYPE.EggBatteryFarm:
        case TILETYPE.MilkBatteryFarm:
        case TILETYPE.ImpossibleBuilding:
        case TILETYPE.Storeroom_BrownWood:
        case TILETYPE.Storeroom_Victorian:
        case TILETYPE.Storeroom_Monkey:
        case TILETYPE.SurveillanceBuilding:
        case TILETYPE.Farmhouse:
        case TILETYPE.BeerBrewery:
        case TILETYPE.FarmProcessor:
        case TILETYPE.RecyclingCenter:
        case TILETYPE.Warehouse:
          return new List<RenderComponent>()
          {
            (RenderComponent) new EnclosureGate(parent)
          };
        case TILETYPE.Gate_Grass:
        case TILETYPE.Gate_Forest:
        case TILETYPE.Gate_Savanah:
        case TILETYPE.Gate_Desert:
        case TILETYPE.Gate_Mountain:
        case TILETYPE.Gate_Arctic:
        case TILETYPE.Gate_Tropical:
        case TILETYPE.Gate_FieldPicketFence:
        case TILETYPE.CorruptedGrass_Gate:
        case TILETYPE.CorruptedForest_Gate:
          return new List<RenderComponent>()
          {
            (RenderComponent) new EnclosureGate(parent)
          };
        case TILETYPE.GlueFactory:
        case TILETYPE.BuffaloWingFactory:
        case TILETYPE.BaconFactory:
        case TILETYPE.Slaughterhouse:
        case TILETYPE.MeatProcessor:
        case TILETYPE.Incinerator:
        case TILETYPE.CrocHandbagFactory:
        case TILETYPE.SnakeSkinFactory:
          return new List<RenderComponent>()
          {
            (RenderComponent) new EnclosureGate(parent),
            (RenderComponent) new FactorySmoke(parent)
          };
        case TILETYPE.GiantPigBalloon:
        case TILETYPE.GiantBearBalloon:
          return new List<RenderComponent>()
          {
            (RenderComponent) new WobbleBalloon(parent)
          };
        case TILETYPE.DNABuilding:
          return new List<RenderComponent>()
          {
            (RenderComponent) new EnclosureGate(parent),
            (RenderComponent) new DNASplicerComponent(parent)
          };
        default:
          return (List<RenderComponent>) null;
      }
    }
  }
}
