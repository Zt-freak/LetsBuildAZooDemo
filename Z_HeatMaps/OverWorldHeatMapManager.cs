// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HeatMaps.OverWorldHeatMapManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.Z_HeatMaps
{
  internal class OverWorldHeatMapManager
  {
    internal static HeatMapType lastSelectedHeatMap = HeatMapType.Water;

    public OverWorldHeatMapManager()
    {
      OverWorldHeatMapManager.lastSelectedHeatMap = HeatMapType.Water;
      Z_GameFlags.SetHeatMapType(OverWorldHeatMapManager.lastSelectedHeatMap);
    }

    public bool UpdateOverWorldHeatMapManager(Player player) => Z_GameFlags.DRAW_heatmaptype == HeatMapType.None;
  }
}
