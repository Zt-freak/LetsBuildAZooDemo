// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._03FamilyTree.Children.ChildrenManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._03FamilyTree.Children
{
  internal class ChildrenManager
  {
    public Vector2 location;
    private CustomerFrame customerframe;

    public ChildrenManager(float width, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.customerframe = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerframe.AddMiniHeading("Children");
      this.customerframe.Resize(new Vector2(width, uiScaleHelper.ScaleY(100f)));
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void UpdateChildrenManager(Vector2 offset) => offset += this.customerframe.frame.vLocation;

    public void DrawChildrenManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerframe.DrawCustomerFrame(offset, spriteBatch);
    }
  }
}
