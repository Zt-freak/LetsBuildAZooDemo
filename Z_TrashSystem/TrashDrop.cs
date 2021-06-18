// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TrashSystem.TrashDrop
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Z_Data;
using TinyZoo.Z_DayNight;
using TinyZoo.Z_OverWorld._OverWorldEnv.Customers.PeopleAttachments;

namespace TinyZoo.Z_TrashSystem
{
  internal class TrashDrop : AnimatedGameObject
  {
    public int UID;
    public Vector2Int TileLocation;
    public TrashType trashtype;
    public bool ISNew;
    public bool ReadyForPickUp;
    public AnimalType animal;
    public int PrisonUID;

    public TrashDrop(
      Vector2Int Location,
      TrashType _trashtype,
      AnimalType _animal = AnimalType.None,
      int _PrisonUID = -1)
    {
      this.UID = Z_GameFlags.TrashUID;
      ++Z_GameFlags.TrashUID;
      this.ISNew = true;
      this.trashtype = _trashtype;
      this.animal = _animal;
      this.PrisonUID = _PrisonUID;
      this.TileLocation = new Vector2Int(Location);
      this.Create();
    }

    internal static int SortTrash(TrashDrop a, TrashDrop b)
    {
      if ((double) a.vLocation.Y < (double) b.vLocation.Y)
        return -1;
      if ((double) a.vLocation.Y > (double) b.vLocation.Y)
        return 1;
      if (a.UID > b.UID)
        return -1;
      return a.UID < b.UID ? 1 : 0;
    }

    private void Create()
    {
      this.IsAnimating = false;
      switch (this.trashtype)
      {
        case TrashType.WhiteWrapper:
          this.DrawRect = new Rectangle(28, 488, 6, 7);
          break;
        case TrashType.HotDogWrapper:
          this.DrawRect = new Rectangle(0, 488, 7, 7);
          break;
        case TrashType.RedCan:
          this.DrawRect = new Rectangle(0, 496, 7, 7);
          break;
        case TrashType.YellowWrapper:
          this.DrawRect = new Rectangle(11, 481, 8, 6);
          break;
        case TrashType.PinkWhiteStripedWrapper:
          this.DrawRect = new Rectangle(8, 496, 8, 7);
          break;
        case TrashType.IceCreamSpill:
          this.DrawRect = new Rectangle(20, 481, 10, 6);
          break;
        case TrashType.SnowConeSpill:
          this.DrawRect = new Rectangle(8, 488, 11, 7);
          break;
        case TrashType.PopsicleSpill:
          this.DrawRect = new Rectangle(50, 494, 9, 9);
          break;
        case TrashType.SundaeSpill:
          this.DrawRect = new Rectangle(17, 496, 10, 7);
          break;
        case TrashType.BananaSpiltSpill:
          this.DrawRect = new Rectangle(41, 485, 7, 8);
          break;
        case TrashType.GlassJar:
          this.DrawRect = new Rectangle(20, 488, 7, 7);
          break;
        case TrashType.RedStraw:
          this.DrawRect = new Rectangle(62, 477, 8, 5);
          break;
        case TrashType.CocktailUmbrella:
          this.DrawRect = new Rectangle(35, 486, 5, 7);
          break;
        case TrashType.Lemon:
          this.DrawRect = new Rectangle(31, 481, 5, 4);
          break;
        case TrashType.PaperPlate:
          this.DrawRect = new Rectangle(49, 485, 7, 8);
          break;
        case TrashType.CottonCandySpill:
          this.DrawRect = new Rectangle(28, 496, 9, 7);
          break;
        case TrashType.SlushieSpill:
          this.DrawRect = new Rectangle(39, 494, 10, 9);
          break;
        case TrashType.CoffeeCup:
          this.DrawRect = new Rectangle(473, 549, 6, 5);
          break;
        case TrashType.PopcornWrapper:
          this.DrawRect = new Rectangle(375, 306, 9, 6);
          break;
        case TrashType.FriesPacket:
          this.DrawRect = new Rectangle(532, 160, 9, 6);
          break;
        case TrashType.BeerMug:
          this.DrawRect = new Rectangle(105, 642, 9, 8);
          break;
        case TrashType.PretzelWrapper:
          this.DrawRect = new Rectangle(723, 72, 7, 6);
          break;
        case TrashType.TacoWrapper:
          this.DrawRect = new Rectangle(297, 401, 7, 5);
          break;
        case TrashType.ChocolateWrapper:
          this.DrawRect = new Rectangle(432, 313, 5, 5);
          break;
        case TrashType.Vomit:
          if (TinyZoo.Game1.Rnd.Next(0, 2) == 0)
            this.DrawRect = new Rectangle(524, 1025, 20, 13);
          else
            this.DrawRect = new Rectangle(629, 1025, 21, 13);
          this.SetUpSimpleAnimation(5, 0.1f);
          this.PlayOnlyOnce = true;
          break;
        case TrashType.AnimalPoop:
          this.DrawRect = PooData.GetAnimalToPoopRectangle(this.animal);
          break;
        default:
          throw new Exception("sef");
      }
      this.bActive = true;
      this.SetDrawOriginToCentre();
      this.SetPostion();
    }

    private void CreateVomit()
    {
      if (this.trashtype != TrashType.Vomit)
        throw new Exception("sef");
      this.DrawRect = new Rectangle(1209, 45, 15, 13);
      this.bActive = true;
      this.SetDrawOriginToCentre();
      this.SetPostion();
    }

    private void SetPostion()
    {
      this.vLocation = TileMath.GetTileToWorldSpace(this.TileLocation);
      this.vLocation = this.vLocation + new Vector2((float) TinyZoo.Game1.Rnd.Next(-8, 8), (float) TinyZoo.Game1.Rnd.Next(-8, 8)) * Sengine.ScreenRatioUpwardsMultiplier;
    }

    public void UpdateBlocked(TrashAndStuff trashandstuff)
    {
      if (!GameFlags.CollisionChanged || !Z_GameFlags.pathfinder.GetIsBlocked(this.TileLocation.X, this.TileLocation.Y) && !Z_GameFlags.IsTheBlockedByNewPenFloor(this.TileLocation.X, this.TileLocation.Y))
        return;
      TrashEntry trashentryfromsave = trashandstuff.GetThis(this);
      this.TileLocation = WalkingPerson.GetRandomUnblockedLocaton();
      if (this.TileLocation == null)
      {
        this.bActive = false;
        trashandstuff.RemoveTrashOnNoPositions(trashentryfromsave);
      }
      else
      {
        this.SetPostion();
        this.SetAlpha(false, 0.2f, 0.0f, 1f);
        trashentryfromsave.TileLocation = new Vector2Int(this.TileLocation);
      }
    }

    public void UpdateTrashDrop(float DeltaTime)
    {
      if (!this.bActive)
        return;
      this.UpdateColours(DeltaTime);
      if (!this.IsAnimating)
        return;
      this.UpdateAnimation(DeltaTime);
    }

    public void DrawTrashDrop()
    {
      this.bActive = true;
      if (!this.bActive)
        return;
      this.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
      this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet);
    }

    internal static TrashType GetPersonAttachementTypeToTrash(
      PersonAttachementType attachmenttype)
    {
      switch (attachmenttype)
      {
        case PersonAttachementType.HotDog:
          return TrashType.HotDogWrapper;
        case PersonAttachementType.Crisps:
          return TrashType.YellowWrapper;
        case PersonAttachementType.Cola:
          return TrashType.RedCan;
        case PersonAttachementType.Churros:
          return TrashType.HotDogWrapper;
        case PersonAttachementType.SingleScoop:
          return TrashType.IceCreamSpill;
        case PersonAttachementType.SnowCone:
          return TrashType.SnowConeSpill;
        case PersonAttachementType.Popsicle:
          return TrashType.PopsicleSpill;
        case PersonAttachementType.Sundae:
          return TrashType.SundaeSpill;
        case PersonAttachementType.BananaSplit:
          return TrashType.BananaSpiltSpill;
        case PersonAttachementType.Parfait:
          return TrashType.GlassJar;
        case PersonAttachementType.Coconut:
          return TrashType.RedStraw;
        case PersonAttachementType.FruitPunch:
          return TrashType.CocktailUmbrella;
        case PersonAttachementType.Mocktail:
          return TrashType.Lemon;
        case PersonAttachementType.Burger:
          return TrashType.WhiteWrapper;
        case PersonAttachementType.CottonCandy:
          return TrashType.CottonCandySpill;
        case PersonAttachementType.Slushie:
          return TrashType.SlushieSpill;
        case PersonAttachementType.Coffee:
          return TrashType.CoffeeCup;
        case PersonAttachementType.Popcorn:
          return TrashType.PopcornWrapper;
        case PersonAttachementType.Fries:
          return TrashType.FriesPacket;
        case PersonAttachementType.CraftBeer:
          return TrashType.BeerMug;
        case PersonAttachementType.Pretzel:
          return TrashType.PretzelWrapper;
        case PersonAttachementType.Taco:
          return TrashType.TacoWrapper;
        case PersonAttachementType.Chocolate:
          return TrashType.ChocolateWrapper;
        default:
          return TrashType.WhiteWrapper;
      }
    }
  }
}
