// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.employees.EHistoryHolder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Z_SummaryPopUps.Generic;

namespace TinyZoo.PlayerDir.employees
{
  internal class EHistoryHolder
  {
    public List<E_WeekHistory> ehistory;
    public int TotalEvents;
    public int TotalSubEvents;

    public EHistoryHolder()
    {
      this.ehistory = new List<E_WeekHistory>();
      this.ehistory.Add(new E_WeekHistory());
    }

    public void EndedADay(int ActionsAvailableThisDay) => this.ehistory[this.ehistory.Count - 1].EndedADay(ActionsAvailableThisDay);

    public void StartNewWeek(int ActionsAvailableThisDay)
    {
      if (this.ehistory.Count <= 0)
        return;
      this.ehistory[this.ehistory.Count - 1].EndedADay(ActionsAvailableThisDay);
    }

    public void CompletedAction()
    {
      ++this.TotalEvents;
      this.ehistory[this.ehistory.Count - 1].CompletedAction();
    }

    public int GetActiosnComplete(TimeSegmentType timesegment, out bool NoData) => throw new Exception("DID NOT FINISH THIS");

    public void TriedToStartAction() => this.ehistory[this.ehistory.Count - 1].TriedToStartAction();

    public EHistoryHolder(Reader reader)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("e", ref _out);
      this.ehistory = new List<E_WeekHistory>();
      for (int index = 0; index < _out; ++index)
        this.ehistory.Add(new E_WeekHistory(reader));
    }

    public void SaveEHistoryHolder(Writer writer)
    {
      writer.WriteInt("e", this.ehistory.Count);
      for (int index = 0; index < this.ehistory.Count; ++index)
        this.ehistory[index].SaveE_WeekHistory(writer);
    }
  }
}
