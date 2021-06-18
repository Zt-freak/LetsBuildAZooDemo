// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.Hapenis.HapenisManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.Hapenis
{
  internal class HapenisManager
  {
    public Vector2 Location;
    private ZGenericText HappinessHeader;
    private SimpleTextHandler AttractionsHeader;
    private CustomerCurrentAction currentAction;
    private CustomerFrame customerframe;
    private StarBar starbar;
    private float basescale;
    private UIScaleHelper uiscale;
    private Vector2 framescale;

    public HapenisManager(SimPerson simperson, float MasterScaleMult, Vector2 MasterVScale)
    {
      this.basescale = MasterScaleMult;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.framescale = this.uiscale.ScaleVector2(new Vector2(150f, 160f));
      double randomFloat = (double) MathStuff.getRandomFloat(0.0f, 5f);
      this.customerframe = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));
      this.starbar = new StarBar(simperson.memberofthepublic.customerneeds.CurrentWantValues[9] * 0.01f, this.basescale, true);
      this.HappinessHeader = new ZGenericText("Happiness", this.basescale, false, _UseOnePointFiveFont: true);
      this.AttractionsHeader = new SimpleTextHandler("Favourite Attractions: ", this.framescale.X - defaultBuffer.X, _Scale: this.basescale, AutoComplete: true);
      this.AttractionsHeader.SetAllColours(ColourData.Z_Cream);
      this.currentAction = new CustomerCurrentAction(this.basescale);
      Vector2 vector2 = -0.5f * this.customerframe.VSCale + defaultBuffer;
      this.HappinessHeader.vLocation = vector2;
      vector2.Y += this.HappinessHeader.GetSize().Y;
      this.starbar.Location.X = 0.0f;
      this.starbar.Location.Y = vector2.Y + 0.5f * this.starbar.GetSize().Y;
      vector2.Y += this.starbar.GetSize().Y + defaultBuffer.Y;
      this.AttractionsHeader.Location = vector2;
      vector2.Y += this.AttractionsHeader.GetHeightOfParagraph();
      vector2.Y += this.uiscale.ScaleY(45f);
      this.currentAction.location = vector2;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void UpdateHapenisManager(SimPerson simperson, WalkingPerson walkingperson) => this.currentAction.UpdateCustomerCurrentAction(simperson, walkingperson);

    public void DrawHapenisManager(SpriteBatch spritebatch, Vector2 Offset)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.HappinessHeader.DrawZGenericText(Offset, spritebatch);
      this.starbar.DrawStarBar(Offset, spritebatch);
      this.AttractionsHeader.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      this.currentAction.DrawCustomerCurrentAction(Offset, spritebatch);
    }
  }
}
