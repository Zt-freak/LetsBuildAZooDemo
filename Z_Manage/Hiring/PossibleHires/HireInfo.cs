// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.PossibleHires.HireInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees;

namespace TinyZoo.Z_Manage.Hiring.PossibleHires
{
  internal class HireInfo
  {
    public GameObjectNineSlice frame;
    private Vector2 VScale;
    private Vector3 SecondaryColour;
    private SimpleTextHandler texty;
    private TextButton hirebuttn;
    private TextButton JobDescriptionButton;
    public PanelType paneltype;
    private bool WasRejectedCandidate;
    private SimpleTextHandler rejection;

    public HireInfo(EmployeeButton employeebutton, bool IsCurrentStaff)
    {
      this.frame = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Brown, out this.SecondaryColour), 7);
      this.VScale = new Vector2(500f, 650f);
      this.frame.vLocation = new Vector2(768f, 384f);
      this.frame.vLocation.Y += 50f;
      this.frame.scale = 2f * Sengine.ScreenRationReductionMultiplier.Y;
      float num = -50f;
      string TextToWrite1;
      if (IsCurrentStaff)
      {
        num = 20f;
        if (employeebutton.IsEmpty)
        {
          this.paneltype = PanelType.Hire;
          if (DebugFlags.IsPCVersion)
          {
            num = -100f;
            TextToWrite1 = "People are always applying for jobs at the zoo!~~Why not take a look at the applicants?~~A company is only as good as its employees!";
          }
          else
            TextToWrite1 = "The zoo could do with a new " + Employees.GetEmployeeThypeToString(employeebutton.emplyeetype) + "!~~Would you like to see the applicants for this role?";
          this.hirebuttn = new TextButton("Hire", 50f);
        }
        else
        {
          this.paneltype = PanelType.Fire;
          TextToWrite1 = "This team member is a great asset to the zoo, but you can fire them if you want to shake things up!";
          this.hirebuttn = new TextButton("Fire", 50f);
          this.hirebuttn.SetButtonColour(BTNColour.Red);
          num = -100f;
        }
      }
      else
      {
        this.paneltype = PanelType.ConfirmHire;
        TextToWrite1 = employeebutton.thispotentialhire.GetJobTitle() + "~~Salary Range: $" + (object) employeebutton.thispotentialhire.employeestats.MinimumWage + " to $" + (object) employeebutton.thispotentialhire.employeestats.MaximumWage;
        this.hirebuttn = new TextButton("Hire!", 50f);
        if (DebugFlags.IsPCVersion)
          this.hirebuttn = new TextButton("Interview!", 90f);
        if (employeebutton.thispotentialhire != null && employeebutton.thispotentialhire.CurrentHireResult != HireResult.NoResult)
        {
          this.hirebuttn = (TextButton) null;
          this.WasRejectedCandidate = true;
          string TextToWrite2 = "You rejected this candidate.";
          switch (employeebutton.thispotentialhire.CurrentHireResult)
          {
            case HireResult.TheyTookTheJob:
              TextToWrite2 = "This candidate took the job!.";
              break;
            case HireResult.TheyDidntACceptYourOffer:
              TextToWrite2 = "This candidate rejected your offer.";
              break;
          }
          this.rejection = new SimpleTextHandler(TextToWrite2, true, 0.4f, RenderMath.GetPixelSizeBestMatch(GameFlags.GetSmallTextScale()));
          this.rejection.paragraph.linemaker.SetAllColours(this.SecondaryColour);
          this.rejection.AutoCompleteParagraph();
        }
        else
          num = -200f;
        this.JobDescriptionButton = new TextButton("Job Description", 90f);
        this.JobDescriptionButton.SetButtonColour(BTNColour.PaleYellow);
      }
      this.texty = new SimpleTextHandler(TextToWrite1, true, 0.4f, GameFlags.GetSmallTextScale());
      this.texty.paragraph.linemaker.SetAllColours(this.SecondaryColour);
      this.texty.AutoCompleteParagraph();
      this.texty.Location.Y = num;
      if (this.hirebuttn != null)
      {
        this.hirebuttn.vLocation.Y = 60f;
        if (this.paneltype == PanelType.ConfirmHire)
          this.hirebuttn.vLocation.Y = -120f;
      }
      if (this.JobDescriptionButton == null)
        return;
      this.JobDescriptionButton.vLocation.Y = -60f;
    }

    public bool UpdateHireInfo(
      Player player,
      Vector2 Offset,
      float DeltaTime,
      out bool DisplayJobDescription)
    {
      DisplayJobDescription = false;
      TextButton descriptionButton = this.JobDescriptionButton;
      return this.hirebuttn != null && this.hirebuttn.UpdateTextButton(player, Offset + this.frame.vLocation, DeltaTime);
    }

    public void DrawHireInfo(Vector2 Offset, BigPersonFrame bigperson)
    {
      this.frame.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VScale);
      this.texty.DrawSimpleTextHandler(this.frame.vLocation + new Vector2(0.0f, 0.0f) + Offset, 1f, AssetContainer.pointspritebatchTop05);
      bigperson.DrawBigPersonFrame(this.frame.vLocation - bigperson.Location + new Vector2(0.0f, -250f) + Offset, AssetContainer.pointspritebatchTop05);
      if (this.hirebuttn != null)
        this.hirebuttn.DrawTextButton(this.frame.vLocation + Offset, 1f, AssetContainer.pointspritebatchTop05);
      TextButton descriptionButton = this.JobDescriptionButton;
      if (!this.WasRejectedCandidate)
        return;
      this.rejection.DrawSimpleTextHandler(this.frame.vLocation + new Vector2(0.0f, 150f) + Offset, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}
