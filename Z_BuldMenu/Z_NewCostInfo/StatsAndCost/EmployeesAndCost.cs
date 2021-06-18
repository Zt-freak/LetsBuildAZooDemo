// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.Z_NewCostInfo.StatsAndCost.EmployeesAndCost
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuldMenu.Z_NewCostInfo.StatsAndCost
{
  internal class EmployeesAndCost
  {
    private CustomerFrame customerframe;
    private SimpleTextHandler simpletext;
    private SimpleTextHandler Heading;
    private List<AnimalInFrame> animals;
    public Vector2 Location;

    public EmployeesAndCost(TILETYPE buildingtype, float BaseScale, bool IsVendingMachine)
    {
      float width_ = 220f * BaseScale;
      string TextToWrite1 = "Employees (N/A)";
      if (!IsVendingMachine)
        TextToWrite1 = "Employees";
      this.Heading = new SimpleTextHandler(TextToWrite1, width_, _Scale: BaseScale, _UseFontOnePointFive: true, AutoComplete: true);
      this.Heading.SetAllColours(ColourData.Z_Cream);
      float y = this.Heading.GetSize().Y;
      this.Heading.Location.X = width_ * -0.5f;
      float num1 = y + BaseScale * 10f;
      string TextToWrite2;
      int num2;
      if (IsVendingMachine)
      {
        TextToWrite2 = "Automated facilty. Requires no employees, but must be repaired by a mechanic.";
        num2 = 1;
      }
      else
      {
        TextToWrite2 = "Employee Capacity: 1~Average Salary $70";
        num2 = 1;
      }
      this.simpletext = new SimpleTextHandler(TextToWrite2, width_, _Scale: BaseScale, AutoComplete: true);
      this.simpletext.Location.X = width_ * -0.5f;
      this.simpletext.SetAllColours(ColourData.Z_Cream);
      this.simpletext.Location.Y = num1;
      float num3 = num1 + this.simpletext.GetSize().Y + BaseScale * 10f;
      this.animals = new List<AnimalInFrame>();
      float num4 = num3 + 5f * BaseScale;
      for (int index = 0; index < num2; ++index)
      {
        if (IsVendingMachine)
        {
          List<AnimalType> employeeAsLict = Employees.GetEmployeeAsLict(EmployeeType.Mechanic);
          this.animals.Add(new AnimalInFrame(employeeAsLict[Game1.Rnd.Next(0, employeeAsLict.Count)], AnimalType.None, TargetSize: (50f * BaseScale), FrameEdgeBuffer: (6f * BaseScale)));
        }
        else
          this.animals.Add(new AnimalInFrame(EmployeeData.GetBuildingtoEmployee(buildingtype, out bool _), AnimalType.None, TargetSize: (50f * BaseScale), FrameEdgeBuffer: (6f * BaseScale)));
        if (index == 0)
          num4 += this.animals[0].GetSize().Y * 0.5f;
        this.animals[index].Location.X = width_ * -0.5f;
        this.animals[index].Location.X += 35f * BaseScale;
        this.animals[index].Location.X += 60f * BaseScale * (float) index;
        this.animals[index].Location.Y = num4;
      }
      if (this.animals.Count > 0)
        num4 += this.animals[0].GetSize().Y * 0.5f;
      float num5 = num4 + 5f * BaseScale;
      this.customerframe = new CustomerFrame(new Vector2(width_ + 20f * BaseScale, num5 + 20f * BaseScale), CustomerFrameColors.Brown, BaseScale);
      this.Heading.Location.Y -= num5 * 0.5f;
      this.simpletext.Location.Y -= num5 * 0.5f;
      for (int index = 0; index < this.animals.Count; ++index)
        this.animals[index].Location.Y -= num5 * 0.5f;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void UpdateStatsAndCostPanel()
    {
    }

    public void DrawStatsAndCostPanel(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.Heading.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      for (int index = 0; index < this.animals.Count; ++index)
        this.animals[index].DrawAnimalInFrame(Offset, spritebatch);
    }
  }
}
