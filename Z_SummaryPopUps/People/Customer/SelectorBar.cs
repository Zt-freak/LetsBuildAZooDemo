// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.SelectorBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class SelectorBar
  {
    private static Rectangle barrect = new Rectangle(885, 542, 9, 4);
    private float basescale;
    private UIScaleHelper uiscale;
    private Vector2 framescale;
    private CustomerFrame frame;
    public Vector2 location;
    private List<SelectorBarButton> buttons;
    private List<Vector2> dashlocs;
    private Vector2 dashsize;
    private GameObject bardash;
    private bool calculate;
    private int selected = -1;

    public int Selected
    {
      get => this.selected;
      set => this.SetSelected(value);
    }

    public SelectorBar(float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.calculate = true;
      this.bardash = new GameObject();
      this.bardash.DrawRect = SelectorBar.barrect;
      this.bardash.SetDrawOriginToCentre();
      this.bardash.SetAllColours(1f, 0.9607843f, 0.8352941f);
      this.dashsize = this.uiscale.ScaleVector2(new Vector2((float) (this.bardash.DrawRect.Width + 9), (float) this.bardash.DrawRect.Height));
      this.buttons = new List<SelectorBarButton>();
      this.dashlocs = new List<Vector2>();
    }

    public void Add(BTNColour colour = BTNColour.White)
    {
      this.buttons.Add(new SelectorBarButton(this.basescale, colour));
      this.calculate = true;
    }

    public void CalculateScaleAndPosition()
    {
      Vector2 zero = Vector2.Zero;
      this.framescale = Vector2.Zero;
      for (int index = 0; index < this.buttons.Count - 1; ++index)
      {
        this.buttons[index].location = zero;
        this.buttons[index].location.X += 0.5f * this.buttons[index].GetSize().X;
        zero.X += this.buttons[index].GetSize().X;
        this.framescale.X += this.buttons[index].GetSize().X;
        this.framescale.Y = Math.Max(this.framescale.Y, this.buttons[index].GetSize().Y);
        this.dashlocs.Add(new Vector2(zero.X + 0.5f * this.dashsize.X, zero.Y));
        zero.X += this.dashsize.X;
        this.framescale.X += this.dashsize.X;
      }
      this.buttons[this.buttons.Count - 1].location = zero;
      this.buttons[this.buttons.Count - 1].location.X += 0.5f * this.buttons[this.buttons.Count - 1].GetSize().X;
      this.framescale.X += this.buttons[this.buttons.Count - 1].GetSize().X;
      foreach (SelectorBarButton button in this.buttons)
        button.location.X -= 0.5f * this.framescale.X;
      for (int index = 0; index < this.dashlocs.Count; ++index)
        this.dashlocs[index] = new Vector2(this.dashlocs[index].X - 0.5f * this.framescale.X, this.dashlocs[index].Y);
      this.frame = new CustomerFrame(this.framescale, true, this.basescale);
    }

    public Vector2 GetSize()
    {
      if (this.calculate)
      {
        this.CalculateScaleAndPosition();
        this.calculate = false;
      }
      return this.framescale;
    }

    public bool UpdateSelectorBar(Player player, Vector2 offset, float DeltaTime)
    {
      if (this.calculate)
      {
        this.CalculateScaleAndPosition();
        this.calculate = false;
      }
      offset += this.location;
      bool flag = false;
      for (int index = 0; index < this.buttons.Count; ++index)
      {
        if (this.buttons[index].UpdateSelectorBarButton(player, offset, DeltaTime))
        {
          this.SetSelected(index);
          break;
        }
      }
      return flag;
    }

    public void SetSelected(int index)
    {
      if (index >= this.buttons.Count)
        return;
      this.selected = index;
      for (int index1 = 0; index1 < this.buttons.Count; ++index1)
        this.buttons[index1].Selected = index1 == this.selected;
    }

    public void DrawSelectorBar(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      CustomerFrame frame = this.frame;
      foreach (Vector2 dashloc in this.dashlocs)
        this.bardash.Draw(spritebatch, AssetContainer.SpriteSheet, offset + dashloc, this.basescale, 1f);
      foreach (SelectorBarButton button in this.buttons)
        button.DrawSelectorBarButton(spritebatch, offset);
    }
  }
}
