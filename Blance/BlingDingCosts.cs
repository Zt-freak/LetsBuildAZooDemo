// Decompiled with JetBrains decompiler
// Type: TinyZoo.Blance.BlingDingCosts
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Tile_Data;

namespace TinyZoo.Blance
{
  internal class BlingDingCosts
  {
    private CstBase[] cstspace;

    public BlingDingCosts()
    {
      this.cstspace = new CstBase[12];
      for (int index = 0; index < this.cstspace.Length; ++index)
        this.cstspace[index] = new CstBase();
      this.cstspace[0].SetUpBell(500, 2f, 0.89f);
      this.cstspace[1].SetUpBell(150, 2.5f, 0.885f);
      this.cstspace[2].SetUpBell(1500, 2.5f, 0.863f);
      this.cstspace[3].SetUpBell(2800, 3f, 0.84f);
      this.cstspace[4].SetUpBell(4000, 4f, 0.81f);
      this.cstspace[5].SetUpBell(8000, 4f, 0.8f);
      this.cstspace[6].SetUpBell(15000, 4f, 0.788f);
      this.cstspace[7].SetUpBell(25000, 4f, 0.8f);
      this.cstspace[8].SetUp(150, 1.773f);
      this.cstspace[9].SetUpStorage();
      this.cstspace[10].SetUp(999999, 1f);
      this.cstspace[11].SetUp(1000, 3f);
    }

    public int GetCost(TILETYPE tile, int Duplicates)
    {
      int num1 = -1;
      if (GameFlags.IsConsoleVersion)
      {
        switch (tile)
        {
          case TILETYPE.KitchenZone:
            num1 = 1300000;
            break;
          case TILETYPE.Research_PrisonPlanet:
            num1 = 1500000;
            break;
          case TILETYPE.Medical:
            num1 = 1500000;
            break;
          case TILETYPE.Security:
            num1 = 1000000;
            break;
          case TILETYPE.Janitor:
            num1 = 1000000;
            break;
          case TILETYPE.Farm:
            num1 = 1000000;
            break;
          case TILETYPE.Water:
            num1 = 1500000;
            break;
        }
      }
      int num2;
      if (tile <= TILETYPE.LongWoodenBench)
      {
        if (tile <= TILETYPE.MediumRock)
        {
          if (tile <= TILETYPE.ThickSignboard)
          {
            switch (tile - 3)
            {
              case TILETYPE.None:
              case TILETYPE.GreenWallCorner:
              case TILETYPE.GreenWallSide:
              case TILETYPE.Floor_GreenGrass:
                goto label_104;
              case TILETYPE.Floor_Dirt:
              case TILETYPE.Floor_RedCircles:
              case TILETYPE.Floor_GreyBricks:
              case TILETYPE.Farm:
              case TILETYPE.Water:
              case TILETYPE.LifeSupport:
              case TILETYPE.Moon:
              case TILETYPE.PinkMoonPlant:
              case TILETYPE.Grasslands_WallCorner:
              case TILETYPE.Grasslands_WallSide:
              case TILETYPE.Grasslands_Floor:
              case TILETYPE.PrisonOuterWall:
              case TILETYPE.PrisonOuterWallLight:
              case TILETYPE.PrisonWallOuterCorner:
              case TILETYPE.PrisonWallInnerCorner:
              case TILETYPE.EMPTY_DIRT_WALKABLE_TILE:
              case TILETYPE.Zoo_PathDoubleSided:
              case TILETYPE.GraveYard_WallCorner:
                goto label_112;
              case TILETYPE.DefaultFence_WallCorner:
                num2 = this.cstspace[0].GetValue(Duplicates);
                goto label_113;
              case TILETYPE.DefaultFence_WallSide:
                num2 = this.cstspace[4].GetValue(Duplicates);
                goto label_113;
              case TILETYPE.Road:
                num2 = this.cstspace[8].GetValue(Duplicates);
                goto label_113;
              case TILETYPE.PowerStation:
                num2 = this.cstspace[6].GetValue(Duplicates);
                goto label_113;
              case TILETYPE.KitchenZone:
                num2 = this.cstspace[7].GetValue(Duplicates);
                goto label_113;
              case TILETYPE.Research_PrisonPlanet:
                num2 = this.cstspace[9].GetValue(Duplicates);
                goto label_113;
              case TILETYPE.Medical:
                num2 = this.cstspace[10].GetValue(Duplicates);
                goto label_113;
              case TILETYPE.Security:
                num2 = this.cstspace[5].GetValue(Duplicates);
                goto label_113;
              case TILETYPE.Bank:
                num2 = this.cstspace[3].GetValue(Duplicates);
                goto label_113;
              case TILETYPE.Solitary:
                num2 = this.cstspace[2].GetValue(Duplicates);
                goto label_113;
              case TILETYPE.Janitor:
                num2 = this.cstspace[1].GetValue(Duplicates);
                goto label_113;
              case TILETYPE.GraveYard_WallSide:
                num2 = this.cstspace[11].GetValue(Duplicates);
                goto label_113;
              case TILETYPE.GraveYard_Floor:
                num2 = 5;
                goto label_113;
              case TILETYPE.GraveYard_FloorGraveStone:
                num2 = 100;
                goto label_113;
              case TILETYPE.HoldingCell:
                num2 = 250;
                goto label_113;
              case TILETYPE.GrassEnclosure:
                num2 = 500;
                goto label_113;
              case TILETYPE.DesertEnclosure:
                num2 = 1000;
                goto label_113;
              case TILETYPE.MountainEnclosure:
                num2 = 2500;
                goto label_113;
              case TILETYPE.ArcticEnclosure:
                num2 = 5000;
                goto label_113;
              case TILETYPE.TropicalEnclosure:
                num2 = 100;
                goto label_113;
              case TILETYPE.ForestEnclosure:
                num2 = 25000;
                goto label_113;
              case TILETYPE.SavannahEnclosure:
                num2 = 50000;
                goto label_113;
              case TILETYPE.FieldPicketFenceEnclosure:
                num2 = 0;
                goto label_113;
              case TILETYPE.CorruptedGrassEnclosure:
                num2 = 0;
                goto label_113;
              default:
                switch (tile - 77)
                {
                  case TILETYPE.None:
                    num2 = 1000;
                    goto label_113;
                  case TILETYPE.GreenWallCorner:
                    num2 = 1000;
                    goto label_113;
                  case TILETYPE.GreenWallSide:
                    num2 = 1000;
                    goto label_113;
                  case TILETYPE.Floor_GreenGrass:
                    num2 = 1000;
                    goto label_113;
                  case TILETYPE.Floor_Dirt:
                    num2 = 1000;
                    goto label_113;
                  case TILETYPE.Floor_RedCircles:
                    num2 = 1000;
                    goto label_113;
                  default:
                    switch (tile - 98)
                    {
                      case TILETYPE.None:
                        goto label_76;
                      case TILETYPE.GreenWallCorner:
                      case TILETYPE.Janitor:
                      case TILETYPE.Grasslands_Floor:
                        num2 = 400;
                        goto label_113;
                      case TILETYPE.Floor_GreyBricks:
                      case TILETYPE.DefaultFence_WallCorner:
                        num2 = 100;
                        goto label_113;
                      case TILETYPE.Road:
                        num2 = 15;
                        goto label_113;
                      case TILETYPE.PowerStation:
                      case TILETYPE.KitchenZone:
                        break;
                      case TILETYPE.Research_PrisonPlanet:
                        goto label_91;
                      case TILETYPE.Medical:
                        num2 = 5;
                        goto label_113;
                      case TILETYPE.Security:
                        num2 = 150;
                        goto label_113;
                      case TILETYPE.Farm:
                        num2 = 5;
                        goto label_113;
                      case TILETYPE.LifeSupport:
                        goto label_90;
                      case TILETYPE.PrisonOuterWallLight:
                        goto label_80;
                      case TILETYPE.GraveYard_WallSide:
                      case TILETYPE.GraveYard_FloorGraveStone:
                        goto label_83;
                      case TILETYPE.HoldingCell:
                        num2 = 40;
                        goto label_113;
                      case TILETYPE.GrassEnclosure:
                      case TILETYPE.DesertEnclosure:
                        num2 = 20;
                        goto label_113;
                      case TILETYPE.ArcticEnclosure:
                        goto label_94;
                      case TILETYPE.TropicalEnclosure:
                        num2 = 15;
                        goto label_113;
                      default:
                        goto label_112;
                    }
                    break;
                }
            }
          }
          else
          {
            if (tile <= TILETYPE.OwlClock)
            {
              switch (tile - 160)
              {
                case TILETYPE.None:
                  num2 = 50;
                  goto label_113;
                case TILETYPE.GreenWallCorner:
                case TILETYPE.Floor_GreenGrass:
                  goto label_112;
                case TILETYPE.GreenWallSide:
label_85:
                  num2 = 30;
                  goto label_113;
                case TILETYPE.Floor_Dirt:
                  break;
                case TILETYPE.Floor_RedCircles:
                  goto label_99;
                default:
                  switch (tile - 174)
                  {
                    case TILETYPE.None:
                    case TILETYPE.GreenWallCorner:
                    case TILETYPE.GreenWallSide:
                    case TILETYPE.Floor_GreenGrass:
                      goto label_94;
                    case TILETYPE.Floor_Dirt:
                      goto label_82;
                    case TILETYPE.Floor_GreyBricks:
                      goto label_83;
                    case TILETYPE.KitchenZone:
                      goto label_85;
                    default:
                      goto label_112;
                  }
              }
            }
            else
            {
              if (tile != TILETYPE.StoreRoom)
              {
                switch (tile - 200)
                {
                  case TILETYPE.None:
                    break;
                  case TILETYPE.KitchenZone:
                  case TILETYPE.Research_PrisonPlanet:
                  case TILETYPE.Medical:
                  case TILETYPE.Security:
                  case TILETYPE.Bank:
                  case TILETYPE.Solitary:
                    goto label_104;
                  case TILETYPE.Farm:
                    goto label_93;
                  case TILETYPE.Water:
                  case TILETYPE.Grasslands_WallCorner:
                  case TILETYPE.Grasslands_WallSide:
                    goto label_90;
                  case TILETYPE.Moon:
                  case TILETYPE.PinkMoonPlant:
                    goto label_94;
                  case TILETYPE.Grasslands_Floor:
                    goto label_91;
                  default:
                    goto label_112;
                }
              }
              num2 = 100;
              goto label_113;
            }
label_93:
            num2 = 15;
            goto label_113;
          }
label_82:
          num2 = 20;
          goto label_113;
label_83:
          num2 = 80;
          goto label_113;
label_90:
          num2 = 1;
          goto label_113;
label_91:
          num2 = 2;
          goto label_113;
label_94:
          num2 = 3;
          goto label_113;
        }
        else if (tile <= TILETYPE.PeacockBush)
        {
          if (tile != TILETYPE.MiniFountain)
          {
            if (tile != TILETYPE.ElegantTallFountain)
            {
              if (tile == TILETYPE.PeacockBush)
              {
                num2 = 50;
                goto label_113;
              }
              else
                goto label_112;
            }
            else
            {
              num2 = 400;
              goto label_113;
            }
          }
          else
          {
            num2 = 200;
            goto label_113;
          }
        }
        else if (tile <= TILETYPE.WhiteClassicLampPost)
        {
          if (tile == TILETYPE.ClassicLampPost || tile == TILETYPE.WhiteClassicLampPost)
          {
            num2 = 25;
            goto label_113;
          }
          else
            goto label_112;
        }
        else if (tile == TILETYPE.GreenGardenBench || tile == TILETYPE.LongWoodenBench)
        {
          num2 = 30;
          goto label_113;
        }
        else
          goto label_112;
      }
      else
      {
        if (tile <= TILETYPE.Enrichment_BlueTrampoline)
        {
          if (tile <= TILETYPE.Volume_Grass)
          {
            switch (tile - 265)
            {
              case TILETYPE.None:
                goto label_76;
              case TILETYPE.GreenWallCorner:
              case TILETYPE.GreenWallSide:
              case TILETYPE.Floor_GreenGrass:
              case TILETYPE.Floor_Dirt:
              case TILETYPE.Floor_RedCircles:
              case TILETYPE.Security:
              case TILETYPE.Bank:
              case TILETYPE.Solitary:
              case TILETYPE.Janitor:
                goto label_112;
              case TILETYPE.Floor_GreyBricks:
              case TILETYPE.DefaultFence_WallCorner:
              case TILETYPE.DefaultFence_WallSide:
              case TILETYPE.Road:
              case TILETYPE.PowerStation:
              case TILETYPE.KitchenZone:
              case TILETYPE.Farm:
                goto label_104;
              case TILETYPE.Research_PrisonPlanet:
                break;
              case TILETYPE.Medical:
                goto label_80;
              case TILETYPE.Water:
                goto label_99;
              default:
                if (tile == TILETYPE.Volume_RedPathway || tile == TILETYPE.Volume_Grass)
                  goto label_104;
                else
                  goto label_112;
            }
          }
          else if (tile <= TILETYPE.Water_LargeLilyPads)
          {
            if (tile != TILETYPE.Volume_WoodenBoards)
            {
              switch (tile - 336)
              {
                case TILETYPE.None:
                case TILETYPE.GreenWallCorner:
                case TILETYPE.GreenWallSide:
                case TILETYPE.Floor_GreenGrass:
                  goto label_104;
                case TILETYPE.Floor_Dirt:
                  num2 = 0;
                  goto label_113;
                case TILETYPE.DefaultFence_WallCorner:
                case TILETYPE.Road:
                  num2 = 20;
                  goto label_113;
                case TILETYPE.Research_PrisonPlanet:
                  num2 = 5;
                  goto label_113;
                case TILETYPE.Medical:
                  num2 = 10;
                  goto label_113;
                default:
                  goto label_112;
              }
            }
            else
              goto label_104;
          }
          else if (tile != TILETYPE.WaterTrough_TreeTrunk)
          {
            if (tile == TILETYPE.Enrichment_BlueTrampoline)
              goto label_110;
            else
              goto label_112;
          }
          else
          {
            num2 = 50;
            goto label_113;
          }
        }
        else if (tile <= TILETYPE.Enrichment_SmallPinkBall)
        {
          if (tile != TILETYPE.Enrichment_SmallBlueBall)
          {
            if (tile != TILETYPE.WaterPumpStation)
            {
              switch (tile - 437)
              {
                case TILETYPE.None:
                  goto label_110;
                case TILETYPE.Floor_Dirt:
                case TILETYPE.Floor_RedCircles:
                case TILETYPE.Floor_GreyBricks:
                case TILETYPE.DefaultFence_WallCorner:
                  break;
                default:
                  goto label_112;
              }
            }
            else
            {
              num2 = 100;
              goto label_113;
            }
          }
          num2 = 10;
          goto label_113;
        }
        else if (tile <= TILETYPE.PineTreeDark)
        {
          switch (tile - 467)
          {
            case TILETYPE.None:
            case TILETYPE.GreenWallCorner:
            case TILETYPE.GreenWallSide:
              goto label_104;
            default:
              if (tile == TILETYPE.PineTreeDark)
                break;
              goto label_112;
          }
        }
        else
        {
          switch (tile - 514)
          {
            case TILETYPE.None:
            case TILETYPE.Floor_GreenGrass:
            case TILETYPE.Floor_Dirt:
              goto label_104;
            case TILETYPE.GreenWallCorner:
            case TILETYPE.GreenWallSide:
              goto label_112;
            default:
              if (tile == TILETYPE.DNABuilding)
              {
                num2 = 500;
                goto label_113;
              }
              else
                goto label_112;
          }
        }
        num2 = 20;
        goto label_113;
label_110:
        num2 = 40;
        goto label_113;
      }
label_76:
      num2 = 300;
      goto label_113;
label_80:
      num2 = 100;
      goto label_113;
label_99:
      num2 = 35;
      goto label_113;
label_104:
      num2 = !TileData.IsSlowFloor(tile) ? (!Z_DebugFlags.IsBetaVersion ? 5 : 1) : 0;
      goto label_113;
label_112:
      return 50;
label_113:
      return GameFlags.IsConsoleVersion && num2 > num1 && num1 > -1 ? num1 : num2;
    }
  }
}
