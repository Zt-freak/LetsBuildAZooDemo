// Decompiled with JetBrains decompiler
// Type: TinyZoo.PeopleNames
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.Localization;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;

namespace TinyZoo
{
  internal class PeopleNames
  {
    internal static string GetName(
      bool Male,
      out int NameStringIndex,
      AnimalType thisenemy,
      int ForceThis = -1)
    {
      string str;
      if (thisenemy <= AnimalType.None)
      {
        str = PetNames.GetName(out NameStringIndex, !Male, ForceThis);
      }
      else
      {
        if (thisenemy == AnimalType.Leeloo)
        {
          NameStringIndex = 0;
          return "Element";
        }
        if (thisenemy == AnimalType.Riddick)
        {
          NameStringIndex = 0;
          return "Vin";
        }
        if (Male)
        {
          switch (PlayerStats.language)
          {
            case Language.Japanese:
              str = JapMaleNames.GetMaleName(out NameStringIndex, ForceThis);
              break;
            case Language.Korean:
              str = KoreanMaleNames.GetMaleName(out NameStringIndex, ForceThis);
              break;
            default:
              str = MaleNames.GetMaleName(out NameStringIndex, ForceThis);
              break;
          }
        }
        else
        {
          switch (PlayerStats.language)
          {
            case Language.Japanese:
              str = JapFemaleNames.GetName(out NameStringIndex, ForceThis);
              break;
            case Language.Korean:
              str = KoreanFemaleNames.GetName(out NameStringIndex, ForceThis);
              break;
            default:
              str = FemalNames.GetName(out NameStringIndex, ForceThis);
              break;
          }
        }
      }
      return char.ToUpper(str[0]).ToString() + str.Substring(1).ToLower();
    }
  }
}
