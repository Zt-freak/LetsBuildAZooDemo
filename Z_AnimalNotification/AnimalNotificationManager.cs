// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalNotification.AnimalNotificationManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_HUD.PointAtThings;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_AnimalNotification
{
  internal class AnimalNotificationManager
  {
    private AnimalNotificationPanel popup;
    private List<NotificationPackage> notifications;
    private AnimalNotificationType notificationtype;
    private static int num = 1;

    public AnimalNotificationManager(
      Player player,
      AnimalNotificationType notificationType,
      List<NotificationPackage> notifications_ = null)
    {
      this.Initialize(player, notificationType, (List<PrisonerInfo>) null, notifications_);
    }

    public AnimalNotificationManager(
      Player player,
      AnimalNotificationType notificationType,
      PrisonerInfo info,
      List<NotificationPackage> notifications_ = null)
    {
      this.Initialize(player, notificationType, new List<PrisonerInfo>()
      {
        info
      }, notifications_);
    }

    public AnimalNotificationManager(
      Player player,
      AnimalNotificationType notificationType,
      List<PrisonerInfo> infolist,
      List<NotificationPackage> notifications_ = null)
    {
      this.Initialize(player, notificationType, infolist, notifications_);
    }

    public AnimalNotificationManager(
      Player player,
      AnimalNotificationType notificationType,
      List<AnimalRenderDescriptor> hybridAnimals,
      List<NotificationPackage> notifications_ = null)
    {
      this.Initialize(player, notificationType, (List<PrisonerInfo>) null, notifications_, hybridAnimals);
    }

    private void Initialize(
      Player player,
      AnimalNotificationType notificationType,
      List<PrisonerInfo> infolist,
      List<NotificationPackage> notifications_,
      List<AnimalRenderDescriptor> hybridAnimal = null)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.notifications = notifications_;
      this.notificationtype = notificationType;
      int reason_ = 0;
      if (this.notifications != null && this.notifications.Count > 0 && this.notifications[0] != null)
        reason_ = (int) this.notifications[0].reasonfornotification;
      this.popup = new AnimalNotificationPanel(player, notificationType, infolist, baseScaleForUi, reason_, hybridAnimal);
      this.popup.location = Sengine.HalfReferenceScreenRes;
      if (!this.popup.ConstructionFailed)
        return;
      if (this.notificationtype == AnimalNotificationType.CRIPSRBirth)
        Z_NotificationManager.ScrubCrisprBirths = true;
      else
        Z_NotificationManager.RecountAllEvents = true;
    }

    public void SetbreakOutData(int HumanDeaths, int AnimalDeaths, int AnimalsLoose) => this.popup.SetbreakOutData(HumanDeaths, AnimalDeaths, AnimalsLoose);

    public bool CheckMouseOver(Player player) => this.popup.CheckMouseOver(player, Vector2.Zero);

    public bool UpdateAnimalNotificationManager(Player player, float DeltaTime)
    {
      bool track = false;
      bool flag = false | this.popup.UpdateAnimalNotificationPanel(player, DeltaTime, out track);
      if (track)
      {
        switch (this.notificationtype)
        {
          case AnimalNotificationType.Birth:
          case AnimalNotificationType.Death:
          case AnimalNotificationType.Hunger:
          case AnimalNotificationType.Fight:
          case AnimalNotificationType.CRIPSRBirth:
            PointOffScreenManager.AddPointerFromNotification(this.notifications);
            break;
        }
      }
      return flag | track;
    }

    public void DrawAnimalNotificationManager() => this.popup.DrawAnimalNotificationPanel(Vector2.Zero, AssetContainer.pointspritebatchTop05);
  }
}
