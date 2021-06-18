// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.JobDesc.JobDescriptionDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.employees;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Manage.Hiring.JobDesc
{
  internal class JobDescriptionDisplay
  {
    private LerpHandler_Float lerper;
    private BlackOut blackout;
    private SimpleTextHandler texty;
    private TextButton hirebuttn;
    private string Heading;
    private bool IsMiniDescription;
    private ScreenHeading heading;
    private CustomerFrame BrownHoleFiller;

    public JobDescriptionDisplay(PotentialHire thispotentialhire, bool _IsMiniDescription = false)
    {
      this.IsMiniDescription = _IsMiniDescription;
      this.heading = new ScreenHeading("JOB DESCRIPTION", 80f);
      this.blackout = new BlackOut();
      this.blackout.SetAllColours(ColourData.ACDarkerBlue);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.Heading = thispotentialhire.GetJobTitle();
      thispotentialhire.employeestats.JobDescription + "~~Salary Range: $" + (object) thispotentialhire.employeestats.MinimumWage + " to $" + (object) thispotentialhire.employeestats.MaximumWage;
      float pixelSizeBestMatch = RenderMath.GetPixelSizeBestMatch(GameFlags.GetSmallTextScale());
      float PercentagePfScreenWidth = 0.8f;
      if (this.IsMiniDescription)
      {
        PercentagePfScreenWidth = 0.35f;
        this.BrownHoleFiller = new CustomerFrame(new Vector2(470f, 350f));
        this.BrownHoleFiller.frame.vLocation.Y = 50f;
      }
      this.texty = new SimpleTextHandler(thispotentialhire.employeestats.JobDescription, true, PercentagePfScreenWidth, pixelSizeBestMatch);
      this.texty.AutoCompleteParagraph();
      this.texty.Location = new Vector2(512f, 400f);
      if (this.IsMiniDescription)
        this.texty.Location = new Vector2(0.0f, 0.0f);
      if (!this.IsMiniDescription)
        return;
      this.lerper.SetLerp(true, 0.0f, 0.0f, 3f);
    }

    public void Exit()
    {
      if ((double) this.lerper.TargetValue == 1.0)
        return;
      this.lerper.SetLerp(true, 0.0f, 1f, 3f, true);
    }

    public bool UpdateJobDescription(float DeltaTime, Player player)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value == 0.0 && (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0)
        this.lerper.SetLerp(true, 0.0f, 1f, 3f, true);
      return (double) this.lerper.Value == 1.0 && (double) this.lerper.TargetValue == 1.0;
    }

    public void DrawJobDescription(BigPersonFrame bigperson, Vector2 SidePanelRoot)
    {
      Vector2 Offset = new Vector2(this.lerper.Value * 1024f, 0.0f);
      if (this.IsMiniDescription)
      {
        SidePanelRoot += new Vector2(0.0f, 50f);
        this.BrownHoleFiller.DrawCustomerFrame(SidePanelRoot, AssetContainer.pointspritebatchTop05);
        this.texty.DrawSimpleTextHandler(SidePanelRoot + new Vector2(0.0f, -60f), 1f, AssetContainer.pointspritebatchTop05);
        this.heading.header.vLocation = new Vector2(0.0f, -100f);
        this.heading.DrawScreenHeading(SidePanelRoot, AssetContainer.pointspritebatchTop05);
      }
      else
      {
        this.blackout.DrawBlackOut(Offset, AssetContainer.pointspritebatchTop05);
        this.heading.DrawScreenHeading(Offset, AssetContainer.pointspritebatchTop05);
        TextFunctions.DrawTextWithDropShadow(this.Heading, 1.5f, new Vector2(100f, 100f) + Offset, Color.White, 0.8f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, false);
        this.texty.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatchTop05);
        bigperson.DrawBigPersonFrame(new Vector2(512f, 450f) - bigperson.Location + new Vector2(0.0f, -200f) + Offset, AssetContainer.pointspritebatchTop05);
      }
    }
  }
}
