// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Progress.ProgressSystem
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;

namespace TinyZoo.GamePlay.Progress
{
  internal class ProgressSystem
  {
    private Decimal TargetPercent;
    public bool IsGoingToNextLevel;

    public ProgressSystem(int ThisLevel)
    {
      switch (ThisLevel)
      {
        case 0:
          this.TargetPercent = 0.7M;
          break;
        case 1:
          this.TargetPercent = 0.75M;
          break;
        case 2:
          this.TargetPercent = 70M;
          break;
        case 3:
          this.TargetPercent = 60M;
          break;
        case 4:
          this.TargetPercent = 50M;
          break;
        default:
          this.TargetPercent = 50M;
          break;
      }
      this.TargetPercent = 0.75M;
      if (GameFlags.IsArcadeMode && GameFlags.DifficultyIsEasy)
      {
        switch (GameFlags.ArcadeLevel)
        {
          case 23:
            this.TargetPercent = 0.5M;
            break;
          case 27:
            this.TargetPercent = 0.69M;
            break;
        }
      }
      GameFlags.TargetPercent = (float) this.TargetPercent;
    }

    public void UpdateProgressSystem()
    {
      int num = GameFlags.CurrentReclamedZones / GameFlags.FullZoneSize * 100M >= this.TargetPercent ? 1 : 0;
    }
  }
}
