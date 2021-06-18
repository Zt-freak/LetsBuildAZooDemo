// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.Path_Renderer.PathPiece
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.GenericUI.Path_Renderer
{
  internal class PathPiece : GameObject
  {
    private static Rectangle[] Rects;

    public PathPiece()
    {
      if (PathPiece.Rects == null)
      {
        PathPiece.Rects = new Rectangle[11];
        PathPiece.Rects[0] = new Rectangle(2097, 1175, 16, 16);
        PathPiece.Rects[1] = new Rectangle(2116, 1175, 16, 16);
        PathPiece.Rects[2] = new Rectangle(2135, 1175, 16, 16);
        PathPiece.Rects[3] = new Rectangle(2154, 1175, 16, 16);
        PathPiece.Rects[4] = new Rectangle(2173, 1175, 16, 16);
        PathPiece.Rects[5] = new Rectangle(2192, 1175, 16, 16);
        PathPiece.Rects[6] = new Rectangle(2211, 1175, 16, 16);
        PathPiece.Rects[7] = new Rectangle(2147, 1194, 16, 16);
        PathPiece.Rects[8] = new Rectangle(2166, 1194, 16, 16);
        PathPiece.Rects[9] = new Rectangle(2185, 1194, 16, 16);
        PathPiece.Rects[10] = new Rectangle(2230, 1175, 16, 16);
      }
      this.DrawOrigin = new Vector2(8f, 8f);
      this.SetAllColours(new Vector3(32f, 189f, 217f) / (float) byte.MaxValue);
    }

    public void UpdatePathPiece()
    {
    }

    public void DrawPathPiece(PathPieceType piecetype, Vector2 Location)
    {
      this.vLocation = Location;
      this.DrawRect = PathPiece.Rects[(int) piecetype];
      this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.EnvironmentSheet);
    }
  }
}
