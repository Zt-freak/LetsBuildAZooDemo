// Decompiled with JetBrains decompiler
// Type: TinyZoo.AssetContainer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Localization;
using SpringIAP;
using System;
using TinyZoo.Audio;
using TinyZoo.Font;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TinyZoo.Utils;
using TRC_Helper;

namespace TinyZoo
{
  internal class AssetContainer
  {
    internal static SpriteBatch PointBlendBatch02;
    internal static SpriteBatch PointBlendBatch04;
    internal static SpriteBatch PointBlendBatch01;
    internal static SpriteBatch FloorBatch;
    internal static SpriteBatch WF_FloorBatch;
    internal static SpriteBatch WF_FloorBatch2;
    internal static SpriteBatch WF_FloorBatch3;
    internal static SpriteBatch pointspritebatch0;
    internal static SpriteBatch spritebacth;
    internal static SpriteBatch pointspritebatch01;
    internal static SpriteBatch pointspritebatch03;
    internal static SpriteBatch pointspritebatchTop05;
    internal static SpriteBatch spritebatch06;
    internal static SpriteBatch pointspritebatch07Final;
    internal static TextureHolder SpriteSheetSheetHolder = new TextureHolder();
    internal static TextureHolder EnvironmentSheetHolder = new TextureHolder();
    internal static TextureHolder EnvironmentSheet2Holder = new TextureHolder();
    internal static Texture2D SpriteSheet;
    internal static Texture2D LogoSheet;
    internal static Texture2D TitleScreen;
    internal static Texture2D AnimalSheet;
    internal static Texture2D EnvironmentSheet;
    internal static Texture2D EnvironmentSheet2;
    internal static Texture2D Fog;
    internal static Texture2D WorldSheet;
    internal static Texture2D AnimalSheet_white;
    internal static Texture2D TRC_Sprites;
    internal static Texture2D SmearSheet;
    internal static Texture2D UISheet;
    internal static Texture2D SocialUI;
    internal static SpringFont _springFont;
    internal static SpringFont roundaboutFont;
    internal static SpringFont SpringFontX1AndHalf;
    internal static SpringFont PixelNumWithBlackOutline;
    internal static SpringFont SinglePixelFontX1AndHalf;
    internal static bool[] LoadedFonts;
    internal static bool IsLoadingLanguageFont = false;
    internal static Texture2D IPhoneFrame;

    internal static void LoadAssets(ContentManager contentmanager)
    {
      SoundEffectsManager.LoadSplashSound();
      AssetContainer.LogoSheet = contentmanager.Load<Texture2D>("LogoSheet");
      AssetContainer.SocialUI = contentmanager.Load<Texture2D>("SocialUI");
      AssetContainer.SpriteSheet = contentmanager.Load<Texture2D>("SpriteSheet");
      AssetContainer.SpriteSheetSheetHolder.texture = AssetContainer.SpriteSheet;
    }

    internal static SpringFont springFont
    {
      get => AssetContainer._springFont;
      set => throw new Exception("DO NOT SET THIS");
    }

    internal static void LoadAssetsDuringSplash(
      ContentManager contentmanager,
      SpringIAPManager springIAPmanager,
      Player player,
      ref int Index,
      ref bool IsDone)
    {
      if (!CloudSaveUtil.JustLoadedFromCloud)
      {
        switch (Index)
        {
          case 1:
            for (int index = 0; index < 14; ++index)
              CategoryData.GetEntriesInThisCategory((CATEGORYTYPE) index);
            AssetContainer.EnvironmentSheet = contentmanager.Load<Texture2D>("EnvironmentSheet");
            AssetContainer.EnvironmentSheet2 = contentmanager.Load<Texture2D>("EnvironmentSheet2");
            Z_GameFlags.SetDefaults();
            AssetContainer.AnimalSheet = contentmanager.Load<Texture2D>("AnimalSheet");
            AssetContainer.AnimalSheet_white = contentmanager.Load<Texture2D>("AnimalSheet_white");
            AssetContainer.EnvironmentSheetHolder.texture = AssetContainer.EnvironmentSheet;
            AssetContainer.EnvironmentSheet2Holder.texture = AssetContainer.EnvironmentSheet2;
            AssetContainer.SmearSheet = contentmanager.Load<Texture2D>("SmearSheet");
            break;
          case 3:
            AssetContainer.WorldSheet = contentmanager.Load<Texture2D>("WorldSheet");
            GameVariables.SetUpIAPManager(springIAPmanager);
            break;
          case 4:
            AssetContainer.Fog = contentmanager.Load<Texture2D>("Fog");
            break;
          case 5:
            AssetContainer.TRC_Sprites = contentmanager.Load<Texture2D>("TRC_Sprites");
            break;
          case 6:
            AssetContainer.TitleScreen = contentmanager.Load<Texture2D>("TitleScreen");
            AssetContainer.FetchAds();
            break;
          case 7:
            MusicManager.LoadMusic();
            break;
          case 8:
            SoundEffectsManager.LoadSFX();
            break;
        }
      }
      if (Index == 9)
        SEngine.Localization.Localization.SetupLocalization(1079, 12, SystemLanguage.LanguageToString, "Localization");
      if (Index == 10)
      {
        if (LoadFontSet.CheckIfLanguageLocalized(player.Stats.GetLanguage()))
        {
          FontHolder.InitializeFonts(contentmanager, player.Stats.GetLanguage());
          player.Stats.SetLanguage(player.Stats.GetLanguage(), player);
        }
        else
        {
          FontHolder.InitializeFonts(contentmanager, Language.English);
          player.Stats.SetLanguage(Language.English, player);
        }
      }
      if (Index == 11)
        TRC_Main.InitializeTRC_Helper(Game1.WhitePixelRect, player.Stats.GetLanguage());
      if (Index == 12)
        GameVariables.SetUpSpringUI(contentmanager, player.Stats.GetLanguage());
      if (Index == 13)
      {
        Language language = PlayerStats.language;
        PlayerStats.language = Language.Uninitialized;
        player.Stats.SetLanguage(language, player);
      }
      ++Index;
      if (Index <= 13)
        return;
      AssetContainer.UISheet = contentmanager.Load<Texture2D>("UISheet");
      IsDone = true;
    }

    internal static void FetchAds()
    {
    }
  }
}
