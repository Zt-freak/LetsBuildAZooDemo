// Decompiled with JetBrains decompiler
// Type: TinyZoo.TextSettings
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.Localization;

namespace TinyZoo
{
  internal class TextSettings
  {
    private static float[] LineHeights = new float[12];
    internal static float LineHeightMultiplier = 1f;
    internal static bool LanguageSwitched = false;
    internal static bool SpecialHeightForCJK = false;
    internal static bool SpecialHeight = false;
    internal static bool UseArcadeOrOutlineFont = true;
    internal static float NonArcadeOrOutlineExtraScale = 3.5f;

    internal static void InitLineHeights()
    {
      for (int index = 0; index < 12; ++index)
      {
        switch (index - 7)
        {
          case 0:
            TextSettings.LineHeights[index] = 1.25f;
            break;
          case 1:
            TextSettings.LineHeights[index] = 1.25f;
            break;
          case 2:
            TextSettings.LineHeights[index] = 1.25f;
            break;
          default:
            TextSettings.LineHeights[index] = 1f;
            break;
        }
      }
    }

    internal static void SwitchLanguage(Language language)
    {
      if (TextSettings.LineHeights == null)
        TextSettings.InitLineHeights();
      TextSettings.LineHeightMultiplier = TextSettings.LineHeights[(int) language];
      if (language != Language.English)
      {
        TextSettings.UseArcadeOrOutlineFont = false;
        if (language == Language.Japanese || language == Language.Chinese_Simplified || language == Language.Chinese_Traditional)
        {
          TextSettings.SpecialHeightForCJK = true;
          TextSettings.SpecialHeight = true;
        }
      }
      else
      {
        TextSettings.UseArcadeOrOutlineFont = true;
        TextSettings.SpecialHeightForCJK = false;
        TextSettings.SpecialHeight = false;
      }
      SEngine.Localization.Localization.SetUserLanguage(language);
    }
  }
}
