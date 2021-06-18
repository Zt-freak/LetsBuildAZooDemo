// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.Notifications.Cleanliness.CleanlinessBinFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;
using TinyZoo.Z_Notification;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.Z_Notification.Notifications.Cleanliness
{
  internal class CleanlinessBinFrame
  {
    public Vector2 location;
    protected float basescale;
    protected UIScaleHelper scalehelper;
    protected CustomerFrame frame;
    protected Vector2 framescale;
    protected Vector2 pad;
    protected MouseoverHandler mouseoverhandler;
    private TextButton button;
    private SimpleTextHandler description;

    public CleanlinessBinFrame(
      float basescale_,
      Z_NotificationType notiftype,
      NotificationAlertStatus notifstatus)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.pad = this.scalehelper.DefaultBuffer;
      this.button = new TextButton(this.basescale, "Track", 40f);
      this.description = new SimpleTextHandler("Build more bins for customers to use.", this.scalehelper.ScaleX(200f), _Scale: this.basescale);
      this.description.SetAllColours(ColourData.Z_Cream);
      this.description.AutoCompleteParagraph();
      this.framescale = new Vector2();
      this.framescale += 2f * this.pad;
      this.framescale += this.description.GetSize();
      this.framescale.Y += this.pad.Y + this.button.GetSize_True().Y;
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.mouseoverhandler = new MouseoverHandler(this.framescale, this.basescale);
      Vector2 vector2 = -0.5f * this.framescale + this.pad;
      this.description.Location = vector2;
      vector2.Y += this.description.GetSize().Y + this.pad.Y;
      this.button.vLocation = vector2;
      this.button.vLocation.Y += 0.5f * this.button.GetSize_True().Y;
      this.button.vLocation.X = 0.0f;
      vector2.Y += this.button.GetSize_True().Y;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateCleanlinessBinFrame(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out bool trackthis)
    {
      offset += this.location;
      trackthis = this.button.UpdateTextButton(player, offset, DeltaTime);
      return (0 | (trackthis ? 1 : 0)) != 0;
    }

    public void DrawCleanlinessBinFrame(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.description.DrawSimpleTextHandler(offset, 1f, spritebatch);
      this.button.DrawTextButton(offset, 1f, spritebatch);
    }
  }
}
