// Decompiled with JetBrains decompiler
// Type: TinyZoo.Audio.MusicManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using SEngine.Content;
using System;

namespace TinyZoo.Audio
{
  internal static class MusicManager
  {
    private static SongTitle SongToPlayNextAfterFade;
    private static SongContainer[] songscontainer;
    private static float fadetime;
    private static bool FadeOut = false;
    private static bool CustomSoundtrack = false;
    private static float OriginalVolume;
    internal static float MusicVol = 0.8f;
    internal static SongTitle CurrentlyPlayingSong = SongTitle.None;
    private static SongTitle LastPlayedOverworldMusic = SongTitle.None;
    private static SongTitle LastPlayedBattleMusic = SongTitle.None;
    internal static bool resumedInterruptedCustomMusic = false;
    private static int checkCounter;
    private static float StartGenericSongTimer;
    private static bool PlaySongUponResuming;

    public static void LoadMusic()
    {
      if (MusicManager.songscontainer == null)
      {
        MusicManager.songscontainer = new SongContainer[23];
        for (int index = 0; index < MusicManager.songscontainer.Length; ++index)
          MusicManager.songscontainer[index] = new SongContainer();
      }
      if (DebugFlags.MusicDisabled)
        return;
      ContentLoadScheduler.AddContent(new ContentLoaderEntry(Game1.MusicMngr, "Music/AC01", ref MusicManager.songscontainer[0]));
      ContentLoadScheduler.AddContent(new ContentLoaderEntry(Game1.MusicMngr, "Music/f_02", ref MusicManager.songscontainer[1]));
      ContentLoadScheduler.AddContent(new ContentLoaderEntry(Game1.MusicMngr, "Music/AC03", ref MusicManager.songscontainer[2]));
      ContentLoadScheduler.AddContent(new ContentLoaderEntry(Game1.MusicMngr, "Music/orch", ref MusicManager.songscontainer[3]));
      ContentLoadScheduler.AddContent(new ContentLoaderEntry(Game1.MusicMngr, "Music/Ambi1", ref MusicManager.songscontainer[4]));
      ContentLoadScheduler.AddContent(new ContentLoaderEntry(Game1.MusicMngr, "Music/ambie2", ref MusicManager.songscontainer[5]));
      ContentLoadScheduler.AddContent(new ContentLoaderEntry(Game1.MusicMngr, "Music/AC_Quiet", ref MusicManager.songscontainer[6]));
      ContentLoadScheduler.AddContent(new ContentLoaderEntry(Game1.MusicMngr, "Music/AC_NC", ref MusicManager.songscontainer[7]));
      ContentLoadScheduler.AddContent(new ContentLoaderEntry(Game1.MusicMngr, "Music/ACousticRth", ref MusicManager.songscontainer[8]));
      ContentLoadScheduler.AddContent(new ContentLoaderEntry(Game1.MusicMngr, "Music/L_an01", ref MusicManager.songscontainer[9]));
      ContentLoadScheduler.AddContent(new ContentLoaderEntry(Game1.MusicMngr, "Music/TrailerV2Fade", ref MusicManager.songscontainer[13]));
      ContentLoadScheduler.AddContent(new ContentLoaderEntry(Game1.MusicMngr, "Music/scream", ref MusicManager.songscontainer[10]));
      ContentLoadScheduler.AddContent(new ContentLoaderEntry(Game1.MusicMngr, "Music/ZooTrailer", ref MusicManager.songscontainer[15]));
      ContentLoadScheduler.AddContent(new ContentLoaderEntry(Game1.MusicMngr, "Music/PhoneTrilogy01", ref MusicManager.songscontainer[11]));
      ContentLoadScheduler.AddContent(new ContentLoaderEntry(Game1.MusicMngr, "Music/PhoneTrilogy02", ref MusicManager.songscontainer[12]));
    }

    public static void LoadThisSong(SongTitle songtitle)
    {
      switch (songtitle)
      {
        case SongTitle.AC01:
          MusicManager.songscontainer[(int) songtitle].song = LoadSongForPlatform.LoadThisSong(Game1.MusicMngr, "Music/AC01");
          break;
        case SongTitle.AC02:
          MusicManager.songscontainer[(int) songtitle].song = LoadSongForPlatform.LoadThisSong(Game1.MusicMngr, "Music/f_02");
          break;
        case SongTitle.AC03:
          MusicManager.songscontainer[(int) songtitle].song = LoadSongForPlatform.LoadThisSong(Game1.MusicMngr, "Music/AC03");
          break;
        case SongTitle.AC04:
          MusicManager.songscontainer[(int) songtitle].song = LoadSongForPlatform.LoadThisSong(Game1.MusicMngr, "Music/orch");
          break;
        case SongTitle.AC05:
          MusicManager.songscontainer[(int) songtitle].song = LoadSongForPlatform.LoadThisSong(Game1.MusicMngr, "Music/Ambi1");
          break;
        case SongTitle.AC06:
          MusicManager.songscontainer[(int) songtitle].song = LoadSongForPlatform.LoadThisSong(Game1.MusicMngr, "Music/ambie2");
          break;
        case SongTitle.AC07:
          MusicManager.songscontainer[(int) songtitle].song = LoadSongForPlatform.LoadThisSong(Game1.MusicMngr, "Music/AC_Quiet");
          break;
        case SongTitle.AC08:
          MusicManager.songscontainer[(int) songtitle].song = LoadSongForPlatform.LoadThisSong(Game1.MusicMngr, "Music/AC_NC");
          break;
        case SongTitle.AC09:
          MusicManager.songscontainer[(int) songtitle].song = LoadSongForPlatform.LoadThisSong(Game1.MusicMngr, "Music/ACousticRth");
          break;
        case SongTitle.AC10:
          MusicManager.songscontainer[(int) songtitle].song = LoadSongForPlatform.LoadThisSong(Game1.MusicMngr, "Music/L_an01");
          break;
        case SongTitle.AC11:
          MusicManager.songscontainer[(int) songtitle].song = LoadSongForPlatform.LoadThisSong(Game1.MusicMngr, "Music/scream");
          break;
        case SongTitle.AC12:
          MusicManager.songscontainer[(int) songtitle].song = LoadSongForPlatform.LoadThisSong(Game1.MusicMngr, "Music/PhoneTrilogy01");
          break;
        case SongTitle.AC13:
          MusicManager.songscontainer[(int) songtitle].song = LoadSongForPlatform.LoadThisSong(Game1.MusicMngr, "Music/PhoneTrilogy02");
          break;
        case SongTitle.TrailerV2Fade:
          MusicManager.songscontainer[(int) songtitle].song = LoadSongForPlatform.LoadThisSong(Game1.MusicMngr, "Music/TrailerV2Fade");
          break;
        case SongTitle.TrailerTheme:
          MusicManager.songscontainer[(int) songtitle].song = LoadSongForPlatform.LoadThisSong(Game1.MusicMngr, "Music/ZooTrailer");
          break;
      }
    }

    public static void playsong(SongTitle SongToPlay, bool restart, bool looping = true)
    {
      looping = false;
      if (DebugFlags.MusicDisabled || MusicManager.IsCustomSoundtrackPlaying)
        return;
      bool flag = false;
      if (MusicManager.CurrentlyPlayingSong != SongToPlay | restart)
      {
        MediaPlayer.Stop();
        flag = true;
        MusicManager.CurrentlyPlayingSong = SongToPlay;
      }
      if (!flag)
        return;
      if (SongToPlay == SongTitle.RandomOverWorldMusic)
      {
        SongTitle songTitle = MusicManager.LastPlayedOverworldMusic;
        while (songTitle == MusicManager.LastPlayedOverworldMusic)
          songTitle = (SongTitle) Game1.Rnd.Next(0, 14);
        int playedOverworldMusic = (int) MusicManager.LastPlayedOverworldMusic;
        SongToPlay = songTitle;
        MusicManager.LastPlayedOverworldMusic = songTitle;
      }
      else if (SongToPlay == SongTitle.TrailerV2Fade)
        MusicManager.LastPlayedOverworldMusic = SongToPlay;
      else if (SongToPlay == SongTitle.RandomBattleMusic)
      {
        SongTitle songTitle = MusicManager.LastPlayedBattleMusic;
        while (songTitle == MusicManager.LastPlayedBattleMusic)
          songTitle = (SongTitle) Game1.Rnd.Next(16, 20);
        SongToPlay = songTitle;
        MusicManager.LastPlayedBattleMusic = songTitle;
      }
      if (SongToPlay >= SongTitle.None)
        return;
      if (MusicManager.songscontainer[(int) SongToPlay].song == (Song) null)
      {
        Console.WriteLine("FORCE LOADING SONG");
        MusicManager.LoadThisSong(SongToPlay);
      }
      Song song = MusicManager.songscontainer[(int) SongToPlay].song;
      if (DebugFlags.MusicDisabled)
        return;
      MediaPlayer.Stop();
      MediaPlayer.IsRepeating = looping;
      MediaPlayer.Volume = MusicManager.MusicVol * 0.5f;
      MediaPlayer.Play(song);
      MusicSequencer.PlayedNewSong(SongToPlay);
    }

    internal static bool IsSongPlaying() => !MusicManager.CustomSoundtrack && MediaPlayer.State == MediaState.Playing;

    public static void BeginFadeOut(float FadeOutTime, SongTitle nextsongtoplay)
    {
      if (MusicManager.CustomSoundtrack)
        return;
      MusicManager.FadeOut = true;
      MusicManager.fadetime = FadeOutTime;
      MusicManager.OriginalVolume = MediaPlayer.Volume;
      MusicManager.SongToPlayNextAfterFade = nextsongtoplay;
    }

    internal static void SetVolumeForOptions(float MusicVol)
    {
      if (MusicManager.CustomSoundtrack || MusicManager.FadeOut)
        return;
      MediaPlayer.Volume = MusicVol;
    }

    internal static bool IsCustomSoundtrackPlaying { get; private set; }

    public static bool CheckForCustomSoundTrack() => MusicManager.IsCustomSoundtrackPlaying;

    public static void ResumedApplication()
    {
    }

    public static void UpdateMusic(float DeltaTime, ref ContentManager MusicMngr)
    {
      if (MusicManager.CustomSoundtrack)
        return;
      MusicSequencer.UpdateMusicSequencer(MusicManager.FadeOut);
      if (!MusicManager.FadeOut)
        return;
      if ((double) (MediaPlayer.Volume = MathHelper.Max(MediaPlayer.Volume - ((double) MusicManager.fadetime != 0.0 ? (float) ((double) MusicManager.OriginalVolume * (double) DeltaTime * (1.0 / (double) MusicManager.fadetime)) : 1f), 0.0f)) != 0.0)
        return;
      MusicManager.FadeOut = false;
      MusicManager.CurrentlyPlayingSong = SongTitle.None;
      MediaPlayer.Stop();
      if (MusicManager.SongToPlayNextAfterFade == SongTitle.None)
        return;
      MediaPlayer.Volume = MusicManager.MusicVol;
      MusicManager.playsong(MusicManager.SongToPlayNextAfterFade, true);
    }
  }
}
