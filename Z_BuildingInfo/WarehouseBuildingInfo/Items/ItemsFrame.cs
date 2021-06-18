// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.WarehouseBuildingInfo.Items.ItemsFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BuildingInfo.WarehouseBuildingInfo.Items
{
  internal class ItemsFrame
  {
    public Vector2 location;
    private ProcessingValueFrame frame;

    public ItemsFrame(ShopEntry shopEntry, Player player, float BaseScale, float ForcedHeight)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.frame = new ProcessingValueFrame(shopEntry.ShopUID, shopEntry.tiletype, player, BaseScale, ForcedHeight);
    }

    public Vector2 GetSize() => this.frame.GetSize();

    public void UpdateItemsFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.frame.UpdateProcessingValueFrame(player, DeltaTime, offset);
    }

    public void DrawItemsFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.frame.DrawProcessingValueFrame(offset, spriteBatch);
    }
  }
}
