// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.Notifications.Cleanliness.CleanlinessNotificationPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_HUD.Z_Notification.Notifications.Cleanliness
{
  internal class CleanlinessNotificationPanel
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private BigBrownPanel panel;
    private Vector2 framescale;
    private Vector2 pad;
    private CleanlinessNotificationFrame notificationframe;

    public CleanlinessNotificationPanel(
      float basescale_,
      Z_NotificationType notiftype,
      NotificationAlertStatus notifstatus)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.pad = this.scalehelper.DefaultBuffer;
      this.notificationframe = new CleanlinessNotificationFrame(this.basescale, notiftype, notifstatus);
      this.framescale = this.notificationframe.GetSize();
      string addHeaderText = "";
      switch (notiftype)
      {
        case Z_NotificationType.F_FoodTrash:
          addHeaderText = "Trash";
          break;
        case Z_NotificationType.F_VomitTrash:
          addHeaderText = "Barf";
          break;
      }
      this.panel = new BigBrownPanel(this.framescale, true, addHeaderText, this.basescale);
      this.panel.Finalize(this.framescale);
    }

    public bool CheckMouseOver(Player player, Vector2 offset) => this.panel.CheckMouseOver(player, offset + this.location);

    public bool UpdateCleanlinessNotificationPanel(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out bool hirejanitors,
      out bool buildbins)
    {
      offset += this.location;
      this.panel.UpdateDragger(player, ref this.location, DeltaTime);
      return (0 | (this.panel.UpdatePanelCloseButton(player, DeltaTime, offset) ? 1 : 0) | (this.notificationframe.UpdateCleanlinessNotificationFrame(player, offset, DeltaTime, out hirejanitors, out buildbins) ? 1 : 0)) != 0;
    }

    public void DrawCleanlinessNotificationPanel(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.panel.DrawBigBrownPanel(offset, spritebatch);
      this.notificationframe.DrawCleanlinessNotificationFrame(spritebatch, offset);
    }
  }
}
