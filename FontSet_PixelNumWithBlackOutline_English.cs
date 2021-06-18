// Decompiled with JetBrains decompiler
// Type: TinyZoo.FontSet_PixelNumWithBlackOutline_English
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;

namespace TinyZoo
{
  internal class FontSet_PixelNumWithBlackOutline_English : FontBase
  {
    public override void CreateFontSet(
      Dictionary<char, SpringFontCharacter> FontDictionary)
    {
      SpringFontCharacter springFontCharacter1 = new SpringFontCharacter('0', new Rectangle(7, 11, 8, 11), 1f, _RightSpacing: 0.5f);
      FontDictionary.Add(springFontCharacter1.Character, springFontCharacter1);
      SpringFontCharacter springFontCharacter2 = new SpringFontCharacter('1', new Rectangle(19, 11, 7, 11), 1f, _RightSpacing: 0.5f);
      FontDictionary.Add(springFontCharacter2.Character, springFontCharacter2);
      SpringFontCharacter springFontCharacter3 = new SpringFontCharacter('2', new Rectangle(30, 11, 8, 11), 1f, _RightSpacing: 0.5f);
      FontDictionary.Add(springFontCharacter3.Character, springFontCharacter3);
      SpringFontCharacter springFontCharacter4 = new SpringFontCharacter('3', new Rectangle(42, 11, 8, 11), 1f, _RightSpacing: 0.5f);
      FontDictionary.Add(springFontCharacter4.Character, springFontCharacter4);
      SpringFontCharacter springFontCharacter5 = new SpringFontCharacter('4', new Rectangle(53, 11, 9, 11), 1f, _RightSpacing: 0.5f);
      FontDictionary.Add(springFontCharacter5.Character, springFontCharacter5);
      SpringFontCharacter springFontCharacter6 = new SpringFontCharacter('5', new Rectangle(65, 11, 8, 11), 1f, _RightSpacing: 0.5f);
      FontDictionary.Add(springFontCharacter6.Character, springFontCharacter6);
      SpringFontCharacter springFontCharacter7 = new SpringFontCharacter('6', new Rectangle(76, 11, 9, 11), 1f, _RightSpacing: 0.5f);
      FontDictionary.Add(springFontCharacter7.Character, springFontCharacter7);
      SpringFontCharacter springFontCharacter8 = new SpringFontCharacter('7', new Rectangle(88, 11, 8, 11), 1f, _RightSpacing: 0.5f);
      FontDictionary.Add(springFontCharacter8.Character, springFontCharacter8);
      SpringFontCharacter springFontCharacter9 = new SpringFontCharacter('8', new Rectangle(99, 11, 9, 11), 1f, _RightSpacing: 0.5f);
      FontDictionary.Add(springFontCharacter9.Character, springFontCharacter9);
      SpringFontCharacter springFontCharacter10 = new SpringFontCharacter('9', new Rectangle(111, 11, 9, 11), 1f, _RightSpacing: 0.5f);
      FontDictionary.Add(springFontCharacter10.Character, springFontCharacter10);
    }

    public FontSet_PixelNumWithBlackOutline_English()
    {
      this.CharacterSet = CharSet.English;
      this.TextureFileName = "Fonts/PixelNumWithBlackOutline";
      this.startIndex = 48;
      this.endIndex = 57;
    }
  }
}
