// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Welfare.WalfareManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Welfare
{
  internal class WalfareManager
  {
    public Vector2 location;
    private CustomerFrame customerframe;
    private SatisfactionBarManager satisfactionaager;

    public WalfareManager(PrisonerInfo animal, Player player, float BaseScale, float width = -1f)
    {
      this.customerframe = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerframe.AddMiniHeading("Welfare");
      this.satisfactionaager = new SatisfactionBarManager(animal, BaseScale);
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      Vector2 zero = Vector2.Zero;
      zero.Y += this.customerframe.GetMiniHeadingHeight();
      Vector2 _vScale = zero + defaultBuffer;
      this.satisfactionaager.Location = _vScale;
      this.satisfactionaager.Location += this.satisfactionaager.GetOffsetFromTopLeft(true);
      _vScale.X += this.satisfactionaager.GetSize(true).X;
      _vScale.X += defaultBuffer.X;
      _vScale.Y += this.satisfactionaager.GetSize().Y;
      _vScale.Y += defaultBuffer.Y;
      if ((double) width != -1.0)
        _vScale.X = width;
      this.customerframe.Resize(_vScale);
      this.satisfactionaager.Location += -this.customerframe.VSCale * 0.5f;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void UpdateWalfareManager(Vector2 offset, Player player)
    {
      offset += this.location;
      this.satisfactionaager.UpdateSatisfactionBarManager(offset, player);
    }

    public void DrawWalfareManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerframe.DrawCustomerFrame(offset, spriteBatch);
      this.satisfactionaager.DrawSatisfactionBarManager(offset, spriteBatch);
    }
  }
}
