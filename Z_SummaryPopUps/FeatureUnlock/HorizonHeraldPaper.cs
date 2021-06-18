// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.FeatureUnlock.HorizonHeraldPaper
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable;
using TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.FeatureUnlock
{
  internal class HorizonHeraldPaper
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private NewsFancyTitle newsTitle;
    private NewsHeaderBar headerBar;
    private ZGenericText articleHeader;
    private RowSegmentRectangle thinLine;
    private NewsContent newsContent;

    public HorizonHeraldPaper(float BaseScale, FeatureUnlockDisplayType unlockType)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      Vector2 zero = Vector2.Zero;
      this.newsTitle = new NewsFancyTitle(BaseScale);
      this.articleHeader = new ZGenericText(FeatureUnlockDisplayData.GetNewsHeaderForThis(unlockType), BaseScale, _UseOnePointFiveFont: true);
      this.articleHeader.SetAllColours(ColourData.Z_DarkTextGray);
      float width = Math.Max(this.articleHeader.GetSize().X, uiScaleHelper.ScaleX(450f));
      string leftString = "Issue " + (object) (int) (unlockType + 1);
      this.headerBar = new NewsHeaderBar(BaseScale, leftString, Z_GameFlags.GetGameDateToday_AsString(), "Free".ToUpper(), width);
      this.thinLine = new RowSegmentRectangle(this.headerBar.GetSize().X, uiScaleHelper.ScaleY(1f), ColourData.Z_DarkTextGray, 1f);
      this.thinLine.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.newsContent = new NewsContent(BaseScale, unlockType, width);
      Vector2 vector2_1 = defaultBuffer + defaultBuffer * 2f;
      this.newsTitle.vLocation.Y = vector2_1.Y;
      this.newsTitle.vLocation.Y += this.newsTitle.GetSize().Y * 0.5f;
      vector2_1.Y += this.newsTitle.GetSize().Y;
      vector2_1.Y += defaultBuffer.Y * 0.5f;
      this.headerBar.location = vector2_1;
      vector2_1.Y += this.headerBar.GetSize().Y;
      vector2_1.Y += defaultBuffer.Y * 0.5f;
      this.articleHeader.vLocation.Y = vector2_1.Y;
      this.articleHeader.vLocation.Y += this.articleHeader.GetSize().Y * 0.5f;
      vector2_1.Y += this.articleHeader.GetSize().Y;
      this.thinLine.vLocation.Y = vector2_1.Y;
      vector2_1.Y += this.thinLine.GetSize().Y;
      vector2_1.Y += defaultBuffer.Y;
      this.newsContent.location = vector2_1;
      vector2_1.Y += this.newsContent.GetSize().Y;
      vector2_1.X += width;
      vector2_1.X += defaultBuffer.X;
      this.customerFrame = new CustomerFrame(vector2_1 + defaultBuffer * 2f, CustomerFrameColors.Paper, BaseScale);
      Vector2 vector2_2 = -this.customerFrame.VSCale * 0.5f;
      this.newsTitle.vLocation.Y += vector2_2.Y;
      this.headerBar.location += vector2_2;
      this.articleHeader.vLocation.Y += vector2_2.Y;
      this.thinLine.vLocation.Y += vector2_2.Y;
      this.newsContent.location += vector2_2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateFeatureNewsFrame(Player player, float DeltaTime, Vector2 offset) => offset += this.location;

    public void DrawFeatureNewsFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.newsTitle.DrawNewsFancyHeaderText(offset, spriteBatch);
      this.headerBar.DrawNewsHeaderBar(offset, spriteBatch);
      this.articleHeader.DrawZGenericText(offset, spriteBatch);
      this.thinLine.DrawRowSegmentRectangle(offset, spriteBatch);
      this.newsContent.DrawNewsContent(offset, spriteBatch);
    }
  }
}
