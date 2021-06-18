// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.QuarantineBuildingInfo.QuaratinedAnimalsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.PlayerDir.Quarantine;

namespace TinyZoo.Z_BuildingInfo.QuarantineBuildingInfo
{
  internal class QuaratinedAnimalsManager
  {
    private QuarantineAnimalsMainPanel panel;

    public QuaratinedAnimalsManager(QuarantineBuilding quarantineBuilding, Player player)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.panel = new QuarantineAnimalsMainPanel(quarantineBuilding, player, baseScaleForUi);
      this.panel.location = new Vector2(512f, 384f);
    }

    public bool CheckMouseOver(Player player) => this.panel.CheckMouseOver(player, Vector2.Zero);

    public bool UpdateQuarantinedAnimalsManager(Player player, float DeltaTime) => this.panel.UpdateQuarantineAnimalsMainPanel(player, DeltaTime, Vector2.Zero);

    public void DrawQuarantinedAnimalsManager() => this.panel.DrawQuarantineAnimalsMainPanel(Vector2.Zero, AssetContainer.pointspritebatchTop05);
  }
}
