// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.Notifications.Cleanliness.CleanlinessNotificationManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Z_HUD.PointAtThings;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_HUD.Z_Notification.Notifications.Cleanliness
{
  internal class CleanlinessNotificationManager
  {
    private CleanlinessNotificationPanel panel;
    private Z_NotificationType notiftype;
    private NotificationAlertStatus notifstatus;

    public CleanlinessNotificationManager(
      Z_NotificationType notiftype_,
      NotificationAlertStatus notifstatus_)
    {
      this.notiftype = notiftype_;
      this.notifstatus = notifstatus_;
      this.panel = new CleanlinessNotificationPanel(Z_GameFlags.GetBaseScaleForUI(), this.notiftype, this.notifstatus);
      this.panel.location = Sengine.HalfReferenceScreenRes;
    }

    public bool CheckMouseOver(Player player) => this.panel.CheckMouseOver(player, Vector2.Zero);

    public bool UpdateCleanlinessNotificationManager(Player player, float DeltaTime)
    {
      bool hirejanitors;
      bool buildbins;
      int num = 0 | (this.panel.UpdateCleanlinessNotificationPanel(player, Vector2.Zero, DeltaTime, out hirejanitors, out buildbins) ? 1 : 0);
      if (hirejanitors)
        ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.HireFromGate, player);
      if (!buildbins)
        return num != 0;
      ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.BuildABin, player);
      return num != 0;
    }

    public void DrawCleanlinessNotificationManager() => this.panel.DrawCleanlinessNotificationPanel(AssetContainer.pointspritebatchTop05, Vector2.Zero);
  }
}
