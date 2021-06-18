// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.FiringManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Z_Employees.Emp_Summary;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_Employees
{
  internal class FiringManager
  {
    private bool SetUpNow;
    private int TotalPeopleFired;
    private FireEmployeePanel firepanel;
    private Employee FiringThisEmployee;
    private int EmployeeIndex;
    private bool CancelIsAllowed;
    private BlackOut blackout;
    private int OwedThisMuch;

    public FiringManager(int _TotalPeopleFired, bool _CancelIsAllowed)
    {
      this.blackout = new BlackOut();
      this.blackout.SetAlpha(0.0f);
      this.blackout.SetAlpha(false, 0.4f, 0.0f, 0.8f);
      this.TotalPeopleFired = _TotalPeopleFired;
      this.SetUpNow = true;
      this.CancelIsAllowed = _CancelIsAllowed;
    }

    public bool UpdateFiringManager(Player player, float DeltaTime)
    {
      this.blackout.UpdateColours(DeltaTime);
      if (this.SetUpNow)
      {
        this.SetUpNow = false;
        for (int index = 0; index < player.employees.employees.Count; ++index)
        {
          if (player.employees.employees[index].quickemplyeedescription != null && player.employees.employees[index].quickemplyeedescription.FireThisEmployee)
          {
            this.OwedThisMuch = Z_GameFlags.GetSalaryThisWeekUntilNow(player.employees.employees[index].Salary, player, player.employees.employees[index].DaysEmployed);
            float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
            this.firepanel = new FireEmployeePanel(player.employees.employees[index].quickemplyeedescription, this.CancelIsAllowed, player.employees.employees[this.EmployeeIndex].Salary * 7, this.OwedThisMuch, baseScaleForUi);
            this.firepanel.location = new Vector2(512f, 384f);
            this.FiringThisEmployee = player.employees.employees[index];
            this.EmployeeIndex = index;
          }
        }
      }
      int thisWeekUntilNow = Z_GameFlags.GetSalaryThisWeekUntilNow(player.employees.employees[this.EmployeeIndex].Salary, player, player.employees.employees[this.EmployeeIndex].DaysEmployed);
      if (thisWeekUntilNow != this.OwedThisMuch)
        this.OwedThisMuch = thisWeekUntilNow;
      SeverenceOption sevoption = this.firepanel.UpdateFireEmployeePanel(DeltaTime, player, Vector2.Zero, this.OwedThisMuch);
      switch (sevoption)
      {
        case SeverenceOption.Count:
          return false;
        case SeverenceOption.Cancel:
          return true;
        default:
          player.unions.FiredEmployee(sevoption, player, this.OwedThisMuch, 50);
          switch (sevoption)
          {
            case SeverenceOption.PayWagesOnly:
              player.Stats.SpendCash(this.OwedThisMuch, SpendingCashOnThis.Wages, player);
              break;
            case SeverenceOption.PayAll:
              player.Stats.SpendCash(this.OwedThisMuch + player.employees.employees[this.EmployeeIndex].Salary * 7, SpendingCashOnThis.Wages, player);
              break;
          }
          this.FireNow(player);
          return true;
      }
    }

    private void FireNow(Player player)
    {
      LiveStats.CheckQuitersOnFiring(player.employees.employees[this.EmployeeIndex]);
      if (player.employees.employees[this.EmployeeIndex].quickemplyeedescription != null)
        player.shopstatus.RemoveThisEmployee(player.employees.employees[this.EmployeeIndex]);
      CustomerManager.RemoveThisEmployee(player.employees.employees[this.EmployeeIndex]);
      player.employees.RemoveEmployee(player.employees.employees[this.EmployeeIndex]);
      Z_NotificationManager.RescrubShops = true;
    }

    public void DrawFiringManager()
    {
      if (this.firepanel == null)
        return;
      this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      this.firepanel.DrawFireEmployeePanel(Vector2.Zero);
    }
  }
}
