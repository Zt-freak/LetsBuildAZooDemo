// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.employees.E_History
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;

namespace TinyZoo.PlayerDir.employees
{
  internal class E_History
  {
    public int ActionsPerformed;
    public int ActionsAvailableThisDay;
    public int ActionsStarted;

    public E_History(int _ActionsAvailableThisDay) => this.ActionsAvailableThisDay = _ActionsAvailableThisDay;

    public void TriedToStartAction() => ++this.ActionsStarted;

    public void CompletedAction() => ++this.ActionsPerformed;

    public E_History(Reader reader)
    {
      int num1 = (int) reader.ReadInt("e", ref this.ActionsPerformed);
      int num2 = (int) reader.ReadInt("e", ref this.ActionsAvailableThisDay);
      int num3 = (int) reader.ReadInt("e", ref this.ActionsStarted);
    }

    public void Save_E_History(Writer writer)
    {
      writer.WriteInt("e", this.ActionsPerformed);
      writer.WriteInt("e", this.ActionsAvailableThisDay);
      writer.WriteInt("e", this.ActionsStarted);
    }
  }
}
