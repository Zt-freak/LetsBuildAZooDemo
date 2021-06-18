// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.AnimalEncounterActionOptions
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_Employees.WorkZonePane;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class AnimalEncounterActionOptions
  {
    private Vector2 framescale;
    private float basescale;
    private UIScaleHelper uiscale;
    private Vector2 pad;
    private CustomerFrame frame;
    public Vector2 location;
    private PortraitRow portraitrow;
    private CheckBoxWithString checkbox;
    private TextButton button;
    private bool hascheckbox;
    private bool applyall;
    private int thisindex;
    private bool inPenPicker;

    public AnimalEncounterActionOptions(WalkingPerson thisperson, float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.pad = this.uiscale.DefaultBuffer;
      this.framescale = new Vector2();
      this.checkbox = new CheckBoxWithString("Enter entire group", false, this.basescale);
      this.checkbox.SetTextColour(ColourData.Z_Cream);
      this.button = new TextButton(this.basescale, "Pick Enclosure", 80f);
      this.portraitrow = new PortraitRow(6, this.basescale, this.uiscale.ScaleX(40f));
      List<WalkingPerson> walkingPersonList = new List<WalkingPerson>();
      if (thisperson.simperson.customertype == CustomerType.Protestor)
        walkingPersonList = CustomerManager.ProtestGroupNavigator.GetWalkingPersons();
      else
        walkingPersonList.Add(thisperson);
      this.thisindex = -1;
      for (int index = 0; index < walkingPersonList.Count; ++index)
      {
        if (walkingPersonList[index].simperson == thisperson.simperson)
          this.thisindex = index;
      }
      this.portraitrow.Add(walkingPersonList[this.thisindex].thispersontype, AnimalType.None);
      for (int index = 0; index < walkingPersonList.Count; ++index)
      {
        if (index != this.thisindex)
          this.portraitrow.Add(walkingPersonList[index].thispersontype, AnimalType.None);
      }
      if (this.portraitrow.Count > 1)
        this.hascheckbox = true;
      this.portraitrow.SetGreyedOut(0, false);
      for (int index = 1; index < this.portraitrow.Count; ++index)
        this.portraitrow.SetGreyedOut(index, true);
      this.framescale.X = this.uiscale.ScaleX((float) byte.MaxValue);
      this.framescale.Y = 2f * this.pad.Y;
      this.framescale.Y += this.portraitrow.GetSize().Y;
      if (this.hascheckbox)
        this.framescale.Y += this.checkbox.GetSize().Y + 0.5f * this.pad.Y;
      this.framescale.Y += this.button.GetSize_True().Y + this.pad.Y;
      this.frame = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));
      Vector2 vector2 = -0.5f * this.framescale + this.pad;
      this.portraitrow.location = vector2;
      this.portraitrow.location.X = 0.0f;
      this.portraitrow.location.Y += 0.5f * this.portraitrow.GetSize().Y;
      vector2.Y += this.portraitrow.GetSize().Y + 0.5f * this.pad.Y;
      if (this.hascheckbox)
      {
        this.checkbox.Location = vector2;
        this.checkbox.Location.X = (float) (0.5 * (double) this.framescale.X - 0.5 * (double) this.checkbox.GetBoxSize().X) - this.pad.X;
        this.checkbox.Location.Y += 0.5f * this.checkbox.GetBoxSize().Y;
        vector2.Y += this.checkbox.GetBoxSize().Y + 0.5f * this.pad.Y;
      }
      vector2.Y += 0.5f * this.pad.Y;
      this.button.vLocation = vector2;
      this.button.vLocation.X = 0.0f;
      this.button.vLocation.Y += 0.5f * this.button.GetSize_True().Y;
      vector2.Y += this.button.GetSize_True().Y + 0.5f * this.pad.Y;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateAnimalEncounterActionOptions(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false | this.button.UpdateTextButton(player, offset, DeltaTime);
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
      return flag;
    }

    public void DrawAnimalEncounterActionOptions(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      if (this.hascheckbox)
        this.checkbox.DrawCheckBoxWithString(offset, spritebatch);
      this.portraitrow.DrawPortraitRow(spritebatch, offset);
      this.button.DrawTextButton(offset, 1f, spritebatch);
    }

    public void SetMode(bool inPenPicker_)
    {
      this.inPenPicker = inPenPicker_;
      if (this.inPenPicker)
      {
        this.button.SetButtonColour(BTNColour.Grey);
        this.button.Locked = true;
      }
      else
      {
        this.button.SetButtonColour(BTNColour.Green);
        this.button.Locked = false;
      }
    }
  }
}
