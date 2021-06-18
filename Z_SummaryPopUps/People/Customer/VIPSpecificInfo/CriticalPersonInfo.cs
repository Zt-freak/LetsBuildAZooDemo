// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo.CriticalPersonInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalNotification;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo
{
  internal class CriticalPersonInfo : VIPInfo
  {
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private LabelledBar Bar01;
    private LabelledBar Bar02;
    private LabelledBar Bar03;
    private SimpleTextHandler interactions;
    private WalkingPerson Ref_WalkingPerson;

    public CriticalPersonInfo(
      float basescale_,
      float forceThisWidth = -1f,
      CustomerType customer = CustomerType.Count,
      WalkingPerson person = null)
    {
      this.Ref_WalkingPerson = person;
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      string TextToWrite = "";
      string text = "Rejected";
      int selectedChoice = this.Ref_WalkingPerson.simperson.memberofthepublic.criticalchoiceVIP.SelectedChoice;
      if (selectedChoice != -1)
      {
        switch (customer)
        {
          case CustomerType.ResearchGrantGuy:
            if (selectedChoice == 0)
            {
              text = "Accepted";
              TextToWrite = "Grant Offer received. Build research building before the end of the day!";
              break;
            }
            TextToWrite = "Grant Offer rejected. You should still consider making a research building.";
            break;
          case CustomerType.AnimalArtist:
            if (selectedChoice == 0)
            {
              text = "Accepted";
              TextToWrite = "A fake-Zebra now exists in your zoo. Your drive to make money is unparalleled.";
              break;
            }
            TextToWrite = "You rejected the opportunity to paint a horse to look like a zebra. You are a very upstanding person.";
            break;
          case CustomerType.GenomeBetaGiver:
            if (selectedChoice == 0)
            {
              text = "Accepted Genome";
              TextToWrite = "You can now make Rabbit and Snake hybrids in the CRISPR Splicer - Build one to start making them now!";
              break;
            }
            text = "Accepted Genome";
            TextToWrite = "You can now make Rabbit and Hippo hybrids in the CRISPR Splicer - Build one to start making them now!";
            break;
        }
      }
      this.interactions = new SimpleTextHandler(TextToWrite, this.scalehelper.ScaleX(200f), _Scale: this.basescale);
      this.interactions.AutoCompleteParagraph();
      this.interactions.SetAllColours(ColourData.Z_Cream);
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.frame.AddMiniHeading(text);
      this.framescale = new Vector2();
      this.framescale += 2f * defaultBuffer;
      if (this.Bar01 != null)
      {
        this.framescale.Y += this.Bar01.GetSize().Y;
        this.framescale.Y += this.Bar02.GetSize().Y;
        this.framescale.Y += this.Bar03.GetSize().Y;
        this.framescale.X += 2f * this.Bar03.GetBarSize().X;
        this.framescale.Y += 3f * defaultBuffer.Y;
      }
      this.framescale.Y += this.interactions.GetSize(true).Y;
      this.framescale.Y += this.frame.GetMiniHeadingHeight();
      if ((double) forceThisWidth > 0.0)
        this.framescale.X = forceThisWidth;
      this.frame.Resize(this.framescale);
      Vector2 vector2 = -0.5f * this.framescale + defaultBuffer;
      vector2.Y += this.frame.GetMiniHeadingHeight();
      if (this.Bar01 != null)
      {
        float num = this.Bar01.GetSize().X - this.Bar01.GetBarSize().X;
        this.Bar01.location = vector2;
        this.Bar01.location.X += num;
        vector2.Y += this.Bar01.GetBarSize().Y + defaultBuffer.Y;
        this.Bar02.location = vector2;
        this.Bar02.location.X += num;
        vector2.Y += this.Bar02.GetBarSize().Y + defaultBuffer.Y;
        this.Bar03.location = vector2;
        this.Bar03.location.X += num;
        vector2.Y += this.Bar03.GetBarSize().Y + defaultBuffer.Y;
      }
      this.interactions.Location = vector2;
    }

    public override Vector2 GetSize() => this.framescale;

    public override bool UpdateVIPInfo(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return false;
    }

    public override void DrawVIPInfo(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      if (this.Bar01 != null)
      {
        this.Bar01.DrawLabelledBar(offset, spritebatch);
        this.Bar02.DrawLabelledBar(offset, spritebatch);
        this.Bar03.DrawLabelledBar(offset, spritebatch);
      }
      this.interactions.DrawSimpleTextHandler(offset, 1f, spritebatch);
    }
  }
}
