// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.ProductionLines.ProductionLineCalc
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.Z_BalanceSystems.ProductionLines
{
  internal class ProductionLineCalc
  {
    internal static TotalsAndBuildings[] totalsAndBuildings = new TotalsAndBuildings[88];

    internal static void ReinitialzeOnGameStart() => ProductionLineCalc.totalsAndBuildings = new TotalsAndBuildings[88];

    internal static void StartNewDay(Player player)
    {
      ProductionLineCalc.totalsAndBuildings = new TotalsAndBuildings[88];
      for (int index = 0; index < player.shopstatus.FacilitiesWithEmployees.Count; ++index)
        player.shopstatus.FacilitiesWithEmployees[index].StartNewDay();
      for (int index = 0; index < player.shopstatus.shopentries.Count; ++index)
        player.shopstatus.shopentries[index].StartNewDay();
      for (int index = 0; index < player.shopstatus.Toilets.Count; ++index)
        player.shopstatus.Toilets[index].StartNewDay();
      for (int index = 0; index < player.shopstatus.Bins.Count; ++index)
        player.shopstatus.Bins[index].StartNewDay();
      for (int index = 0; index < player.shopstatus.Benches.Count; ++index)
        player.shopstatus.Benches[index].StartNewDay();
    }
  }
}
