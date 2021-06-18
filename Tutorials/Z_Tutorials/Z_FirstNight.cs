// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.Z_Tutorials.Z_FirstNight
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_DayNight;

namespace TinyZoo.Tutorials.Z_Tutorials
{
  internal class Z_FirstNight
  {
    private SmartCharacterBox charactertextbox;

    public Z_FirstNight() => this.charactertextbox = new SmartCharacterBox("Congratulations, your zoo is off to a great start!~I can't wait to see what tomorrow brings!", AnimalType.Administrator, _ScaleMult: Sengine.UltraWideSreenDownardsMultiplier);

    public bool UpdateZ_FirstNight(ref float SimulationTime, ref float DeltaTime, Player player)
    {
      if ((double) DayNightManager.NightLerpValue == 1.0)
      {
        FeatureFlags.BlockPersonInfo = true;
        FeatureFlags.BlockAllUI = true;
        SimulationTime = 0.0f;
        if (this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player))
        {
          FeatureFlags.BlockPersonInfo = false;
          FeatureFlags.BlockAllUI = false;
          return true;
        }
      }
      return false;
    }

    public void DrawZ_FirstNight() => this.charactertextbox.DrawSmartCharacterBox();
  }
}
