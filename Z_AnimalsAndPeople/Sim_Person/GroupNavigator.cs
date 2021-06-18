// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.GroupNavigator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class GroupNavigator
  {
    private List<WalkingPerson> Ref_WalkingPeopleInThisGroup;
    private int RoundRobin;

    public GroupNavigator() => this.Ref_WalkingPeopleInThisGroup = new List<WalkingPerson>();

    public void AddMe(WalkingPerson walkingperson) => this.Ref_WalkingPeopleInThisGroup.Add(walkingperson);

    public int GetGroupSize() => this.Ref_WalkingPeopleInThisGroup.Count;

    public List<WalkingPerson> GetWalkingPersons() => this.Ref_WalkingPeopleInThisGroup;

    public Vector2Int TryFindAPlaceToWalkTo(WalkingPerson walkingperson)
    {
      for (int index = this.Ref_WalkingPeopleInThisGroup.Count - 1; index > -1; --index)
      {
        if (!this.Ref_WalkingPeopleInThisGroup[index].IsAtive || this.Ref_WalkingPeopleInThisGroup[index].simperson.memberofthepublic.LeftParkEarly || this.Ref_WalkingPeopleInThisGroup[index].simperson.memberofthepublic.LeftTheParkBecauseOfThis != ParkLeavingReason.None)
          this.Ref_WalkingPeopleInThisGroup.RemoveAt(index);
      }
      if (walkingperson == this.Ref_WalkingPeopleInThisGroup[0])
        return (Vector2Int) null;
      Vector2Int vector2Int = new Vector2Int(this.Ref_WalkingPeopleInThisGroup[0].pathnavigator.CurrentTile);
      if (this.RoundRobin > 11)
        this.RoundRobin = 0;
      for (int roundRobin = this.RoundRobin; roundRobin < 12; ++roundRobin)
      {
        ++this.RoundRobin;
        switch (roundRobin)
        {
          case 0:
            if (!Z_GameFlags.pathfinder.GetIsBlocked(vector2Int.X + 1, vector2Int.Y))
              return new Vector2Int(vector2Int.X + 1, vector2Int.Y);
            break;
          case 1:
            if (!Z_GameFlags.pathfinder.GetIsBlocked(vector2Int.X - 1, vector2Int.Y))
              return new Vector2Int(vector2Int.X - 1, vector2Int.Y);
            break;
          case 2:
            if (!Z_GameFlags.pathfinder.GetIsBlocked(vector2Int.X, vector2Int.Y + 1))
              return new Vector2Int(vector2Int.X, vector2Int.Y + 1);
            break;
          case 3:
            if (!Z_GameFlags.pathfinder.GetIsBlocked(vector2Int.X, vector2Int.Y - 1))
              return new Vector2Int(vector2Int.X, vector2Int.Y - 1);
            break;
          case 4:
            if (!Z_GameFlags.pathfinder.GetIsBlocked(vector2Int.X + 1, vector2Int.Y - 1))
              return new Vector2Int(vector2Int.X + 1, vector2Int.Y - 1);
            break;
          case 5:
            if (!Z_GameFlags.pathfinder.GetIsBlocked(vector2Int.X - 1, vector2Int.Y + 1))
              return new Vector2Int(vector2Int.X - 1, vector2Int.Y + 1);
            break;
          case 6:
            if (!Z_GameFlags.pathfinder.GetIsBlocked(vector2Int.X + 1, vector2Int.Y + 1))
              return new Vector2Int(vector2Int.X + 1, vector2Int.Y + 1);
            break;
          case 7:
            if (!Z_GameFlags.pathfinder.GetIsBlocked(vector2Int.X - 1, vector2Int.Y - 1))
              return new Vector2Int(vector2Int.X - 1, vector2Int.Y - 1);
            break;
          case 8:
            if (!Z_GameFlags.pathfinder.GetIsBlocked(vector2Int.X + 2, vector2Int.Y))
              return new Vector2Int(vector2Int.X + 2, vector2Int.Y);
            break;
          case 9:
            if (!Z_GameFlags.pathfinder.GetIsBlocked(vector2Int.X - 2, vector2Int.Y))
              return new Vector2Int(vector2Int.X - 2, vector2Int.Y);
            break;
          case 10:
            if (!Z_GameFlags.pathfinder.GetIsBlocked(vector2Int.X, vector2Int.Y + 2))
              return new Vector2Int(vector2Int.X, vector2Int.Y + 2);
            break;
          case 11:
            if (!Z_GameFlags.pathfinder.GetIsBlocked(vector2Int.X, vector2Int.Y - 2))
              return new Vector2Int(vector2Int.X, vector2Int.Y - 2);
            break;
        }
      }
      if (this.RoundRobin == 11)
        this.RoundRobin = 0;
      return vector2Int;
    }
  }
}
