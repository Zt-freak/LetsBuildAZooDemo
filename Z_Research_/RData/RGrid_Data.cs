// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.RData.RGrid_Data
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_Research_.RData
{
  internal class RGrid_Data
  {
    private static List<REntry> ResearchEntries;
    private static UpgradeCategoryHolder[] UpgradeCategories;
    private static ResearchUpgradeInfoSet[] researchinfosets;

    internal static List<REntry> GetResearchData()
    {
      if (RGrid_Data.ResearchEntries == null)
      {
        RGrid_Data.ResearchEntries = new List<REntry>();
        if (Z_DebugFlags.IsBetaVersion)
        {
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TreePackTwo_TallPineTrees, new Rectangle(643, 723, 32, 32), "Deco1 Pack - Tall Pine Trees", "Unlock 2 wonderful pine trees for your planting in your zoo.", 1, UpgradeCategory.Trees, new TILETYPE[2]
          {
            TILETYPE.PineTree,
            TILETYPE.PineTreeDark
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentSeven_SmallBall, new Rectangle(353, 768, 32, 32), "Animal Enrichment Pack - Small Ball", "Small balls provide basic enrichment for your animals.", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[5]
          {
            TILETYPE.Enrichment_SmallBlueBall,
            TILETYPE.Enrichment_SmallCyanBall,
            TILETYPE.Enrichment_SmallGreenBall,
            TILETYPE.Enrichment_SmallRedBall,
            TILETYPE.Enrichment_SmallPinkBall
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PlantsPackOne_PottedPlants, new Rectangle(782, 69, 32, 32), "Deco1 Pack - Potted Plants", "Add a couple more lovely potted plants to your collection.", 1, UpgradeCategory.Trees, new TILETYPE[2]
          {
            TILETYPE.BonsaiPlantPot,
            TILETYPE.PottedPlant
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DrinkShop_Slushy, new Rectangle(509, 570, 32, 32), "Facility Pack - Slushie Shop", "Unlock the Slushie Shop to quench the thirst of your visitors.", 2, UpgradeCategory.GiftShops, new TILETYPE[1]
          {
            TILETYPE.SlushieShop
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FloorPackOne, new Rectangle(709, 756, 32, 32), "Deco2 Pack - Pavement", "Unlock the Red Pathway and Cobblestone Floor to put more colors on your pavements.", 4, UpgradeCategory.Floors, new TILETYPE[2]
          {
            TILETYPE.Volume_RedPathway,
            TILETYPE.Floor_Cobblestone
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.ArchitextPlusOne, new Rectangle(709, 657, 32, 32), "Tech Pack - Researcher +1", "Rearranging some furniture in the Research Hub will allow you to employ 1 additional Researcher!", 10, UpgradeCategory.Research, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FountainTwo_ElegantTallFountain, new Rectangle(429, 537, 32, 32), "Deco1 Pack - Tall Fountain", "PLACEHOLDER", 5, UpgradeCategory.Trees, new TILETYPE[1]
          {
            TILETYPE.ElegantTallFountain
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.GiftShop_BalloonShop, new Rectangle(409, 471, 32, 32), "Facility Pack - Balloon Shop", "Selling smiles to your visitors in the form of animal balloons!", 6, UpgradeCategory.GiftShops, new TILETYPE[1]
          {
            TILETYPE.BalloonShop
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TolietPackOne, new Rectangle(643, 756, 32, 32), "Facility Pack - Toilet", "PLACEHOLDER", 2, UpgradeCategory.GiftShops, new TILETYPE[1]
          {
            TILETYPE.BrickToilet
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.BenchPackTwo, new Rectangle(676, 690, 32, 32), "Deco2 Pack - Benches", "When your visitors walk around for long enough, they would want to sit and recover their fatigue.", 2, UpgradeCategory.Floors, new TILETYPE[2]
          {
            TILETYPE.LongWoodenBench,
            TILETYPE.GreenGardenBench
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackTen_ZooSign, new Rectangle(709, 789, 32, 32), "Deco2 Pack - Zoo Sign", "Unlock 2 more deco packs to get 10% money back when demolishing", 5, UpgradeCategory.Floors, new TILETYPE[1]
          {
            TILETYPE.ZooStandee
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.SignboardPackTwo_Navigational, new Rectangle(841, 657, 32, 32), "Deco2 Pack - Navigational", "PLACEHOLDER", 3, UpgradeCategory.Floors, new TILETYPE[2]
          {
            TILETYPE.PenguinSignboard,
            TILETYPE.SnakeSignpost
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LamppostPackOne_Classic, new Rectangle(352, 189, 32, 32), "Deco1 Pack - Lights", "PLACEHOLDER", 2, UpgradeCategory.Trees, new TILETYPE[2]
          {
            TILETYPE.ClassicLampPost,
            TILETYPE.WhiteClassicLampPost
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PlantsPackEleven_PeacockBush, new Rectangle(841, 855, 32, 32), "Deco1 Pack- Peacock Bush", "Unlock 3 more plants to increase decoration score by 5%", 6, UpgradeCategory.Trees, new TILETYPE[1]
          {
            TILETYPE.PeacockBush
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentFifteen_Trampoline, new Rectangle(557, 372, 32, 32), "Animal Enrichment Pack - Trampoline", "PLACEHOLDER", 3, UpgradeCategory.AnimalEnrichment, new TILETYPE[2]
          {
            TILETYPE.Enrichment_BlueTrampoline,
            TILETYPE.Enrichment_PinkTrampoline
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.SheltersPackOne, new Rectangle(676, 756, 32, 32), "Deco2 Pack - Shelter", "PLACEHOLDER", 2, UpgradeCategory.Floors, new TILETYPE[2]
          {
            TILETYPE.Umbrella,
            TILETYPE.TikiShelter
          }));
          RGrid_Data.SortEntriesIntoHashSets(RGrid_Data.ResearchEntries);
        }
        else
        {
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Quarantine, new Rectangle(676, 657, 32, 32), "Quarantine Buidling", "Allows you to safely house sick animals, or seperate new arrivals from the rest of the animals to reduce the risk of spreading disease.", 10, UpgradeCategory.Vets, new TILETYPE[2]
          {
            TILETYPE.VetOffice,
            TILETYPE.QuarantineOffice
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TreePackOne, new Rectangle(821, 194, 32, 32), "Tree Pack", "Unlock 2 more trees to increase tree decoration score bonus by 5%", 1, UpgradeCategory.Trees, new TILETYPE[2]
          {
            TILETYPE.BigTree,
            TILETYPE.TreeWithVines
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.BenchPackOne_Chairs, new Rectangle(848, 36, 32, 32), "Bench Pack - Chairs", "Unlock 2 more benches to increase Bench Max Energy Cap by 5%", 2, UpgradeCategory.Benches, new TILETYPE[3]
          {
            TILETYPE.GreenChair,
            TILETYPE.WoodenChair,
            TILETYPE.SmallBarTable
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.BreedingFacility, new Rectangle(643, 657, 32, 32), "Breeding Facility", "Allows selective breeding, to increase you chances of birthing unique offspring.", 10, UpgradeCategory.AnimalNursery, new TILETYPE[1]
          {
            TILETYPE.Nursery
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.ArchitextPlusOne, new Rectangle(709, 657, 32, 32), "Research Pack - Researcher +1", "Rearranging some furniture in the Research Hub will allow you to employ 1 additional Researcher!", 10, UpgradeCategory.Research, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PlantsPackTwelve_Flowers, new Rectangle(775, 855, 32, 32), "Plant Pack - Flowers", "Unlock 3 more plants to increase decoration score by 5%", 1, UpgradeCategory.Plants, new TILETYPE[2]
          {
            TILETYPE.YellowBush,
            TILETYPE.IrisPlantPot
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TreePackTwo_TallPineTrees, new Rectangle(643, 723, 32, 32), "Tree Pack - Tall Pine Trees", "Unlock 2 more trees to increase tree decoration score bonus by 5%", 1, UpgradeCategory.Trees, new TILETYPE[2]
          {
            TILETYPE.PineTree,
            TILETYPE.PineTreeDark
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.BenchPackTwo, new Rectangle(676, 690, 32, 32), "Bench Pack", "Unlock 2 more benches to increase Bench Max Energy Cap by 5%", 2, UpgradeCategory.Benches, new TILETYPE[2]
          {
            TILETYPE.LongWoodenBench,
            TILETYPE.GreenGardenBench
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TolietPackOne, new Rectangle(643, 756, 32, 32), "Toliet Pack", "Unlock 1 more toliet to increase Toliet Capacity by 2", 2, UpgradeCategory.Toilet, new TILETYPE[1]
          {
            TILETYPE.BrickToilet
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PlantsPackTwo, new Rectangle(676, 723, 32, 32), "Plant Pack", "Unlock 3 more plants to increase decoration score by 5%", 2, UpgradeCategory.Plants, new TILETYPE[2]
          {
            TILETYPE.Bamboo,
            TILETYPE.AztecPlant
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TreePackThree_TallPalmTrees, new Rectangle(821, 161, 32, 32), "Tree Pack - Tall Pine Trees", "Unlock 2 more trees to increase tree decoration score bonus by 5%", 1, UpgradeCategory.Trees, new TILETYPE[2]
          {
            TILETYPE.PalmTree,
            TILETYPE.PalmTreesTall
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.BenchPackThree, new Rectangle(815, 36, 32, 32), "Bench Pack", "Unlock 2 more benches to increase Bench Max Energy Cap by 5%", 2, UpgradeCategory.Benches, new TILETYPE[2]
          {
            TILETYPE.TreeLogBench,
            TILETYPE.UmbrellaBench
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PlantsPackOne_PottedPlants, new Rectangle(782, 69, 32, 32), "Plant Pack - Potted Plants", "Unlock 3 more plants to increase decoration score by 5%", 2, UpgradeCategory.Plants, new TILETYPE[2]
          {
            TILETYPE.BonsaiPlantPot,
            TILETYPE.PottedPlant
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PlantsPackThree_ArticPlants, new Rectangle(709, 855, 32, 32), "Plant Pack - Artic Plants", "Unlock 3 more plants to increase decoration score by 5%", 2, UpgradeCategory.Plants, new TILETYPE[3]
          {
            TILETYPE.BlueGrass,
            TILETYPE.ArticBush,
            TILETYPE.DarkSmallPlant
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TreePackFour_FancyTrees, new Rectangle(821, 128, 32, 32), "Tree Pack - Fancy Trees", "Unlock 2 more trees to increase every trees decoration value by 1 point", 1, UpgradeCategory.Trees, new TILETYPE[2]
          {
            TILETYPE.LightGreenTree,
            TILETYPE.SakuraTree
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.BenchPackFour_Animal, new Rectangle(650, 74, 32, 32), "Bench Pack - Animal Benches", "Unlock 2 more benches to increase Bench Max Energy Cap by 5%", 2, UpgradeCategory.Benches, new TILETYPE[3]
          {
            TILETYPE.SnakeBench,
            TILETYPE.PandaChair,
            TILETYPE.CamelChair
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PlantsPackFour_SmallDesertPlants, new Rectangle(742, 855, 32, 32), "Plant Pack - Small Desert Plants", "Unlock 3 more plants to increase decoration score by 5%", 2, UpgradeCategory.Plants, new TILETYPE[3]
          {
            TILETYPE.DesertCactusDeco,
            TILETYPE.SmallCactus,
            TILETYPE.SmallGrass
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TolietPackTwo, new Rectangle(643, 756, 32, 32), "Toliet Pack", "Unlock 1 more toliet to increase Toliet Capacity by 2", 2, UpgradeCategory.Toilet, new TILETYPE[2]
          {
            TILETYPE.IglooToilet,
            TILETYPE.JungleToliet
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TreePackFive_SeasonTrees, new Rectangle(854, 223, 32, 32), "Tree Pack - Season Trees", "Unlock 2 more trees to increase tree decoration score bonus by 5%", 1, UpgradeCategory.Trees, new TILETYPE[3]
          {
            TILETYPE.DesertTree,
            TILETYPE.IcyTree,
            TILETYPE.ColoredTree
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.BenchPackFive_Themed, new Rectangle(683, 69, 32, 32), "Bench Pack - Themed Benches", "Unlock 2 more benches to increase Bench Max Energy Cap by 5%", 2, UpgradeCategory.Benches, new TILETYPE[2]
          {
            TILETYPE.IceChair,
            TILETYPE.AztacChair
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PlantsPackFive_BigDesertPlants, new Rectangle(742, 855, 32, 32), "Plant Pack - Big Desert Plants", "Unlock 3 more plants to increase decoration score by 5%", 2, UpgradeCategory.Plants, new TILETYPE[2]
          {
            TILETYPE.CactusLong,
            TILETYPE.Cactus
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PlantsPackSix_SpecialForestPlants, new Rectangle(676, 723, 32, 32), "Plant Pack - Special Forest Plants", "Unlock 3 more plants to increase decoration score by 5%", 2, UpgradeCategory.Plants, new TILETYPE[2]
          {
            TILETYPE.GlowingPlant,
            TILETYPE.TreeStump
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PlantsPackSeven_Mushrooms, new Rectangle(643, 855, 32, 32), "Plant Pack - Mushrooms", "Unlock 3 more plants to increase decoration score by 5%", 2, UpgradeCategory.Plants, new TILETYPE[2]
          {
            TILETYPE.RedMushrooms,
            TILETYPE.BrownMushrooms
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TreePackSix_DryTrees, new Rectangle(854, 190, 32, 32), "Tree Pack - Dry Trees", "Unlock 2 more trees to increase tree decoration score bonus by 5%", 1, UpgradeCategory.Trees, new TILETYPE[2]
          {
            TILETYPE.DeadWinterTree,
            TILETYPE.DeadTree
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.BenchPackSix_Swings, new Rectangle(782, 36, 32, 32), "Bench Pack - Swings", "Unlock 2 more benches to increase Bench Max Energy Cap by 5%", 2, UpgradeCategory.Benches, new TILETYPE[2]
          {
            TILETYPE.SwingingBench,
            TILETYPE.TreeSwing
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PlantsPackEight_LargeFerns, new Rectangle(676, 855, 32, 32), "Plant Pack - Large Ferns", "Unlock 3 more plants to increase decoration score by 5%", 2, UpgradeCategory.Plants, new TILETYPE[2]
          {
            TILETYPE.LeafyFern,
            TILETYPE.LargeFern
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PlantsPackNine_HedgeAnimals, new Rectangle(808, 855, 32, 32), "Plant Pack - Hedge Animals", "Unlock 3 more plants to increase decoration score by 5%", 2, UpgradeCategory.Plants, new TILETYPE[3]
          {
            TILETYPE.FlamingoHedge,
            TILETYPE.GiraffeHedge,
            TILETYPE.ElephantHedge
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PlantsPackTen_Bushes, new Rectangle(749, 69, 32, 32), "Plant Pack - Bushes", "Unlock 3 more plants to increase decoration score by 5%", 2, UpgradeCategory.Plants, new TILETYPE[2]
          {
            TILETYPE.SmallBush,
            TILETYPE.DarkBush
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TreePackSeven_GiantTree, new Rectangle(854, 157, 32, 32), "Tree Pack - Giant Tree", "Unlock 2 more trees to increase tree decoration score bonus by 5%", 1, UpgradeCategory.Trees, new TILETYPE[1]
          {
            TILETYPE.GiantTree
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PlantsPackEleven_PeacockBush, new Rectangle(841, 855, 32, 32), "Plant Pack - Peacock Bush", "Unlock 3 more plants to increase decoration score by 5%", 2, UpgradeCategory.Plants, new TILETYPE[1]
          {
            TILETYPE.PeacockBush
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.SheltersPackOne, new Rectangle(676, 756, 32, 32), "Shelter Pack", "Unlock 1 more shelter to increase Shelters Max Energy Cap", 2, UpgradeCategory.Shelters, new TILETYPE[2]
          {
            TILETYPE.Umbrella,
            TILETYPE.TikiShelter
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.SheltersPackTwo, new Rectangle(676, 756, 32, 32), "Shelter Pack", "Unlock 1 more shelter to increase Shelters Max Energy Cap", 2, UpgradeCategory.Shelters, new TILETYPE[2]
          {
            TILETYPE.IceShelter,
            TILETYPE.MushroomShelter
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.SheltersPackThree, new Rectangle(676, 756, 32, 32), "Shelter Pack", "Unlock 1 more shelter to increase Shelters Max Energy Cap", 2, UpgradeCategory.Shelters, new TILETYPE[2]
          {
            TILETYPE.AsianPavillion,
            TILETYPE.OldWestShelter
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.SignboardPackOne_Navigational, new Rectangle(841, 657, 32, 32), "Signboard Pack - Navigational", "Unlock 3 more to increase effective range of signs by 5%", 1, UpgradeCategory.Signboard, new TILETYPE[2]
          {
            TILETYPE.ArrowSignboard,
            TILETYPE.BarSignboard
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.SignboardPackTwo_Navigational, new Rectangle(841, 657, 32, 32), "Signboard Pack - Navigational", "Unlock 3 more to increase effective range of signs by 5%", 1, UpgradeCategory.Signboard, new TILETYPE[2]
          {
            TILETYPE.PenguinSignboard,
            TILETYPE.SnakeSignpost
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.SignboardPackThree_Navigational, new Rectangle(841, 657, 32, 32), "Signboard Pack - Navigational", "Unlock 3 more to increase effective range of signs by 5%", 1, UpgradeCategory.Signboard, new TILETYPE[2]
          {
            TILETYPE.WestWoodenBoard,
            TILETYPE.AztacMap
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TolietPackThree, new Rectangle(643, 756, 32, 32), "Toliet Pack", "Unlock 1 more toliet to increase Toliet Capacity by 2", 2, UpgradeCategory.Toilet, new TILETYPE[2]
          {
            TILETYPE.OldWestToliet,
            TILETYPE.AsianToliet
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.SignboardPackFour_Intructional, new Rectangle(808, 657, 32, 32), "Signboard Pack - Intructional", "Unlock 3 more to increase effective range of signs by 5%", 1, UpgradeCategory.Signboard, new TILETYPE[2]
          {
            TILETYPE.NoPhotoSign,
            TILETYPE.MenuSign
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.SignboardPackFive_Intructional, new Rectangle(808, 657, 32, 32), "Signboard Pack - Intructional", "Unlock 3 more to increase effective range of signs by 5%", 1, UpgradeCategory.Signboard, new TILETYPE[2]
          {
            TILETYPE.NoSwimmingSign,
            TILETYPE.CrocCrossingSign
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.SignboardPackSix_Intructional, new Rectangle(808, 657, 32, 32), "Signboard Pack - Intructional", "Unlock 3 more to increase effective range of signs by 5%", 1, UpgradeCategory.Signboard, new TILETYPE[2]
          {
            TILETYPE.CrossSign,
            TILETYPE.DangerSign
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.SignboardPackSeven_Educational, new Rectangle(775, 657, 32, 32), "Signboard Pack - Educational", "Unlock 3 more to increase effective range of signs by 5%", 1, UpgradeCategory.Signboard, new TILETYPE[3]
          {
            TILETYPE.SignboardFront,
            TILETYPE.AztecSign,
            TILETYPE.NoticeBoard
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FloorPackOne, new Rectangle(709, 756, 32, 32), "Floor Pack", "Unlock 2 more floors to decrease energy loss of customer walking through park by 5%", 1, UpgradeCategory.Floors, new TILETYPE[2]
          {
            TILETYPE.Volume_RedPathway,
            TILETYPE.Floor_Cobblestone
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FloorPackTwo_Colorful, new Rectangle(709, 756, 32, 32), "Floor Pack - Colorful", "Unlock 2 more floors to decrease energy loss of customer walking through park by 5%", 1, UpgradeCategory.Floors, new TILETYPE[2]
          {
            TILETYPE.Floor_BlueCircleTiles,
            TILETYPE.Floor_OrangeTiles
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FloorPackThree, new Rectangle(709, 756, 32, 32), "Floor Pack", "Unlock 2 more floors to decrease energy loss of customer walking through park by 5%", 1, UpgradeCategory.Floors, new TILETYPE[2]
          {
            TILETYPE.Floor_PinkSmallTiles,
            TILETYPE.Floor_BrownOctoTile
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FloorPackFour_Deco, new Rectangle(709, 756, 32, 32), "Floor Pack - Deco Tiles", "Unlock 2 more floors to decrease energy loss of customer walking through park by 5%", 1, UpgradeCategory.Floors, new TILETYPE[2]
          {
            TILETYPE.Floor_PawDecoTile,
            TILETYPE.Floor_BrownSquareTile
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FloorPackFive_Nature, new Rectangle(775, 756, 32, 32), "Floor Pack - Nature", "Unlock 2 more floors to decrease energy loss of customer walking through park by 5%", 1, UpgradeCategory.Floors, new TILETYPE[2]
          {
            TILETYPE.Volume_WhiteSnow,
            TILETYPE.Volume_Sand
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FloorPackSix_Nature, new Rectangle(808, 756, 32, 32), "Floor Pack - Nature", "Unlock 2 more floors to decrease energy loss of customer walking through park by 5%", 1, UpgradeCategory.Floors, new TILETYPE[2]
          {
            TILETYPE.Volume_LightMud,
            TILETYPE.Floor_StonePebbles
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FloorPackSeven_Nature, new Rectangle(808, 756, 32, 32), "Floor Pack - Nature", "Unlock 2 more floors to decrease energy loss of customer walking through park by 5%", 1, UpgradeCategory.Floors, new TILETYPE[2]
          {
            TILETYPE.Floor_WoodenPlanks,
            TILETYPE.Floor_WoodenTrunk
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FloorPackEight_Grass, new Rectangle(742, 756, 32, 32), "Floor Pack - Grass", "Unlock 2 more floors to decrease energy loss of customer walking through park by 5%", 1, UpgradeCategory.Floors, new TILETYPE[3]
          {
            TILETYPE.Volume_DarkGrass,
            TILETYPE.Volume_YellowGrass,
            TILETYPE.Volume_LightGrass
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TolietPackFour, new Rectangle(643, 756, 32, 32), "Toliet Pack - Temple", "Unlock 1 more toliet to increase Toliet Capacity by 2", 2, UpgradeCategory.Toilet, new TILETYPE[1]
          {
            TILETYPE.AztacToliet
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FloorPackNine, new Rectangle(709, 756, 32, 32), "Floor Pack", "Unlock 2 more floors to decrease energy loss of customer walking through park by 5%", 1, UpgradeCategory.Floors, new TILETYPE[2]
          {
            TILETYPE.AztacTempleFloor,
            TILETYPE.Floor_GreenAndBlueDiamondTile
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.RockPackOne, new Rectangle(841, 822, 32, 32), "Rock Pack", "Unlock 3 more to unlock PLACEHOLDER", 1, UpgradeCategory.Rocks, new TILETYPE[2]
          {
            TILETYPE.DesertRockDeco,
            TILETYPE.MediumStones
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.RockPackTwo_LargeRocks, new Rectangle(808, 822, 32, 32), "Rock Pack - Large Rocks", "Unlock 3 more to unlock PLACEHOLDER", 1, UpgradeCategory.Rocks, new TILETYPE[2]
          {
            TILETYPE.LargeMossyRock,
            TILETYPE.BigRocks
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.RockPackThree_LargeRocks, new Rectangle(808, 789, 32, 32), "Rock Pack - Large Rocks", "Unlock 3 more to unlock PLACEHOLDER", 1, UpgradeCategory.Rocks, new TILETYPE[2]
          {
            TILETYPE.BigMountainRocks,
            TILETYPE.OrangeLargeRocks
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.RockPackFour_IceRocks, new Rectangle(841, 789, 32, 32), "Rock Pack - Ice Rocks", "Unlock 3 more to unlock PLACEHOLDER", 1, UpgradeCategory.Rocks, new TILETYPE[3]
          {
            TILETYPE.SmallIceRocks,
            TILETYPE.ThinIceRocks,
            TILETYPE.IceCrystals
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.SpeakerPackOne, new Rectangle(841, 756, 32, 32), "Speaker Pack", "Unlock 1 more to unlock PLACEHOLDER", 2, UpgradeCategory.Speakers, new TILETYPE[2]
          {
            TILETYPE.SmallSpeaker,
            TILETYPE.LargeSpeaker
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.SpeakerPackTwo_Theme, new Rectangle(841, 756, 32, 32), "Speaker Pack - Themed", "Unlock 1 more to unlock PLACEHOLDER", 2, UpgradeCategory.Speakers, new TILETYPE[2]
          {
            TILETYPE.IceSpeaker,
            TILETYPE.RockSpeaker
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.EnergyPackOne_SolarPanel, new Rectangle(874, 789, 32, 32), "Energy Pack - Solar Panel", "Unlocks Solar Panel in game (Only avaliable if playing GOOD)", 10, UpgradeCategory.Energy, new TILETYPE[1]
          {
            TILETYPE.SolarPanel
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.EnergyPackTwo_WindTurbine, new Rectangle(907, 789, 32, 32), "Energy Pack - Wind Turbine", "Unlocks Wind Turbine in game (Only avaliable if playing GOOD)", 10, UpgradeCategory.Energy, new TILETYPE[1]
          {
            TILETYPE.WindTurbine
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.EnergyPackThree_WaterCollectionBuilding, new Rectangle(412, 662, 32, 32), "Water Collection Building", "Unlocks Water Collection Building in game (Only avaliable if playing GOOD)", 10, UpgradeCategory.Energy, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.EnergyPackFour_RecyclingCenter, new Rectangle(478, 662, 32, 32), "Recycling Center", "Unlocks Recycling Center in game (Only avaliable if playing GOOD)", 10, UpgradeCategory.Energy, new TILETYPE[1]
          {
            TILETYPE.RecyclingCenter
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackOne_Water, new Rectangle(709, 723, 32, 32), "Lake Pack", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[3]
          {
            TILETYPE.Volume_Water,
            TILETYPE.Water_SmallLilyPads,
            TILETYPE.Water_LargeLilyPads
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackTwo_WaterPlants, new Rectangle(709, 723, 32, 32), "Lake Pack - Water Plants", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[2]
          {
            TILETYPE.Water_Reeds,
            TILETYPE.Water_LotusFlower
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackThree_WalkablePaths, new Rectangle(716, 69, 32, 32), "Lake Pack - Walkable Paths", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[2]
          {
            TILETYPE.Water_FlatRock,
            TILETYPE.Water_WoodenBoards
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackFour_WaterRocks, new Rectangle(742, 723, 32, 32), "Lake Pack - Water Rocks", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[2]
          {
            TILETYPE.Water_Rock,
            TILETYPE.Water_RockBoulders
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackFive_GrassyRocks, new Rectangle(742, 723, 32, 32), "Lake Pack - Grassy Rocks", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[2]
          {
            TILETYPE.Water_GrassyRock,
            TILETYPE.Water_SmallGrassyRock
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackSix_BirdStatues, new Rectangle(775, 723, 32, 32), "Lake Pack - Bird Statues", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[2]
          {
            TILETYPE.Water_BirdStatue,
            TILETYPE.Water_FlappingBirdStatue
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackSeven_StatueFountains, new Rectangle(19, 603, 32, 32), "Lake Pack - Animal Fountains", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[2]
          {
            TILETYPE.Water_FishFountain,
            TILETYPE.Water_FrogFountain
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackEight_FountainsInWater, new Rectangle(808, 723, 32, 32), "Lake Pack - Fountains", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[3]
          {
            TILETYPE.Water_Fountain,
            TILETYPE.Water_CannonballJet,
            TILETYPE.Water_WaterJet
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackNine_FancyFountainsInWater, new Rectangle(808, 723, 32, 32), "Lake Pack - Fancy Fountains", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[2]
          {
            TILETYPE.Water_WaterJarFountain,
            TILETYPE.Water_DeerScare
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackTen_WaterMill, new Rectangle(841, 723, 32, 32), "Lake Pack - Water Mill", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[1]
          {
            TILETYPE.Water_WaterMill
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackEleven_LightsInWater, new Rectangle(874, 723, 32, 32), "Lake Pack - Lights", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[3]
          {
            TILETYPE.Water_Lanturn,
            TILETYPE.Water_LightBall,
            TILETYPE.Water_WaterLanturn
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackTwelve_SunkenShip, new Rectangle(709, 690, 32, 32), "Lake Pack - Sunken Ship", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[2]
          {
            TILETYPE.Water_FloatingCrate,
            TILETYPE.Water_FloatingBarrel
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackThirteen_SunkenShip, new Rectangle(709, 690, 32, 32), "Lake Pack - Sunken Ship", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[2]
          {
            TILETYPE.Water_Skull,
            TILETYPE.Water_TreasureChest
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackFourteen_SunkenShip, new Rectangle(709, 690, 32, 32), "Lake Pack - Sunken Ship", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[1]
          {
            TILETYPE.Water_SunkenShip
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackFifteen_Artic, new Rectangle(742, 690, 32, 32), "Lake Pack - Artic", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[2]
          {
            TILETYPE.Water_IceRocks,
            TILETYPE.Water_FlatIce
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackSixteen_Artic, new Rectangle(742, 690, 32, 32), "Lake Pack - Artic", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[2]
          {
            TILETYPE.Water_IceBoulders,
            TILETYPE.Water_IceArchRock
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackSeventeen_Mangrove, new Rectangle(775, 690, 32, 32), "Lake Pack - Mangrove", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[2]
          {
            TILETYPE.Water_MangroveTree,
            TILETYPE.Water_SmallMangroveTree
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackEighteen_SunkenRocks, new Rectangle(821, 227, 32, 32), "Lake Pack - Sunken Rocks", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[3]
          {
            TILETYPE.Water_SunkenRocksLarge,
            TILETYPE.Water_SunkenRocksMed,
            TILETYPE.Water_SunkenRocksSmall
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackNineteen_WaterAttraction, new Rectangle(808, 690, 32, 32), "Lake Pack - Float House", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[1]
          {
            TILETYPE.Water_FloatHouse
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackTwenty_WaterAttraction, new Rectangle(841, 690, 32, 32), "Lake Pack - Boat Adventure", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LakePackTwentyOne_WaterAttraction, new Rectangle(874, 690, 32, 32), "Lake Pack - Swam Boats", "Unlock 4 more to increase Visitor Happiness and Virality by 5%", 5, UpgradeCategory.Lake, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackOne_AnimalStatues, new Rectangle(775, 822, 32, 32), "Deco Pack - Animal Statues", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.MonkeyStatue,
            TILETYPE.RockElephant
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackTwo_Statues, new Rectangle(179, 570, 32, 32), "Deco Pack - Statues", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.AztacSnakeStatue,
            TILETYPE.HeShee
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackThree_Statues, new Rectangle(212, 570, 32, 32), "Deco Pack - Giant Flower", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[1]
          {
            TILETYPE.GiantSunFlower
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackFour_DesertStatues, new Rectangle(146, 570, 32, 32), "Deco Pack - Desert Statues", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.Sphinx,
            TILETYPE.Anubis
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackFive_SnowyStatues, new Rectangle(113, 570, 32, 32), "Deco Pack - Snowy Statues", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.LionSnowSculpture,
            TILETYPE.ZooIceSign
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackSix_AnimalIceStatues, new Rectangle(80, 570, 32, 32), "Deco Pack - Animal Ice Statues", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.SealIceSculpture,
            TILETYPE.BearIceSculpture
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackSeven_AnimalIceStatues, new Rectangle(80, 570, 32, 32), "Deco Pack - Animal Ice Statues", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[3]
          {
            TILETYPE.BirdIceSculpture,
            TILETYPE.GiraffeIceSculpture,
            TILETYPE.DeerIceSculpture
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackEight_Standees, new Rectangle(742, 822, 32, 32), "Deco Pack - Standees", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[3]
          {
            TILETYPE.PenguinStandee,
            TILETYPE.SealStandee,
            TILETYPE.WantedPoster
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackNine_Banners, new Rectangle(709, 822, 32, 32), "Deco Pack - Banners", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.MonkeyBanner,
            TILETYPE.CarpStreamers
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackTen_ZooSign, new Rectangle(709, 789, 32, 32), "Deco Pack - Zoo Sign", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[1]
          {
            TILETYPE.ZooStandee
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackEleven_Balloons, new Rectangle(675, 822, 32, 32), "Deco Pack - Balloons", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.GiantPigBalloon,
            TILETYPE.GiantBearBalloon
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackTwelve_Balloon, new Rectangle(47, 570, 32, 32), "Deco Pack - Balloons", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[1]
          {
            TILETYPE.HorizonValleyZooBalloon
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackThirteen_AirDancers, new Rectangle(643, 822, 32, 32), "Deco Pack - Air Dancers", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.GiraffeAirDancer,
            TILETYPE.SnakeAirDancer
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackFourteen_Archways, new Rectangle(179, 603, 32, 32), "Deco Pack - Archways", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.WhiteMetalVineArch,
            TILETYPE.BlackMetalRoseArch
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackFifteen_NatureArchways, new Rectangle(146, 603, 32, 32), "Deco Pack - Nature Archways", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[3]
          {
            TILETYPE.HedgeArchYellowFlowers,
            TILETYPE.HedgeArchWhiteFlowers,
            TILETYPE.JungleArch
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackSixteen_DesertArchways, new Rectangle(643, 789, 32, 32), "Deco Pack - Desert Archways", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.OrangeStoneArch,
            TILETYPE.BoneArch
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackSeventeen_ThemeArchways, new Rectangle(643, 789, 32, 32), "Deco Pack - Theme Archways", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.IceArch,
            TILETYPE.AztacTempleGate
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackEighteen_ThemeArchways, new Rectangle(643, 789, 32, 32), "Deco Pack - Theme Archways", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.AsianGate,
            TILETYPE.WesternArch
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackNineteen_MiniDeco, new Rectangle(212, 603, 32, 32), "Deco Pack - Totems", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.Totem,
            TILETYPE.WoodenTotem
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackTwenty_ThemedMiniDeco, new Rectangle(815, 69, 32, 32), "Deco Pack", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.SandTower,
            TILETYPE.Snowman
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackTwentyOne_OldWestMiniDeco, new Rectangle(775, 789, 32, 32), "Deco Pack - Old West", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[3]
          {
            TILETYPE.WestBarrel,
            TILETYPE.WestWoodenBox,
            TILETYPE.BoneDeco
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackTwentyTwo_OldWestMiniDeco, new Rectangle(775, 789, 32, 32), "Deco Pack - Old West", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.Cart,
            TILETYPE.Caravan
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackTwentyThree_DesertStructures, new Rectangle(775, 789, 32, 32), "Deco Pack - Old West Structures", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.WaterTower,
            TILETYPE.WesternWindmill
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackTwentyFour_Pyramid, new Rectangle(742, 789, 32, 32), "Deco Pack - Pyramid", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.Pyramid,
            TILETYPE.SmallPyramid
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackTwentyFive_LargeSign, new Rectangle(709, 789, 32, 32), "Deco Pack - Large Signs", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[2]
          {
            TILETYPE.ZooRockSign,
            TILETYPE.RockyZooSign
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackTwentySix_Temple, new Rectangle(14, 570, 32, 32), "Deco Pack - Temple", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[1]
          {
            TILETYPE.AztacTemple
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackTwentySeven_House, new Rectangle(676, 789, 32, 32), "Deco Pack - Ice Castle", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[1]
          {
            TILETYPE.IceCastle
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackTwentyEight_House, new Rectangle(85, 603, 32, 32), "Deco Pack - House", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[1]
          {
            TILETYPE.TallRoofHouse
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DecoPackTwentyNine_House, new Rectangle(52, 603, 32, 32), "Deco Pack - Lighthouse", "Unlock 2 more deco packs to get 10% money back when demolishing", 1, UpgradeCategory.Decoration, new TILETYPE[1]
          {
            TILETYPE.LightHouse
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FoodShop_ChurroShop, new Rectangle(495, 438, 32, 32), "Churro Shop", "Unlock 1 more shop to increase the number of people who can be inside a Food shop by 1", 1, UpgradeCategory.FoodShops, new TILETYPE[1]
          {
            TILETYPE.ChurroShop
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FoodShop_IceCreamTruck, new Rectangle(528, 405, 32, 32), "Ice Cream Truck", "Unlock 1 more shop to increase the number of people who can be inside a Food shop by 1", 1, UpgradeCategory.FoodShops, new TILETYPE[1]
          {
            TILETYPE.IceCreamTruck
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FoodShop_CottonCandyShop, new Rectangle(327, 702, 32, 32), "Cotton Candy Shop", "Unlock 1 more shop to increase the number of people who can be inside a Food shop by 1", 1, UpgradeCategory.FoodShops, new TILETYPE[1]
          {
            TILETYPE.CottonCandyShop
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FoodShop_TacoTruck, new Rectangle(561, 405, 32, 32), "Taco Truck", "Unlock 1 more shop to increase the number of people who can be inside a Food shop by 1", 1, UpgradeCategory.FoodShops, new TILETYPE[1]
          {
            TILETYPE.TacoTruck
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FoodShop_PandaBurg, new Rectangle(228, 702, 32, 32), "Panda Burg", "Unlock 1 more shop to increase the number of people who can be inside a Food shop by 1", 1, UpgradeCategory.FoodShops, new TILETYPE[1]
          {
            TILETYPE.PandaBurgerShop
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FoodShop_KangarooPizzaShop, new Rectangle(261, 702, 32, 32), "Kangaroo Pizza", "Unlock 1 more shop to increase the number of people who can be inside a Food shop by 1", 1, UpgradeCategory.FoodShops, new TILETYPE[1]
          {
            TILETYPE.KangarooPizzaShop
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FoodShop_BigIceCreamShop, new Rectangle(294, 702, 32, 32), "Ice Cream Shop", "Unlock 1 more shop to increase the number of people who can be inside a Food shop by 1", 1, UpgradeCategory.FoodShops, new TILETYPE[1]
          {
            TILETYPE.BigIceCreamShop
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FoodShop_PretzelShop, new Rectangle(528, 438, 32, 32), "Pretzel Shop", "Unlock 1 more shop to increase the number of people who can be inside a Food shop by 1", 1, UpgradeCategory.FoodShops, new TILETYPE[1]
          {
            TILETYPE.PretzelShop
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FoodShop_PopcornWeaselShop, new Rectangle(561, 438, 32, 32), "Popcorn Weasel Shop", "Unlock 1 more shop to increase the number of people who can be inside a Food shop by 1", 1, UpgradeCategory.FoodShops, new TILETYPE[1]
          {
            TILETYPE.PopcornWeaselShop
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FoodShop_ShellShack, new Rectangle(294, 669, 32, 32), "Shell Shack Shop", "Unlock 1 more shop to increase the number of people who can be inside a Food shop by 1", 1, UpgradeCategory.FoodShops, new TILETYPE[1]
          {
            TILETYPE.ShellShackShop
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Factory_EggBatteryFarm, new Rectangle(33, 89, 32, 32), "Factory - Egg Battery", "Unlock 1 more factory to increase output from killing animals from factories", 1, UpgradeCategory.Factories, new TILETYPE[1]
          {
            TILETYPE.EggBatteryFarm
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Factory_MilkBatteryFarm, new Rectangle(0, 89, 32, 32), "Factory - Milk Battery", "Unlock 1 more factory to increase output from killing animals from factories", 1, UpgradeCategory.Factories, new TILETYPE[1]
          {
            TILETYPE.MilkBatteryFarm
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Factory_BaconFactory, new Rectangle(0, 122, 32, 32), "Factory - Bacon Battery", "Unlock 1 more factory to increase output from killing animals from factories", 1, UpgradeCategory.Factories, new TILETYPE[1]
          {
            TILETYPE.BaconFactory
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Factory_BuffaloWingFactory, new Rectangle(66, 89, 32, 32), "Factory - Buffalo Wing Battery", "Unlock 1 more factory to increase output from killing animals from factories", 1, UpgradeCategory.Factories, new TILETYPE[1]
          {
            TILETYPE.BuffaloWingFactory
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Factory_GlueFactory, new Rectangle(66, 122, 32, 32), "Factory - Glue Battery", "Unlock 1 more factory to increase output from killing animals from factories", 1, UpgradeCategory.Factories, new TILETYPE[1]
          {
            TILETYPE.GlueFactory
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Factory_SlaughterHouse, new Rectangle(33, 122, 32, 32), "Factory - Slaughterhouse", "Unlock 1 more factory to increase output from killing animals from factories", 1, UpgradeCategory.Factories, new TILETYPE[1]
          {
            TILETYPE.Slaughterhouse
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Factory_SnakeSkinFactory, new Rectangle(917, 191, 32, 32), "Factory - Snake Skin Factory", "Unlock 1 more factory to increase output from killing animals from factories", 1, UpgradeCategory.Factories, new TILETYPE[1]
          {
            TILETYPE.SnakeSkinFactory
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Factory_CrocHandbagFactory, new Rectangle(369, 381, 32, 32), "Factory - Croc Handbag Factory", "Unlock 1 more factory to increase output from killing animals from factories", 1, UpgradeCategory.Factories, new TILETYPE[1]
          {
            TILETYPE.CrocHandbagFactory
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DrinkShop_CoconutShop, new Rectangle(542, 570, 32, 32), "Coconut Shop", "Unlock 1 more to increase the number of people who can be inside a Drink shop by 2", 1, UpgradeCategory.DrinksShop, new TILETYPE[1]
          {
            TILETYPE.CoconutShop
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DrinkShop_Slushy, new Rectangle(509, 570, 32, 32), "Slushie Shop", "Unlock 1 more to increase the number of people who can be inside a Drink shop by 2", 1, UpgradeCategory.DrinksShop, new TILETYPE[1]
          {
            TILETYPE.SlushieShop
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DrinkShop_KatCoffee, new Rectangle(476, 570, 32, 32), "Kat Coffee Shop", "Unlock 1 more to increase the number of people who can be inside a Drink shop by 2", 1, UpgradeCategory.DrinksShop, new TILETYPE[1]
          {
            TILETYPE.KatCoffeeShop
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.DrinkShop_RustyKeg, new Rectangle(443, 570, 32, 32), "Rusty Keg Shop", "Unlock 1 more to increase the number of people who can be inside a Drink shop by 2", 1, UpgradeCategory.DrinksShop, new TILETYPE[1]
          {
            TILETYPE.RustyKegShop
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.VendingMachine, new Rectangle(410, 570, 32, 32), "Vending Machine", "Increase Vending Machine Speed by 5%", 1, UpgradeCategory.DrinksShop, new TILETYPE[1]
          {
            TILETYPE.ChocolateVendingMachine
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.GiftShop_BalloonShop, new Rectangle(409, 471, 32, 32), "Balloon Shop", "Unlock 1 more gift shop to add scent to make temptation rating higher", 1, UpgradeCategory.GiftShops, new TILETYPE[1]
          {
            TILETYPE.BalloonShop
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.ATMMachine, new Rectangle(443, 603, 32, 32), "ATM Machine", "Allows customers to withdraw more money", 10, UpgradeCategory.ATM, new TILETYPE[1]
          {
            TILETYPE.ATMMachine
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LamppostPackOne_Classic, new Rectangle(352, 189, 32, 32), "Lamppost Pack - Classic", "PLACEHOLDER", 1, UpgradeCategory.Lamppost, new TILETYPE[2]
          {
            TILETYPE.ClassicLampPost,
            TILETYPE.WhiteClassicLampPost
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LampppostPackTwo, new Rectangle(749, 36, 32, 32), "Lamppost Pack", "PLACEHOLDER", 1, UpgradeCategory.Lamppost, new TILETYPE[2]
          {
            TILETYPE.BallLampPost,
            TILETYPE.CurlLampPost
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LamppostPackThree_TwinLights, new Rectangle(854, 124, 32, 32), "Lamppost Pack - Twin Lights", "PLACEHOLDER", 1, UpgradeCategory.Lamppost, new TILETYPE[2]
          {
            TILETYPE.TwinLamppost,
            TILETYPE.TwinCurlsLampPost
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LamppostPackFour_ThemedLights, new Rectangle(683, 36, 32, 32), "Lamppost Pack - Theme Lights", "PLACEHOLDER", 1, UpgradeCategory.Lamppost, new TILETYPE[3]
          {
            TILETYPE.WoodenLampPost,
            TILETYPE.TripletsLampPost,
            TILETYPE.AsianLight
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LammpostPackFive_LowLights, new Rectangle(716, 36, 32, 32), "Lamppost Pack - Low Lights", "PLACEHOLDER", 1, UpgradeCategory.Lamppost, new TILETYPE[2]
          {
            TILETYPE.FloorLight,
            TILETYPE.AztecTorch
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.LamppostPackSix_AnimalLights, new Rectangle(650, 41, 32, 32), "Lamppost Pack - Animal Lights", "PLACEHOLDER", 1, UpgradeCategory.Lamppost, new TILETYPE[2]
          {
            TILETYPE.FlamingoLampPost,
            TILETYPE.SealLampPost
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalShelterPackOne_Cave, new Rectangle(165, 89, 32, 32), "Animal Shelter - Cave", "Unlock 1 more shelter for shelters to give animals 5% more privacy", 1, UpgradeCategory.AnimalShelter, new TILETYPE[2]
          {
            TILETYPE.Shelter_SmallRockCave,
            TILETYPE.Shelter_LargeRockCave
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalShelterPackTwo, new Rectangle(165, 122, 32, 32), "Animal Shelter", "Unlock 1 more shelter for shelters to give animals 5% more privacy", 1, UpgradeCategory.AnimalShelter, new TILETYPE[1]
          {
            TILETYPE.Shelter_LargeWoodenHouse
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalShelterPackThree, new Rectangle(132, 122, 32, 32), "Animal Shelter", "Unlock 1 more shelter for shelters to give animals 5% more privacy", 1, UpgradeCategory.AnimalShelter, new TILETYPE[1]
          {
            TILETYPE.Shelter_MossyRock
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalShelterPackFour_Ice, new Rectangle(495, 405, 32, 32), "Animal Shelter", "Unlock 1 more shelter for shelters to give animals 5% more privacy", 1, UpgradeCategory.AnimalShelter, new TILETYPE[2]
          {
            TILETYPE.Shelter_IceRocks,
            TILETYPE.Shelter_Igloo
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentOne_WoodenLogs, new Rectangle(66, 155, 32, 32), "Animal Enrichment - Wooden Logs", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[3]
          {
            TILETYPE.Enrichment_WoodenLogs,
            TILETYPE.Enrichment_WoodenBeam2,
            TILETYPE.Enrichment_WoodenBeam3
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentTwo_BoneToy, new Rectangle(0, 155, 32, 32), "Animal Enrichment - Chew Toys", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[3]
          {
            TILETYPE.Enrichment_ChewToyPurpleBone,
            TILETYPE.Enrichment_ChewToyBrownBone,
            TILETYPE.Enrichment_ChewToyRope
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentThree_WaterSprinklers, new Rectangle(353, 801, 32, 32), "Animal Enrichment - Water Sprinklers", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[1]
          {
            TILETYPE.Enrichment_WaterSprinklers
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentFour_Cliff, new Rectangle(132, 155, 32, 32), "Animal Enrichment - Cliff", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[3]
          {
            TILETYPE.Enrichment_RockCliff,
            TILETYPE.Enrichment_IceCliff,
            TILETYPE.Enrichment_BrownCliff
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentFive_ScentMarker, new Rectangle(353, 834, 32, 32), "Animal Enrichment - Scent Markers", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[3]
          {
            TILETYPE.Enrichment_ScentMarkerGrey,
            TILETYPE.Enrichment_ScentMarkerGreen,
            TILETYPE.Enrichment_ScentMarkerBrown
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentSix_TugBallJollyBall, new Rectangle(353, 867, 32, 32), "Animal Enrichment - Jollyball", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[1]
          {
            TILETYPE.Enrichment_TugBallJollyBallYellow
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentSeven_SmallBall, new Rectangle(353, 768, 32, 32), "Animal Enrichment - Small Ball", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[5]
          {
            TILETYPE.Enrichment_SmallBlueBall,
            TILETYPE.Enrichment_SmallCyanBall,
            TILETYPE.Enrichment_SmallGreenBall,
            TILETYPE.Enrichment_SmallRedBall,
            TILETYPE.Enrichment_SmallPinkBall
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentEight_LargeBall, new Rectangle(353, 735, 32, 32), "Animal Enrichment - Large Ball", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[5]
          {
            TILETYPE.Enrichment_LargeRedBall,
            TILETYPE.Enrichment_LargeWhiteBall,
            TILETYPE.Enrichment_LargeGreenBall,
            TILETYPE.Enrichment_LargeYellowBall,
            TILETYPE.Enrichment_LargeBlueBall
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentNine_WoodenPosts, new Rectangle(491, 372, 32, 32), "Animal Enrichment - Scratching Post", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[2]
          {
            TILETYPE.Enrichment_ScratchingPostWood,
            TILETYPE.Enrichment_ScratchingPoleTree
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentTen_Cardboard, new Rectangle(231, 122, 32, 32), "Animal Enrichment - Cardboard", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[2]
          {
            TILETYPE.Enrichment_CardboardBox2,
            TILETYPE.Enrichment_LargeCardboardBox
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentEleven_Tunnel, new Rectangle(33, 155, 32, 32), "Animal Enrichment - Tunnels", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[2]
          {
            TILETYPE.Enrichment_TunnelGreen,
            TILETYPE.Enrichment_TunnelWoodenLog
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentTwelve_RopeToy, new Rectangle(198, 122, 32, 32), "Animal Enrichment - Rope Toy", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[1]
          {
            TILETYPE.Enrichment_TugRopeToy
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentThirteen_RockPerch, new Rectangle(165, 155, 32, 32), "Animal Enrichment - Rock Perch", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[5]
          {
            TILETYPE.Enrichment_RockPerch,
            TILETYPE.Enrichment_BrownRockPerch,
            TILETYPE.Enrichment_YellowRockPerch,
            TILETYPE.Enrichment_LogPerch,
            TILETYPE.Enrichment_NetPerch
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentFourteen_HighWoodBeamPerch, new Rectangle(99, 155, 32, 32), "Animal Enrichment - High Perch", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[3]
          {
            TILETYPE.Enrichment_HighWoodBeamPerch,
            TILETYPE.Enrichment_HighPerch,
            TILETYPE.Enrichment_TreeHighPerch
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentFifteen_Trampoline, new Rectangle(557, 372, 32, 32), "Animal Enrichment - Trampoline", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[2]
          {
            TILETYPE.Enrichment_BlueTrampoline,
            TILETYPE.Enrichment_PinkTrampoline
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentSixteen_CarTire, new Rectangle(198, 89, 32, 32), "Animal Enrichment - Car Tire", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[2]
          {
            TILETYPE.Enrichment_FlatCarTire,
            TILETYPE.Enrichment_HangingCarTire
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentSeventeen_SaltBlock, new Rectangle(231, 89, 32, 32), "Animal Enrichment - Salt Block", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[1]
          {
            TILETYPE.Enrichment_SaltBlock
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AnimalEnrichmentEighteen_Mirror, new Rectangle(524, 372, 32, 32), "Animal Enrichment - Mirror", "Unlock 4 more to increase effectiveness per enrichment item by 5%", 1, UpgradeCategory.AnimalEnrichment, new TILETYPE[2]
          {
            TILETYPE.Enrichment_MirrorRect,
            TILETYPE.Enrichment_MirrorRound
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FountainOne_Mini, new Rectangle(429, 537, 32, 32), "Fountain - Mini Fountain", "Unlock 2 more to increase Water Pump Range by 2 tiles", 2, UpgradeCategory.Fountains, new TILETYPE[1]
          {
            TILETYPE.MiniFountain
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FountainTwo_ElegantTallFountain, new Rectangle(429, 537, 32, 32), "Fountain - Tall Fountain", "Unlock 2 more to increase Water Pump Range by 2 tiles", 2, UpgradeCategory.Fountains, new TILETYPE[1]
          {
            TILETYPE.ElegantTallFountain
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FountainThree_Elephant, new Rectangle(462, 537, 32, 32), "Fountain - Elephant Fountains", "Unlock 2 more to increase Water Pump Range by 2 tiles", 2, UpgradeCategory.Fountains, new TILETYPE[2]
          {
            TILETYPE.ElephantFountain,
            TILETYPE.ElephantMarbleFountain
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FountainFour_Waterfall, new Rectangle(528, 537, 32, 32), "Fountain - Waterfall", "Unlock 2 more to increase Water Pump Range by 2 tiles", 2, UpgradeCategory.Fountains, new TILETYPE[1]
          {
            TILETYPE.RockyWaterfall
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FountainFive_Waterfall, new Rectangle(495, 537, 32, 32), "Fountain - Waterfall", "Unlock 2 more to increase Water Pump Range by 2 tiles", 2, UpgradeCategory.Fountains, new TILETYPE[1]
          {
            TILETYPE.JungleWaterfall
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.InsectExhibitOne_Glass, new Rectangle(874, 855, 32, 32), "Insect Exhibit", "Unlock 2 more to increase visitor education points by 5%", 1, UpgradeCategory.Insects, new TILETYPE[1]
          {
            TILETYPE.GlassExhibit
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.InsectExhibitTwo_Beetle, new Rectangle(874, 855, 32, 32), "Insect Exhibit - Beetle", "Unlock 2 more to increase visitor education points by 5%", 1, UpgradeCategory.Insects, new TILETYPE[1]
          {
            TILETYPE.BeetleGlassExhibit
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.InsectExhibitThree_Butterfly, new Rectangle(874, 756, 32, 32), "Insect Exhibit - Butterfly", "Unlock 2 more to increase visitor education points by 5%", 1, UpgradeCategory.Insects, new TILETYPE[1]
          {
            TILETYPE.ButterflyGlassExhibit
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PenWaterOne_DoubleTroughs, new Rectangle(132, 89, 32, 32), "Pen Water - Large Troughs", "Unlock 2 more to have self-filtering water", 1, UpgradeCategory.Pens, new TILETYPE[3]
          {
            TILETYPE.WaterTrough_Metal,
            TILETYPE.WaterTrough_Wooden,
            TILETYPE.WaterTrough_Rock
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PenWaterTwo_DoubleTroughs, new Rectangle(99, 122, 32, 32), "Pen Water - Large Troughs", "Unlock 2 more to have self-filtering water", 1, UpgradeCategory.Pens, new TILETYPE[2]
          {
            TILETYPE.WaterTrough_Leaf,
            TILETYPE.WaterTrough_IceRock
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AttractionOne_MicroAttractions, new Rectangle(353, 636, 32, 32), "Attractions - Micro Attractions", "Unlock 2 more attractions for machines in attractions increase durability and require less maintanance", 5, UpgradeCategory.Attractions, new TILETYPE[2]
          {
            TILETYPE.CoinSquasher,
            TILETYPE.Binoculars
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AttractionTwo_RentARidable, new Rectangle(542, 603, 32, 32), "Attractions - Rent A Ridable", "Unlock 2 more attractions for machines in attractions increase durability and require less maintanance", 5, UpgradeCategory.Attractions, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AttractionThree_Caruosel, new Rectangle(254, 636, 32, 32), "Attractions - Caruosel", "Unlock 2 more attractions for machines in attractions increase durability and require less maintanance", 5, UpgradeCategory.Attractions, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AttractionFour_HippoRiverRapids, new Rectangle(287, 636, 32, 32), "Attractions - Hippo River Rapids", "Unlock 2 more attractions for machines in attractions increase durability and require less maintanance", 5, UpgradeCategory.Attractions, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AttractionFive_PettingZoo, new Rectangle(320, 636, 32, 32), "Attractions - Petting Zoo", "Unlock 2 more attractions for machines in attractions increase durability and require less maintanance", 5, UpgradeCategory.Attractions, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AttractionSix_TreeExhibit, new Rectangle(221, 636, 32, 32), "Attractions - Tree Exhibit", "Unlock 2 more attractions for machines in attractions increase durability and require less maintanance", 5, UpgradeCategory.Attractions, new TILETYPE[1]
          {
            TILETYPE.TreeExhibit
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AttractionSeven_AnimalShow, new Rectangle(228, 669, 32, 32), "Attractions - Animal Show", "Unlock 2 more attractions for machines in attractions increase durability and require less maintanance", 5, UpgradeCategory.Attractions, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AttractionEight_AnimalRiding, new Rectangle(476, 603, 32, 32), "Attractions - Animal Riding", "Unlock 2 more attractions for machines in attractions increase durability and require less maintanance", 5, UpgradeCategory.Attractions, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AttractionNine_TigerSelfie, new Rectangle(981, 629, 32, 32), "Attractions - Tiger Selfie", "Unlock 2 more attractions for machines in attractions increase durability and require less maintanance", 5, UpgradeCategory.Attractions, new TILETYPE[1]
          {
            TILETYPE.TigerPhoto
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AttractionTen_HelicopterTour, new Rectangle(261, 669, 32, 32), "Attractions - Helicopter Tour", "Unlock 2 more attractions for machines in attractions increase durability and require less maintanance", 5, UpgradeCategory.Attractions, new TILETYPE[1]
          {
            TILETYPE.HelicopterRide
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AttractionEleven_HotAirBalloon, new Rectangle(509, 603, 32, 32), "Attractions - Hot Air Balloon", "Unlock 2 more attractions for machines in attractions increase durability and require less maintanance", 5, UpgradeCategory.Attractions, new TILETYPE[1]
          {
            TILETYPE.HotAirBalloonRide
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.AttractionTwelve_AnimalColosseum, new Rectangle(577, 662, 32, 32), "Attractions - Animal Colosseum", "Unlock 2 more attractions for machines in attractions increase durability and require less maintanance", 5, UpgradeCategory.Attractions, new TILETYPE[1]
          {
            TILETYPE.AnimalColosseum
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.BinPackOne_Animal, new Rectangle(973, 855, 32, 32), "Bins - Animal Bin", "Unlock 1 more bin to increase Trash Capacity by 10%", 1, UpgradeCategory.Bins, new TILETYPE[1]
          {
            TILETYPE.PenguinTrashbin
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.BinPackTwo_Animal, new Rectangle(973, 855, 32, 32), "Bins - Animal Bins", "Unlock 1 more bin to increase Trash Capacity by 10%", 1, UpgradeCategory.Bins, new TILETYPE[2]
          {
            TILETYPE.LionTrashbin,
            TILETYPE.BearTrashbin
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.BinPackThree, new Rectangle(973, 855, 32, 32), "Bins - Wooden Bin", "Unlock 1 more bin to increase Trash Capacity by 10%", 1, UpgradeCategory.Bins, new TILETYPE[1]
          {
            TILETYPE.WoodenTrashbin
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.BinPackFour_RecyclingBin, new Rectangle(973, 822, 32, 32), "Bins - Recycling Bin", "Unlock 1 more bin to increase Trash Capacity by 10%", 1, UpgradeCategory.Bins, new TILETYPE[1]
          {
            TILETYPE.RecyclingBin
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.EmployeeUpgradeOne_Mascots, new Rectangle(264, 537, 32, 32), "Employee Upgrades - Mascots", "Unlock 5 more to increase Staff Walk speed by 10%", 1, UpgradeCategory.Employee, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.EmployeeUpgradeTwo_Tours, new Rectangle(297, 537, 32, 32), "Employee Upgrades - Tours", "Unlock 5 more to increase Staff Walk speed by 10%", 1, UpgradeCategory.Employee, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.EmployeeUpgradeThree_Volunteers, new Rectangle(165, 537, 32, 32), "Employee Upgrades - Volunteers", "Unlock 5 more to increase Staff Walk speed by 10%", 1, UpgradeCategory.Employee, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.EmployeeUpgradeFour_PepTalks, new Rectangle(198, 537, 32, 32), "Employee Upgrades - Pep Talks", "Unlock 5 more to increase Staff Walk speed by 10%", 1, UpgradeCategory.Employee, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.EmployeeUpgradeFive_Advertising, new Rectangle(231, 537, 32, 32), "Employee Upgrades - Advertising", "Unlock 5 more to increase Staff Walk speed by 10%", 1, UpgradeCategory.Employee, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.EmployeeUpgradeSix_IllegalImmigrants, new Rectangle(132, 537, 32, 32), "Employee Upgrades - Illegal Immigrants", "Unlock 5 more to increase Staff Walk speed by 10%", 1, UpgradeCategory.Employee, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.BreedingFacility_AddRooms, new Rectangle(643, 690, 32, 32), "Breeding Facility - Add Rooms", "Adds more breeding rooms.", 10, UpgradeCategory.AnimalNursery, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.BreedingFacility_UltraSound, new Rectangle(574, 471, 32, 32), "Breeding Facility - Ultrasound", "Adds baby prediction to breeding rooms, allows you to predict the Sex and Appearance of any offspring before it is borm, and abort it if you want to", 10, UpgradeCategory.AnimalNursery, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FoodShop_ConvertToLabMeat, new Rectangle(327, 669, 32, 32), "Food Shop - Convert to Lab Meat", "Become able to convert all meat to lab meat", 10, UpgradeCategory.FoodShops, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.ImprobableBuilding, new Rectangle(327, 669, 32, 32), "Improabable Building", "Generates self-sustaining food for carnivores", 10, new TILETYPE[1]
          {
            TILETYPE.ImpossibleBuilding
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.PenWaterThree_BuildWater, new Rectangle(99, 89, 32, 32), "Pen Water - Build Water", "Unlock 2 more to have self-filtering water", 2, UpgradeCategory.Pens, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.BreedingFacility_RareBreeds, new Rectangle(245, 603, 32, 32), "Breeding Facility - Rare Breeds", "Increases chances of getting a rare breed", 10, UpgradeCategory.Advertising, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Advertising_MorePeople, new Rectangle(377, 570, 32, 32), "Advertisments", "More people will come to your zoo.", 10, UpgradeCategory.Advertising, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Advertising_AnimalDonors, new Rectangle(344, 570, 32, 32), "Advertising - Animal Donors", "Increase chances of animal donations to the zoo.", 10, UpgradeCategory.Advertising, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Advertising_RicherPeople, new Rectangle(311, 570, 32, 32), "Advertising - Money", "People who come to your zoo will start with more money/More VIPS will come", 1, UpgradeCategory.Advertising, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Advertising_Bribery, new Rectangle(278, 570, 32, 32), "Advertising - Bribery", "Allows you to bribe people for good reviews.", 10, UpgradeCategory.Advertising, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Advertising_Sponsors, new Rectangle(245, 570, 32, 32), "Advertising - Sponsors", "Unlock Sponsors - Sponsors will donate to your zoo as passive income.", 10, UpgradeCategory.Advertising, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Ticketing_SeasonPass, new Rectangle(541, 471, 32, 32), "Ticketing - Season Pass", "Unlock Season Pass.", 1, UpgradeCategory.TicketingBooth, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.ZooBusOne, new Rectangle(442, 471, 32, 32), "Zoo Bus Upgrade - Red Bus", "Unlock to bring more visitors per day.", 10, UpgradeCategory.ZooBus, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.ZooBusTwo, new Rectangle(475, 471, 32, 32), "Zoo Bus Upgrade - Upsized White Bus", "Unlock to bring more visitors per day.", 10, UpgradeCategory.ZooBus, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.ZooBusThree, new Rectangle(508, 471, 32, 32), "Zoo Bus Upgrade - Double Decker", "Unlock to bring more visitors per day.", 10, UpgradeCategory.ZooBus, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Security_HireGuards, new Rectangle(278, 603, 32, 32), "Security - Guards", "Unlocks Security Guards.", 10, UpgradeCategory.Security, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Security_SecurityRooms, new Rectangle(311, 603, 32, 32), "Security - Security Rooms", "Unlocks Security Rooms - Guards can be stationed here so they can get to places quicker.", 10, UpgradeCategory.Security, new TILETYPE[1]
          {
            TILETYPE.SurveillanceBuilding
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Security_BribePolice, new Rectangle(344, 603, 32, 32), "Security - Bribery", "Unlock this to hire thugs to beat up police, or bribe policeman.", 10, UpgradeCategory.Security, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Security_Flyers, new Rectangle(377, 603, 32, 32), "Security - Flyers", "Put up Educational Flyers at Entrance, causing vandals and hooligans appear less", 10, UpgradeCategory.Security, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Security_Cameras, new Rectangle(410, 603, 32, 32), "Security - Cameras", "Unlock Security Cameras to spot activities sooner.", 10, UpgradeCategory.Security, new TILETYPE[1]
          {
            TILETYPE.Security
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TransportOne, new Rectangle(366, 505, 32, 32), "Transport - Tram", "Unlocks trams and tram stations.", 10, UpgradeCategory.Transport, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TransportTwo, new Rectangle(399, 504, 32, 32), "Transport - Train", "Unlocks trains and train stations.", 10, UpgradeCategory.Transport, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Animals_Training, new Rectangle(432, 504, 32, 32), "Animals - Training", "Animal less upset when humans are in range", 10, UpgradeCategory.Animals, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Animals_Supplements, new Rectangle(465, 504, 32, 32), "Animals - Supplements", "Animals lifespan increase by 5%", 10, UpgradeCategory.Animals, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Animals_NutritionTablets, new Rectangle(498, 504, 32, 32), "Animals - Nutrition Tablets", "Animals will require less food.", 10, UpgradeCategory.Animals, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Animals_BlackMarket, new Rectangle(531, 504, 32, 32), "Animals - Black Market", "Black Market Trader will stop by more often.", 10, UpgradeCategory.Animals, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Animals_Cheaper, new Rectangle(564, 504, 32, 32), "Animals - Good Reputation", "Animals sold at shop are cheaper.", 10, UpgradeCategory.Animals, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Animals_AnimalRelease, new Rectangle(940, 789, 32, 32), "Animal Rehabilitation Building", "Allows you to rehabilitate animals and set them free, back into the wild.", 10, UpgradeCategory.Facilies, new TILETYPE[1]
          {
            TILETYPE.AnimalRehabilitationBuilding
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Animals_Sedation, new Rectangle(973, 789, 32, 32), "Animals - Sedation", "Animals will become less agressive and unlikely to break out.", 10, UpgradeCategory.Animals, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Research_IncreaseLifespan, new Rectangle(396, 537, 32, 32), "Research Animals", "Increase lifespan of genetically modified animals.", 10, UpgradeCategory.Research, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Storeroom_StockCapacity, new Rectangle(99, 537, 32, 32), "Storeroom - Stock Capacity", "Increase Stock Capacity", 10, UpgradeCategory.StoreRoom, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Storeroom_AddFridge, new Rectangle(66, 537, 32, 32), "Storeroom - Add Fridge", "Increase Shelf life of items in storerooms.", 10, UpgradeCategory.StoreRoom, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Storeroom_Post, new Rectangle(33, 537, 32, 32), "Storeroom - Post Systems", "Decreases the time taken for items to come in.", 10, UpgradeCategory.StoreRoom, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Storeroom_AniMallMembership, new Rectangle(0, 537, 32, 32), "Storeroom - AniMall Membership", "Reduce cost of order. ", 10, UpgradeCategory.StoreRoom, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.InfoBooth, new Rectangle(363, 537, 32, 32), "Info Booth", "Allows you to see customer surveys.", 10, UpgradeCategory.Facilies, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.StaffRoom, new Rectangle(330, 537, 32, 32), "Staff Room", "Allows staff to stay in zoo instead of leaving.", 10, UpgradeCategory.Facilies, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.CRISPR, new Rectangle(544, 662, 32, 32), "CRISPR DNA Building", "Allows you to edit animal genes to create new species.", 10, UpgradeCategory.Facilies, new TILETYPE[1]
          {
            TILETYPE.DNABuilding
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.CommercialWarehouse, new Rectangle(610, 662, 32, 32), "Commercial Warehouse", "Allows you store and sell processed products from your zoo.", 10, UpgradeCategory.Facilies, new TILETYPE[1]
          {
            TILETYPE.Warehouse
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Vets_DiseaseResearch, new Rectangle(907, 855, 32, 32), "Vets - Disease Research", "Become able to cure more animal illnesses.", 10, UpgradeCategory.Vets, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Vets_EmployeeTraining, new Rectangle(907, 822, 32, 32), "Vets - Employee Training", "Speed up time taken to treat animals.", 10, UpgradeCategory.Vets, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Vets_AnimalVaccination, new Rectangle(940, 822, 32, 32), "Vets - Animal Vaccination", "Animals become less prone to disease", 10, UpgradeCategory.Vets, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Vets_ExperiencedWorkers, new Rectangle(940, 855, 32, 32), "Vets - More Experience", "More experienced vets turn up for hiring.", 10, UpgradeCategory.Vets, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FencePackOne, new Rectangle(874, 822, 32, 32), "Fence Pack", "PLACEHOLDER", 1, UpgradeCategory.Fences, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.FencePackTwo, new Rectangle(874, 822, 32, 32), "Fence Pack", "PLACEHOLDER", 1, UpgradeCategory.Fences, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.GiftShop_ElephantGiftShop, new Rectangle(376, 471, 32, 32), "Gift Shop", "Unlock 1 more gift shop to add scent to make temptation rating higher", 1, UpgradeCategory.GiftShops, new TILETYPE[1]
          {
            TILETYPE.ElephantGiftShop
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Farm_UnlockFarming, new Rectangle(544, 695, 32, 32), "Farming Unlock", "Allows you to grow crops and hire farmers.", 1, UpgradeCategory.Farm, new TILETYPE[2]
          {
            TILETYPE.Farmhouse,
            TILETYPE.EmptySoilPatch
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Farm_Windmill, new Rectangle(610, 695, 32, 32), "Windmill", "Allows you to process harvested Wheat into Flour.", 1, UpgradeCategory.Farm, new TILETYPE[1]
          {
            TILETYPE.Windmill
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Farm_CropsProcessingBuilding, new Rectangle(445, 662, 32, 32), "Crops Processing Building", "Allows you to process harvested crops into other products.", 1, UpgradeCategory.Farm, new TILETYPE[1]
          {
            TILETYPE.FarmProcessor
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Farm_Silo, new Rectangle(511, 662, 32, 32), "Silo", "Unlock the Silo decoration.", 1, UpgradeCategory.Farm, new TILETYPE[1]
          {
            TILETYPE.Silo
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Farm_Barn, new Rectangle(577, 695, 32, 32), "Barn", "Allows you to morally milk your cows and get eggs from your chickens.", 1, UpgradeCategory.Farm, new TILETYPE[1]
          {
            TILETYPE.Barn
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Farm_Greenhouse, new Rectangle(511, 695, 32, 32), "Greenhouse", "Unlock the Greenhouse decoration.", 1, UpgradeCategory.Farm, new TILETYPE[1]
          {
            TILETYPE.Greenhouse
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.Farm_Beerbrewery, new Rectangle(478, 695, 32, 32), "Beer Brewery", "Allows you to turn harvested hops to beer.", 1, UpgradeCategory.Farm, new TILETYPE[1]
          {
            TILETYPE.BeerBrewery
          }));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
          RGrid_Data.ResearchEntries.Add(new REntry(UnlockTYPE.TempValue, new Rectangle(742, 657, 32, 32), "PLACEHOLDER", "Placeholder", 1, new TILETYPE[0]));
        }
      }
      RGrid_Data.SortEntriesIntoHashSets(RGrid_Data.ResearchEntries);
      return RGrid_Data.ResearchEntries;
    }

    public static bool IsThisAStarBuildingUnlock(UnlockTYPE unlockType) => unlockType == UnlockTYPE.BreedingFacility || unlockType == UnlockTYPE.Security_Cameras;

    private static void SortEntriesIntoHashSets(List<REntry> entries)
    {
      RGrid_Data.UpgradeCategories = new UpgradeCategoryHolder[41];
      for (int index = 0; index < entries.Count; ++index)
      {
        UpgradeCategory upgradeCategory = entries[index].upgradeCategory;
        if (upgradeCategory != UpgradeCategory.Count)
        {
          if (RGrid_Data.UpgradeCategories[(int) upgradeCategory] == null)
            RGrid_Data.UpgradeCategories[(int) upgradeCategory] = new UpgradeCategoryHolder();
          RGrid_Data.UpgradeCategories[(int) upgradeCategory].unlocks.Add(entries[index].unlocktype);
        }
      }
    }

    internal static int GetNumberUnlockedFromThisSet(
      UpgradeCategory upgradeCategory,
      Player player,
      out int totalInSet)
    {
      if (upgradeCategory == UpgradeCategory.Count)
      {
        totalInSet = -1;
        return -1;
      }
      if (RGrid_Data.UpgradeCategories == null || RGrid_Data.UpgradeCategories[(int) upgradeCategory] == null)
        RGrid_Data.GetResearchData();
      int num = 0;
      totalInSet = 0;
      if (RGrid_Data.UpgradeCategories[(int) upgradeCategory] != null)
      {
        HashSet<UnlockTYPE> unlocks = RGrid_Data.UpgradeCategories[(int) upgradeCategory].unlocks;
        totalInSet = unlocks.Count;
        foreach (UnlockTYPE unlockType in unlocks)
        {
          if (player.unlocks.UnlockedThings[(int) unlockType] > -1)
            ++num;
        }
      }
      return num;
    }

    public static string GetBonusSetsString(UpgradeCategory upgradeCategory, Player player) => RGrid_Data.GetBonusSetsString(upgradeCategory, player, out int _, out int _, out ResearchUpgradeInfoSet _, out int _);

    public static string GetBonusSetsString(
      UpgradeCategory upgradeCategory,
      Player player,
      out int numberUnlocked,
      out int totalInSet,
      out ResearchUpgradeInfoSet tiers,
      out int nextTierIndex)
    {
      string str = string.Empty;
      tiers = (ResearchUpgradeInfoSet) null;
      nextTierIndex = -1;
      numberUnlocked = RGrid_Data.GetNumberUnlockedFromThisSet(upgradeCategory, player, out totalInSet);
      if (numberUnlocked == -1)
        return str;
      int num = 0;
      tiers = RGrid_Data.GetUpgradeTiers(upgradeCategory);
      for (int index = 0; index < tiers.researchinfo.Count; ++index)
      {
        if (numberUnlocked < tiers.researchinfo[index].TotalInSet)
        {
          nextTierIndex = index;
          num = tiers.researchinfo[index].TotalInSet - numberUnlocked;
          break;
        }
      }
      if (num == 0)
        return tiers.researchinfo.Count == 0 ? str : "You have unlocked all packs of this category!";
      if (Z_DebugFlags.IsBetaVersion)
      {
        switch (upgradeCategory)
        {
          case UpgradeCategory.Trees:
            str = string.Format("Unlock {0} more Deco Packs to increase tree deco value by 10%.", (object) num, (object) 5);
            break;
          case UpgradeCategory.Floors:
            str = string.Format("Unlock {0} more Deco2 Packs to decrease customer energy use from walking by 20%", (object) num, (object) 4);
            break;
          case UpgradeCategory.AnimalEnrichment:
            str = string.Format("Unlock {0} more Animal Enrichment Packs to unlock animal psychology studies.", (object) num, (object) 2);
            break;
          case UpgradeCategory.GiftShops:
            str = string.Format("Unlock {0} more Facility Packs to add credit card support to vending machines, leaving more money in your guests pockets to spend on other things.", (object) num, (object) 3);
            break;
          case UpgradeCategory.Research:
            str = string.Format("Unlock {0} more Tech Packs to increase capacity for 1 more researcher.", (object) num, (object) 1);
            break;
        }
      }
      else
      {
        switch (upgradeCategory)
        {
          case UpgradeCategory.Benches:
            str = string.Format("Unlock {0} more Bench Packs to increase Bench Max Energy Cap by {1}%.", (object) num, (object) 5);
            break;
          case UpgradeCategory.Shelters:
            str = string.Format("Unlock {0} more Shelter Packs to increase Shelters Max Energy Cap by {1}%.", (object) num, (object) 5);
            break;
          case UpgradeCategory.Trees:
            str = string.Format("Unlock {0} more Tree Packs to increase tree decoration score bonus by {1}%.", (object) num, (object) 5);
            break;
          case UpgradeCategory.Plants:
            str = string.Format("Unlock {0} more Plant Packs to increase decoration score by {1}%.", (object) num, (object) 5);
            break;
          case UpgradeCategory.Lamppost:
            str = "PLACEHOLDER";
            break;
          case UpgradeCategory.Signboard:
            str = string.Format("Unlock {0} more Signboard Packs to increase effective range of Signs by {1}%.", (object) num, (object) 5);
            break;
          case UpgradeCategory.Lake:
            str = string.Format("Unlock {0} more Lake Packs to increase Visitor Happiness and Virality by {1}%.", (object) num, (object) 5);
            break;
          case UpgradeCategory.Floors:
            str = string.Format("Unlock {0} more Floor Packs to decrease energy loss of customers walking through park by {1}%.", (object) num, (object) 5);
            break;
          case UpgradeCategory.Rocks:
            str = string.Format("Unlock {0} more Rock Packs to increase PLACEHOLDER by {1}%.", (object) num, (object) 5);
            break;
          case UpgradeCategory.Speakers:
            str = string.Format("Unlock {0} more Speakers Packs to increase PLACEHOLDER by {1}%.", (object) num, (object) 5);
            break;
          case UpgradeCategory.Toilet:
            str = string.Format("Unlock {0} more Toilet Packs to increase Toilet Capacity by {1}%.", (object) num, (object) 5);
            break;
          case UpgradeCategory.Decoration:
            str = string.Format("Unlock {0} more Deco Packs to get {1}% money back when demolishing.", (object) num, (object) 5);
            break;
          case UpgradeCategory.FoodShops:
            str = string.Format("Unlock {0} more Food Packs to increase the number of people who can be inside a Food shop by {1}.", (object) num, (object) 1);
            break;
          case UpgradeCategory.Attractions:
            str = string.Format("Unlock {0} more Attraction Packs for attractions to have increased durability and require less maintanance.", (object) num);
            break;
          case UpgradeCategory.Factories:
            str = string.Format("Unlock {0} more Factory Packs to increase output from killing animals from factories by {1}%.", (object) num, (object) 5);
            break;
          case UpgradeCategory.Insects:
            str = string.Format("Unlock {0} more Insect Packs to increase visitor education points by {1}%.", (object) num, (object) 5);
            break;
          case UpgradeCategory.Pens:
            str = string.Format("Unlock {0} more Pen Packs to have self-filtering water.", (object) num);
            break;
          case UpgradeCategory.AnimalShelter:
            str = string.Format("Unlock {0} more Animal Shelter Packs for shelters to give animals {1}% more privacy.", (object) num, (object) 5);
            break;
          case UpgradeCategory.AnimalEnrichment:
            str = string.Format("Unlock {0} more Animal Enrichment Packs to increase effectiveness per enrichment item by {1}%.", (object) num, (object) 5);
            break;
          case UpgradeCategory.DrinksShop:
            str = string.Format("Unlock {0} more Drink Packs to increase the number of people who can be inside a Drink shop {1}.", (object) num, (object) 2);
            break;
          case UpgradeCategory.Fountains:
            str = string.Format("Unlock {0} more Fountain Packs to increase Water Pump Range by {1} tiles.", (object) num, (object) 2);
            break;
          case UpgradeCategory.AnimalNursery:
            str = string.Format("Unlock {0} more Nursery Packs to add a breeding room.", (object) num);
            break;
          case UpgradeCategory.Bins:
            str = string.Format("Unlock {0} more Bin Packs to increase Trash Capacity by {1}%", (object) num, (object) 5);
            break;
          case UpgradeCategory.Employee:
            str = string.Format("Unlock {0} more Employee Packs to increase Employee Walk Speed by {1}%", (object) num, (object) 5);
            break;
        }
      }
      return str;
    }

    public static ResearchUpgradeInfoSet GetUpgradeTiers(
      UpgradeCategory upgradeCategory)
    {
      if (RGrid_Data.researchinfosets == null)
        RGrid_Data.researchinfosets = new ResearchUpgradeInfoSet[41];
      if (RGrid_Data.researchinfosets[(int) upgradeCategory] == null)
      {
        if (Z_DebugFlags.IsBetaVersion)
        {
          switch (upgradeCategory)
          {
            case UpgradeCategory.Trees:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(5, ResearchUpgradeType.DecoValueUp);
              break;
            case UpgradeCategory.Floors:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(5, ResearchUpgradeType.EnergyUsePerStep);
              break;
            case UpgradeCategory.AnimalEnrichment:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(2, ResearchUpgradeType.PsychologyStudies);
              break;
            case UpgradeCategory.GiftShops:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(3, ResearchUpgradeType.CreditCardVendingMachines);
              break;
            case UpgradeCategory.Research:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(1, ResearchUpgradeType.ExtraResearchers);
              break;
          }
        }
        else
        {
          switch (upgradeCategory)
          {
            case UpgradeCategory.Benches:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(3);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(5);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(6);
              break;
            case UpgradeCategory.Shelters:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(2);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(3);
              break;
            case UpgradeCategory.Trees:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(3);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(5);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(7);
              break;
            case UpgradeCategory.Plants:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(4);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(8);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(12);
              break;
            case UpgradeCategory.Lamppost:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(0);
              break;
            case UpgradeCategory.Signboard:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(4);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(7);
              break;
            case UpgradeCategory.Lake:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(5);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(10);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(15);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(21);
              break;
            case UpgradeCategory.Floors:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(3);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(6);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(9);
              break;
            case UpgradeCategory.Rocks:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(0);
              break;
            case UpgradeCategory.Speakers:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(0);
              break;
            case UpgradeCategory.Toilet:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(2);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(4);
              break;
            case UpgradeCategory.Decoration:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(3);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(7);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(11);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(15);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(19);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(22);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(26);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(29);
              break;
            case UpgradeCategory.FoodShops:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(2);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(4);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(6);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(8);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(10);
              break;
            case UpgradeCategory.Attractions:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(3);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(6);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(9);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(11);
              break;
            case UpgradeCategory.Farm:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(4);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(7);
              break;
            case UpgradeCategory.Factories:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(2);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(4);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(6);
              break;
            case UpgradeCategory.Insects:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(3);
              break;
            case UpgradeCategory.Pens:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(3);
              break;
            case UpgradeCategory.AnimalShelter:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(2);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(4);
              break;
            case UpgradeCategory.AnimalEnrichment:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(5);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(9);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(14);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(18);
              break;
            case UpgradeCategory.DrinksShop:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(2);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(4);
              break;
            case UpgradeCategory.GiftShops:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(2);
              break;
            case UpgradeCategory.Fountains:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(3);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(5);
              break;
            case UpgradeCategory.Advertising:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(5);
              break;
            case UpgradeCategory.TicketingBooth:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(0);
              break;
            case UpgradeCategory.ZooBus:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(0);
              break;
            case UpgradeCategory.Security:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(5);
              break;
            case UpgradeCategory.Vets:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(0);
              break;
            case UpgradeCategory.AnimalNursery:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(0);
              break;
            case UpgradeCategory.Transport:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(0);
              break;
            case UpgradeCategory.Bins:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(2);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(3);
              RGrid_Data.researchinfosets[(int) upgradeCategory].AddNew(4);
              break;
            case UpgradeCategory.Animals:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(0);
              break;
            case UpgradeCategory.Research:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(0);
              break;
            case UpgradeCategory.StoreRoom:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(0);
              break;
            case UpgradeCategory.Employee:
              RGrid_Data.researchinfosets[(int) upgradeCategory] = new ResearchUpgradeInfoSet(6);
              break;
          }
        }
      }
      return RGrid_Data.researchinfosets[(int) upgradeCategory];
    }
  }
}
