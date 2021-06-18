// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.Cohabitation.ThreatEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_BalanceSystems.Animals.Cohabitation
{
  internal class ThreatEntry
  {
    private float PowerAggressionDifferential;
    private float TotalPackSizeMultiplier;
    private AnimalType ThisPredator;
    public int TotalOfThisEnemy;

    public ThreatEntry(
      float _PowerAggressionDifferential,
      float _TotalPackSizeMultiplier,
      AnimalType _ThisPredator,
      int _TotalOfThisEnemy)
    {
      this.PowerAggressionDifferential = _PowerAggressionDifferential;
      this.TotalPackSizeMultiplier = _TotalPackSizeMultiplier;
      this.ThisPredator = _ThisPredator;
      this.TotalOfThisEnemy = _TotalOfThisEnemy;
    }
  }
}
