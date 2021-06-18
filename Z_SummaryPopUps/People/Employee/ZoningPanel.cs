// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Employee.ZoningPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TinyZoo.Z_EditZone;
using TinyZoo.Z_Employees.WorkZonePane;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.Employee.Zoning;

namespace TinyZoo.Z_SummaryPopUps.People.Employee
{
  internal class ZoningPanel
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private TextButton button;
    private ZoningSelPanel zoningselect;
    private WalkingPerson walkingperson;

    public ZoningPanel(WalkingPerson walkingperson_, float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.walkingperson = walkingperson_;
      this.frame = new CustomerFrame(Vector2.Zero, BaseScale: this.basescale);
      this.frame.AddMiniHeading("Zoning");
      this.button = new TextButton(this.basescale, "Zone", 40f);
      this.framescale = 2f * defaultBuffer;
      this.framescale.Y += this.frame.GetMiniHeadingHeight();
      this.framescale += this.button.GetSize_True();
      this.frame.Resize(this.framescale);
      Vector2 vector2 = -0.5f * this.framescale + defaultBuffer;
      vector2.Y += this.frame.GetMiniHeadingHeight();
      this.button.vLocation = vector2 + 0.5f * this.button.GetSize_True();
    }

    public void ForceToThisWidth(float width)
    {
      this.framescale.X = Math.Max(this.framescale.X, width);
      this.frame.Resize(this.framescale);
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateZoningPanel(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false;
      if (this.button.UpdateTextButton(player, offset, DeltaTime))
      {
        OverWorldManager.zoneEditManager = new ZoneEditManager(this.walkingperson.simperson.Ref_Employee.workzoneinfo, player, TILETYPE.None, _employeetype: this.walkingperson.simperson.Ref_Employee.employeetype);
        OverWorldManager.overworldstate = OverWOrldState.EditZone;
        flag = ((flag ? 1 : 0) | 1) != 0;
      }
      if (OverWorldManager.zoneEditManager != null && this.walkingperson.simperson.Ref_Employee.workzoneinfo == null)
      {
        this.walkingperson.simperson.Ref_Employee.workzoneinfo = new WorkZoneInfo(5);
        this.walkingperson.simperson.Ref_Employee.workzoneinfo.workzonetype = Employees.GetThisEmplyeesWorkZoneType(this.walkingperson.simperson.Ref_Employee.employeetype);
      }
      return flag;
    }

    public void DrawZoningPanel(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.button.DrawTextButton(offset, 1f, spritebatch);
      if (this.zoningselect == null)
        return;
      this.zoningselect.DrawZoningSelPanel(Sengine.HalfReferenceScreenRes, AssetContainer.pointspritebatchTop05);
    }
  }
}
