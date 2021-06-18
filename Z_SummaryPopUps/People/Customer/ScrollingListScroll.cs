// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.ScrollingListScroll
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
  internal class ScrollingListScroll
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private List<CustomerFrame> emptyslots;
    private Vector2 framescale;
    private Vector2 pad;
    private List<ScrollingListEntry> entries;
    private Vector2 entrysize;
    private float betweenpad;
    private bool needresize;
    private int maxrows;
    private float scrolloffset;
    public bool maxedup = true;
    public bool maxeddown = true;

    public ScrollingListScroll(
      float basescale_,
      int maxrows_ = 8,
      bool alternatecolours = false,
      float betweenPadMultiplier = 0.0f)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.pad = this.scalehelper.DefaultBuffer;
      this.maxrows = maxrows_;
      this.betweenpad = betweenPadMultiplier * this.pad.Y;
      this.entries = new List<ScrollingListEntry>();
      this.needresize = true;
    }

    public void SetEmptyEntrySize(Vector2 size) => this.entrysize = size;

    public void SizeAndPosition()
    {
      this.framescale = new Vector2();
      foreach (ScrollingListEntry entry in this.entries)
      {
        this.framescale.Y += entry.GetSize().Y;
        this.framescale.X = Math.Max(this.framescale.X, entry.GetSize().X);
      }
      this.emptyslots = new List<CustomerFrame>();
      for (int count = this.entries.Count; count < this.maxrows; ++count)
      {
        CustomerFrame customerFrame = new CustomerFrame(this.entrysize, true, this.basescale);
        customerFrame.SetAlphaed();
        this.emptyslots.Add(customerFrame);
      }
      this.framescale.Y += (float) (this.maxrows - this.entries.Count) * this.entrysize.Y;
      this.framescale.Y += (float) (this.maxrows - 1) * this.betweenpad;
      this.framescale.X = Math.Max(this.framescale.X, this.entrysize.X);
      this.framescale.X += 2f * this.pad.X;
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      Vector2 vector2 = new Vector2();
      vector2.X = 0.0f;
      vector2.Y = -0.5f * this.framescale.Y;
      foreach (ScrollingListEntry entry in this.entries)
      {
        entry.location = vector2;
        entry.location.Y += 0.5f * entry.GetSize().Y;
        vector2.Y += entry.GetSize().Y + this.betweenpad;
      }
      if (this.entries.Count >= this.maxrows)
        return;
      foreach (CustomerFrame emptyslot in this.emptyslots)
      {
        emptyslot.location = vector2;
        emptyslot.location.Y += 0.5f * this.entrysize.Y;
        vector2.Y += this.entrysize.Y + this.betweenpad;
      }
    }

    public void Add(ScrollingListEntry entry)
    {
      int num = this.entries.Count % 2;
      this.entries.Add(entry);
      this.entries[this.entries.Count - 1].DarkerFrame = false;
      this.needresize = true;
    }

    public Vector2 GetSize()
    {
      if (this.needresize)
      {
        this.SizeAndPosition();
        this.needresize = false;
      }
      return this.framescale;
    }

    public void UpdateScroller(Player player)
    {
      int num = Math.Max(0, this.entries.Count - this.maxrows);
      this.scrolloffset += this.scalehelper.ScaleY(0.24f * player.inputmap.momentumwheel.MovementThisFrame);
      if ((double) this.scrolloffset > 0.0)
        this.scrolloffset = 0.0f;
      if ((double) this.scrolloffset >= (double) -num * ((double) this.entrysize.Y + 0.5 * (double) this.scalehelper.DefaultBuffer.Y))
        return;
      this.scrolloffset = (float) -num * (this.entrysize.Y + 0.5f * this.scalehelper.DefaultBuffer.Y);
    }

    public bool UpdateScrollingListScroll(Player player, Vector2 offset, float DeltaTime)
    {
      if (this.needresize)
      {
        this.SizeAndPosition();
        this.needresize = false;
      }
      offset += this.location;
      offset.Y += this.scrolloffset;
      bool flag1 = false;
      this.maxedup = false;
      this.maxeddown = false;
      int num = Math.Max(0, this.entries.Count - this.maxrows);
      if ((double) this.scrolloffset > -0.00999999977648258)
        this.maxedup = true;
      if ((double) this.scrolloffset < (double) -num * ((double) this.entrysize.Y + 0.5 * (double) this.scalehelper.DefaultBuffer.Y) + 0.00999999977648258)
        this.maxeddown = true;
      foreach (ScrollingListEntry entry in this.entries)
      {
        bool flag2 = (double) this.scrolloffset + (double) entry.location.Y < -0.5 * (double) this.framescale.Y || (double) this.scrolloffset + (double) entry.location.Y > 0.5 * (double) this.framescale.Y;
        entry.DisableInput = flag2;
        entry.UpdateScrollingListEntry(player, offset, DeltaTime);
      }
      return flag1;
    }

    public void DrawScrollingListScroll(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      offset.Y += this.scrolloffset;
      foreach (ScrollingListEntry entry in this.entries)
      {
        if ((double) this.scrolloffset + (double) entry.location.Y > -0.5 * (double) this.framescale.Y - 0.5 * (double) entry.GetSize().Y && (double) this.scrolloffset + (double) entry.location.Y < 0.5 * (double) this.framescale.Y + 0.5 * (double) entry.GetSize().Y)
          entry.DrawScrollingListEntry(spritebatch, offset);
      }
      foreach (CustomerFrame emptyslot in this.emptyslots)
        emptyslot.DrawCustomerFrame(offset, spritebatch);
    }
  }
}
