// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._02Habitat.EnclosureCapacityManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal._02Habitat.EnclosureCap;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._02Habitat
{
  internal class EnclosureCapacityManager
  {
    public Vector2 location;
    private CustomerFrame customerframe;
    private EnclosureCapacity enclosurecapacity;

    public EnclosureCapacityManager(float width, PrisonZone pz, int FloorSpace, float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.customerframe = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerframe.AddMiniHeading("Enclosure Capacity");
      this.enclosurecapacity = new EnclosureCapacity(pz, (float) FloorSpace, BaseScale);
      Vector2 zero = Vector2.Zero;
      zero.Y += this.customerframe.GetMiniHeadingHeight();
      zero.X += defaultBuffer.X;
      zero.Y += defaultBuffer.Y * 0.5f;
      this.enclosurecapacity.Location.Y = zero.Y;
      this.enclosurecapacity.Location += this.enclosurecapacity.GetOffsetFromTopLeft();
      this.enclosurecapacity.Location.X -= this.enclosurecapacity.GetSize().X * 0.5f;
      zero.Y += this.enclosurecapacity.GetSize().Y;
      zero.Y += defaultBuffer.Y * 0.5f;
      zero.X = width;
      this.customerframe.Resize(zero);
      this.enclosurecapacity.Location.Y += (-this.customerframe.VSCale * 0.5f).Y;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void UpdateEnclosureCapacityManager()
    {
    }

    public void DrawEnclosureCapacityManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerframe.DrawCustomerFrame(offset, spriteBatch);
      this.enclosurecapacity.DrawEnclosureCapacity(offset, spriteBatch);
    }
  }
}
