// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.VariableAnimalList
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease
{
  internal class VariableAnimalList
  {
    public CustomerFrame BGframe;
    private List<AnimalInFrame> animals;
    private bool WillDrawPlus;
    private int Skipped;
    private float BaseScale;
    public Vector2 Location;

    public VariableAnimalList(
      List<AnimalType> animalsforlist,
      float _BaseScale,
      float PremultiplierBaseScaleWidth,
      int TotalNonQuestionMarks = -1)
    {
      this.BaseScale = _BaseScale;
      float num1 = 5f;
      float num2 = 40f;
      this.BGframe = new CustomerFrame(new Vector2(PremultiplierBaseScaleWidth, (num2 + num1 + num1) * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y), BaseScale: this.BaseScale, UseTinyRect: true);
      this.animals = new List<AnimalInFrame>();
      for (int index = 0; index < animalsforlist.Count; ++index)
      {
        this.animals.Add(new AnimalInFrame(animalsforlist[index], AnimalType.None, TargetSize: (num2 * this.BaseScale), FrameEdgeBuffer: (5f * this.BaseScale), BaseScale: this.BaseScale));
        this.animals[index].Location.X = (num2 + num1) * this.BaseScale * (float) index;
        this.animals[index].Location.X -= PremultiplierBaseScaleWidth * 0.5f;
        this.animals[index].Location.X += (num2 * 0.5f + num1) * this.BaseScale;
        if ((double) this.animals[index].Location.X >= (double) PremultiplierBaseScaleWidth * 0.5 - ((double) num2 + (double) num1) && index < animalsforlist.Count - 1)
        {
          this.Skipped = animalsforlist.Count - index;
          this.WillDrawPlus = true;
          break;
        }
      }
      this.Location.Y = this.BaseScale * 10f * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public void DrawVariableAnimalList(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.BGframe.DrawCustomerFrame(Offset, spritebatch);
      for (int index = 0; index < this.animals.Count; ++index)
      {
        if (this.WillDrawPlus && index == this.animals.Count - 1)
          this.animals[index].DrawPlusMore(Offset, spritebatch, this.Skipped, this.BaseScale, AssetContainer.springFont);
        else
          this.animals[index].DrawAnimalInFrame(Offset, spritebatch);
      }
    }
  }
}
