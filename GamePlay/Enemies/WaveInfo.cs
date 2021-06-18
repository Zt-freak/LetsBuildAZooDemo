// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Enemies.WaveInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.GamePlay.Enemies
{
  internal class WaveInfo
  {
    public List<IntakePerson> People;
    public List<PrisonerInfo> animalinfo;
    public CityName CameFromHere = CityName.Count;
    public List<EnemyRenderer> enemyrenderersFromChopper;
    public int OrderUID = -1;

    public WaveInfo(IntakeInfo prisoners)
    {
      this.People = new List<IntakePerson>();
      for (int index = 0; index < prisoners.People.Count; ++index)
        this.People.Add(prisoners.People[index]);
      if (prisoners.CameFromHere == this.CameFromHere)
        return;
      this.CameFromHere = this.CameFromHere == CityName.Count ? prisoners.CameFromHere : throw new Exception("NO WAY - this means you will lose the city where this came from - you need to make a lis of cities, and the animals that are shipping from each one");
    }

    public void SetFromCrate(List<EnemyRenderer> enemyrenderers, int _OrderUID)
    {
      this.CameFromHere = CityName.Count;
      this.OrderUID = _OrderUID;
      this.enemyrenderersFromChopper = enemyrenderers;
    }

    public WaveInfo(List<IntakePerson> _People) => this.People = _People;

    public void AddPrisonerInfo(PrisonerInfo animal)
    {
      animal.DONOTRESETDATA_MovedAnimal = true;
      if (this.People == null)
        this.People = new List<IntakePerson>();
      if (this.animalinfo == null)
        this.animalinfo = new List<PrisonerInfo>();
      this.animalinfo.Add(animal);
      this.People.Add(animal.intakeperson);
    }

    public void MergeIntakeInfo(IntakeInfo prisoners)
    {
      for (int index = 0; index < prisoners.People.Count; ++index)
        this.People.Add(prisoners.People[index]);
    }

    public void RemoveThisPerson(IntakePerson person)
    {
      for (int index = this.People.Count - 1; index > -1; --index)
      {
        if (this.People[index] == person)
          this.People.RemoveAt(index);
      }
    }
  }
}
