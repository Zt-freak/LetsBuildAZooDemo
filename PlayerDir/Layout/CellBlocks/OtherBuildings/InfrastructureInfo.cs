// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.CellBlocks.OtherBuildings.InfrastructureInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.Tile_Data;

namespace TinyZoo.PlayerDir.Layout.CellBlocks.OtherBuildings
{
  internal class InfrastructureInfo
  {
    public TILETYPE tiletype;
    public Vector2Int Location;
    public int BuildOrder_ForTrailer;

    public InfrastructureInfo(TILETYPE _tiletype, Vector2Int _Location, int BuildIndex_Debug)
    {
      this.tiletype = _tiletype;
      this.Location = new Vector2Int(_Location);
      this.BuildOrder_ForTrailer = Z_GameFlags.BuildOrder_DebugTrailer;
      ++Z_GameFlags.BuildOrder_DebugTrailer;
    }

    public void SetConsumption(ConsumptionStatus consumptionstatus)
    {
      if (!DebugFlags.BuildingsNeedResources)
        return;
      TileStats tileStats = TileData.GetTileStats(this.tiletype);
      if (tileStats.Consumptions != null)
      {
        for (int index = 0; index < tileStats.Consumptions.Length; ++index)
        {
          if (tileStats.Consumptions[index] != 0)
            consumptionstatus.ConsumptionValues[index] += (float) tileStats.Consumptions[index];
        }
      }
      if (tileStats.Productions == null)
        return;
      for (int index = 0; index < tileStats.Productions.Length; ++index)
      {
        if (tileStats.Productions[index] != 0)
          consumptionstatus.GenerationValues[index] += (float) tileStats.Productions[index];
      }
    }

    public InfrastructureInfo(Reader reader)
    {
      int _out = 0;
      int num1 = (int) reader.ReadInt("i", ref _out);
      this.tiletype = (TILETYPE) _out;
      this.Location = new Vector2Int(reader);
      int num2 = (int) reader.ReadInt("i", ref this.BuildOrder_ForTrailer);
    }

    public void SaveInfrastructureInfo(Writer writer)
    {
      writer.WriteInt("i", (int) this.tiletype);
      this.Location.SaveVector2Int(writer);
      writer.WriteInt("i", this.BuildOrder_ForTrailer);
    }
  }
}
