// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.WarehouseBuildingInfo.WarehouseInfoFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.WarehouseBuildingInfo
{
  internal class WarehouseInfoFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private SimpleTextHandler desc;

    public WarehouseInfoFrame(float BaseScale, float ForcedWidth)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.customerFrame = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerFrame.AddMiniHeading("Info");
      Vector2 zero = Vector2.Zero;
      zero.Y += this.customerFrame.GetMiniHeadingHeight();
      zero.Y += defaultBuffer.Y;
      this.desc = new SimpleTextHandler("All products made in your factories end up here. They are sold every Friday when the goods truck arrives to collect them.", ForcedWidth - defaultBuffer.X, true, BaseScale, AutoComplete: true);
      this.desc.SetAllColours(ColourData.Z_Cream);
      this.desc.Location.Y = zero.Y;
      this.desc.Location.Y += this.desc.GetHeightOfOneLine() * 0.5f;
      zero.Y += uiScaleHelper.ScaleY(50f);
      zero.X = ForcedWidth;
      this.customerFrame.Resize(zero);
      this.desc.Location.Y += (-this.customerFrame.VSCale * 0.5f).Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateWarehouseInfoFrame()
    {
    }

    public void DrawWarehouseInfoFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.desc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
