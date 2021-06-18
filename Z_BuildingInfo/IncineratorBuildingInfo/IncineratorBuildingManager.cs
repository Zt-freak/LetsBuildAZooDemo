// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.IncineratorBuildingInfo.IncineratorBuildingManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuildingInfo.IncineratorBuildingInfo
{
  internal class IncineratorBuildingManager
  {
    private IncineratorBuildingPanel panel;

    public IncineratorBuildingManager(int BuildingUID, TILETYPE tileTYPE, Player player)
    {
      double baseScaleForUi = (double) Z_GameFlags.GetBaseScaleForUI();
      this.panel = new IncineratorBuildingPanel(BuildingUID, tileTYPE, player);
      this.panel.location = new Vector2(512f, 384f);
    }

    public bool CheckMouseOver(Player player) => this.panel.CheckMouseOver(player, Vector2.Zero);

    public bool UpdateIncineratorBuildingManager(Player player, float DeltaTime) => this.panel.UpdateIncineratorBuildingPanel(player, DeltaTime, Vector2.Zero);

    public void DrawIncineratorBuildingManager() => this.panel.DrawIncineratorBuildingPanel(Vector2.Zero, AssetContainer.pointspritebatchTop05);
  }
}
