// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.EmployeeButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Manage.Hiring
{
  internal class EmployeeButton
  {
    private GameObjectNineSlice frame;
    public BigPersonFrame bigperson;
    public Vector2 Location;
    private Vector2 VScale;
    private SimpleTextHandler textparagraph;
    public bool IsEmpty;
    private Color textcolour;
    private bool Selected;
    public IntakePerson intakeperson;
    public EmployeeType emplyeetype;
    private string Heading;
    public PotentialHire thispotentialhire;
    private StarBar starbar;
    private bool WasUnwantedHire;

    public EmployeeButton(
      bool _IsEmpty,
      Employee emplyee,
      PotentialHire _person,
      EmployeeType emplythis)
    {
      this.thispotentialhire = _person;
      if (_person != null)
        this.WasUnwantedHire = _person.CurrentHireResult == HireResult.YouDintPickThem || _person.CurrentHireResult == HireResult.TheyDidntACceptYourOffer || _person.CurrentHireResult == HireResult.TheyTookTheJob;
      if (emplyee != null)
        _person = this.thispotentialhire;
      this.emplyeetype = EmployeeType.Mascot;
      if (_person != null)
        this.intakeperson = _person.intakeperson;
      else if (emplyee != null)
        this.intakeperson = emplyee.intakeperson;
      this.IsEmpty = _IsEmpty;
      this.Location.X = 256f;
      this.textcolour = new Color(120, 90, 90);
      if (this.intakeperson != null)
      {
        this.Heading = emplyee == null ? Employees.GetEmployeeThypeToString(emplythis) : Employees.GetEmployeeThypeToString(emplyee.employeetype);
        if (_person != null)
          this.Heading = _person.GetJobTitle();
        this.bigperson = new BigPersonFrame(this.intakeperson.animaltype, this.IsEmpty, this.intakeperson.Name);
        if (this.WasUnwantedHire)
          this.bigperson.GreyOut();
      }
      else
      {
        this.Heading = "Hire New Employee";
        this.bigperson = new BigPersonFrame(AnimalType.KeeperWhite, this.IsEmpty);
      }
      this.bigperson.Location.X = -180f;
      string TextToWrite = " ";
      if (this.intakeperson != null)
      {
        if (emplyee != null)
        {
          TextToWrite = "Employee: " + this.intakeperson.Name;
          this.starbar = new StarBar(emplyee.GetPerformance());
        }
        else
          TextToWrite = "Applicant: " + this.intakeperson.Name;
      }
      this.textparagraph = new SimpleTextHandler(TextToWrite, false, 0.3f, RenderMath.GetPixelSizeBestMatch(GameFlags.GetSmallTextScale()), false, false);
      this.textparagraph.AutoCompleteParagraph();
      this.textparagraph.paragraph.linemaker.SetAllColours(1f, 1f, 1f);
      this.SelectThis(false, true);
      this.VScale = new Vector2(500f, 150f * Sengine.ScreenRationReductionMultiplier.Y);
      if (!DebugFlags.IsPCVersion)
        return;
      this.VScale.Y *= 0.5f;
    }

    public bool UpdateEmployeeButton(Vector2 Offset, float DeltaTime, Player player)
    {
      this.bigperson.UpdateBigPersonFrame(DeltaTime, player);
      return MathStuff.CheckPointCollision(true, Offset + this.Location, 1f, this.VScale.X, this.VScale.Y, player.player.touchinput.MultiTouchTapArray[0]);
    }

    public void SelectThis(bool IsSelect, bool Force = false)
    {
      if (!(this.Selected != IsSelect | Force))
        return;
      this.Selected = IsSelect;
      if (!this.Selected)
      {
        Vector3 SecondaryColour;
        this.frame = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Grey, out SecondaryColour), 7);
        this.textparagraph.paragraph.linemaker.SetAllColours(SecondaryColour);
        this.textcolour = new Color(SecondaryColour);
      }
      else
      {
        Vector3 SecondaryColour;
        this.frame = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
        this.textparagraph.paragraph.linemaker.SetAllColours(SecondaryColour);
        this.textcolour = new Color(SecondaryColour);
      }
      this.frame.scale = RenderMath.GetPixelSizeBestMatch(1f);
    }

    public void DrawEmployeeButton(Vector2 Offset)
    {
      Offset += this.Location;
      this.frame.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VScale * Sengine.ScreenRatioUpwardsMultiplier);
      this.bigperson.DrawBigPersonFrame(Offset, AssetContainer.pointspritebatchTop05);
      if (this.IsEmpty)
      {
        if (DebugFlags.IsPCVersion)
          TextFunctions.DrawTextWithDropShadow(this.Heading, 1f * Sengine.ScreenRationReductionMultiplier.Y, Offset + new Vector2(-120f, -10f), this.textcolour, 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, false);
        else
          TextFunctions.DrawTextWithDropShadow(this.Heading, 1f, Offset + new Vector2(-120f, -10f), this.textcolour, 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, false);
      }
      else if (DebugFlags.IsPCVersion)
      {
        TextFunctions.DrawTextWithDropShadow(this.Heading, 1f * Sengine.ScreenRationReductionMultiplier.Y, Offset + new Vector2(-120f, -30f), this.textcolour, 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, false);
        this.textparagraph.DrawSimpleTextHandler(Offset + new Vector2(-120f, 0.0f), 1f, AssetContainer.pointspritebatchTop05);
      }
      else
      {
        TextFunctions.DrawTextWithDropShadow(this.Heading, 1f, Offset + new Vector2(-120f, -50f), this.textcolour, 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, false);
        this.textparagraph.DrawSimpleTextHandler(Offset + new Vector2(-120f, -20f), 1f, AssetContainer.pointspritebatchTop05);
      }
    }
  }
}
