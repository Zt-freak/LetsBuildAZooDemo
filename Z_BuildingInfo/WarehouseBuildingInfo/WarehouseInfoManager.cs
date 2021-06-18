// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.WarehouseBuildingInfo.WarehouseInfoManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuildingInfo.WarehouseBuildingInfo
{
  internal class WarehouseInfoManager
  {
    private WarehouseInfoPanel panel;

    public WarehouseInfoManager(int BuildingUID, TILETYPE tileType, Player player)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.panel = new WarehouseInfoPanel(player.shopstatus.GetThisFacility(BuildingUID), player, baseScaleForUi);
      this.panel.location = new Vector2(512f, 384f);
    }

    public bool CheckMouseOver(Player player) => this.panel.CheckMouseOver(player, Vector2.Zero);

    public bool UpdateWarehouseInfoManager(Player player, float DeltaTime) => this.panel.UpdateWarehouseInfoPanel(player, DeltaTime, Vector2.Zero);

    public void DrawWarehouseInfoManager() => this.panel.DrawWarehouseInfoPanel(Vector2.Zero, AssetContainer.pointspritebatchTop05);
  }
}
