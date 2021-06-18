// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedResult.VariantDiscovered.VariantMouseoverPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BreedResult.VariantDiscovered
{
  internal class VariantMouseoverPanel
  {
    private Vector2 location;
    private AnimalType type;
    private int variant;
    private AnimalInFrame animalInFrame;
    private float basescale;
    private float framesize = 35f;

    public VariantMouseoverPanel(
      Vector2 location_,
      AnimalType type_,
      int variant_,
      float basescale_)
    {
      this.basescale = basescale_;
      this.location = location_;
      this.type = type_;
      this.variant = variant_;
      this.animalInFrame = new AnimalInFrame(type_, AnimalType.None, variant_, this.framesize * this.basescale, 3f, this.basescale);
    }

    public void DrawVariantMouseoverPanel(Vector2 offset) => this.animalInFrame.DrawAnimalInFrame(offset + this.location);

    public Vector2 GetSize(bool noScreenRatioMult = false)
    {
      Vector2 vector2 = new Vector2(this.framesize * this.basescale);
      if (!noScreenRatioMult)
        vector2 *= Sengine.ScreenRatioUpwardsMultiplier;
      return vector2;
    }
  }
}
