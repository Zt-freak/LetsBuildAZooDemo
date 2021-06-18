// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.employees.E_WeekHistory
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;

namespace TinyZoo.PlayerDir.employees
{
  internal class E_WeekHistory
  {
    public int SalaryPaid;
    public int SalaryOwed;
    public List<E_History> ehistory;

    public E_WeekHistory() => this.ehistory = new List<E_History>();

    public void EndedADay(int ActionsAvailableThisDay) => this.ehistory.Add(new E_History(ActionsAvailableThisDay));

    public void TriedToStartAction()
    {
      if (this.ehistory.Count == 0)
        this.ehistory.Add(new E_History(1));
      this.ehistory[this.ehistory.Count - 1].TriedToStartAction();
    }

    public int GetActiosnComplete(int DasToGetFromEnd)
    {
      int num = 0;
      for (int index = this.ehistory.Count - 1; index > -1; --index)
        num += this.ehistory[index].ActionsPerformed;
      return num;
    }

    public void CompletedAction()
    {
      if (this.ehistory.Count == 0)
      {
        this.ehistory.Add(new E_History(0));
        Console.WriteLine("Has to add ehistory - I think this means something happened before the day started");
      }
      this.ehistory[this.ehistory.Count - 1].CompletedAction();
    }

    public E_WeekHistory(Reader reader)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("e", ref _out);
      this.ehistory = new List<E_History>();
      for (int index = 0; index < _out; ++index)
        this.ehistory.Add(new E_History(reader));
    }

    public void SaveE_WeekHistory(Writer writer)
    {
      writer.WriteInt("e", this.ehistory.Count);
      for (int index = 0; index < this.ehistory.Count; ++index)
        this.ehistory[index].Save_E_History(writer);
    }
  }
}
