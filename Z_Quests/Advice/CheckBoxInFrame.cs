// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.Advice.CheckBoxInFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Quests.Advice
{
  internal class CheckBoxInFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private LabelledCheckbox checkbox;

    public CheckBoxInFrame(float BaseScale, bool startTicked)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.checkbox = new LabelledCheckbox("Pin Task", false, BaseScale, startTicked);
      this.checkbox.location.X += this.checkbox.GetSize().X * 0.5f;
      this.checkbox.location.X -= this.checkbox.GetBoxSize().X * 0.5f;
      Vector2 _VSCale = this.checkbox.GetSize() + uiScaleHelper.DefaultBuffer;
      _VSCale.X += uiScaleHelper.DefaultBuffer.X;
      this.customerFrame = new CustomerFrame(_VSCale, CustomerFrameColors.DarkBrown, BaseScale);
    }

    public bool GetIsTicked() => this.checkbox.IsTicked;

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateCheckBoxInFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      return this.checkbox.UpdateLabelledCheckbox(player, offset, DeltaTime);
    }

    public void DrawCheckBoxInFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.checkbox.DrawLabelledCheckbox(spriteBatch, offset);
    }
  }
}
