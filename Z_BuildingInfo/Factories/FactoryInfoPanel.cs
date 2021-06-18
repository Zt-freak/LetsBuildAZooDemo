// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Factories.FactoryInfoPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuildingInfo.Generic;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ActiveProcess;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BuildingInfo.Factories
{
  internal class FactoryInfoPanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private Summary24Hr summary;
    private ActiveProcessDisplayFrame activeProcess;

    public FactoryInfoPanel(int BuildingUID, TILETYPE tileType, Player player, float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, TileData.GetTileStats(tileType).Name, BaseScale);
      ShopEntry thisFacility = player.shopstatus.GetThisFacility(BuildingUID);
      this.activeProcess = new ActiveProcessDisplayFrame(thisFacility, player, BaseScale);
      this.summary = new Summary24Hr(thisFacility, player, BaseScale, this.activeProcess.GetSize().X);
      Vector2 zero = Vector2.Zero;
      this.summary.location.Y = zero.Y;
      this.summary.location.Y += this.summary.GetSize().Y * 0.5f;
      zero.Y += this.summary.GetSize().Y;
      zero.Y += defaultBuffer.Y;
      this.activeProcess.location.Y = zero.Y;
      this.activeProcess.location.Y += this.activeProcess.GetSize().Y * 0.5f;
      zero.Y += this.activeProcess.GetSize().Y;
      zero.X = this.summary.GetSize().X;
      this.bigBrownPanel.Finalize(zero);
      Vector2 frameOffsetFromTop = this.bigBrownPanel.GetFrameOffsetFromTop();
      this.summary.location.Y += frameOffsetFromTop.Y;
      this.activeProcess.location.Y += frameOffsetFromTop.Y;
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateFactoryInfoPanel(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
      this.summary.UpdateSummary24Hr(player, DeltaTime, offset);
      this.activeProcess.UpdateActiveProcessDisplayFrame(player, DeltaTime, offset);
      return this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset);
    }

    public void DrawFactoryInfoPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.summary.PreDrawSummary24Hr(offset, spriteBatch);
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.summary.DrawSummary24Hr(offset, spriteBatch);
      this.activeProcess.DrawActiveProcessDisplayFrame(offset, spriteBatch);
    }
  }
}
