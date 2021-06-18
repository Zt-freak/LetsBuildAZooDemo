// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.Notifications.Cleanliness.CleanlinessDescriptionFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;
using TinyZoo.Z_Notification;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.Z_Notification.Notifications.Cleanliness
{
  internal class CleanlinessDescriptionFrame
  {
    public Vector2 location;
    protected float basescale;
    protected UIScaleHelper scalehelper;
    protected CustomerFrame frame;
    protected Vector2 framescale;
    protected Vector2 pad;
    protected MouseoverHandler mouseoverhandler;
    private SimpleTextHandler description;

    public CleanlinessDescriptionFrame(
      float basescale_,
      Z_NotificationType notiftype,
      NotificationAlertStatus notifstatus)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.pad = this.scalehelper.DefaultBuffer;
      string TextToWrite = "";
      if (notiftype != Z_NotificationType.F_FoodTrash)
      {
        if (notiftype != Z_NotificationType.F_VomitTrash)
          throw new NotImplementedException();
        switch (notifstatus)
        {
          case NotificationAlertStatus.Tick:
            TextToWrite = "Your zoo is free of vomit!";
            break;
          case NotificationAlertStatus.Exclamation:
            TextToWrite = "The lost lunches on the floor of your zoo is becoming a problem.";
            break;
          case NotificationAlertStatus.Danger_Worst:
            TextToWrite = "The zoo is suffering from the results of people feeling a bit unwell.";
            break;
        }
      }
      else
      {
        switch (notifstatus)
        {
          case NotificationAlertStatus.Tick:
            TextToWrite = "Your zoo is clean of trash!";
            break;
          case NotificationAlertStatus.Exclamation:
            TextToWrite = "There is some litter on the floor of your zoo.";
            break;
          case NotificationAlertStatus.Danger_Worst:
            TextToWrite = "The zoo is suffering from too much trash on the floor.";
            break;
        }
      }
      this.description = new SimpleTextHandler(TextToWrite, this.scalehelper.ScaleX(200f), _Scale: this.basescale);
      this.description.SetAllColours(ColourData.Z_Cream);
      this.description.AutoCompleteParagraph();
      this.framescale = new Vector2();
      this.framescale += 2f * this.pad;
      this.framescale += this.description.GetSize();
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.mouseoverhandler = new MouseoverHandler(this.framescale, this.basescale);
      Vector2 vector2 = -0.5f * this.framescale + this.pad;
      this.description.Location = vector2;
      vector2.Y += this.description.GetSize().Y + this.pad.Y;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateCleanlinessDescriptionFrame(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out bool trackthis)
    {
      offset += this.location;
      trackthis = false;
      return false;
    }

    public void DrawCleanlinessDescriptionFrame(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.description.DrawSimpleTextHandler(offset, 1f, spritebatch);
    }
  }
}
