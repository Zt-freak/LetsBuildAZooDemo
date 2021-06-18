// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.Cohabitation.AnimalCohabitRelationship
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_BalanceSystems.Animals.Cohabitation
{
  internal class AnimalCohabitRelationship
  {
    public AnimalType aimaltype;
    public bool IsCarnivreThreat;
    public bool IsWeakerCarnivore;
    public float CarnivoreThreatValue;

    public AnimalCohabitRelationship(AnimalType otheranimal) => this.aimaltype = otheranimal;

    public void SetStrongerCarnivore(float ThreatValue)
    {
      this.IsCarnivreThreat = true;
      this.CarnivoreThreatValue = ThreatValue;
    }

    public void SetWeakerCarnivore() => this.IsWeakerCarnivore = true;
  }
}
