// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.QuitManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Z_Employees.Emp_Summary.Quit;
using TinyZoo.Z_Employees.QuickPick;

namespace TinyZoo.Z_Employees
{
  internal class QuitManager
  {
    private QuitJobMainPanel quitJobPanel;

    public QuitManager(QuickEmployeeDescription employee)
    {
      this.quitJobPanel = new QuitJobMainPanel(employee);
      this.quitJobPanel.location = new Vector2(512f, 384f);
    }

    public bool CheckMouseOver(Player player) => this.quitJobPanel.CheckMouseOver(player, Vector2.Zero);

    public void UpdateQuitManager(Player player, float DeltaTime)
    {
      switch (this.quitJobPanel.UpdateQuitJobMainPanel(player, DeltaTime, Vector2.Zero, out int _))
      {
      }
    }

    public void DrawQuitManager() => this.quitJobPanel.DrawQuitJobMainPanel(Vector2.Zero);
  }
}
