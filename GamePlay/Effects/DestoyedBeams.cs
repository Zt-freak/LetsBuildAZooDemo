// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Effects.DestoyedBeams
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.beams;

namespace TinyZoo.GamePlay.Effects
{
  internal class DestoyedBeams
  {
    internal static List<Vector2> BeamsDooms = new List<Vector2>();
    private List<Explosion> explosions;

    public DestoyedBeams() => this.explosions = new List<Explosion>();

    public void UpdateDestoyedBeams(float DeltaTime)
    {
      if (DestoyedBeams.BeamsDooms.Count > 0)
      {
        for (int index1 = 0; index1 < DestoyedBeams.BeamsDooms.Count; ++index1)
        {
          bool flag = false;
          for (int index2 = 0; index2 < this.explosions.Count; ++index2)
          {
            if (!this.explosions[index2].bActive && !flag)
            {
              flag = true;
              this.explosions[index2].Reset(DestoyedBeams.BeamsDooms[index1]);
            }
          }
          if (!flag)
            this.explosions.Add(new Explosion(DestoyedBeams.BeamsDooms[index1]));
        }
        CameraShake.BeginCameraShake(TinyZoo.Game1.Rnd);
        DestoyedBeams.BeamsDooms = new List<Vector2>();
      }
      for (int index = 0; index < this.explosions.Count; ++index)
        this.explosions[index].UpdateExplosion(DeltaTime);
    }

    public void DrawDestoyedBeams()
    {
      for (int index = 0; index < this.explosions.Count; ++index)
        this.explosions[index].DrawExplosion();
    }
  }
}
