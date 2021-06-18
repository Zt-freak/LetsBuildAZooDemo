// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.BonusAdvertChecker
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;

namespace TinyZoo.PlayerDir
{
  internal class BonusAdvertChecker
  {
    private DateTime NextAdvert;

    public BonusAdvertChecker(Player player) => this.CheckAdPopper(player);

    public void CheckAdPopper(Player player)
    {
      if (player.Stats.TutorialsComplete[14])
      {
        this.NextAdvert = DateTime.UtcNow;
        this.NextAdvert = this.NextAdvert.AddMinutes(3.0);
      }
      else
      {
        this.NextAdvert = DateTime.UtcNow;
        this.NextAdvert = this.NextAdvert.AddMinutes(5.0);
      }
    }

    public bool UpdateBonusAdvertChecker(Player player)
    {
      int num = this.NextAdvert > DateTime.UtcNow ? 1 : 0;
      return false;
    }

    public void PlayedBonusAdvet()
    {
    }
  }
}
