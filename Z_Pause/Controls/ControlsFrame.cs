// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Pause.Controls.ControlsFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Pause.Controls.Keyboard;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Pause.Controls
{
  internal class ControlsFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private KeyboardLayoutView keyboard;

    public ControlsFrame(float BaseScale, Player player)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      Vector2 vector2 = defaultBuffer;
      this.keyboard = new KeyboardLayoutView(player, BaseScale);
      this.keyboard.location = vector2;
      this.customerFrame = new CustomerFrame(vector2 + this.keyboard.GetSize() + defaultBuffer, CustomerFrameColors.Brown, BaseScale);
      this.keyboard.location += -this.customerFrame.VSCale * 0.5f;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateControlsFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.keyboard.UpdateKeyboardLayoutViewManager(player, DeltaTime, offset);
    }

    public void DrawControlsFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.keyboard.DrawKeyboardLayoutViewManager(offset, spriteBatch);
    }
  }
}
