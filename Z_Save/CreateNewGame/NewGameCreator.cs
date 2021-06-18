// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Save.CreateNewGame.NewGameCreator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.Utils;
using TinyZoo.Audio;
using TinyZoo.Z_OverWorld.PathFinding_Nodes;

namespace TinyZoo.Z_Save.CreateNewGame
{
  internal class NewGameCreator
  {
    internal static void CreateNewGame(ref Player player)
    {
      GFX_Resolution gfxresolution = player.Stats.gfxresolution;
      double sfxVolume = (double) SoundEffectsManager.SFXVolume;
      float musicVol = MusicManager.MusicVol;
      player = new Player(0);
      player.Stats.gfxresolution = gfxresolution;
      SoundEffectsManager.SFXVolume = (float) sfxVolume;
      MusicManager.MusicVol = musicVol;
      FeatureFlags.BlockAllUI = true;
      Z_GameFlags.HasStartedFirstDay = false;
      FeatureFlags.BlockBuyLand = true;
      Z_GameFlags.ScrubForSaleSigns = true;
      FeatureFlags.FlashBuildFromNotificationTrack = true;
      FeatureFlags.FlashTrade = true;
      Z_GameFlags.TopBarIsBlockedForTutorial = true;
      PathFindingManager.CreateDirectory();
      ShopNavigation.ResetShopNavigation();
    }
  }
}
