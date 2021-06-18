// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.NegotiateActionPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalNotification;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class NegotiateActionPopUp : CustomerActionPopUp
  {
    private SliderWithValue slider;
    private LabelledCheckbox checkbox;
    private PortraitRow portraitrow;
    private List<int> bribeRequired;
    private int numbribed;
    private TextButton button;
    private ZGenericText salarychange;
    private StatsListerBox statsLister;
    private bool hascheckbox;
    private bool applyall;
    private WalkingPerson thisperson;
    private int thisindex;
    private NegotiateActionPopUp.NegoPopUpState state;
    private float timer;
    private int cycles;
    private List<int> deliberationdurations;

    public NegotiateActionPopUp(WalkingPerson walkingPerson, float basescale_)
      : base(basescale_)
    {
      this.thisperson = walkingPerson;
      this.state = NegotiateActionPopUp.NegoPopUpState.Normal;
      this.slider = new SliderWithValue(-20, 50, 200f, this.basescale, 0.2857143f, "", "%", forcePlusSymbol_: true);
      this.checkbox = new LabelledCheckbox("Offer for all on strike", false, this.basescale);
      this.button = new TextButton(this.basescale, "Commit", 40f);
      this.portraitrow = new PortraitRow(6, this.basescale, this.uiscale.ScaleX(40f));
      this.bribeRequired = new List<int>();
      this.salarychange = new ZGenericText("Offer Salary Adjustment", this.basescale, false, _UseOnePointFiveFont: true);
      this.statsLister = new StatsListerBox(this.basescale, false, useLargerFont: false);
      this.statsLister.AddOrUpdate("Previous Best Offer", 15.ToString() + "%");
      this.statsLister.AddOrUpdate("Current Demand", 25.ToString() + "%");
      this.statsLister.AddOrUpdate("Striker's Confidence", 70.ToString() + "%");
      this.statsLister.AddOrUpdate("Current Pay", "10000");
      this.statsLister.AddOrUpdate("Pay Increase", "$2000");
      List<WalkingPerson> walkingPersonList = new List<WalkingPerson>();
      if (walkingPerson.simperson.customertype == CustomerType.Protestor)
        walkingPersonList = CustomerManager.ProtestGroupNavigator.GetWalkingPersons();
      else
        walkingPersonList.Add(walkingPerson);
      this.thisindex = -1;
      for (int index = 0; index < walkingPersonList.Count; ++index)
      {
        if (walkingPersonList[index].simperson == walkingPerson.simperson)
          this.thisindex = index;
      }
      this.portraitrow.Add(walkingPersonList[this.thisindex].thispersontype, AnimalType.None);
      this.bribeRequired.Add(Game1.Rnd.Next(100));
      for (int index = 0; index < walkingPersonList.Count; ++index)
      {
        if (index != this.thisindex)
        {
          this.portraitrow.Add(walkingPersonList[index].thispersontype, AnimalType.None);
          this.bribeRequired.Add(Game1.Rnd.Next(100));
        }
      }
      if (this.portraitrow.Count > 1)
        this.hascheckbox = true;
      this.framescale = 2f * this.pad;
      this.framescale.X += Math.Max(this.slider.GetSize().X, this.portraitrow.GetSize().X);
      this.framescale.Y += this.slider.GetSize().Y;
      if (this.hascheckbox)
        this.framescale.Y += this.checkbox.GetSize().Y + 0.5f * this.pad.Y;
      this.framescale.Y += this.portraitrow.GetSize().Y + 0.5f * this.pad.Y;
      this.framescale.Y += this.button.GetSize_True().Y + 0.5f * this.pad.Y;
      this.framescale.Y += this.salarychange.GetSize().Y + 0.5f * this.pad.Y;
      this.framescale.Y += this.statsLister.GetSize().Y + 0.5f * this.pad.Y;
      this.SizeFrame();
      Vector2 vector2 = -0.5f * this.framescale + this.pad;
      this.salarychange.vLocation = vector2;
      this.salarychange.vLocation.X = 0.0f;
      this.salarychange.vLocation.X = -0.5f * this.salarychange.GetSize().X;
      vector2.Y += this.salarychange.GetSize().Y + 0.5f * this.pad.Y;
      this.statsLister.location = vector2 + 0.5f * this.statsLister.GetSize();
      this.statsLister.location.X = 0.0f;
      vector2.Y += this.statsLister.GetSize().Y + 0.5f * this.pad.Y;
      this.portraitrow.location = vector2;
      this.portraitrow.location.X = 0.0f;
      this.portraitrow.location.Y += 0.5f * this.portraitrow.GetSize().Y;
      vector2.Y += this.portraitrow.GetSize().Y + 0.5f * this.pad.Y;
      if (this.hascheckbox)
      {
        this.checkbox.location = vector2;
        this.checkbox.location.X = (float) (0.5 * (double) this.framescale.X - 0.5 * (double) this.checkbox.GetBoxSize().X) - this.pad.X;
        this.checkbox.location.Y += 0.5f * this.checkbox.GetBoxSize().Y;
        vector2.Y += this.checkbox.GetBoxSize().Y + 0.5f * this.pad.Y;
      }
      this.slider.location = new Vector2();
      this.slider.location.Y = vector2.Y + 0.5f * this.slider.GetSize().Y;
      vector2.Y += this.slider.GetSize().Y + 0.5f * this.pad.Y;
      this.button.vLocation = vector2;
      this.button.vLocation.X = 0.0f;
      this.button.vLocation.Y += 0.5f * this.button.GetSize_True().Y;
      vector2.Y += this.button.GetSize_True().Y + 0.5f * this.pad.Y;
    }

    public override bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false;
      switch (this.state)
      {
        case NegotiateActionPopUp.NegoPopUpState.Normal:
          this.slider.UpdateSliderWithValue(player, offset, DeltaTime);
          if (this.hascheckbox)
          {
            this.checkbox.UpdateLabelledCheckbox(player, offset, DeltaTime);
            if (this.applyall != !this.checkbox.IsTicked)
            {
              this.applyall = !this.checkbox.IsTicked;
              for (int index = 0; index < this.portraitrow.Count; ++index)
                this.portraitrow.SetGreyedOut(index, this.applyall && (uint) index > 0U);
            }
          }
          if (this.button.UpdateTextButton(player, offset, DeltaTime))
          {
            this.state = NegotiateActionPopUp.NegoPopUpState.Animating;
            this.timer = 1.25f;
            this.button.SetAllColours(ColourData.Z_Gray);
            this.deliberationdurations = new List<int>(this.portraitrow.Count);
            for (int index = 0; index < this.portraitrow.Count; ++index)
              this.deliberationdurations.Add(Game1.Rnd.Next(11, 21));
            break;
          }
          break;
        case NegotiateActionPopUp.NegoPopUpState.Animating:
          if ((double) this.timer > 0.2)
          {
            this.timer = 0.0f;
            ++this.cycles;
            for (int index = 0; index < this.portraitrow.Count; ++index)
              this.portraitrow.SetShowActiveIcon(index, Game1.Rnd.Next(0, 5) == 0);
          }
          if (this.cycles > 20)
          {
            this.state = NegotiateActionPopUp.NegoPopUpState.End;
            for (int index = 0; index < this.portraitrow.Count; ++index)
              this.portraitrow.SetShowActiveIcon(index, false);
          }
          this.timer += DeltaTime;
          break;
      }
      return flag;
    }

    public override void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.slider.DrawSliderWithValue(spritebatch, offset);
      if (this.hascheckbox)
        this.checkbox.DrawLabelledCheckbox(spritebatch, offset);
      this.statsLister.DrawStatsListerBox(offset, spritebatch);
      this.salarychange.DrawZGenericText(offset, spritebatch);
      this.portraitrow.DrawPortraitRow(spritebatch, offset);
      this.button.DrawTextButton(offset, 1f, spritebatch);
    }

    private enum NegoPopUpState
    {
      Normal,
      Animating,
      End,
      Count,
    }
  }
}
