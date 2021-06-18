// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.BoxZones.ClearParticles
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TinyZoo.GamePlay.BoxZones
{
  internal class ClearParticles
  {
    private List<BoxParticle> parties;

    public ClearParticles(Vector2 TopLeft, Vector2 BottomRight)
    {
      double num1 = (double) (BottomRight - TopLeft).X * (double) (BottomRight - TopLeft).Y;
      this.parties = new List<BoxParticle>();
      int num2 = (int) (num1 * 0.025000000372529);
      for (int index = 0; index < num2; ++index)
        this.parties.Add(new BoxParticle(TopLeft, BottomRight));
    }

    public void UpdateClearParticles(float DeltaTime)
    {
      for (int index = 0; index < this.parties.Count; ++index)
        this.parties[index].UpdateBoxParticle(DeltaTime);
    }

    public void DrawClearParticles()
    {
      for (int index = 0; index < this.parties.Count; ++index)
        this.parties[index].DrawBoxParticle();
    }
  }
}
