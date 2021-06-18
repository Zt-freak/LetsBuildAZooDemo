// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.StaffPayment.PayStaffManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;

namespace TinyZoo.Z_WeekOver.StaffPayment
{
  internal class PayStaffManager
  {
    private GameObjectNineSlice FrameForWHoleThing;
    private List<EmplyeeAndStats> people;
    private Vector2 VSCALE;
    private ScreenHeading subheading;
    private StringInBox SalarySummary;
    private TextButton Next;

    public PayStaffManager(Player player)
    {
      this.subheading = new ScreenHeading("SALARY COSTS", 90f);
      this.subheading.header.vLocation = Vector2.Zero;
      Vector3 SecondaryColour;
      this.FrameForWHoleThing = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.FrameForWHoleThing.vLocation = new Vector2(512f, 384f);
      int salaries = player.employees.GetSalaries();
      int cashHeld = player.Stats.GetCashHeld(false);
      if (cashHeld > salaries)
      {
        player.Stats.SpendCash(salaries, SpendingCashOnThis.Wages, player);
        this.SalarySummary = new StringInBox("PAYROLL: $" + (object) salaries, 2f, 150f, true);
        this.SalarySummary.SetAsButtonFrame(BTNColour.Green);
      }
      else
      {
        if (cashHeld > 0)
          player.Stats.SpendCash(cashHeld, SpendingCashOnThis.Wages, player);
        this.SalarySummary = new StringInBox("PAYROLL: $" + (object) salaries + "   YOU DO NOT HAVE ENOUGH MONEY! THE STAFF MISSED OUT ON $" + (object) (salaries - cashHeld) + " WAGES", 2f, 400f, true);
        this.SalarySummary.SetAsButtonFrame(BTNColour.Red);
      }
      this.VSCALE = new Vector2(900f, 600f);
      this.people = new List<EmplyeeAndStats>();
      int num1 = 0;
      int num2 = 0;
      int num3 = 15;
      float num4 = 55f;
      for (int index = 0; index < player.employees.employees.Count; ++index)
      {
        if (player.employees.employees[index].intakeperson == null)
          this.people.Add(new EmplyeeAndStats(player.employees.employees[index].quickemplyeedescription.thisemployee, player.employees.employees[index].quickemplyeedescription.CurrentSalary, EMOTION.None, SecondaryColour));
        else
          this.people.Add(new EmplyeeAndStats(player.employees.employees[index].intakeperson.animaltype, player.employees.employees[index].Salary, EMOTION.None, SecondaryColour));
        this.people[index].vLocation.X = (float) num2 * num4;
        this.people[index].vLocation.Y = (float) (80 * num1);
        ++num2;
        if (num2 == num3)
        {
          num2 = 0;
          ++num1;
        }
        this.people[index].vLocation.X -= num4 * ((float) num3 * 0.5f);
        this.people[index].vLocation.X += num4 * 0.5f;
      }
      this.Next = new TextButton(nameof (Next), 40f);
      this.Next.vLocation = new Vector2(900f, 700f);
    }

    public bool UpdatePayStaffManager(Player player, float DeltaTime, Vector2 Offset)
    {
      for (int index = 0; index < this.people.Count; ++index)
        this.people[index].UpdateEmplyeeAndStats(DeltaTime);
      return this.Next.UpdateTextButton(player, Offset, DeltaTime);
    }

    public void DrawPayStaffManager(Vector2 Offset)
    {
      this.FrameForWHoleThing.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VSCALE);
      this.subheading.DrawScreenHeading(this.FrameForWHoleThing.vLocation + new Vector2(0.0f, this.VSCALE.Y * -0.5f), AssetContainer.pointspritebatch03);
      this.SalarySummary.DrawStringInBox(this.FrameForWHoleThing.vLocation + new Vector2(0.0f, (float) ((double) this.VSCALE.Y * -0.5 + 50.0)));
      for (int index = 0; index < this.people.Count; ++index)
        this.people[index].DrawEmplyeeAndStats(new Vector2(0.0f, -150f) + Offset + this.FrameForWHoleThing.vLocation);
      this.Next.DrawTextButton(Offset, 1f, AssetContainer.pointspritebatch03);
    }
  }
}
