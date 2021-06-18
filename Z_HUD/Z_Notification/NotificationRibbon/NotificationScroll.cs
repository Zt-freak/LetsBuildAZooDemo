// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.NotificationScroll
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_HUD.Z_Notification.NotificationRibbon
{
  internal class NotificationScroll
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private Vector2 framescale;
    private Vector2 size;
    private List<RibbonEntry> ribbonEntries;
    private ButtHolder buttholder;
    private int maxrows;
    private float scrolloffset;
    public bool maxedup = true;
    public bool maxeddown = true;

    public NotificationScroll(
      List<RibbonEntry> ribbonEntries_,
      ButtHolder buttholder_,
      float basescale_,
      int maxrows_ = 6)
    {
      this.ribbonEntries = ribbonEntries_;
      this.maxrows = maxrows_;
      this.buttholder = buttholder_;
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.framescale = new Vector2();
      this.size = new Vector2();
      if (this.ribbonEntries != null && this.ribbonEntries.Count > 0)
      {
        this.size.X = this.ribbonEntries[0].GetSize().X;
        this.size.Y = this.ribbonEntries[0].GetSize().Y * (float) this.ribbonEntries.Count;
        this.framescale.X = this.ribbonEntries[0].GetSize().X;
        this.framescale.Y = this.ribbonEntries[0].GetSize().Y * (float) Math.Min(this.ribbonEntries.Count, this.maxrows);
      }
      Vector2 vector2 = new Vector2();
      vector2.X = 0.0f;
      vector2.Y = -0.5f * this.framescale.Y;
      foreach (RibbonEntry ribbonEntry in this.ribbonEntries)
      {
        ribbonEntry.location = vector2;
        ribbonEntry.location.X = 0.0f;
        ribbonEntry.location.Y += 0.5f * ribbonEntry.GetSize().Y;
        vector2.Y += ribbonEntry.GetSize().Y;
      }
    }

    public Vector2 GetSize() => this.framescale;

    public void Add(RibbonEntry entry) => this.ribbonEntries.Add(entry);

    public void UpdateScroller(Player player)
    {
      int num = Math.Max(0, this.ribbonEntries.Count - this.maxrows);
      this.scrolloffset += this.uiscale.ScaleY(0.3f * player.inputmap.momentumwheel.MovementThisFrame);
      if ((double) this.scrolloffset > 0.0)
        this.scrolloffset = 0.0f;
      if ((double) this.scrolloffset >= (double) -num * (double) this.ribbonEntries[0].GetSize().Y)
        return;
      this.scrolloffset = (float) -num * this.ribbonEntries[0].GetSize().Y;
    }

    public bool UpdateNotificationScroll(
      Player player,
      bool updatebuttons,
      bool dismissAll,
      Vector2 offset,
      float DeltaTime,
      out NotificationInfo notifcationpressed,
      out bool remakeRibbon)
    {
      offset += this.location;
      offset.Y += this.scrolloffset;
      remakeRibbon = false;
      notifcationpressed = (NotificationInfo) null;
      this.maxedup = false;
      this.maxeddown = false;
      int num = Math.Max(0, this.ribbonEntries.Count - this.maxrows);
      if ((double) this.scrolloffset > -0.00999999977648258)
        this.maxedup = true;
      if ((double) this.scrolloffset < (double) -num * (double) this.ribbonEntries[0].GetSize().Y + 0.00999999977648258)
        this.maxeddown = true;
      foreach (RibbonEntry ribbonEntry in this.ribbonEntries)
      {
        bool dismiss = false;
        bool disableInput = (double) this.scrolloffset + (double) ribbonEntry.location.Y < -0.5 * (double) this.framescale.Y || (double) this.scrolloffset + (double) ribbonEntry.location.Y > 0.5 * (double) this.framescale.Y;
        if (ribbonEntry.UpdateRibbonEntry(player, disableInput, DeltaTime, offset, out dismiss) & updatebuttons)
          notifcationpressed = ribbonEntry.notificationinfo;
        if (((0 | (dismiss ? 1 : 0) | (dismissAll ? 1 : 0)) & (updatebuttons ? 1 : 0)) != 0)
        {
          this.buttholder.notificationpackages.Remove(ribbonEntry.notificationpack);
          remakeRibbon = true;
        }
      }
      return notifcationpressed != null;
    }

    public void DrawNotificationScroll(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      offset.Y += this.scrolloffset;
      foreach (RibbonEntry ribbonEntry in this.ribbonEntries)
      {
        if ((double) this.scrolloffset + (double) ribbonEntry.location.Y > -0.5 * (double) this.framescale.Y - 0.5 * (double) ribbonEntry.GetSize().Y && (double) this.scrolloffset + (double) ribbonEntry.location.Y < 0.5 * (double) this.framescale.Y + 0.5 * (double) ribbonEntry.GetSize().Y)
          ribbonEntry.DrawRibbonEntry(spritebatch, offset);
      }
    }
  }
}
