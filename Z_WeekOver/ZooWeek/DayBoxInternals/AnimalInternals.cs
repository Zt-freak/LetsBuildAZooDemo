// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.ZooWeek.DayBoxInternals.AnimalInternals
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.NewDiscoveryScreen;

namespace TinyZoo.Z_WeekOver.ZooWeek.DayBoxInternals
{
  internal class AnimalInternals
  {
    private List<AnimalRenderer> animals;

    public AnimalInternals(List<Vector2Int> _animals)
    {
      this.animals = new List<AnimalRenderer>();
      for (int index = 0; index < _animals.Count; ++index)
      {
        this.animals.Add(new AnimalRenderer((AnimalType) _animals[index].X, _animals[index].Y));
        float num = 80f / (float) (this.animals.Count + 1);
        this.animals[index].enemy.vLocation = new Vector2(num * (float) index, 0.0f);
        this.animals[index].enemy.vLocation.X -= 40f;
        this.animals[index].enemy.vLocation.X += num;
        this.animals[index].enemy.scale = RenderMath.GetPixelSizeBestMatch(2f);
      }
    }

    public void UpdateInternalStaff(float DeltaTime)
    {
      for (int index = 0; index < this.animals.Count; ++index)
        this.animals[index].UpdateAnimal(DeltaTime);
    }

    public void DrawInternalStaff(Vector2 Offset)
    {
      for (int index = 0; index < this.animals.Count; ++index)
        this.animals[index].DrawAnimal(Offset);
    }
  }
}
