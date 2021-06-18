// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.IncineratorBuildingInfo.IncineratorBuildingPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuildingInfo.Generic;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ActiveProcess;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.QueueView;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.IncineratorBuildingInfo
{
  internal class IncineratorBuildingPanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private Summary24Hr summary24hr;
    private ActiveProcessDisplayFrame activeProcessFrame;
    private DeadAnimalsInQueue queueFrame;

    public IncineratorBuildingPanel(int BuildingUID, TILETYPE tileTYPE, Player player)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      UIScaleHelper uiScaleHelper = new UIScaleHelper(baseScaleForUi);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      ShopEntry thisFacility = player.shopstatus.GetThisFacility(BuildingUID);
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Incinerator", baseScaleForUi);
      this.activeProcessFrame = new ActiveProcessDisplayFrame(thisFacility, player, baseScaleForUi);
      this.summary24hr = new Summary24Hr(thisFacility, player, baseScaleForUi, this.activeProcessFrame.GetSize().X);
      Vector2 zero = Vector2.Zero;
      this.summary24hr.location = zero;
      this.summary24hr.location += this.summary24hr.GetSize() * 0.5f;
      zero.Y = this.summary24hr.GetSize().Y;
      zero.Y += defaultBuffer.Y;
      this.activeProcessFrame.location = zero;
      this.activeProcessFrame.location += this.activeProcessFrame.GetSize() * 0.5f;
      zero.Y += this.activeProcessFrame.GetSize().Y;
      zero.X += this.summary24hr.GetSize().X;
      zero.X += defaultBuffer.X;
      this.queueFrame = new DeadAnimalsInQueue(thisFacility.factoryproduction, baseScaleForUi, zero.Y, uiScaleHelper.ScaleX(100f), CustomerFrameColors.Brown);
      this.queueFrame.location.X = zero.X;
      this.queueFrame.location += this.queueFrame.GetSize() * 0.5f;
      zero.X += this.queueFrame.GetSize().X;
      this.bigBrownPanel.Finalize(zero);
      Vector2 frameOffsetFromTop = this.bigBrownPanel.GetFrameOffsetFromTop();
      this.summary24hr.location += frameOffsetFromTop;
      this.activeProcessFrame.location += frameOffsetFromTop;
      this.queueFrame.location += frameOffsetFromTop;
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateIncineratorBuildingPanel(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
      this.summary24hr.UpdateSummary24Hr(player, DeltaTime, offset);
      this.activeProcessFrame.UpdateActiveProcessDisplayFrame(player, DeltaTime, offset);
      this.queueFrame.UpdateDeadAnimalsInQueue(player, offset);
      return this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset);
    }

    public void DrawIncineratorBuildingPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.summary24hr.PreDrawSummary24Hr(offset, spriteBatch);
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.summary24hr.DrawSummary24Hr(offset, spriteBatch);
      this.activeProcessFrame.DrawActiveProcessDisplayFrame(offset, spriteBatch);
      this.queueFrame.DrawDeadAnimalsInQueue(offset, spriteBatch);
    }
  }
}
