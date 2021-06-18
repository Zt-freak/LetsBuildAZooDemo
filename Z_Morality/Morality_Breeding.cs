// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Morality.Morality_Breeding
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_Morality
{
  internal class Morality_Breeding
  {
    internal static void CalcMorality_Breeding(Player player, ref float MoralityScore)
    {
      float num = MathHelper.Clamp((float) (player.Stats.BabiesMovedLate - player.Stats.BabiesMovedEarly) * 0.25f, -MoralityData.MaxBreedingScale, MoralityData.MaxBreedingScale);
      MoralityScore += num;
    }
  }
}
