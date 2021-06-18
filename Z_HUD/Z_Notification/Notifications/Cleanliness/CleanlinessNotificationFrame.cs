// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.Notifications.Cleanliness.CleanlinessNotificationFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;
using TinyZoo.Z_Notification;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.Z_Notification.Notifications.Cleanliness
{
  internal class CleanlinessNotificationFrame
  {
    public Vector2 location;
    protected float basescale;
    protected UIScaleHelper scalehelper;
    protected CustomerFrame frame;
    protected Vector2 framescale;
    protected Vector2 pad;
    private CleanlinessDescriptionFrame description;
    private CleanlinessJanitorFrame janitor;
    private CleanlinessBinFrame bin;

    public CleanlinessNotificationFrame(
      float basescale_,
      Z_NotificationType notiftype,
      NotificationAlertStatus notifstatus)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.pad = this.scalehelper.DefaultBuffer;
      this.description = new CleanlinessDescriptionFrame(this.basescale, notiftype, notifstatus);
      this.framescale = new Vector2();
      this.framescale += this.description.GetSize();
      if (notifstatus == NotificationAlertStatus.Exclamation || notifstatus == NotificationAlertStatus.Danger_Worst)
      {
        switch (notiftype)
        {
          case Z_NotificationType.F_FoodTrash:
            this.janitor = new CleanlinessJanitorFrame(this.basescale, notiftype, notifstatus);
            this.bin = new CleanlinessBinFrame(this.basescale, notiftype, notifstatus);
            this.framescale.Y += this.pad.Y + this.janitor.GetSize().Y;
            this.framescale.Y += this.pad.Y + this.bin.GetSize().Y;
            break;
          case Z_NotificationType.F_VomitTrash:
            this.janitor = new CleanlinessJanitorFrame(this.basescale, notiftype, notifstatus);
            this.framescale.Y += this.pad.Y + this.janitor.GetSize().Y;
            break;
        }
      }
      Vector2 vector2 = -0.5f * this.framescale;
      this.description.location = vector2 + 0.5f * this.description.GetSize();
      vector2.Y += this.description.GetSize().Y + this.pad.Y;
      if (this.janitor != null)
      {
        this.janitor.location = vector2 + 0.5f * this.janitor.GetSize();
        vector2.Y += this.janitor.GetSize().Y + this.pad.Y;
      }
      if (this.bin == null)
        return;
      this.bin.location = vector2 + 0.5f * this.bin.GetSize();
      vector2.Y += this.bin.GetSize().Y + this.pad.Y;
    }

    public Vector2 GetSize() => this.framescale;

    public virtual bool UpdateCleanlinessNotificationFrame(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out bool hirejanitor,
      out bool buildbins)
    {
      offset += this.location;
      bool flag = false;
      hirejanitor = false;
      buildbins = false;
      if (this.janitor != null)
        flag |= this.janitor.UpdateCleanlinessJanitorFrame(player, offset, DeltaTime, out hirejanitor);
      if (this.bin != null)
        flag |= this.bin.UpdateCleanlinessBinFrame(player, offset, DeltaTime, out buildbins);
      return flag;
    }

    public virtual void DrawCleanlinessNotificationFrame(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.description.DrawCleanlinessDescriptionFrame(spritebatch, offset);
      if (this.janitor != null)
        this.janitor.DrawCleanlinessJanitorFrame(spritebatch, offset);
      if (this.bin == null)
        return;
      this.bin.DrawCleanlinessBinFrame(spritebatch, offset);
    }
  }
}
