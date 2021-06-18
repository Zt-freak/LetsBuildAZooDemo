// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.NotificationRibbonManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_HUD.Z_Notification.NotificationRibbon
{
  internal class NotificationRibbonManager
  {
    private float basescale;
    private TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.NotificationRibbon ribbon;
    private UIScaleHelper uiscale;
    public ButtHolder REF_buttonpressed;
    private RibbonType type;
    private NotificationInfo notifInfo;
    private float ribbonwidth;
    private Vector2 topleftOfRibbon;

    public NotificationRibbonManager(
      RibbonType type_,
      ButtHolder buttonpressed,
      Vector2 topleftOfRibbon_,
      NotificationInfo notificationInfo = null)
    {
      this.ribbonwidth = 140f;
      this.type = type_;
      this.topleftOfRibbon = topleftOfRibbon_;
      if (notificationInfo == null)
        this.InitRibbon_Summary(buttonpressed);
      else
        this.notifInfo = notificationInfo;
    }

    private void InitRibbon_Summary(ButtHolder buttonpressed, bool dontlerp = false)
    {
      this.REF_buttonpressed = buttonpressed;
      this.StartCreate();
      this.ribbon = new TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.NotificationRibbon(buttonpressed, this.basescale, this.uiscale.ScaleX((float) (-1.0 * (10.0 + (double) this.ribbonwidth))), dontlerp: dontlerp);
      this.FinishCreate();
    }

    private void StartCreate()
    {
      this.basescale = Z_GameFlags.GetBaseScaleForUI();
      this.uiscale = new UIScaleHelper(this.basescale);
    }

    private void FinishCreate()
    {
      this.ribbon.location = this.topleftOfRibbon;
      this.ribbon.location += 0.5f * this.ribbon.GetSize();
    }

    public bool CheckMouseOver(Player player) => this.ribbon.CheckMouseOver(player, Vector2.Zero);

    public float GetOffset() => this.ribbon.GetOffset();

    public bool RefreshRibbon()
    {
      bool flag = false;
      if (this.REF_buttonpressed.GetNotificationTypesAndCounts().Count > 0)
        this.InitRibbon_Summary(this.REF_buttonpressed, true);
      else
        flag = true;
      return flag;
    }

    public bool UpdateNotificationRibbonManager(
      Player player,
      float DeltaTime,
      out NotificationInfo notificationinfo,
      ref bool remakeRibbon)
    {
      bool RemakeRibbon;
      if (this.ribbon.UpdateNotificationRibbon(player, Vector2.Zero, DeltaTime, out notificationinfo, out RemakeRibbon))
        return true;
      remakeRibbon |= RemakeRibbon;
      if (this.REF_buttonpressed.dirty)
      {
        this.REF_buttonpressed.dirty = false;
        remakeRibbon = true;
      }
      return false;
    }

    public void DrawNotificationRibbonManager(SpriteBatch spritebatch) => this.ribbon.DrawNotificationRibbon(spritebatch, Vector2.Zero);
  }
}
