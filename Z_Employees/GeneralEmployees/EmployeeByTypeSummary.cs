// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.GeneralEmployees.EmployeeByTypeSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.Z_Employees.GeneralEmployees.EM_Bar;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.ManageEmployeeMain.HiringSummary;
using TinyZoo.Z_SummaryPopUps.People;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_WorldMap.Maps.MapLocations.MapMarkers;

namespace TinyZoo.Z_Employees.GeneralEmployees
{
  internal class EmployeeByTypeSummary
  {
    private MiniHeading StaffName;
    private List<Employee> theseemployees;
    private CustomerFrame customerFrame;
    private MiniHeading minihaeading;
    public Vector2 Location;
    private LittleSummaryButton_AndHeading littlesummarybutton;
    public AnimalInFrame animalinframe;
    private TotalEmployeesCol totalemployees;
    private TotalEmployeesCol TotalSalary;
    private SimpleTextHandler simpletext;
    public EmployeeType employeetype;
    private RedLight redLight;
    private SpinningInProgressIcon spinningIcon;
    private Vector2 lightLoc;
    private float BaseScale;
    private UIScaleHelper scaleHelper;

    public EmployeeByTypeSummary(
      EmployeeType _employeetype,
      Player player,
      float _BaseScale,
      float UnmultipliedWidth)
    {
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      Vector2 defaultBuffer = this.scaleHelper.DefaultBuffer;
      this.employeetype = _employeetype;
      this.theseemployees = player.employees.GetEmployeesOfThisType(this.employeetype);
      float y = this.scaleHelper.ScaleY(75f);
      this.customerFrame = new CustomerFrame(new Vector2(UnmultipliedWidth * this.BaseScale, y));
      AnimalType animal = Employees.GetEmployee(this.employeetype, out bool _);
      if (this.theseemployees.Count > 0)
        animal = this.theseemployees[this.theseemployees.Count - 1].intakeperson == null ? this.theseemployees[this.theseemployees.Count - 1].quickemplyeedescription.thisemployee : this.theseemployees[this.theseemployees.Count - 1].intakeperson.animaltype;
      this.animalinframe = new AnimalInFrame(animal, AnimalType.None, TargetSize: (50f * this.BaseScale), FrameEdgeBuffer: (4f * this.BaseScale), BaseScale: this.BaseScale);
      if (this.theseemployees.Count == 0)
        this.animalinframe.SetAnimalGreyedOut(true);
      float num1 = defaultBuffer.X + this.scaleHelper.ScaleX(UnmultipliedWidth) * -0.5f;
      this.animalinframe.Location.X += this.animalinframe.GetSize().X * 0.5f;
      this.animalinframe.Location.X += num1;
      float num2 = num1 + this.animalinframe.GetSize().X;
      string employeeThypeToString = Employees.GetEmployeeThypeToString(this.employeetype);
      this.StaffName = new MiniHeading(this.customerFrame.VSCale - new Vector2((float) (((double) this.animalinframe.GetSize().X + 10.0 * (double) this.BaseScale) * 2.0), 0.0f), employeeThypeToString, 1f, this.BaseScale);
      float x1 = num2 + defaultBuffer.X;
      float width_ = this.scaleHelper.ScaleX(135f);
      this.simpletext = new SimpleTextHandler(Employees.GetEmployeeTypeDescription(this.employeetype), width_, _Scale: this.BaseScale);
      this.simpletext.AutoCompleteParagraph();
      this.simpletext.SetAllColours(ColourData.Z_Cream);
      this.simpletext.Location = new Vector2(x1, this.animalinframe.GetSize().Y * -0.5f);
      this.simpletext.Location.Y += this.BaseScale * 14f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      float x2 = x1 + width_;
      this.lightLoc = new Vector2(x2, defaultBuffer.Y - this.customerFrame.VSCale.Y * 0.5f);
      float num3 = x2 + defaultBuffer.X;
      this.totalemployees = new TotalEmployeesCol(this.customerFrame.VSCale, this.BaseScale, this.theseemployees, false);
      this.totalemployees.Location.X = num3;
      this.totalemployees.Location.X += this.totalemployees.GetSize().X * 0.5f;
      float num4 = num3 + this.totalemployees.GetSize().X + this.scaleHelper.ScaleX(10f);
      this.TotalSalary = new TotalEmployeesCol(this.customerFrame.VSCale, this.BaseScale, this.theseemployees, true);
      this.TotalSalary.Location.X = num4;
      this.TotalSalary.Location.X += this.TotalSalary.GetSize().X * 0.5f;
      float num5 = num4 + this.TotalSalary.GetSize().X;
      float num6 = this.scaleHelper.ScaleX(UnmultipliedWidth) - (num5 + this.scaleHelper.ScaleX(UnmultipliedWidth) * 0.5f);
      float num7 = num5 + num6 * 0.5f;
      LittleSummaryButtonType summarytype = LittleSummaryButtonType.Locate;
      if (this.theseemployees.Count == 0)
        summarytype = LittleSummaryButtonType.Hire;
      this.littlesummarybutton = new LittleSummaryButton_AndHeading(summarytype, this.BaseScale);
      this.littlesummarybutton.SetCustomHeading("View/Hire");
      this.littlesummarybutton.Location.X = num7;
      this.CheckAndDoLock();
      this.CheckToAddLight(player);
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    private void CheckAndDoLock()
    {
      if (!Z_DebugFlags.IsBetaVersion)
        return;
      switch (this.employeetype)
      {
        case EmployeeType.Janitor:
          break;
        case EmployeeType.Keeper:
          break;
        case EmployeeType.Architect:
          break;
        case EmployeeType.ShopKeeper:
          break;
        case EmployeeType.DNAResearcher:
          break;
        default:
          this.customerFrame.LockForBeta();
          break;
      }
    }

    private void CheckToAddLight(Player player)
    {
      OpenPositions positionForThisEmployee = player.employees.openPositionsContainer.GetOpenPositionForThisEmployee(this.employeetype);
      if (FeatureFlags.FlashHireFromGateForQuest && this.employeetype == EmployeeType.Janitor)
        this.AddLight();
      if (positionForThisEmployee == null)
        return;
      if (positionForThisEmployee.GetNumberOfApplicants() > 0)
      {
        this.AddLight();
      }
      else
      {
        if (positionForThisEmployee.NumberOfPositionsOpened <= 0)
          return;
        this.AddSpinningIcon();
      }
    }

    private void AddLight()
    {
      this.redLight = new RedLight(true, true, this.BaseScale);
      this.redLight.vLocation = this.lightLoc;
      this.redLight.vLocation.X -= this.redLight.GetSize().X * 0.5f;
      this.redLight.vLocation.Y += this.redLight.GetSize().Y * 0.5f;
    }

    private void AddSpinningIcon()
    {
      this.spinningIcon = new SpinningInProgressIcon(this.BaseScale);
      if (this.redLight != null)
      {
        this.spinningIcon.vLocation = this.redLight.vLocation;
        this.spinningIcon.vLocation.Y += this.redLight.GetSize().Y;
        this.spinningIcon.vLocation.Y += this.spinningIcon.GetSize().Y * 0.5f;
      }
      else
      {
        this.spinningIcon.vLocation = this.lightLoc;
        this.spinningIcon.vLocation.Y += this.spinningIcon.GetSize().Y * 0.5f;
        this.spinningIcon.vLocation.X -= this.spinningIcon.GetSize().X * 0.5f;
      }
    }

    public bool UpdateEmployeeByTypeSummary(Player player, float DeltaTime, Vector2 Offset)
    {
      if (!this.customerFrame.Active)
        return false;
      Offset.X += this.Location.X;
      Offset.Y += this.Location.Y;
      if (this.redLight != null)
        this.redLight.UpdateColours(DeltaTime);
      if (this.spinningIcon != null)
        this.spinningIcon.UpdateSpinningInProgressIcon(DeltaTime);
      return this.littlesummarybutton.UpdateLittleSummaryButton_AndHeading(DeltaTime, player, Offset);
    }

    public void DrawEmployeeByTypeSummary(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset.X += this.Location.X;
      Offset.Y += this.Location.Y;
      this.customerFrame.DrawCustomerFrame(Offset, spritebatch);
      this.animalinframe.DrawAnimalInFrame(Offset, spritebatch);
      this.StaffName.DrawMiniHeading(Offset);
      this.littlesummarybutton.DrawLittleSummaryButton_AndHeading(Offset, spritebatch);
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      this.totalemployees.DrawTotalEmployeesCol(Offset, spritebatch);
      this.TotalSalary.DrawTotalEmployeesCol(Offset, spritebatch);
      if (this.redLight != null)
        this.redLight.DrawRedLight(spritebatch, spritebatch, Offset, true);
      if (this.spinningIcon != null)
        this.spinningIcon.DrawSpinningInProgressIcon(Offset, spritebatch);
      this.customerFrame.DrawDarkOverlay(Offset, spritebatch);
    }
  }
}
