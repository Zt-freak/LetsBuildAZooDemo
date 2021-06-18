// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.ResearcherInfo.BigNumberInFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Research_.ResearcherInfo
{
  internal class BigNumberInFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private ZGenericText text;

    public BigNumberInFrame(
      float BaseScale,
      string textForSizing,
      float textScaleMult = 2f,
      CustomerFrameColors frameColor = CustomerFrameColors.Brown)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.text = new ZGenericText(textForSizing, BaseScale * textScaleMult, _UseOnePointFiveFont: true);
      ZGenericText text = this.text;
      text.vLocation = text.vLocation + new Vector2(1f, 2f);
      this.customerFrame = new CustomerFrame(this.text.GetSize() + uiScaleHelper.DefaultBuffer, frameColor, BaseScale);
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void SetText(string _text) => this.text.textToWrite = _text;

    public void UpdateBigNumberInFrame()
    {
    }

    public void DrawBigNumberInFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.text.DrawZGenericText(offset, spriteBatch);
    }
  }
}
