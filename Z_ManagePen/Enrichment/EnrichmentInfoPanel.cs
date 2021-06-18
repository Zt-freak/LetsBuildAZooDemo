// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Enrichment.EnrichmentInfoPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_BuldMenu.Z_NewCostInfo.StatsAndCost;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManagePen.Enrichment.EnrichmentItem;
using TinyZoo.Z_ManagePen.Enrichment.EnrichmentOnAnimals;

namespace TinyZoo.Z_ManagePen.Enrichment
{
  internal class EnrichmentInfoPanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private ItemInfoFrame itemInfoFrame;
    private EnrichmentEffectFrame effectFrame;
    private BuildingCost costFrame;
    private NotUnlockedPanel notUnlocked;

    public EnrichmentInfoPanel(
      TILETYPE selectedItem,
      PrisonZone pen,
      float BaseScale,
      float Xbuffer,
      float Ybuffer,
      Player player,
      bool lockedByResearch)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      float num1 = 0.0f;
      float num2 = uiScaleHelper.ScaleX(190f);
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Item Info", BaseScale);
      float y;
      if (lockedByResearch)
      {
        this.notUnlocked = new NotUnlockedPanel(BaseScale, num2, player, selectedItem);
        y = num1 + this.notUnlocked.GetSize().Y;
      }
      else
      {
        this.itemInfoFrame = new ItemInfoFrame(selectedItem, BaseScale, num2);
        this.itemInfoFrame.location.X = Xbuffer;
        float num3 = num1 + uiScaleHelper.ScaleY(75f);
        this.costFrame = new BuildingCost(selectedItem, player, BaseScale, num2);
        this.costFrame.Location.Y = num3;
        this.costFrame.Location.Y += this.costFrame.GetSize().Y * 0.5f;
        y = num3 + this.costFrame.GetSize().Y;
        if (CategoryData.GetEntriesInThisCategory(CATEGORYTYPE.Pen_Enrichment).Contains(selectedItem))
        {
          float num4 = y + defaultBuffer.Y;
          this.effectFrame = new EnrichmentEffectFrame(selectedItem, pen, BaseScale, num2, Xbuffer, Ybuffer);
          this.effectFrame.location.Y = num4 + this.effectFrame.GetSize().Y * 0.5f;
          y = num4 + this.effectFrame.GetSize().Y;
        }
      }
      this.bigBrownPanel.Finalize(new Vector2(num2, y));
      Vector2 frameOffsetFromTop = this.bigBrownPanel.GetFrameOffsetFromTop();
      if (this.itemInfoFrame != null)
        this.itemInfoFrame.location += frameOffsetFromTop;
      if (this.effectFrame != null)
        this.effectFrame.location.Y += frameOffsetFromTop.Y;
      if (this.costFrame == null)
        return;
      this.costFrame.Location.Y += frameOffsetFromTop.Y;
    }

    public Vector2 GetSize() => this.bigBrownPanel.vScale * Sengine.ScreenRatioUpwardsMultiplier;

    public void RefreshBar()
    {
      if (this.effectFrame == null)
        return;
      this.effectFrame.RefreshBar();
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateEnrichmentInfoPanel(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset))
        return true;
      if (this.itemInfoFrame != null)
        this.itemInfoFrame.UpdateItemInfoFrame();
      if (this.costFrame != null)
        this.costFrame.UpdateBuildingCost(player, 1, (Z_PenBuilder) null, DeltaTime);
      if (this.effectFrame != null)
        this.effectFrame.UpdateEnrichmentEffectFrame(player, DeltaTime, offset);
      if (this.notUnlocked != null)
        this.notUnlocked.UpdateNotUnlockedPanel(player, DeltaTime, offset);
      return false;
    }

    public void DrawEnrichmentInfoPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      if (this.itemInfoFrame != null)
        this.itemInfoFrame.DrawItemInfoFrame(offset, spriteBatch);
      if (this.costFrame != null)
        this.costFrame.DrawBuildingCost(offset, spriteBatch);
      if (this.effectFrame != null)
        this.effectFrame.DrawEnrichmentEffectFrame(offset, spriteBatch);
      if (this.notUnlocked == null)
        return;
      this.notUnlocked.DrawNotUnlockedPanel(offset, spriteBatch);
    }
  }
}
