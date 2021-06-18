// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components.BuildingAnimator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.Tile_Data;

namespace TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components
{
  internal class BuildingAnimator : RenderComponent
  {
    private int Frame;
    private float FrameRate = 0.2f;
    private float FrameTimer;
    private int TotalFrames;
    private float MinHoldOnFrameZero;
    private float MaxHoldOnFrameZero;
    private float holder;

    public BuildingAnimator(TileRenderer parent, TileInfo tinfo = null)
      : base(parent, RenderComponentType.BuildingAnimator)
    {
      bool flag = true;
      this.TotalFrames = 8;
      this.Frame = 0;
      if (tinfo != null)
      {
        this.TotalFrames = tinfo.BaseFrame;
        this.FrameRate = tinfo.BaseFrameRate;
      }
      else
      {
        switch (parent.tiletypeonconstruct)
        {
          case TILETYPE.GoatIAP:
            this.TotalFrames = 8;
            this.MaxHoldOnFrameZero = 3f;
            flag = false;
            break;
          case TILETYPE.TrashCompactorIAP:
            this.TotalFrames = 23;
            break;
          case TILETYPE.FlowerIAP:
            this.TotalFrames = 7;
            break;
          case TILETYPE.SmallGiftShop:
            this.TotalFrames = 1;
            break;
          case TILETYPE.LionHotDogShop:
            this.TotalFrames = 1;
            break;
          case TILETYPE.ElephantGiftShop:
            this.TotalFrames = 1;
            break;
          case TILETYPE.IceCreamTruck:
            this.TotalFrames = 1;
            break;
          case TILETYPE.BigIceCreamShop:
            this.TotalFrames = 1;
            break;
          case TILETYPE.CoconutShop:
            this.TotalFrames = 1;
            break;
          case TILETYPE.DrinksVendingMachine:
            this.TotalFrames = 1;
            break;
          case TILETYPE.SnacksVendingMachine:
            this.TotalFrames = 1;
            break;
          case TILETYPE.SignboardFront:
            this.TotalFrames = 1;
            break;
          case TILETYPE.Lamppost:
            this.TotalFrames = 1;
            break;
          case TILETYPE.GreenDustbin:
            this.TotalFrames = 1;
            break;
          case TILETYPE.WhiteDustbin:
            this.TotalFrames = 1;
            break;
          case TILETYPE.PlantPot:
            this.TotalFrames = 1;
            break;
          case TILETYPE.RedFlag:
            this.TotalFrames = 1;
            break;
          case TILETYPE.RoundFountain:
            this.TotalFrames = 1;
            break;
          case TILETYPE.PandaBurgerShop:
            this.TotalFrames = 1;
            break;
          case TILETYPE.BalloonShop:
            this.TotalFrames = 1;
            break;
          case TILETYPE.FlowerPatch:
            this.TotalFrames = 1;
            break;
          case TILETYPE.DangerSign:
            this.TotalFrames = 1;
            break;
          case TILETYPE.SmallDesertRockDeco:
            this.TotalFrames = 1;
            break;
          case TILETYPE.DesertRockDeco:
            this.TotalFrames = 1;
            break;
          case TILETYPE.DesertCactusDeco:
            this.TotalFrames = 1;
            break;
          case TILETYPE.KangarooPizzaShop:
            this.TotalFrames = 1;
            break;
          case TILETYPE.CottonCandyShop:
            this.TotalFrames = 1;
            break;
          case TILETYPE.SlushieShop:
            this.TotalFrames = 1;
            break;
          case TILETYPE.ChurroShop:
            this.TotalFrames = 1;
            break;
          case TILETYPE.WoodenToilet:
            this.TotalFrames = 1;
            break;
          case TILETYPE.SnakeHuggingBooth:
            this.TotalFrames = 1;
            break;
          case TILETYPE.TreeExhibit:
            this.TotalFrames = 1;
            break;
          case TILETYPE.InfoBooth:
            this.TotalFrames = 1;
            break;
          case TILETYPE.BusStop:
            this.TotalFrames = 1;
            break;
          case TILETYPE.BarSignboard:
            this.TotalFrames = 1;
            break;
          case TILETYPE.Umbrella:
            this.TotalFrames = 1;
            break;
          case TILETYPE.BigTree:
            this.TotalFrames = 1;
            break;
          case TILETYPE.TikiShelter:
            this.TotalFrames = 1;
            break;
          case TILETYPE.ZooMap:
            this.TotalFrames = 1;
            break;
          case TILETYPE.BrownBench:
            this.TotalFrames = 1;
            break;
          case TILETYPE.WhiteBench:
            this.TotalFrames = 1;
            break;
          case TILETYPE.NoSwimmingSign:
            this.TotalFrames = 1;
            break;
          case TILETYPE.PottedPlant:
            this.TotalFrames = 1;
            break;
          case TILETYPE.ThickSignboard:
            this.TotalFrames = 1;
            break;
          case TILETYPE.ElephantFountain:
            this.TotalFrames = 1;
            break;
          case TILETYPE.FloorLight:
            this.TotalFrames = 1;
            break;
          case TILETYPE.RustyKegShop:
            this.TotalFrames = 1;
            break;
          case TILETYPE.PopcornWeaselShop:
            this.TotalFrames = 1;
            break;
          case TILETYPE.Nursery:
            this.TotalFrames = 1;
            break;
          case TILETYPE.QuarantineOffice:
            this.TotalFrames = 1;
            break;
          case TILETYPE.VetOffice:
            this.TotalFrames = 1;
            break;
          case TILETYPE.ElephantMarbleFountain:
            this.TotalFrames = 1;
            break;
          case TILETYPE.FlamingoHedge:
            this.TotalFrames = 1;
            break;
          case TILETYPE.GiraffeHedge:
            this.TotalFrames = 1;
            break;
          case TILETYPE.ElephantHedge:
            this.TotalFrames = 1;
            break;
          case TILETYPE.MonkeyStatue:
            this.TotalFrames = 1;
            break;
          case TILETYPE.GiantSunFlower:
            this.TotalFrames = 1;
            break;
          case TILETYPE.ZooStandee:
            this.TotalFrames = 1;
            break;
          case TILETYPE.PenguinStandee:
            this.TotalFrames = 1;
            break;
          case TILETYPE.BearStandee:
            this.TotalFrames = 1;
            break;
          case TILETYPE.SnakeBench:
            this.TotalFrames = 1;
            break;
          case TILETYPE.ZooTree:
            this.TotalFrames = 1;
            break;
          case TILETYPE.SnakeSignpost:
            this.TotalFrames = 1;
            break;
          case TILETYPE.UmbrellaBench:
            this.TotalFrames = 1;
            break;
          case TILETYPE.MonkeyBanner:
            this.TotalFrames = 1;
            break;
          case TILETYPE.SakuraTree:
            this.TotalFrames = 1;
            break;
          case TILETYPE.LionPlayground:
            this.TotalFrames = 1;
            break;
          case TILETYPE.SpringChicken:
            this.TotalFrames = 1;
            break;
          case TILETYPE.SpringHorse:
            this.TotalFrames = 1;
            break;
          case TILETYPE.RockElephant:
            this.TotalFrames = 1;
            break;
          case TILETYPE.YellowBush:
            this.TotalFrames = 1;
            break;
          case TILETYPE.RedFlower:
            this.TotalFrames = 1;
            break;
          case TILETYPE.WhiteFlower:
            this.TotalFrames = 1;
            break;
          case TILETYPE.PurpleFlower:
            this.TotalFrames = 1;
            break;
          case TILETYPE.PurpleFlowerPatch:
            this.TotalFrames = 1;
            break;
          case TILETYPE.PenguinTrashbin:
            this.TotalFrames = 1;
            break;
          case TILETYPE.SealStandee:
            this.TotalFrames = 1;
            break;
          case TILETYPE.RedShelter:
            this.TotalFrames = 1;
            break;
          case TILETYPE.CrocCrossingSign:
            this.TotalFrames = 1;
            break;
          case TILETYPE.MenuSign:
            this.TotalFrames = 1;
            break;
          case TILETYPE.SmallBarTable:
            this.TotalFrames = 1;
            break;
          case TILETYPE.NoPhotoSign:
            this.TotalFrames = 1;
            break;
          case TILETYPE.OwlClock:
            this.TotalFrames = 1;
            break;
          case TILETYPE.HeShee:
            this.TotalFrames = 1;
            break;
          case TILETYPE.Totem:
            this.TotalFrames = 1;
            break;
          case TILETYPE.AztecSign:
            this.TotalFrames = 1;
            break;
          case TILETYPE.AztecTorch:
            this.TotalFrames = 1;
            break;
          case TILETYPE.AztecPlant:
            this.TotalFrames = 1;
            break;
          case TILETYPE.WoodenTotem:
            this.TotalFrames = 1;
            break;
          case TILETYPE.StoreRoom:
            this.TotalFrames = 1;
            break;
          case TILETYPE.ArchitectOffice:
            this.TotalFrames = 1;
            break;
          case TILETYPE.LargeArchitectOffice:
            this.TotalFrames = 1;
            break;
          case TILETYPE.GlueFactory:
            this.TotalFrames = 1;
            break;
          case TILETYPE.BuffaloWingFactory:
            this.TotalFrames = 1;
            break;
          case TILETYPE.BaconFactory:
            this.TotalFrames = 1;
            break;
          case TILETYPE.Barn:
            this.TotalFrames = 1;
            break;
          case TILETYPE.EmptySoilPatch:
            this.TotalFrames = 1;
            break;
          case TILETYPE.Wheat_SmallGrown:
            this.TotalFrames = 1;
            break;
          case TILETYPE.Wheat_HalfGrown:
            this.TotalFrames = 1;
            break;
          case TILETYPE.Wheat_FullGrown:
            this.TotalFrames = 1;
            break;
          case TILETYPE.Silo:
            this.TotalFrames = 1;
            break;
          case TILETYPE.PalmTree:
            this.TotalFrames = 1;
            break;
          case TILETYPE.GreenTree:
            this.TotalFrames = 1;
            break;
          case TILETYPE.LongGrass:
            this.TotalFrames = 1;
            break;
          case TILETYPE.IrisPlantPot:
            this.TotalFrames = 1;
            break;
          case TILETYPE.YellowPlantPot:
            this.TotalFrames = 1;
            break;
          case TILETYPE.BonsaiPlantPot:
            this.TotalFrames = 1;
            break;
          case TILETYPE.Ferns:
            this.TotalFrames = 1;
            break;
          case TILETYPE.SmallRock:
            this.TotalFrames = 1;
            break;
          case TILETYPE.MediumRock:
            this.TotalFrames = 1;
            break;
          case TILETYPE.LargeMossyRock:
            this.TotalFrames = 1;
            break;
          case TILETYPE.TigerPhoto:
            this.TotalFrames = 1;
            break;
          case TILETYPE.NoticeBoard:
            this.TotalFrames = 1;
            break;
          case TILETYPE.BeetleGlassExhibit:
            this.TotalFrames = 1;
            break;
          case TILETYPE.ButterflyGlassExhibit:
            this.TotalFrames = 1;
            break;
          case TILETYPE.GlassExhibit:
            this.TotalFrames = 1;
            break;
          case TILETYPE.Volume_Water:
            this.TotalFrames = 4;
            break;
        }
      }
      if (flag)
        this.Frame = TinyZoo.Game1.Rnd.Next(0, this.TotalFrames);
      this.FrameTimer = MathStuff.getRandomFloat(0.0f, this.FrameRate);
      this.SetFrame(parent);
      if ((double) this.MaxHoldOnFrameZero <= 0.0)
        return;
      this.holder = MathStuff.getRandomFloat(this.MinHoldOnFrameZero, this.MaxHoldOnFrameZero);
      this.holder *= 0.5f;
    }

    private void SetFrame(TileRenderer parent)
    {
      parent.DrawRect.X = this.OriginalRectangle.X + (this.OriginalRectangle.Width + 1) * this.Frame;
      parent.DrawRect.Y = this.OriginalRectangle.Y;
      ++this.Frame;
      if (this.Frame > this.TotalFrames - 1)
      {
        this.Frame = 0;
        this.holder = 0.0f;
        if ((double) this.MaxHoldOnFrameZero > 0.0)
          this.holder = MathStuff.getRandomFloat(this.MinHoldOnFrameZero, this.MaxHoldOnFrameZero);
      }
      if (this.TotalFrames <= 15)
        return;
      if (this.Frame > 14)
      {
        parent.DrawRect.Y = this.OriginalRectangle.Y + (this.OriginalRectangle.Height + 1);
        parent.DrawRect.X = this.OriginalRectangle.X + (this.OriginalRectangle.Width + 1) * (this.Frame - 15);
      }
      if (this.Frame != 0)
        return;
      parent.DrawRect.X = this.OriginalRectangle.X + (this.OriginalRectangle.Width + 1) * this.Frame;
      parent.DrawRect.Y = this.OriginalRectangle.Y;
    }

    public override bool UpdateRenderComponent(TileRenderer parent, float DeltaTime)
    {
      if (this.Frame == 0 && (double) this.holder > 0.0)
      {
        this.holder -= DeltaTime;
        return false;
      }
      this.FrameTimer += DeltaTime;
      if ((double) this.FrameTimer > (double) this.FrameRate)
      {
        this.FrameTimer -= this.FrameRate;
        if ((double) this.FrameTimer > (double) this.FrameRate)
          this.FrameTimer = 0.0f;
        this.SetFrame(parent);
      }
      return false;
    }
  }
}
