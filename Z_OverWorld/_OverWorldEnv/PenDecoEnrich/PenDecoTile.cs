// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.PenDecoEnrich.PenDecoTile
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_OverWorld._OverWorldEnv.PenDecoEnrich.DecoManagers;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.PenDecoEnrich
{
  internal class PenDecoTile
  {
    public List<BaseDecoObject> baseobjects;
    private Vector2Int location;
    private static Vector2 Position;

    public PenDecoTile(Vector2Int _location)
    {
      this.location = new Vector2Int(_location);
      this.baseobjects = new List<BaseDecoObject>();
    }

    public void AddDecoObject(BaseDecoObject AdsThis) => this.baseobjects.Add(AdsThis);

    public void UpdatePenDecoTile(float DeltaTime)
    {
      for (int index = this.baseobjects.Count - 1; index > -1; --index)
      {
        if (this.baseobjects[index].UpdateBaseDecoObject(DeltaTime))
          this.baseobjects.RemoveAt(index);
      }
    }

    public void DrawPenDecoTile()
    {
      PenDecoTile.Position = TileMath.GetTileToWorldSpace(this.location);
      for (int index = 0; index < this.baseobjects.Count; ++index)
        this.baseobjects[index].DrawBaseDecoObject(PenDecoTile.Position);
    }
  }
}
