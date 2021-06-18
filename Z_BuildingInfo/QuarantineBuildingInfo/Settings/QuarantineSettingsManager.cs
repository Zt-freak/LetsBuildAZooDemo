// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.QuarantineBuildingInfo.Settings.QuarantineSettingsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_BuildingInfo.QuarantineBuildingInfo.Settings
{
  internal class QuarantineSettingsManager
  {
    private QuarantineSettingsPanel panel;

    public QuarantineSettingsManager(Player player)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.panel = new QuarantineSettingsPanel(player, baseScaleForUi);
      this.panel.location = new Vector2(512f, 384f);
    }

    public bool CheckMouseOver(Player player) => this.panel.CheckMouseOver(player, Vector2.Zero);

    public bool UpdateQuarantineSettingsManager(Player player, float DeltaTime) => this.panel.UpdateQuarantineSettingsPanel(player, DeltaTime, Vector2.Zero);

    public void DrawQuarantineSettingsManager() => this.panel.DrawQuarantineSettingsPanel(Vector2.Zero, AssetContainer.pointspritebatchTop05);
  }
}
