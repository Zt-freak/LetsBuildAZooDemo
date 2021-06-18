// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.Config
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.IO;

namespace TinyZoo.Utils
{
  internal class Config
  {
    internal static bool StartInSafeMode;

    internal static void loadConfig()
    {
      TrailerDemoFlags.LoadTrailerFlags();
      if (!Reader.DoesFileExist("Config.txt"))
        return;
      StreamReader streamReader = new StreamReader("Config.txt");
      if (streamReader == null)
        return;
      if (streamReader.ReadLine() != "StartSafeMode")
      {
        Console.WriteLine("Config.txt IS CORRUPT");
      }
      else
      {
        string str = streamReader.ReadLine();
        if (str.Length <= 0)
          return;
        Config.StartInSafeMode = str[0] == 't' || str[0] == 'T';
      }
    }
  }
}
