// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.ArchitectController
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using TinyZoo.PlayerDir;
using TinyZoo.Z_BalanceSystems.LoadValues;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class ArchitectController
  {
    private int Progress;
    private int CyclesForPorgress;
    private Employee RefEmployee;
    private bool CyclesSet;

    public ArchitectController(Employee _RefEmployee)
    {
      this.RefEmployee = _RefEmployee;
      this.SetCycles();
    }

    private void SetCycles()
    {
      this.CyclesSet = true;
      this.CyclesForPorgress = 150 - ((int) this.RefEmployee.quickemplyeedescription.Determination + (int) this.RefEmployee.quickemplyeedescription.Determination / 2);
      this.CyclesForPorgress += 50;
      this.CyclesForPorgress /= 2;
      if (this.CyclesForPorgress >= 25)
        return;
      this.CyclesForPorgress = 50;
    }

    public void UpdateResearchProgress(float Cycles, Player player)
    {
      if (!this.CyclesSet)
      {
        if (this.RefEmployee == null)
          throw new Exception("NOT SETABLE");
        this.SetCycles();
      }
      else if (this.CyclesForPorgress == 0)
        this.CyclesForPorgress = 200;
      for (this.Progress += (int) ((double) Cycles * (double) BVal.ReaserachSpeedMult); this.Progress > this.CyclesForPorgress; this.Progress -= this.CyclesForPorgress)
      {
        ++this.RefEmployee.ResearchProgress;
        if (this.RefEmployee.ResearchProgress >= 100)
        {
          if (this.RefEmployee.ResearchPointHistory.Count > 0 && this.RefEmployee.ResearchPointHistory[this.RefEmployee.ResearchPointHistory.Count - 1].X == (int) Player.financialrecords.GetDaysPassed())
            ++this.RefEmployee.ResearchPointHistory[this.RefEmployee.ResearchPointHistory.Count - 1].Y;
          else
            this.RefEmployee.ResearchPointHistory.Add(new Vector2Int((int) Player.financialrecords.GetDaysPassed(), 1));
          this.RefEmployee.ResearchProgress -= 100;
          ++player.unlocks.ResearchPoints;
          LiveStats.EarnedResearch = true;
        }
      }
    }
  }
}
