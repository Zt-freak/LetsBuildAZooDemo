// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Processing.Z_ProcessingManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuildingInfo.Factories;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel;

namespace TinyZoo.Z_Processing
{
  internal class Z_ProcessingManager
  {
    private MeatProcessingPanel panel;
    private FactoryInfoPanel factoryInfoPanel;

    public Z_ProcessingManager(int BuildingUID, TILETYPE tileType, Player player)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      if (TileData.IsAMeatProcessingPlant(tileType) || TileData.IsAVegetableProcessingPlant(tileType))
      {
        player.animalProcessing.GetBuildingByUID(BuildingUID, out int _);
        this.panel = new MeatProcessingPanel(BuildingUID, tileType, player, baseScaleForUi);
        this.panel.location = new Vector2(650f, 384f);
      }
      else
      {
        this.factoryInfoPanel = new FactoryInfoPanel(BuildingUID, tileType, player, baseScaleForUi);
        this.factoryInfoPanel.location = new Vector2(512f, 384f);
      }
    }

    public bool CheckMouseOver(Player player)
    {
      if (this.factoryInfoPanel != null)
        return this.factoryInfoPanel.CheckMouseOver(player, Vector2.Zero);
      return this.panel != null && this.panel.CheckMouseOver(player, Vector2.Zero);
    }

    public bool UpdateZ_ProcessingManager(float DeltaTime, Player player)
    {
      if (this.panel != null)
        return this.panel.UpdateMeatProcessingPanel(player, DeltaTime, Vector2.Zero);
      return this.factoryInfoPanel != null && this.factoryInfoPanel.UpdateFactoryInfoPanel(player, DeltaTime, Vector2.Zero);
    }

    public void DrawZ_ProcessingManager()
    {
      if (this.panel != null)
      {
        this.panel.DrawMeatProcessingPanel(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      }
      else
      {
        if (this.factoryInfoPanel == null)
          return;
        this.factoryInfoPanel.DrawFactoryInfoPanel(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      }
    }
  }
}
