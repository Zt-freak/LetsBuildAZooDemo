// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Generic.Summary.InformationPopup.InfoPopupFrameBase
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.Generic.Summary.InformationPopup
{
  internal class InfoPopupFrameBase
  {
    public Vector2 location;
    public CustomerFrame customerFrame;
    public UIScaleHelper scaleHelper;
    public Vector2 buffer;

    public InfoPopupFrameBase(float BaseScale)
    {
      this.scaleHelper = new UIScaleHelper(BaseScale);
      this.buffer = this.scaleHelper.DefaultBuffer;
      this.customerFrame = new CustomerFrame(this.scaleHelper.ScaleVector2(new Vector2(200f, 100f)), CustomerFrameColors.Brown, BaseScale);
    }

    public virtual Vector2 GetSize() => this.customerFrame.VSCale;

    public virtual void UpdateInfoPopupFrame()
    {
    }

    public virtual void DrawInfoPopupFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
    }
  }
}
