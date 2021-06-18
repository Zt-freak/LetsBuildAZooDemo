// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Particles.ParticleManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_Particles
{
  internal class ParticleManager
  {
    private List<Z_Particle> particles;
    private int LastParticleUsed;
    private int MaxPool = 500;

    public ParticleManager() => this.particles = new List<Z_Particle>();

    public List<Z_Particle> SpawnParticle(
      Vector2 Location,
      float FloorHeight,
      float FloorMax,
      int Total,
      ParticleType particle,
      bool ReturnList = true,
      AnimalFoodType foodtype = AnimalFoodType.Count)
    {
      List<Z_Particle> zParticleList = new List<Z_Particle>();
      while (Total > 0)
      {
        int lastParticleUsed = this.LastParticleUsed;
        for (; this.LastParticleUsed < this.particles.Count; ++this.LastParticleUsed)
        {
          int count = this.particles.Count;
          if (!this.particles[this.LastParticleUsed].bActive)
          {
            this.particles[this.LastParticleUsed].SpawnParticle(Location, particle, MathStuff.getRandomFloat(FloorHeight, FloorMax));
            zParticleList.Add(this.particles[this.LastParticleUsed]);
            --Total;
            if (Total == 0)
              return zParticleList;
          }
        }
        for (this.LastParticleUsed = 0; this.LastParticleUsed < lastParticleUsed; ++this.LastParticleUsed)
        {
          if (!this.particles[this.LastParticleUsed].bActive)
          {
            this.particles[this.LastParticleUsed].SpawnParticle(Location, particle, MathStuff.getRandomFloat(FloorHeight, FloorMax));
            zParticleList.Add(this.particles[this.LastParticleUsed]);
            --Total;
            if (Total == 0)
              return zParticleList;
          }
        }
        if (this.particles.Count >= this.MaxPool)
          return zParticleList;
        while (Total > 0)
        {
          --Total;
          if (this.particles.Count >= this.MaxPool)
            return zParticleList;
          Z_Particle zParticle = new Z_Particle();
          zParticle.SpawnParticle(Location, particle, MathStuff.getRandomFloat(FloorHeight, FloorMax));
          this.particles.Add(zParticle);
          zParticleList.Add(zParticle);
        }
      }
      return zParticleList;
    }

    public void UpdateParticleManager(float DeltaTime)
    {
      for (int index = 0; index < this.particles.Count; ++index)
        this.particles[index].UpdateZ_Particle(DeltaTime);
    }

    public void DrawParticleManager()
    {
    }
  }
}
