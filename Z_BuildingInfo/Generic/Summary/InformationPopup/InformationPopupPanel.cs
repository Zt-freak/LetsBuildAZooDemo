// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Generic.Summary.InformationPopup.InformationPopupPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalNotification;
using TinyZoo.Z_BuildingInfo.Generic.Summary.Header;
using TinyZoo.Z_BuildingInfo.Generic.Summary.InformationPopup.Specific;
using TinyZoo.Z_BuildingInfo.Generic.Summary.InfoRow;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BuildingInfo.Generic.Summary.InformationPopup
{
  internal class InformationPopupPanel
  {
    public Vector2 location;
    private MovingTray tray;
    private InfoPopupFrameBase frame;

    public InformationPopupPanel(
      SummaryInfoType infoType,
      ShopEntry shopEntry,
      IndustryType industryType,
      float BaseScale,
      MovingTrayDirection direction)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      string headertext = string.Empty;
      switch (infoType)
      {
        case SummaryInfoType.AnimalsBurnt:
        case SummaryInfoType.AnimalsOrCropsProcessed:
          this.frame = (InfoPopupFrameBase) new AnimalsProcessedInfo(shopEntry, BaseScale);
          if (TileData.IsAMeatProcessingPlant(shopEntry.tiletype) || TileData.IsAnIncinerator(shopEntry.tiletype))
          {
            headertext = "Animals";
            break;
          }
          if (TileData.IsAVegetableProcessingPlant(shopEntry.tiletype))
          {
            headertext = "Crops";
            break;
          }
          break;
        case SummaryInfoType.OperationWorkload:
          this.frame = (InfoPopupFrameBase) new OperationWorkloadInfo(shopEntry.ShopUID, shopEntry.tiletype, BaseScale);
          headertext = "Workload";
          break;
        case SummaryInfoType.ProductsProduced:
          this.frame = (InfoPopupFrameBase) new MeatMadeInfo(shopEntry, BaseScale);
          if (TileData.IsAMeatProcessingPlant(shopEntry.tiletype))
          {
            headertext = "Meat";
            break;
          }
          if (TileData.IsAVegetableProcessingPlant(shopEntry.tiletype))
          {
            headertext = "Veggies";
            break;
          }
          break;
        case SummaryInfoType.TotalCategoryRevenue:
          this.frame = (InfoPopupFrameBase) new TotalIndustryRevenueInfo(industryType, BaseScale);
          headertext = IndustryCategoryData.GetIndustryTypeToString(industryType);
          break;
        default:
          headertext = "Information";
          this.frame = new InfoPopupFrameBase(BaseScale);
          break;
      }
      Vector2 size = this.frame.GetSize();
      Vector2 destinationOffset = new Vector2(size.X + defaultBuffer.X * 1.5f, 0.0f);
      if (direction == MovingTrayDirection.Left)
        destinationOffset.X *= -1f;
      this.tray = new MovingTray(size, destinationOffset, direction, BaseScale, headertext);
      this.tray.StartLerp();
    }

    public Vector2 GetSize() => this.tray.GetSizeWithoutBorder();

    public void UpdateInformationPopupPanel(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.tray.UpdateMovingTray(player, DeltaTime, offset);
    }

    public void DrawInformationPopupPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.tray.DrawMovingTray(offset, spriteBatch);
      offset += this.tray.GetTruePosition();
      this.frame.DrawInfoPopupFrame(offset, spriteBatch);
    }

    public void PostDrawInformationPopupPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.tray.DrawOpenCloseButton(offset, spriteBatch);
    }
  }
}
