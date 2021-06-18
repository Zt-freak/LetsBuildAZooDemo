// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.SelectCell.Orders.OrderAssignmentFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.OverWorld.SelectCell.Orders.Rows;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.OverWorld.SelectCell.Orders
{
  internal class OrderAssignmentFrame
  {
    private CustomerFrame customerFrame;
    private OrderAssignmentRowContainer rowContainer;

    public OrderAssignmentFrame(float BaseScale, List<IntakePerson> orderList)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.rowContainer = new OrderAssignmentRowContainer(BaseScale, orderList);
      Vector2 zero = Vector2.Zero;
      zero.X += defaultBuffer.X;
      this.rowContainer.location.Y = zero.Y;
      Vector2 _VSCale = zero + this.rowContainer.GetSize();
      _VSCale.X += defaultBuffer.X;
      this.customerFrame = new CustomerFrame(_VSCale, CustomerFrameColors.Brown, BaseScale);
      this.rowContainer.location.Y += (this.customerFrame.VSCale * -0.5f).Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void SetData(PrisonZone prisonzone) => this.rowContainer.SetData(prisonzone);

    public void UpdateOrderAssignmentFrame(Player player, float DeltaTime, Vector2 offset) => this.rowContainer.UpdateOrderAssignmentRowContainer(player, DeltaTime, offset);

    public NewAnimalsInCellInfo GetDataChanged_ForCellInfo() => this.rowContainer.GetDataChanged_ForCellInfo();

    public List<IntakePerson> GetAnimalListSelected(bool SetAsAssigned) => this.rowContainer.GetAnimalListSelected(SetAsAssigned);

    public void DrawOrderAssignmentFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.rowContainer.DrawOrderAssignmentRowContainer(offset, spriteBatch);
    }
  }
}
