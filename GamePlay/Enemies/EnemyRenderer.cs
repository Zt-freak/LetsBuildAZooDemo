// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Enemies.EnemyRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Buttons;
using System;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_DayNight;

namespace TinyZoo.GamePlay.Enemies
{
  internal class EnemyRenderer : AnimatedGameObject
  {
    private DeathSkull deathskull;
    private GameObject WrongCell;
    public float HalfEnemyWidth;
    public float HalfEnemyHeight;
    public float LowerFootHeight;
    public GameObject Shadow;
    public bool IsDead;
    public Vector2 DirectionFacing;
    private GameObject VOOl;
    public Animalator animator;
    private DirectionPressed facingthisway;
    private AnimatedGameObject Head;
    public CorpseRenderer corpse;
    public AnimalType enemytype;
    private int Variant;
    private float MouseOverAlphs = 0.4f;

    public EnemyRenderer(
      AnimalType _enemytype,
      int CLIndex,
      AnimalType ReplacementHead = AnimalType.None,
      int HeadVariant = 0)
    {
      this.ReconstructAsNew(_enemytype, CLIndex, ReplacementHead, HeadVariant);
    }

    public void ReconstructAsNew(
      AnimalType _enemytype,
      int CLIndex,
      AnimalType ReplacementHead = AnimalType.None,
      int HeadVariant = 0)
    {
      this.Variant = CLIndex;
      if (_enemytype == AnimalType.Rabbit && CLIndex == 0 || _enemytype == AnimalType.Goose && CLIndex == 0 || (_enemytype == AnimalType.Armadillo && CLIndex == 1 || _enemytype == AnimalType.Beavers && CLIndex == 5) || (_enemytype == AnimalType.Alpaca && CLIndex == 0 || _enemytype == AnimalType.Meerkat && CLIndex == 9 || (_enemytype == AnimalType.Lion && CLIndex == 6 || _enemytype == AnimalType.Crocodile && CLIndex == 9)) || (_enemytype == AnimalType.Peacock && CLIndex == 3 || _enemytype == AnimalType.Badger && CLIndex == 5 || (_enemytype == AnimalType.Skunk && CLIndex == 6 || _enemytype == AnimalType.Monkey && CLIndex == 3) || (_enemytype == AnimalType.Monkey && CLIndex == 7 || _enemytype == AnimalType.Seal && CLIndex == 4 || (_enemytype == AnimalType.Fox && CLIndex == 1 || _enemytype == AnimalType.Chicken && CLIndex == 0))) || _enemytype == AnimalType.PolarBear)
        this.MouseOverAlphs = 0.7f;
      this.enemytype = _enemytype;
      this.VOOl = new GameObject();
      this.VOOl.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.VOOl.SetDrawOriginToCentre();
      EnemyInfoPack enemyRectangle = EnemyData.GetEnemyRectangle(this.enemytype);
      this.Shadow = new GameObject();
      ShadowInfo enemyShadow = EnemyData.GetEnemyShadow(this.enemytype);
      this.Shadow.DrawRect = enemyShadow.ShadowRect;
      this.Shadow.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.Shadow.DrawOrigin.Y -= enemyShadow.OriginY;
      this.DrawRect = enemyRectangle.GetBaseWalk(CLIndex);
      int frames = enemyRectangle.Frames;
      this.DrawOrigin = enemyRectangle.Origin;
      if (ReplacementHead != AnimalType.None)
      {
        int variant = this.Variant;
        Rectangle BodyRect;
        Vector2 HeadOffsetFromBody;
        Rectangle HeadRect;
        Vector2 HeadOrigin;
        GeneData.GetHybrid(this.enemytype, ReplacementHead, out BodyRect, out HeadOffsetFromBody, out HeadRect, out HeadOrigin, CLIndex, HeadVariant);
        this.DrawRect = BodyRect;
        this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
        this.Head = new AnimatedGameObject();
        this.Head.DrawRect = HeadRect;
        this.Head.DrawOrigin.Y = HeadOrigin.Y - HeadOffsetFromBody.Y;
        this.Head.DrawOrigin.Y += this.DrawOrigin.Y;
        this.Head.DrawOrigin.X = HeadOrigin.X - HeadOffsetFromBody.X;
        this.Head.DrawOrigin.X += this.DrawOrigin.X;
      }
      TinyZoo.Game1.Rnd.Next(0, 3);
      this.DirectionFacing = new Vector2(1f, 1f);
      this.SetUpSimpleAnimation(frames, 0.2f);
      this.HalfEnemyWidth = (float) (this.DrawRect.Width / 2);
      this.HalfEnemyWidth -= 3f;
      this.HalfEnemyHeight = (float) this.DrawRect.Height - this.DrawOrigin.Y;
      this.HalfEnemyHeight -= 2f;
      this.HalfEnemyHeight *= Sengine.ScreenRatioUpwardsMultiplier.Y;
      if ((double) this.HalfEnemyHeight > 6.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y)
        this.HalfEnemyHeight = 6f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      if (this.enemytype <= AnimalType.SecretAnimalsCount)
        this.animator = new Animalator(EnemyData.GetEnemyAnimator(this.enemytype));
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
    }

    public void SetDead(CauseOfDeath causeofdeath, bool IsABaby)
    {
      if (this.IsDead)
        return;
      this.corpse = new CorpseRenderer(causeofdeath, this.enemytype, this.Variant);
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.IsDead = true;
      if (!IsABaby)
        return;
      this.corpse.scale = 0.5f;
    }

    public void SetAsBaby(bool IsBaby = true) => this.animator.SetAsBaby(IsBaby);

    public void SetWrongCell()
    {
      this.WrongCell = new GameObject();
      this.WrongCell.DrawRect = new Rectangle(103, 22, 7, 7);
      this.WrongCell.SetAllColours(ColourData.FernRed);
      this.WrongCell.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.WrongCell.DrawOrigin.Y += this.DrawOrigin.Y + 1f;
    }

    public bool IsWrongCell() => this.WrongCell != null;

    public void KillThis()
    {
      TinyZoo.Game1.Rnd.Next(0, 6);
      this.deathskull = new DeathSkull();
      this.deathskull.vLocation = this.vLocation;
    }

    public bool CanMove() => this.animator == null || this.IsDead || this.animator.GetCanMove();

    public override void SetAllColours(Vector3 colors)
    {
      base.SetAllColours(colors);
      if (this.Head == null)
        return;
      this.Head.SetAllColours(colors);
    }

    public void UpdateAnimalRenderer(float DeltaTime)
    {
      if (this.IsDead)
        this.corpse.UpdateCorpseRenderer(DeltaTime);
      else if (this.animator != null)
        this.animator.UpdateAnimalator(DeltaTime);
      else
        this.UpdateAnimation(DeltaTime);
    }

    public void DrawEnemyRenderer(bool MouseOver = false)
    {
      if (this.IsDead)
        this.corpse.DrawCorpseRenderer(this.vLocation);
      else if (this.deathskull != null)
      {
        this.deathskull.DrawDeathSkull();
      }
      else
      {
        if (this.animator != null)
        {
          this.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
          if (TinyZoo.Game1.gamestate != GAMESTATE.PhotoMode)
          {
            this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet, this.vLocation + this.animator.PositionalOffset * this.scale, this.scale * this.animator.CurrentScale * this.DirectionFacing, this.animator.CurrentRotation);
            if (this.Head != null)
            {
              this.Head.vLocation = this.vLocation;
              this.Head.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
              this.Head.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet, this.vLocation + this.animator.PositionalOffset * this.scale, this.scale * this.animator.CurrentScale * this.DirectionFacing, this.animator.CurrentRotation);
            }
            if (MouseOver)
            {
              this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet_white, this.vLocation + this.animator.PositionalOffset * this.scale, this.scale * this.animator.CurrentScale * this.DirectionFacing, this.animator.CurrentRotation, true, this.DrawRect, this.fAlpha * this.MouseOverAlphs, this.GetColour());
              if (this.Head != null)
                this.Head.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet_white, this.Head.vLocation + this.animator.PositionalOffset * this.scale, this.Head.scale * this.animator.CurrentScale * this.DirectionFacing, this.animator.CurrentRotation, true, this.Head.DrawRect, this.Head.fAlpha * this.MouseOverAlphs, this.GetColour());
            }
          }
          else
          {
            this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet, this.vLocation + this.animator.PositionalOffset * this.scale, this.scale * Vector2.One * this.DirectionFacing, this.animator.CurrentRotation);
            if (this.Head != null)
            {
              this.Head.vLocation = this.vLocation;
              this.Head.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
              this.Head.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet, this.vLocation + this.animator.PositionalOffset * this.scale, this.scale * Vector2.One * this.DirectionFacing, this.animator.CurrentRotation);
            }
          }
        }
        else
          this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet);
        if (this.WrongCell == null || GameFlags.PhotoMode)
          return;
        this.WrongCell.vLocation = this.vLocation;
      }
    }

    public void ScreenSpaceDrawEnemyRendererShadow(Vector2 Offset) => this.ScreenSpaceDrawEnemyRendererShadow(Offset, AssetContainer.pointspritebatchTop05);

    public void ScreenSpaceDrawEnemyRendererShadow(
      Vector2 Offset,
      SpriteBatch spritchbatch,
      float ScaleMultipier = 1f)
    {
      this.Shadow.scale = this.scale * ScaleMultipier;
      this.Shadow.vLocation = this.vLocation;
      if (this.animator != null)
        this.Shadow.Draw(spritchbatch, AssetContainer.AnimalSheet, Offset, new Vector2(this.scale * this.animator.CurrentScale.X, this.scale * Sengine.ScreenRatioUpwardsMultiplier.Y));
      else
        this.Shadow.Draw(spritchbatch, AssetContainer.AnimalSheet, Offset, new Vector2(this.scale, this.scale * Sengine.ScreenRatioUpwardsMultiplier.Y));
    }

    public Vector2 GetSize(out float OriginX, out float PixelsWide)
    {
      Vector2 drawOrigin1 = this.DrawOrigin;
      float x = (float) this.DrawRect.Width - this.DrawOrigin.X;
      Vector2 drawOrigin2 = this.DrawOrigin;
      float y = Math.Max((float) this.DrawRect.Height, this.DrawOrigin.Y);
      PixelsWide = (float) this.DrawRect.Width;
      OriginX = this.DrawOrigin.X;
      Vector2 vector2 = this.scale * new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height);
      if (this.Head == null)
        return vector2;
      if ((double) Math.Max((float) this.Head.DrawRect.Height, this.Head.DrawOrigin.Y) > (double) y)
        y = Math.Max((float) this.Head.DrawRect.Height, this.Head.DrawOrigin.Y);
      float num = (float) this.Head.DrawRect.Width - this.Head.DrawOrigin.X + this.DrawOrigin.X;
      if ((double) num > (double) x)
        x = num;
      PixelsWide = x;
      return this.scale * new Vector2(x, y);
    }

    public void ScreenSpaceDrawEnemyRenderer(Vector2 Offset) => this.ScreenSpaceDrawEnemyRenderer(Offset, AssetContainer.pointspritebatchTop05);

    public void ScreenSpaceDrawEnemyRenderer(
      Vector2 Offset,
      SpriteBatch spritebatch,
      float ScaleMultipier = 1f,
      float alphaMult = 1f)
    {
      if (this.animator != null)
      {
        this.Rotation = this.animator.CurrentRotation;
        this.Draw(spritebatch, AssetContainer.AnimalSheet, Offset + this.animator.PositionalOffset * this.scale, this.scale * this.animator.CurrentScale * Sengine.ScreenRatioUpwardsMultiplier * ScaleMultipier, this.fAlpha * alphaMult);
        if (this.Head == null)
          return;
        this.Head.Draw(spritebatch, AssetContainer.AnimalSheet, this.vLocation + Offset + this.animator.PositionalOffset * this.scale, this.scale * this.animator.CurrentScale * Sengine.ScreenRatioUpwardsMultiplier * ScaleMultipier, this.fAlpha * alphaMult);
      }
      else
      {
        this.Draw(spritebatch, AssetContainer.AnimalSheet, Offset, this.scale * ScaleMultipier, this.fAlpha * alphaMult);
        if (this.Head == null)
          return;
        this.Head.Draw(spritebatch, AssetContainer.AnimalSheet, Offset, this.scale * ScaleMultipier, this.fAlpha * alphaMult);
      }
    }
  }
}
