// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Effects.FlashEffect
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.GamePlay.Effects.DamageEffects;

namespace TinyZoo.GamePlay.Effects
{
  internal class FlashEffect
  {
    public bool BActive;
    private List<DamageParticls> particles;

    public FlashEffect()
    {
      this.particles = new List<DamageParticls>();
      for (int Index = 0; Index < 30; ++Index)
        this.particles.Add(new DamageParticls(Index));
    }

    public void DoFlash(Vector2 Location)
    {
      for (int index = 0; index < 30; ++index)
        this.particles[index].LaunchParticle(Location);
      this.BActive = true;
    }

    public void UpdateFlashEffect(float DeltaTime)
    {
      if (!this.BActive)
        return;
      this.BActive = false;
      for (int index = 0; index < 30; ++index)
      {
        this.particles[index].UpdateDamageParticls(DeltaTime);
        if ((double) this.particles[index].fAlpha > 0.0)
          this.BActive = true;
      }
    }

    public void DrawFlashEffect()
    {
      if (!this.BActive)
        return;
      for (int index = 0; index < 30; ++index)
        this.particles[index].DrawParticle();
    }
  }
}
