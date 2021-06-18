// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.Cubes.PayTheStaff
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents;
using TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents.Characters;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_WeekOver.V2.Cubes
{
  internal class PayTheStaff : BaseCube
  {
    private AnimalPopperPanel animalpopperpanel;
    private LimitedLineRenderer limitedlinerenderer;
    private List<Employee> employees;
    private bool AddedFirst;
    private int EmployeeIndex;
    private int PAY;
    private ZGenericText PayoutString;

    public PayTheStaff(float _BaseScale, Player player)
      : base(_BaseScale, true, new Vector3(0.9568627f, 0.9411765f, 0.8784314f))
    {
      List<IntakePerson> intakePersonList = new List<IntakePerson>();
      this.employees = player.employees.employees;
      this.animalpopperpanel = new AnimalPopperPanel((List<IntakePerson>) null, player.employees.employees, _BaseScale, _BaseScale * 60f, 60f);
      this.AddMiniHeading("Salary Payments", new Vector3(0.9511111f, 0.4313726f, 0.172549f));
      this.lerperBaseCube.Delay = 0.5f;
      this.limitedlinerenderer = new LimitedLineRenderer(3, false, this.BaseScale);
      this.limitedlinerenderer.SetAllColours(new Vector3(1.008889f, 0.5803922f, 0.3803922f));
      this.PayoutString = new ZGenericText(this.BaseScale, _UseOnePointFiveFont: true);
      this.PayoutString.SetAllColours(new Vector3(1.008889f, 0.5803922f, 0.3803922f));
      this.AlsoWaitForCashBeforeMovingOn = true;
    }

    public override void UpdateBaseCube(float DeltaTime, Player player, Vector2 Offset)
    {
      if ((double) TinyZoo.Game1.screenfade.fAlpha != 0.0)
        return;
      if ((double) this.lerperBaseCube.Value == 1.0)
      {
        if (!this.AddedFirst)
        {
          this.AddedFirst = true;
          this.ADDanimal(player);
        }
        int num = this.animalpopperpanel.UpdateAnimalPopperPanel(DeltaTime, player);
        for (int index = 0; index < num; ++index)
          this.ADDanimal(player);
      }
      base.UpdateBaseCube(DeltaTime, player, Offset);
    }

    private void ADDanimal(Player player)
    {
      if (this.EmployeeIndex < this.employees.Count && this.employees[this.EmployeeIndex].intakeperson != null)
      {
        AnimalType _AnimalType = this.employees[this.EmployeeIndex].intakeperson == null ? this.employees[this.EmployeeIndex].quickemplyeedescription.thisemployee : this.employees[this.EmployeeIndex].intakeperson.animaltype;
        int salary = this.employees[this.EmployeeIndex].Salary;
        this.limitedlinerenderer.AddLine(EmployeesStats.GetJobTitle(this.employees[this.EmployeeIndex].employeetype, _AnimalType) + " $" + (object) salary);
        this.PAY += salary;
        this.PayoutString.textToWrite = "TOTAL: $" + (object) this.PAY;
        player.Stats.SpendCash_AllowLoan(salary, SpendingCashOnThis.Wages, player, true);
      }
      ++this.EmployeeIndex;
    }

    public override bool LerpComplete(CurrentFinances currentfinances) => this.animalpopperpanel.LerpComplete() && base.LerpComplete(currentfinances);

    public override void DrawBaseCube(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      base.DrawBaseCube(Offset, spritebatch);
      Offset.Y -= 20f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.animalpopperpanel.DrawAnimalPopperPanel(Offset, spritebatch);
      Offset.Y += 30f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.limitedlinerenderer.DrawLimitedLineRenderer(Offset, spritebatch);
      Offset.Y += 60f * this.BaseScale;
      this.PayoutString.DrawZGenericText(Offset, spritebatch);
    }
  }
}
