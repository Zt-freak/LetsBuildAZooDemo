// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.Corpse.Corpse_Calculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.Z_BalanceSystems.Animals.Corpse
{
  internal class Corpse_Calculator
  {
    internal static void Cal_Corpses(PrisonZone prisonzone)
    {
      for (int index = 0; index < prisonzone.prisonercontainer.prisoners.Count; ++index)
      {
        int num = prisonzone.prisonercontainer.prisoners[index].IsDead ? 1 : 0;
      }
    }
  }
}
