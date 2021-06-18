// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerPanelsManagers.StrikePanelsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.CustomerPanelsManagers
{
  internal class StrikePanelsManager
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scaleHelper;
    private Vector2 pad;
    private CustomerFrame frame;
    private CustomerActionList actions;
    private VIPInfo info;
    private Vector2 framescale;

    public StrikePanelsManager(WalkingPerson person, float basescale_)
    {
      this.basescale = basescale_;
      this.scaleHelper = new UIScaleHelper(this.basescale);
      this.pad = this.scaleHelper.DefaultBuffer;
      this.actions = new CustomerActionList(this.basescale);
      CustomerViewManager.GetCustomerTypeOfThisPerson(person);
      this.info = (VIPInfo) new StrikeInfo(person, this.basescale);
      this.actions.AddAction(CustomerActionType.Security);
      this.actions.AddAction(CustomerActionType.Demands);
      this.actions.AddAction(CustomerActionType.Fire);
      this.actions.AddAction(CustomerActionType.Negotiate);
      if (this.actions.Count < 1)
        this.actions = (CustomerActionList) null;
      this.framescale = new Vector2();
      this.framescale.Y = this.info.GetSize().Y;
      this.framescale.X = this.info.GetSize().X;
      if (this.actions != null)
      {
        this.framescale.X += this.actions.GetSize().X + this.pad.X;
        this.framescale.Y = Math.Max(this.actions.GetSize().Y, this.framescale.Y);
        this.actions.ForceToHeight(this.framescale.Y);
      }
      this.frame = new CustomerFrame(this.framescale, true, this.basescale);
      Vector2 vector2 = -0.5f * this.framescale;
      this.info.location = vector2 + 0.5f * this.info.GetSize();
      vector2.X += this.info.GetSize().X + this.pad.X;
      if (this.actions == null)
        return;
      this.actions.location = vector2 + 0.5f * this.actions.GetSize();
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateStrikePanelsManager(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out CustomerActionType OUTactionType)
    {
      offset += this.location;
      OUTactionType = CustomerActionType.None;
      this.info.UpdateVIPInfo(player, offset, DeltaTime);
      return this.actions != null && this.actions.UpdateCustomerActionsList(player, offset, DeltaTime, out OUTactionType);
    }

    public void DrawStrikePanelsManager(SpriteBatch spriteBatch, Vector2 offset)
    {
      offset += this.location;
      this.info.DrawVIPInfo(spriteBatch, offset);
      if (this.actions == null)
        return;
      this.actions.DrawCustomerActionsList(spriteBatch, offset);
    }
  }
}
