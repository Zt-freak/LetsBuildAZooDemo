// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.VIPProfile
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class VIPProfile
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private Vector2 framescale;
    private AnimalInFrame picture;
    private string name;
    private Vector2 nameloc;
    private string role;
    private Vector2 roleloc;
    private string descstr;
    private SimpleTextHandler desc;
    private Vector2 currloc;

    public VIPProfile(WalkingPerson person, string name_, float basescale_, float forceWidth = -1f)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      CustomerType typeOfThisPerson = CustomerViewManager.GetCustomerTypeOfThisPerson(person);
      this.role = PeopleInParkRow.GetRoleInSocietyToString(person.simperson.roleinsociety, typeOfThisPerson);
      this.name = name_;
      float num = 195f;
      switch (typeOfThisPerson)
      {
        case CustomerType.Normal:
        case CustomerType.SecretShopper:
          this.descstr = "A visitor of the zoo";
          break;
        case CustomerType.Protestor:
          this.descstr = "This animal rights activist is protesting some part of zoo operation they deem unethical";
          break;
        case CustomerType.HealthInspector:
          this.descstr = "An inspector that will rate the food hygiene in some shops, for better or for worse.";
          break;
        case CustomerType.AnimalWelfareOfficer:
          this.descstr = "An officer that will evaluate the welfare and treatment of your animals.";
          break;
        case CustomerType.SafetyInspector:
          this.descstr = "A safety inspector that will check if your zoo abides by the safety regulations.";
          break;
        case CustomerType.Influencer:
          this.descstr = "A content creator with a substantial online following.";
          break;
        case CustomerType.Biker:
          this.descstr = "A biker, pack hunters of the highway";
          break;
        case CustomerType.Teacher:
          this.descstr = "A teacher taking a group of students out on a field trip to the zoo.";
          break;
        case CustomerType.Student:
          this.descstr = "A student on a field trip to the zoo!";
          break;
        case CustomerType.FoodCritic:
          this.descstr = "A food critic who has arrived without advance notice to rate the food in the zoo.";
          break;
        case CustomerType.BlackMarket:
          this.descstr = "You want to get something special? I have something really interesting, just don't ask me where I got it.";
          break;
        case CustomerType.Policeman:
          this.descstr = "A policeman who will not hesitate to take action to keep the zoo safe.";
          break;
        case CustomerType.ResearchGrantGuy:
          num = 215f;
          this.name = "Issac";
          this.descstr = "A research scholar, who has the power to fund the betterment of mankind's understanding of the natural world.";
          break;
        case CustomerType.AnimalArtist:
          this.name = "Salvador";
          this.descstr = "An artist with an eye for crafting animals into something they are not.";
          break;
        case CustomerType.GenomeBetaGiver:
          this.name = "Nicole";
          this.descstr = "An employee of the Mon-Santa agrochemical and agricultural biotechnology corporation.";
          break;
        default:
          this.descstr = "A customer of the zoo";
          break;
      }
      this.picture = new AnimalInFrame(person.thispersontype, AnimalType.None, TargetSize: (50f * this.basescale), BaseScale: (2f * this.basescale));
      Vector2 vector2 = 2f * this.uiscale.ScaleVector2(AssetContainer.SpringFontX1AndHalf.MeasureString(this.name));
      this.uiscale.ScaleVector2(AssetContainer.SpringFontX1AndHalf.MeasureString(this.role));
      this.desc = new SimpleTextHandler(this.descstr, this.uiscale.ScaleX(num), _Scale: this.basescale);
      this.desc.SetAllColours(ColourData.Z_Cream);
      this.desc.AutoCompleteParagraph();
      this.framescale = 2f * defaultBuffer;
      this.framescale.X += this.desc.GetSize().X;
      this.framescale.Y += this.picture.GetSize().Y + this.desc.GetSize().Y + defaultBuffer.Y;
      if ((double) forceWidth != -1.0)
        this.framescale.X = forceWidth;
      this.currloc = -0.5f * this.framescale + defaultBuffer;
      this.picture.Location = this.currloc + 0.5f * this.picture.GetSize();
      this.currloc.X += this.picture.GetSize().X + defaultBuffer.X;
      this.nameloc = this.currloc;
      this.currloc.Y += vector2.Y;
      this.roleloc = this.currloc;
      this.currloc = this.picture.Location;
      this.currloc.X -= 0.5f * this.picture.GetSize().X;
      this.currloc.Y += 0.5f * this.picture.GetSize().Y + defaultBuffer.Y;
      this.desc.Location = this.currloc;
      this.frame = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateVIPProfile(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return false;
    }

    public void DrawVIPProfile(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.picture.DrawAnimalInFrame(offset, spritebatch);
      TextFunctions.DrawTextWithDropShadow(this.name, 2f * this.basescale, offset + this.nameloc, new Color(ColourData.Z_Cream), 1f, AssetContainer.SpringFontX1AndHalf, spritebatch, false);
      TextFunctions.DrawTextWithDropShadow(this.role, this.basescale, offset + this.roleloc, new Color(ColourData.Z_Cream), 1f, AssetContainer.SpringFontX1AndHalf, spritebatch, false);
      this.desc.DrawSimpleTextHandler(offset, 1f, spritebatch);
    }
  }
}
