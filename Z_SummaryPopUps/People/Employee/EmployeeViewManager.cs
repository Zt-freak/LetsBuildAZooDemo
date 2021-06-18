// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Employee.EmployeeViewManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Employee
{
  internal class EmployeeViewManager
  {
    private Vector2 Location;
    private BigBrownPanel panel;
    private string Name;
    private WalkingPerson person;
    private bool hidemainpanel;
    private GenericEmployeePanels genericPanels;
    private EmployeeType employeetype;
    private Vector2 framescale;
    private UIScaleHelper scalehelper;
    private float basescale;
    private ActionPopUpManager popup;
    private LookAtThisThingButton lookAtThisThingButton;

    public EmployeeViewManager(Player player, WalkingPerson person_)
    {
      this.basescale = Z_GameFlags.GetBaseScaleForUI();
      this.person = person_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.employeetype = EmployeeViewManager.GetEmployeeTypeOfThisPerson(this.person);
      string addHeaderText = this.employeetype.ToString();
      this.Name = this.person.simperson.GetName();
      this.framescale = new Vector2();
      int employeetype = (int) this.employeetype;
      this.genericPanels = new GenericEmployeePanels(player, this.person, this.basescale);
      this.framescale += this.genericPanels.GetSize();
      this.panel = new BigBrownPanel(Vector2.Zero, true, addHeaderText, this.basescale);
      this.panel.Finalize(this.framescale);
      this.Location = this.Location = new Vector2((float) (1024.0 - (double) this.scalehelper.ScaleX(CustomerViewManager.topRightLocBuffer_Raw.X) - (double) this.framescale.X * 0.5), 384f);
      this.lookAtThisThingButton = new LookAtThisThingButton(this.person, this.basescale);
      this.lookAtThisThingButton.location.X = this.panel.GetMiniHeadingSize(false).X + this.scalehelper.DefaultBuffer.X;
      this.lookAtThisThingButton.location -= this.panel.vScale * 0.5f;
      this.lookAtThisThingButton.location.X += this.lookAtThisThingButton.GetSize().X * 0.5f + this.scalehelper.DefaultBuffer.X;
      this.lookAtThisThingButton.location.Y += (float) ((double) this.panel.GetMiniHeadingSize(false).Y * 0.5 + (double) this.scalehelper.DefaultBuffer.Y * 0.5);
      this.lookAtThisThingButton.location.Y -= this.lookAtThisThingButton.GetSize().Y * 0.5f;
    }

    public static EmployeeType GetEmployeeTypeOfThisPerson(WalkingPerson person) => person.simperson.Ref_Employee.employeetype;

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.Location;
      return this.popup != null && this.popup.CheckMouseOver(player) || this.panel.CheckMouseOver(player, offset);
    }

    public bool UpdateEmployeeViewManager(Vector2 offset, Player player, float DeltaTime)
    {
      this.hidemainpanel = false;
      this.panel.UpdateDragger(player, ref this.Location, DeltaTime);
      bool flag1 = false;
      if (this.panel.UpdatePanelCloseButton(player, DeltaTime, this.Location + offset))
        flag1 = true;
      bool flag2 = false;
      CustomerActionType actiontype = CustomerActionType.None;
      if (this.panel.Active)
      {
        int employeetype = (int) this.employeetype;
        flag2 |= this.genericPanels.UpdateGenericEmployeePanels(player, offset + this.Location, DeltaTime, out actiontype);
      }
      if (this.popup == null)
      {
        if (flag2)
        {
          if (actiontype != CustomerActionType.None)
          {
            this.popup = new ActionPopUpManager(actiontype, this.basescale, this.person, player);
            flag2 = false;
          }
          this.panel.Active = false;
        }
      }
      else
      {
        this.hidemainpanel = this.popup.hidemainpanel;
        bool ForceCloseEverythingOnClose;
        if (this.popup.UpdateActionPopUpManager(player, DeltaTime, out ForceCloseEverythingOnClose))
        {
          this.popup = (ActionPopUpManager) null;
          this.panel.Active = true;
          this.genericPanels.RefreshPanels();
          if (ForceCloseEverythingOnClose)
            flag2 = ((flag2 ? 1 : 0) | 1) != 0;
        }
      }
      bool flag3 = flag1 | flag2;
      if (this.panel.Active && this.lookAtThisThingButton != null)
      {
        this.lookAtThisThingButton.UpdateLookAtThisThingButton(player, DeltaTime, offset + this.Location);
        if (flag3)
          this.lookAtThisThingButton.OnButtonDestroy();
      }
      return flag3;
    }

    public void DrawEmployeeViewManager(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.Location;
      if (!this.hidemainpanel)
      {
        this.panel.DrawBigBrownPanel(offset);
        if (this.lookAtThisThingButton != null)
          this.lookAtThisThingButton.DrawLookAtThisThingButton(offset, AssetContainer.pointspritebatchTop05);
        int employeetype = (int) this.employeetype;
        this.genericPanels.DrawGenericEmployeePanels(spritebatch, offset);
        this.panel.DrawDarkOverlay(offset, spritebatch);
      }
      else if (this.lookAtThisThingButton != null)
        this.lookAtThisThingButton.DrawLookAtThisThingButton(offset, AssetContainer.pointspritebatchTop05);
      if (this.popup == null)
        return;
      this.popup.DrawActionPopUpManager(AssetContainer.pointspritebatchTop05);
    }
  }
}
