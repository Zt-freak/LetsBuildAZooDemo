// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.RibbonButtons.NotificationAlertIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine.Objects;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.RibbonButtons
{
  internal class NotificationAlertIcon
  {
    private static Rectangle tickrect = new Rectangle(382, 459, 14, 12);
    private static Rectangle exclamrect = new Rectangle(37, 346, 14, 12);
    private static Rectangle dangerrect = new Rectangle(37, 334, 14, 12);
    private static Rectangle specialrect = new Rectangle(37, 322, 14, 12);
    private static Rectangle starrect = new Rectangle(37, 307, 13, 14);
    private static Rectangle boxrect = new Rectangle(295, 114, 13, 13);
    private ZGenericUIDrawObject box;
    private float basescale;
    public Vector2 location;
    private UIScaleHelper uiscale;
    private Vector2 boxsize;
    private static float scalemult = 1.6f;
    private bool flash;
    private bool drawicon;

    public NotificationAlertIcon(float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.box = new ZGenericUIDrawObject(NotificationAlertIcon.tickrect, this.basescale, AssetContainer.SpriteSheet);
      this.flash = false;
    }

    public Vector2 GetSize() => this.uiscale.ScaleVector2(new Vector2((float) NotificationAlertIcon.boxrect.Width, (float) NotificationAlertIcon.boxrect.Height)) * NotificationAlertIcon.scalemult;

    public void ChangeStatus(NotificationAlertStatus status)
    {
      switch (status)
      {
        case NotificationAlertStatus.Tick:
          this.box.SetDrawRect(NotificationAlertIcon.tickrect);
          this.flash = false;
          break;
        case NotificationAlertStatus.Exclamation:
          this.box.SetDrawRect(NotificationAlertIcon.exclamrect);
          this.flash = false;
          break;
        case NotificationAlertStatus.Danger_Worst:
          this.box.SetDrawRect(NotificationAlertIcon.dangerrect);
          this.flash = true;
          break;
        case NotificationAlertStatus.Special_Heart:
          this.box.SetDrawRect(NotificationAlertIcon.specialrect);
          this.flash = false;
          break;
        case NotificationAlertStatus.Special_Star:
          this.box.SetDrawRect(NotificationAlertIcon.starrect);
          this.flash = false;
          break;
        case NotificationAlertStatus.Count:
          this.box.SetDrawRect(Rectangle.Empty);
          this.flash = false;
          break;
        default:
          this.box.SetDrawRect(NotificationAlertIcon.tickrect);
          break;
      }
    }

    public bool UpdateNotificationAlertIcon(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      int num = 0;
      if (!this.flash)
        return num != 0;
      if ((double) FlashingAlpha.Medium.fAlpha > 0.5)
      {
        this.box.Alpha = 1f;
        return num != 0;
      }
      this.box.Alpha = 0.0f;
      return num != 0;
    }

    public void DrawNotificationAlertIcon(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.box.DrawZGenericUIDrawObject(spritebatch, offset);
    }
  }
}
