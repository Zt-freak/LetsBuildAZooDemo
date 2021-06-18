// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.Negotiation.NegotiationManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine.Buttons;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees;
using TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer;
using TinyZoo.Z_Manage.Hiring.Interview.Negotiation.OffserRejectOrAccept;
using TinyZoo.Z_Manage.Hiring.Interview.Negotiation.PickThisPerson;
using TinyZoo.Z_Manage.Hiring.PossibleHires;

namespace TinyZoo.Z_Manage.Hiring.Negotiation
{
  internal class NegotiationManager
  {
    private PickThisOrNot makeoffer;
    private MakeOfferManager makeoffermanager;
    private PotentialHire REF_hirethisguy;
    private OfferAcceptOrRejectManager acceptorreject;
    private int OffersMade;

    public NegotiationManager(PotentialHire hirethisguy)
    {
      this.makeoffer = new PickThisOrNot();
      this.REF_hirethisguy = hirethisguy;
    }

    public bool UpdateNegotiationManager(Player player, float DeltaTime, ref bool newPersonGotJob)
    {
      bool WasYes;
      if (this.makeoffer != null && this.makeoffer.UpdatemakePickThisOrNot(DeltaTime, player, out WasYes))
      {
        if (WasYes)
        {
          this.makeoffer = (PickThisOrNot) null;
          this.makeoffermanager = new MakeOfferManager(this.REF_hirethisguy);
        }
        else
        {
          player.employees.potentialhires.SetInterviewStatus(this.REF_hirethisguy, HireResult.YouDintPickThem);
          return true;
        }
      }
      if (this.acceptorreject != null && this.acceptorreject.UpdateOfferAcceptOrRejectManager(DeltaTime, player))
      {
        if (this.acceptorreject.GotJob)
        {
          EmployeeType employeetype;
          EmployeeData.IsThisAnEmployee(this.REF_hirethisguy.intakeperson.animaltype, out employeetype);
          player.employees.AddThisEmplyee(this.REF_hirethisguy.intakeperson, employeetype, this.makeoffermanager.GetOfferValue(), this.REF_hirethisguy.employeestats.Determination, player);
          player.worldhistory.EmployedSomeone(this.REF_hirethisguy.intakeperson.animaltype);
          WalkingPerson NewEmployee = CustomerManager.AddPerson(this.REF_hirethisguy.intakeperson.animaltype, player.employees.employees[player.employees.employees.Count - 1], player: player);
          NewEmployee.ForceRotationAndHold(DirectionPressed.Down, 2f);
          ParkGate.NewEmployeeWantsToGoThoughGate(NewEmployee);
          newPersonGotJob = true;
          player.employees.potentialhires.SetInterviewStatus(this.REF_hirethisguy, HireResult.TheyTookTheJob);
          PossibleHireManager.ForceExitAllTheWay = true;
          return true;
        }
        if (this.OffersMade < 2)
        {
          this.acceptorreject = (OfferAcceptOrRejectManager) null;
        }
        else
        {
          player.employees.potentialhires.SetInterviewStatus(this.REF_hirethisguy, HireResult.TheyDidntACceptYourOffer);
          return true;
        }
      }
      if (this.makeoffermanager != null && this.acceptorreject == null && this.makeoffermanager.UpdateMakeOfferManager(player, DeltaTime))
      {
        this.acceptorreject = new OfferAcceptOrRejectManager(this.makeoffermanager.GetOfferValue(), this.REF_hirethisguy, this.OffersMade == 0);
        ++this.OffersMade;
      }
      return false;
    }

    public void DrawNegotiationManager(Vector2 Offset)
    {
      if (this.makeoffer != null)
        this.makeoffer.DrawPickThisOrNot(Offset);
      if (this.makeoffermanager != null && this.acceptorreject == null)
        this.makeoffermanager.DrawMakeOffserManager(Offset);
      if (this.acceptorreject == null)
        return;
      this.acceptorreject.DrawOfferAcceptOrRejectManager(Offset);
    }
  }
}
