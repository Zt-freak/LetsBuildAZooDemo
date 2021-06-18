// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Employee.GenericEmployeePanels
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_Employees.Emp_Summary;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions;
using TinyZoo.Z_SummaryPopUps.People.Employee.EmployeeView;

namespace TinyZoo.Z_SummaryPopUps.People.Employee
{
  internal class GenericEmployeePanels
  {
    public Vector2 location;
    private Vector2 framescale;
    private UIScaleHelper scalehelper;
    private float basescale;
    private EmployeeSummaryPanel summary;
    private EmployeeInfo info;
    private WalkingPerson walkingperson;
    private ZoningPanel zoning;
    private EmploymentPanel employmentpanel;
    private CustomerActionList actionlist;

    public GenericEmployeePanels(Player player, WalkingPerson walkingperson_, float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.walkingperson = walkingperson_;
      SimPerson simperson = this.walkingperson.simperson;
      EmployeeType employeetype = this.walkingperson.simperson.Ref_Employee.employeetype;
      this.actionlist = new CustomerActionList(this.basescale);
      this.actionlist.AddAction(CustomerActionType.AdjustSalary);
      this.actionlist.AddAction(CustomerActionType.AdjustBreaks);
      this.actionlist.AddAction(CustomerActionType.GiveBonus);
      this.actionlist.AddAction(CustomerActionType.Fire);
      this.actionlist.AddAction(CustomerActionType.MoveToGate);
      if (this.actionlist != null)
      {
        for (int index = 0; index < this.actionlist.Count; ++index)
        {
          bool IsLockedBecauseofBeta;
          bool isActive = GenericPanelsManager.IsThisActionEnabled(this.actionlist.customerActions[index].actiontype, out IsLockedBecauseofBeta);
          if (!isActive)
            this.actionlist.LockThisAction(this.actionlist.customerActions[index].actiontype, isActive, IsLockedBecauseofBeta);
        }
      }
      this.summary = new EmployeeSummaryPanel(simperson.Ref_Employee.quickemplyeedescription, false, false, this.basescale);
      this.employmentpanel = new EmploymentPanel(this.walkingperson, this.basescale, this.summary.GetSize().X);
      if (Employees.ThisEmplyeeSupportsZoning(this.walkingperson.simperson.Ref_Employee.employeetype))
      {
        this.zoning = new ZoningPanel(this.walkingperson, this.basescale);
        this.zoning.ForceToThisWidth(this.summary.GetSize().X);
      }
      switch (employeetype)
      {
        case EmployeeType.Vet:
          this.info = (EmployeeInfo) new VetInfo(this.basescale);
          break;
        case EmployeeType.ShopKeeper:
          this.info = (EmployeeInfo) new ShopkeeperInfo(this.walkingperson, this.basescale, this.summary.GetSize().X);
          break;
      }
      this.framescale = new Vector2();
      this.framescale.X += this.summary.GetSize().X;
      this.framescale.X += this.actionlist.GetSize().X + defaultBuffer.X;
      float val1 = 0.0f + this.summary.GetSize().Y + (this.employmentpanel.GetSize().Y + defaultBuffer.Y);
      if (this.info != null)
        val1 += defaultBuffer.Y + this.info.GetSize().Y;
      if (this.zoning != null)
        val1 += defaultBuffer.Y + this.zoning.GetSize().Y;
      float y = this.actionlist.GetSize().Y;
      this.framescale.Y += Math.Max(val1, y);
      Vector2 vector2 = -0.5f * this.framescale;
      this.summary.location = vector2 + 0.5f * this.summary.GetSize();
      this.actionlist.location = vector2 + 0.5f * this.actionlist.GetSize();
      this.actionlist.location.X += this.summary.GetSize().X + defaultBuffer.X;
      vector2.Y += this.summary.GetSize().Y + defaultBuffer.Y;
      this.employmentpanel.location = vector2 + 0.5f * this.employmentpanel.GetSize();
      vector2.Y += this.employmentpanel.GetSize().Y + defaultBuffer.Y;
      if (this.info != null)
      {
        this.info.location = vector2 + 0.5f * this.info.GetSize();
        vector2.Y += this.info.GetSize().Y + defaultBuffer.Y;
      }
      if (this.zoning == null)
        return;
      this.zoning.location = vector2 + 0.5f * this.zoning.GetSize();
      vector2.Y += this.zoning.GetSize().Y + defaultBuffer.Y;
    }

    public Vector2 GetSize() => this.framescale;

    public void RefreshPanels()
    {
      Vector2 location1 = this.summary.location;
      this.summary = new EmployeeSummaryPanel(this.walkingperson.simperson.Ref_Employee.quickemplyeedescription, false, false, this.basescale);
      this.summary.location = location1;
      Vector2 location2 = this.employmentpanel.location;
      this.employmentpanel = new EmploymentPanel(this.walkingperson, this.basescale, this.summary.GetSize().X);
      this.employmentpanel.location = location2;
    }

    public bool UpdateGenericEmployeePanels(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out CustomerActionType actiontype)
    {
      offset += this.location;
      this.summary.UpdateEmployeeSummary(DeltaTime, player, offset);
      bool flag = this.employmentpanel.UpdateEmploymentPanel(player, offset, DeltaTime);
      if (this.zoning != null)
        flag |= this.zoning.UpdateZoningPanel(player, offset, DeltaTime);
      actiontype = CustomerActionType.None;
      if (this.actionlist != null && this.actionlist.UpdateCustomerActionsList(player, offset, DeltaTime, out actiontype))
        flag = true;
      return flag;
    }

    public void DrawGenericEmployeePanels(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      if (this.info != null)
        this.info.DrawEmployeeInfo(spritebatch, offset);
      this.employmentpanel.DrawEmploymentPanel(spritebatch, offset);
      this.actionlist.DrawCustomerActionsList(spritebatch, offset);
      if (this.zoning != null)
        this.zoning.DrawZoningPanel(spritebatch, offset);
      this.summary.DrawEmployeeSummary(offset, spritebatch);
    }
  }
}
