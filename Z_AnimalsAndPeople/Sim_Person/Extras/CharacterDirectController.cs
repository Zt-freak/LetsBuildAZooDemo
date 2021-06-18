// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras.CharacterDirectController
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PathFinding;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras
{
  internal class CharacterDirectController
  {
    public Vector2 Velocity;

    public bool UpdateCharacterDirectController(
      Player player,
      float DeltaTime,
      ref bool IsWalking,
      WalkingPerson Person,
      PathNavigator pathnavigator)
    {
      IsWalking = false;
      if (player.inputmap.CharacterMovementStick != Vector2.Zero || this.Velocity != Vector2.Zero)
      {
        Vector2 velocity = this.Velocity;
        if ((double) Math.Abs(velocity.X) > (double) Math.Abs(velocity.Y))
        {
          if ((double) velocity.X > 0.0)
          {
            if (Person.directionmoving != DirectionPressed.Right)
            {
              Person.directionmoving = DirectionPressed.Right;
              Person.SetFrame();
            }
          }
          else if (Person.directionmoving != DirectionPressed.Left)
          {
            Person.directionmoving = DirectionPressed.Left;
            Person.SetFrame();
          }
        }
        else if ((double) velocity.Y > 0.0)
        {
          if (Person.directionmoving != DirectionPressed.Up)
          {
            Person.directionmoving = DirectionPressed.Up;
            Person.SetFrame();
          }
        }
        else if (Person.directionmoving != DirectionPressed.Down)
        {
          Person.directionmoving = DirectionPressed.Down;
          Person.SetFrame();
        }
        float num1 = 100f;
        if (TrailerDemoFlags.HasTrailerFlag)
          num1 = 45f;
        Vector2Int worldSpaceToTile1 = TileMath.GetWorldSpaceToTile(Person.vLocation);
        float num2 = 4f;
        float num3 = 8f;
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        bool flag4 = false;
        bool flag5 = false;
        float num4 = 7f;
        if (this.Velocity != player.inputmap.CharacterMovementStick)
        {
          if ((double) this.Velocity.X < (double) player.inputmap.CharacterMovementStick.X)
          {
            this.Velocity.X += DeltaTime * num4;
            if ((double) this.Velocity.X > (double) player.inputmap.CharacterMovementStick.X)
              this.Velocity.X = player.inputmap.CharacterMovementStick.X;
          }
          else if ((double) this.Velocity.X > (double) player.inputmap.CharacterMovementStick.X)
          {
            this.Velocity.X -= DeltaTime * num4;
            if ((double) this.Velocity.X < (double) player.inputmap.CharacterMovementStick.X)
              this.Velocity.X = player.inputmap.CharacterMovementStick.X;
          }
          if ((double) this.Velocity.Y < (double) player.inputmap.CharacterMovementStick.Y)
          {
            this.Velocity.Y += DeltaTime * num4;
            if ((double) this.Velocity.Y > (double) player.inputmap.CharacterMovementStick.Y)
              this.Velocity.Y = player.inputmap.CharacterMovementStick.Y;
          }
          else if ((double) this.Velocity.Y > (double) player.inputmap.CharacterMovementStick.Y)
          {
            this.Velocity.Y -= DeltaTime * num4;
            if ((double) this.Velocity.Y < (double) player.inputmap.CharacterMovementStick.Y)
              this.Velocity.Y = player.inputmap.CharacterMovementStick.Y;
          }
        }
        Person.vLocation.X += this.Velocity.X * DeltaTime * num1;
        Person.vLocation.Y += (float) ((double) this.Velocity.Y * (double) DeltaTime * -(double) num1);
        Vector2 tileToWorldSpace = TileMath.GetTileToWorldSpace(worldSpaceToTile1);
        if ((double) this.Velocity.X < 0.0)
        {
          if (Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X - 1, worldSpaceToTile1.Y))
          {
            flag2 = true;
          }
          else
          {
            if (Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X - 1, worldSpaceToTile1.Y - 1) && (double) Person.vLocation.Y < (double) tileToWorldSpace.Y - (double) num2 && (double) Person.vLocation.X < (double) tileToWorldSpace.X - (double) num2)
            {
              if (!Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X, worldSpaceToTile1.Y - 1))
              {
                player.inputmap.CharacterMovementStick.X = 0.0f;
                Person.vLocation.X = tileToWorldSpace.X - num2;
                this.Velocity.X = 0.0f;
              }
              else
              {
                player.inputmap.CharacterMovementStick.Y = 0.0f;
                Person.vLocation.Y = tileToWorldSpace.Y - num2;
                this.Velocity.Y = 0.0f;
              }
            }
            if (Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X - 1, worldSpaceToTile1.Y + 1) && (double) Person.vLocation.Y > (double) tileToWorldSpace.Y + (double) num3 && (double) Person.vLocation.X < (double) tileToWorldSpace.X - (double) num2)
            {
              if (!Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X, worldSpaceToTile1.Y + 1))
              {
                player.inputmap.CharacterMovementStick.X = 0.0f;
                Person.vLocation.X = tileToWorldSpace.X - num2;
                this.Velocity.X = 0.0f;
              }
              else
              {
                player.inputmap.CharacterMovementStick.Y = 0.0f;
                Person.vLocation.Y = tileToWorldSpace.Y + num3;
                this.Velocity.Y = 0.0f;
              }
            }
          }
        }
        else if ((double) this.Velocity.X > 0.0)
        {
          if (Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X + 1, worldSpaceToTile1.Y))
          {
            flag1 = true;
          }
          else
          {
            if (Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X + 1, worldSpaceToTile1.Y - 1) && (double) Person.vLocation.Y < (double) tileToWorldSpace.Y - (double) num2 && (double) Person.vLocation.X > (double) tileToWorldSpace.X + (double) num2)
            {
              if (!Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X, worldSpaceToTile1.Y - 1))
              {
                this.Velocity.X = 0.0f;
                player.inputmap.CharacterMovementStick.X = 0.0f;
                Person.vLocation.X = tileToWorldSpace.X + num2;
              }
              else
              {
                player.inputmap.CharacterMovementStick.Y = 0.0f;
                this.Velocity.Y = 0.0f;
                Person.vLocation.Y = tileToWorldSpace.Y - num2;
              }
            }
            if (Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X + 1, worldSpaceToTile1.Y + 1) && (double) Person.vLocation.Y > (double) tileToWorldSpace.Y + (double) num3 && (double) Person.vLocation.X > (double) tileToWorldSpace.X + (double) num2)
            {
              if (!Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X, worldSpaceToTile1.Y + 1))
              {
                player.inputmap.CharacterMovementStick.X = 0.0f;
                this.Velocity.X = 0.0f;
                Person.vLocation.X = tileToWorldSpace.X + num2;
              }
              else
              {
                this.Velocity.Y = 0.0f;
                player.inputmap.CharacterMovementStick.Y = 0.0f;
                Person.vLocation.Y = tileToWorldSpace.Y + num3;
              }
            }
          }
        }
        if ((double) this.Velocity.Y < 0.0)
        {
          if (Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X, worldSpaceToTile1.Y + 1))
          {
            flag3 = true;
          }
          else
          {
            if (Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X - 1, worldSpaceToTile1.Y + 1) && (double) Person.vLocation.X < (double) tileToWorldSpace.X - (double) num2 && (double) Person.vLocation.Y > (double) tileToWorldSpace.Y + (double) num3)
            {
              if (!Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X - 1, worldSpaceToTile1.Y))
              {
                Person.vLocation.Y = tileToWorldSpace.Y + num3;
                player.inputmap.CharacterMovementStick.Y = 0.0f;
                this.Velocity.Y = 0.0f;
              }
              else
              {
                Person.vLocation.X = tileToWorldSpace.X - num2;
                player.inputmap.CharacterMovementStick.X = 0.0f;
              }
            }
            if (Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X + 1, worldSpaceToTile1.Y + 1) && (double) Person.vLocation.X > (double) tileToWorldSpace.X + (double) num2 && (double) Person.vLocation.Y > (double) tileToWorldSpace.Y + (double) num3)
            {
              if (!Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X + 1, worldSpaceToTile1.Y))
              {
                Person.vLocation.Y = tileToWorldSpace.Y + num3;
                player.inputmap.CharacterMovementStick.Y = 0.0f;
                this.Velocity.Y = 0.0f;
              }
              else
              {
                Person.vLocation.X = tileToWorldSpace.X + num2;
                this.Velocity.X = 0.0f;
                player.inputmap.CharacterMovementStick.X = 0.0f;
              }
            }
          }
        }
        else if ((double) this.Velocity.Y > 0.0)
        {
          if (Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X, worldSpaceToTile1.Y - 1))
          {
            flag4 = true;
          }
          else
          {
            if (Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X - 1, worldSpaceToTile1.Y - 1) && (double) Person.vLocation.X < (double) tileToWorldSpace.X - (double) num2 && (double) Person.vLocation.Y < (double) tileToWorldSpace.Y - (double) num2)
            {
              if (!Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X - 1, worldSpaceToTile1.Y))
              {
                Person.vLocation.Y = tileToWorldSpace.Y - num2;
                player.inputmap.CharacterMovementStick.Y = 0.0f;
                this.Velocity.Y = 0.0f;
              }
              else
              {
                Person.vLocation.X = tileToWorldSpace.X - num2;
                player.inputmap.CharacterMovementStick.X = 0.0f;
                this.Velocity.X = 0.0f;
              }
            }
            if (Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X + 1, worldSpaceToTile1.Y - 1) && (double) Person.vLocation.X > (double) tileToWorldSpace.X + (double) num2 && (double) Person.vLocation.Y < (double) tileToWorldSpace.Y - (double) num2)
            {
              if (!Z_GameFlags.pathfinder.GetIsBlocked(worldSpaceToTile1.X + 1, worldSpaceToTile1.Y))
              {
                Person.vLocation.Y = tileToWorldSpace.Y - num2;
                this.Velocity.Y = 0.0f;
                player.inputmap.CharacterMovementStick.Y = 0.0f;
              }
              else
              {
                Person.vLocation.X = tileToWorldSpace.X + num2;
                player.inputmap.CharacterMovementStick.X = 0.0f;
                this.Velocity.X = 0.0f;
              }
            }
          }
        }
        if (flag2 | flag1 | flag4 | flag3)
        {
          Vector2Int worldSpaceToTile2 = TileMath.GetWorldSpaceToTile(Person.vLocation);
          if (flag2)
          {
            if (worldSpaceToTile2.X == worldSpaceToTile1.X)
            {
              if ((double) Person.vLocation.X < (double) tileToWorldSpace.X - (double) num2)
              {
                player.inputmap.CharacterMovementStick.X = 0.0f;
                Person.vLocation.X = tileToWorldSpace.X - num2;
              }
            }
            else
            {
              player.inputmap.CharacterMovementStick.X = 0.0f;
              Person.vLocation.X = tileToWorldSpace.X - num2;
            }
          }
          else if (flag1)
          {
            if (worldSpaceToTile2.X == worldSpaceToTile1.X)
            {
              if ((double) Person.vLocation.X > (double) tileToWorldSpace.X + (double) num2)
              {
                player.inputmap.CharacterMovementStick.X = 0.0f;
                Person.vLocation.X = tileToWorldSpace.X + num2;
              }
            }
            else
            {
              player.inputmap.CharacterMovementStick.X = 0.0f;
              Person.vLocation.X = tileToWorldSpace.X + num2;
            }
          }
          else if (flag5 | flag5)
          {
            if ((double) player.inputmap.CharacterMovementStick.X > 0.0)
            {
              if (flag5)
              {
                if (worldSpaceToTile2.X == worldSpaceToTile1.X)
                {
                  if ((double) Person.vLocation.X > (double) tileToWorldSpace.X + (double) num2 && (double) Person.vLocation.Y < (double) tileToWorldSpace.Y - (double) num2)
                  {
                    player.inputmap.CharacterMovementStick.X = 0.0f;
                    Person.vLocation.X = tileToWorldSpace.X + num2;
                  }
                }
                else if ((double) Person.vLocation.Y < (double) tileToWorldSpace.Y - (double) num2)
                {
                  player.inputmap.CharacterMovementStick.X = 0.0f;
                  Person.vLocation.X = tileToWorldSpace.X + num2;
                }
              }
              else if (flag5)
              {
                if (worldSpaceToTile2.X == worldSpaceToTile1.X)
                {
                  if ((double) Person.vLocation.X > (double) tileToWorldSpace.X + (double) num2 && (double) Person.vLocation.Y > (double) tileToWorldSpace.Y + (double) num2)
                  {
                    player.inputmap.CharacterMovementStick.X = 0.0f;
                    Person.vLocation.X = tileToWorldSpace.X + num2;
                  }
                }
                else if ((double) Person.vLocation.Y > (double) tileToWorldSpace.Y + (double) num2)
                {
                  player.inputmap.CharacterMovementStick.X = 0.0f;
                  Person.vLocation.X = tileToWorldSpace.X + num2;
                }
              }
            }
            else if ((double) player.inputmap.CharacterMovementStick.X < 0.0)
            {
              if (flag5)
              {
                if (worldSpaceToTile2.X == worldSpaceToTile1.X)
                {
                  if ((double) Person.vLocation.X < (double) tileToWorldSpace.X - (double) num2 && (double) Person.vLocation.Y < (double) tileToWorldSpace.Y - (double) num2)
                  {
                    player.inputmap.CharacterMovementStick.X = 0.0f;
                    Person.vLocation.X = tileToWorldSpace.X - num2;
                  }
                }
                else if ((double) Person.vLocation.Y < (double) tileToWorldSpace.Y - (double) num2)
                {
                  player.inputmap.CharacterMovementStick.X = 0.0f;
                  Person.vLocation.X = tileToWorldSpace.X - num2;
                }
              }
              else if (flag5)
              {
                if (worldSpaceToTile2.X == worldSpaceToTile1.X)
                {
                  if ((double) Person.vLocation.X > (double) tileToWorldSpace.X + (double) num2 && (double) Person.vLocation.Y < (double) tileToWorldSpace.Y - (double) num2)
                  {
                    player.inputmap.CharacterMovementStick.X = 0.0f;
                    Person.vLocation.X = tileToWorldSpace.X + num2;
                  }
                }
                else if ((double) Person.vLocation.Y < (double) tileToWorldSpace.Y - (double) num2)
                {
                  player.inputmap.CharacterMovementStick.X = 0.0f;
                  Person.vLocation.X = tileToWorldSpace.X + num2;
                }
              }
            }
          }
          if (flag4)
          {
            if (worldSpaceToTile2.Y == worldSpaceToTile1.Y)
            {
              if ((double) Person.vLocation.Y < (double) tileToWorldSpace.Y - (double) num2)
              {
                player.inputmap.CharacterMovementStick.Y = 0.0f;
                Person.vLocation.Y = tileToWorldSpace.Y - num2;
              }
            }
            else
            {
              player.inputmap.CharacterMovementStick.Y = 0.0f;
              Person.vLocation.Y = tileToWorldSpace.Y - num2;
            }
          }
          else if (flag3)
          {
            if (worldSpaceToTile2.Y == worldSpaceToTile1.Y)
            {
              if ((double) Person.vLocation.Y > (double) tileToWorldSpace.Y + (double) num3)
              {
                player.inputmap.CharacterMovementStick.Y = 0.0f;
                Person.vLocation.Y = tileToWorldSpace.Y + num3;
              }
            }
            else
            {
              player.inputmap.CharacterMovementStick.Y = 0.0f;
              Person.vLocation.Y = tileToWorldSpace.Y + num3;
            }
          }
        }
        Person.UpdateAnimation((float) ((double) DeltaTime * (double) player.inputmap.CharacterMovementStick.Length() * 1.5 * ((double) num1 * 0.00666659977287054)));
        Vector2Int worldSpaceToTile3 = TileMath.GetWorldSpaceToTile(Person.vLocation);
        if (!pathnavigator.CurrentTile.CompareMatches(worldSpaceToTile3))
        {
          pathnavigator.IsNavigating = false;
          pathnavigator.CurrentTile = worldSpaceToTile3;
          return true;
        }
      }
      return false;
    }
  }
}
