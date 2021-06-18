// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps.SecurityActionPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps
{
  internal class SecurityActionPopUp : CustomerActionPopUp
  {
    private PersonnelAssign personnelassign;
    private UseOfForcePanel force;
    private TextButton button;
    private bool locked;
    private float moralityrating;

    public SecurityActionPopUp(float basescale_, float moralityrating_)
      : base(basescale_)
    {
      this.locked = false;
      this.moralityrating = moralityrating_;
      this.personnelassign = new PersonnelAssign(this.basescale);
      this.force = new UseOfForcePanel(this.moralityrating, this.basescale);
      this.button = new TextButton(this.basescale, "Deploy", 40f);
      this.framescale = this.framescale + this.force.GetSize();
      this.framescale.X = Math.Max(this.framescale.X, this.personnelassign.GetSize().X);
      this.framescale.Y += this.personnelassign.GetSize().Y + this.pad.Y;
      this.framescale.Y += this.button.GetSize_True().Y + this.pad.Y;
      this.SizeFrame();
      Vector2 vector2 = -0.5f * this.framescale;
      this.personnelassign.location = vector2;
      this.personnelassign.location.X = 0.0f;
      this.personnelassign.location.Y += 0.5f * this.personnelassign.GetSize().Y;
      vector2.Y += this.personnelassign.GetSize().Y + this.pad.Y;
      this.force.location = vector2;
      this.force.location.X = 0.0f;
      this.force.location.Y += 0.5f * this.force.GetSize().Y;
      vector2.Y += this.force.GetSize().Y + this.pad.Y;
      this.button.vLocation = vector2;
      this.button.vLocation.X = 0.0f;
      this.button.vLocation.Y += 0.5f * this.button.GetSize_True().Y;
      vector2.Y += this.button.GetSize_True().Y;
    }

    public override bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false;
      this.personnelassign.UpdatePersonnelAssign(player, offset, DeltaTime);
      this.force.UpdateUseOfForcePanel(player, offset, DeltaTime);
      int selected = this.force.Selected;
      if (selected < 0)
      {
        this.locked = true;
        this.button.SetButtonColour(BTNColour.Grey);
        this.button.Locked = true;
      }
      else if (selected > 2 && (double) this.moralityrating > -10.0)
      {
        this.locked = true;
        this.button.SetButtonColour(BTNColour.Grey);
        this.button.Locked = true;
      }
      else if (selected < 1 && (double) this.moralityrating < 10.0)
      {
        this.locked = true;
        this.button.SetButtonColour(BTNColour.Grey);
        this.button.Locked = true;
      }
      else
      {
        this.locked = false;
        this.button.SetButtonColour(BTNColour.Green);
        this.button.Locked = false;
      }
      if (!this.locked)
        flag |= this.button.UpdateTextButton(player, offset, DeltaTime);
      return flag;
    }

    public override void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.personnelassign.DrawPersonnelAssign(spritebatch, offset);
      this.button.DrawTextButton(offset, 1f, spritebatch);
      this.force.DrawUseOfForcePanel(spritebatch, offset);
    }
  }
}
