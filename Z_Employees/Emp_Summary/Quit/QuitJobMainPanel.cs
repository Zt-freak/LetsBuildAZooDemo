// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.Emp_Summary.Quit.QuitJobMainPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_Employees.Emp_Summary.Quit
{
  internal class QuitJobMainPanel
  {
    public Vector2 location;
    private BigBrownPanel mainFrame;
    private MiniHeading header;
    private EmployeeSummaryPanel employeePanel;
    private SimpleTextHandler text;
    private QuitOptionsPanel quitOptionsPanel;

    public QuitJobMainPanel(QuickEmployeeDescription employee)
    {
      this.mainFrame = new BigBrownPanel();
      this.mainFrame.vScale = new Vector2(335f, 455f);
      this.employeePanel = new EmployeeSummaryPanel(employee, false);
      this.employeePanel.location.Y = (float) (-((double) this.mainFrame.vScale.Y * 0.5) + (double) this.employeePanel.brownFrame.VSCale.Y * 0.5);
      this.employeePanel.location.Y += AnimalPopUpManager.VerticalBuffer + AnimalPopUpManager.TopAreaBuffer;
      EmployeeType employeetype;
      EmployeeData.IsThisAnEmployee(employee.thisemployee, out employeetype);
      string jobTitle = EmployeesStats.GetJobTitle(employeetype, employee.thisemployee);
      this.header = new MiniHeading(this.mainFrame.vScale, "Resignation", 1.2f);
      this.text = new SimpleTextHandler(employee.NAME + " wants to resign as " + jobTitle + ". What would you like to do?", true, (float) (((double) this.mainFrame.vScale.X - 25.0) / 1024.0), RenderMath.GetPixelSizeBestMatch(GameFlags.GetSmallTextScale()));
      this.text.SetAllColours(ColourData.Z_Cream);
      this.text.AutoCompleteParagraph();
      this.text.Location = this.employeePanel.location + new Vector2(0.0f, (float) ((double) this.employeePanel.brownFrame.VSCale.Y * 0.5 + 25.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y));
      this.quitOptionsPanel = new QuitOptionsPanel(employee);
      this.quitOptionsPanel.location = this.text.Location + new Vector2(0.0f, 90f);
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.mainFrame.CheckMouseOver(player, offset);
    }

    public QuitOptions UpdateQuitJobMainPanel(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out int NewPayIfApplicable)
    {
      offset += this.location;
      NewPayIfApplicable = 0;
      return this.quitOptionsPanel.UpdateQuitOptionsPanel(player, DeltaTime, offset, out NewPayIfApplicable);
    }

    public void DrawQuitJobMainPanel(Vector2 offset)
    {
      offset += this.location;
      this.mainFrame.DrawBigBrownPanel(offset);
      this.header.DrawMiniHeading(offset);
      this.employeePanel.DrawEmployeeSummary(offset, AssetContainer.pointspritebatchTop05);
      this.text.DrawSimpleTextHandler(offset, 1f, AssetContainer.pointspritebatchTop05);
      this.quitOptionsPanel.DrawQuitOptionsPanel(offset, AssetContainer.pointspritebatchTop05);
    }
  }
}
