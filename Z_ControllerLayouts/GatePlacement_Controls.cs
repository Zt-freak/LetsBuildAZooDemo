// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ControllerLayouts.GatePlacement_Controls
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System.Collections.Generic;
using TinyZoo.PlayerDir;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens.WallBuilder;

namespace TinyZoo.Z_ControllerLayouts
{
  internal class GatePlacement_Controls
  {
    private List<Vector2Int> RefFenceTiles;
    private ButtonRepeater repeater;
    private bool IsHolding;
    private int CycleDir;
    private int SelectedIndex;
    private List<Vector2Int> Floors;
    private Vector2Int LOCATION;

    public GatePlacement_Controls()
    {
      this.SelectedIndex = 0;
      this.repeater = new ButtonRepeater();
    }

    public Vector2Int GetSelectedLocation() => this.LOCATION;

    public bool UpdateGatePlacement_Controls(
      float DeltaTime,
      Player player,
      PerimeterBuilder perimeterBuilder)
    {
      player.inputmap.CameraStick = Vector2.Zero;
      DirectionPressed Direction;
      if (this.repeater.UpdateMenuRepeats(DeltaTime, out Direction, (double) player.inputmap.GatePlacementStick.Y > 0.0, (double) player.inputmap.GatePlacementStick.Y < 0.0, (double) player.inputmap.GatePlacementStick.X < 0.0, (double) player.inputmap.GatePlacementStick.X > 0.0))
      {
        if (!this.IsHolding)
        {
          if (this.Floors == null)
            this.Floors = perimeterBuilder.GetFloorsAsPositions();
          InternalDirection iternaldirection;
          InnerDirectionCalculator.GetThisInnerDirection(this.SelectedIndex, perimeterBuilder.CommitedBuildTiles[this.SelectedIndex].TileLocation, this.Floors, out iternaldirection, perimeterBuilder.CommitedBuildTiles);
          this.CycleDir = 1;
          Vector2Int vector2Int1 = new Vector2Int(perimeterBuilder.CommitedBuildTiles[this.SelectedIndex].TileLocation);
          Vector2Int vector2Int2 = this.SelectedIndex <= 0 ? perimeterBuilder.CommitedBuildTiles[perimeterBuilder.CommitedBuildTiles.Count - 1].TileLocation - vector2Int1 : perimeterBuilder.CommitedBuildTiles[this.SelectedIndex - 1].TileLocation - vector2Int1;
          switch (perimeterBuilder.CommitedBuildTiles[this.SelectedIndex].cellcornertype)
          {
            case CellCornerType.StraightLeftRight:
              if (iternaldirection == InternalDirection.Down)
              {
                if (Direction == DirectionPressed.Down || Direction == DirectionPressed.Right)
                {
                  this.CycleDir = 1;
                  if (vector2Int2.X > 0)
                  {
                    this.CycleDir = -1;
                    break;
                  }
                  break;
                }
                this.CycleDir = -1;
                if (vector2Int2.X > 0)
                {
                  this.CycleDir = 1;
                  break;
                }
                break;
              }
              if (Direction == DirectionPressed.Down || Direction == DirectionPressed.Right)
              {
                this.CycleDir = -1;
                if (vector2Int2.X < 0)
                {
                  this.CycleDir = 1;
                  break;
                }
                break;
              }
              this.CycleDir = 1;
              if (vector2Int2.X < 0)
              {
                this.CycleDir = -1;
                break;
              }
              break;
            case CellCornerType.StraightUpDown:
              if (iternaldirection == InternalDirection.Left)
              {
                if (Direction == DirectionPressed.Down || Direction == DirectionPressed.Right)
                {
                  this.CycleDir = 1;
                  if (vector2Int2.Y > 0)
                  {
                    this.CycleDir = -1;
                    break;
                  }
                  break;
                }
                this.CycleDir = -1;
                if (vector2Int2.Y > 0)
                {
                  this.CycleDir = 1;
                  break;
                }
                break;
              }
              if (Direction == DirectionPressed.Down || Direction == DirectionPressed.Right)
              {
                this.CycleDir = -1;
                if (vector2Int2.Y < 0)
                {
                  this.CycleDir = 1;
                  break;
                }
                break;
              }
              this.CycleDir = 1;
              if (vector2Int2.Y < 0)
              {
                this.CycleDir = -1;
                break;
              }
              break;
          }
        }
        this.IsHolding = true;
        this.SelectedIndex += this.CycleDir;
        if (this.SelectedIndex >= perimeterBuilder.CommitedBuildTiles.Count)
          this.SelectedIndex = 0;
        else if (this.SelectedIndex < 0)
          this.SelectedIndex = perimeterBuilder.CommitedBuildTiles.Count - 1;
        this.LOCATION = perimeterBuilder.CommitedBuildTiles[this.SelectedIndex].TileLocation;
        if (this.LOCATION != null)
          player.inputmap.PointerLocation = RenderMath.TranslateWorldSpaceToScreenSpace(TileMath.GetTileToWorldSpace(this.LOCATION));
        return true;
      }
      if (!this.repeater.AnyButtonIsHeld)
        this.IsHolding = false;
      if (this.LOCATION != null)
        player.inputmap.PointerLocation = RenderMath.TranslateWorldSpaceToScreenSpace(TileMath.GetTileToWorldSpace(this.LOCATION));
      return false;
    }
  }
}
