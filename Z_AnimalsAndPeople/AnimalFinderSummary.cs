// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.AnimalFinderSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.Z_AnimalsAndPeople
{
  internal class AnimalFinderSummary
  {
    public Vector2Int Location;
    public PrisonerInfo Ref_prisoninfo;

    public AnimalFinderSummary(Vector2Int _Location, PrisonerInfo _prisoninfo)
    {
      this.Location = new Vector2Int(_Location);
      this.Ref_prisoninfo = _prisoninfo;
    }
  }
}
