// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.AchievementSetUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SpringSocial;
using System.Collections.Generic;

namespace TinyZoo.Utils
{
  internal class AchievementSetUp
  {
    public static void SetUpAchievements(SocialManagerMain socialmanager)
    {
      List<string> AchievementTitles = new List<string>();
      for (int index = 0; index < 13; ++index)
        AchievementTitles.Add("NULL");
      List<string> LeaderBoards = new List<string>();
      AchievementTitles[0] = "CaptureAlien2";
      AchievementTitles[1] = "CaptureAlien3";
      AchievementTitles[2] = "CaptureAlien10";
      AchievementTitles[3] = "CaptureAlien20";
      AchievementTitles[4] = "CaptureAlien30";
      AchievementTitles[5] = "CaptureAlien40";
      AchievementTitles[6] = "CaptureAlien50";
      AchievementTitles[7] = "Have400Prisoners";
      AchievementTitles[8] = "DiscoverTheHoldingCell";
      AchievementTitles[9] = "Build5UniqueCellBlockTypes";
      AchievementTitles[10] = "Build10UniqueCellBlockTypes";
      AchievementTitles[11] = "Secret_KillAPrisoner";
      AchievementTitles[12] = "SecretReviveAPrisoner";
      for (int index = 0; index < 13; ++index)
      {
        int num = AchievementTitles[index] == "NULL" ? 1 : 0;
      }
      socialmanager.InitializeSteam();
      socialmanager.Achievements.InitializeSteamAchievementsAndLeaderBoard(AchievementTitles, LeaderBoards);
    }
  }
}
