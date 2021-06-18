// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Breeding.RandomBreeding.CheckRandomBreedsOnStartDay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks;

namespace TinyZoo.Z_Breeding.RandomBreeding
{
  internal class CheckRandomBreedsOnStartDay
  {
    internal static void ForceTutorialBreed(Player player, PrisonerContainer prisonercontainer)
    {
      PrisonerInfo prisonerInfo1 = (PrisonerInfo) null;
      PrisonerInfo prisonerInfo2 = (PrisonerInfo) null;
      for (int index = 0; index < prisonercontainer.prisoners.Count; ++index)
      {
        if (prisonercontainer.prisoners[index].intakeperson.animaltype != AnimalType.Rabbit)
          throw new Exception("This tutorial is expecteding 2 rabbits!");
        if (!prisonercontainer.prisoners[index].intakeperson.IsAGirl)
          prisonerInfo1 = prisonercontainer.prisoners[index];
        else
          prisonerInfo2 = prisonercontainer.prisoners[index];
      }
      int Percent = 100;
      int IndexOfLuckyBaby = 1;
      player.breeds.StartBreed(-1, Percent, AnimalType.Rabbit, IndexOfLuckyBaby, player, prisonerInfo1.intakeperson.CLIndex, prisonerInfo2.intakeperson.CLIndex, prisonerInfo2.intakeperson.UID, prisonerInfo1.intakeperson.UID);
      prisonerInfo2.IsPregnant = true;
      if (Z_GameFlags.HasStartedFirstDay)
        return;
      ++player.breeds.Unplannedbreeds[0].DaysLeft;
    }
  }
}
