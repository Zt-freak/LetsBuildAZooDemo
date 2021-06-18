// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment.PerchInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment
{
  internal class PerchInfo
  {
    public TILETYPE TileType;
    private List<PurchLocation> Perchpoints_Rot0;
    private List<PurchLocation> Perchpoints_Rot1;
    private List<PurchLocation> Perchpoints_Rot2;
    private List<PurchLocation> Perchpoints_Rot3;

    public PerchInfo(
      TILETYPE tileType,
      Vector2 FirstPoint,
      int RotationForThisPoint = 0,
      bool DrawBehind = false)
    {
      this.TileType = tileType;
      if (RotationForThisPoint != 0)
        throw new Exception("Please use Add point for this! I was too lasy to type a switch case!!!");
      this.Perchpoints_Rot0 = new List<PurchLocation>();
      this.Perchpoints_Rot0.Add(new PurchLocation(FirstPoint, DrawBehind));
      this.Perchpoints_Rot1 = new List<PurchLocation>();
      this.Perchpoints_Rot2 = new List<PurchLocation>();
      this.Perchpoints_Rot3 = new List<PurchLocation>();
    }

    public void AddPoint(Vector2 ExtraPoint, int RotationClockWise, bool DrawAnimalBehindItem = false)
    {
      switch (RotationClockWise)
      {
        case 0:
          this.Perchpoints_Rot0.Add(new PurchLocation(ExtraPoint, DrawAnimalBehindItem));
          break;
        case 1:
          this.Perchpoints_Rot1.Add(new PurchLocation(ExtraPoint, DrawAnimalBehindItem));
          break;
        case 2:
          this.Perchpoints_Rot2.Add(new PurchLocation(ExtraPoint, DrawAnimalBehindItem));
          break;
        case 3:
          this.Perchpoints_Rot3.Add(new PurchLocation(ExtraPoint, DrawAnimalBehindItem));
          break;
      }
    }

    public List<PurchLocation> GetPerchPoints(int RotationClockWise)
    {
      switch (RotationClockWise)
      {
        case 0:
          return this.Perchpoints_Rot0;
        case 1:
          return this.Perchpoints_Rot1;
        case 2:
          return this.Perchpoints_Rot2;
        default:
          return this.Perchpoints_Rot3;
      }
    }
  }
}
