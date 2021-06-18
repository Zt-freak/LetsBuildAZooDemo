// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.ParkSummary.ParkSummaryManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_SummaryPopUps.ParkSummary
{
  internal class ParkSummaryManager
  {
    private ParkSummaryPanel parkSummaryPanel;

    public ParkSummaryManager(Player player)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.parkSummaryPanel = new ParkSummaryPanel(player, baseScaleForUi);
      this.parkSummaryPanel.location = new Vector2(512f, 384f);
    }

    public bool CheckMouseOver(Player player) => this.parkSummaryPanel.CheckMouseOver(player, Vector2.Zero);

    public bool UpdateParkSummaryManager(Player player, float DeltaTime) => this.parkSummaryPanel.UpdateParkSummaryPanel(player, DeltaTime, Vector2.Zero);

    public void DrawParkSummaryManager() => this.parkSummaryPanel.DrawParkSummaryPanel(Vector2.Zero, AssetContainer.pointspritebatchTop05);
  }
}
