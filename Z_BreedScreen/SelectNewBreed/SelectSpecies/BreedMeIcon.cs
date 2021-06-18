// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies.BreedMeIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Objects;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_BreedScreen.SelectBreedingPair;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies
{
  internal class BreedMeIcon
  {
    public AnimalInFrame animalInFrame;
    public Vector2 Location;
    public AnimalsForBreedInfo breedinfo;
    private bool NewOptions;
    private MiniChromosone Chromosone;
    private bool ChromosoneOnRight;

    public BreedMeIcon(
      AnimalsForBreedInfo _breedinfo,
      AnimalType animal,
      Player player,
      bool isFemale,
      int Variant = -1,
      bool _IsNew = false,
      float Size_Raw = 25f,
      float BaseScale = -1f,
      bool PositionChromosoneOnRight = false)
    {
      AnimalRenderDescriptor animal1 = new AnimalRenderDescriptor(animal, Variant, _IsFemale: isFemale);
      this.SetUp(_breedinfo, animal1, player, _IsNew, Size_Raw, BaseScale, PositionChromosoneOnRight);
    }

    public BreedMeIcon(
      AnimalRenderDescriptor animal,
      float BaseScale,
      bool PositionChromosoneOnRight = false)
    {
      this.SetUp((AnimalsForBreedInfo) null, animal, (Player) null, BaseScale: BaseScale, PositionChromosoneOnRight: PositionChromosoneOnRight);
    }

    public void SetUp(
      AnimalsForBreedInfo _breedinfo,
      AnimalRenderDescriptor animal,
      Player player,
      bool _IsNew = false,
      float Size_Raw = 25f,
      float BaseScale = -1f,
      bool PositionChromosoneOnRight = false)
    {
      this.ChromosoneOnRight = PositionChromosoneOnRight;
      float num = new UIScaleHelper(BaseScale).GetDefaultXBuffer() * 0.5f;
      this.breedinfo = _breedinfo;
      this.animalInFrame = new AnimalInFrame(animal.bodyAnimalType, animal.headAnimalType, animal.variant, Size_Raw * BaseScale, 6f * BaseScale, BaseScale, animal.headVariant);
      this.NewOptions = this.breedinfo != null && this.breedinfo.HasNewOpportunity(player) > 0 || _IsNew;
      this.Chromosone = new MiniChromosone(animal.IsFemale, BaseScale);
      if (PositionChromosoneOnRight)
        this.Chromosone.vLocation = new Vector2((float) ((double) this.animalInFrame.GetSize().X * 0.5 + (double) num + (double) this.Chromosone.GetSize().X * 0.5), 0.0f);
      else
        this.Chromosone.vLocation = new Vector2((float) (-(double) this.animalInFrame.GetSize().X * 0.5 - (double) num - (double) this.Chromosone.GetSize().X * 0.5), 0.0f);
    }

    public Vector2 GetSize()
    {
      Vector2 size = this.animalInFrame.GetSize();
      return new Vector2(Math.Abs(this.animalInFrame.Location.X - this.Chromosone.vLocation.X) + (float) ((double) this.Chromosone.GetSize().X * 0.5 + (double) size.X * 0.5), size.Y);
    }

    public float GetOffsetToDrawFromLeft() => this.ChromosoneOnRight ? this.animalInFrame.GetSize().X * 0.5f : Math.Abs(this.animalInFrame.Location.X - this.Chromosone.vLocation.X) + this.Chromosone.GetSize().X * 0.5f;

    public void UpdateBreedMeIcon(Player player, Vector2 Offset, float DeltaTime)
    {
      Offset += this.Location;
      this.animalInFrame.UpdateForMouseOver(player, Offset);
    }

    public void DrawBreedMeIcon(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.animalInFrame.DrawAnimalInFrame(Offset, spritebatch);
      if (this.NewOptions)
        TextFunctions.DrawTextWithDropShadow("NEW", RenderMath.GetPixelSizeBestMatch(1f), Offset + new Vector2(25f, -25f), Color.Goldenrod, (float) ((double) FlashingAlpha.Medium.fAlpha * 0.5 + 0.5), AssetContainer.SpringFontX1AndHalf, spritebatch, false, true);
      this.animalInFrame.DrawMouseOver(Offset, spritebatch);
      this.Chromosone.DrawMiniChromosone(Offset, spritebatch);
    }
  }
}
