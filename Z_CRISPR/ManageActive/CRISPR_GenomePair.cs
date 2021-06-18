// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CRISPR.ManageActive.CRISPR_GenomePair
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.CRISPR;
using TinyZoo.Z_BreedResult.VariantDiscovered;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_CRISPR.ManageActive
{
  internal class CRISPR_GenomePair
  {
    public Vector2 location;
    private AnimalInFrame[] animals;
    private float totalHeight;
    private DNAIcon dnaIcon;
    private ZGenericText headingObj;

    public CRISPR_GenomePair(CrisprActiveBreed breed, float BaseScale)
    {
      this.totalHeight = 0.0f;
      Vector2 upwardsMultiplier = Sengine.ScreenRatioUpwardsMultiplier;
      this.headingObj = new ZGenericText("Genome Pair: ".ToUpper(), BaseScale, _UseOnePointFiveFont: true);
      float y = this.headingObj.GetSize().Y;
      this.headingObj.vLocation.Y = this.totalHeight + y * 0.5f;
      this.totalHeight += y;
      float num = 25f * BaseScale;
      this.animals = new AnimalInFrame[2];
      this.animals[0] = new AnimalInFrame(breed.genomeOne, AnimalType.None, TargetSize: (50f * BaseScale), FrameEdgeBuffer: (6f * BaseScale), BaseScale: BaseScale);
      this.animals[1] = new AnimalInFrame(breed.genomeTwo, AnimalType.None, TargetSize: (50f * BaseScale), FrameEdgeBuffer: (6f * BaseScale), BaseScale: BaseScale);
      this.animals[0].Location.X -= num + this.animals[0].FrameVSCALE.X * 0.5f;
      this.animals[1].Location.X += num + this.animals[1].FrameVSCALE.X * 0.5f;
      this.animals[0].Location.Y += this.totalHeight + this.animals[0].FrameVSCALE.Y * 0.5f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.animals[1].Location.Y = this.animals[0].Location.Y;
      this.totalHeight += this.animals[0].FrameVSCALE.Y * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.dnaIcon = new DNAIcon(BaseScale);
      this.dnaIcon.SetUpSimpleAnimation();
      this.dnaIcon.SetDrawOriginToCentre();
      this.dnaIcon.vLocation.Y = this.animals[0].Location.Y;
      this.dnaIcon.vLocation.X = (float) (((double) this.animals[1].Location.X + (double) this.animals[0].Location.X) * 0.5);
    }

    public float GetHeight() => this.totalHeight;

    public void UpdateCRISPR_GenomePair(float DeltaTime) => this.dnaIcon.UpdateDNAIconAnimation(DeltaTime);

    public void DrawCRISPR_GenomePair(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.animals.Length; ++index)
        this.animals[index].DrawAnimalInFrame(offset, spriteBatch);
      this.dnaIcon.DrawDNAIcon(offset, spriteBatch);
      this.headingObj.DrawZGenericText(offset, spriteBatch);
    }
  }
}
