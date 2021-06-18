// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.AnimalDeliveryUI.AnimalDeliveryList
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Animals;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.AnimalDeliveryUI
{
  internal class AnimalDeliveryList
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private List<CustomerFrame> emptyslots;
    private Vector2 framescale;
    private List<AnimalDeliveryListEntry> entries;
    private Vector2 entrySize;
    private int maxrows;
    private float scrolloffset;
    public bool maxedup = true;
    public bool maxeddown = true;

    public AnimalDeliveryList(
      float basescale_,
      List<AnimalOrder> animalsonorderforthispen,
      int maxrows_ = 8)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.maxrows = maxrows_;
      this.entries = new List<AnimalDeliveryListEntry>();
      for (int index1 = 0; index1 < animalsonorderforthispen.Count; ++index1)
      {
        for (int index2 = 0; index2 < animalsonorderforthispen[index1].incominganaimals.Count; ++index2)
          this.entries.Add(new AnimalDeliveryListEntry(this.basescale, animalsonorderforthispen[index1].incominganaimals[index2], animalsonorderforthispen[index1], true));
      }
      this.entrySize = this.entries.Count == 0 ? this.uiscale.ScaleVector2(new Vector2(260f, 30f)) : this.entries[0].GetSize();
      this.framescale.X = this.entrySize.X + 2f * defaultBuffer.X;
      this.framescale.Y += (float) this.maxrows * this.entrySize.Y;
      this.framescale.Y += (float) (this.maxrows - 1) * 0.5f * defaultBuffer.Y;
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      Vector2 vector2 = new Vector2();
      vector2.X = 0.0f;
      vector2.Y = -0.5f * this.framescale.Y;
      foreach (AnimalDeliveryListEntry entry in this.entries)
      {
        entry.location = vector2;
        entry.location.Y += 0.5f * entry.GetSize().Y;
        vector2.Y += entry.GetSize().Y + 0.5f * defaultBuffer.Y;
      }
      this.emptyslots = new List<CustomerFrame>();
      if (this.entries.Count >= this.maxrows)
        return;
      for (int index = 0; index < this.maxrows - this.entries.Count; ++index)
      {
        CustomerFrame customerFrame = new CustomerFrame(this.entrySize, true, this.basescale);
        customerFrame.location = vector2;
        customerFrame.location.Y += 0.5f * this.entrySize.Y;
        customerFrame.SetAlphaed();
        this.emptyslots.Add(customerFrame);
        vector2.Y += this.entrySize.Y + 0.5f * defaultBuffer.Y;
      }
    }

    public Vector2 GetSize() => this.framescale;

    public void UpdateScroller(Player player)
    {
      int num = Math.Max(0, this.entries.Count - this.maxrows);
      this.scrolloffset += this.uiscale.ScaleY(0.24f * player.inputmap.momentumwheel.MovementThisFrame);
      if ((double) this.scrolloffset > 0.0)
        this.scrolloffset = 0.0f;
      if ((double) this.scrolloffset >= (double) -num * ((double) this.entrySize.Y + 0.5 * (double) this.uiscale.DefaultBuffer.Y))
        return;
      this.scrolloffset = (float) -num * (this.entrySize.Y + 0.5f * this.uiscale.DefaultBuffer.Y);
    }

    public bool UpdateAnimalDeliveryList(
      Player player,
      Vector2 offset,
      out IntakePerson animalinfo,
      out AnimalOrder orderinfo,
      float DeltaTime)
    {
      offset += this.location;
      offset.Y += this.scrolloffset;
      bool flag = false;
      animalinfo = (IntakePerson) null;
      orderinfo = (AnimalOrder) null;
      this.maxedup = false;
      this.maxeddown = false;
      int num = Math.Max(0, this.entries.Count - this.maxrows);
      if ((double) this.scrolloffset > -0.00999999977648258)
        this.maxedup = true;
      if ((double) this.scrolloffset < (double) -num * ((double) this.entrySize.Y + 0.5 * (double) this.uiscale.DefaultBuffer.Y) + 0.00999999977648258)
        this.maxeddown = true;
      foreach (AnimalDeliveryListEntry entry in this.entries)
      {
        bool disableinput_ = (double) this.scrolloffset + (double) entry.location.Y < -0.5 * (double) this.framescale.Y || (double) this.scrolloffset + (double) entry.location.Y > 0.5 * (double) this.framescale.Y;
        if (entry.UpdateAnimalDeliveryListEntry(player, offset, DeltaTime, disableinput_))
        {
          animalinfo = entry.AnimalInfo;
          orderinfo = entry.Ref_AnimalsOnOrder;
        }
      }
      return flag;
    }

    public void DrawAnimalDeliveryList(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      offset.Y += this.scrolloffset;
      foreach (AnimalDeliveryListEntry entry in this.entries)
      {
        if ((double) this.scrolloffset + (double) entry.location.Y > -0.5 * (double) this.framescale.Y - 0.5 * (double) entry.GetSize().Y && (double) this.scrolloffset + (double) entry.location.Y < 0.5 * (double) this.framescale.Y + 0.5 * (double) entry.GetSize().Y)
          entry.DrawAnimalDeliveryListEntry(spritebatch, offset);
      }
      foreach (CustomerFrame emptyslot in this.emptyslots)
        emptyslot.DrawCustomerFrame(offset, spritebatch);
    }
  }
}
