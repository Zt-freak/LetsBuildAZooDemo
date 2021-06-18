// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.PhotoModeSurround.PhotoModeSurroundRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.PhotoModeSurround
{
  internal class PhotoModeSurroundRenderer
  {
    private EdgePiece edgepiece;
    private EdgePiece edgepieceFlipper;
    private EdgePiece RoadLeftpiece;
    private EdgePiece RoadLiftFlipper;
    private EdgePiece RoadRuight;
    private EdgePiece RoadRightFlipper;

    public PhotoModeSurroundRenderer()
    {
      this.edgepiece = new EdgePiece();
      this.edgepieceFlipper = new EdgePiece(true);
      this.RoadLeftpiece = new EdgePiece(IsRoadLeft: true);
      this.RoadRuight = new EdgePiece(IsRoadRight: true);
      this.RoadLeftpiece.SetDrawOriginToPoint(DrawOriginPosition.TopRight);
      this.RoadRuight.FlipRender = true;
      this.RoadRuight.SetDrawOriginToPoint(DrawOriginPosition.TopRight);
      this.RoadLiftFlipper = new EdgePiece(true, true);
      this.RoadLiftFlipper.SetDrawOriginToPoint(DrawOriginPosition.TopRight);
      this.RoadRightFlipper = new EdgePiece(true, IsRoadRight: true);
      this.RoadRightFlipper = new EdgePiece(true, IsRoadRight: true);
      this.RoadRightFlipper.FlipRender = true;
      this.RoadRightFlipper.SetDrawOriginToPoint(DrawOriginPosition.TopRight);
    }

    public void UpdatePhotoModeSurroundRenderer()
    {
    }

    public void DrawPhotoModeSurroundRenderer()
    {
      Vector2 tileToWorldSpace1 = TileMath.GetTileToWorldSpace(new Vector2Int(TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize()));
      tileToWorldSpace1.X += 496f;
      float x1 = RenderMath.TranslateScreenSpaceToWorldSpace(Vector2.Zero).X;
      float x2 = RenderMath.TranslateScreenSpaceToWorldSpace(new Vector2(1024f, 0.0f)).X;
      for (int index = 0; index < 8; ++index)
      {
        Vector2 tileToWorldSpace2 = TileMath.GetTileToWorldSpace(new Vector2Int(0, TileMath.GetOverWorldMapSize_YSize() - (6 + 32 * index)));
        tileToWorldSpace2.Y -= 8f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.edgepiece.vLocation = tileToWorldSpace2;
        this.edgepiece.FlipRender = false;
        this.edgepiece.DrawEdgePiece();
        this.edgepieceFlipper.FlipRender = false;
        this.edgepieceFlipper.vLocation = this.edgepiece.vLocation;
        if (index == 0)
        {
          this.RoadLeftpiece.vLocation = tileToWorldSpace2;
          this.RoadLeftpiece.DrawEdgePiece();
          this.RoadRuight.vLocation = tileToWorldSpace2;
          this.RoadRuight.vLocation.X = tileToWorldSpace1.X;
          this.RoadRuight.DrawEdgePiece();
        }
        while ((double) this.edgepieceFlipper.vLocation.X > (double) x1)
        {
          this.edgepieceFlipper.vLocation.X -= 512f;
          this.edgepieceFlipper.DrawEdgePiece();
          if (index == 0)
          {
            this.RoadLiftFlipper.vLocation = this.edgepieceFlipper.vLocation;
            this.RoadLiftFlipper.DrawEdgePiece();
          }
        }
        this.edgepiece.vLocation.X = tileToWorldSpace1.X;
        this.edgepiece.FlipRender = true;
        this.edgepiece.DrawEdgePiece();
        this.edgepieceFlipper.FlipRender = true;
        this.edgepieceFlipper.vLocation = this.edgepiece.vLocation;
        while ((double) this.edgepieceFlipper.vLocation.X < (double) x2 + 512.0)
        {
          this.edgepieceFlipper.vLocation.X += 512f;
          this.edgepieceFlipper.DrawEdgePiece();
          if (index == 0)
          {
            this.RoadRightFlipper.vLocation = this.edgepieceFlipper.vLocation;
            this.RoadRightFlipper.DrawEdgePiece();
          }
        }
      }
    }
  }
}
