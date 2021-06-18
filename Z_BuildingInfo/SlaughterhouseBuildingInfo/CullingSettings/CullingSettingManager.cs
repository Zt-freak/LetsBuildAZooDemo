// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.CullingSettingManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.Popup;

namespace TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings
{
  internal class CullingSettingManager
  {
    private CullingSettingPanel panel;
    private EditCullingPopup popUpPanel;
    private float BaseScale;

    public CullingSettingManager(Player player)
    {
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      this.panel = new CullingSettingPanel(player, this.BaseScale);
      this.panel.location = new Vector2(512f, 384f);
    }

    public bool CheckMouseOver(Player player) => this.panel.CheckMouseOver(player, Vector2.Zero);

    public bool UpdateCullingSettingManager(Player player, float DeltaTime)
    {
      AnimalType editThisAnimal;
      if (this.panel.UpdateCullingSettingPanel(player, DeltaTime, Vector2.Zero, out editThisAnimal))
        return true;
      if (editThisAnimal != AnimalType.None)
      {
        this.popUpPanel = new EditCullingPopup(editThisAnimal, this.BaseScale);
        this.popUpPanel.location = this.panel.location;
        this.panel.SetIsActive(false);
      }
      if (this.popUpPanel != null && this.popUpPanel.UpdateEditCullingPopup(player, DeltaTime, Vector2.Zero))
      {
        this.popUpPanel = (EditCullingPopup) null;
        this.panel.SetIsActive(true);
      }
      return false;
    }

    public void DrawCullingSettingManager()
    {
      this.panel.DrawCullingSettingPanel(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      if (this.popUpPanel == null)
        return;
      this.popUpPanel.DrawEditCullingPopup(Vector2.Zero, AssetContainer.pointspritebatchTop05);
    }
  }
}
