// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.LayoutEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.PenBuilder.MovePen;

namespace TinyZoo.PlayerDir.Layout
{
  internal class LayoutEntry
  {
    public TILETYPE TEMP_OldUnderFloorType;
    public TILETYPE tiletype;
    public TILETYPE _UnderFloorTiletype;
    public int RotationClockWise;
    public int Variant = -1;
    private bool IsChild;
    public Vector2Int ParentLocation = new Vector2Int();

    public LayoutEntry(TILETYPE _tiletype) => this.tiletype = _tiletype;

    public bool GetIsChild() => this.IsChild;

    public TILETYPE UnderFloorTiletype
    {
      get => this._UnderFloorTiletype;
      set => this._UnderFloorTiletype = !TileData.IsThisWater(value) ? value : throw new Exception("kjsf");
    }

    public void SetChild(Vector2Int _ParentLocation, TILETYPE _tiletype)
    {
      this.tiletype = _tiletype;
      this.IsChild = true;
      this.ParentLocation = new Vector2Int(_ParentLocation);
    }

    public void UnsetChild()
    {
      this.IsChild = false;
      this.ParentLocation = (Vector2Int) null;
    }

    public bool isChild() => this.IsChild;

    public Vector2Int GetParentLocation() => this.ParentLocation;

    public void CloneFromBaseTileDesc(BaseTileDesc layoutentry)
    {
      this.RotationClockWise = layoutentry.Rotation;
      this.Variant = layoutentry.Variant;
      if (this.Variant != -1)
        throw new Exception("THIS IS NO LONGER ALLLLLOWED! - to save space on teh save");
      this.UnderFloorTiletype = layoutentry.underfloortype;
      this.tiletype = layoutentry.tiletype;
    }

    public void SetParentLocation(Vector2Int parentlocation) => this.ParentLocation = new Vector2Int(parentlocation);

    public LayoutEntry(Reader reader, bool IsFloor)
    {
      int _out = 1;
      int num1 = (int) reader.ReadInt("t", ref _out);
      this.tiletype = (TILETYPE) _out;
      if (this.tiletype == TILETYPE.None)
        return;
      int num2 = (int) reader.ReadInt("t", ref this.RotationClockWise);
      int num3 = (int) reader.ReadBool("t", ref this.IsChild);
      if (this.IsChild)
        this.ParentLocation = new Vector2Int(reader);
      if (!IsFloor || this.RotationClockWise == 0)
        return;
      int num4 = (int) reader.ReadInt("t", ref _out);
      this.UnderFloorTiletype = (TILETYPE) _out;
    }

    public void SaveLayoutEntry(Writer writer, bool IsFloor)
    {
      writer.WriteInt("t", (int) this.tiletype);
      if (this.tiletype == TILETYPE.None)
        return;
      writer.WriteInt("t", this.RotationClockWise);
      writer.WriteBool("t", this.IsChild);
      if (this.IsChild)
        this.ParentLocation.SaveVector2Int(writer);
      if (!IsFloor || this.RotationClockWise == 0)
        return;
      writer.WriteInt("t", (int) this.UnderFloorTiletype);
    }
  }
}
