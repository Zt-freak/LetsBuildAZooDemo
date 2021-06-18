// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ConfirmBreed.AnimalAndSex
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade.BigAnimalFrame;

namespace TinyZoo.Z_BreedScreen.ConfirmBreed
{
  internal class AnimalAndSex
  {
    private Chromosone Sexicon;
    private AnimalInFrame animalInFrame;
    private GameObjectNineSlice nineslice;
    private Vector2 FrameVSCALE;
    public Vector2 Location;
    private string Heading;
    private GameObject HeadingTextThing;

    public AnimalAndSex(
      int Variant,
      AnimalType animaltype,
      bool IsFemale,
      string _Heading = "",
      float BaseScale = 2f,
      bool IsDead = false)
    {
      this.animalInFrame = new AnimalInFrame(animaltype, AnimalType.None, Variant, BaseScale * 25f, BaseScale: BaseScale);
      if (IsDead)
        this.animalInFrame.SetDead(animaltype, Variant);
      this.Heading = _Heading;
      this.Sexicon = new Chromosone(IsFemale);
      this.Sexicon.scale = BaseScale;
      this.FrameVSCALE = this.animalInFrame.FrameVSCALE * Sengine.ScreenRatioUpwardsMultiplier;
      this.FrameVSCALE.X *= 2f;
      this.FrameVSCALE.X += 5f * BaseScale;
      this.nineslice = new GameObjectNineSlice(new Rectangle(885, 546, 21, 21), 7);
      this.nineslice.scale = BaseScale * 2f;
      this.animalInFrame.Location.X -= BaseScale * 13f;
      this.Sexicon.vLocation.X = this.animalInFrame.Location.X * -1f;
      if (this.Heading.Length > 0)
      {
        this.HeadingTextThing = new GameObject();
        this.HeadingTextThing.vLocation.Y = BaseScale * -10f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.HeadingTextThing.scale = BaseScale;
        this.FrameVSCALE.Y += 10f * BaseScale;
        this.animalInFrame.Location.Y += BaseScale * 5f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.Sexicon.vLocation.Y = this.animalInFrame.Location.Y;
      }
      if (!IsDead)
        return;
      this.Heading = "IS DEAD";
      this.nineslice.SetAllColours(1f, 0.0f, 0.0f);
    }

    public Vector2 GetSize() => this.FrameVSCALE * Sengine.ScreenRatioUpwardsMultiplier;

    public float GetHeight() => this.FrameVSCALE.Y * Sengine.ScreenRatioUpwardsMultiplier.Y;

    public void UpdateAnimalAndSex()
    {
    }

    public void DrawAnimalAndSex(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.nineslice.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, Offset, this.FrameVSCALE);
      this.Sexicon.DrawChromosone(Offset, spritebatch);
      this.animalInFrame.JustDrawAnimal(Offset, spritebatch);
      if (this.HeadingTextThing == null)
        return;
      TextFunctions.DrawJustifiedText(this.Heading, this.HeadingTextThing.scale, this.HeadingTextThing.vLocation + Offset, this.HeadingTextThing.GetColour(), this.HeadingTextThing.fAlpha, AssetContainer.springFont, spritebatch);
    }
  }
}
