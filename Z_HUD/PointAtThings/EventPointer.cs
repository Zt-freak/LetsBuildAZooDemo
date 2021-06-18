// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.PointAtThings.EventPointer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Objects;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Z_Fights;

namespace TinyZoo.Z_HUD.PointAtThings
{
  internal class EventPointer : GameObject
  {
    private GameObject Icon;
    private Vector2 TargetPosition;
    public EnemyRenderer PointAtThisAnimal;
    public EnemyRenderer PointAtThisOtherAnimal;
    private GameObject Rounder;
    public SpecialEventType eventtype;
    public TutorialQuestSpecial tutorialquestspecial;
    public OffscreenPointerType offscreenPointerType;
    private Vector2 TargetPositionInWorldSpace;
    private bool IsTinyIcon;
    private Texture2D Iconsheet;
    public FightManager fightmanager;
    private bool WillUndulate;

    public EventPointer(
      SpecialEventType _eventtype,
      OffscreenPointerType _offscreenPointerType,
      TutorialQuestSpecial _tutorialquestspecial = TutorialQuestSpecial.None)
    {
      this.Iconsheet = AssetContainer.UISheet;
      this.tutorialquestspecial = _tutorialquestspecial;
      this.scale = Z_GameFlags.GetBaseScaleForUI();
      this.Icon = new GameObject();
      this.eventtype = _eventtype;
      this.offscreenPointerType = _offscreenPointerType;
      int offscreenPointerType = (int) this.offscreenPointerType;
      float Alpha = 0.8f;
      this.Icon.DrawRect = EventPointer.GetRectangleForThisPointerType(this.offscreenPointerType, out this.Iconsheet);
      switch (this.offscreenPointerType)
      {
        case OffscreenPointerType.GoToQuestBuilding:
          this.WillUndulate = true;
          break;
        case OffscreenPointerType.PointAtNoWater:
          this.Icon.SetAlpha(Alpha);
          break;
        case OffscreenPointerType.NoEnrichment:
          this.Icon.SetAlpha(Alpha);
          break;
        case OffscreenPointerType.NoWaterConnection:
          this.Icon.SetAlpha(Alpha);
          break;
      }
      this.IsTinyIcon = false;
      if (this.offscreenPointerType == OffscreenPointerType.HungryAnimals || this.offscreenPointerType == OffscreenPointerType.DeadAnimals)
        this.IsTinyIcon = true;
      this.fAlpha = 0.0f;
      this.Rounder = new GameObject();
      this.Rounder.DrawRect = new Rectangle(41, 0, 40, 40);
      this.Rounder.SetDrawOriginToCentre();
      this.Rounder.scale = this.scale;
      this.DrawRect = new Rectangle(0, 0, 40, 40);
      this.Rounder.fAlpha = 0.0f;
      this.Icon.SetDrawOriginToCentre();
      this.SetDrawOriginToCentre();
      this.Icon.scale = this.scale;
      this.bActive = true;
    }

    public void SetTargetLocation(Vector2 PositionInworldSpace) => this.TargetPositionInWorldSpace = PositionInworldSpace;

    public static Rectangle GetRectangleForThisPointerType(
      OffscreenPointerType offscreenPointerType,
      out Texture2D texture)
    {
      Rectangle rectangle;
      switch (offscreenPointerType)
      {
        case OffscreenPointerType.GereralAlertBuilding:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(334, 448, 12, 28);
          break;
        case OffscreenPointerType.GoToQuestBuilding:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(873, 508, 12, 28);
          break;
        case OffscreenPointerType.AnimalFight:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(306, 459, 26, 28);
          break;
        case OffscreenPointerType.PointAtNoWater:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(355, 420, 20, 26);
          break;
        case OffscreenPointerType.PointAtLowWater:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(355, 420, 20, 26);
          break;
        case OffscreenPointerType.HungryAnimals:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(333, 422, 21, 24);
          break;
        case OffscreenPointerType.DeadAnimals:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(283, 462, 22, 25);
          break;
        case OffscreenPointerType.NewBirths:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(259, 463, 23, 24);
          break;
        case OffscreenPointerType.NoEnrichment:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(347, 447, 28, 29);
          break;
        case OffscreenPointerType.BreedingRoomBirth:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(259, 463, 23, 24);
          break;
        case OffscreenPointerType.CripserBirth:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(238, 463, 20, 24);
          break;
        case OffscreenPointerType.BuildADrinksShop:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(215, 483, 19, 25);
          break;
        case OffscreenPointerType.BuildAnyShop:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(215, 460, 22, 22);
          break;
        case OffscreenPointerType.BuildAFoodShop:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(212, 436, 26, 23);
          break;
        case OffscreenPointerType.BuildAGiftShop:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(187, 409, 21, 23);
          break;
        case OffscreenPointerType.BuildABench:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(0, 460, 23, 23);
          break;
        case OffscreenPointerType.BuildABin:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(209, 409, 21, 24);
          break;
        case OffscreenPointerType.ShopNeedsEmployee:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(252, 418, 22, 25);
          break;
        case OffscreenPointerType.JobApplicants:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(0, 413, 20, 23);
          break;
        case OffscreenPointerType.TicketPrice:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(188, 433, 23, 25);
          break;
        case OffscreenPointerType.BuildAPen:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(188, 433, 23, 25);
          break;
        case OffscreenPointerType.AddAnimalsToYourPen:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(191, 459, 23, 24);
          break;
        case OffscreenPointerType.HireFromGate:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(0, 413, 20, 23);
          break;
        case OffscreenPointerType.NoWaterConnection:
          texture = AssetContainer.SpriteSheet;
          rectangle = new Rectangle(144, 458, 23, 28);
          break;
        default:
          throw new Exception("ADD RECTANGLE HERE");
      }
      return rectangle;
    }

    public bool UpdateEventPointer(float DeltaTime)
    {
      if (!this.bActive)
        return false;
      if (this.PointAtThisAnimal != null)
        this.TargetPosition = RenderMath.TranslateWorldSpaceToScreenSpace(this.PointAtThisAnimal.vLocation);
      else if (this.eventtype == SpecialEventType.AnimalFight)
      {
        this.TargetPosition = this.PointAtThisAnimal.vLocation - this.PointAtThisOtherAnimal.vLocation;
        this.TargetPosition.X *= 0.5f;
        this.TargetPosition.Y *= 0.5f;
        this.TargetPosition.X += this.PointAtThisAnimal.vLocation.X;
        this.TargetPosition.Y += this.PointAtThisAnimal.vLocation.Y;
        this.TargetPosition = RenderMath.TranslateWorldSpaceToScreenSpace(this.TargetPosition);
        if (this.fightmanager.IsDone)
          return true;
      }
      else
        this.TargetPosition = RenderMath.TranslateWorldSpaceToScreenSpace(this.TargetPositionInWorldSpace);
      bool WasOffScreen;
      Vector2 ScreenEdge;
      float Rotation;
      MathStuff.GetPointingOffScreen(this.TargetPosition, out WasOffScreen, out ScreenEdge, out Rotation, 120f * this.scale, OffScreenToScreen_SmoothTransition: true);
      if (WasOffScreen)
      {
        this.Rotation = Rotation;
        this.Rotation -= 0.7853982f;
        this.vLocation = ScreenEdge;
        if ((double) this.fAlpha < 0.800000011920929)
          this.fAlpha += DeltaTime * 4f;
        if ((double) this.fAlpha > 0.800000011920929)
        {
          this.Rounder.fAlpha = 0.0f;
        }
        else
        {
          this.Rounder.fAlpha = 0.8f - this.fAlpha;
          if ((double) this.Rounder.fAlpha >= 0.300000011920929)
            ;
        }
      }
      else
      {
        if ((double) this.fAlpha > 0.0)
          this.fAlpha -= DeltaTime * 5f;
        if ((double) this.fAlpha < 0.0)
          this.fAlpha = 0.0f;
        this.vLocation = this.TargetPosition;
        this.Rounder.fAlpha = (float) (0.800000011920929 * (1.0 - (double) this.fAlpha));
      }
      return false;
    }

    internal static string GetOffscreenPointerTypeToPinString(OffscreenPointerType pointertype)
    {
      switch (pointertype)
      {
        case OffscreenPointerType.PointAtNoWater:
          return "No Water";
        case OffscreenPointerType.PointAtLowWater:
          return "Low Water";
        case OffscreenPointerType.HungryAnimals:
          return "Hungry Animals";
        case OffscreenPointerType.DeadAnimals:
          return "Dead Animals";
        case OffscreenPointerType.NewBirths:
          return "New Births";
        case OffscreenPointerType.DeadPeople:
          return "Dead People";
        case OffscreenPointerType.NoEnrichment:
          return "No Enrichment";
        case OffscreenPointerType.BreedingRoomBirth:
          return "Breeding Program Birth";
        case OffscreenPointerType.CripserBirth:
          return "CRISPR Birth";
        case OffscreenPointerType.SickAnimals:
          return "Sick Animals";
        case OffscreenPointerType.BuildADrinksShop:
          return "Build Drinks Shop";
        case OffscreenPointerType.BuildAnyShop:
          return "Build a Shop";
        case OffscreenPointerType.BuildAFoodShop:
          return "Build Food Shop";
        case OffscreenPointerType.BuildAGiftShop:
          return "Build Gift Shop";
        case OffscreenPointerType.BuildABench:
          return "Build a Bench";
        case OffscreenPointerType.BuildABin:
          return "Build a Bin";
        case OffscreenPointerType.ShopNeedsEmployee:
          return "Employee Needed";
        case OffscreenPointerType.JobApplicants:
          return "Job Applicants";
        case OffscreenPointerType.TicketPrice:
          return "Tickets";
        case OffscreenPointerType.BuildAPen:
          return "Build a pen";
        case OffscreenPointerType.AddAnimalsToYourPen:
          return "Add Animals";
        case OffscreenPointerType.HireFromGate:
          return "Hire Janitor";
        case OffscreenPointerType.NoWaterConnection:
          return "Link Water";
        default:
          return "NO TEXT NAME";
      }
    }

    public void DrawEventPointer()
    {
      if (!this.bActive)
        return;
      this.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.Icon.scale = this.scale;
      float num = 0.5f;
      if (this.IsTinyIcon)
        num = 1f;
      if ((double) Sengine.WorldOriginandScale.Z < (double) num)
      {
        this.Icon.scale = this.scale - (num - Sengine.WorldOriginandScale.Z);
        this.scale = this.Icon.scale;
      }
      if ((double) this.Rounder.fAlpha > 0.0)
      {
        this.Rounder.scale = this.scale;
        this.Rounder.vLocation = this.vLocation;
        this.Rounder.Draw(AssetContainer.pointspritebatch03, AssetContainer.UISheet, Vector2.Zero, this.Rounder.fAlpha * this.Icon.fAlpha);
      }
      this.Draw(AssetContainer.pointspritebatch03, AssetContainer.UISheet, Vector2.Zero, this.fAlpha * this.Icon.fAlpha);
      if (this.WillUndulate)
        this.Icon.Draw(AssetContainer.pointspritebatch03, this.Iconsheet, this.vLocation + new Vector2(0.0f, (float) ((double) FlashingAlpha.MediumSin * 5.0 - 3.0)));
      else
        this.Icon.Draw(AssetContainer.pointspritebatch03, this.Iconsheet, this.vLocation);
    }
  }
}
