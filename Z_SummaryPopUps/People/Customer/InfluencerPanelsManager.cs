// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.InfluencerPanelsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class InfluencerPanelsManager
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private Vector2 framescale;
    private VIPProfile profile;
    private CustomerActionList actions;
    private InfluencerInfo infobox;

    public InfluencerPanelsManager(SimPerson simperson, WalkingPerson person, float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.profile = new VIPProfile(person, person.simperson.memberofthepublic.Name, this.basescale);
      this.infobox = new InfluencerInfo(person, this.basescale);
      this.actions = new CustomerActionList(this.basescale);
      this.actions.AddAction(CustomerActionType.Bribe);
      this.actions.AddAction(CustomerActionType.AnimalEncounter);
      this.actions.AddAction(CustomerActionType.Security);
      this.framescale = new Vector2();
      this.framescale.X += this.profile.GetSize().X + defaultBuffer.X + this.actions.GetSize().X;
      this.framescale.Y = Math.Max(this.actions.GetSize().Y, this.profile.GetSize().Y + this.infobox.GetSize().Y + defaultBuffer.Y);
      this.actions.ForceToHeight(this.framescale.Y);
      Vector2 vector2 = -0.5f * this.framescale;
      this.profile.location = vector2 + 0.5f * this.profile.GetSize();
      vector2.X += this.profile.GetSize().X + defaultBuffer.X;
      this.actions.location = vector2 + 0.5f * this.actions.GetSize();
      vector2.Y += this.profile.GetSize().Y + defaultBuffer.Y;
      this.infobox.location.X = (float) (-0.5 * (double) this.framescale.X + 0.5 * (double) this.infobox.GetSize().X);
      this.infobox.location.Y = vector2.Y + 0.5f * this.infobox.GetSize().Y;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateInfluencerPanelsManager(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out CustomerActionType OUTActionType)
    {
      offset += this.location;
      OUTActionType = CustomerActionType.None;
      return (0 | (this.actions.UpdateCustomerActionsList(player, offset, DeltaTime, out OUTActionType) ? 1 : 0)) != 0;
    }

    public void DrawInfluencerPanelsManager(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.profile.DrawVIPProfile(spritebatch, offset);
      this.actions.DrawCustomerActionsList(spritebatch, offset);
      this.infobox.DrawInfluencerInfo(spritebatch, offset);
    }
  }
}
