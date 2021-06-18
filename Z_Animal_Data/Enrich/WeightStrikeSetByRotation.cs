// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Animal_Data.Enrich.WeightStrikeSetByRotation
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;

namespace TinyZoo.Z_Animal_Data.Enrich
{
  internal class WeightStrikeSetByRotation
  {
    public RotationWeightStrike[] strikes;

    public void AddWeightClass(int WeightClass, Rectangle DrawRect, int Frames)
    {
      if (this.strikes == null)
        this.strikes = new RotationWeightStrike[WeightClass + 1];
      else if (this.strikes.Length <= WeightClass)
        throw new Exception("PASS IN THE BIGGEST WEIGHT FCLASS FIRST");
      this.strikes[WeightClass] = new RotationWeightStrike(DrawRect, Frames);
    }
  }
}
