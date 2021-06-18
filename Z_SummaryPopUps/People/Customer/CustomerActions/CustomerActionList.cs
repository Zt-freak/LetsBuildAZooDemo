// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.CustomerActionList
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions
{
  internal class CustomerActionList
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private Vector2 framescale;
    private float forceheight;
    private bool useFrame;

    public List<CustomerAction> customerActions { get; private set; }

    public int Count => this.customerActions.Count;

    public CustomerActionList(float basescale_, bool useFrame_ = true)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.useFrame = useFrame_;
      this.customerActions = new List<CustomerAction>();
      this.CalculateFrame();
    }

    public void CalculateFrame()
    {
      this.framescale = new Vector2();
      if (this.customerActions.Count < 1)
        return;
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.framescale = new Vector2();
      foreach (CustomerAction customerAction in this.customerActions)
      {
        this.framescale.X = Math.Max(this.framescale.X, customerAction.GetSize().X);
        this.framescale.Y += customerAction.GetSize().Y + 0.5f * defaultBuffer.Y;
      }
      if (this.useFrame)
        this.framescale += 2f * defaultBuffer;
      this.framescale.Y -= 0.5f * defaultBuffer.Y;
      if ((double) this.framescale.Y < (double) this.forceheight)
        this.framescale.Y = this.forceheight;
      float num = -0.5f * this.framescale.Y;
      if (this.useFrame)
        num += defaultBuffer.Y;
      foreach (CustomerAction customerAction in this.customerActions)
      {
        customerAction.location.Y = num + 0.5f * customerAction.GetSize().Y;
        num += customerAction.GetSize().Y + 0.5f * defaultBuffer.Y;
      }
      this.frame = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));
    }

    public void AddAction(CustomerActionType actiontype, bool active = true)
    {
      this.customerActions.Add(new CustomerAction(actiontype, this.basescale, active));
      this.CalculateFrame();
    }

    public void LockThisAction(
      CustomerActionType actionType,
      bool isActive,
      bool printBetaLocktext)
    {
      for (int index = 0; index < this.customerActions.Count; ++index)
      {
        if (this.customerActions[index].actiontype == actionType)
        {
          this.customerActions[index].Active = isActive;
          if (!printBetaLocktext)
            break;
          this.customerActions[index].LockForBeta();
          break;
        }
      }
    }

    public void ForceToHeight(float height)
    {
      this.forceheight = height;
      this.CalculateFrame();
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateCustomerActionsList(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out CustomerActionType OUTactiontype)
    {
      offset += this.location;
      bool flag = false;
      OUTactiontype = CustomerActionType.None;
      foreach (CustomerAction customerAction in this.customerActions)
      {
        if (customerAction.UpdateCustomerAction(player, offset, DeltaTime, out OUTactiontype))
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    public void DrawCustomerActionsList(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      if (this.useFrame)
        this.frame.DrawCustomerFrame(offset, spritebatch);
      foreach (CustomerAction customerAction in this.customerActions)
        customerAction.DrawCustomerAction(spritebatch, offset);
    }
  }
}
