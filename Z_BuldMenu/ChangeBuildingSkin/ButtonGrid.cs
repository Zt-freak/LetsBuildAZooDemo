// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.ChangeBuildingSkin.ButtonGrid
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuldMenu.ChangeBuildingSkin
{
  internal class ButtonGrid
  {
    public Vector2 location;
    private int numcols;
    private List<ZGenericButton> buttons;
    private bool dirty;
    private float basescale;
    private UIScaleHelper uiscale;
    private Vector2 totalsize;
    private CustomerFrame frame;

    public ButtonGrid(int columns, float basescale_)
    {
      this.numcols = columns;
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.buttons = new List<ZGenericButton>();
      this.dirty = false;
    }

    public void Add(ZGenericButton button)
    {
      this.buttons.Add(button);
      this.dirty = true;
    }

    public int Count => this.buttons.Count;

    public ZGenericButton this[int key] => this.buttons[key];

    public void PositionButtons()
    {
      Vector2 zero = Vector2.Zero;
      foreach (ZGenericButton button in this.buttons)
      {
        if ((double) zero.X < (double) button.GetSize().X)
          zero.X = button.GetSize().X;
        if ((double) zero.Y < (double) button.GetSize().Y)
          zero.Y = button.GetSize().Y;
      }
      Vector2 vector2_1 = new Vector2(this.uiscale.GetDefaultXBuffer(), this.uiscale.GetDefaultYBuffer());
      Vector2 vector2_2 = vector2_1;
      int num = 0;
      int index1 = 0;
      while (index1 < this.buttons.Count)
      {
        ++num;
        for (int index2 = 0; index2 < this.numcols; ++index2)
        {
          ZGenericButton button = this.buttons[index1];
          button.location = vector2_2;
          button.location += 0.5f * zero;
          vector2_2.X += zero.X + vector2_1.X;
          ++index1;
          if (index1 >= this.buttons.Count)
            break;
        }
        vector2_2.X = vector2_1.X;
        vector2_2.Y += zero.Y + vector2_1.Y;
      }
      this.totalsize = Vector2.Zero;
      this.totalsize.X = (float) ((double) this.numcols * (double) zero.X + (double) (this.numcols + 1) * (double) vector2_1.X);
      this.totalsize.Y = (float) ((double) num * (double) zero.Y + (double) (num + 1) * (double) vector2_1.Y);
      foreach (ZGenericButton button in this.buttons)
        button.location += -0.5f * this.totalsize;
      this.frame = new CustomerFrame(this.totalsize, BaseScale: this.basescale);
    }

    public Vector2 GetSize() => this.totalsize;

    public int UpdateButtonGrid(Player player, Vector2 offset, float DeltaTime)
    {
      int num = -1;
      if (this.dirty)
      {
        this.PositionButtons();
        this.dirty = false;
      }
      for (int index = 0; index < this.buttons.Count; ++index)
      {
        if (this.buttons[index].UpdateZGenericButton(player, offset + this.location, DeltaTime))
        {
          num = index;
          break;
        }
      }
      return num;
    }

    public void DrawButtonGrid(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      foreach (ZGenericButton button in this.buttons)
      {
        button.DrawZGenericButton(spritebatch, offset);
        button.DrawButtonHighlight(spritebatch, offset);
      }
    }
  }
}
