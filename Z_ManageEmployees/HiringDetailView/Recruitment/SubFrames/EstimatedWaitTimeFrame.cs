// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames.EstimatedWaitTimeFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames
{
  internal class EstimatedWaitTimeFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private ZGenericText text;
    private string baseString;

    public EstimatedWaitTimeFrame(float BaseScale)
    {
      this.baseString = "Estimated Waiting Time: ";
      this.text = new ZGenericText(this.baseString, BaseScale, _UseOnePointFiveFont: true);
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      defaultBuffer.Y *= 0.5f;
      Vector2 vector2 = Vector2.Zero + defaultBuffer + this.text.GetSize();
      vector2.X += uiScaleHelper.ScaleX(100f);
      this.customerFrame = new CustomerFrame(vector2 + defaultBuffer, CustomerFrameColors.DarkBrown, BaseScale);
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void SetTimeString(string time) => this.text.textToWrite = this.baseString + time;

    public void DrawTextInFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.text.DrawZGenericText(offset, spriteBatch);
    }
  }
}
