// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.CellBlocks.OtherBuildings.InfrastructureSave
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Tile_Data;

namespace TinyZoo.PlayerDir.Layout.CellBlocks.OtherBuildings
{
  internal class InfrastructureSave
  {
    private List<InfrastructureInfo> infrastructures;

    public InfrastructureSave() => this.infrastructures = new List<InfrastructureInfo>();

    public void BuildStructure(Vector2Int position, TILETYPE ThingToBuld) => this.infrastructures.Add(new InfrastructureInfo(ThingToBuld, position, Z_GameFlags.BuildOrder_DebugTrailer));

    public float GetInfrastuctureValue(out float DecorativesValue)
    {
      float num = 0.0f;
      DecorativesValue = 0.0f;
      for (int index = 0; index < this.infrastructures.Count; ++index)
      {
        switch (TileData.GetTileInfo(this.infrastructures[index].tiletype).categorytype)
        {
          case CATEGORYTYPE.Facilities:
          case CATEGORYTYPE.Amenities:
          case CATEGORYTYPE.Attractions:
          case CATEGORYTYPE.Farm:
            num += 0.25f;
            break;
          case CATEGORYTYPE.Floors:
            DecorativesValue += 0.01f;
            break;
          case CATEGORYTYPE.Nature:
          case CATEGORYTYPE.Signboards:
          case CATEGORYTYPE.Decorative:
          case CATEGORYTYPE.Light:
          case CATEGORYTYPE.Benches:
            DecorativesValue += 0.08f;
            break;
        }
      }
      return num;
    }

    public bool HasABuildng_Shop()
    {
      for (int index = 0; index < this.infrastructures.Count; ++index)
      {
        if (TileData.IsAShopOrProfitMakingThing(this.infrastructures[index].tiletype))
          return true;
      }
      return false;
    }

    public bool HasAnArchitectureBuilding()
    {
      for (int index = 0; index < this.infrastructures.Count; ++index)
      {
        if (TileData.IsAnArchitectOffice(this.infrastructures[index].tiletype))
          return true;
      }
      return false;
    }

    public int GetCountOfThisSpecialBuilding(TILETYPE strcture)
    {
      int num = 0;
      for (int index = 0; index < this.infrastructures.Count; ++index)
      {
        if (this.infrastructures[index].tiletype == strcture)
          ++num;
      }
      return num;
    }

    public void SellStructure(Vector2Int position, LayoutEntry _layoutentry)
    {
      bool flag = false;
      for (int index = this.infrastructures.Count - 1; index > -1; --index)
      {
        if (this.infrastructures[index].Location.CompareMatches(position) && (_layoutentry == null || this.infrastructures[index].tiletype == _layoutentry.tiletype))
        {
          flag = !flag ? true : throw new Exception("TWO TILES ON SALE PLACE");
          this.infrastructures.RemoveAt(index);
        }
      }
      if (!flag)
        throw new Exception("TILE NOT FOUND");
    }

    public int GetTotalResearch()
    {
      int num = 0;
      for (int index = 0; index < this.infrastructures.Count; ++index)
      {
        if (this.infrastructures[index].tiletype == TILETYPE.Research_PrisonPlanet)
          ++num;
      }
      return num;
    }

    public void BuildTileFromTileRenderer(TileRenderer tilerenderer) => this.infrastructures.Add(new InfrastructureInfo(tilerenderer.Ref_layoutentry.tiletype, tilerenderer.TileLocation, Z_GameFlags.BuildOrder_DebugTrailer));

    public void SetConsumption(ConsumptionStatus consumptionstatus)
    {
      for (int index = 0; index < this.infrastructures.Count; ++index)
        this.infrastructures[index].SetConsumption(consumptionstatus);
    }

    public void SaveInfrastructureSave(Writer writer)
    {
      writer.WriteInt("i", this.infrastructures.Count);
      for (int index = 0; index < this.infrastructures.Count; ++index)
        this.infrastructures[index].SaveInfrastructureInfo(writer);
    }

    public InfrastructureSave(Reader reader)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("i", ref _out);
      this.infrastructures = new List<InfrastructureInfo>();
      for (int index = 0; index < _out; ++index)
        this.infrastructures.Add(new InfrastructureInfo(reader));
    }
  }
}
