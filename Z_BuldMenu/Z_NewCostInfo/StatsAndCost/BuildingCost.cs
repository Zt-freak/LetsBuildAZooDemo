// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.Z_NewCostInfo.StatsAndCost.BuildingCost
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_BuldMenu.Z_NewCostInfo.PenBuildInfo;

namespace TinyZoo.Z_BuldMenu.Z_NewCostInfo.StatsAndCost
{
  internal class BuildingCost
  {
    public Vector2 Location;
    public PenBuildInfoManager costmanager;

    public BuildingCost(TILETYPE buldingtype, Player player, float BaseScale, float BaseWidth) => this.costmanager = new PenBuildInfoManager(buldingtype, player, BaseScale, true, BaseWidth);

    public void FlashRedCantAfford() => this.costmanager.FlashRedCantAfford();

    public Vector2 GetSize() => this.costmanager.GetSize();

    public void UpdateBuildingCost(
      Player player,
      int TileCount,
      Z_PenBuilder z_penbuilder,
      float DeltaTime)
    {
      this.costmanager.UpdatePenBuildInfoManager(player, TileCount, z_penbuilder, DeltaTime);
    }

    public void DrawBuildingCost(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.costmanager.DrawPenBuildInfoManager(Offset, spritebatch);
    }
  }
}
