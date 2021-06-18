// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.CurrentDeadAnimals
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;

namespace TinyZoo.Z_BalanceSystems
{
  internal class CurrentDeadAnimals
  {
    internal static List<Vector2Int> DeadAnimalsByCellUID_AndCollector = new List<Vector2Int>();

    public static void AddDeadAnimal(int CellBlockUID) => CurrentDeadAnimals.DeadAnimalsByCellUID_AndCollector.Add(new Vector2Int(CellBlockUID, 0));

    public static void RefreshMeatCollectorsJob(Player player)
    {
      List<Employee> employeesOfThisType = player.employees.GetEmployeesOfThisType(EmployeeType.MeatProcessorWorker);
      for (int index = 0; index < employeesOfThisType.Count; ++index)
        CustomerManager.GetThisEmployee(employeesOfThisType[index]).simperson.CheckJob();
    }

    public static void StartNewDay() => CurrentDeadAnimals.DeadAnimalsByCellUID_AndCollector = new List<Vector2Int>();
  }
}
