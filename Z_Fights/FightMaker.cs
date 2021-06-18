// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Fights.FightMaker
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.Z_Fights
{
  internal class FightMaker
  {
    internal static bool TryToStartFight(
      Player player,
      int AnimalUID,
      int ExpectedCellBlock,
      bool IsFamilyFight,
      out PrisonerInfo AnimalThatStartedFight)
    {
      if (Z_DebugFlags.IsBetaVersion)
      {
        AnimalThatStartedFight = (PrisonerInfo) null;
        return false;
      }
      PrisonZone thisCellBlock = player.prisonlayout.cellblockcontainer.GetThisCellBlock(ExpectedCellBlock);
      AnimalThatStartedFight = (PrisonerInfo) null;
      if (thisCellBlock != null)
      {
        AnimalThatStartedFight = thisCellBlock.prisonercontainer.GetThisPrisoner(AnimalUID);
        if (AnimalThatStartedFight != null && !AnimalThatStartedFight.IsCurrentlyFighting)
        {
          FightManager fightManager1 = (FightManager) null;
          FightManager fightManager2;
          if (!IsFamilyFight)
          {
            int maxValue = 0;
            for (int index = 0; index < thisCellBlock.prisonercontainer.prisoners.Count; ++index)
            {
              if (!thisCellBlock.prisonercontainer.prisoners[index].IsCurrentlyFighting && !thisCellBlock.prisonercontainer.prisoners[index].IsDead && thisCellBlock.prisonercontainer.prisoners[index].intakeperson.animaltype != AnimalThatStartedFight.intakeperson.animaltype)
                ++maxValue;
            }
            if (maxValue > 0)
            {
              int num = Game1.Rnd.Next(0, maxValue);
              for (int index = 0; index < thisCellBlock.prisonercontainer.prisoners.Count; ++index)
              {
                if (!thisCellBlock.prisonercontainer.prisoners[index].IsCurrentlyFighting && !thisCellBlock.prisonercontainer.prisoners[index].IsDead && thisCellBlock.prisonercontainer.prisoners[index].intakeperson.animaltype != AnimalThatStartedFight.intakeperson.animaltype)
                {
                  --num;
                  if (num <= 0)
                  {
                    fightManager2 = new FightManager(AnimalThatStartedFight, thisCellBlock.prisonercontainer.prisoners[index]);
                    return true;
                  }
                }
              }
            }
          }
          int maxValue1 = 0;
          for (int index = 0; index < thisCellBlock.prisonercontainer.prisoners.Count; ++index)
          {
            if (!thisCellBlock.prisonercontainer.prisoners[index].IsCurrentlyBrokenOut && !thisCellBlock.prisonercontainer.prisoners[index].IsCurrentlyFighting && (!thisCellBlock.prisonercontainer.prisoners[index].IsDead && thisCellBlock.prisonercontainer.prisoners[index].intakeperson.animaltype == AnimalThatStartedFight.intakeperson.animaltype) && AnimalThatStartedFight != thisCellBlock.prisonercontainer.prisoners[index])
              ++maxValue1;
          }
          if (maxValue1 > 0)
          {
            int num = Game1.Rnd.Next(0, maxValue1);
            for (int index = 0; index < thisCellBlock.prisonercontainer.prisoners.Count; ++index)
            {
              if (!thisCellBlock.prisonercontainer.prisoners[index].IsCurrentlyFighting && !thisCellBlock.prisonercontainer.prisoners[index].IsDead && (thisCellBlock.prisonercontainer.prisoners[index].intakeperson.animaltype == AnimalThatStartedFight.intakeperson.animaltype && AnimalThatStartedFight != thisCellBlock.prisonercontainer.prisoners[index]))
              {
                --num;
                if (num <= 0)
                {
                  fightManager2 = new FightManager(AnimalThatStartedFight, thisCellBlock.prisonercontainer.prisoners[index]);
                  return true;
                }
              }
            }
          }
          if (fightManager1 != null)
            return true;
        }
      }
      return false;
    }
  }
}
