// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.AllShopSummary.Row.C01_Shop
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.OverWorld.OverWorldBuildMenu;
using TinyZoo.Tile_Data;
using TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood;

namespace TinyZoo.Z_ManageShop.AllShopSummary.Row
{
  internal class C01_Shop
  {
    private TopTextMini toptextmini;
    private TopTextMini toptextminiTo;
    private BIconAndCost buildicon;
    public Vector2 Location;

    public C01_Shop(
      float HeightForText,
      TILETYPE tiletype,
      Player player,
      float MidTextHeight,
      float BaseScale)
    {
      this.toptextmini = new TopTextMini("Shop", BaseScale, HeightForText, false, true);
      this.toptextmini.CenterJustify = true;
      this.toptextmini.vLocation.X = 10f;
      this.buildicon = new BIconAndCost(tiletype, 0, player, false);
      this.buildicon.Location = new Vector2(-15f, 0.0f);
      this.buildicon.Location.Y = MidTextHeight;
      this.buildicon.buldingcon.scale *= 0.666666f;
      this.toptextminiTo = new TopTextMini(TileData.GetTileStats(tiletype).Name, BaseScale, MidTextHeight - 5f, false, true);
      this.toptextminiTo.SetAsSplit();
    }

    public void DrawColumn(Vector2 Offset)
    {
      Offset += this.Location;
      this.toptextmini.DrawTopTextMini(Offset);
      this.buildicon.DrawBIconAndCost(Offset, false, 0.0f, AssetContainer.pointspritebatchTop05);
      this.toptextminiTo.DrawTopTextMini(Offset);
    }
  }
}
