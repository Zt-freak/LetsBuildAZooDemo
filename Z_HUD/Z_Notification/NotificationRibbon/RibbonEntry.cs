// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.RibbonEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.RibbonButtons;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_HUD.Z_Notification.NotificationRibbon
{
  internal class RibbonEntry
  {
    private static Vector3 alertred = new Vector3(0.8627451f, 0.5058824f, 0.4901961f);
    private static Vector3 exclamyellow = new Vector3(0.6901961f, 0.6117647f, 0.3607843f);
    public static Rectangle mouseoverRect = new Rectangle(985, 219, 26, 26);
    public static Rectangle rect = new Rectangle(985, 188, 26, 26);
    private static float rawWidth = 140f;
    private static Color colour = new Color(ColourData.Z_TextBrown);
    private RibbonButton button;
    private NotificationAlertIcon alertcounter;
    private ZGenericButton dismissButton;
    public Z_NotificationType notiftype;
    public NotificationInfo notificationinfo;
    public NotificationPackage notificationpack;
    private float basescale;
    private UIScaleHelper uiscale;
    private float width;
    private Vector2 rectVscale;
    public Vector2 location;
    public bool tint;
    private GameObject tintRect;
    private bool mouseover;
    private MouseoverHandler mouseoverhandler;
    private ZGenericText text;
    private GameObject darkoverlay;
    private GameObject colouroverlay;
    private Vector2 darkoverlaysize;
    private Vector2 colouroverlaysize;
    private bool drawcolouroverlay;

    internal static int SortRibbonEntries(RibbonEntry a, RibbonEntry b)
    {
      if (a.notiftype > b.notiftype)
        return 1;
      return a.notiftype < b.notiftype ? -1 : 0;
    }

    public RibbonEntry(
      float basescale_,
      NotificationInfo notificationinfo_,
      NotificationPackage notificationpack_ = null,
      bool tint_ = false)
    {
      this.tint = tint_;
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.notificationpack = notificationpack_;
      this.notificationinfo = notificationinfo_;
      this.width = this.uiscale.ScaleX(RibbonEntry.rawWidth);
      if (this.notificationinfo != null)
        this.notiftype = this.notificationinfo.notificationType;
      else if (notificationpack_ != null)
        this.notiftype = this.notificationpack.notificationtype;
      this.text = new ZGenericText(NotificationPackage.GetZ_NotificationTypeToString(this.notiftype), this.basescale);
      this.text.SetAllColours(new Vector3(1f));
      this.button = new RibbonButton(this.basescale, this.notiftype);
      this.alertcounter = new NotificationAlertIcon(this.basescale);
      if (this.notificationpack != null)
        this.dismissButton = new ZGenericButton(this.basescale, RibbonEntry.rect, RibbonEntry.mouseoverRect);
      this.button.location.X = -0.5f * this.width;
      this.button.location.X += 0.5f * this.button.GetSize().X + this.uiscale.GetDefaultXBuffer();
      ZGenericButton dismissButton = this.dismissButton;
      this.rectVscale = new Vector2(this.uiscale.ScaleX(RibbonEntry.rawWidth), this.button.GetSize().Y + 0.5f * this.uiscale.GetDefaultYBuffer());
      this.text.vLocation = this.button.location;
      this.text.vLocation.X += (float) (0.5 * (double) this.button.GetSize().X + 0.5 * (double) this.text.GetSize().X + 0.5 * (double) this.uiscale.DefaultBuffer.X);
      this.alertcounter = new NotificationAlertIcon(this.basescale);
      this.alertcounter.location.X = (float) (0.5 * (double) this.width - 0.5 * (double) this.alertcounter.GetSize().X) - this.uiscale.DefaultBuffer.X;
      this.alertcounter.location.Y += 0.0f;
      this.darkoverlay = new GameObject();
      this.darkoverlay.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.darkoverlay.SetDrawOriginToCentre();
      this.darkoverlay.SetAllColours(new Vector3(0.0f));
      this.darkoverlaysize = this.rectVscale;
      this.darkoverlaysize.X = this.alertcounter.GetSize().X + 2f * this.uiscale.DefaultBuffer.X;
      this.colouroverlay = new GameObject();
      this.colouroverlay.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.colouroverlay.SetDrawOriginToCentre();
      this.colouroverlaysize = this.rectVscale;
      this.drawcolouroverlay = false;
      this.darkoverlay.vLocation = new Vector2();
      this.darkoverlay.vLocation.Y = 0.0f;
      this.darkoverlay.vLocation.X = (float) (0.5 * (double) this.rectVscale.X - 0.5 * (double) this.darkoverlaysize.X);
      this.tintRect = new GameObject();
      this.tintRect.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.tintRect.SetDrawOriginToCentre();
      this.tintRect.SetAllColours(Vector3.Zero);
      this.mouseoverhandler = new MouseoverHandler(this.rectVscale.X, this.rectVscale.Y, this.basescale);
    }

    public Vector2 GetSize() => this.rectVscale;

    public bool UpdateRibbonEntry(
      Player player,
      bool disableInput,
      float DeltaTime,
      Vector2 offset,
      out bool dismiss)
    {
      dismiss = false;
      bool flag = false;
      offset += this.location;
      this.mouseover = this.mouseoverhandler.UpdateMouseoverHandler(player, offset, DeltaTime) && !disableInput;
      this.alertcounter.ChangeStatus(this.notificationpack.AlertStatus);
      switch (this.notificationpack.AlertStatus)
      {
        case NotificationAlertStatus.Tick:
          this.drawcolouroverlay = false;
          break;
        case NotificationAlertStatus.Exclamation:
          this.colouroverlay.SetAllColours(RibbonEntry.exclamyellow);
          this.drawcolouroverlay = true;
          break;
        case NotificationAlertStatus.Danger_Worst:
          this.colouroverlay.SetAllColours(RibbonEntry.alertred);
          this.drawcolouroverlay = true;
          break;
        case NotificationAlertStatus.Special_Heart:
          this.colouroverlay.SetAllColours(ColourData.Z_BarBabyGreen);
          this.drawcolouroverlay = true;
          break;
        default:
          this.drawcolouroverlay = false;
          break;
      }
      this.alertcounter.UpdateNotificationAlertIcon(player, offset, DeltaTime);
      if (!disableInput && this.mouseover && !dismiss)
        flag = (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0;
      return flag;
    }

    public void DrawRibbonEntry(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      if (this.drawcolouroverlay)
        this.colouroverlay.Draw(spritebatch, AssetContainer.SpriteSheet, offset, this.colouroverlaysize, 1f);
      this.darkoverlay.Draw(spritebatch, AssetContainer.SpriteSheet, offset, this.darkoverlaysize, 0.1f);
      if (this.tint)
        this.tintRect.Draw(spritebatch, AssetContainer.SpriteSheet, offset, this.rectVscale, 0.1f);
      this.button.DrawRibbonButton(spritebatch, offset);
      if (this.notificationinfo != null)
      {
        this.alertcounter.DrawNotificationAlertIcon(offset, spritebatch);
        this.text.DrawZGenericText(offset, spritebatch);
      }
      if (!this.mouseover || this.dismissButton != null && this.dismissButton.mouseover)
        return;
      this.mouseoverhandler.DrawMouseOverHandler(spritebatch, offset);
    }
  }
}
