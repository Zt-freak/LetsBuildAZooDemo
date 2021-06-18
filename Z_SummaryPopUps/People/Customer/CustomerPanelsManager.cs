// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerPanelsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer.Hapenis;
using TinyZoo.Z_SummaryPopUps.People.Customer.PersonSatisfaction;
using TinyZoo.Z_SummaryPopUps.People.Customer.PurchaseHistory;
using TinyZoo.Z_SummaryPopUps.People.Customer.Thoughts;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class CustomerPanelsManager
  {
    private Vector2 Location;
    private HapenisManager happinessmanager;
    private ThoughtsManager thoughts;
    private PurchasedStuff purchasedstuff;
    private PersonAndSatisfaction personandjoy;
    private UIScaleHelper uiscale;
    private string Name;
    private Vector2 framescale;
    private SimPerson refSimperson;
    private WalkingPerson Ref_WalkingPerson;

    public CustomerPanelsManager(
      SimPerson simperson,
      WalkingPerson person,
      Vector2 MasterVScale,
      float basescale)
    {
      this.Ref_WalkingPerson = person;
      this.refSimperson = simperson;
      this.uiscale = new UIScaleHelper(basescale);
      Vector2 vector2_1 = new Vector2(this.uiscale.GetDefaultXBuffer(), this.uiscale.GetDefaultYBuffer());
      this.Name = person.simperson.memberofthepublic.Name;
      this.personandjoy = new PersonAndSatisfaction(person, basescale, MasterVScale, false, simperson.GetName());
      this.happinessmanager = new HapenisManager(simperson, basescale, MasterVScale);
      this.framescale = new Vector2();
      this.framescale.X = this.personandjoy.GetSize().X + this.happinessmanager.GetSize().X + this.uiscale.DefaultBuffer.X;
      this.thoughts = new ThoughtsManager(simperson, this.framescale.X, basescale);
      this.purchasedstuff = new PurchasedStuff(simperson.memberofthepublic, basescale, this.framescale.X);
      this.framescale.Y = (float) ((double) this.personandjoy.GetSize().Y + (double) this.thoughts.GetSize().Y + (double) this.purchasedstuff.GetSize().Y + 2.0 * (double) this.uiscale.DefaultBuffer.Y);
      Vector2 vector2_2 = -0.5f * this.framescale;
      this.personandjoy.Location = vector2_2 + 0.5f * this.personandjoy.GetSize();
      this.happinessmanager.Location = vector2_2;
      this.happinessmanager.Location.X += this.personandjoy.GetSize().X + vector2_1.X;
      this.happinessmanager.Location += 0.5f * this.happinessmanager.GetSize();
      vector2_2.X = 0.0f;
      vector2_2.Y += this.personandjoy.GetSize().Y + vector2_1.Y;
      this.thoughts.Location = vector2_2;
      this.thoughts.Location.Y += 0.5f * this.thoughts.GetSize().Y;
      vector2_2.Y += this.thoughts.GetSize().Y + vector2_1.Y;
      this.purchasedstuff.Location = vector2_2;
      this.purchasedstuff.Location.Y += 0.5f * this.purchasedstuff.GetSize().Y;
      vector2_2.Y += this.purchasedstuff.GetSize().Y + vector2_1.Y;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateCustomerPanelsManager(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.Location;
      this.personandjoy.UpdatePersonAndSatisfaction(offset, player);
      this.happinessmanager.UpdateHapenisManager(this.refSimperson, this.Ref_WalkingPerson);
      return false;
    }

    public void DrawCustomerPanelsManager(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.Location;
      this.happinessmanager.DrawHapenisManager(AssetContainer.pointspritebatchTop05, offset);
      this.thoughts.DrawThoughtsManager(AssetContainer.pointspritebatchTop05, offset);
      this.purchasedstuff.DrawPurchasedStuff(AssetContainer.pointspritebatchTop05, offset);
      this.personandjoy.DrawPersonAndSatisfaction(AssetContainer.pointspritebatchTop05, offset);
    }
  }
}
