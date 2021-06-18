// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.SelectCell.Orders.Rows.OrderSpaceSummaryRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.EmployeeView;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.OverWorld.SelectCell.Orders.Rows
{
  internal class OrderSpaceSummaryRow
  {
    public Vector2 location;
    private PerformanceTableRowFrame frame;
    private ZGenericText text;
    private ZGenericText value;

    public OrderSpaceSummaryRow(float BaseScale, float rowHeight, float[] widths)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.text = new ZGenericText("Total Remaining Space For Ideal Conditions", BaseScale, false, true, true);
      this.value = new ZGenericText("XX", BaseScale, _UseOnePointFiveFont: true);
      float[] numArray = new float[2];
      float num1 = ((IEnumerable<float>) widths).Sum();
      float num2 = num1 * -0.5f;
      for (int index = 0; index < widths.Length; ++index)
      {
        float num3 = num2 + widths[index] * 0.5f;
        if (index == 4)
        {
          numArray[0] = (float) ((double) num3 + (double) num1 * 0.5 - (double) widths[index] * 0.5);
          numArray[1] = num1 - numArray[0];
          this.text.vLocation.X = num3 - widths[index] * 0.5f - defaultBuffer.X;
          this.text.vLocation.Y -= this.text.GetSize().Y * 0.5f;
          this.value.vLocation.X = num3;
          break;
        }
        num2 = num3 + widths[index] * 0.5f;
      }
      this.frame = new PerformanceTableRowFrame(BaseScale, rowHeight, CustomerFrameColors.BlueWithLighterBlueBorder, true, numArray);
      this.SetData((PrisonZone) null);
    }

    public Vector2 GetSize() => this.frame.GetSize();

    public void SetData(PrisonZone prisonZone, int numberSelected = -1, float differenceInSpace = -1f)
    {
      if (prisonZone == null || numberSelected <= 0)
      {
        this.value.textToWrite = "-";
        this.frame.RemoveColumnColor(1);
      }
      else
      {
        this.value.textToWrite = differenceInSpace.ToString();
        if (prisonZone != null)
        {
          if ((double) differenceInSpace < 0.0)
            this.frame.ColorThisColumnRed(1, OrderAssignmentRowContainer.GetCellColouredMargin().X, OrderAssignmentRowContainer.GetCellColouredMargin().Y);
          else
            this.frame.ColorThisColumnGreen(1, OrderAssignmentRowContainer.GetCellColouredMargin().X, OrderAssignmentRowContainer.GetCellColouredMargin().Y);
        }
        else
          this.frame.RemoveColumnColor(1);
      }
    }

    public void DrawOrderSpaceSummaryRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.frame.DrawPerformanceTableRowFrame(offset, spriteBatch);
      this.text.DrawZGenericText(offset, spriteBatch);
      this.value.DrawZGenericText(offset, spriteBatch);
      this.frame.PostDrawPerformanceTableRowFrame(offset, spriteBatch);
    }
  }
}
