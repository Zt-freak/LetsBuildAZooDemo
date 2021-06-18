// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.ZooWeek.DayBoxInternals.InternalStaff
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_WeekOver.StaffPayment;

namespace TinyZoo.Z_WeekOver.ZooWeek.DayBoxInternals
{
  internal class InternalStaff
  {
    private List<EmplyeeAndStats> employees;

    public InternalStaff(List<AnimalType> NewEmployees)
    {
      this.employees = new List<EmplyeeAndStats>();
      for (int index = 0; index < NewEmployees.Count; ++index)
      {
        this.employees.Add(new EmplyeeAndStats(NewEmployees[index]));
        float num = 80f / (float) (this.employees.Count + 1);
        this.employees[index].vLocation = new Vector2(num * (float) index, 0.0f);
        this.employees[index].vLocation.X -= 40f;
        this.employees[index].vLocation.X += num;
        this.employees[index].scale = RenderMath.GetPixelSizeBestMatch(2f);
      }
    }

    public void UpdateInternalStaff(float DeltaTime)
    {
      for (int index = 0; index < this.employees.Count; ++index)
        this.employees[index].UpdateEmplyeeAndStats(DeltaTime);
    }

    public void DrawInternalStaff(Vector2 Offset)
    {
      for (int index = 0; index < this.employees.Count; ++index)
        this.employees[index].DrawEmplyeeAndStats(Offset);
    }
  }
}
