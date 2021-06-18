// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Sponsorships
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.sponsor;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;

namespace TinyZoo.PlayerDir
{
  internal class Sponsorships
  {
    private List<CurrentSponsor> sponsoprs;

    public Sponsorships() => this.sponsoprs = new List<CurrentSponsor>();

    public void AddSponsorship(CurrentSponsor newsponsor) => this.sponsoprs.Add(newsponsor);

    public void StartNewDay(Player player)
    {
      int num = 0;
      for (int index = this.sponsoprs.Count - 1; index > -1; --index)
      {
        if (this.sponsoprs[index].DaysRemaining > 0)
        {
          --this.sponsoprs[index].DaysRemaining;
          num += this.sponsoprs[index].Value;
        }
        if (this.sponsoprs[index].DaysRemaining <= 0)
          this.sponsoprs.RemoveAt(index);
      }
      if (num <= 0)
        return;
      Player.financialrecords.RecievedGrant(num);
      NotificationBubbleManager.Instance.AddNotificationBubbleToQueue(new NotificationBubbleInfo(nameof (Sponsorships), "Earnings: $" + (object) num, true));
      player.Stats.GiveCash(num, player);
    }

    public void SaveSponsorships(Writer writer)
    {
      writer.WriteInt("v", this.sponsoprs.Count);
      for (int index = 0; index < this.sponsoprs.Count; ++index)
        this.sponsoprs[index].SaveCurrentSponsor(writer);
    }

    public Sponsorships(Reader reader)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("v", ref _out);
      this.sponsoprs = new List<CurrentSponsor>();
      for (int index = 0; index < _out; ++index)
        this.sponsoprs.Add(new CurrentSponsor(reader));
    }
  }
}
