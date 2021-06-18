// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.RecruitmentAgencyFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment
{
  internal class RecruitmentAgencyFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private MiniHeading miniHeading;
    private SimpleTextHandler explanationText;
    private TextButton textButton;

    public RecruitmentAgencyFrame(float forceWidth, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      double defaultXbuffer = (double) uiScaleHelper.GetDefaultXBuffer();
      Vector2 vector2_1 = Vector2.One * 10f;
      float num1 = 0.0f;
      this.miniHeading = new MiniHeading(Vector2.Zero, "Instant Hire: Recruitment Agency", 1f, BaseScale);
      float num2 = num1 + (this.miniHeading.GetTextHeight() + uiScaleHelper.ScaleY(vector2_1.Y) + defaultYbuffer);
      this.explanationText = new SimpleTextHandler("Instantly find and hire employees through a Recruitment Agency for a fee.", true, (float) ((double) forceWidth / 1024.0 * 0.899999976158142), BaseScale);
      this.explanationText.AutoCompleteParagraph();
      this.explanationText.Location.Y += num2 + this.explanationText.GetHeightOfOneLine() * 0.5f;
      this.explanationText.SetAllColours(ColourData.Z_Cream);
      float num3 = num2 + this.explanationText.GetHeightOfParagraph() + defaultYbuffer;
      this.textButton = new TextButton(BaseScale, "Recruit", 50f, _OverrideFrameScale: BaseScale);
      this.textButton.vLocation.Y = num3;
      Vector2 size = this.textButton.GetSize();
      this.textButton.vLocation.Y += size.Y * 0.5f;
      float y = num3 + size.Y + defaultYbuffer;
      this.customerFrame = new CustomerFrame(new Vector2(forceWidth, y), BaseScale: BaseScale);
      this.miniHeading.SetTextPosition(this.customerFrame.VSCale, vector2_1.X, vector2_1.Y);
      Vector2 vector2_2 = -this.customerFrame.VSCale * 0.5f;
      this.explanationText.Location.Y += vector2_2.Y;
      this.textButton.vLocation.Y += vector2_2.Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateRecruitmentAgencyFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      return this.textButton.UpdateTextButton(player, offset, DeltaTime);
    }

    public void DrawRecruitmentAgencyFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.miniHeading.DrawMiniHeading(offset, spriteBatch);
      this.explanationText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.textButton.DrawTextButton(offset, 1f, spriteBatch);
    }
  }
}
