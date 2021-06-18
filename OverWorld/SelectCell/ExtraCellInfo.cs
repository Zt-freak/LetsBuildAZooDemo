// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.SelectCell.ExtraCellInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.SelectCell.Orders;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.OverWorld.SelectCell
{
  internal class ExtraCellInfo
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private ZGenericText numberOfAnimals;
    private ZGenericText spaceText;

    public ExtraCellInfo(float BaseScale, float width)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      defaultBuffer.Y = uiScaleHelper.ScaleY(3f);
      this.numberOfAnimals = new ZGenericText(string.Format("+{0} animals", (object) 0), BaseScale);
      this.spaceText = new ZGenericText(string.Format("Space Used: {0}/{1}", (object) 0, (object) 0), BaseScale);
      this.numberOfAnimals.SetAllColours(ColourData.Z_TextBrown);
      this.spaceText.SetAllColours(ColourData.Z_TextBrown);
      float y = this.numberOfAnimals.GetSize().Y;
      Vector2 zero = Vector2.Zero;
      zero.Y += defaultBuffer.Y;
      this.numberOfAnimals.vLocation.Y = zero.Y + y * 0.5f;
      zero.Y += y;
      this.spaceText.vLocation.Y = zero.Y + y * 0.5f;
      zero.Y += y;
      zero.X = uiScaleHelper.ScaleX(125f);
      zero.Y += defaultBuffer.Y;
      this.customerFrame = new CustomerFrame(zero, ColourData.Z_CreamFADED, BaseScale);
      Vector2 vector2 = this.customerFrame.VSCale * -0.5f;
      this.numberOfAnimals.vLocation.Y += vector2.Y;
      this.spaceText.vLocation.Y += vector2.Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void SetData(NewAnimalsInCellInfo info)
    {
      this.numberOfAnimals.textToWrite = string.Format("+{0} animals:", (object) info.NumberOfAnimals);
      this.spaceText.textToWrite = string.Format("Space Used: {0}/{1}", (object) info.TotalSpaceNeeded, (object) info.CurrentPenSpace);
      if ((double) info.TotalSpaceNeeded > (double) info.CurrentPenSpace)
        this.spaceText.SetAllColours(ColourData.Z_BarRed);
      else
        this.spaceText.SetAllColours(ColourData.Z_TextBrown);
    }

    public static string GetThreatAlertStatusToString(NotificationAlertStatus status)
    {
      switch (status)
      {
        case NotificationAlertStatus.None:
        case NotificationAlertStatus.Tick:
        case NotificationAlertStatus.Special_Heart:
        case NotificationAlertStatus.Special_Star:
          return "None";
        case NotificationAlertStatus.Exclamation:
          return "Medium";
        case NotificationAlertStatus.Danger_Worst:
          return "High";
        default:
          return "Unknown";
      }
    }

    public void DrawExtraCellInfo(Vector2 offset, SpriteBatch spriteBatch, float AlphaMult = 1f)
    {
      offset += this.location;
      this.customerFrame.SetAlphaed(AlphaMult);
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.numberOfAnimals.DrawZGenericText(offset, spriteBatch, AlphaMult);
      this.spaceText.DrawZGenericText(offset, spriteBatch, AlphaMult);
    }
  }
}
