// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._04Profitability.Appeal.AppealManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._04Profitability.Appeal
{
  internal class AppealManager
  {
    public Vector2 location;
    private CustomerFrame customerframe;

    public AppealManager(float width, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.customerframe = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerframe.AddMiniHeading("Appeal of Species");
      this.customerframe.Resize(new Vector2(width, uiScaleHelper.ScaleY(100f)));
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void UpdateAppealManager()
    {
    }

    public void DrawAppealManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerframe.DrawCustomerFrame(offset, spriteBatch);
    }
  }
}
