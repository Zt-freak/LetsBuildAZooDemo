// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Events.BreakOut.BreakoutByPen
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_AnimalsAndPeople;

namespace TinyZoo.Z_Events.BreakOut
{
  internal class BreakoutByPen
  {
    public PrisonZone REF_prisonzone;
    public List<AnimalRenderMan> EscapedAnimals = new List<AnimalRenderMan>();

    public BreakoutByPen(PrisonZone prisonzone)
    {
      this.EscapedAnimals = new List<AnimalRenderMan>();
      this.REF_prisonzone = prisonzone;
    }

    public void Scrubnimals(ref int NewAnimalsDead)
    {
      for (int index = this.EscapedAnimals.Count - 1; index > -1; --index)
      {
        if (this.EscapedAnimals[index].REF_prisonerinfo.IsDead)
        {
          this.EscapedAnimals.RemoveAt(index);
          ++NewAnimalsDead;
        }
      }
    }

    public void AddEscapedAnimal(AnimalRenderMan animal) => this.EscapedAnimals.Add(animal);

    public AnimalRenderMan GetAnimalToHunt() => this.EscapedAnimals.Count > 0 ? this.EscapedAnimals[Game1.Rnd.Next(0, this.EscapedAnimals.Count)] : (AnimalRenderMan) null;
  }
}
