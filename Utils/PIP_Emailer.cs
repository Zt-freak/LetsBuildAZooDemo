// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.PIP_Emailer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Spring.Comms;
using SpringSocial;

namespace TinyZoo.Utils
{
  internal class PIP_Emailer
  {
    public static void SendEmail(Player player, bool IsGDPR)
    {
      string TextBody = "Hi Springloaded, \n I have something I'd like to ask about this game...";
      string Heading = "Prison Planet - Enquiry";
      if (IsGDPR)
      {
        TextBody = "";
        Heading = "Prison Planet Data Deletion Request";
      }
      UserData user = player.socialmanager.GetUser();
      bool HasThisSocial;
      SocialPair thisSocialPair1 = user.socialpaircontainer.GetThisSocialPair(SocialType.Pix_FriendCode, out HasThisSocial);
      if (HasThisSocial)
        TextBody = TextBody + "\n FriendCode: " + thisSocialPair1.UID;
      SocialPair thisSocialPair2 = user.socialpaircontainer.GetThisSocialPair(SocialType.Pixona, out HasThisSocial);
      if (HasThisSocial)
        TextBody = TextBody + "\n PixUID: " + thisSocialPair2.UID;
      SocialPair thisSocialPair3 = user.socialpaircontainer.GetThisSocialPair(MainVariables.ThisGame, out HasThisSocial);
      if (HasThisSocial)
        TextBody = TextBody + "\n PIPID: " + thisSocialPair3.UID;
      SocialPair thisSocialPair4 = user.socialpaircontainer.GetThisSocialPair(SocialType.IOS_GameCentre, out HasThisSocial);
      if (HasThisSocial)
        TextBody = TextBody + "\n Game Center: " + thisSocialPair4.UID;
      SocialPair thisSocialPair5 = user.socialpaircontainer.GetThisSocialPair(SocialType.GooglePlayServices, out HasThisSocial);
      if (HasThisSocial)
        TextBody = TextBody + "\n Google Play: " + thisSocialPair5.UID;
      if (player.Stats.OldPixID != null && player.Stats.OldPixID.Length > 0)
        TextBody = TextBody + "\n OG PixID: " + player.Stats.OldPixID;
      if (IsGDPR)
        TextBody += "I would like to remove all of my data from your servers, or request access to all data (if any) you have saved related to my account. NOTE - PLEASE SPECIFY YOUR REQUEST WHEN EMAILING US, We will respond as soon as possible";
      SEngine.Email_Sender.SendEmail(Heading, TextBody, "support@springloadedsoftware.com");
    }
  }
}
