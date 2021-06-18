// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Corpse.Flies
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TinyZoo.Z_AnimalsAndPeople.Corpse
{
  internal class Flies
  {
    private List<Fly> flies;

    public Flies()
    {
      this.flies = new List<Fly>();
      for (int index = 0; index < 6; ++index)
        this.flies.Add(new Fly());
    }

    public void UpdateFlies(float DeltaTime)
    {
      for (int index = 0; index < this.flies.Count; ++index)
        this.flies[index].UpdateFly(DeltaTime);
    }

    public void DrawFlies(Vector2 WorldSpaceLocation)
    {
      for (int index = 0; index < this.flies.Count; ++index)
        this.flies[index].DrawFly(WorldSpaceLocation);
    }
  }
}
