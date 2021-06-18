// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Enrichment.EnrichmentInfoManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManagePen.Enrichment.EnrichmentOnAnimals;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.Z_ManagePen.Enrichment
{
  internal class EnrichmentInfoManager
  {
    public Vector2 location;
    private EnrichmentInfoPanel enrichmentInfoPanel;
    private TILETYPE TileType;
    private PrisonZone refPen;
    private float BaseScale;
    private float Xbuffer;
    private float Ybuffer;

    public EnrichmentInfoManager(PrisonZone pen)
    {
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      this.Ybuffer = uiScaleHelper.GetDefaultYBuffer();
      this.Xbuffer = uiScaleHelper.GetDefaultXBuffer();
      this.refPen = pen;
      EnrichmentEffectFrame.currentAnimalTypeIndex = 0;
    }

    public Vector2 GetSize() => this.enrichmentInfoPanel != null ? this.enrichmentInfoPanel.GetSize() : Vector2.Zero;

    public void SetNewItem(TILETYPE _TileType, Player player)
    {
      bool lockedByResearch = MainBarManager.IsThisBuildingDisabled(_TileType, player);
      this.TileType = _TileType;
      this.enrichmentInfoPanel = new EnrichmentInfoPanel(_TileType, this.refPen, this.BaseScale, this.Xbuffer, this.Ybuffer, player, lockedByResearch);
      this.enrichmentInfoPanel.location = this.enrichmentInfoPanel.GetSize() * -0.5f;
    }

    public void RefreshBar()
    {
      if (this.enrichmentInfoPanel == null)
        return;
      this.enrichmentInfoPanel.RefreshBar();
    }

    public bool UpdateEnrichmentInfoManager(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      return this.enrichmentInfoPanel != null && this.enrichmentInfoPanel.UpdateEnrichmentInfoPanel(player, DeltaTime, offset);
    }

    public bool CheckMouseOver(Player player, Vector2 offset) => this.enrichmentInfoPanel != null && this.enrichmentInfoPanel.CheckMouseOver(player, offset);

    public void DrawEnrichmentInfoManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.enrichmentInfoPanel == null)
        return;
      this.enrichmentInfoPanel.DrawEnrichmentInfoPanel(offset, spriteBatch);
    }
  }
}
