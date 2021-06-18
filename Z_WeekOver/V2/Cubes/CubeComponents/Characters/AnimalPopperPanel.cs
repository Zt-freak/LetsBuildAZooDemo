// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents.Characters.AnimalPopperPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;

namespace TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents.Characters
{
  internal class AnimalPopperPanel
  {
    private List<PeoplePopper> peoplepoppers;
    private int ActivePoppers;

    public AnimalPopperPanel(
      List<IntakePerson> Peoples,
      List<Employee> employees,
      float BaseScale,
      float ArrayWidth,
      float AnimalSize = 80f)
    {
      this.peoplepoppers = new List<PeoplePopper>();
      if (Peoples != null)
      {
        for (int index = 0; index < Peoples.Count; ++index)
          this.peoplepoppers.Add(new PeoplePopper(Peoples[index], BaseScale, 0.3f, AnimalSize));
      }
      if (employees != null)
      {
        for (int index = 0; index < employees.Count; ++index)
          this.peoplepoppers.Add(new PeoplePopper(employees[index], BaseScale, 0.3f, AnimalSize));
      }
      if (this.peoplepoppers.Count <= 1)
        return;
      float num = (float) ((double) ArrayWidth / (double) this.peoplepoppers.Count - 1.0);
      for (int index = 0; index < this.peoplepoppers.Count; ++index)
      {
        this.peoplepoppers[index].Location.X = num * (float) index;
        this.peoplepoppers[index].Location.X -= ArrayWidth * 0.5f;
      }
    }

    public int UpdateAnimalPopperPanel(float DeltaTime, Player player)
    {
      int num = 0;
      for (int index = 0; index < this.peoplepoppers.Count; ++index)
      {
        if (this.peoplepoppers[index].UpdatePeoplePopper(DeltaTime, this.ActivePoppers >= index))
        {
          ++this.ActivePoppers;
          ++num;
        }
      }
      return num;
    }

    public bool LerpComplete()
    {
      bool flag = true;
      for (int index = 0; index < this.peoplepoppers.Count; ++index)
      {
        if (!this.peoplepoppers[index].LerpComplete())
          flag = false;
      }
      return flag;
    }

    public void DrawAnimalPopperPanel(Vector2 Offset, SpriteBatch spritebatch)
    {
      for (int index = 0; index < this.peoplepoppers.Count; ++index)
        this.peoplepoppers[index].DrawPeoplePopper(Offset, spritebatch);
    }
  }
}
