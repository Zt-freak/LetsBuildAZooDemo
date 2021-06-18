// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CRISPR.SelectSpecies.CRIPSRresultRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_BalanceSystems.Animals.CRISPR;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_CRISPR.SelectSpecies
{
  internal class CRIPSRresultRow
  {
    public Vector2 location;
    private AnimalInFrame animalOne;
    private AnimalInFrame animalTwo;
    private ZGenericText plusSign;
    private float BaseScale;
    private float Xbuffer;
    private SimpleTextHandler detailsPara;

    public CRIPSRresultRow(float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.Xbuffer = 10f * this.BaseScale;
    }

    public void SetAnimals(AnimalType one, AnimalType two, Player player)
    {
      if (one != AnimalType.None)
      {
        this.animalOne = new AnimalInFrame(one, AnimalType.None, TargetSize: (25f * this.BaseScale), BaseScale: this.BaseScale);
        this.animalOne.Location.X += this.animalOne.FrameVSCALE.X * 0.5f;
        this.animalOne.Location.X += this.Xbuffer;
      }
      else
        this.animalOne = (AnimalInFrame) null;
      if (two != AnimalType.None)
      {
        this.animalTwo = new AnimalInFrame(two, AnimalType.None, TargetSize: (25f * this.BaseScale), BaseScale: this.BaseScale);
        this.animalTwo.Location.X = (float) ((double) this.animalOne.Location.X + (double) this.animalOne.FrameVSCALE.X + (double) this.Xbuffer * 2.0);
        this.detailsPara = new SimpleTextHandler(string.Empty + "Days To Completion: " + (object) CRISPRCalculator.GetDaysForThisCRISPRBreed(one, two), false, 75f / 256f, this.BaseScale, false, false);
        this.detailsPara.AutoCompleteParagraph();
        this.detailsPara.SetAllColours(ColourData.Z_Cream);
        this.detailsPara.Location.X = this.animalTwo.Location.X + this.animalTwo.FrameVSCALE.X * 0.5f + this.Xbuffer;
        this.detailsPara.Location.Y -= this.animalTwo.FrameVSCALE.Y * 0.5f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      }
      else
      {
        this.animalTwo = (AnimalInFrame) null;
        this.detailsPara = (SimpleTextHandler) null;
      }
      if (this.animalOne == null)
        return;
      this.plusSign = new ZGenericText(" +", this.BaseScale, _UseOnePointFiveFont: true);
      this.plusSign.vLocation.X = this.animalOne.Location.X + this.animalOne.FrameVSCALE.X * 0.5f + this.Xbuffer;
    }

    public void UpdateCRIPSRresultRow()
    {
    }

    public void DrawCRIPSRresultRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.animalOne != null)
        this.animalOne.DrawAnimalInFrame(offset, spriteBatch);
      if (this.animalTwo != null)
        this.animalTwo.DrawAnimalInFrame(offset, spriteBatch);
      if (this.plusSign != null)
        this.plusSign.DrawZGenericText(offset, spriteBatch);
      if (this.detailsPara == null)
        return;
      this.detailsPara.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
