// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.SelectCell.Orders.Rows.OrderInfoHeader
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.OverWorld.SelectCell.Orders.Rows
{
  internal class OrderInfoHeader
  {
    public Vector2 location;
    private Vector2 size;
    private LabelledCheckbox checkBox;
    private List<ZGenericText> headerText;

    public bool CheckboxTicked
    {
      get => this.checkBox.IsTicked;
      set
      {
        this.checkBox.ForceSetTickStatus(value);
        this.SetText();
      }
    }

    public OrderInfoHeader(float BaseScale, float[] widths)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      defaultBuffer.X *= 0.5f;
      float num1 = ((IEnumerable<float>) widths).Sum();
      float num2 = (float) (-(double) num1 * 0.5);
      this.headerText = new List<ZGenericText>();
      for (int index = 0; index < 5; ++index)
      {
        float num3 = num2 + widths[index] * 0.5f;
        ZGenericText zgenericText = new ZGenericText(this.GetStringForColumn((OrderInfoColumn) index), BaseScale);
        zgenericText.vLocation.X = num3;
        num2 = num3 + widths[index] * 0.5f;
        this.size.Y = Math.Max(zgenericText.GetSize().Y, this.size.Y);
        this.headerText.Add(zgenericText);
      }
      this.checkBox = new LabelledCheckbox("Select All", true, BaseScale);
      this.checkBox.location.X = (float) (-(double) num1 * 0.5);
      this.checkBox.location.X += this.checkBox.GetBoxSize().X * 0.5f;
      this.checkBox.location.X += defaultBuffer.X;
      this.size.X = num1;
      this.size.Y = Math.Max(this.size.Y, this.checkBox.GetSize().Y);
    }

    private string GetStringForColumn(OrderInfoColumn column)
    {
      switch (column)
      {
        case OrderInfoColumn.Animal:
          return "";
        case OrderInfoColumn.Habitat:
          return "Habitat";
        case OrderInfoColumn.Threat:
          return "Threat";
        case OrderInfoColumn.Territory:
          return "Territory";
        case OrderInfoColumn.Seperation:
          return "Separation";
        default:
          return "COLUMN HEADER";
      }
    }

    public Vector2 GetSize() => this.size;

    public bool UpdateOrderInfoHeader(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (!this.checkBox.UpdateLabelledCheckbox(player, offset, DeltaTime))
        return false;
      this.SetText();
      return true;
    }

    private void SetText()
    {
      if (this.checkBox.IsTicked)
        this.checkBox.SetNewText("Deselect All");
      else
        this.checkBox.SetNewText("Select All");
    }

    public void DrawOrderInfoHeader(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.checkBox.DrawLabelledCheckbox(spriteBatch, offset);
      for (int index = 0; index < this.headerText.Count; ++index)
        this.headerText[index].DrawZGenericText(offset, spriteBatch);
    }
  }
}
