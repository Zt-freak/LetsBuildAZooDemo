// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.HeaderAndArrows
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.ParkSummary.Finances
{
  internal class HeaderAndArrows
  {
    public Vector2 location;
    private SimpleArrowPageButtons arrows;
    private MiniHeading miniHeading;
    private CustomerFrame customerFrame;

    public HeaderAndArrows(float BaseScale, float ForcedWidth)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 vector2_1 = new Vector2(10f, 5f);
      double defaultXbuffer = (double) uiScaleHelper.GetDefaultXBuffer();
      float num1 = uiScaleHelper.GetDefaultYBuffer() * 0.5f;
      float num2 = 0.0f;
      this.miniHeading = new MiniHeading(Vector2.Zero, "BLAH", 1f, BaseScale);
      this.miniHeading.SetAllColours(ColourData.Z_FrameDarkBrown);
      float y = num2 + (this.miniHeading.GetTextHeight(true) + uiScaleHelper.ScaleY(vector2_1.Y)) + num1;
      this.customerFrame = new CustomerFrame(new Vector2(ForcedWidth, y), CustomerFrameColors.DarkerCream, BaseScale);
      Vector2 vector2_2 = -this.customerFrame.VSCale * 0.5f;
      this.miniHeading.SetTextPosition(this.customerFrame.VSCale, vector2_1.X, vector2_1.Y);
      this.arrows = new SimpleArrowPageButtons(BaseScale, _DoNotDrawFrame: true);
      Vector2 size = this.arrows.GetSize(true);
      this.arrows.Location.X = (float) ((double) ForcedWidth * 0.5 - (double) size.X * 0.5);
      this.arrows.Location.Y -= this.miniHeading.vLocation.Y + size.Y * 0.5f;
      this.arrows.Location += new Vector2(uiScaleHelper.ScaleX(-3f), uiScaleHelper.ScaleY(3f));
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void ChangeHeaderText(string newText) => this.miniHeading.EditText(newText);

    public int UpdateHeaderAndArrows(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      return this.arrows.UpdateSimpleArrowPageButtons(DeltaTime, player, offset);
    }

    public void DrawHeaderAndArrows(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.miniHeading.DrawMiniHeading(offset, spriteBatch);
      this.arrows.DrawSimpleArrowPageButtons(offset, spriteBatch);
    }
  }
}
