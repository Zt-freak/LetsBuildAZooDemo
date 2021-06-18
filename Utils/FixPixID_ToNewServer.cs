// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.FixPixID_ToNewServer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Spring.Comms;
using SpringSocial;
using SpringSocial.CloudSave;
using System.Collections.Generic;
using TRC_Helper;
using TRC_Helper.MessageRenderer.Alert;

namespace TinyZoo.Utils
{
  internal class FixPixID_ToNewServer
  {
    internal static bool IsCheckingCloud;
    internal static string NewPixIDCloudSave;
    private static string OldPIXIDCLoudSave;
    private static int CloudStep;
    private static bool FailedThisSession;
    private static bool HasOldSave;
    private static bool HasNewSave;

    public static void CheckFixPixID(Player player)
    {
      bool HasThisSocial;
      SocialPair thisSocialPair = player.socialmanager.getThisSocialPair(SocialType.Pixona, out HasThisSocial);
      if (!HasThisSocial)
        return;
      player.Stats.NeedToDoPixFix = true;
      player.Stats.OldPixID = thisSocialPair.UID;
      UserData user = player.socialmanager.GetUser();
      user.socialpaircontainer.TryToDeleteThisSocial(SocialType.Pixona);
      List<SocialPair> avaialbleSocialPairs = user.socialpaircontainer.GetAllAvaialbleSocialPairs();
      for (int index = 0; index < avaialbleSocialPairs.Count; ++index)
      {
        if (avaialbleSocialPairs[index].GetThisSocialtype() == MainVariables.ThisGame || avaialbleSocialPairs[index].GetThisSocialtype() == SocialType.Facebook || (avaialbleSocialPairs[index].GetThisSocialtype() == SocialType.GooglePlayServices || avaialbleSocialPairs[index].GetThisSocialtype() == SocialType.IOS_GameCentre))
        {
          avaialbleSocialPairs[index].RegisteredWithServer = false;
          user.HasSubAccountToRegisterOnServer = true;
        }
      }
    }

    internal static void UpdateFixPixID_ToNewServer(Player player)
    {
      if (FixPixID_ToNewServer.FailedThisSession || TinyZoo.Game1.gamestate == GAMESTATE.Settings)
        return;
      if (!FixPixID_ToNewServer.IsCheckingCloud)
      {
        if (!SpringCommManager.Singleton.IsSessionConnected)
          return;
        bool HasThisSocial;
        SocialPair thisSocialPair = player.socialmanager.getThisSocialPair(SocialType.Pixona, out HasThisSocial);
        if (!HasThisSocial)
          return;
        if (player.Stats.OldPixID == thisSocialPair.UID)
        {
          player.Stats.NeedToDoPixFix = false;
          player.OldSaveThisPlayer();
        }
        else
        {
          bool LoadWasStarted;
          CloudSaveManager.TryToLoadFromCloudSave(SocialType.Pixona, out LoadWasStarted, thisSocialPair.UID);
          if (!LoadWasStarted)
            return;
          FixPixID_ToNewServer.CloudStep = 1;
          FixPixID_ToNewServer.IsCheckingCloud = true;
        }
      }
      else
      {
        switch (FixPixID_ToNewServer.CloudStep)
        {
          case 1:
            switch (CloudSaveManager.cloudesavestate)
            {
              case CloudSaveState.LoadingFromCloud:
                if (FixPixID_ToNewServer.CloudStep != 2)
                  return;
                CloudSaveManager.TryToLoadFromCloudSave(SocialType.Pixona, out bool _, player.Stats.OldPixID);
                return;
              case CloudSaveState.Loading_Success:
                FixPixID_ToNewServer.HasNewSave = true;
                FixPixID_ToNewServer.NewPixIDCloudSave = CloudSaveManager.LastLoadedCLoudSave;
                FixPixID_ToNewServer.CloudStep = 2;
                goto case CloudSaveState.LoadingFromCloud;
              case CloudSaveState.Error_StringNullOrEmpty:
                FixPixID_ToNewServer.CloudStep = 2;
                goto case CloudSaveState.LoadingFromCloud;
              default:
                FixPixID_ToNewServer.IsCheckingCloud = false;
                FixPixID_ToNewServer.FailedThisSession = true;
                goto case CloudSaveState.LoadingFromCloud;
            }
          case 2:
            switch (CloudSaveManager.cloudesavestate)
            {
              case CloudSaveState.LoadingFromCloud:
                return;
              case CloudSaveState.Loading_Success:
                FixPixID_ToNewServer.HasOldSave = true;
                FixPixID_ToNewServer.OldPIXIDCLoudSave = CloudSaveManager.LastLoadedCLoudSave;
                FixPixID_ToNewServer.CloudStep = 3;
                return;
              case CloudSaveState.Error_StringNullOrEmpty:
                FixPixID_ToNewServer.CloudStep = 3;
                return;
              default:
                FixPixID_ToNewServer.IsCheckingCloud = false;
                FixPixID_ToNewServer.FailedThisSession = true;
                return;
            }
          case 3:
            SocialPair thisSocialPair1 = player.socialmanager.getThisSocialPair(SocialType.Pixona, out bool _);
            if (FixPixID_ToNewServer.HasOldSave && !FixPixID_ToNewServer.HasNewSave)
            {
              FixPixID_ToNewServer.CloudStep = 4;
              CloudSaveManager.TryToSaveToCloud(SocialType.Pixona, out bool _, thisSocialPair1.UID, FixPixID_ToNewServer.OldPIXIDCLoudSave);
              break;
            }
            if (FixPixID_ToNewServer.HasNewSave && FixPixID_ToNewServer.HasOldSave)
            {
              FixPixID_ToNewServer.IsCheckingCloud = false;
              player.Stats.NeedToDoPixFix = false;
              TRC_Main.CreateAlert(AlertType.TextBlock_BodyHeading, "We have detected an error", "You have two saves on the cloud, please use the contact button in the settings to message us, and we can help you recover the other save. If there is no issue with your current save, do upload your current save into the cloud.");
              break;
            }
            if (FixPixID_ToNewServer.HasNewSave && !FixPixID_ToNewServer.HasOldSave)
            {
              FixPixID_ToNewServer.IsCheckingCloud = false;
              player.Stats.NeedToDoPixFix = false;
              break;
            }
            if (FixPixID_ToNewServer.HasOldSave || FixPixID_ToNewServer.HasNewSave)
              break;
            FixPixID_ToNewServer.IsCheckingCloud = false;
            player.Stats.NeedToDoPixFix = false;
            break;
          case 4:
            if (CloudSaveManager.cloudesavestate == CloudSaveState.Saving_SaveSuccess)
            {
              FixPixID_ToNewServer.IsCheckingCloud = false;
              player.Stats.NeedToDoPixFix = false;
              break;
            }
            if (CloudSaveManager.cloudesavestate == CloudSaveState.SavingToCloud)
              break;
            FixPixID_ToNewServer.IsCheckingCloud = false;
            FixPixID_ToNewServer.FailedThisSession = true;
            break;
        }
      }
    }
  }
}
