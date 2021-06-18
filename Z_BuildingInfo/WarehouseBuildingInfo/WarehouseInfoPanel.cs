// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.WarehouseBuildingInfo.WarehouseInfoPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_BuildingInfo.Generic;
using TinyZoo.Z_BuildingInfo.WarehouseBuildingInfo.Items;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BuildingInfo.WarehouseBuildingInfo
{
  internal class WarehouseInfoPanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private Summary24Hr summary;
    private ItemsFrame items;
    private WarehouseInfoFrame info;

    public WarehouseInfoPanel(ShopEntry shopEntry, Player player, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Warehouse", BaseScale);
      Vector2 zero = Vector2.Zero;
      this.summary = new Summary24Hr(shopEntry, player, BaseScale);
      this.summary.location = zero;
      this.summary.location += this.summary.GetSize() * 0.5f;
      zero.Y += this.summary.GetSize().Y;
      zero.Y += defaultBuffer.Y;
      this.info = new WarehouseInfoFrame(BaseScale, this.summary.GetSize().X);
      this.info.location = zero;
      this.info.location += this.info.GetSize() * 0.5f;
      zero.X += this.summary.GetSize().X;
      zero.X += defaultBuffer.X;
      zero.Y = 0.0f;
      this.items = new ItemsFrame(shopEntry, player, BaseScale, uiScaleHelper.ScaleY(300f));
      this.items.location = zero;
      this.items.location += this.items.GetSize() * 0.5f;
      zero.X += this.items.GetSize().X;
      zero.Y += Math.Max(this.summary.GetSize().Y + defaultBuffer.Y + this.info.GetSize().Y, this.items.GetSize().Y);
      this.bigBrownPanel.Finalize(zero);
      Vector2 frameOffsetFromTop = this.bigBrownPanel.GetFrameOffsetFromTop();
      this.summary.location += frameOffsetFromTop;
      this.items.location += frameOffsetFromTop;
      this.info.location += frameOffsetFromTop;
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateWarehouseInfoPanel(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
      this.summary.UpdateSummary24Hr(player, DeltaTime, offset);
      this.items.UpdateItemsFrame(player, DeltaTime, offset);
      this.info.UpdateWarehouseInfoFrame();
      return this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset);
    }

    public void DrawWarehouseInfoPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.summary.PreDrawSummary24Hr(offset, spriteBatch);
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.summary.DrawSummary24Hr(offset, spriteBatch);
      this.info.DrawWarehouseInfoFrame(offset, spriteBatch);
      this.items.DrawItemsFrame(offset, spriteBatch);
    }
  }
}
