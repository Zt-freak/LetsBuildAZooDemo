// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_EditZone.SelectedZones.Patrol_Zones.DragAndDrawZone
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System;

namespace TinyZoo.Z_EditZone.SelectedZones.Patrol_Zones
{
  internal class DragAndDrawZone : GameObject
  {
    private Vector2 StartLocation;
    private bool IsPainintingOrDeleting;
    private bool IsDelete;
    private Vector2 TopLeft;
    private Vector2 BottomRight;
    private bool FirstDrag;
    private Vector2 TouchStartLocation;
    private DirectionPressed DraggingThisCorner;

    public DragAndDrawZone(Vector2 _StartLocation_ScreenSpace)
    {
      this.StartLocation = _StartLocation_ScreenSpace;
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.scale = 1f * TileMath.TileSize;
      this.vLocation = RenderMath.TranslateScreenSpaceToWorldSpace(_StartLocation_ScreenSpace);
      this.SetAlpha(0.3f);
      this.StartLocation = this.vLocation;
      this.TopLeft = this.StartLocation;
      this.BottomRight = this.StartLocation;
      this.IsDelete = false;
      this.FirstDrag = true;
    }

    public void UpdateDragAndDrawZone(Player player)
    {
      if ((double) player.player.touchinput.MultiTouchTouchLocations[0].X > 0.0)
      {
        if (!this.IsPainintingOrDeleting)
        {
          this.IsPainintingOrDeleting = true;
          this.IsDelete = false;
          Z_GameFlags.ForceRightMouseDrag = true;
          this.TouchStartLocation = player.player.touchinput.MultiTouchTouchLocations[0];
        }
        else
        {
          Vector2 vector2 = (player.player.touchinput.MultiTouchTouchLocations[0] - this.TouchStartLocation) / Sengine.WorldOriginandScale.Z;
          this.TouchStartLocation = player.player.touchinput.MultiTouchTouchLocations[0];
          Z_GameFlags.ForceRightMouseDrag = true;
          if (this.FirstDrag && vector2 != Vector2.Zero)
          {
            if ((double) vector2.X < 0.0)
            {
              this.DraggingThisCorner = (double) vector2.Y >= 0.0 ? DirectionPressed.SouthWest : DirectionPressed.NorthWest;
              this.FirstDrag = false;
            }
            else if ((double) vector2.X > 0.0)
            {
              this.DraggingThisCorner = (double) vector2.Y >= 0.0 ? DirectionPressed.SouthEast : DirectionPressed.NorthEast;
              this.FirstDrag = false;
            }
          }
          if (!this.FirstDrag)
          {
            switch (this.DraggingThisCorner)
            {
              case DirectionPressed.NorthEast:
                this.TopLeft.Y += vector2.Y;
                this.BottomRight.X += vector2.X;
                this.BottomRight.Y -= vector2.Y * Sengine.ScreenRationReductionMultiplier.Y - vector2.Y;
                break;
              case DirectionPressed.SouthEast:
                vector2.Y *= Sengine.ScreenRationReductionMultiplier.Y;
                this.BottomRight.Y += vector2.Y;
                this.BottomRight.X += vector2.X;
                break;
              case DirectionPressed.SouthWest:
                vector2.Y *= Sengine.ScreenRationReductionMultiplier.Y;
                this.BottomRight.Y += vector2.Y;
                this.TopLeft.X += vector2.X;
                break;
              case DirectionPressed.NorthWest:
                this.TopLeft.Y += vector2.Y;
                this.TopLeft.X += vector2.X;
                this.BottomRight.Y -= vector2.Y * Sengine.ScreenRationReductionMultiplier.Y - vector2.Y;
                break;
            }
            if ((double) this.BottomRight.X < (double) this.TopLeft.X)
            {
              Console.WriteLine("X_FLIPPY" + (object) TinyZoo.Game1.Rnd.Next(0, 100));
              float num = this.TopLeft.X - this.BottomRight.X;
              this.BottomRight.X = this.TopLeft.X;
              this.TopLeft.X -= num;
              if (this.DraggingThisCorner == DirectionPressed.NorthEast)
                this.DraggingThisCorner = DirectionPressed.NorthWest;
              else if (this.DraggingThisCorner == DirectionPressed.SouthEast)
                this.DraggingThisCorner = DirectionPressed.SouthWest;
              else if (this.DraggingThisCorner == DirectionPressed.SouthWest)
                this.DraggingThisCorner = DirectionPressed.SouthEast;
              else if (this.DraggingThisCorner == DirectionPressed.NorthWest)
                this.DraggingThisCorner = DirectionPressed.NorthEast;
            }
            if ((double) this.BottomRight.Y < (double) this.TopLeft.Y)
            {
              Console.WriteLine("Y_FLIPPY" + (object) TinyZoo.Game1.Rnd.Next(0, 100) + (object) this.DraggingThisCorner);
              float num = this.TopLeft.Y - this.BottomRight.Y;
              this.BottomRight.Y = this.TopLeft.Y;
              this.TopLeft.Y -= num;
              if (this.DraggingThisCorner == DirectionPressed.NorthEast)
                this.DraggingThisCorner = DirectionPressed.SouthEast;
              else if (this.DraggingThisCorner == DirectionPressed.NorthWest)
                this.DraggingThisCorner = DirectionPressed.SouthWest;
              else if (this.DraggingThisCorner == DirectionPressed.SouthEast)
                this.DraggingThisCorner = DirectionPressed.NorthEast;
              else if (this.DraggingThisCorner == DirectionPressed.SouthWest)
                this.DraggingThisCorner = DirectionPressed.NorthWest;
            }
          }
          Console.WriteLine("Dragging" + (object) this.DraggingThisCorner);
        }
      }
      else
      {
        this.IsPainintingOrDeleting = false;
        this.FirstDrag = true;
      }
    }

    public void DrawDragAndDrawZone()
    {
      this.vLocation = this.TopLeft;
      Vector2 Scale1 = new Vector2(this.BottomRight.X - this.TopLeft.X, this.BottomRight.Y - this.TopLeft.Y);
      if (Scale1 == Vector2.Zero)
        Scale1 = Vector2.One;
      this.SetAlpha(0.2f);
      this.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, this.vLocation, Scale1, 0.0f);
      this.SetAlpha(0.4f);
      this.vLocation.X = (float) Math.Round((double) this.TopLeft.X / (double) TileMath.TileSize);
      this.vLocation.Y = (float) Math.Round((double) this.TopLeft.Y / (double) TileMath.TileSize);
      this.vLocation = this.vLocation * (float) (int) TileMath.TileSize;
      Scale1.X = (float) Math.Round((double) Scale1.X / (double) TileMath.TileSize);
      Scale1.Y = (float) Math.Round((double) Scale1.Y / (double) TileMath.TileSize);
      Vector2 Scale2 = Scale1 * TileMath.TileSize;
      this.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, this.vLocation, Scale2, 0.0f);
    }
  }
}
