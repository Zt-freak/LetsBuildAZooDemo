// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Animal_Data.AnimatedPerchInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Z_Animal_Data.Enrich;

namespace TinyZoo.Z_Animal_Data
{
  internal class AnimatedPerchInfo
  {
    public bool StartFastForHighStriker;
    private WeightStrikeSetByRotation[] weightsets;

    public AnimatedPerchInfo(bool IsFlipRender)
    {
      if (IsFlipRender)
        this.weightsets = new WeightStrikeSetByRotation[3];
      else
        this.weightsets = new WeightStrikeSetByRotation[4];
    }

    public void SetUpWeightStrike(int Rotation, int WeightClass, Rectangle DrawRect, int Frames)
    {
      if (this.weightsets[Rotation] == null)
        this.weightsets[Rotation] = new WeightStrikeSetByRotation();
      this.weightsets[Rotation].AddWeightClass(WeightClass, DrawRect, Frames);
    }

    public WeightStrikeSetByRotation GetThisRotation(int Rotation) => Rotation == 3 && this.weightsets.Length == 4 ? this.weightsets[2] : this.weightsets[Rotation];
  }
}
