// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalNotification.AnimalVersusIcons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_AnimalNotification
{
  internal class AnimalVersusIcons
  {
    private AnimalInFrame animalIcon0;
    private AnimalInFrame animalIcon1;
    private Vector2 vsLoc;
    private string name0;
    private string name1;
    private Vector2 nameLoc0;
    private Vector2 nameLoc1;
    private float basescale;
    public Vector2 location;
    private UIScaleHelper uiScale;
    private float nameHeight;
    private float paddingX;

    public AnimalVersusIcons(PrisonerInfo animal0, PrisonerInfo animal1, float basescale_)
    {
      this.basescale = basescale_;
      this.uiScale = new UIScaleHelper(basescale_);
      float TargetSize = 50f * this.basescale;
      this.animalIcon0 = new AnimalInFrame(animal0.intakeperson.animaltype, animal0.intakeperson.HeadType, animal0.intakeperson.CLIndex, TargetSize, BaseScale: (2f * this.basescale));
      this.animalIcon1 = new AnimalInFrame(animal1.intakeperson.animaltype, animal1.intakeperson.HeadType, animal1.intakeperson.CLIndex, TargetSize, BaseScale: (2f * this.basescale));
      float num1 = this.uiScale.ScaleX(20f);
      this.animalIcon0.Location.X = -0.5f * this.animalIcon0.GetSize().X - num1;
      this.animalIcon1.Location.X = 0.5f * this.animalIcon0.GetSize().X + num1;
      this.nameHeight = this.uiScale.ScaleY(AssetContainer.SpringFontX1AndHalf.MeasureString("just need the height").Y);
      float num2 = 0.5f * this.nameHeight;
      this.nameLoc0 = this.animalIcon0.Location;
      this.nameLoc0.Y += 0.5f * this.animalIcon0.GetSize().Y + num2;
      this.nameLoc1 = this.animalIcon1.Location;
      this.nameLoc1.Y += 0.5f * this.animalIcon1.GetSize().Y + num2;
      this.name0 = animal0.intakeperson.Name;
      this.name1 = animal1.intakeperson.Name;
      this.animalIcon0.Location.Y -= num2;
      this.animalIcon1.Location.Y -= num2;
      this.nameLoc0.Y -= num2;
      this.nameLoc1.Y -= num2;
      this.vsLoc.Y -= 0.5f * num2;
    }

    public void DrawAnimalVersusIcons(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      TextFunctions.DrawJustifiedText("vs", this.basescale, offset + this.vsLoc, new Color(ColourData.Z_Cream), 1f, AssetContainer.roundaboutFont, spritebatch);
      this.animalIcon0.DrawAnimalInFrame(offset, spritebatch);
      this.animalIcon1.DrawAnimalInFrame(offset, spritebatch);
      TextFunctions.DrawJustifiedText(this.name0, this.basescale, offset + this.nameLoc0, new Color(ColourData.Z_Cream), 1f, AssetContainer.SpringFontX1AndHalf, spritebatch);
      TextFunctions.DrawJustifiedText(this.name1, this.basescale, offset + this.nameLoc1, new Color(ColourData.Z_Cream), 1f, AssetContainer.SpringFontX1AndHalf, spritebatch);
    }

    public Vector2 GetSize()
    {
      Vector2 vector2;
      vector2.X = (float) ((double) this.animalIcon0.GetSize().X + (double) this.animalIcon1.GetSize().X + 2.0 * (double) this.paddingX);
      vector2.Y = Math.Max(this.animalIcon0.GetSize().Y, this.animalIcon1.GetSize().Y) + this.nameHeight;
      return vector2;
    }
  }
}
