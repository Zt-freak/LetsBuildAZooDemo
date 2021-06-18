// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Dungeon.FloorPiece
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Tile_Data;

namespace TinyZoo.GamePlay.Dungeon
{
  internal class FloorPiece : GameObject
  {
    private bool ISWall;
    private Texture2D teturetodraw;

    public FloorPiece(int XLoc, int YLoc, TILETYPE floortype, int _Rotation, int Variant = -1)
    {
      TileInfo tileInfo = TileData.GetTileInfo(floortype);
      this.DrawRect = tileInfo.GetRect(_Rotation, ref this.Rotation, Variant);
      this.vLocation = new Vector2((float) (XLoc * 16), (float) (YLoc * 16) * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.SetDrawOriginToCentre();
      this.teturetodraw = tileInfo.DrawTexture.texture;
    }

    public void SetAsCornerOrWall() => this.ISWall = true;

    public void UpdateFloorPiece()
    {
    }

    public void DrawFloorPiece() => this.WorldOffsetDraw(AssetContainer.pointspritebatch01, this.teturetodraw);
  }
}
