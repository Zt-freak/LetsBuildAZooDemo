// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Park.ParkDecoSector
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BalanceSystems.Park
{
  internal class ParkDecoSector
  {
    private HashSet<TILETYPE> tilesfoundhere;
    private List<Vector3Int> TilesTotalsAndFootPrints;
    private HashSet<TILETYPE> SignsFoundHere;
    private List<Vector3Int> Signs_TilesTotalsAndFootPrints;
    public float LastDecoValue;
    public float SignValue;
    public float Efficiency;
    public float SignEfficiency;

    public ParkDecoSector()
    {
      this.LastDecoValue = 0.0f;
      this.SignValue = 0.0f;
      this.tilesfoundhere = new HashSet<TILETYPE>();
      this.SignsFoundHere = new HashSet<TILETYPE>();
      this.TilesTotalsAndFootPrints = new List<Vector3Int>();
      this.Signs_TilesTotalsAndFootPrints = new List<Vector3Int>();
    }

    public void AddSign(TILETYPE tiletype)
    {
      if (this.SignsFoundHere.Contains(tiletype))
      {
        for (int index = 0; index < this.Signs_TilesTotalsAndFootPrints.Count; ++index)
        {
          if ((TILETYPE) this.Signs_TilesTotalsAndFootPrints[index].X == tiletype)
            ++this.Signs_TilesTotalsAndFootPrints[index].Y;
        }
      }
      else
      {
        this.SignsFoundHere.Add(tiletype);
        TileInfo tileInfo = TileData.GetTileInfo(tiletype);
        this.Signs_TilesTotalsAndFootPrints.Add(new Vector3Int((int) tiletype, 1, tileInfo.GetTileWidth(0) * tileInfo.GetTileHeight(0)));
      }
    }

    public void AddDeco(TILETYPE tiletype)
    {
      if (this.tilesfoundhere.Contains(tiletype))
      {
        for (int index = 0; index < this.TilesTotalsAndFootPrints.Count; ++index)
        {
          if ((TILETYPE) this.TilesTotalsAndFootPrints[index].X == tiletype)
            ++this.TilesTotalsAndFootPrints[index].Y;
        }
      }
      else
      {
        this.tilesfoundhere.Add(tiletype);
        TileInfo tileInfo = TileData.GetTileInfo(tiletype);
        this.TilesTotalsAndFootPrints.Add(new Vector3Int((int) tiletype, 1, tileInfo.GetTileWidth(0) * tileInfo.GetTileHeight(0)));
      }
    }

    public void CalculateDeco()
    {
      this.LastDecoValue = 0.0f;
      this.SignValue = 0.0f;
      float num1 = 75f;
      for (int index1 = 0; index1 < this.TilesTotalsAndFootPrints.Count; ++index1)
      {
        float num2 = 1f;
        for (int index2 = 0; index2 < this.TilesTotalsAndFootPrints[index1].Y; ++index2)
        {
          if (index2 > 2)
          {
            if ((double) num2 > 0.0500000007450581)
              num2 -= 0.1f * num2;
            else
              num2 = 0.05f;
          }
          if ((double) this.LastDecoValue >= (double) num1)
          {
            float num3 = num2 * ((num1 * 2f - this.LastDecoValue) / num1);
            if ((double) num3 > 0.0)
              this.LastDecoValue += num3;
          }
          else
            this.LastDecoValue += num2;
        }
      }
      for (int index1 = 0; index1 < this.Signs_TilesTotalsAndFootPrints.Count; ++index1)
      {
        float num2 = 1f;
        for (int index2 = 0; index2 < this.Signs_TilesTotalsAndFootPrints[index1].Y; ++index2)
        {
          if (index2 > 2)
          {
            if ((double) num2 > 0.0500000007450581)
              num2 -= 0.1f * num2;
            else
              num2 = 0.05f;
          }
          if ((double) this.SignValue >= 30.0)
            this.SignValue += num2 * (float) ((50.0 - (double) this.SignValue) / 20.0);
          else
            this.SignValue += num2;
        }
      }
      this.Efficiency = this.LastDecoValue / num1;
      if ((double) this.Efficiency > 1.0)
        this.Efficiency = 1f;
      this.SignEfficiency = this.SignValue / 30f;
      if ((double) this.SignEfficiency > 100.0)
        this.SignEfficiency = 100f;
      this.LastDecoValue *= 1.333333f;
    }
  }
}
