// Decompiled with JetBrains decompiler
// Type: TinyZoo.FontSet_Korean_1_CJK
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;

namespace TinyZoo
{
  internal class FontSet_Korean_1_CJK : FontBase
  {
    public override void CreateFontSet(
      Dictionary<char, SpringFontCharacter> FontDictionary)
    {
      // ISSUE: The method is too long to display (51273 instructions)
    }

    public FontSet_Korean_1_CJK()
    {
      this.CharacterSet = CharSet.CJK;
      this.TextureFileName = "Fonts/KoreanFont_pg01";
      this.startIndex = 44032;
      this.endIndex = 47047;
    }
  }
}
