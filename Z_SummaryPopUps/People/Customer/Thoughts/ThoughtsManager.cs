// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.Thoughts.ThoughtsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.Thoughts
{
  internal class ThoughtsManager
  {
    public CustomerFrame customerframe;
    public Vector2 Location;
    private SimpleTextHandler text;
    private MiniHeading miniheading;
    private float basescale;
    private UIScaleHelper uiscale;
    private Vector2 framescale;

    public ThoughtsManager(SimPerson simperson, float ForcedWidth, float _basescale)
    {
      this.basescale = _basescale;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.miniheading = new MiniHeading(Vector2.Zero, "Thoughts:", 1f, this.basescale);
      string text = this.GetText(simperson);
      float num = 0.0f + (this.miniheading.GetTextHeight(true) + this.uiscale.GetDefaultYBuffer()) + this.uiscale.GetDefaultYBuffer();
      this.text = new SimpleTextHandler(text, ForcedWidth - this.uiscale.DefaultBuffer.X, _Scale: this.basescale, AutoComplete: true);
      this.text.paragraph.linemaker.SetAllColours(ColourData.Z_Cream);
      this.text.AutoCompleteParagraph();
      this.text.Location = new Vector2();
      this.text.Location.X = this.uiscale.GetDefaultXBuffer();
      this.text.Location.Y = num;
      float y = num + this.text.GetHeightOfParagraph() + this.uiscale.GetDefaultYBuffer();
      this.framescale = new Vector2(ForcedWidth, y);
      this.customerframe = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));
      this.text.Location += -this.customerframe.VSCale * 0.5f;
      if (simperson.memberofthepublic.ThisCustomerDecidedNotToPay || simperson.memberofthepublic.LeftParkEarly)
        this.customerframe.SetAlertRed();
      this.miniheading.SetTextPosition(this.customerframe.VSCale);
    }

    private string GetText(SimPerson simperson)
    {
      Game1.Rnd.Next(0, 5);
      if (simperson.memberofthepublic.ThisCustomerDecidedNotToPay)
      {
        switch (Game1.Rnd.Next(0, 4))
        {
          case 0:
            return "I thought the tickets would be cheaper, I am not paying that much to enter a zoo!";
          case 1:
            return "How much money do these people think I have? I can't afford to spend so much for a ticket to a zoo, I think I will just stare at neighborhood cats instead.";
          case 2:
            return "I don't care how good people say this zoo is, it's not worth the price of admission. Do I look like I am made of money?";
          case 3:
            return "I should get into the zoo business if they can charge that much for tickets! There is no way I am going in.";
        }
      }
      else if (simperson.memberofthepublic.LeftParkEarly)
      {
        switch (simperson.memberofthepublic.LeftTheParkBecauseOfThis)
        {
          case ParkLeavingReason.NoIcecreamForChilli:
            return "That taco was so spicy, I would do anything for some ice cream! I couldn't find any, so I think I need to go home and recover!";
          case ParkLeavingReason.NoToilets:
            return "Where are the bathrooms in this place?";
          case ParkLeavingReason.NoFood:
            return "I am starving, who would have a zoo without enough food options for everyone?";
          case ParkLeavingReason.NoDrinks:
            return "I am so thirsty! I can't stay here if I can't find anywhere to buy a drink";
          case ParkLeavingReason.NoBenches:
            return "I am so tired, I wish there were more places to sit down and get some enegy back!";
          case ParkLeavingReason.NothingLeftToDo:
            switch (Game1.Rnd.Next(0, 3))
            {
              case 0:
                return "I wish there were more things to do in this zoo. I hope the bus comes soon, I am bored.";
              case 1:
                return "I have seen everything I came to see!";
              default:
                return "There is nothing more for me to do.";
            }
          case ParkLeavingReason.ProtestsUpsetMe:
            return "The protestors really made me feel unfomfortable giving this zoo money";
          case ParkLeavingReason.DeadAnimals:
            return "I can't believe this park has dead animals init";
          case ParkLeavingReason.EnclosureFilfth:
            return "The dirt these animals live in is unnaceptable";
          case ParkLeavingReason.GarbageInZoo:
            return "This zoo is filfthy!";
          case ParkLeavingReason.NoATM_NoMoney:
            return "I have no money left, I need to find an ATM.";
        }
      }
      else
        return simperson.customertype == CustomerType.Protestor ? "This zoo doesnt treat it's animals well enough" : simperson.memberofthepublic.GetThought(simperson);
      return "NO STRING";
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void DrawThoughtsManager(SpriteBatch spritebatch, Vector2 Offset)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.miniheading.DrawMiniHeading(Offset);
      this.text.DrawSimpleTextHandler(Offset, 1f, spritebatch);
    }
  }
}
