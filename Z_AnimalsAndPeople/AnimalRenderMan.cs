// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.AnimalRenderMan
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.SwitchRandom;
using TinyZoo.Z_AnimalsAndPeople.PenNav;
using TinyZoo.Z_Events.BreakOut;
using TinyZoo.Z_Fights;
using TinyZoo.Z_OverWorld;

namespace TinyZoo.Z_AnimalsAndPeople
{
  internal class AnimalRenderMan : Enemy
  {
    private FightManager REF_fightmanager;
    public bool HoldingToy;
    public bool IsBeingEnriched;
    public int LastInteractionPoint_UID = -1;
    public bool BlockWalkingAndRendering;
    public bool BlockRendering;
    public PrisonerInfo REF_prisonerinfo;
    public float EnrchmentDelay;
    public PenNavigator pennavigation;
    private AnimatedGameObject EnrichAttachment;
    private Texture2D DrawAttachmentWithThis;

    public AnimalRenderMan(
      RandomResultContainer rand,
      IntakePerson person,
      PrisonerInfo prisonerinfo = null)
      : base(rand, person)
    {
      this.REF_prisonerinfo = prisonerinfo;
      if (prisonerinfo.GetIsABaby())
        this.enemyrenderere.SetAsBaby();
      if (prisonerinfo.IsPainted)
        this.enemyrenderere.ReconstructAsNew(prisonerinfo.GetAnimalPainted(), prisonerinfo.intakeperson.CLIndex, prisonerinfo.intakeperson.HeadType, prisonerinfo.intakeperson.HeadVariant);
      this.CheckDeath();
    }

    public void SetForIrregularEnclosure(
      List<Vector2Int> FloorLocations,
      int PenUID,
      bool ForceLoc = false)
    {
      this.pennavigation = new PenNavigator(FloorLocations, PenUID, this.refperson.animaltype);
      this.pennavigation.StartNavigating(ref this.enemyrenderere.vLocation, this.enemyrenderere.DrawRect, ForceLoc);
    }

    public void AddFightController(FightManager fightmanager) => this.REF_fightmanager = fightmanager;

    internal static int SortAnimal(AnimalRenderMan a, AnimalRenderMan b)
    {
      if ((double) a.enemyrenderere.vLocation.Y < (double) b.enemyrenderere.vLocation.Y)
        return -1;
      return (double) a.enemyrenderere.vLocation.Y > (double) b.enemyrenderere.vLocation.Y ? 1 : 0;
    }

    public void CheckDeath()
    {
      if (this.REF_prisonerinfo == null || !this.REF_prisonerinfo.IsDead)
        return;
      this.enemyrenderere.SetDead(this.REF_prisonerinfo.causeofdeath, this.REF_prisonerinfo.GetIsABaby());
    }

    public void EndBreakOutReturnToPen()
    {
      this.REF_prisonerinfo.IsCurrentlyBrokenOut = false;
      this.pennavigation.EndBreakOut(this.enemyrenderere.vLocation);
    }

    public bool UpdateAnimal(float DeltaTime)
    {
      if ((double) this.EnrchmentDelay > 0.0)
        this.EnrchmentDelay -= DeltaTime;
      if (this.REF_prisonerinfo != null && this.REF_prisonerinfo.IsDead)
        this.enemyrenderere.SetDead(this.REF_prisonerinfo.causeofdeath, this.REF_prisonerinfo.GetIsABaby());
      if (this.pennavigation == null)
        return this.UpdateEnemyBouncing(DeltaTime);
      if (this.REF_prisonerinfo.IsCurrentlyFighting)
      {
        if (this.REF_fightmanager.UpdateFightManager(DeltaTime, this.REF_prisonerinfo))
        {
          if (!this.REF_fightmanager.animalattackhandler_Attaccker.HasTeleported)
          {
            this.enemyrenderere.vLocation = this.REF_fightmanager.animalattackhandler_Attaccker.OpponentRenderer.enemyrenderere.vLocation;
            ++this.enemyrenderere.vLocation.Y;
            float num = this.REF_fightmanager.animalattackhandler_Attaccker.OpponentRenderer.enemyrenderere.DrawOrigin.X + this.enemyrenderere.DrawOrigin.X;
            bool AttackerWentLeft;
            if ((double) this.REF_fightmanager.animalattackhandler_Attaccker.OpponentRenderer.enemyrenderere.DirectionFacing.X < 0.0)
            {
              this.enemyrenderere.vLocation.X -= num;
              this.enemyrenderere.DirectionFacing.X = 1f;
              AttackerWentLeft = true;
            }
            else
            {
              this.enemyrenderere.vLocation.X += num;
              this.enemyrenderere.FlipRender = false;
              this.enemyrenderere.DirectionFacing.X = -1f;
              AttackerWentLeft = false;
            }
            MoneyRenderer.PopIcon(this.enemyrenderere.vLocation + (this.REF_fightmanager.animalattackhandler_Attaccker.OpponentRenderer.enemyrenderere.vLocation - this.enemyrenderere.vLocation) * 0.5f, IconPopType.FightStart);
            this.REF_fightmanager.animalattackhandler_Attaccker.HasTeleported = true;
            this.pennavigation.FixLocationsOnStartFight(this.enemyrenderere, this.REF_fightmanager.animalattackhandler_Attaccker.OpponentRenderer.enemyrenderere, AttackerWentLeft);
          }
          if (this.REF_fightmanager.animalattackhandler_Attaccker.HasWon)
            this.REF_prisonerinfo.IsCurrentlyFighting = false;
          else if (this.REF_fightmanager.animalattackhandler_Attaccker.Dead)
          {
            this.REF_prisonerinfo.IsCurrentlyFighting = false;
            this.REF_prisonerinfo.IsDead = true;
            this.REF_prisonerinfo.causeofdeath = CauseOfDeath.KilledInAnimalFight;
            this.CheckDeath();
          }
        }
        else if (this.REF_fightmanager.animalattackhandler_Defender.HasWon)
          this.REF_prisonerinfo.IsCurrentlyFighting = false;
        else if (this.REF_fightmanager.animalattackhandler_Defender.Dead)
        {
          this.REF_prisonerinfo.IsCurrentlyFighting = false;
          this.REF_prisonerinfo.IsDead = true;
          this.REF_prisonerinfo.causeofdeath = CauseOfDeath.KilledInAnimalFight;
          this.CheckDeath();
        }
      }
      else if (this.enemyrenderere.CanMove() && !this.BlockWalkingAndRendering)
        this.pennavigation.UpdatePenNavigator(ref this.enemyrenderere.vLocation, DeltaTime, this.enemyrenderere, this.REF_prisonerinfo);
      this.enemyrenderere.UpdateAnimalRenderer(DeltaTime);
      if (this.REF_prisonerinfo.JustFed)
      {
        this.REF_prisonerinfo.JustFed = false;
        MoneyRenderer.FeedAnimal(this.enemyrenderere.vLocation - new Vector2(0.0f, 16f));
      }
      return false;
    }

    public void AddAttachementToDraw(AnimatedGameObject objec, Texture2D _DrawAttachmentWithThis)
    {
      this.DrawAttachmentWithThis = _DrawAttachmentWithThis;
      this.EnrichAttachment = objec;
    }

    public void StartBreakOut(Vector2Int GateLocation, Vector2Int SpaceBehindGate, int Cell_UID)
    {
      if (this.REF_prisonerinfo.IsCurrentlyBrokenOut)
        return;
      this.pennavigation.StartBreakOut(GateLocation, SpaceBehindGate);
      this.REF_prisonerinfo.IsCurrentlyBrokenOut = true;
      BreakOutManager.AddEscapedAnimal(this, Cell_UID);
    }

    public void DrawAnimal()
    {
      if (this.BlockWalkingAndRendering)
        return;
      if (!this.BlockRendering)
        this.DrawEnemy();
      if (this.EnrichAttachment != null)
      {
        this.EnrichAttachment.FlipRender = this.enemyrenderere.FlipRender;
        this.EnrichAttachment.WorldOffsetDraw(AssetContainer.pointspritebatch01, this.DrawAttachmentWithThis, this.enemyrenderere.vLocation + this.enemyrenderere.animator.PositionalOffset * this.enemyrenderere.scale, this.enemyrenderere.animator.CurrentScale, 0.0f);
        this.EnrichAttachment = (AnimatedGameObject) null;
      }
      if (this.REF_fightmanager == null || !this.REF_fightmanager.DrawFightManager(this.enemyrenderere.vLocation, this.REF_prisonerinfo))
        return;
      this.REF_fightmanager = (FightManager) null;
    }

    public void DrawBlockRenderAnimal()
    {
      this.DrawEnemy();
      if (this.EnrichAttachment != null)
      {
        this.EnrichAttachment.FlipRender = this.enemyrenderere.FlipRender;
        this.EnrichAttachment.WorldOffsetDraw(AssetContainer.pointspritebatch01, this.DrawAttachmentWithThis, this.enemyrenderere.vLocation + this.enemyrenderere.animator.PositionalOffset * this.enemyrenderere.scale, this.enemyrenderere.animator.CurrentScale, 0.0f);
        this.EnrichAttachment = (AnimatedGameObject) null;
      }
      if (this.REF_fightmanager == null || !this.REF_fightmanager.DrawFightManager(this.enemyrenderere.vLocation, this.REF_prisonerinfo))
        return;
      this.REF_fightmanager = (FightManager) null;
    }
  }
}
