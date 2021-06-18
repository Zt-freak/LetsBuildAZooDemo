// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.PersonCaller.CallPerson
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;

namespace TinyZoo.Z_AnimalsAndPeople.PersonCaller
{
  internal class CallPerson
  {
    internal static Employee FindNearestNonBusyPerson(
      List<Employee> Employees,
      Vector2Int GoHere,
      out WalkingPerson walker)
    {
      List<WalkingPerson> walkingPersonList = new List<WalkingPerson>();
      List<int> intList1 = new List<int>();
      List<int> intList2 = new List<int>();
      int num = -1;
      int index1 = -1;
      for (int index2 = 0; index2 < Employees.Count; ++index2)
      {
        walkingPersonList.Add(CustomerManager.GetThisEmployee(Employees[0]));
        int PathLength;
        if (walkingPersonList[index2].CheckCanWalkHere(GoHere, out PathLength))
        {
          intList1.Add(index2);
          intList2.Add(PathLength);
          if (num == -1 || num > PathLength)
          {
            num = PathLength;
            index1 = index2;
          }
        }
      }
      if (index1 > -1)
      {
        walker = walkingPersonList[index1];
        return Employees[index1];
      }
      walker = walkingPersonList[0];
      return Employees[0];
    }
  }
}
