// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.AnimalInFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.NewDiscoveryScreen;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_BreedScreen.BreedChambers;
using TinyZoo.Z_Farms.CropSum.SeedPicker;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_GenericUI
{
  internal class AnimalInFrame
  {
    private AnimalRenderer animalrenderer;
    private GameObjectNineSlice ANimalNineSliveFrame;
    private Vector2 HalfSize;
    private Vector2 AnimalSize;
    public Vector2 Location;
    private float FrameEdge;
    public Vector2 FrameVSCALE;
    public bool MouseOver;
    private GameObjectNineSlice MouseOverFrame;
    private CustomerFrame MouseOverOutlineFrame;
    private Vector2 originalFrameSize;
    private Vector2 mouseOverOutlineExtra;
    private bool showactiveicon;
    private ActiveIcon activeicon;
    private Vector2 activeiconoffset;
    private float basescale;
    private SeedIcon seedcropicon;
    private CorpseRenderer corsperenderer;

    public AnimalInFrame(
      AnimalType animal,
      AnimalType head,
      int Variant = 0,
      float TargetSize = 100f,
      float FrameEdgeBuffer = 6f,
      float BaseScale = -1f,
      int HeadVariant = -1,
      CROPTYPE croptype = CROPTYPE.Count,
      bool DrawGrownPlant = false)
    {
      if ((double) BaseScale == -1.0)
        BaseScale = RenderMath.GetPixelSizeBestMatch(3f);
      this.basescale = BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.FrameVSCALE = new Vector2(TargetSize, TargetSize);
      this.FrameEdge = FrameEdgeBuffer;
      this.ANimalNineSliveFrame = new GameObjectNineSlice(new Rectangle(885, 546, 21, 21), 7);
      this.ANimalNineSliveFrame.scale = BaseScale;
      if (croptype != CROPTYPE.Count)
      {
        this.seedcropicon = new SeedIcon(croptype, this.basescale, TargetSize, DrawGrownPlant);
      }
      else
      {
        this.animalrenderer = new AnimalRenderer(animal, Variant, head, HeadVariant);
        this.animalrenderer.enemy.vLocation = Vector2.Zero;
        this.animalrenderer.enemy.scale = 1f;
        this.MouseOverFrame = new GameObjectNineSlice(new Rectangle(885, 546, 21, 21), 7);
        this.MouseOverFrame.scale = this.ANimalNineSliveFrame.scale;
        this.MouseOverFrame.SetAlpha(0.3f);
        this.mouseOverOutlineExtra = Vector2.One * 2f * (TargetSize / 25f);
        this.MouseOverOutlineFrame = new CustomerFrame((this.FrameVSCALE + this.mouseOverOutlineExtra) * Sengine.ScreenRatioUpwardsMultiplier, ColourData.Z_Cream, BaseScale);
        this.originalFrameSize = this.FrameVSCALE;
        float OriginX;
        float PixelsWide;
        this.AnimalSize = this.animalrenderer.GetSize(out OriginX, out PixelsWide);
        this.animalrenderer.enemy.scale = (TargetSize - this.FrameEdge) / Math.Max(this.AnimalSize.X, this.AnimalSize.Y);
        this.AnimalSize = this.animalrenderer.GetSize(out OriginX, out PixelsWide);
        this.HalfSize = this.AnimalSize;
        this.HalfSize = new Vector2(0.0f, this.HalfSize.Y * -0.5f);
        this.animalrenderer.enemy.vLocation.X += (OriginX - PixelsWide * 0.5f) * this.animalrenderer.enemy.scale;
      }
      this.activeicon = new ActiveIcon(false, 0.5f * this.basescale, true);
      this.activeicon.vLocation = Vector2.Zero;
      this.activeiconoffset = new Vector2(0.4f * this.FrameVSCALE.X, -0.8f * this.FrameVSCALE.Y) * Sengine.ScreenRatioUpwardsMultiplier;
    }

    public void SetDead(AnimalType animal, int Variant)
    {
      this.animalrenderer.enemy.IsDead = true;
      this.corsperenderer = new CorpseRenderer(CauseOfDeath.Count, animal, Variant);
    }

    public void SetShowActiveIcon(bool show) => this.showactiveicon = show;

    public void SetActiveIconState(bool active) => this.activeicon = new ActiveIcon(active, 0.5f * this.basescale, true);

    public void UpdateForAnimation(float DeltaTime) => this.animalrenderer.UpdateAnimal(DeltaTime);

    public bool UpdateForMouseOver(Player player, Vector2 Offset)
    {
      Offset += this.Location;
      this.MouseOver = false;
      if (MathStuff.CheckPointCollision(true, Offset + this.ANimalNineSliveFrame.vLocation, 1f, this.FrameVSCALE.X, this.FrameVSCALE.Y, player.inputmap.PointerLocation))
      {
        this.MouseOver = true;
        return true;
      }
      Vector2 overOutlineExtra = this.mouseOverOutlineExtra;
      if (!MathStuff.CheckPointCollision(true, Offset + this.ANimalNineSliveFrame.vLocation, 1f, this.MouseOverOutlineFrame.VSCale.X, this.MouseOverOutlineFrame.VSCale.Y, player.inputmap.PointerLocation))
        return false;
      this.MouseOver = true;
      return true;
    }

    public void SetFrameBlack() => this.ANimalNineSliveFrame.SetAllColours(0.0f, 0.0f, 0.0f);

    public void SetFrameColour(Vector3 colour, bool isTint = false)
    {
      if (isTint)
      {
        this.ANimalNineSliveFrame.SetAllColours(colour);
      }
      else
      {
        GameObjectNineSlice gameObjectNineSlice = new GameObjectNineSlice(new Rectangle(948, 484, 21, 21), 7);
        gameObjectNineSlice.scale = this.ANimalNineSliveFrame.scale;
        gameObjectNineSlice.SetAllColours(colour);
        this.ANimalNineSliveFrame = gameObjectNineSlice;
      }
    }

    public void SetAnimalGreyedOut(bool IsBlack, bool LowerAlpha = true)
    {
      if (IsBlack)
      {
        if (this.animalrenderer == null)
          return;
        this.animalrenderer.enemy.SetAllColours(Color.Black.ToVector3());
        if (!LowerAlpha)
          return;
        this.animalrenderer.enemy.SetAlpha(0.4f);
      }
      else
      {
        if (this.animalrenderer == null)
          return;
        this.animalrenderer.enemy.SetAllColours(Color.White.ToVector3());
        this.animalrenderer.enemy.SetAlpha(1f);
      }
    }

    public void Darken(bool isDarken)
    {
      if (isDarken)
      {
        Color color;
        if (this.animalrenderer != null)
        {
          this.SetAnimalAlpha(0.7f);
          EnemyRenderer enemy = this.animalrenderer.enemy;
          color = Color.Gray;
          Vector3 vector3 = color.ToVector3();
          enemy.SetAllColours(vector3);
        }
        else if (this.seedcropicon != null)
          this.seedcropicon.Darken();
        color = Color.LightGray;
        this.SetFrameColour(color.ToVector3(), true);
      }
      else
      {
        Color white;
        if (this.animalrenderer != null)
        {
          this.SetAnimalAlpha(1f);
          EnemyRenderer enemy = this.animalrenderer.enemy;
          white = Color.White;
          Vector3 vector3 = white.ToVector3();
          enemy.SetAllColours(vector3);
        }
        else if (this.seedcropicon != null)
        {
          SeedIcon seedcropicon = this.seedcropicon;
          white = Color.White;
          Vector3 vector3 = white.ToVector3();
          seedcropicon.SetAllColours(vector3);
          this.seedcropicon.SetAlpha(1f);
        }
        white = Color.White;
        this.SetFrameColour(white.ToVector3(), true);
      }
    }

    public void SetAnimalAlpha(float alpha)
    {
      if (this.animalrenderer == null)
        return;
      this.animalrenderer.enemy.SetAlpha(alpha);
    }

    public Vector2 GetSize(bool GetSizeOfContentsInsteadOfFrame = false)
    {
      if (GetSizeOfContentsInsteadOfFrame)
      {
        if (this.animalrenderer != null)
          return this.animalrenderer.GetSize(out float _, out float _);
        if (this.seedcropicon != null)
          return this.seedcropicon.GetSize();
      }
      return this.FrameVSCALE * Sengine.ScreenRatioUpwardsMultiplier;
    }

    public void DrawAnimalInFrame(Vector2 Offset) => this.DrawAnimalInFrame(Offset, AssetContainer.pointspritebatchTop05);

    public void DrawPlusMore(
      Vector2 Offset,
      SpriteBatch spritebatch,
      int more,
      float textbaseScale,
      SpringFont font,
      bool DrawMouseOverOutline = false,
      string customString = "",
      bool withoutframe = false)
    {
      if (!withoutframe)
      {
        if (this.MouseOver & DrawMouseOverOutline)
        {
          this.FrameVSCALE = this.originalFrameSize - this.mouseOverOutlineExtra;
          this.MouseOverOutlineFrame.DrawCustomerFrame(Offset + this.Location, spritebatch);
        }
        else
          this.FrameVSCALE = this.originalFrameSize;
        Vector2 vector2 = Offset + -this.HalfSize * Sengine.ScreenRatioUpwardsMultiplier;
        this.ANimalNineSliveFrame.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, this.HalfSize * Sengine.ScreenRatioUpwardsMultiplier + vector2 + this.Location, this.FrameVSCALE * Sengine.ScreenRatioUpwardsMultiplier);
      }
      this.animalrenderer.enemy.vLocation.Y = 0.0f;
      string stringToDraw = "+" + (object) more;
      if (!string.IsNullOrEmpty(customString))
        stringToDraw = customString;
      TextFunctions.DrawJustifiedText(stringToDraw, textbaseScale, Offset + this.Location, new Color(ColourData.Z_DarkTextGray), 1f, font, spritebatch);
    }

    public void DrawAnimalInFrame(
      Vector2 Offset,
      SpriteBatch spritebatch,
      bool DrawMouseOverOutline = false)
    {
      if (this.MouseOver & DrawMouseOverOutline)
      {
        this.FrameVSCALE = this.originalFrameSize - this.mouseOverOutlineExtra;
        this.MouseOverOutlineFrame.DrawCustomerFrame(Offset + this.Location, spritebatch);
      }
      else
        this.FrameVSCALE = this.originalFrameSize;
      if (this.seedcropicon != null)
        this.seedcropicon.DrawSeedIcon(spritebatch, Offset + this.Location);
      else if (this.corsperenderer != null)
      {
        this.corsperenderer.SetDrawOriginToCentre();
        this.corsperenderer.scale = this.animalrenderer.enemy.scale;
        this.corsperenderer.ScreenSpaceDrawCorpseRenderer(Offset + this.Location, spritebatch);
      }
      else
      {
        Offset += -this.HalfSize * Sengine.ScreenRatioUpwardsMultiplier;
        this.animalrenderer.enemy.vLocation.Y = 0.0f;
        this.ANimalNineSliveFrame.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, this.HalfSize * Sengine.ScreenRatioUpwardsMultiplier + Offset + this.Location, this.FrameVSCALE * Sengine.ScreenRatioUpwardsMultiplier);
        this.animalrenderer.ScreenSpaceDraw(Offset + this.Location, spritebatch, false);
        if (!this.showactiveicon)
          return;
        this.activeicon.DrawActiveIcon(spritebatch, Offset + this.Location + this.activeiconoffset);
      }
    }

    public void JustDrawAnimal(
      Vector2 Offset,
      SpriteBatch spritebatch,
      float AlphaMult = 1f,
      float ScaleMultiplier = 1f)
    {
      if (this.corsperenderer != null)
      {
        this.corsperenderer.SetDrawOriginToCentre();
        this.corsperenderer.scale = this.animalrenderer.enemy.scale;
        this.corsperenderer.ScreenSpaceDrawCorpseRenderer(Offset + this.Location, spritebatch);
      }
      else if (this.seedcropicon != null)
      {
        this.seedcropicon.DrawSeedIcon(spritebatch, Offset + this.Location, ScaleMultiplier);
      }
      else
      {
        Offset += -this.HalfSize * Sengine.ScreenRatioUpwardsMultiplier;
        this.animalrenderer.enemy.vLocation.Y = 0.0f;
        this.animalrenderer.ScreenSpaceDraw(Offset + this.Location, spritebatch, false, ScaleMultiplier, AlphaMult);
      }
    }

    public void DrawMouseOver(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      if (!this.MouseOver)
        return;
      Offset += -this.HalfSize * Sengine.ScreenRatioUpwardsMultiplier;
      this.MouseOverFrame.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, this.HalfSize * Sengine.ScreenRatioUpwardsMultiplier + Offset, this.FrameVSCALE * Sengine.ScreenRatioUpwardsMultiplier);
      this.MouseOver = false;
    }
  }
}
