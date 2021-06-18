// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Fights.AnimalAttackHandler
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_OverWorld;
using TinyZoo.Z_Particles;

namespace TinyZoo.Z_Fights
{
  internal class AnimalAttackHandler
  {
    private HealthBar HealthBar;
    private float ALphaBar;
    private float TimeToNextStrike;
    private AnimalStat Attackerstats;
    private float Defence;
    private float AttackSpeed;
    private float Strength;
    private float DamagePerStrike;
    public AnimalAttackHandler opponent;
    public AnimalRenderMan OpponentRenderer;
    public AnimalRenderMan ThisAnimal;
    public float Health;
    public List<Z_Particle> particles;
    public List<Z_Particle> AttakSwipeParticles;
    public bool HasWon;
    public bool Dead;
    public bool HasTeleported;
    private bool Jumping_WitingForSTrike;
    private Vector2 LastDrawLocation;

    public AnimalAttackHandler(AnimalStat stats, AnimalRenderMan thisrenderer)
    {
      this.ThisAnimal = thisrenderer;
      this.ThisAnimal.enemyrenderere.animator.StopJumping();
      this.ALphaBar = 1f;
      this.Dead = false;
      this.HasWon = false;
      this.Attackerstats = stats;
      this.HealthBar = new HealthBar();
      this.Defence = 100f - stats.Defence;
      this.Defence *= 0.01f;
      this.Defence *= 0.8f;
      if ((double) this.Defence >= 1.0)
        throw new Exception("khsf");
      this.Strength = stats.Strength;
      this.AttackSpeed = stats.AttackSpeed;
      this.DamagePerStrike = stats.Strength * (float) (0.300000011920929 + (100.0 - (double) this.Attackerstats.AttackSpeed) * 0.00999999977648258);
      this.SetNextTimeTStrike();
      this.TimeToNextStrike *= 0.5f;
      this.Health = 100f;
      this.HasTeleported = false;
    }

    private void SetNextTimeTStrike()
    {
      this.TimeToNextStrike = (float) (0.300000011920929 + (100.0 - (double) this.Attackerstats.AttackSpeed) * 0.0199999995529652);
      this.TimeToNextStrike *= 2f;
      this.TimeToNextStrike += (float) Game1.Rnd.Next(0, 100) * 0.01f;
    }

    public void DoDamage(float DMG, ref bool IsDone)
    {
      if (this.Dead)
        return;
      Vector2 vLocation = this.ThisAnimal.enemyrenderere.vLocation;
      vLocation.Y -= (float) this.ThisAnimal.enemyrenderere.DrawRect.Height / 2f;
      DMG *= this.Defence;
      this.Health -= DMG;
      if ((double) this.Health <= 0.0)
      {
        if (!this.Dead)
        {
          MoneyRenderer.DoDamage(this.LastDrawLocation - new Vector2(0.0f, 16f), IconPopType.Damage, (int) ((double) DMG + (double) this.Health));
          this.Health = 0.0f;
          IsDone = true;
          this.opponent.WonFight();
          this.Dead = true;
          this.AttakSwipeParticles = OverWorldManager.particlemanager.SpawnParticle(vLocation, this.ThisAnimal.enemyrenderere.vLocation.Y, this.ThisAnimal.enemyrenderere.vLocation.Y + 5f, 1, ParticleType.FightDeath, false);
          this.particles = OverWorldManager.particlemanager.SpawnParticle(vLocation, this.ThisAnimal.enemyrenderere.vLocation.Y, this.ThisAnimal.enemyrenderere.vLocation.Y + 5f, 20, ParticleType.Blood, false);
        }
      }
      else
      {
        this.particles = OverWorldManager.particlemanager.SpawnParticle(vLocation, this.ThisAnimal.enemyrenderere.vLocation.Y, this.ThisAnimal.enemyrenderere.vLocation.Y + 5f, 10, ParticleType.Blood, false);
        MoneyRenderer.DoDamage(this.LastDrawLocation - new Vector2(0.0f, 16f), IconPopType.Damage, (int) DMG);
        this.AttakSwipeParticles = OverWorldManager.particlemanager.SpawnParticle(vLocation, this.ThisAnimal.enemyrenderere.vLocation.Y, this.ThisAnimal.enemyrenderere.vLocation.Y + 5f, 1, ParticleType.AttackFlash, false);
      }
      this.HealthBar.SetHealthBarFullness(this.Health * 0.01f);
    }

    private void WonFight() => this.HasWon = true;

    public void UpdateAnimalAttackHandler(float DeltaTime, ref bool IsDone)
    {
      if (!this.HasWon)
      {
        this.TimeToNextStrike -= DeltaTime;
        if ((double) this.TimeToNextStrike >= 0.0)
          return;
        if (this.Jumping_WitingForSTrike)
        {
          this.ThisAnimal.enemyrenderere.animator.TargetJumpPeakLoc = new Vector2(this.OpponentRenderer.enemyrenderere.vLocation.X, this.OpponentRenderer.enemyrenderere.vLocation.Y - (float) this.OpponentRenderer.enemyrenderere.DrawRect.Height) - this.ThisAnimal.enemyrenderere.vLocation;
          if (!this.ThisAnimal.enemyrenderere.animator.JustHitJumpPeak)
            return;
          this.ThisAnimal.enemyrenderere.animator.JustHitJumpPeak = false;
          this.SetNextTimeTStrike();
          this.opponent.DoDamage(this.DamagePerStrike, ref IsDone);
          this.Jumping_WitingForSTrike = false;
        }
        else
        {
          this.ThisAnimal.enemyrenderere.animator.TryToJumpForAttack(new Vector2(this.OpponentRenderer.enemyrenderere.vLocation.X, this.OpponentRenderer.enemyrenderere.vLocation.Y - (float) this.OpponentRenderer.enemyrenderere.DrawRect.Height) - this.ThisAnimal.enemyrenderere.vLocation);
          this.Jumping_WitingForSTrike = true;
        }
      }
      else
      {
        if ((double) this.ALphaBar <= 0.0)
          return;
        this.ALphaBar -= DeltaTime;
        if ((double) this.ALphaBar >= 0.0)
          return;
        this.ALphaBar = 0.0f;
      }
    }

    public bool DrawAnimalAttackHandler(Vector2 Location)
    {
      bool flag = false;
      if (this.particles != null)
      {
        for (int index = 0; index < this.particles.Count; ++index)
        {
          this.particles[index].DrawParticle(AssetContainer.pointspritebatch03);
          if (this.particles[index].bActive)
            flag = true;
        }
      }
      if (this.AttakSwipeParticles != null)
      {
        for (int index = 0; index < this.AttakSwipeParticles.Count; ++index)
        {
          this.AttakSwipeParticles[index].DrawParticle(AssetContainer.pointspritebatch03);
          if (this.AttakSwipeParticles[index].bActive)
            flag = true;
        }
      }
      this.LastDrawLocation = Location;
      this.HealthBar.DrawHealthBar(Location, this.ALphaBar);
      if (!this.Dead && !this.HasWon || flag)
        return false;
      if (this.HasWon)
        this.ThisAnimal.enemyrenderere.animator.UnStopJumping();
      return true;
    }
  }
}
