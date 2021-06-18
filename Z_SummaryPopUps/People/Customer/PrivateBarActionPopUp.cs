// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.PrivateBarActionPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class PrivateBarActionPopUp : CustomerActionPopUp
  {
    private PortraitRow portraits;
    private OnOffToggleWithLabels toggle;
    private TextButton button;
    private SimpleTextHandler text;
    private WalkingPerson thisperson;
    private int thisindex;

    public PrivateBarActionPopUp(WalkingPerson walkingPerson, float basescale_)
      : base(basescale_)
    {
      this.thisperson = walkingPerson;
      this.button = new TextButton(this.basescale, SEngine.Localization.Localization.GetText(935), 40f);
      this.toggle = new OnOffToggleWithLabels(this.basescale, SEngine.Localization.Localization.GetText(943), SEngine.Localization.Localization.GetText(944));
      this.portraits = new PortraitRow(6, this.basescale, this.uiscale.ScaleX(40f));
      List<WalkingPerson> walkingPersonList = new List<WalkingPerson>();
      switch (walkingPerson.simperson.customertype)
      {
        case CustomerType.Protestor:
          walkingPersonList = CustomerManager.ProtestGroupNavigator.GetWalkingPersons();
          break;
        case CustomerType.Biker:
          walkingPersonList = CustomerManager.ProtestGroupNavigator.GetWalkingPersons();
          break;
        default:
          walkingPersonList.Add(walkingPerson);
          break;
      }
      this.thisindex = -1;
      for (int index = 0; index < walkingPersonList.Count; ++index)
      {
        if (walkingPersonList[index].simperson == walkingPerson.simperson)
          this.thisindex = index;
      }
      this.portraits.Add(walkingPersonList[this.thisindex].thispersontype, AnimalType.None);
      for (int index = 0; index < walkingPersonList.Count; ++index)
      {
        if (index != this.thisindex)
          this.portraits.Add(walkingPersonList[index].thispersontype, AnimalType.None);
      }
      this.framescale = new Vector2();
      this.framescale.X = Math.Max(this.portraits.GetSize().X, this.toggle.GetSize().X);
      this.framescale.X = Math.Max(this.framescale.X, this.uiscale.ScaleX(220f));
      this.framescale = this.framescale + 2f * this.pad;
      this.framescale.Y += this.portraits.GetSize().Y;
      this.framescale.Y += this.pad.Y + this.toggle.GetSize().Y;
      this.framescale.Y += this.pad.Y + this.button.GetSize_True().Y;
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      Vector2 vector2 = -0.5f * this.framescale + this.pad;
      this.portraits.location = vector2 + 0.5f * this.portraits.GetSize();
      this.portraits.location.X = 0.0f;
      vector2.Y += this.portraits.GetSize().Y + this.pad.Y;
      this.toggle.location = vector2 + 0.5f * this.toggle.GetSize();
      this.toggle.location.X = 0.0f;
      vector2.Y += this.toggle.GetSize().Y + this.pad.Y;
      this.button.vLocation = vector2 + 0.5f * this.button.GetSize();
      this.button.vLocation.X = 0.0f;
      vector2.Y += this.toggle.GetSize().Y + this.pad.Y;
    }

    public override bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      this.portraits.UpdatePortraitRow(player, offset, DeltaTime);
      this.toggle.UpdateOnOffToggleWithLabels(player, offset, DeltaTime);
      return (0 | (this.button.UpdateTextButton(player, offset, DeltaTime) ? 1 : 0)) != 0;
    }

    public override void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.portraits.DrawPortraitRow(spritebatch, offset);
      this.toggle.DrawOnOffToggleWithLabels(spritebatch, offset);
      this.button.DrawTextButton(offset, 1f, spritebatch);
    }
  }
}
