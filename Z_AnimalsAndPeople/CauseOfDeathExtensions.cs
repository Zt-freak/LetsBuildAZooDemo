// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.CauseOfDeathExtensions
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.Z_AnimalsAndPeople
{
  internal static class CauseOfDeathExtensions
  {
    public static string GetName(this CauseOfDeath cause) => cause.ToString();

    public static string GetDescription(this CauseOfDeath cause)
    {
      switch (cause)
      {
        case CauseOfDeath.Hunger:
          return "starved to death";
        case CauseOfDeath.KilledForFood:
          return "was slaughtered for food";
        case CauseOfDeath.OldAge:
          return "passed peacefully of old age";
        case CauseOfDeath.euthanized:
          return "has been put to sleep";
        case CauseOfDeath.KilledInAnimalFight:
          return "died a warrior's death in the ring";
        default:
          return "died of unknown causes";
      }
    }
  }
}
