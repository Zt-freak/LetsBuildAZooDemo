// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HeatMaps.HeatMapManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;

namespace TinyZoo.Z_HeatMaps
{
  internal class HeatMapManager
  {
    private WaterMap watermap;
    public AnimalPrivacyMap animalprivacymap;
    internal static DecoMap decomap;
    private HygieneMap fooddrinksmap;

    public void DoubleCheckWaterSetUp(Player player)
    {
      if (this.watermap == null)
        this.watermap = new WaterMap(player);
      else
        this.watermap.UpdateWaterMap(player);
    }

    public void DoubleCheckAnimalPrivacySetUp(Player player)
    {
      if (this.animalprivacymap == null)
        this.animalprivacymap = new AnimalPrivacyMap(player);
      else
        this.animalprivacymap.UpdateAnimalPrivacyMap(player);
    }

    public void UpdateHeatMapManager(Player player, float DeltaTime)
    {
      switch (Z_GameFlags.DRAW_heatmaptype)
      {
        case HeatMapType.Water:
          if (this.watermap == null)
            this.watermap = new WaterMap(player);
          this.watermap.UpdateWaterMap(player);
          break;
        case HeatMapType.AnimalPrivacy:
          if (this.animalprivacymap == null)
            this.animalprivacymap = new AnimalPrivacyMap(player);
          this.animalprivacymap.UpdateAnimalPrivacyMap(player);
          break;
        case HeatMapType.Deco:
          HeatMapManager.decomap.DecoMapUpdateDecoMap(DeltaTime);
          break;
        case HeatMapType.Hygiene:
          if (this.fooddrinksmap == null)
            this.fooddrinksmap = new HygieneMap(player);
          this.fooddrinksmap.UpdateHygieneMap(player);
          break;
      }
    }

    public bool IsWaterMapMade() => this.watermap != null;

    public bool GetHasWaterAccess(int XLoc, int YLoc)
    {
      if (this.watermap != null)
        return this.watermap.GetHasWaterAccess(XLoc, YLoc);
      throw new Exception("YOU NEDED TO MAKE THIS THING");
    }

    public void DrawHeatMapManager()
    {
      switch (Z_GameFlags.DRAW_heatmaptype)
      {
        case HeatMapType.Water:
          if (this.watermap == null)
            break;
          this.watermap.DrawWaterMap();
          break;
        case HeatMapType.AnimalPrivacy:
          if (this.animalprivacymap == null)
            break;
          this.animalprivacymap.DrawAnimalPrivacyMap();
          break;
        case HeatMapType.Deco:
          if (HeatMapManager.decomap == null)
            break;
          HeatMapManager.decomap.DrawDecoMap();
          break;
        case HeatMapType.Hygiene:
          if (this.fooddrinksmap == null)
            break;
          this.fooddrinksmap.DrawFoodDrinksShopsMap();
          break;
      }
    }
  }
}
