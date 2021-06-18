// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.Cohabitation.ThreatPackV2
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;

namespace TinyZoo.Z_BalanceSystems.Animals.Cohabitation
{
  internal class ThreatPackV2
  {
    public AnimalStat AnimalStatsForThisAnimal;
    public SimpleThreatPack simplethreatpack;
    private List<ThreatEntry> threatentries;
    private float TotalThreat;

    public ThreatPackV2(SimpleThreatPack _simplethreatpack)
    {
      this.simplethreatpack = _simplethreatpack;
      this.threatentries = new List<ThreatEntry>();
    }

    public void AddThreat(
      float Diff,
      float TotalPackSizeMultiplier,
      AnimalType Predator,
      int TotalOfThisEnemy)
    {
      this.threatentries.Add(new ThreatEntry(Diff, TotalPackSizeMultiplier, Predator, TotalOfThisEnemy));
      if ((double) TotalPackSizeMultiplier < 1.0)
      {
        float num = (float) ((double) TotalPackSizeMultiplier * (double) Diff * 0.5);
        Diff = Diff * 0.5f + num;
      }
      this.TotalThreat += Diff * (float) (0.75 + (double) TotalOfThisEnemy / 4.0);
    }

    public NotificationAlertStatus GetAlerStatus()
    {
      if ((double) this.TotalThreat > 5.0)
        return NotificationAlertStatus.Danger_Worst;
      return (double) this.TotalThreat > 0.00999999977648258 ? NotificationAlertStatus.Exclamation : NotificationAlertStatus.Tick;
    }
  }
}
