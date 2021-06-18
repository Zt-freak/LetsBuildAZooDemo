// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.SerializeCurrentMapToCodeFile
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.IO;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.Tile_Data;

namespace TinyZoo.Utils
{
  internal class SerializeCurrentMapToCodeFile
  {
    internal static void DoSerializeCurrentMapToCodeFile(Player player)
    {
      using (FileStream fileStream = new FileStream("ThisMap.cs", FileMode.Create))
      {
        StreamWriter streamWriter = new StreamWriter((Stream) fileStream);
        LayoutData layoutData = new LayoutData();
        streamWriter.WriteLine("using TinyZoo.PlayerDir;");
        streamWriter.WriteLine("using TinyZoo.PlayerDir.Layout;");
        streamWriter.WriteLine("using TinyZoo.Tile_Data;");
        streamWriter.WriteLine("using SEngine;");
        streamWriter.WriteLine("");
        streamWriter.WriteLine("    namespace TinyZoo.Maps");
        streamWriter.WriteLine("    {");
        streamWriter.WriteLine("        class ThisMap");
        streamWriter.WriteLine("        {");
        streamWriter.WriteLine("        internal static LayoutData GetMap()");
        streamWriter.WriteLine("        {");
        streamWriter.WriteLine("        LayoutData layout = new LayoutData();");
        streamWriter.WriteLine("        layout.BaseTileTypes = new LayoutEntry[" + (object) player.prisonlayout.layout.BaseTileTypes.GetLength(0) + "," + (object) player.prisonlayout.layout.BaseTileTypes.GetLength(1) + "];");
        streamWriter.WriteLine("         for (int X = 0; X < layout.BaseTileTypes.GetLength(0); X++)");
        streamWriter.WriteLine("         {");
        streamWriter.WriteLine("         for (int Y = 0; Y < layout.BaseTileTypes.GetLength(1); Y++)");
        streamWriter.WriteLine("         {");
        streamWriter.WriteLine("         layout.BaseTileTypes[X, Y] = new LayoutEntry(TILETYPE.None);");
        streamWriter.WriteLine("         }");
        streamWriter.WriteLine("         }");
        for (int index1 = 0; index1 < player.prisonlayout.layout.BaseTileTypes.GetLength(0); ++index1)
        {
          for (int index2 = 0; index2 < player.prisonlayout.layout.BaseTileTypes.GetLength(1); ++index2)
          {
            if (player.prisonlayout.layout.BaseTileTypes[index1, index2].tiletype != TILETYPE.None)
            {
              streamWriter.WriteLine("         layout.BaseTileTypes[" + (object) index1 + "," + (object) index2 + "] = new LayoutEntry((TILETYPE)" + (object) (int) player.prisonlayout.layout.BaseTileTypes[index1, index2].tiletype + ");");
              if (player.prisonlayout.layout.BaseTileTypes[index1, index2].UnderFloorTiletype != TILETYPE.None)
                streamWriter.WriteLine("         layout.BaseTileTypes[" + (object) index1 + "," + (object) index2 + "].UnderFloorTiletype = (TILETYPE) " + (object) (int) player.prisonlayout.layout.BaseTileTypes[index1, index2].UnderFloorTiletype + ";");
              if (player.prisonlayout.layout.BaseTileTypes[index1, index2].RotationClockWise != 0)
                streamWriter.WriteLine("         layout.BaseTileTypes[" + (object) index1 + "," + (object) index2 + "].RotationClockWise = " + (object) player.prisonlayout.layout.BaseTileTypes[index1, index2].RotationClockWise + ";");
              if (player.prisonlayout.layout.BaseTileTypes[index1, index2].Variant != -1)
                streamWriter.WriteLine("         layout.BaseTileTypes[" + (object) index1 + "," + (object) index2 + "].Variant = " + (object) player.prisonlayout.layout.BaseTileTypes[index1, index2].Variant + ";");
              if (player.prisonlayout.layout.BaseTileTypes[index1, index2].GetIsChild())
                streamWriter.WriteLine("         layout.BaseTileTypes[" + (object) index1 + "," + (object) index2 + "].SetChild(new Vector2Int(" + (object) player.prisonlayout.layout.BaseTileTypes[index1, index2].ParentLocation.X + "," + (object) player.prisonlayout.layout.BaseTileTypes[index1, index2].ParentLocation.Y + "), (TILETYPE) " + (object) (int) player.prisonlayout.layout.BaseTileTypes[index1, index2].tiletype + ");");
            }
          }
        }
        streamWriter.WriteLine("        layout.FloorTileTypes = new LayoutEntry[" + (object) player.prisonlayout.layout.FloorTileTypes.GetLength(0) + "," + (object) player.prisonlayout.layout.FloorTileTypes.GetLength(1) + "];");
        streamWriter.WriteLine("         for (int X = 0; X < layout.FloorTileTypes.GetLength(0); X++)");
        streamWriter.WriteLine("         {");
        streamWriter.WriteLine("         for (int Y = 0; Y < layout.FloorTileTypes.GetLength(1); Y++)");
        streamWriter.WriteLine("         {");
        streamWriter.WriteLine("         layout.FloorTileTypes[X, Y] = new LayoutEntry(TILETYPE.None);");
        streamWriter.WriteLine("         }");
        streamWriter.WriteLine("         }");
        for (int index1 = 0; index1 < player.prisonlayout.layout.FloorTileTypes.GetLength(0); ++index1)
        {
          for (int index2 = 0; index2 < player.prisonlayout.layout.FloorTileTypes.GetLength(1); ++index2)
          {
            if (player.prisonlayout.layout.FloorTileTypes[index1, index2].tiletype != TILETYPE.None)
            {
              streamWriter.WriteLine("         layout.FloorTileTypes[" + (object) index1 + "," + (object) index2 + "] = new LayoutEntry((TILETYPE)" + (object) (int) player.prisonlayout.layout.FloorTileTypes[index1, index2].tiletype + ");");
              if (player.prisonlayout.layout.FloorTileTypes[index1, index2].UnderFloorTiletype != TILETYPE.None)
                streamWriter.WriteLine("         layout.FloorTileTypes[" + (object) index1 + "," + (object) index2 + "].UnderFloorTiletype = (TILETYPE) " + (object) (int) player.prisonlayout.layout.FloorTileTypes[index1, index2].UnderFloorTiletype + ";");
              if (player.prisonlayout.layout.FloorTileTypes[index1, index2].RotationClockWise != 0)
                streamWriter.WriteLine("         layout.FloorTileTypes[" + (object) index1 + "," + (object) index2 + "].RotationClockWise = " + (object) player.prisonlayout.layout.FloorTileTypes[index1, index2].RotationClockWise + ";");
              if (player.prisonlayout.layout.FloorTileTypes[index1, index2].Variant != -1)
                streamWriter.WriteLine("         layout.FloorTileTypes[" + (object) index1 + "," + (object) index2 + "].Variant = " + (object) player.prisonlayout.layout.FloorTileTypes[index1, index2].Variant + ";");
              if (player.prisonlayout.layout.FloorTileTypes[index1, index2].GetIsChild())
                streamWriter.WriteLine("         layout.FloorTileTypes[" + (object) index1 + "," + (object) index2 + "].SetChild(new Vector2Int(" + (object) player.prisonlayout.layout.FloorTileTypes[index1, index2].ParentLocation.X + "," + (object) player.prisonlayout.layout.FloorTileTypes[index1, index2].ParentLocation.Y + "), (TILETYPE) " + (object) (int) player.prisonlayout.layout.FloorTileTypes[index1, index2].tiletype + ");");
            }
          }
        }
        streamWriter.WriteLine("    return layout;");
        streamWriter.WriteLine("    }");
        streamWriter.WriteLine("}");
        streamWriter.WriteLine("}");
        streamWriter.Close();
      }
      using (FileStream fileStream = new FileStream("TheseEnclosures.cs", FileMode.Create))
      {
        StreamWriter streamWriter = new StreamWriter((Stream) fileStream);
        streamWriter.WriteLine("using TinyZoo.PlayerDir;");
        streamWriter.WriteLine("using TinyZoo.PlayerDir.Layout;");
        streamWriter.WriteLine("using TinyZoo.Tile_Data;");
        streamWriter.WriteLine("using SEngine;");
        streamWriter.WriteLine("using TinyZoo.PlayerDir.Layout.CellBlocks;");
        streamWriter.WriteLine("");
        streamWriter.WriteLine("    namespace TinyZoo.Maps");
        streamWriter.WriteLine("    {");
        streamWriter.WriteLine("        class TheseEnclosures");
        streamWriter.WriteLine("        {");
        streamWriter.WriteLine("        internal static CellblockMananger GetCellblockMananger()");
        streamWriter.WriteLine("        {");
        streamWriter.WriteLine("        CellblockMananger cellblock = new CellblockMananger();");
        CellblockMananger cellblockMananger = new CellblockMananger();
        streamWriter.WriteLine("         PrisonZone pz;");
        for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
        {
          streamWriter.WriteLine("         pz = new PrisonZone(" + (object) player.prisonlayout.cellblockcontainer.prisonzones[index1].Cell_UID + ", (CellBlockType)" + (object) (int) player.prisonlayout.cellblockcontainer.prisonzones[index1].CellBLOCKTYPE + ");");
          streamWriter.WriteLine("         pz.YV_SetGate(" + (object) player.prisonlayout.cellblockcontainer.prisonzones[index1].GetGateLocation().X + ", " + (object) player.prisonlayout.cellblockcontainer.prisonzones[index1].GetGateLocation().Y + ");");
          for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones[index1].FenceTiles.Count; ++index2)
            streamWriter.WriteLine("         pz.FenceTiles.Add(new Vector2Int(" + (object) player.prisonlayout.cellblockcontainer.prisonzones[index1].FenceTiles[index2].X + "," + (object) player.prisonlayout.cellblockcontainer.prisonzones[index1].FenceTiles[index2].Y + "));");
          for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones[index1].FloorTiles.Count; ++index2)
            streamWriter.WriteLine("         pz.FloorTiles.Add(new Vector2Int(" + (object) player.prisonlayout.cellblockcontainer.prisonzones[index1].FloorTiles[index2].X + "," + (object) player.prisonlayout.cellblockcontainer.prisonzones[index1].FloorTiles[index2].Y + "));");
          streamWriter.WriteLine("         cellblock.prisonzones.Add(pz);");
        }
        streamWriter.WriteLine("         return cellblock;");
        streamWriter.WriteLine("    }");
        streamWriter.WriteLine("}");
        streamWriter.WriteLine("}");
        streamWriter.Close();
      }
    }
  }
}
