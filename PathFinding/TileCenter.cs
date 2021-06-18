// Decompiled with JetBrains decompiler
// Type: TinyZoo.PathFinding.TileCenter
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System;
using System.Collections.Generic;

namespace TinyZoo.PathFinding
{
  internal class TileCenter
  {
    public bool UpdateNavigationTileCenter(
      float DeltaTime,
      List<PathNode> CurrentPath,
      ref Vector2Int CurrentTile,
      float MovementSpeed,
      ref Vector2 TileRelativeLocation,
      ref DirectionPressed directionmovedthisframe,
      ref bool facingLeft,
      bool endAtCenter,
      bool IsNavigating,
      ref bool MovedTile)
    {
      if (CurrentPath == null)
        return IsNavigating;
      if (CurrentPath.Count == 0)
        return true;
      if (CurrentPath[0].Location.CompareMatches(CurrentTile))
      {
        CurrentPath.RemoveAt(0);
        TileRelativeLocation = Vector2.Zero;
      }
      else
      {
        Vector2 vector2 = new Vector2((float) (CurrentPath[0].Location.X - CurrentTile.X), (float) (CurrentPath[0].Location.Y - CurrentTile.Y)) * 2f - TileRelativeLocation;
        if (vector2 == Vector2.Zero)
          return true;
        vector2.Normalize();
        TileRelativeLocation += vector2 * DeltaTime * MovementSpeed;
        if ((double) DeltaTime > 0.0)
        {
          if ((double) vector2.X != 0.0 || (double) vector2.Y != 0.0)
          {
            if ((double) vector2.X > 0.0)
            {
              directionmovedthisframe = DirectionPressed.Right;
              facingLeft = false;
            }
            else if ((double) vector2.X < 0.0)
            {
              directionmovedthisframe = DirectionPressed.Left;
              facingLeft = true;
            }
            if ((double) vector2.Y > 0.0)
            {
              directionmovedthisframe = DirectionPressed.Down;
              facingLeft = false;
            }
            else if ((double) vector2.Y < 0.0)
            {
              directionmovedthisframe = DirectionPressed.Up;
              facingLeft = true;
            }
          }
          else if ((double) TileRelativeLocation.X > 0.0)
          {
            directionmovedthisframe = DirectionPressed.Right;
            facingLeft = false;
          }
          else if ((double) TileRelativeLocation.X < 0.0)
          {
            directionmovedthisframe = DirectionPressed.Left;
            facingLeft = true;
          }
          else if ((double) TileRelativeLocation.Y > 0.0)
          {
            directionmovedthisframe = DirectionPressed.Down;
            facingLeft = false;
          }
          else if ((double) TileRelativeLocation.Y < 0.0)
          {
            directionmovedthisframe = DirectionPressed.Up;
            facingLeft = true;
          }
        }
        if ((double) Math.Abs(TileRelativeLocation.X) > 2.0 || (double) Math.Abs(TileRelativeLocation.Y) > 2.0)
        {
          CurrentTile = new Vector2Int(CurrentPath[0].Location);
          MovedTile = true;
          TileRelativeLocation = Vector2.Zero;
          if (CurrentPath[0].IsWarp && CurrentPath.Count > 0 && !CurrentPath[1].Location.ThisIsNextToThis(CurrentPath[0].Location))
          {
            CurrentTile = new Vector2Int(CurrentPath[1].Location);
            TileRelativeLocation = Vector2.Zero;
          }
          CurrentPath.RemoveAt(0);
          if (CurrentPath.Count == 0)
            return true;
        }
      }
      return false;
    }
  }
}
