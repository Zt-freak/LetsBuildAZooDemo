// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Generic.Summary.Header.BuildingSummaryHeader
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.Generic.Summary.Header
{
  internal class BuildingSummaryHeader
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private ZGenericText headerText;
    private ZGenericText revenueText;
    private LittleSummaryButton infoButton;

    public BuildingSummaryHeader(
      IndustryType industryType,
      Player player,
      float BaseScale,
      float ForcedWidth)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      string industryTypeToString = IndustryCategoryData.GetIndustryTypeToString(industryType);
      int num1 = 0;
      this.headerText = new ZGenericText(industryTypeToString, BaseScale, false, _UseOnePointFiveFont: true);
      this.revenueText = new ZGenericText("$" + (object) num1, BaseScale, false, true, true);
      this.infoButton = new LittleSummaryButton(LittleSummaryButtonType.BlueInfoCircle, _BaseScale: BaseScale);
      Vector2 vector2_1 = new Vector2(defaultBuffer.X, 0.0f);
      this.headerText.vLocation.X = vector2_1.X;
      this.headerText.vLocation.Y -= this.headerText.GetSize().Y * 0.5f;
      Vector2 vector2_2 = vector2_1 + this.headerText.GetSize();
      vector2_2.X += defaultBuffer.X;
      float num2 = ForcedWidth - defaultBuffer.X;
      this.infoButton.vLocation.X = num2;
      this.infoButton.vLocation.X -= this.infoButton.GetSize().X * 0.5f;
      this.revenueText.vLocation.X = num2 - this.infoButton.GetSize().X - defaultBuffer.X;
      this.revenueText.vLocation.Y -= this.revenueText.GetSize().Y * 0.5f;
      vector2_2.Y += defaultBuffer.Y * 2f;
      this.customerFrame = new CustomerFrame(new Vector2(ForcedWidth, vector2_2.Y), CustomerFrameColors.BlueWithLighterBlueBorder, BaseScale);
      Vector2 vector2_3 = -this.customerFrame.VSCale * 0.5f;
      this.headerText.vLocation.X += vector2_3.X;
      this.infoButton.vLocation.X += vector2_3.X;
      this.revenueText.vLocation.X += vector2_3.X;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateBuildingSummaryHeader(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      return this.infoButton.UpdateLittleSummaryButton(DeltaTime, player, offset);
    }

    public void DrawBuildingSummaryHeader(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.headerText.DrawZGenericText(offset, spriteBatch);
      this.revenueText.DrawZGenericText(offset, spriteBatch);
      this.infoButton.DrawLittleSummaryButton(offset, spriteBatch);
    }
  }
}
