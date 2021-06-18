// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras.DoorPathFinder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.PathFinding;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras
{
  internal class DoorPathFinder
  {
    internal static void GetPathToPenInterior(
      ref List<PathNode> targets,
      bool IsLeaving,
      Vector2Int SpaceInfrontOfDoor,
      Vector2Int DoorLocation,
      ref Vector2Int InsideLocation)
    {
      if (SpaceInfrontOfDoor.X != DoorLocation.X)
      {
        if (IsLeaving)
        {
          if (SpaceInfrontOfDoor.X > DoorLocation.X)
            targets.Add(new PathNode(DoorLocation.X + 1, DoorLocation.Y));
          else
            targets.Add(new PathNode(DoorLocation.X - 1, DoorLocation.Y));
        }
        else if (SpaceInfrontOfDoor.X > DoorLocation.X)
          targets.Add(new PathNode(DoorLocation.X - 1, DoorLocation.Y));
        else
          targets.Add(new PathNode(DoorLocation.X + 1, DoorLocation.Y));
      }
      else if (IsLeaving)
      {
        if (SpaceInfrontOfDoor.Y > DoorLocation.Y)
          targets.Add(new PathNode(DoorLocation.X, DoorLocation.Y + 1));
        else
          targets.Add(new PathNode(DoorLocation.X, DoorLocation.Y - 1));
      }
      else if (SpaceInfrontOfDoor.Y > DoorLocation.Y)
        targets.Add(new PathNode(DoorLocation.X, DoorLocation.Y - 1));
      else
        targets.Add(new PathNode(DoorLocation.X, DoorLocation.Y + 1));
      InsideLocation = new Vector2Int(targets[1].XLoc, targets[1].YLoc);
    }

    internal static void GetPathToBuildingInterior(
      ref List<PathNode> targets,
      bool IsLeaving,
      Vector2Int SpaceInfrontOfDoor,
      Vector2Int InsideLocation)
    {
      int num1 = 1;
      if (IsLeaving)
        num1 = 0;
      if (SpaceInfrontOfDoor.X != InsideLocation.X)
      {
        int num2 = InsideLocation.X - SpaceInfrontOfDoor.X;
        if (num2 < 0)
        {
          for (int index = 0; index > num2; --index)
            targets.Add(new PathNode(SpaceInfrontOfDoor.X - num1 + index, SpaceInfrontOfDoor.Y));
        }
        else
        {
          for (int index = 0; index < num2; ++index)
            targets.Add(new PathNode(SpaceInfrontOfDoor.X + num1 + index, SpaceInfrontOfDoor.Y));
        }
      }
      else
      {
        int num2 = InsideLocation.Y - SpaceInfrontOfDoor.Y;
        if (num2 < 0)
        {
          for (int index = 0; index > num2; --index)
            targets.Add(new PathNode(SpaceInfrontOfDoor.X, SpaceInfrontOfDoor.Y - num1 + index));
        }
        else
        {
          for (int index = 0; index < num2; ++index)
            targets.Add(new PathNode(SpaceInfrontOfDoor.X, SpaceInfrontOfDoor.Y - num1 + index));
        }
      }
      if (!IsLeaving)
        return;
      targets.Reverse();
    }
  }
}
