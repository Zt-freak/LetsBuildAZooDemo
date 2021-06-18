// Decompiled with JetBrains decompiler
// Type: TinyZoo.PathFinding.CollisionRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;

namespace TinyZoo.PathFinding
{
  internal class CollisionRenderer
  {
    private static GameObject Block;

    internal static void RenderCollisionRenderer(PathSet pathset, float TileScale)
    {
      if (CollisionRenderer.Block == null)
      {
        CollisionRenderer.Block = new GameObject();
        CollisionRenderer.Block.DrawRect = TinyZoo.Game1.WhitePixelRect;
        CollisionRenderer.Block.SetDrawOriginToCentre();
        CollisionRenderer.Block.fAlpha = 0.4f;
      }
      CollisionRenderer.Block.scale = TileScale;
      for (int index1 = 0; index1 < pathset.nodesasGrid.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < pathset.nodesasGrid.GetLength(1); ++index2)
        {
          CollisionRenderer.Block.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(pathset.nodesasGrid[index1, index2].XLoc, pathset.nodesasGrid[index1, index2].YLoc));
          if (pathset.nodesasGrid[index1, index2].IsBlocking)
          {
            CollisionRenderer.Block.SetAllColours(1f, 0.0f, 0.0f);
            CollisionRenderer.Block.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
          }
          else if (pathset.nodesasGrid[index1, index2].IsWarp)
          {
            CollisionRenderer.Block.SetAllColours(1f, 0.5f, 0.8f);
            CollisionRenderer.Block.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, 0.9f);
          }
          else
          {
            CollisionRenderer.Block.SetAllColours(0.0f, 1f, 0.0f);
            CollisionRenderer.Block.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
          }
        }
      }
    }
  }
}
