// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.NotificationBubble.NotificationBubbleManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_HUD.Z_Notification.NotificationBubble
{
  internal class NotificationBubbleManager
  {
    private Queue<NotificationBubbleInfo> bubblequeue;
    private List<NotificationBubblePopUp> popups;
    private UIScaleHelper uiscale;
    private int popupindex;
    private float basescale;
    private int previndex;
    private static int popupspoolsize = 2;
    private static NotificationBubbleManager instance = (NotificationBubbleManager) null;

    public static NotificationBubbleManager Instance
    {
      get
      {
        if (NotificationBubbleManager.instance == null)
          NotificationBubbleManager.instance = new NotificationBubbleManager();
        return NotificationBubbleManager.instance;
      }
    }

    internal static void QuickAddNotification(
      float ValueBefore,
      float ValueAfter,
      BubbleMainType bubbletype)
    {
      if (Math.Round((double) ValueBefore) == Math.Round((double) ValueAfter))
        return;
      int num = (int) (Math.Round((double) ValueAfter) - Math.Round((double) ValueBefore));
      string str = "Facilities ";
      switch (bubbletype)
      {
        case BubbleMainType.Animals:
          str = "Animal Rating ";
          break;
        case BubbleMainType.Publicity:
          str = "Publicity ";
          break;
        case BubbleMainType.Deco:
          str = "Decoration ";
          break;
      }
      string heading_;
      string bodytext_;
      if (num > 0)
      {
        heading_ = str + "Up";
        bodytext_ = "+" + (object) num;
      }
      else
      {
        heading_ = str + "Down";
        bodytext_ = string.Concat((object) num);
      }
      NotificationBubbleManager.Instance.AddNotificationBubbleToQueue(new NotificationBubbleInfo(heading_, bodytext_));
    }

    private NotificationBubbleManager()
    {
      this.bubblequeue = new Queue<NotificationBubbleInfo>();
      this.popups = new List<NotificationBubblePopUp>();
      this.basescale = Z_GameFlags.GetBaseScaleForUI();
      this.uiscale = new UIScaleHelper(this.basescale);
      this.popupindex = 0;
      this.previndex = NotificationBubbleManager.popupspoolsize - 1;
      for (int index = 0; index < NotificationBubbleManager.popupspoolsize; ++index)
        this.popups.Add((NotificationBubblePopUp) null);
    }

    public void AddNotificationBubbleToQueue(NotificationBubbleInfo info) => this.bubblequeue.Enqueue(info);

    private void SendPopUp()
    {
      if (this.bubblequeue.Count <= 0)
        return;
      NotificationBubblePopUp popup = this.popups[this.previndex];
      if (popup != null && !popup.exiting)
        return;
      this.popups[this.popupindex] = new NotificationBubblePopUp(this.bubblequeue.Dequeue(), this.basescale);
      this.popups[this.popupindex].location.X = (float) (0.5 * (double) this.popups[this.popupindex].GetSize().X + 0.5 * (double) this.uiscale.DefaultBuffer.X);
      this.popups[this.popupindex].location.Y = (float) ((double) Sengine.ReferenceScreenRes.Y - 0.5 * (double) this.popups[this.popupindex].GetSize().Y - 0.5 * (double) this.uiscale.DefaultBuffer.Y);
      this.previndex = this.popupindex;
      this.popupindex = (this.popupindex + 1) % NotificationBubbleManager.popupspoolsize;
    }

    public bool UpdateNotificationBubbleManager(Player player, float DeltaTime)
    {
      bool flag = false;
      if (this.bubblequeue.Count > 0)
        this.SendPopUp();
      for (int index = 0; index < this.popups.Count; ++index)
      {
        if (this.popups[index] != null && this.popups[index].UpdateNotificationBubblePopUp(player, Vector2.Zero, DeltaTime))
          this.popups[index] = (NotificationBubblePopUp) null;
      }
      return flag;
    }

    public void DrawNotificationBubbleManager()
    {
      foreach (NotificationBubblePopUp popup in this.popups)
        popup?.DrawNotificationBubblePopUp(AssetContainer.pointspritebatchTop05, Vector2.Zero);
    }
  }
}
