// Decompiled with JetBrains decompiler
// Type: TinyZoo.FontSet_JapanesePunctuation_CJK
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;

namespace TinyZoo
{
  internal class FontSet_JapanesePunctuation_CJK : FontBase
  {
    public override void CreateFontSet(
      Dictionary<char, SpringFontCharacter> FontDictionary)
    {
      SpringFontCharacter springFontCharacter1 = new SpringFontCharacter('　', new Rectangle(1, 1, 6, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter1.Character, springFontCharacter1);
      SpringFontCharacter springFontCharacter2 = new SpringFontCharacter('、', new Rectangle(8, 1, 5, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter2.Character, springFontCharacter2);
      SpringFontCharacter springFontCharacter3 = new SpringFontCharacter('。', new Rectangle(14, 1, 8, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter3.Character, springFontCharacter3);
      SpringFontCharacter springFontCharacter4 = new SpringFontCharacter('〃', new Rectangle(23, 1, 9, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter4.Character, springFontCharacter4);
      SpringFontCharacter springFontCharacter5 = new SpringFontCharacter('〄', new Rectangle(33, 1, 17, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter5.Character, springFontCharacter5);
      SpringFontCharacter springFontCharacter6 = new SpringFontCharacter('々', new Rectangle(51, 1, 11, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter6.Character, springFontCharacter6);
      SpringFontCharacter springFontCharacter7 = new SpringFontCharacter('〆', new Rectangle(63, 1, 14, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter7.Character, springFontCharacter7);
      SpringFontCharacter springFontCharacter8 = new SpringFontCharacter('〇', new Rectangle(78, 1, 17, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter8.Character, springFontCharacter8);
      SpringFontCharacter springFontCharacter9 = new SpringFontCharacter('〈', new Rectangle(96, 1, 6, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter9.Character, springFontCharacter9);
      SpringFontCharacter springFontCharacter10 = new SpringFontCharacter('〉', new Rectangle(103, 1, 6, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter10.Character, springFontCharacter10);
      SpringFontCharacter springFontCharacter11 = new SpringFontCharacter('《', new Rectangle(110, 1, 10, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter11.Character, springFontCharacter11);
      SpringFontCharacter springFontCharacter12 = new SpringFontCharacter('》', new Rectangle(121, 1, 10, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter12.Character, springFontCharacter12);
      SpringFontCharacter springFontCharacter13 = new SpringFontCharacter('「', new Rectangle(132, 1, 7, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter13.Character, springFontCharacter13);
      SpringFontCharacter springFontCharacter14 = new SpringFontCharacter('」', new Rectangle(140, 1, 8, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter14.Character, springFontCharacter14);
      SpringFontCharacter springFontCharacter15 = new SpringFontCharacter('『', new Rectangle(149, 1, 10, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter15.Character, springFontCharacter15);
      SpringFontCharacter springFontCharacter16 = new SpringFontCharacter('』', new Rectangle(160, 1, 10, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter16.Character, springFontCharacter16);
      SpringFontCharacter springFontCharacter17 = new SpringFontCharacter('【', new Rectangle(1, 22, 7, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter17.Character, springFontCharacter17);
      SpringFontCharacter springFontCharacter18 = new SpringFontCharacter('】', new Rectangle(9, 22, 8, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter18.Character, springFontCharacter18);
      SpringFontCharacter springFontCharacter19 = new SpringFontCharacter('〒', new Rectangle(18, 22, 14, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter19.Character, springFontCharacter19);
      SpringFontCharacter springFontCharacter20 = new SpringFontCharacter('〓', new Rectangle(33, 22, 14, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter20.Character, springFontCharacter20);
      SpringFontCharacter springFontCharacter21 = new SpringFontCharacter('〔', new Rectangle(48, 22, 6, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter21.Character, springFontCharacter21);
      SpringFontCharacter springFontCharacter22 = new SpringFontCharacter('〕', new Rectangle(55, 22, 6, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter22.Character, springFontCharacter22);
      SpringFontCharacter springFontCharacter23 = new SpringFontCharacter('〖', new Rectangle(62, 22, 8, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter23.Character, springFontCharacter23);
      SpringFontCharacter springFontCharacter24 = new SpringFontCharacter('〗', new Rectangle(71, 22, 8, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter24.Character, springFontCharacter24);
      SpringFontCharacter springFontCharacter25 = new SpringFontCharacter('〘', new Rectangle(80, 22, 8, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter25.Character, springFontCharacter25);
      SpringFontCharacter springFontCharacter26 = new SpringFontCharacter('〙', new Rectangle(89, 22, 8, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter26.Character, springFontCharacter26);
      SpringFontCharacter springFontCharacter27 = new SpringFontCharacter('〚', new Rectangle(98, 22, 7, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter27.Character, springFontCharacter27);
      SpringFontCharacter springFontCharacter28 = new SpringFontCharacter('〛', new Rectangle(106, 22, 7, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter28.Character, springFontCharacter28);
      SpringFontCharacter springFontCharacter29 = new SpringFontCharacter('〜', new Rectangle(114, 22, 15, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter29.Character, springFontCharacter29);
      SpringFontCharacter springFontCharacter30 = new SpringFontCharacter('〝', new Rectangle(130, 22, 7, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter30.Character, springFontCharacter30);
      SpringFontCharacter springFontCharacter31 = new SpringFontCharacter('〞', new Rectangle(138, 22, 9, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter31.Character, springFontCharacter31);
      SpringFontCharacter springFontCharacter32 = new SpringFontCharacter('〟', new Rectangle(148, 22, 8, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter32.Character, springFontCharacter32);
      SpringFontCharacter springFontCharacter33 = new SpringFontCharacter('〠', new Rectangle(1, 43, 11, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter33.Character, springFontCharacter33);
      SpringFontCharacter springFontCharacter34 = new SpringFontCharacter('〡', new Rectangle(13, 43, 24, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter34.Character, springFontCharacter34);
      SpringFontCharacter springFontCharacter35 = new SpringFontCharacter('〢', new Rectangle(38, 43, 7, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter35.Character, springFontCharacter35);
      SpringFontCharacter springFontCharacter36 = new SpringFontCharacter('〣', new Rectangle(46, 43, 11, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter36.Character, springFontCharacter36);
      SpringFontCharacter springFontCharacter37 = new SpringFontCharacter('〤', new Rectangle(58, 43, 12, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter37.Character, springFontCharacter37);
      SpringFontCharacter springFontCharacter38 = new SpringFontCharacter('〥', new Rectangle(71, 43, 11, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter38.Character, springFontCharacter38);
      SpringFontCharacter springFontCharacter39 = new SpringFontCharacter('〦', new Rectangle(83, 43, 15, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter39.Character, springFontCharacter39);
      SpringFontCharacter springFontCharacter40 = new SpringFontCharacter('〧', new Rectangle(99, 43, 15, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter40.Character, springFontCharacter40);
      SpringFontCharacter springFontCharacter41 = new SpringFontCharacter('〨', new Rectangle(115, 43, 15, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter41.Character, springFontCharacter41);
      SpringFontCharacter springFontCharacter42 = new SpringFontCharacter('〩', new Rectangle(131, 43, 16, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter42.Character, springFontCharacter42);
      SpringFontCharacter springFontCharacter43 = new SpringFontCharacter('〪', new Rectangle(148, 43, 5, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter43.Character, springFontCharacter43);
      SpringFontCharacter springFontCharacter44 = new SpringFontCharacter('〫', new Rectangle(154, 43, 6, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter44.Character, springFontCharacter44);
      SpringFontCharacter springFontCharacter45 = new SpringFontCharacter('〬', new Rectangle(161, 43, 6, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter45.Character, springFontCharacter45);
      SpringFontCharacter springFontCharacter46 = new SpringFontCharacter('〭', new Rectangle(168, 43, 5, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter46.Character, springFontCharacter46);
      SpringFontCharacter springFontCharacter47 = new SpringFontCharacter('〮', new Rectangle(174, 43, 4, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter47.Character, springFontCharacter47);
      SpringFontCharacter springFontCharacter48 = new SpringFontCharacter('〯', new Rectangle(179, 43, 4, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter48.Character, springFontCharacter48);
      SpringFontCharacter springFontCharacter49 = new SpringFontCharacter('〰', new Rectangle(1, 64, 19, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter49.Character, springFontCharacter49);
      SpringFontCharacter springFontCharacter50 = new SpringFontCharacter('〱', new Rectangle(21, 64, 9, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter50.Character, springFontCharacter50);
      SpringFontCharacter springFontCharacter51 = new SpringFontCharacter('〲', new Rectangle(31, 64, 13, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter51.Character, springFontCharacter51);
      SpringFontCharacter springFontCharacter52 = new SpringFontCharacter('〳', new Rectangle(45, 64, 9, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter52.Character, springFontCharacter52);
      SpringFontCharacter springFontCharacter53 = new SpringFontCharacter('〴', new Rectangle(55, 64, 14, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter53.Character, springFontCharacter53);
      SpringFontCharacter springFontCharacter54 = new SpringFontCharacter('〵', new Rectangle(70, 64, 9, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter54.Character, springFontCharacter54);
      SpringFontCharacter springFontCharacter55 = new SpringFontCharacter('〶', new Rectangle(80, 64, 17, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter55.Character, springFontCharacter55);
      SpringFontCharacter springFontCharacter56 = new SpringFontCharacter('〷', new Rectangle(98, 64, 19, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter56.Character, springFontCharacter56);
      SpringFontCharacter springFontCharacter57 = new SpringFontCharacter('〸', new Rectangle(118, 64, 14, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter57.Character, springFontCharacter57);
      SpringFontCharacter springFontCharacter58 = new SpringFontCharacter('〹', new Rectangle(133, 64, 14, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter58.Character, springFontCharacter58);
      SpringFontCharacter springFontCharacter59 = new SpringFontCharacter('〺', new Rectangle(148, 64, 15, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter59.Character, springFontCharacter59);
      SpringFontCharacter springFontCharacter60 = new SpringFontCharacter('〻', new Rectangle(164, 64, 8, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter60.Character, springFontCharacter60);
      SpringFontCharacter springFontCharacter61 = new SpringFontCharacter('〼', new Rectangle(173, 64, 7, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter61.Character, springFontCharacter61);
      SpringFontCharacter springFontCharacter62 = new SpringFontCharacter('〽', new Rectangle(181, 64, 16, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter62.Character, springFontCharacter62);
      SpringFontCharacter springFontCharacter63 = new SpringFontCharacter('〾', new Rectangle(198, 64, 14, 20), 0.44f, _RightSpacing: -0.5f);
      FontDictionary.Add(springFontCharacter63.Character, springFontCharacter63);
    }

    public FontSet_JapanesePunctuation_CJK()
    {
      this.CharacterSet = CharSet.CJK;
      this.TextureFileName = "jappunctuation";
      this.startIndex = 12288;
      this.endIndex = 12350;
    }
  }
}
