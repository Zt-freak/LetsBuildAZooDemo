// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.LabelledCheckboxList
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
  internal class LabelledCheckboxList
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private Vector2 framescale;
    private Vector2 pad;
    private List<LabelledCheckbox> checkboxes;
    private bool calculate;

    public int Count => this.checkboxes.Count;

    public LabelledCheckboxList(float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.pad = this.scalehelper.DefaultBuffer;
      this.checkboxes = new List<LabelledCheckbox>();
    }

    public Vector2 GetSize()
    {
      if (this.calculate)
      {
        this.CalculateSizeAndPosition();
        this.calculate = false;
      }
      return this.framescale;
    }

    private void CalculateSizeAndPosition()
    {
      this.framescale = Vector2.Zero;
      foreach (LabelledCheckbox checkbox in this.checkboxes)
      {
        this.framescale.X = Math.Max(this.framescale.X, checkbox.GetSize().X);
        this.framescale.Y += checkbox.GetSize().Y + this.pad.Y;
      }
      if (this.checkboxes.Count > 0)
        this.framescale.Y -= this.pad.Y;
      Vector2 vector2 = -0.5f * this.framescale;
      foreach (LabelledCheckbox checkbox in this.checkboxes)
      {
        checkbox.location = vector2 + 0.5f * checkbox.GetBoxSize();
        vector2.Y += checkbox.GetBoxSize().Y + this.pad.Y;
      }
    }

    public int AddCheckbox(string label)
    {
      this.calculate = true;
      this.checkboxes.Add(new LabelledCheckbox(label, true, this.basescale));
      return this.checkboxes.Count - 1;
    }

    public bool GetTicked(int index) => this.checkboxes[index].IsTicked;

    public int UpdateLabelledCheckboxList(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      if (this.calculate)
      {
        this.CalculateSizeAndPosition();
        this.calculate = false;
      }
      int num = -1;
      for (int index = 0; index < this.checkboxes.Count; ++index)
      {
        if (this.checkboxes[index].UpdateLabelledCheckbox(player, offset, DeltaTime))
        {
          num = index;
          break;
        }
      }
      return num;
    }

    public void DrawLabelledCheckboxList(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      foreach (LabelledCheckbox checkbox in this.checkboxes)
        checkbox.DrawLabelledCheckbox(spritebatch, offset);
    }
  }
}
