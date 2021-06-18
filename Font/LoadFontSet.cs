// Decompiled with JetBrains decompiler
// Type: TinyZoo.Font.LoadFontSet
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework.Content;
using SEngine;
using SEngine.Localization;
using TinyZoo.Utils;

namespace TinyZoo.Font
{
  internal class LoadFontSet
  {
    internal static void LoadAndGenLanguages(ContentManager contentManager)
    {
    }

    internal static void LoadLanguage(Language language, bool useThreading = false)
    {
      FontLoader.LoadLanguage(Language.English, AssetContainer._springFont, useThreading);
      FontLoader.LoadLanguage(Language.English, AssetContainer.roundaboutFont, useThreading);
      FontLoader.LoadLanguage(Language.English, AssetContainer.SpringFontX1AndHalf, useThreading);
      FontLoader.LoadLanguage(Language.English, AssetContainer.PixelNumWithBlackOutline, useThreading);
      FontLoader.LoadLanguage(Language.English, AssetContainer.SinglePixelFontX1AndHalf, useThreading);
    }

    internal static void AddFonts(SpringFont springFont, LoadFontSet.FontType type)
    {
      switch (type)
      {
        case LoadFontSet.FontType.PixelFont:
          FontSet_English fontSetEnglish = new FontSet_English();
          springFont.AddFontBase((FontBase) fontSetEnglish);
          FontSet_Hiragana_Hiragana hiraganaHiragana = new FontSet_Hiragana_Hiragana();
          springFont.AddFontBase((FontBase) hiraganaHiragana);
          LoadFontSet.LoadCJKCharacters(springFont);
          LoadFontSet.LoadRussianCharacters(springFont);
          LoadFontSet.LoadKoreanHangulCharacters(springFont);
          break;
        case LoadFontSet.FontType.RoundaboutFont:
          FontSet_Roundabout_English roundaboutEnglish = new FontSet_Roundabout_English();
          springFont.AddFontBase((FontBase) roundaboutEnglish);
          break;
        case LoadFontSet.FontType.SpringFontX1AndHalf:
          FontSet_BiggerPixel_English biggerPixelEnglish = new FontSet_BiggerPixel_English();
          springFont.AddFontBase((FontBase) biggerPixelEnglish);
          break;
        case LoadFontSet.FontType.PixelNumWithBlackOutline:
          FontSet_PixelNumWithBlackOutline_English blackOutlineEnglish = new FontSet_PixelNumWithBlackOutline_English();
          springFont.AddFontBase((FontBase) blackOutlineEnglish);
          break;
        case LoadFontSet.FontType.SinglePixelFontX1AndHalf:
          SinglePixelFontX1AndHalf_English x1AndHalfEnglish = new SinglePixelFontX1AndHalf_English();
          springFont.AddFontBase((FontBase) x1AndHalfEnglish);
          break;
      }
    }

    internal static void LoadCJKCharacters(SpringFont springFont)
    {
      FontSet_JapanesePunctuation_CJK japanesePunctuationCjk = new FontSet_JapanesePunctuation_CJK();
      springFont.AddFontBase((FontBase) japanesePunctuationCjk);
      FontSet_JapaneseRoman_CJK japaneseRomanCjk = new FontSet_JapaneseRoman_CJK();
      springFont.AddFontBase((FontBase) japaneseRomanCjk);
      FontSet_Kanji_CJK_01 fontSetKanjiCjk01 = new FontSet_Kanji_CJK_01();
      springFont.AddFontBase((FontBase) fontSetKanjiCjk01);
      FontSet_Kanji_CJK_02 fontSetKanjiCjk02 = new FontSet_Kanji_CJK_02();
      springFont.AddFontBase((FontBase) fontSetKanjiCjk02);
      FontSet_Kanji_CJK_03 fontSetKanjiCjk03 = new FontSet_Kanji_CJK_03();
      springFont.AddFontBase((FontBase) fontSetKanjiCjk03);
      FontSet_Kanji_CJK_04 fontSetKanjiCjk04 = new FontSet_Kanji_CJK_04();
      springFont.AddFontBase((FontBase) fontSetKanjiCjk04);
      FontSet_Kanji_CJK_05 fontSetKanjiCjk05 = new FontSet_Kanji_CJK_05();
      springFont.AddFontBase((FontBase) fontSetKanjiCjk05);
      FontSet_Kanji_CJK_06 fontSetKanjiCjk06 = new FontSet_Kanji_CJK_06();
      springFont.AddFontBase((FontBase) fontSetKanjiCjk06);
      FontSet_Kanji_CJK_07 fontSetKanjiCjk07 = new FontSet_Kanji_CJK_07();
      springFont.AddFontBase((FontBase) fontSetKanjiCjk07);
      FontSet_Kanji_CJK_08 fontSetKanjiCjk08 = new FontSet_Kanji_CJK_08();
      springFont.AddFontBase((FontBase) fontSetKanjiCjk08);
    }

    internal static void LoadRussianCharacters(SpringFont springFont)
    {
      FontSet_RussianWide_Russian russianWideRussian = new FontSet_RussianWide_Russian();
      springFont.AddFontBase((FontBase) russianWideRussian);
    }

    internal static void LoadKoreanHangulCharacters(SpringFont springFont)
    {
      FontSet_Korean_1_CJK fontSetKorean1Cjk = new FontSet_Korean_1_CJK();
      springFont.AddFontBase((FontBase) fontSetKorean1Cjk);
      FontSet_Korean_2_CJK fontSetKorean2Cjk = new FontSet_Korean_2_CJK();
      springFont.AddFontBase((FontBase) fontSetKorean2Cjk);
      FontSet_Korean_3_CJK fontSetKorean3Cjk = new FontSet_Korean_3_CJK();
      springFont.AddFontBase((FontBase) fontSetKorean3Cjk);
      FontSet_Korean_4_CJK fontSetKorean4Cjk = new FontSet_Korean_4_CJK();
      springFont.AddFontBase((FontBase) fontSetKorean4Cjk);
    }

    internal static bool CheckIfLanguageLocalized(Language lang) => LanguageInformation.GetSupportedLanguages().Contains(lang);

    internal static void LoadEnglish()
    {
      if (AssetContainer.LoadedFonts[0])
        return;
      FontLoader.LoadLanguage(Language.English, AssetContainer.springFont);
      AssetContainer.LoadedFonts[0] = true;
    }

    internal static void LoadJapanese()
    {
      if (AssetContainer.LoadedFonts[7])
        return;
      AssetContainer.LoadedFonts[7] = true;
    }

    public enum FontType
    {
      PixelFont,
      CleanFont,
      RoundaboutFont,
      SpringFontX1AndHalf,
      PixelNumWithBlackOutline,
      SinglePixelFontX1AndHalf,
      None,
    }
  }
}
