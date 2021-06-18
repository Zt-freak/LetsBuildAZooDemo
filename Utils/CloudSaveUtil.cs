// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.CloudSaveUtil
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.Utils
{
  internal class CloudSaveUtil
  {
    internal static Player REF_player;
    internal static bool JustLoadedFromCloud;

    public static void GetCloudSave(out string CloudDescription, out string CloudSaveData)
    {
      CloudSaveData = CloudSaveUtil.REF_player.OldSaveThisPlayer(true);
      CloudDescription = "Hours played: " + CloudSaveUtil.REF_player.playerbehaviour.GetTimePlayedAsStringForCloudSave();
      CloudDescription += "~";
      CloudDescription = CloudDescription + "Aliens Researched: " + (object) (CloudSaveUtil.REF_player.Stats.research.AnimalsResearchedLength() + 1);
      CloudDescription += "~";
      CloudDescription = CloudDescription + "Total CellBlocks: " + (object) CloudSaveUtil.REF_player.prisonlayout.cellblockcontainer.prisonzones.Count;
      CloudDescription += "~";
      CloudDescription = CloudDescription + "Total Aliens Held: " + (object) CloudSaveUtil.REF_player.prisonlayout.cellblockcontainer.GetTotalAliensHeldIncludingDeadAndHoldingCells();
    }

    public static void LoadFromCloud(string CloudSaveData)
    {
      CloudSaveUtil.REF_player.LoadThisPlayerFromCloudSave(CloudSaveData);
      CloudSaveUtil.REF_player.OldSaveThisPlayer();
      Game1.SetNextGameState(GAMESTATE.SplashScreenSetUp);
      CloudSaveUtil.REF_player = (Player) null;
      CloudSaveUtil.JustLoadedFromCloud = true;
      Game1.screenfade.BeginFade(true);
    }
  }
}
