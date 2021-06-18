// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Employee.ShopkeeperInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Employee
{
  internal class ShopkeeperInfo : EmployeeInfo
  {
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private MiniHeading heading;
    private ZGenericText servedtext;
    private ZGenericText servetimetext;
    private ZGenericText idletimetext;
    private ZGenericText breaktimetext;
    private ZGenericText otherstimetext;
    private float customersserved;
    private float servetime;
    private float idletime;
    private float breaktime;
    private float otherstime;

    public ShopkeeperInfo(WalkingPerson person, float basescale_, float forceToThisWidth = -1f)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      QuickEmployeeDescription quickemplyeedescription = person.simperson.Ref_Employee.quickemplyeedescription;
      this.customersserved = (float) quickemplyeedescription.CustomersServed;
      this.servetime = quickemplyeedescription.TimeSpentServing;
      this.breaktime = quickemplyeedescription.TimeSpentOnBreaks;
      this.heading = new MiniHeading(Vector2.Zero, "Info", 1f, this.basescale);
      this.servedtext = new ZGenericText("Customers served: " + (object) this.customersserved, this.basescale, false);
      this.servetimetext = new ZGenericText(this.servetime.ToString("f1") + "% time serving customers", this.basescale, false);
      this.idletimetext = new ZGenericText(this.idletime.ToString("f1") + "% time idle", this.basescale, false);
      this.breaktimetext = new ZGenericText(this.breaktime.ToString("f1") + "% time on break", this.basescale, false);
      this.otherstimetext = new ZGenericText(this.otherstime.ToString("f1") + "% time on other things", this.basescale, false);
      this.framescale = 2f * defaultBuffer;
      this.framescale.Y += this.heading.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.framescale.Y += this.servedtext.GetSize().Y;
      this.framescale.Y += this.servetimetext.GetSize().Y;
      this.framescale.Y += this.idletimetext.GetSize().Y;
      this.framescale.Y += this.breaktimetext.GetSize().Y;
      this.framescale.Y += this.otherstimetext.GetSize().Y;
      this.framescale.X += Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(this.servedtext.GetSize().X, this.servetimetext.GetSize().X), this.idletimetext.GetSize().X), this.breaktimetext.GetSize().X), this.otherstimetext.GetSize().X), this.otherstimetext.GetSize().X);
      this.framescale.X = Math.Max(this.framescale.X, forceToThisWidth);
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.heading.SetTextPosition(this.framescale);
      Vector2 vector2 = -0.5f * this.framescale + defaultBuffer;
      vector2.Y += this.heading.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.servedtext.vLocation = vector2;
      vector2.Y += this.servedtext.GetSize().Y;
      this.servetimetext.vLocation = vector2;
      vector2.Y += this.servetimetext.GetSize().Y;
      this.idletimetext.vLocation = vector2;
      vector2.Y += this.idletimetext.GetSize().Y;
      this.breaktimetext.vLocation = vector2;
      vector2.Y += this.breaktimetext.GetSize().Y;
      this.otherstimetext.vLocation = vector2;
      vector2.Y += this.otherstimetext.GetSize().Y;
    }

    public override Vector2 GetSize() => this.framescale;

    public override bool UpdateEmployeeInfo(Player player, Vector2 offset, float DeltaTime) => false;

    public override void DrawEmployeeInfo(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.heading.DrawMiniHeading(offset, spritebatch);
      this.servedtext.DrawZGenericText(offset, spritebatch);
      this.servetimetext.DrawZGenericText(offset, spritebatch);
      this.idletimetext.DrawZGenericText(offset, spritebatch);
      this.breaktimetext.DrawZGenericText(offset, spritebatch);
      this.otherstimetext.DrawZGenericText(offset, spritebatch);
    }
  }
}
