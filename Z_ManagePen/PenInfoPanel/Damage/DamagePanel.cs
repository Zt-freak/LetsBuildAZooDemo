// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.PenInfoPanel.Damage.DamagePanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalsAndPeople.PersonCaller;
using TinyZoo.Z_Events;
using TinyZoo.Z_Events.BreakOut;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManagePen.Instructional;
using TinyZoo.Z_PenInfo.MainBar;
using TinyZoo.Z_SummaryPopUps.People;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManagePen.PenInfoPanel.Damage
{
  internal class DamagePanel
  {
    private CustomerFrame customerFrame;
    private DamageBar damagebar;
    private MiniHeading minihaeading;
    private Vector2 Location;
    private LittleSummaryButton_AndHeading littlesummarybutton;
    private SimpleBuildingRenderer GateItem;
    private List<Employee> Mechanics;
    private InstructionalPanel instructionalPanel;
    private PrisonZone REF_prisonzone;
    private float BaseScale;

    public DamagePanel(
      float _BaseScale,
      PrisonZone prisonzone,
      float UnMultipliedWidth,
      Player player)
    {
      this.BaseScale = _BaseScale;
      this.REF_prisonzone = prisonzone;
      TILETYPE tiletype = TileData.GetCellBlockTypeToPice(prisonzone.CellBLOCKTYPE, CellBlockPiece.Gate);
      this.damagebar = new DamageBar(this.BaseScale, prisonzone);
      this.customerFrame = new CustomerFrame(new Vector2(UnMultipliedWidth * this.BaseScale, 95f * this.BaseScale));
      float num = 15f * this.BaseScale;
      this.damagebar.Location.Y = num;
      this.minihaeading = new MiniHeading(this.customerFrame.VSCale, SEngine.Localization.Localization.GetText(1017), 1f, this.BaseScale);
      if ((double) prisonzone.GateIntegrity <= 0.0)
      {
        tiletype = TileData.GetGateTileTypeToBrokenGateTileType(tiletype);
        this.customerFrame.SetAlertRed();
      }
      this.GateItem = new SimpleBuildingRenderer(tiletype);
      this.GateItem.SetSize(50f * this.BaseScale, this.BaseScale * 3f);
      this.GateItem.vLocation = new Vector2((float) ((double) UnMultipliedWidth * (double) this.BaseScale * -0.5), 0.0f);
      this.GateItem.vLocation.X += (float) (50.0 * (double) this.BaseScale * 0.5) * Sengine.ScreenRationReductionMultiplier.Y;
      this.GateItem.vLocation.X += 10f * this.BaseScale;
      this.GateItem.vLocation.Y = num;
      this.Mechanics = new List<Employee>();
      for (int index = 0; index < player.employees.employees.Count; ++index)
      {
        if (player.employees.employees[index].employeetype == EmployeeType.Mechanic)
          this.Mechanics.Add(player.employees.employees[index]);
      }
      if (this.Mechanics.Count > 0)
      {
        this.littlesummarybutton = new LittleSummaryButton_AndHeading(LittleSummaryButtonType.CallSomeone, this.BaseScale);
        this.littlesummarybutton.SetCustomHeading(SEngine.Localization.Localization.GetText(1018));
      }
      else
      {
        this.littlesummarybutton = new LittleSummaryButton_AndHeading(LittleSummaryButtonType.AssignMoreStaff, this.BaseScale);
        this.littlesummarybutton.SetCustomHeading(SEngine.Localization.Localization.GetText(1019));
      }
      this.littlesummarybutton.Location.X = this.GateItem.vLocation.X * -1f;
      this.littlesummarybutton.Location.Y = num;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateDamagePanel(
      float DeltaTime,
      Player player,
      Vector2 Offset,
      out bool RemakeThis)
    {
      RemakeThis = false;
      Offset.X += this.Location.X;
      Offset.Y += this.Location.Y;
      if (this.instructionalPanel != null)
      {
        if (this.instructionalPanel.UpdateInstructionalPanel(DeltaTime, player, Offset))
          this.instructionalPanel = (InstructionalPanel) null;
        return false;
      }
      if (this.littlesummarybutton.UpdateLittleSummaryButton_AndHeading(DeltaTime, player, Offset))
      {
        if (this.Mechanics.Count == 0)
        {
          this.instructionalPanel = new InstructionalPanel(SEngine.Localization.Localization.GetText(1020), Z_GameFlags.GetBaseScaleForUI(), this.customerFrame.VSCale.X, this.customerFrame.VSCale.Y);
        }
        else
        {
          if (this.Mechanics.Count == 1)
          {
            CustomerManager.CallPersonToLocation(this.REF_prisonzone.GetNavigableGateLoction(), this.Mechanics[0]);
          }
          else
          {
            WalkingPerson walker;
            Employee nearestNonBusyPerson = CallPerson.FindNearestNonBusyPerson(this.Mechanics, this.REF_prisonzone.GetNavigableGateLoction(), out walker);
            CustomerManager.CallPersonToLocation(this.REF_prisonzone.GetNavigableGateLoction(), nearestNonBusyPerson, walker);
          }
          this.REF_prisonzone.FixGate(player);
          if (BreakOutManager.JustFixedAGate == null)
            BreakOutManager.JustFixedAGate = new List<int>();
          BreakOutManager.JustFixedAGate.Add(this.REF_prisonzone.Cell_UID);
          Z_EventsManager.AGateWasFixed = true;
          RemakeThis = true;
        }
      }
      return false;
    }

    public void DrawDamagePanel(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset.X += this.Location.X;
      Offset.Y += this.Location.Y;
      this.customerFrame.DrawCustomerFrame(Offset, spritebatch);
      if (this.instructionalPanel != null)
      {
        this.instructionalPanel.DrawInstructionalPanel(Offset, spritebatch);
      }
      else
      {
        this.damagebar.DrawDamageBar(spritebatch, Offset);
        this.minihaeading.DrawMiniHeading(Offset, spritebatch);
        this.GateItem.DrawSimpleBuildingRenderer(Offset, spritebatch);
        this.littlesummarybutton.DrawLittleSummaryButton_AndHeading(Offset, spritebatch);
      }
    }
  }
}
