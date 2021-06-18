// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.TrashEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_TrashSystem;

namespace TinyZoo.PlayerDir
{
  internal class TrashEntry
  {
    public Vector2Int TileLocation;
    public TrashType trashtype;
    private AnimalType animal = AnimalType.None;
    public int PrisonUID;

    public TrashEntry(
      Vector2Int _TileLocation,
      TrashType _trashtype,
      AnimalType _animal = AnimalType.None,
      int _PrisonUID = -1)
    {
      this.TileLocation = new Vector2Int(_TileLocation);
      this.trashtype = _trashtype;
    }

    public TrashEntry(Reader reader)
    {
      this.TileLocation = new Vector2Int(reader);
      int _out1 = 0;
      int num1 = (int) reader.ReadInt("t", ref _out1);
      this.trashtype = (TrashType) _out1;
      int _out2 = -1;
      if (this.trashtype != TrashType.AnimalPoop)
        return;
      int num2 = (int) reader.ReadInt("t", ref _out1);
      int num3 = (int) reader.ReadInt("t", ref _out2);
    }

    public void SaveTrashEntry(Writer writer)
    {
      this.TileLocation.SaveVector2Int(writer);
      writer.WriteInt("t", (int) this.trashtype);
      this.PrisonUID = -1;
      if (this.trashtype != TrashType.AnimalPoop)
        return;
      writer.WriteInt("t", (int) this.animal);
      writer.WriteInt("t", this.PrisonUID);
    }
  }
}
