// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.NotificationInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_Notification;

namespace TinyZoo.Z_HUD.Z_Notification.NotificationRibbon
{
  internal class NotificationInfo
  {
    public Z_NotificationType notificationType;
    public int NumberOfNotificatonsOfThisType;

    public NotificationInfo(Z_NotificationType _ThieNotificationType) => this.notificationType = _ThieNotificationType;
  }
}
