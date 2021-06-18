// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.Path_Renderer.PathRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using SEngine.Objects;
using System;
using System.Collections.Generic;
using TinyZoo.PathFinding;

namespace TinyZoo.GenericUI.Path_Renderer
{
  internal class PathRenderer
  {
    public PathPiece pathpiece;
    public bool IsActive = true;
    private bool HasAlphaFlash;

    public PathRenderer(bool _HasAlphaFlash = true)
    {
      this.HasAlphaFlash = _HasAlphaFlash;
      this.pathpiece = new PathPiece();
    }

    public void UpdatePathRenderer()
    {
    }

    public void DrawPathRenderer(
      List<PathNode> pathnodes,
      Vector2Int CurrentLocation,
      Vector2 WorldSpaceDrawLoc,
      float PercentageThroughParentTile,
      bool IsInPen = false)
    {
      if (!this.IsActive)
        return;
      DirectionPressed directionPressed = DirectionPressed.None;
      this.pathpiece.scale = 1f;
      if (CurrentLocation != null)
      {
        this.pathpiece.fAlpha = 1f;
        this.pathpiece.DrawPathPiece(PathPieceType.Start, WorldSpaceDrawLoc);
      }
      if (pathnodes == null || pathnodes.Count <= 0)
        return;
      for (int index = 0; index < pathnodes.Count; ++index)
      {
        if (index > 0)
          directionPressed = pathnodes[index].Location.GetDirectionToThis(pathnodes[index - 1].Location);
        else if (index == 0 && CurrentLocation != null && !pathnodes[index].Location.CompareMatches(CurrentLocation))
          directionPressed = pathnodes[index].Location.GetDirectionToThis(CurrentLocation);
        if (index < pathnodes.Count - 1)
        {
          DirectionPressed directionToThis = pathnodes[index].Location.GetDirectionToThis(pathnodes[index + 1].Location);
          switch (index)
          {
            case -1:
              switch (directionToThis)
              {
                case DirectionPressed.Up:
                case DirectionPressed.Down:
                  this.pathpiece.DrawPathPiece(PathPieceType.Vertical, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                  continue;
                case DirectionPressed.Right:
                case DirectionPressed.Left:
                  this.pathpiece.DrawPathPiece(PathPieceType.Horizontal, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                  continue;
                default:
                  continue;
              }
            case 0:
              this.SetAlphaOnPiece();
              break;
          }
          switch (directionPressed)
          {
            case DirectionPressed.Up:
              switch (directionToThis)
              {
                case DirectionPressed.Right:
                  this.pathpiece.DrawPathPiece(PathPieceType.BottomLeftCorner, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                  continue;
                case DirectionPressed.Down:
                  this.pathpiece.DrawPathPiece(PathPieceType.Vertical, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                  continue;
                case DirectionPressed.Left:
                  this.pathpiece.DrawPathPiece(PathPieceType.BottomRightCorner, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                  continue;
                default:
                  continue;
              }
            case DirectionPressed.Right:
              switch (directionToThis)
              {
                case DirectionPressed.Up:
                  this.pathpiece.DrawPathPiece(PathPieceType.BottomLeftCorner, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                  continue;
                case DirectionPressed.Down:
                  this.pathpiece.DrawPathPiece(PathPieceType.TopLeftCorner, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                  continue;
                case DirectionPressed.Left:
                  this.pathpiece.DrawPathPiece(PathPieceType.Horizontal, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                  continue;
                default:
                  continue;
              }
            case DirectionPressed.Down:
              switch (directionToThis)
              {
                case DirectionPressed.Up:
                  this.pathpiece.DrawPathPiece(PathPieceType.Vertical, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                  continue;
                case DirectionPressed.Right:
                  this.pathpiece.DrawPathPiece(PathPieceType.TopLeftCorner, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                  continue;
                case DirectionPressed.Down:
                  throw new Exception("DOUBLE BACK?");
                case DirectionPressed.Left:
                  this.pathpiece.DrawPathPiece(PathPieceType.TopRightCorner, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                  continue;
                default:
                  continue;
              }
            case DirectionPressed.Left:
              switch (directionToThis)
              {
                case DirectionPressed.Up:
                  this.pathpiece.DrawPathPiece(PathPieceType.BottomRightCorner, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                  continue;
                case DirectionPressed.Right:
                  this.pathpiece.DrawPathPiece(PathPieceType.Horizontal, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                  continue;
                case DirectionPressed.Down:
                  this.pathpiece.DrawPathPiece(PathPieceType.TopRightCorner, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                  continue;
                default:
                  continue;
              }
            default:
              continue;
          }
        }
        else
        {
          this.SetAlphaOnPiece();
          if (IsInPen)
          {
            this.pathpiece.scale = 1f;
            this.pathpiece.SetAlpha(1f);
            this.pathpiece.DrawPathPiece(PathPieceType.Start, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
          }
          else
          {
            switch (directionPressed)
            {
              case DirectionPressed.Up:
                this.pathpiece.DrawPathPiece(PathPieceType.DownArrow, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                continue;
              case DirectionPressed.Right:
                this.pathpiece.DrawPathPiece(PathPieceType.LeftArrow, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                continue;
              case DirectionPressed.Down:
                this.pathpiece.DrawPathPiece(PathPieceType.UpArrow, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                continue;
              case DirectionPressed.Left:
                this.pathpiece.DrawPathPiece(PathPieceType.RightArrow, TileMath.GetTileToWorldSpace(pathnodes[index].Location));
                continue;
              default:
                continue;
            }
          }
        }
      }
    }

    private void SetAlphaOnPiece()
    {
      this.pathpiece.scale = FlashingAlpha.Medium.fAlpha * 0.5f;
      if ((double) FlashingAlpha.Medium.fTargetAlpha == 0.0)
        this.pathpiece.scale = (float) (0.5 + (1.0 - (double) FlashingAlpha.Medium.fAlpha) * 0.5);
      this.pathpiece.scale = 1f - this.pathpiece.scale;
      if ((double) this.pathpiece.scale > 0.800000011920929)
      {
        this.pathpiece.fAlpha = this.pathpiece.scale - 0.8f;
        this.pathpiece.fAlpha = (float) (1.0 - (double) this.pathpiece.fAlpha * 5.0);
      }
      else
        this.pathpiece.fAlpha = 1f;
      this.pathpiece.scale *= 0.6f;
    }
  }
}
