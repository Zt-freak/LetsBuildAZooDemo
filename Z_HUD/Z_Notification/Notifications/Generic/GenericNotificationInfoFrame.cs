// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.Notifications.Generic.GenericNotificationInfoFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.Z_Notification.Notifications.Generic
{
  internal class GenericNotificationInfoFrame
  {
    private SimpleTextHandler bodyPara;
    private TextButton textButton;
    private CustomerFrame customerFrame;

    public GenericNotificationInfoFrame(string BodyText, string ButtonText, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float num1 = defaultYbuffer;
      float num2 = 300f;
      this.bodyPara = new SimpleTextHandler(BodyText, true, (float) ((double) num2 / 1024.0 * 0.899999976158142) * BaseScale, BaseScale);
      this.bodyPara.AutoCompleteParagraph();
      this.bodyPara.SetAllColours(ColourData.Z_Cream);
      this.bodyPara.Location.Y = num1;
      this.bodyPara.Location.Y += this.bodyPara.GetHeightOfOneLine() * 0.5f;
      float num3 = num1 + this.bodyPara.GetHeightOfParagraph();
      if (!string.IsNullOrEmpty(ButtonText))
      {
        float num4 = num3 + defaultYbuffer;
        this.textButton = new TextButton(BaseScale, ButtonText, 50f, _OverrideFrameScale: BaseScale);
        Vector2 size = this.textButton.GetSize();
        this.textButton.vLocation.Y = num4 + size.Y * 0.5f;
        num3 = num4 + size.Y;
      }
      float y = num3 + defaultYbuffer;
      this.customerFrame = new CustomerFrame(new Vector2(uiScaleHelper.ScaleX(num2), y), CustomerFrameColors.Brown, BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.bodyPara.Location.Y += vector2.Y;
      if (this.textButton == null)
        return;
      this.textButton.vLocation.Y += vector2.Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateGenericNotificationInfoFrame(Player player, float DeltaTime, Vector2 offset) => this.textButton != null && this.textButton.UpdateTextButton(player, offset, DeltaTime);

    public void DrawGenericNotificationInfoFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.bodyPara.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      if (this.textButton == null)
        return;
      this.textButton.DrawTextButton(offset, 1f, spriteBatch);
    }
  }
}
