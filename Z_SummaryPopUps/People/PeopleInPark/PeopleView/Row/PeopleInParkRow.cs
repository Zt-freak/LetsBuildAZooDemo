// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.PeopleInParkRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info;

namespace TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row
{
  internal class PeopleInParkRow
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private AnimalInFrame animalInFrame;
    private ZGenericText name;
    private ZGenericText role;
    private PeopleInfoDisplay info;
    private LittleSummaryButton magnifyingButton;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    private PeopleViewInfoType refInfoType;
    internal static float rowHeight_Raw = 35f;
    private bool rowActive;

    public WalkingPerson refPerson { get; private set; }

    public PeopleInParkRow(float _BaseScale, float forcedWidth)
    {
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.customerFrame = new CustomerFrame(new Vector2(forcedWidth, this.scaleHelper.ScaleY(PeopleInParkRow.rowHeight_Raw)), CustomerFrameColors.DarkBrown, this.BaseScale);
      this.ToggleDarken(true);
    }

    public void PopulateRow(WalkingPerson person)
    {
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      float defaultXbuffer = this.scaleHelper.GetDefaultXBuffer();
      double defaultYbuffer = (double) this.scaleHelper.GetDefaultYBuffer();
      this.refPerson = person;
      float num1 = 25f;
      float num2 = defaultXbuffer * 0.5f + vector2.X;
      this.animalInFrame = new AnimalInFrame(person.thispersontype, AnimalType.None, TargetSize: this.scaleHelper.ScaleX(num1), FrameEdgeBuffer: this.scaleHelper.ScaleX(6f), BaseScale: this.BaseScale);
      this.animalInFrame.Location.X = num2 + this.animalInFrame.GetSize().X * 0.5f;
      float num3 = num2 + (this.animalInFrame.GetSize().X + defaultXbuffer);
      bool flag = person.simperson.roleinsociety != RoleInSociety.Customer;
      this.name = new ZGenericText(string.Empty + person.simperson.GetName(), this.BaseScale, false, _UseOnePointFiveFont: (!flag));
      Vector2 size = this.name.GetSize();
      this.name.vLocation.X = num3;
      this.name.vLocation.Y -= size.Y * 0.5f;
      float num4 = num3 + this.scaleHelper.ScaleX(130f);
      if (flag)
      {
        this.role = new ZGenericText(PeopleInParkRow.GetRoleInSocietyToString(person.simperson.roleinsociety, person.simperson.customertype), this.BaseScale, false, _UseOnePointFiveFont: true);
        this.role.vLocation.X = this.name.vLocation.X;
        this.role.vLocation.Y -= this.role.GetSize().Y * 0.5f;
        this.name.vLocation.Y -= this.scaleHelper.ScaleY(5f);
        this.role.vLocation.Y += this.scaleHelper.ScaleY(5f);
      }
      this.info = new PeopleInfoDisplay(this.BaseScale);
      this.info.SetInfoType(this.refInfoType);
      this.info.location.X = num4;
      float num5 = num4 + this.scaleHelper.ScaleX(165f);
      this.magnifyingButton = new LittleSummaryButton(LittleSummaryButtonType.Locate, _BaseScale: this.BaseScale);
      this.magnifyingButton.vLocation.X = num5;
      float num6 = num5 + (float) ((double) this.magnifyingButton.GetSize().X * 0.5 + (double) defaultXbuffer * 0.5);
      this.rowActive = true;
      this.ToggleDarken(false);
    }

    public void ToggleDarken(bool isDark)
    {
      if (isDark)
      {
        this.customerFrame.SetAlphaed();
        if (this.animalInFrame == null)
          return;
        this.magnifyingButton.SetDisabled(true);
        this.animalInFrame.Darken(true);
        this.info.Darken();
        this.name.SetInactiveColor();
        if (this.role == null)
          return;
        this.role.SetInactiveColor();
      }
      else
      {
        this.customerFrame.SetAlphaed(1f);
        this.magnifyingButton.SetDisabled(false);
        this.animalInFrame.Darken(false);
      }
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void SetInfoType(PeopleViewInfoType infoType)
    {
      this.refInfoType = infoType;
      if (this.info == null)
        return;
      this.info.SetInfoType(this.refInfoType);
    }

    public static string GetRoleInSocietyToString(RoleInSociety role, CustomerType customerType)
    {
      switch (role)
      {
        case RoleInSociety.Avatar:
          return "You";
        case RoleInSociety.Employee:
          return customerType == CustomerType.Policeman ? "Police Man" : "Employee";
        case RoleInSociety.Customer:
          switch (customerType)
          {
            case CustomerType.Normal:
            case CustomerType.SecretShopper:
            case CustomerType.HeartAttack:
              return "Visitor";
            case CustomerType.Protestor:
              return "Protestor";
            case CustomerType.Footballer:
              return "FootBaller";
            case CustomerType.HealthInspector:
              return "Food Hygiene Inspector";
            case CustomerType.AnimalWelfareOfficer:
              return "Animal Welfare Officer";
            case CustomerType.SafetyInspector:
              return "Safety Inspector";
            case CustomerType.Influencer:
              return "Influencer";
            case CustomerType.Biker:
              return "Biker";
            case CustomerType.Teacher:
              return "Teacher";
            case CustomerType.Student:
              return "Student";
            case CustomerType.FoodCritic:
              return "Food Critic";
            case CustomerType.AnimalRightsActivist:
              return "Animal Rights Activist";
            case CustomerType.Drunkard:
              return "Drunk Visitor";
            case CustomerType.Hunter:
              return "Hunter";
            case CustomerType.MovieStar:
              return "Movie Star";
            case CustomerType.FakeSuperVillain:
              return "Super Villian";
            case CustomerType.UFO:
              return "UFO";
            case CustomerType.BlackMarket:
              return "Black Market Dealer";
            case CustomerType.ResearchGrantGuy:
              return "Researcher";
            case CustomerType.AnimalArtist:
              return "Artist";
            case CustomerType.GenomeBetaGiver:
              return "Geneticist";
            default:
              return "Visitor";
          }
        case RoleInSociety.BlackMarket:
          return "Black Market Dealer";
        default:
          return "NA_" + role.ToString();
      }
    }

    public bool UpdatePeopleInParkRow(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.refPerson != null)
      {
        this.info.UpdatePeopleInfoDisplay(offset, this.refPerson);
        if (this.rowActive && this.refPerson.simperson.memberofthepublic.IsAtBusWaiting)
        {
          this.rowActive = false;
          this.ToggleDarken(true);
        }
        if (this.magnifyingButton.UpdateLittleSummaryButton(DeltaTime, player, offset))
          return true;
      }
      return false;
    }

    public void DrawPeopleInParkRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      if (this.refPerson == null)
        return;
      this.animalInFrame.DrawAnimalInFrame(offset, spriteBatch);
      this.name.DrawZGenericText(offset, spriteBatch);
      if (this.role != null)
        this.role.DrawZGenericText(offset, spriteBatch);
      this.info.DrawPeopleInfoDisplay(offset, spriteBatch);
      this.magnifyingButton.DrawLittleSummaryButton(offset, spriteBatch);
    }
  }
}
