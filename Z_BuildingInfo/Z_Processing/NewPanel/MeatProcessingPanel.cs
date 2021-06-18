// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.MeatProcessingPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuildingInfo.Generic;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ActiveProcess;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel
{
  internal class MeatProcessingPanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private Summary24Hr summary24Hr;
    private ProcessingValueFrame produceValueFrame;
    private ActiveProcessDisplayFrame activeProcessFrame;
    private ShopEntry refShopEntry;

    public MeatProcessingPanel(int BuildingUID, TILETYPE tileType, Player player, float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      string empty = string.Empty;
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, !TileData.IsAVegetableProcessingPlant(tileType) ? "Meat Processing" : "Vegetable Processing", BaseScale);
      this.refShopEntry = player.shopstatus.GetThisFacility(BuildingUID);
      this.activeProcessFrame = new ActiveProcessDisplayFrame(this.refShopEntry, player, BaseScale);
      this.summary24Hr = new Summary24Hr(this.refShopEntry, player, BaseScale, this.activeProcessFrame.GetSize().X);
      Vector2 zero1 = Vector2.Zero;
      this.summary24Hr.location = zero1;
      this.summary24Hr.location += this.summary24Hr.GetSize() * 0.5f;
      zero1.Y += this.summary24Hr.GetSize().Y;
      zero1.Y += defaultBuffer.Y;
      this.activeProcessFrame.location = zero1;
      this.activeProcessFrame.location += this.activeProcessFrame.GetSize() * 0.5f;
      zero1 += this.activeProcessFrame.GetSize();
      Vector2 zero2 = Vector2.Zero;
      zero2.X = zero1.X + defaultBuffer.X;
      this.produceValueFrame = new ProcessingValueFrame(BuildingUID, tileType, player, BaseScale, zero1.Y);
      this.produceValueFrame.location = zero2;
      this.produceValueFrame.location += this.produceValueFrame.GetSize() * 0.5f;
      Vector2 vector2 = zero2 + this.produceValueFrame.GetSize();
      Vector2 zero3 = Vector2.Zero;
      zero3.X = vector2.X;
      zero3.Y = Math.Max(zero1.Y, vector2.Y);
      this.bigBrownPanel.Finalize(zero3);
      Vector2 frameOffsetFromTop = this.bigBrownPanel.GetFrameOffsetFromTop();
      this.summary24Hr.location += frameOffsetFromTop;
      this.produceValueFrame.location += frameOffsetFromTop;
      this.activeProcessFrame.location += frameOffsetFromTop;
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateMeatProcessingPanel(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
      this.summary24Hr.UpdateSummary24Hr(player, DeltaTime, offset);
      this.produceValueFrame.UpdateProcessingValueFrame(player, DeltaTime, offset);
      this.activeProcessFrame.UpdateActiveProcessDisplayFrame(player, DeltaTime, offset);
      return this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset);
    }

    public void DrawMeatProcessingPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.summary24Hr.PreDrawSummary24Hr(offset, spriteBatch);
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.summary24Hr.DrawSummary24Hr(offset, spriteBatch);
      this.activeProcessFrame.DrawActiveProcessDisplayFrame(offset, spriteBatch);
      this.produceValueFrame.DrawProcessingValueFrame(offset, spriteBatch);
    }
  }
}
