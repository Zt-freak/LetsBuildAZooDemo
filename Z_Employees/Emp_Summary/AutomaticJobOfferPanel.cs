// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.Emp_Summary.AutomaticJobOfferPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Z_BalanceSystems.Employees.JobApplicants;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_Employees.Emp_Summary
{
  internal class AutomaticJobOfferPanel
  {
    public Vector2 location;
    private BigBrownPanel BigFrame;
    private SimpleTextHandler text;
    private QuickEmployeeDescription Employee;
    private AdjustSalaryPopUp adjustsalary;
    private LerpHandler_Float lerper;
    private SimpleTextHandler extraAgencyText;
    private int agencyFee;

    public AutomaticJobOfferPanel(
      QuickEmployeeDescription newemployee,
      float BaseScale,
      Player player,
      bool AllowClose = false,
      bool IsAgencyHire = false)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.Employee = newemployee;
      this.agencyFee = -1;
      EmployeeType employeetype;
      EmployeeData.IsThisAnEmployee(newemployee.thisemployee, out employeetype);
      string jobTitle = EmployeesStats.GetJobTitle(employeetype, newemployee.thisemployee, (int) newemployee.seniorityLevel, false);
      this.adjustsalary = new AdjustSalaryPopUp(newemployee, BaseScale);
      this.BigFrame = new BigBrownPanel(Vector2.Zero, AllowClose, "Hire", BaseScale);
      float x = this.adjustsalary.GetSize().X;
      this.text = new SimpleTextHandler("Choose a salary for your new " + jobTitle + ".", x, true, BaseScale);
      this.text.SetAllColours(ColourData.Z_Cream);
      this.text.AutoCompleteParagraph();
      if (IsAgencyHire)
      {
        this.agencyFee = HiringAgency.GetAgencyFeeToHireThisPerson(employeetype);
        this.extraAgencyText = new SimpleTextHandler("Agency Fee: $" + (object) this.agencyFee, x, true, BaseScale, true, true);
        this.extraAgencyText.SetAllColours(ColourData.Z_Cream);
      }
      Vector2 vector2_1 = new Vector2();
      Vector2 size = this.adjustsalary.GetSize();
      size.Y += defaultBuffer.Y + this.text.GetSize().Y;
      if (this.extraAgencyText != null)
        size.Y += defaultBuffer.Y + this.extraAgencyText.GetSize().Y;
      this.BigFrame.Finalize(size);
      Vector2 vector2_2 = -0.5f * size;
      this.adjustsalary.location = vector2_2 + 0.5f * this.adjustsalary.GetSize();
      vector2_2.Y += defaultBuffer.Y + this.adjustsalary.GetSize().Y;
      this.text.Location = vector2_2;
      this.text.Location.X += 0.5f * this.text.GetSize().X;
      this.text.Location.Y += 0.5f * this.text.GetHeightOfOneLine();
      vector2_2.Y += defaultBuffer.Y + this.text.GetSize().Y;
      if (this.extraAgencyText != null)
      {
        this.extraAgencyText.Location = vector2_2;
        this.extraAgencyText.Location.X += 0.5f * this.extraAgencyText.GetSize().X;
        this.extraAgencyText.Location.Y += 0.5f * this.extraAgencyText.GetHeightOfOneLine();
        vector2_2.Y += defaultBuffer.Y + this.extraAgencyText.GetSize().Y;
      }
      this.lerper = new LerpHandler_Float();
      this.LerpIn();
    }

    public void LerpIn()
    {
      if (ManageEmployeeManager.DisableLerp)
        this.lerper.Value = 0.0f;
      else
        this.lerper.SetLerp(true, 1f, 0.0f, 3f);
    }

    public void LerpOff()
    {
      if (ManageEmployeeManager.DisableLerp)
        this.lerper.Value = 1f;
      else
        this.lerper.SetLerp(false, 0.0f, 1f, 3f);
    }

    public bool IsOffScreen() => (double) this.lerper.Value == 1.0;

    public int GetSalarySet() => (int) this.adjustsalary.CurrentSalary;

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      offset.X += this.lerper.Value * ManageEmployeeManager.LerpDistance;
      return this.BigFrame.CheckMouseOver(player, offset);
    }

    public bool UpdateAutomaticJobOfferPanel(
      float DeltaTime,
      Player player,
      Vector2 offset,
      out bool isCancel)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      isCancel = false;
      offset += this.location;
      offset.X += this.lerper.Value * ManageEmployeeManager.LerpDistance;
      if ((double) this.lerper.Value == 0.0)
      {
        Vector2 localposition = offset;
        this.BigFrame.UpdateDragger(player, ref localposition, DeltaTime);
        this.location = localposition - offset + this.location;
        this.text.UpdateSimpleTextHandler(DeltaTime);
        if (this.adjustsalary.UpdateCustomerActionPopUp(player, offset, DeltaTime))
        {
          this.Employee.CurrentSalary = (int) this.adjustsalary.CurrentSalary;
          return true;
        }
        if (this.BigFrame.UpdatePanelCloseButton(player, DeltaTime, offset))
        {
          isCancel = true;
          return true;
        }
      }
      return false;
    }

    public void DrawAutomaticJobOfferPanel(Vector2 offset) => this.DrawAutomaticJobOfferPanel(offset, AssetContainer.pointspritebatchTop05);

    public void DrawAutomaticJobOfferPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      if ((double) this.lerper.Value == 1.0)
        return;
      offset += this.location;
      offset.X += this.lerper.Value * ManageEmployeeManager.LerpDistance;
      this.BigFrame.DrawBigBrownPanel(offset, spriteBatch);
      this.text.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.adjustsalary.DrawCustomerActionPopUp(spriteBatch, offset);
      if (this.extraAgencyText == null)
        return;
      this.extraAgencyText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
