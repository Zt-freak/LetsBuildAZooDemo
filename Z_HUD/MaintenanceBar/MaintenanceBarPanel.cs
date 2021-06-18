// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.MaintenanceBar.MaintenanceBarPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManagePen.Instructional;
using TinyZoo.Z_PenInfo.MainBar;
using TinyZoo.Z_SummaryPopUps.People;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.MaintenanceBar
{
  internal class MaintenanceBarPanel
  {
    private CustomerFrame customerFrame;
    private TinyZoo.Z_HUD.MaintenanceBar.MaintenanceBar bar;
    public Vector2 location;
    private LittleSummaryButton_AndHeading littlesummarybutton;
    private List<Employee> employees;
    private SimpleBuildingRenderer building;
    private AnimalInFrame animalinframe;
    private InstructionalPanel instructionalPanel;
    private float basescale;
    private static float rawWidth = 200f;
    private UIScaleHelper uiscale;
    private Vector2 framescale;

    public MaintenanceBarPanel(PrisonerInfo animalinfo, float _basescale, Player player)
    {
      this.basescale = _basescale;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.bar = new TinyZoo.Z_HUD.MaintenanceBar.MaintenanceBar(100f, this.basescale);
      this.animalinframe = new AnimalInFrame(animalinfo.intakeperson.animaltype, animalinfo.intakeperson.HeadType, animalinfo.intakeperson.CLIndex, 50f * this.basescale, 6f * this.basescale, 2f * this.basescale, animalinfo.intakeperson.HeadVariant);
      this.animalinframe.SetDead(animalinfo.intakeperson.animaltype, animalinfo.intakeperson.CLIndex);
      this.employees = new List<Employee>();
      this.employees = player.employees.GetEmployeesOfThisType(EmployeeType.MeatProcessorWorker);
      bool flag = false;
      if (!(player.shopstatus.GetTotalOfThese(TILETYPE.Incinerator) > 0 | player.shopstatus.GetTotalOfThese(TILETYPE.MeatProcessor) > 0))
      {
        this.littlesummarybutton = new LittleSummaryButton_AndHeading(LittleSummaryButtonType.BuildingRequired, this.basescale);
        this.littlesummarybutton.SetCustomHeading("Requires~Incinerator");
        flag = true;
      }
      else if (this.employees.Count > 0)
      {
        this.littlesummarybutton = new LittleSummaryButton_AndHeading(LittleSummaryButtonType.CallSomeone, this.basescale);
        this.littlesummarybutton.SetCustomHeading("Call Corpse~Collector");
      }
      else
      {
        this.littlesummarybutton = new LittleSummaryButton_AndHeading(LittleSummaryButtonType.AssignMoreStaff, this.basescale);
        this.littlesummarybutton.SetCustomHeading("Requires Corpse~Collector");
        flag = true;
      }
      this.framescale.X = this.bar.GetSize().X;
      this.framescale.X += 6f * this.uiscale.DefaultBuffer.X + this.animalinframe.GetSize().X + Math.Max(this.littlesummarybutton.GetTextSize().X, this.littlesummarybutton.GetSize().X);
      this.framescale.Y = this.animalinframe.GetSize().Y + 2f * this.uiscale.DefaultBuffer.Y;
      this.customerFrame = new CustomerFrame(this.framescale, CustomerFrameColors.Brown, this.basescale);
      if (flag)
        this.customerFrame.SetAlertRed();
      this.littlesummarybutton.Location.X = (float) ((double) this.bar.location.X + 0.5 * (double) this.bar.GetSize().X + 3.0 * (double) this.uiscale.DefaultBuffer.X + 0.5 * (double) this.littlesummarybutton.GetSize().X);
      this.littlesummarybutton.Location.Y += this.littlesummarybutton.GetTextSize().Y * 0.5f;
      this.animalinframe.Location.X = -this.littlesummarybutton.Location.X;
      this.bar.location.Y -= this.bar.GetSize().Y * 0.5f;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateMaintenanceBarPanel(
      float DeltaTime,
      Player player,
      Vector2 offset,
      out bool RemakeThis)
    {
      RemakeThis = false;
      offset += this.location;
      if (this.instructionalPanel != null)
      {
        if (this.instructionalPanel.UpdateInstructionalPanel(DeltaTime, player, offset))
          this.instructionalPanel = (InstructionalPanel) null;
        return false;
      }
      if (this.littlesummarybutton.UpdateLittleSummaryButton_AndHeading(DeltaTime, player, offset))
      {
        if (this.employees.Count == 0)
        {
          this.instructionalPanel = new InstructionalPanel("Build an Incinerator or a Meat Processing Plant to incinerate or process this carcass.", this.basescale, this.customerFrame.VSCale.X, this.customerFrame.VSCale.Y);
        }
        else
        {
          int count = this.employees.Count;
          RemakeThis = true;
        }
      }
      return false;
    }

    public void DrawMaintenanceBarPanel(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spritebatch);
      if (this.instructionalPanel != null)
      {
        this.instructionalPanel.DrawInstructionalPanel(offset, spritebatch);
      }
      else
      {
        this.animalinframe.DrawAnimalInFrame(offset, spritebatch);
        this.littlesummarybutton.DrawLittleSummaryButton_AndHeading(offset, spritebatch);
        this.bar.DrawMaintenanceBar(spritebatch, offset);
      }
    }
  }
}
