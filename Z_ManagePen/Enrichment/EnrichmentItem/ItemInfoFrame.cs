// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Enrichment.EnrichmentItem.ItemInfoFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.Z_ManagePen.Enrichment.EnrichmentItem
{
  internal class ItemInfoFrame
  {
    public Vector2 location;
    private SimpleBuildingRenderer itemRenderer;
    private ZGenericText itemName;
    private SimpleTextHandler itemDesc;
    private float refBaseScale;
    private UIScaleHelper _scaleHelper;
    private float refforceWidth;
    private Vector2 size;

    public ItemInfoFrame(TILETYPE selectedItem, float BaseScale, float forceWidth)
    {
      this.refBaseScale = BaseScale;
      this.refforceWidth = forceWidth;
      this._scaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = this._scaleHelper.DefaultBuffer;
      this.itemName = new ZGenericText("No Item Selected", BaseScale, false, _UseOnePointFiveFont: true);
      this.size = Vector2.Zero;
      if (selectedItem == TILETYPE.None)
        return;
      Vector2 vector2 = this._scaleHelper.ScaleVector2(Vector2.One * 30f);
      this.itemRenderer = new SimpleBuildingRenderer(selectedItem);
      this.itemRenderer.SetSize(vector2.X, this.refBaseScale);
      TileStats tileStats = TileData.GetTileStats(selectedItem);
      this.itemName.textToWrite = tileStats.Name;
      this.itemDesc = new SimpleTextHandler(tileStats.Description, (float) ((double) this.refforceWidth - (double) vector2.X - (double) defaultBuffer.X * 2.0), _Scale: this.refBaseScale);
      this.itemDesc.AutoCompleteParagraph();
      this.itemDesc.SetAllColours(ColourData.Z_Cream);
      this.itemRenderer.vLocation = this.size;
      SimpleBuildingRenderer itemRenderer = this.itemRenderer;
      itemRenderer.vLocation = itemRenderer.vLocation + vector2 * 0.25f;
      this.size.X += vector2.X;
      this.size.X += defaultBuffer.X;
      this.itemName.vLocation.X = this.size.X;
      this.size.Y += this.itemName.GetSize().Y;
      this.itemDesc.Location = this.size;
      this.size.Y = Math.Max(vector2.Y, this.itemName.GetSize().Y + this.itemDesc.GetHeightOfParagraph());
    }

    public Vector2 GetSize() => this.size;

    public void UpdateItemInfoFrame()
    {
    }

    public void DrawItemInfoFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.itemName.DrawZGenericText(offset, spriteBatch);
      this.itemDesc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      if (this.itemRenderer == null)
        return;
      this.itemRenderer.DrawSimpleBuildingRenderer(offset, spriteBatch);
    }
  }
}
