// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Generic.Summary24Hr
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_AnimalNotification;
using TinyZoo.Z_BuildingInfo.Generic.Summary.Header;
using TinyZoo.Z_BuildingInfo.Generic.Summary.InformationPopup;
using TinyZoo.Z_BuildingInfo.Generic.Summary.InfoRow;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.Generic
{
  internal class Summary24Hr
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private BuildingSummaryHeader header;
    private ThisBuildingSummary thisBuildingSummary;
    private InformationPopupPanel popupPanel;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    private IndustryType refIndustryType;
    private ShopEntry refShopEntry;

    public Summary24Hr(ShopEntry shopEntry, Player player, float _BaseScale, float forceThisWidth = -1f)
    {
      this.BaseScale = _BaseScale;
      this.refShopEntry = shopEntry;
      this.refIndustryType = IndustryCategoryData.GetBuildingToIndustryType(shopEntry.tiletype);
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      Vector2 defaultBuffer = this.scaleHelper.DefaultBuffer;
      float x = this.scaleHelper.ScaleX(300f);
      if ((double) forceThisWidth != -1.0)
        x = forceThisWidth;
      this.customerFrame = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, this.BaseScale);
      this.customerFrame.AddMiniHeading("Yesterday's Report");
      this.header = new BuildingSummaryHeader(this.refIndustryType, player, this.BaseScale, x - defaultBuffer.X * 2f);
      this.thisBuildingSummary = new ThisBuildingSummary(this.refShopEntry, this.BaseScale, x - defaultBuffer.X * 2f);
      float num1 = this.customerFrame.GetMiniHeadingHeight() + defaultBuffer.Y;
      this.header.location.Y = num1;
      this.header.location.Y += this.header.GetSize().Y * 0.5f;
      float num2 = num1 + this.header.GetSize().Y + defaultBuffer.Y;
      this.thisBuildingSummary.location.Y = num2;
      this.thisBuildingSummary.location.Y += this.thisBuildingSummary.GetSize().Y * 0.5f;
      float y = num2 + this.thisBuildingSummary.GetSize().Y + defaultBuffer.Y;
      this.customerFrame.Resize(new Vector2(x, y));
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.header.location.Y += vector2.Y;
      this.thisBuildingSummary.location.Y += vector2.Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateSummary24Hr(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      SummaryInfoType infoType = SummaryInfoType.Count;
      if (this.header.UpdateBuildingSummaryHeader(player, DeltaTime, offset))
        infoType = SummaryInfoType.TotalCategoryRevenue;
      SummaryInfoType summaryInfoType = this.thisBuildingSummary.UpdateThisBuildingSummary(player, DeltaTime, offset);
      if (summaryInfoType != SummaryInfoType.Count)
        infoType = summaryInfoType;
      if (infoType != SummaryInfoType.Count)
      {
        MovingTrayDirection direction = MovingTrayDirection.Left;
        this.popupPanel = new InformationPopupPanel(infoType, this.refShopEntry, this.refIndustryType, this.BaseScale, direction);
        this.popupPanel.location.X = this.customerFrame.VSCale.X * 0.5f;
        this.popupPanel.location.X -= this.popupPanel.GetSize().X * 0.5f;
        if (direction == MovingTrayDirection.Left)
          this.popupPanel.location.X *= -1f;
        this.popupPanel.location.Y -= this.customerFrame.VSCale.Y * 0.5f;
        this.popupPanel.location.Y += this.popupPanel.GetSize().Y * 0.5f;
      }
      if (this.popupPanel == null)
        return;
      this.popupPanel.UpdateInformationPopupPanel(player, DeltaTime, offset);
    }

    public void PreDrawSummary24Hr(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.popupPanel == null)
        return;
      this.popupPanel.DrawInformationPopupPanel(offset, spriteBatch);
    }

    public void DrawSummary24Hr(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.header.DrawBuildingSummaryHeader(offset, spriteBatch);
      this.thisBuildingSummary.DrawThisBuildingSummary(offset, spriteBatch);
      if (this.popupPanel == null)
        return;
      this.popupPanel.PostDrawInformationPopupPanel(offset, spriteBatch);
    }
  }
}
