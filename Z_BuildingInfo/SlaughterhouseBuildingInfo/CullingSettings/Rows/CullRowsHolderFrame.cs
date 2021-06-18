// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.Rows.CullRowsHolderFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.Rows
{
  internal class CullRowsHolderFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private CullAnimalRows rows;
    private RowSegmentRectangle topMask;
    private RowSegmentRectangle bottomMask;
    private SimpleTextHandler desc;

    public CullRowsHolderFrame(Player player, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.rows = new CullAnimalRows(player, BaseScale);
      Vector2 sizeOfOneRow = this.rows.GetSizeOfOneRow();
      this.topMask = new RowSegmentRectangle(sizeOfOneRow.X + defaultBuffer.X, sizeOfOneRow.Y, ColourData.Z_FrameMidBrown, 1f);
      this.topMask.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.bottomMask = new RowSegmentRectangle(sizeOfOneRow.X + defaultBuffer.X, sizeOfOneRow.Y, ColourData.Z_FrameMidBrown, 1f);
      this.bottomMask.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.desc = new SimpleTextHandler("Employees here will cull animals from pens in this building's zones according to these specified settings.", sizeOfOneRow.X - defaultBuffer.X * 2f, true, BaseScale, AutoComplete: true);
      this.desc.SetAllColours(ColourData.Z_Cream);
      Vector2 _VSCale = new Vector2(sizeOfOneRow.X + defaultBuffer.X * 2f, uiScaleHelper.ScaleY(400f));
      this.desc.Location.Y = (float) ((double) sizeOfOneRow.Y * 0.5 + (double) this.desc.GetHeightOfOneLine() * 0.5 - (double) this.desc.GetHeightOfParagraph() * 0.5);
      this.topMask.vLocation.Y = sizeOfOneRow.Y;
      this.rows.location.Y = sizeOfOneRow.Y;
      this.rows.AddScroll(_VSCale.Y - sizeOfOneRow.Y * 2f);
      this.bottomMask.vLocation.Y = _VSCale.Y - sizeOfOneRow.Y;
      this.customerFrame = new CustomerFrame(_VSCale, CustomerFrameColors.Brown, BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.rows.location.Y += vector2.Y;
      this.topMask.vLocation.Y += vector2.Y;
      this.bottomMask.vLocation.Y += vector2.Y;
      this.desc.Location.Y += vector2.Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public AnimalType UpdateCullRowsHolderFrame(
      Player player,
      float DeltaTime,
      Vector2 offset)
    {
      offset += this.location;
      return this.rows.UpdateCullAnimalRows(player, DeltaTime, offset);
    }

    public void DrawCullRowsHolderFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.rows.DrawCullAnimalRows(offset, spriteBatch);
      this.topMask.DrawRowSegmentRectangle(offset, spriteBatch);
      this.bottomMask.DrawRowSegmentRectangle(offset, spriteBatch);
      this.desc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
