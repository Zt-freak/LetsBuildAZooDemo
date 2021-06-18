// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SaveUtils
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_Save.Header;

namespace TinyZoo
{
  internal class Z_SaveUtils
  {
    private static HeaderData headerdata;
    internal static List<int> AvalableSaves;
    internal static int MaxSaves = 10;

    internal static bool CheckSaveFiles()
    {
      Z_SaveUtils.AvalableSaves = new List<int>();
      for (int index = 1; index < Z_SaveUtils.MaxSaves; ++index)
      {
        if (Reader.DoesFileExist(Player.FileNameForSave() + (object) index))
          Z_SaveUtils.AvalableSaves.Add(index);
      }
      return Z_SaveUtils.AvalableSaves.Count > 0;
    }

    internal static void LoadSaveInfo()
    {
    }

    internal static void CreateSaveInfo()
    {
    }

    internal static HeaderInfo GetHeaderInfo(int SaveSlot)
    {
      if (Z_SaveUtils.headerdata == null)
        Z_SaveUtils.LoadHeader();
      return Z_SaveUtils.headerdata.GetHeaderInfo(SaveSlot);
    }

    internal static void LoadHeader()
    {
      if (Reader.DoesFileExist(Player.FileNameForSave() + "Head"))
        Z_SaveUtils.headerdata = new HeaderData(new Reader(Player.FileNameForSave() + "Head", FlagSettings.SaveIsEncrypted));
      else
        Z_SaveUtils.headerdata = new HeaderData();
    }

    internal static void SaveHeader()
    {
    }
  }
}
