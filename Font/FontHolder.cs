// Decompiled with JetBrains decompiler
// Type: TinyZoo.Font.FontHolder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework.Content;
using SEngine;
using SEngine.Localization;
using TRC_Helper;

namespace TinyZoo.Font
{
  internal class FontHolder
  {
    internal static void InitializeFonts(ContentManager contentManager, Language lang)
    {
      SpringFontUtil.Init(ref contentManager);
      AssetContainer._springFont = new SpringFont();
      AssetContainer.roundaboutFont = new SpringFont();
      AssetContainer.SpringFontX1AndHalf = new SpringFont();
      AssetContainer.PixelNumWithBlackOutline = new SpringFont();
      AssetContainer.SinglePixelFontX1AndHalf = new SpringFont();
      LoadFontSet.AddFonts(AssetContainer._springFont, LoadFontSet.FontType.PixelFont);
      LoadFontSet.AddFonts(AssetContainer.roundaboutFont, LoadFontSet.FontType.RoundaboutFont);
      LoadFontSet.AddFonts(AssetContainer.SpringFontX1AndHalf, LoadFontSet.FontType.SpringFontX1AndHalf);
      LoadFontSet.AddFonts(AssetContainer.PixelNumWithBlackOutline, LoadFontSet.FontType.PixelNumWithBlackOutline);
      LoadFontSet.AddFonts(AssetContainer.SinglePixelFontX1AndHalf, LoadFontSet.FontType.SinglePixelFontX1AndHalf);
      TRC_Main.SetAssetsOnLoad(AssetContainer._springFont);
      LoadFontSet.LoadLanguage(lang);
    }
  }
}
