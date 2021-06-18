// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.BribeActionPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_Employees.WorkZonePane;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class BribeActionPopUp : CustomerActionPopUp
  {
    private SliderWithValue slider;
    private CheckBoxWithString checkbox;
    private PortraitRow portraitrow;
    private List<int> bribeRequired;
    private int numbribed;
    private string bribeprogress;
    private Vector2 bribeprogloc;
    private TextButton button;
    private bool hascheckbox;
    private bool applyall;
    private WalkingPerson thisperson;
    private int thisindex;
    private List<WalkingPerson> walkingpersons;
    private int highestmaxbribe;
    private int maxbribe;

    public BribeActionPopUp(WalkingPerson walkingPerson, float basescale_)
      : base(basescale_)
    {
      this.thisperson = walkingPerson;
      this.checkbox = new CheckBoxWithString("Bribe entire group", false, this.basescale);
      this.checkbox.SetTextColour(ColourData.Z_Cream);
      this.button = new TextButton(this.basescale, "Commit", 40f);
      this.portraitrow = new PortraitRow(6, this.basescale, this.uiscale.ScaleX(40f), true);
      this.bribeRequired = new List<int>();
      List<WalkingPerson> walkingPersonList = new List<WalkingPerson>();
      if (walkingPerson.simperson.customertype == CustomerType.Protestor)
        walkingPersonList = CustomerManager.ProtestGroupNavigator.GetWalkingPersons();
      else
        walkingPersonList.Add(walkingPerson);
      this.thisindex = -1;
      for (int index = 0; index < walkingPersonList.Count; ++index)
      {
        if (walkingPersonList[index].UID == walkingPerson.UID)
          this.thisindex = index;
      }
      this.maxbribe = 0;
      this.highestmaxbribe = 0;
      this.portraitrow.Add(walkingPersonList[this.thisindex].thispersontype, AnimalType.None);
      int customertype1 = (int) walkingPersonList[this.thisindex].simperson.customertype;
      this.bribeRequired.Add(this.GetBribeValue(walkingPersonList[this.thisindex], out this.maxbribe));
      this.highestmaxbribe = Math.Max(this.highestmaxbribe, this.maxbribe);
      for (int index = 0; index < walkingPersonList.Count; ++index)
      {
        if (index != this.thisindex)
        {
          this.portraitrow.Add(walkingPersonList[index].thispersontype, AnimalType.None);
          int customertype2 = (int) walkingPersonList[index].simperson.customertype;
          this.bribeRequired.Add(this.GetBribeValue(walkingPersonList[this.thisindex], out this.maxbribe));
          this.highestmaxbribe = Math.Max(this.highestmaxbribe, this.maxbribe);
        }
      }
      if (this.portraitrow.Count > 1)
        this.hascheckbox = true;
      this.slider = new SliderWithValue(0, (int) ((double) this.highestmaxbribe * 1.20000004768372), 200f, this.basescale, 0.0f);
      this.UpdateBribeStatus();
      Vector2 vector2_1 = 2f * this.uiscale.ScaleVector2(AssetContainer.springFont.MeasureString(this.bribeprogress));
      this.framescale = 2f * this.pad;
      this.framescale.X += Math.Max(this.slider.GetSize().X, this.portraitrow.GetSize().X);
      this.framescale.Y += this.slider.GetSize().Y;
      if (this.hascheckbox)
        this.framescale.Y += this.checkbox.GetSize().Y + 0.5f * this.pad.Y;
      this.framescale.Y += this.portraitrow.GetSize().Y + 0.5f * this.pad.Y;
      this.framescale.Y += vector2_1.Y + 0.5f * this.pad.Y;
      this.framescale.Y += this.button.GetSize_True().Y + 0.5f * this.pad.Y;
      this.SizeFrame();
      Vector2 vector2_2 = -0.5f * this.framescale + this.pad;
      this.portraitrow.location = vector2_2;
      this.portraitrow.location.X = 0.0f;
      this.portraitrow.location.Y += 0.5f * this.portraitrow.GetSize().Y;
      vector2_2.Y += this.portraitrow.GetSize().Y + 0.5f * this.pad.Y;
      if (this.hascheckbox)
      {
        this.checkbox.Location = vector2_2;
        this.checkbox.Location.X = (float) (0.5 * (double) this.framescale.X - 0.5 * (double) this.checkbox.GetBoxSize().X) - this.pad.X;
        this.checkbox.Location.Y += 0.5f * this.checkbox.GetBoxSize().Y;
        vector2_2.Y += this.checkbox.GetBoxSize().Y + 0.5f * this.pad.Y;
      }
      this.slider.location = new Vector2();
      this.slider.location.Y = vector2_2.Y + 0.5f * this.slider.GetSize().Y;
      vector2_2.Y += this.slider.GetSize().Y + 0.5f * this.pad.Y;
      this.bribeprogloc = vector2_2;
      this.bribeprogloc.X = 0.0f;
      this.bribeprogloc.Y += 0.5f * vector2_1.Y;
      vector2_2.Y += vector2_1.Y + 0.5f * this.pad.Y;
      this.button.vLocation = vector2_2;
      this.button.vLocation.X = 0.0f;
      this.button.vLocation.Y += 0.5f * this.button.GetSize_True().Y;
      vector2_2.Y += this.button.GetSize_True().Y + 0.5f * this.pad.Y;
    }

    private void UpdateBribeStatus()
    {
      this.numbribed = 0;
      for (int index = 0; index < this.portraitrow.Count; ++index)
      {
        bool active = this.bribeRequired[index] <= this.slider.GetValue();
        this.numbribed += active ? 1 : 0;
        this.portraitrow.SetActiveIcon(index, active);
      }
      bool flag = this.bribeRequired[0] <= this.slider.GetValue();
      if (this.hascheckbox && this.checkbox.IsTicked())
        this.bribeprogress = this.numbribed.ToString() + "/" + (object) this.portraitrow.Count + " bribed";
      else
        this.bribeprogress = flag ? "Bribe success" : "Bribe fail";
    }

    public int GetBribeValue(WalkingPerson person, out int maxbribe)
    {
      switch (person.simperson.customertype)
      {
        case CustomerType.Protestor:
          if (person.simperson.memberofthepublic.BribeValue < 0)
            person.simperson.memberofthepublic.BribeValue = TinyZoo.Game1.Rnd.Next(10, 1000);
          maxbribe = person.simperson.GetTotalProtestors() * 1000;
          return person.simperson.memberofthepublic.BribeValue;
        case CustomerType.AnimalWelfareOfficer:
          return person.simperson.memberofthepublic.animalwelfarecontroller.GetBribeValue(out maxbribe);
        default:
          maxbribe = 8000;
          return TinyZoo.Game1.Rnd.Next(2000, 8000);
      }
    }

    public override bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag1 = false;
      this.slider.UpdateSliderWithValue(player, offset, DeltaTime);
      this.UpdateBribeStatus();
      if (this.hascheckbox)
      {
        this.checkbox.UpdateCheckBoxWithString(player, offset);
        if (this.applyall != !this.checkbox.IsTicked())
        {
          this.applyall = !this.checkbox.IsTicked();
          for (int index = 0; index < this.portraitrow.Count; ++index)
            this.portraitrow.SetGreyedOut(index, this.applyall && (uint) index > 0U);
        }
      }
      int cashHeld = player.Stats.GetCashHeld();
      if (this.numbribed > 0 && cashHeld >= this.slider.GetValue())
      {
        flag1 |= this.button.UpdateTextButton(player, offset, DeltaTime);
        this.button.SetButtonColour(BTNColour.Green);
      }
      else
        this.button.SetButtonColour(BTNColour.Grey);
      bool flag2 = false;
      if (flag1)
      {
        if (player.Stats.SpendCash(this.slider.GetValue(), SpendingCashOnThis.Bribe, player))
        {
          flag2 = true;
          if (this.thisperson.simperson.customertype == CustomerType.AnimalWelfareOfficer)
            this.thisperson.simperson.memberofthepublic.animalwelfarecontroller.ConfirmBribe();
        }
        this.ForceCloseEverythingOnClose = true;
      }
      return flag1 & flag2;
    }

    public override void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.slider.DrawSliderWithValue(spritebatch, offset);
      if (this.hascheckbox)
        this.checkbox.DrawCheckBoxWithString(offset, spritebatch);
      this.portraitrow.DrawPortraitRow(spritebatch, offset);
      TextFunctions.DrawJustifiedText(this.bribeprogress, 2f * this.basescale, offset + this.bribeprogloc, new Color(ColourData.Z_Cream), 1f, AssetContainer.springFont, spritebatch);
      this.button.DrawTextButton(offset, 1f, spritebatch);
    }
  }
}
