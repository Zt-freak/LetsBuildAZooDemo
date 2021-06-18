// Decompiled with JetBrains decompiler
// Type: TinyZoo.BreakOut.BreakOutData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.Tile_Data;

namespace TinyZoo.BreakOut
{
  internal class BreakOutData
  {
    internal static PrisonZone GetMap()
    {
      int num1 = 25;
      int num2 = 13;
      new LayoutData().AddNewCellBlock(new Vector2Int(1, 1), new Vector2Int(num1, num2), true, (WallsAndFloorsManager) null, CellBlockType.Mountain);
      PrisonZone prisonZone = new PrisonZone(1, 1, num1, num2, 0, CellBlockType.Mountain, new Vector2Int(-1, -1))
      {
        prisonercontainer = new PrisonerContainer()
      };
      prisonZone.prisonercontainer.prisoners.Add(new PrisonerInfo(new IntakePerson(AnimalType.Snake), false, Vector2.Zero, CellBlockType.Mountain));
      return prisonZone;
    }
  }
}
