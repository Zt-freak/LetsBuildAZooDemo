// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.Cohabitation.SimpleThreatPack
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_BalanceSystems.Animals.Cohabitation
{
  internal class SimpleThreatPack
  {
    public AnimalType animaltype;
    public int Total;
    public int TotalBabies;

    public SimpleThreatPack(AnimalType _animaltype)
    {
      this.Total = 1;
      this.animaltype = _animaltype;
    }

    public void UpdateSimpleThreatPack()
    {
    }

    public void DrawSimpleThreatPack()
    {
    }
  }
}
