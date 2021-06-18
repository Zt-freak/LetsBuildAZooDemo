// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.FireEmployeePopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_Employees;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class FireEmployeePopUp : CustomerActionPopUp
  {
    private ConfirmationDialog dialog;
    private WalkingPerson walkingperson;
    private PortraitRow currportraits;
    private PortraitRow singleportrait;
    private PortraitRow groupportrait;
    private List<WalkingPerson> walkingpersons;
    private LabelledCheckbox checkbox;
    private bool allcheckbox;

    public FireEmployeePopUp(WalkingPerson walkingperson_, float basescale_, bool allcheckbox_ = false)
      : base(basescale_)
    {
      this.walkingperson = walkingperson_;
      this.dialog = new ConfirmationDialog("Fire", "Are you sure you want to fire this employee?", this.basescale, IncludeBrownPanel: false);
      this.allcheckbox = allcheckbox_;
      this.singleportrait = new PortraitRow(8, this.basescale);
      this.singleportrait.Add(this.walkingperson.thispersontype, AnimalType.None);
      this.groupportrait = new PortraitRow(8, this.basescale);
      this.groupportrait.Add(this.walkingperson.thispersontype, AnimalType.None);
      this.groupportrait.Add(this.walkingperson.thispersontype, AnimalType.None);
      this.walkingpersons = CustomerManager.ProtestGroupNavigator.GetWalkingPersons();
      foreach (WalkingPerson walkingperson in this.walkingpersons)
      {
        if (walkingperson.UID != this.walkingperson.UID)
          this.groupportrait.Add(walkingperson.thispersontype, AnimalType.None);
      }
      if (this.allcheckbox)
        this.checkbox = new LabelledCheckbox("Fire all on strike", false, this.basescale);
      this.currportraits = this.singleportrait;
      this.framescale = new Vector2();
      this.framescale = this.framescale + 2f * this.pad;
      this.framescale.Y += this.groupportrait.GetSize().Y;
      if (this.allcheckbox)
        this.framescale.Y += this.checkbox.GetSize().Y;
      this.framescale.Y += this.dialog.GetSizeOfContentsFrame().Y;
      this.framescale.X += Math.Max(this.dialog.GetSizeOfContentsFrame().X, this.groupportrait.GetSize().X);
      this.SizeFrame();
      Vector2 vector2 = -0.5f * this.framescale + this.pad;
      this.singleportrait.location = vector2 + 0.5f * this.currportraits.GetSize();
      this.singleportrait.location.X = 0.0f;
      this.groupportrait.location = vector2 + 0.5f * this.currportraits.GetSize();
      this.groupportrait.location.X = 0.0f;
      vector2.Y += this.currportraits.GetSize().Y;
      if (this.allcheckbox)
      {
        vector2.X = 0.5f * this.framescale.X;
        this.checkbox.location = vector2;
        this.checkbox.location.X -= 0.5f * this.checkbox.GetBoxSize().X + this.pad.X;
        this.checkbox.location.Y += 0.5f * this.checkbox.GetSize().Y;
        vector2.Y += this.checkbox.GetSize().Y;
        vector2.X = -0.5f * this.framescale.X;
      }
      this.dialog.location = vector2 + 0.5f * this.dialog.GetSizeOfContentsFrame();
      this.SizeFrame();
    }

    public override bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false;
      bool confirmed = false;
      if (this.allcheckbox)
      {
        this.checkbox.UpdateLabelledCheckbox(player, offset, DeltaTime);
        this.currportraits = !this.checkbox.IsTicked ? this.singleportrait : this.groupportrait;
      }
      if (this.dialog.UpdateConfirmationDialog(player, offset, DeltaTime, out confirmed))
      {
        flag = true;
        if (confirmed)
        {
          OverWorldManager.overworldstate = OverWOrldState.FireEmployees;
          this.walkingperson.simperson.Ref_Employee.quickemplyeedescription.FireThisEmployee = true;
          OverWorldManager.firingmanager = new FiringManager(1, true);
          this.ForceCloseEverythingOnClose = true;
        }
      }
      return flag;
    }

    public override void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.dialog.DrawConfirmationDialog(spritebatch, offset);
      this.currportraits.DrawPortraitRow(spritebatch, offset);
      if (!this.allcheckbox)
        return;
      this.checkbox.DrawLabelledCheckbox(spritebatch, offset);
    }
  }
}
