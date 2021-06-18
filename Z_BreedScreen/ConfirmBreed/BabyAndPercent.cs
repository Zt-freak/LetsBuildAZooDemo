// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ConfirmBreed.BabyAndPercent
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BreedScreen.ConfirmBreed
{
  internal class BabyAndPercent
  {
    private GameObject titleobject;
    private AnimalInFrame animalInFrame;
    public int Percent;
    public Vector2 Location;

    public BabyAndPercent(int Variant, AnimalType animaltype, float BaseScale)
    {
      this.animalInFrame = new AnimalInFrame(animaltype, AnimalType.None, Variant, 25f * BaseScale, 6f * BaseScale, BaseScale);
      this.titleobject = new GameObject();
      this.titleobject.vLocation.Y = -this.animalInFrame.GetSize().Y;
      this.titleobject.scale = BaseScale;
      this.titleobject.SetAllColours(ColourData.Z_Cream);
    }

    public void SetFrameColour(Vector3 colour) => this.animalInFrame.SetFrameColour(colour);

    public Vector2 GetSize() => new Vector2(this.animalInFrame.GetSize().X, this.GetHeight());

    public float GetHeight() => this.animalInFrame.GetSize().Y + this.GetTextOffset();

    public float GetTextOffset() => Math.Abs(this.titleobject.vLocation.Y) - this.animalInFrame.GetSize().Y * 0.5f;

    public void DrawBabyAndPercent(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.animalInFrame.DrawAnimalInFrame(Offset, AssetContainer.pointspritebatchTop05);
      TextFunctions.DrawJustifiedText(this.Percent.ToString() + "%", this.titleobject.scale, Offset + this.titleobject.vLocation, this.titleobject.GetColour(), 1f, AssetContainer.SpringFontX1AndHalf, spritebatch);
    }
  }
}
