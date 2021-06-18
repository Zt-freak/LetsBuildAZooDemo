// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.WorkZonePane.WorkZone
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.Z_EditZone.SelectedZones.SimpleZoneDrag;

namespace TinyZoo.Z_Employees.WorkZonePane
{
  internal class WorkZone
  {
    public Vector2Int TopLeft;
    public Vector2Int BottomRight;

    public WorkZone(Vector2Int _TopLeft, Vector2Int _BottomRight)
    {
      this.TopLeft = new Vector2Int(_TopLeft);
      this.BottomRight = new Vector2Int(_BottomRight);
    }

    public WorkZone(ZonePainter zonepainter)
    {
      this.TopLeft = TileMath.GetWorldSpaceToTile(zonepainter.TopLeft);
      this.BottomRight = TileMath.GetWorldSpaceToTile(zonepainter.BottomRight);
    }

    public Vector2Int GetRandomUnblockedLocaton()
    {
      bool flag = false;
      int num = 0;
      while (!flag)
      {
        Vector2Int vector2Int = new Vector2Int(TinyZoo.Game1.Rnd.Next(this.TopLeft.X, this.BottomRight.X), TinyZoo.Game1.Rnd.Next(this.TopLeft.Y, this.BottomRight.Y + 1));
        if (!Z_GameFlags.pathfinder.GetIsBlocked(vector2Int.X, vector2Int.Y))
          return vector2Int;
        ++num;
        if (num > 20)
          return (Vector2Int) null;
      }
      return (Vector2Int) null;
    }

    public bool IsInWorkZone(Vector2Int LOC) => LOC.X >= this.TopLeft.X && LOC.Y >= this.TopLeft.Y && LOC.X < this.BottomRight.X && LOC.Y < this.BottomRight.Y;

    public void SaveWorkZone(Writer writer)
    {
      this.TopLeft.SaveVector2Int(writer);
      this.BottomRight.SaveVector2Int(writer);
    }

    public WorkZone(Reader reader)
    {
      this.TopLeft = new Vector2Int(reader);
      this.BottomRight = new Vector2Int(reader);
    }
  }
}
