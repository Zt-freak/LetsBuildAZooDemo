// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras.BuildingDistance
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.PlayerDir.Shops;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras
{
  internal class BuildingDistance
  {
    private float Distance;
    public ShopEntry REF_shopentry;

    public BuildingDistance(Vector2Int LocationA, Vector2Int LocationB, ShopEntry shopentry)
    {
      this.Distance = (new Vector2((float) LocationA.X, (float) LocationA.Y) - new Vector2((float) LocationB.X, (float) LocationB.Y)).LengthSquared();
      this.REF_shopentry = shopentry;
    }

    internal static int SortBuildingDistance(BuildingDistance A, BuildingDistance B)
    {
      if ((double) A.Distance < (double) B.Distance)
        return -1;
      return (double) A.Distance < (double) B.Distance ? 1 : 0;
    }
  }
}
