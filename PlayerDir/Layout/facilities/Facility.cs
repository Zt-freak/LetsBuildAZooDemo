// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.facilities.Facility
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;

namespace TinyZoo.PlayerDir.Layout.facilities
{
  internal class Facility
  {
    public Vector2Int Location;

    public Facility(Vector2Int _Location) => this.Location = new Vector2Int(_Location);

    public Facility(Reader reader) => this.Location = new Vector2Int(reader);

    public void SaveWaterPump(Writer writer) => this.Location.SaveVector2Int(writer);
  }
}
