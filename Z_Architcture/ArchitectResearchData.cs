// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Architcture.ArchitectResearchData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Architcture.RData;

namespace TinyZoo.Z_Architcture
{
  internal class ArchitectResearchData
  {
    private static ResearchSet[] researchsets;

    internal static ResearchSet GetResearchSet(CATEGORYTYPE categorytype)
    {
      if (ArchitectResearchData.researchsets == null)
        ArchitectResearchData.researchsets = new ResearchSet[14];
      if (ArchitectResearchData.researchsets[(int) categorytype] == null)
      {
        ArchitectResearchData.researchsets[(int) categorytype] = new ResearchSet();
        List<TILETYPE> entriesInThisCategory = CategoryData.GetEntriesInThisCategory(categorytype);
        int num1 = 0;
        int num2 = 0;
        int _COLUMN = 0;
        for (int index = 0; index < entriesInThisCategory.Count; ++index)
        {
          bool flag = false;
          if (ArchitectResearchData.IsThisUnlockedAtTheStart(entriesInThisCategory[index]))
            flag = true;
          int _TotalDaysToUnlock = num2 * 4;
          switch (num2)
          {
            case 0:
              _TotalDaysToUnlock = 1;
              break;
            case 1:
              _TotalDaysToUnlock = 3;
              break;
            case 2:
              _TotalDaysToUnlock = 6;
              break;
          }
          int num3 = num1 % 4;
          int _Row = 0;
          switch (num3)
          {
            case 0:
            case 3:
              ArchitectResearchData.researchsets[(int) categorytype].entries.Add(new ResearchEntry(_TotalDaysToUnlock, entriesInThisCategory[index], _Row, _COLUMN));
              if (_Row == 0 || _Row == 1)
                ++_COLUMN;
              if (!flag)
                ++num2;
              ++num1;
              continue;
            case 1:
              _Row = -1;
              goto case 0;
            default:
              _Row = 1;
              goto case 0;
          }
        }
      }
      return ArchitectResearchData.researchsets[(int) categorytype];
    }

    internal static bool IsThisUnlockedAtTheStart(TILETYPE tiletype) => tiletype == TILETYPE.SmallGiftShop || tiletype == TILETYPE.SnacksVendingMachine || (tiletype == TILETYPE.StoreRoom || tiletype == TILETYPE.ArchitectOffice);
  }
}
