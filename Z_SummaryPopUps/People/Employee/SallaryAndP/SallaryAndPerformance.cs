// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Employee.SallaryAndP.SallaryAndPerformance
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_SummaryPopUps.People.Animal;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Employee.SallaryAndP
{
  internal class SallaryAndPerformance
  {
    public CustomerFrame customerframe;
    public Vector2 Location;
    private SimpleTextHandler text;
    private GameObject CLRRR;
    private MiniHeading miniheading;

    public SallaryAndPerformance(Vector2 MasterVScale, SimPerson simperson, float MasterScaleMult)
    {
      this.miniheading = new MiniHeading(MasterVScale, "Employee Records:", MasterScaleMult);
      string str = "Customers served: 1";
      string TextToWrite;
      switch (simperson.Ref_Employee.employeetype)
      {
        case EmployeeType.Janitor:
          TextToWrite = "Garbage Collected:" + (object) simperson.Ref_Employee.ehistory.TotalEvents + "~Weekly Salary: $" + (object) simperson.Ref_Employee.Salary + "~Days Employed: $" + (object) simperson.Ref_Employee.DaysEmployed;
          break;
        case EmployeeType.Keeper:
          TextToWrite = "Pens Cleaned:" + (object) simperson.Ref_Employee.ehistory.TotalEvents + "~Animals Fed:" + (object) simperson.Ref_Employee.ehistory.TotalSubEvents + "~Weekly Salary: $" + (object) simperson.Ref_Employee.Salary + "~Days Employed: $" + (object) simperson.Ref_Employee.DaysEmployed;
          break;
        default:
          TextToWrite = str + "~Weekly Salary: $" + (object) simperson.Ref_Employee.Salary + "~Days Employed: $" + (object) simperson.Ref_Employee.DaysEmployed;
          break;
      }
      this.customerframe = new CustomerFrame(new Vector2(MasterVScale.X - AnimalPopUpManager.Space, 120f));
      float PercentagePfScreenWidth = (float) (((double) this.customerframe.VSCale.X - (double) AnimalPopUpManager.VerticalBuffer * 1.0) / 1024.0);
      this.text = new SimpleTextHandler(TextToWrite, false, PercentagePfScreenWidth, RenderMath.GetPixelSizeBestMatch(1f), false, false);
      this.text.paragraph.linemaker.SetAllColours(ColourData.Z_CreamFADED);
      this.text.AutoCompleteParagraph();
      this.CLRRR = new GameObject();
      this.CLRRR.SetAllColours(ColourData.Z_Cream);
    }

    public void UpdateSallaryAndPerformance()
    {
    }

    public void DrawSallaryAndPerformance(SpriteBatch spritebatch, Vector2 Offset)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      TextFunctions.DrawTextWithDropShadow("Thoughts:", RenderMath.GetPixelSizeBestMatch(1f), Offset + new Vector2(this.customerframe.VSCale.X * -0.5f + AnimalPopUpManager.VerticalBuffer, this.customerframe.VSCale.Y * -0.5f + AnimalPopUpManager.VerticalBuffer), this.CLRRR.GetColour(), 1f, AssetContainer.SpringFontX1AndHalf, spritebatch, false);
      this.text.DrawSimpleTextHandler(Offset + new Vector2(this.customerframe.VSCale.X * -0.5f + AnimalPopUpManager.VerticalBuffer, (float) ((double) this.customerframe.VSCale.Y * -0.5 + (double) AnimalPopUpManager.VerticalBuffer * 3.0)), 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}
