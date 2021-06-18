// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.Airspace.AirDeliveryCrate
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
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_OverWorld.Airspace
{
  internal class AirDeliveryCrate
  {
    private AnimatedGameObject frontlayer;
    private AnimatedGameObject backlayer;
    private Rectangle frontRect = new Rectangle(208, 1197, 51, 39);
    private Vector2 frontOrigin = new Vector2(36f, 31f);
    private Rectangle backRect = new Rectangle(208, 1237, 38, 38);
    private Vector2 backOrigin = new Vector2(19f, 24f);
    private Rectangle frontRect_crispr = new Rectangle(878, 1530, 34, 41);
    private Vector2 frontOrigin_crispr = new Vector2(18f, 33f);
    private Rectangle backRect_crispr = new Rectangle(799, 1632, 40, 48);
    private Vector2 backOrigin_crispr = new Vector2(20f, 30f);
    private List<EnemyRenderer> enemyrenderers;
    private List<Vector2> randomoffsets;
    private AirDeliveryCrate.CrateState state;
    private WaveInfo Ref_deliveryWave;
    private bool frontfinished;
    private float animationAlpha = 1f;
    private bool drawanimals;
    private bool open;
    private float opentimer;
    private float opendelay;
    private int OrderUID;
    private bool usecrisprcrate;
    public float alpha = 1f;
    public Vector2 location;

    public AirDeliveryCrate(WaveInfo delivery, int _OrderUID, bool usecrisprcrate_ = false)
    {
      this.usecrisprcrate = usecrisprcrate_;
      this.OrderUID = _OrderUID;
      this.Ref_deliveryWave = delivery;
      this.frontlayer = new AnimatedGameObject();
      if (this.usecrisprcrate)
      {
        this.frontlayer.DrawRect = this.frontRect_crispr;
        this.frontlayer.DrawOrigin = this.frontOrigin_crispr;
        this.frontlayer.SetUpSimpleAnimation(11, 0.12f);
      }
      else
      {
        this.frontlayer.DrawRect = this.frontRect;
        this.frontlayer.DrawOrigin = this.frontOrigin;
        this.frontlayer.SetUpSimpleAnimation(7, 0.12f);
      }
      this.frontlayer.PlayOnlyOnce = true;
      this.backlayer = new AnimatedGameObject();
      if (this.usecrisprcrate)
      {
        this.backlayer.DrawRect = this.backRect_crispr;
        this.backlayer.DrawOrigin = this.backOrigin_crispr;
        this.backlayer.SetUpSimpleAnimation(3, 0.12f);
      }
      else
      {
        this.backlayer.DrawRect = this.backRect;
        this.backlayer.DrawOrigin = this.backOrigin;
        this.backlayer.SetUpSimpleAnimation(4, 0.12f);
      }
      this.backlayer.PlayOnlyOnce = true;
      this.state = AirDeliveryCrate.CrateState.Start;
      this.enemyrenderers = new List<EnemyRenderer>();
      this.randomoffsets = new List<Vector2>();
      for (int index = 0; index < delivery.People.Count; ++index)
      {
        IntakePerson _intakeperson = delivery.People[index];
        EnemyRenderer enemyRenderer = new EnemyRenderer(_intakeperson.animaltype, _intakeperson.CLIndex, _intakeperson.HeadType, _intakeperson.HeadVariant);
        PrisonerInfo prisonerInfo = new PrisonerInfo(_intakeperson, false, Vector2.Zero, CellBlockType.Count);
        enemyRenderer.SetAsBaby(prisonerInfo.GetIsABaby());
        this.enemyrenderers.Add(enemyRenderer);
        this.randomoffsets.Add((float) (1.79999995231628 * (TinyZoo.Game1.Rnd.NextDouble() - 0.5)) * new Vector2(TileMath.HalfTileSize, TileMath.HalfTileSize * Sengine.ScreenRatioUpwardsMultiplier.Y));
      }
    }

    public void DelayedOpen(float delayinseconds = 1f)
    {
      this.opendelay = delayinseconds;
      this.opentimer = 0.0f;
    }

    public void Open() => this.open = true;

    public bool UpdateAirDeliveryCrate(float DeltaTime, Player player, ref bool HasDelivered)
    {
      bool flag = false;
      switch (this.state)
      {
        case AirDeliveryCrate.CrateState.Start:
          this.state = AirDeliveryCrate.CrateState.Closed;
          break;
        case AirDeliveryCrate.CrateState.Closed:
          if ((double) this.opentimer < (double) this.opendelay)
          {
            this.opentimer += DeltaTime;
            if ((double) this.opentimer >= (double) this.opendelay)
              this.Open();
          }
          if (this.open)
          {
            this.state = AirDeliveryCrate.CrateState.Opening;
            this.open = false;
            break;
          }
          break;
        case AirDeliveryCrate.CrateState.Opening:
          this.frontlayer.UpdateAnimation(DeltaTime);
          if (this.usecrisprcrate)
          {
            if (this.frontlayer.Frame == 9)
              this.drawanimals = true;
            if (!this.frontlayer.AnimationFinished)
            {
              int num = this.frontlayer.Frame - 9;
              int _Frame = num < 0 ? 0 : num;
              this.backlayer.Frame = _Frame;
              this.backlayer.ForceToFrame(_Frame);
            }
            else
            {
              if (!this.frontfinished)
                this.backlayer.ForceToFrame(this.backlayer.Frame + 1);
              this.backlayer.UpdateAnimation(DeltaTime);
              this.frontfinished = true;
            }
          }
          else
          {
            if (this.frontlayer.Frame == 5)
              this.drawanimals = true;
            if (!this.frontlayer.AnimationFinished)
            {
              int num = this.frontlayer.Frame - 4;
              int _Frame = num < 0 ? 0 : num;
              this.backlayer.Frame = _Frame;
              this.backlayer.ForceToFrame(_Frame);
            }
            else
            {
              if (!this.frontfinished)
                this.backlayer.ForceToFrame(this.backlayer.Frame + 1);
              this.backlayer.UpdateAnimation(DeltaTime);
              this.frontfinished = true;
            }
          }
          if (this.backlayer.AnimationFinished)
          {
            this.backlayer.AnimationFinished = false;
            if ((double) this.animationAlpha > 0.0)
            {
              this.animationAlpha -= 1f * DeltaTime;
              break;
            }
            if (this.drawanimals)
            {
              this.drawanimals = false;
              this.Ref_deliveryWave.SetFromCrate(this.enemyrenderers, this.OrderUID);
              player.livestats.AnimalsJustTraded = this.Ref_deliveryWave;
              player.livestats.AddNewAnimalsToEnclosure(player, player.animalsonorder.GetPrisonID(this.OrderUID));
            }
            this.animationAlpha = 0.0f;
            this.state = AirDeliveryCrate.CrateState.Opened;
            break;
          }
          break;
        case AirDeliveryCrate.CrateState.Opened:
          HasDelivered = true;
          break;
      }
      this.frontlayer.SetAlpha(this.alpha * this.animationAlpha);
      this.backlayer.SetAlpha(this.alpha * this.animationAlpha);
      return flag;
    }

    public void DrawAirDeliveryCrate(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.backlayer.WorldOffsetDraw(spritebatch, AssetContainer.AnimalSheet, this.location, 1f, 0.0f);
      if (this.drawanimals)
      {
        for (int index = 0; index < this.enemyrenderers.Count; ++index)
        {
          EnemyRenderer enemyrenderer = this.enemyrenderers[index];
          enemyrenderer.vLocation = this.randomoffsets[index] + offset;
          enemyrenderer.DrawEnemyRenderer();
        }
      }
      if (this.frontfinished)
        return;
      this.frontlayer.WorldOffsetDraw(spritebatch, AssetContainer.AnimalSheet, this.location, 1f, 0.0f);
    }

    private enum CrateState
    {
      Start,
      Closed,
      Opening,
      Opened,
      Count,
    }
  }
}
