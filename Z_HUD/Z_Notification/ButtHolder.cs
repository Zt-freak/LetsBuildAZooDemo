// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.ButtHolder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI.OveWorldUI;
using TinyZoo.OverWorld;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_HUD.Z_Notification
{
  internal class ButtHolder
  {
    private OWCategoryButton Shortcutbutt;
    private LerpHandler_Float lerper;
    private static float VertcalGap = 50f;
    public NotificationCategory notificationcat;
    private float Start;
    private Vector2 offset;
    public List<NotificationPackage> notificationpackages;
    private GameObject AlertCountFrame;
    private Queue<Z_NotificationAnnounce> announcelist;
    private Z_NotificationAnnounce notifannounce;
    public bool dirty;
    private float NumberFontScale;
    private int alertcount;

    public ButtHolder(NotificationCategory _notificationcat)
    {
      this.AlertCountFrame = new GameObject();
      this.AlertCountFrame.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.AlertCountFrame.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      this.notificationpackages = new List<NotificationPackage>();
      this.notificationcat = _notificationcat;
      this.dirty = false;
      this.NumberFontScale = Z_GameFlags.GetBaseScaleForUI();
      switch (_notificationcat)
      {
        case NotificationCategory.Animals:
          this.Shortcutbutt = new OWCategoryButton(OverworldButtons.AlertAnimals);
          break;
        case NotificationCategory.Staff:
          this.Shortcutbutt = new OWCategoryButton(OverworldButtons.AlertStaff);
          break;
        case NotificationCategory.Customers:
          this.Shortcutbutt = new OWCategoryButton(OverworldButtons.AlertEmergency);
          break;
        case NotificationCategory.Facilities:
          this.Shortcutbutt = new OWCategoryButton(OverworldButtons.AlertBuilding);
          break;
        case NotificationCategory.Quests:
          this.Shortcutbutt = new OWCategoryButton(OverworldButtons.AlertQuest);
          break;
      }
      this.Start = 100f;
      this.Shortcutbutt.Location = new Vector2(OWCategoryButton.SizeBTN * 0.5f, 100f);
      this.AlertCountFrame.scale = this.Shortcutbutt.Icon.scale * 1.5f;
      this.AlertCountFrame.SetAllColours(1f, 1f, 1f);
      this.AlertCountFrame.DrawRect = new Rectangle(409, 107, 20, 15);
      this.AlertCountFrame.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      this.announcelist = new Queue<Z_NotificationAnnounce>();
      this.lerper = new LerpHandler_Float();
    }

    public List<NotificationPackage> GetAllNotificationsOfThisType(
      Z_NotificationType nottype)
    {
      List<NotificationPackage> notificationPackageList = new List<NotificationPackage>();
      for (int index = 0; index < this.notificationpackages.Count; ++index)
      {
        if (this.notificationpackages[index].notificationtype == nottype)
          notificationPackageList.Add(this.notificationpackages[index]);
      }
      return notificationPackageList;
    }

    public List<NotificationInfo> GetNotificationTypesAndCounts()
    {
      List<NotificationInfo> notificationInfoList = new List<NotificationInfo>();
      for (int index1 = this.notificationpackages.Count - 1; index1 > -1; --index1)
      {
        bool flag = false;
        int index2 = -1;
        for (int index3 = 0; index3 < notificationInfoList.Count; ++index3)
        {
          if (notificationInfoList[index3].notificationType == this.notificationpackages[index1].notificationtype)
          {
            index2 = index3;
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          notificationInfoList.Add(new NotificationInfo(this.notificationpackages[index1].notificationtype));
          index2 = notificationInfoList.Count - 1;
        }
        ++notificationInfoList[index2].NumberOfNotificatonsOfThisType;
      }
      return notificationInfoList;
    }

    public bool RemoveThis(Z_NotificationType notification, int AnimalOrPenUID = -1)
    {
      bool flag = false;
      for (int index = this.notificationpackages.Count - 1; index > -1; --index)
      {
        if (this.notificationpackages[index].notificationtype == notification)
        {
          if (AnimalOrPenUID > -1)
          {
            if (this.notificationpackages[index].AnimalOrPenUID == AnimalOrPenUID)
            {
              this.notificationpackages.RemoveAt(index);
              flag = true;
              this.dirty = true;
            }
          }
          else
          {
            flag = true;
            this.notificationpackages.RemoveAt(index);
            this.dirty = true;
          }
        }
      }
      return flag;
    }

    public bool HasThisNotification(Z_NotificationType notification)
    {
      for (int index = this.notificationpackages.Count - 1; index > -1; --index)
      {
        if (this.notificationpackages[index].notificationtype == notification)
          return true;
      }
      return false;
    }

    public void ScrubThisAnimalFromAllNotifications(int AnimalUID)
    {
      for (int index = this.notificationpackages.Count - 1; index > -1; --index)
      {
        if (this.notificationpackages[index].AnimalOrPenUID == AnimalUID)
        {
          this.notificationpackages.RemoveAt(index);
          this.dirty = true;
        }
      }
    }

    public void TryAndRemoveSickAnimal(int UID)
    {
      for (int index = this.notificationpackages.Count - 1; index > -1; --index)
      {
        if (this.notificationpackages[index].notificationtype == Z_NotificationType.A_AnimalSick && this.notificationpackages[index].AnimalOrPenUID == UID)
        {
          this.notificationpackages.RemoveAt(index);
          this.dirty = true;
        }
      }
    }

    public void TryAndScrubAnimalDuplicates(NotificationPackage notpackage)
    {
      if (notpackage.notificationtype == Z_NotificationType.A_AnimalHunger)
      {
        for (int index = this.notificationpackages.Count - 1; index > -1; --index)
        {
          if ((this.notificationpackages[index].notificationtype == Z_NotificationType.A_NoWater || this.notificationpackages[index].notificationtype == Z_NotificationType.A_NoEnrichment || this.notificationpackages[index].notificationtype == Z_NotificationType.A_AnimalHunger) && this.notificationpackages[index].notificationtype == notpackage.notificationtype)
          {
            this.notificationpackages.RemoveAt(index);
            this.dirty = true;
          }
          else if (this.notificationpackages[index].AnimalOrPenUID == notpackage.AnimalOrPenUID && this.notificationpackages[index].notificationtype == notpackage.notificationtype)
          {
            this.notificationpackages.RemoveAt(index);
            this.dirty = true;
          }
        }
      }
      else
      {
        if (notpackage.notificationtype != Z_NotificationType.A_CRISPR_HybridBirth)
          return;
        for (int index = this.notificationpackages.Count - 1; index > -1; --index)
        {
          if (this.notificationpackages[index].notificationtype == notpackage.notificationtype && this.notificationpackages[index].AnimalOrPenUID == notpackage.AnimalOrPenUID)
          {
            this.notificationpackages.RemoveAt(index);
            this.dirty = true;
          }
        }
      }
    }

    public void TryAndScrubQuestDuplicates(NotificationPackage notpackage)
    {
      if (notpackage.notificationtype != Z_NotificationType.Q_QuestComplete)
        return;
      for (int index = this.notificationpackages.Count - 1; index > -1; --index)
      {
        if (this.notificationpackages[index].questDesc.heroquesttype == notpackage.questDesc.heroquesttype && this.notificationpackages[index].notificationtype == notpackage.notificationtype)
        {
          this.notificationpackages.RemoveAt(index);
          this.dirty = true;
        }
      }
    }

    public void RandomizeNotificatonPackages()
    {
    }

    public void AddNotification(NotificationPackage notpackage)
    {
      this.notificationpackages.Add(notpackage);
      this.dirty = true;
      if (notpackage.AlertStatus == NotificationAlertStatus.Tick)
        return;
      bool flag = false;
      foreach (Z_NotificationAnnounce notificationAnnounce in this.announcelist)
      {
        if (notificationAnnounce.package.notificationtype == notpackage.notificationtype)
        {
          flag = true;
          break;
        }
      }
      if (flag || notpackage.AlertStatus == NotificationAlertStatus.Tick)
        return;
      Z_NotificationAnnounce notificationAnnounce1 = new Z_NotificationAnnounce(Z_GameFlags.GetBaseScaleForUI(), notpackage);
      notificationAnnounce1.location.X = (float) (this.AlertCountFrame.DrawRect.Width + 5);
      notificationAnnounce1.location.X += 0.5f * notificationAnnounce1.GetSize().X;
      this.announcelist.Enqueue(notificationAnnounce1);
    }

    public void HandleNotification(
      List<NotificationPackage> notificationpackages,
      out bool remove,
      Player player)
    {
      remove = false;
      if (notificationpackages.Count == 1)
      {
        OverWorldManager.zoopopupHolder.CreateZooPopUps(notificationpackages, out remove, player);
      }
      else
      {
        if (notificationpackages.Count <= 1)
          return;
        OverWorldManager.zoopopupHolder.CreateZooPopUps(notificationpackages, out remove, player);
      }
    }

    public Vector2 GetButtSize() => this.Shortcutbutt.GetSize();

    public Vector2 GetButtLocation() => this.Shortcutbutt.Location;

    public bool UpdateButtHolder(ref int ActiveBeforeThis, float DeltaTime, Player player)
    {
      bool flag1 = false;
      if ((double) this.lerper.TargetValue != (double) ActiveBeforeThis)
        this.lerper.SetLerp(false, 0.0f, (float) ActiveBeforeThis, 3f, true);
      ButtHolder.VertcalGap = 50f;
      this.Shortcutbutt.Location.Y = this.Start + (float) ActiveBeforeThis * ButtHolder.VertcalGap;
      if ((double) this.lerper.Value == 0.0 && this.notificationpackages.Count > 0 && (this.Shortcutbutt.UpdateOWCategoryButton(DeltaTime, player) && this.notificationpackages.Count > 0))
        flag1 = true;
      if (this.notifannounce == null)
      {
        if (this.announcelist.Count > 0)
          this.notifannounce = this.announcelist.Dequeue();
      }
      else
      {
        bool kill;
        bool flag2 = this.notifannounce.UpdateZ_NotificationAnnounce(player, this.Shortcutbutt.LastDrawLocation + this.Shortcutbutt.Icon.vLocation, DeltaTime, out kill);
        if (flag2)
        {
          bool remove;
          this.HandleNotification(new List<NotificationPackage>()
          {
            this.notifannounce.package
          }, out remove, player);
          if (remove)
            this.notificationpackages.Remove(this.notifannounce.package);
        }
        if (kill | flag2)
          this.notifannounce = (Z_NotificationAnnounce) null;
      }
      if (this.notificationpackages.Count > 0)
        ++ActiveBeforeThis;
      this.alertcount = 0;
      for (int index = 0; index < 30; ++index)
      {
        foreach (NotificationPackage notificationpackage in this.notificationpackages)
        {
          if (notificationpackage.notificationtype == (Z_NotificationType) index && (notificationpackage.AlertStatus == NotificationAlertStatus.Exclamation || notificationpackage.AlertStatus == NotificationAlertStatus.Danger_Worst || (notificationpackage.AlertStatus == NotificationAlertStatus.Special_Heart || notificationpackage.AlertStatus == NotificationAlertStatus.Special_Star)))
          {
            ++this.alertcount;
            break;
          }
        }
      }
      return flag1;
    }

    public void CheckNotifications(Player player)
    {
      for (int index = this.notificationpackages.Count - 1; index > -1; --index)
      {
        if (this.notificationpackages[index].notificationtype == Z_NotificationType.C_Population_BuyThing_GenericStore)
        {
          if (Z_NotificationManager.MadeABuilding && player.shopstatus.HasShops())
            this.notificationpackages.RemoveAt(index);
        }
        else if (this.notificationpackages[index].notificationtype == Z_NotificationType.Dep_Population_OpenTheZoo && (double) Z_GameFlags.DayTimer > 0.0)
          this.notificationpackages.RemoveAt(index);
      }
    }

    public void DrawButtHolder(float XLOX)
    {
      if (this.notificationpackages.Count <= 0)
        return;
      this.offset.X = XLOX;
      Vector2 vector2 = this.Shortcutbutt.LastDrawLocation + this.Shortcutbutt.Icon.vLocation;
      if (this.notifannounce != null)
        this.notifannounce.DrawZ_NotificationAnnounce(vector2, AssetContainer.pointspritebatch03);
      this.AlertCountFrame.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, vector2);
      string stringToDraw = string.Concat((object) this.alertcount);
      if (this.alertcount > 99)
        stringToDraw = "99+";
      TextFunctions.DrawTextWithDropShadow(stringToDraw, 0.5f * Z_GameFlags.GetBaseScaleForUI(), vector2 + new Vector2(16f, -8f), Color.White, 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03, false);
      this.Shortcutbutt.DrawOWCategoryButton(this.offset);
    }
  }
}
