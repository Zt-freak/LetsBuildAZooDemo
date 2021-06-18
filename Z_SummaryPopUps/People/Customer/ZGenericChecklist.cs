// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.ZGenericChecklist
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class ZGenericChecklist
  {
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private Vector2 framescale;
    private Vector2 pad;
    public Vector2 location;
    private bool useSpringfont1point5;
    private List<ZGenericChecklistItem> items;
    private bool needsUpdate;
    private bool drawWithFrame;

    public int Count => this.items.Count;

    public ZGenericChecklist(float basescale_, bool drawWithFrame_ = false, bool useSpringfont1point5_ = false)
    {
      this.drawWithFrame = drawWithFrame_;
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.pad = this.uiscale.DefaultBuffer;
      this.framescale = new Vector2();
      this.items = new List<ZGenericChecklistItem>();
      this.useSpringfont1point5 = useSpringfont1point5_;
    }

    public void SetTextColour(Vector3 colour)
    {
      foreach (ZGenericChecklistItem zgenericChecklistItem in this.items)
        zgenericChecklistItem.SetTextColour(colour);
    }

    public void UpdateLayout()
    {
      this.framescale = new Vector2();
      if (this.drawWithFrame)
        this.framescale = 2f * this.pad;
      foreach (ZGenericChecklistItem zgenericChecklistItem in this.items)
      {
        this.framescale.X = Math.Max(this.framescale.X, zgenericChecklistItem.GetSize().X + 2f * this.pad.X);
        this.framescale.Y += zgenericChecklistItem.GetSize().Y + 0.5f * this.pad.Y;
      }
      if (this.items.Count > 0)
        this.framescale.Y -= 0.5f * this.pad.Y;
      this.frame = new CustomerFrame(this.framescale, true, this.basescale);
      Vector2 vector2 = -0.5f * this.framescale;
      if (this.drawWithFrame)
        vector2 += this.pad;
      foreach (ZGenericChecklistItem zgenericChecklistItem in this.items)
      {
        zgenericChecklistItem.location = vector2 + 0.5f * zgenericChecklistItem.GetSize();
        vector2.Y += zgenericChecklistItem.GetSize().Y + 0.5f * this.pad.Y;
      }
    }

    public Vector2 GetSize()
    {
      if (this.needsUpdate)
      {
        this.UpdateLayout();
        this.needsUpdate = false;
      }
      return this.framescale;
    }

    public void Add(string text, bool completed = false, int useThisNum = -1)
    {
      this.items.Add(new ZGenericChecklistItem(text, completed, this.basescale, this.useSpringfont1point5, useThisNum));
      this.needsUpdate = true;
    }

    public bool IsComplete(int index) => this.items[index].IsComplete;

    public bool UpdateZGenericChecklist(float DeltaTime)
    {
      if (this.needsUpdate)
      {
        this.UpdateLayout();
        this.needsUpdate = false;
      }
      return false;
    }

    public void DrawZGenericChecklist(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      if (this.drawWithFrame)
        this.frame.DrawCustomerFrame(offset, spritebatch);
      foreach (ZGenericChecklistItem zgenericChecklistItem in this.items)
        zgenericChecklistItem.DrawZGenericChecklistItem(offset, spritebatch);
    }
  }
}
