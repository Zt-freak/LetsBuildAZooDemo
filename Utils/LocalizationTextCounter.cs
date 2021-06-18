// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.LocalizationTextCounter
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.FileInOut;
using SEngine.Localization;
using System;
using System.Collections.Generic;

namespace TinyZoo.Utils
{
  internal class LocalizationTextCounter
  {
    public static void CountLanguages()
    {
      List<Language> supportedLanguages = LanguageInformation.GetSupportedLanguages();
      int[] numArray = new int[supportedLanguages.Count];
      for (int index = 0; index < supportedLanguages.Count; ++index)
      {
        CSVFileReader csvFileReader = new CSVFileReader();
        int num = -1;
        if (csvFileReader.Read("Localization\\" + SEngine.Localization.Localization.GetLanguageFileSubdirectoryPath(SystemLanguage.LanguageToString[(int) supportedLanguages[index]])))
        {
          if (num == -1)
            csvFileReader.GetRowCount();
          else if (num != csvFileReader.GetRowCount())
            throw new Exception("This language has the wrong number of entries" + (object) supportedLanguages[index]);
          for (int Row = 1; Row < csvFileReader.GetRowCount(); ++Row)
          {
            if (csvFileReader.GetCell(Row, 2).ToUpper() == "MISSING")
            {
              Console.WriteLine(SystemLanguage.LanguageToString[(int) supportedLanguages[index]] + (object) (StringID) (Row - 1));
              ++numArray[index];
            }
          }
        }
      }
      for (int index = 0; index < numArray.Length; ++index)
      {
        if (numArray[index] > 0)
          Console.WriteLine("Missing Lines " + SEngine.Localization.Localization.GetLanguageFileSubdirectoryPath(SystemLanguage.LanguageToString[(int) supportedLanguages[index]]) + ": " + (object) numArray[index]);
      }
    }
  }
}
