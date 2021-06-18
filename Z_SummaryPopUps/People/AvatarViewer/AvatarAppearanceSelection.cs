// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.AvatarViewer.AvatarAppearanceSelection
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.AvatarViewer
{
  internal class AvatarAppearanceSelection
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private AvatarSkinsGrid skinGrid;

    public AvatarAppearanceSelection(Player player, WalkingPerson walkingPerson, float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.customerFrame = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerFrame.AddMiniHeading("Change Appearance");
      Vector2 zero = Vector2.Zero;
      zero.Y += this.customerFrame.GetMiniHeadingHeight();
      Vector2 vector2 = zero + defaultBuffer;
      this.skinGrid = new AvatarSkinsGrid(player, walkingPerson, BaseScale);
      this.skinGrid.location.Y = vector2.Y;
      Vector2 _vScale = vector2 + this.skinGrid.GetSize();
      _vScale.Y += defaultBuffer.Y;
      _vScale.X += defaultBuffer.X;
      this.customerFrame.Resize(_vScale);
      this.skinGrid.location.Y += (-this.customerFrame.VSCale * 0.5f).Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateAvatarAppearanceSelection(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      return this.skinGrid.UpdateAvatarSkinsGrid(player, DeltaTime, offset);
    }

    public void DrawAvatarAppearanceSelection(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.skinGrid.DrawAvatarSkinsGrid(offset, spriteBatch);
    }
  }
}
