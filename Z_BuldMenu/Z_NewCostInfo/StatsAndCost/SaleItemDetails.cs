// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.Z_NewCostInfo.StatsAndCost.SaleItemDetails
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Tile_Data;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_BuldMenu.Z_NewCostInfo.StatsAndCost
{
  internal class SaleItemDetails
  {
    private CustomerFrame customerframe;
    private SimpleTextHandler simpletext;
    private SimpleTextHandler Heading;
    public Vector2 Location;
    private List<SatisfactionBarAndText> satiationbars;

    public SaleItemDetails(
      TILETYPE buildingtype,
      Player player,
      float BaseScale,
      float PreMutipliedWidth)
    {
      this.Heading = new SimpleTextHandler("Product Details", PreMutipliedWidth, _Scale: BaseScale, _UseFontOnePointFive: true, AutoComplete: true);
      this.Heading.SetAllColours(ColourData.Z_Cream);
      float y1 = this.Heading.GetSize().Y;
      this.Heading.Location.X = PreMutipliedWidth * -0.5f;
      float num1 = y1 + BaseScale * 10f;
      this.simpletext = new SimpleTextHandler("Sells: Burgers.~Average Market Value: $1.20.~Average Profit Per Sale: $0.60", PreMutipliedWidth, _Scale: BaseScale, AutoComplete: true);
      this.simpletext.Location.X = PreMutipliedWidth * -0.5f;
      this.simpletext.SetAllColours(ColourData.Z_Cream);
      this.simpletext.Location.Y = num1;
      float num2 = num1 + this.simpletext.GetSize().Y + BaseScale * 10f;
      this.satiationbars = new List<SatisfactionBarAndText>();
      this.satiationbars.Add(new SatisfactionBarAndText("Hunger + 10%", 0.1f, BaseScale));
      float y2 = this.satiationbars[0].GetSize().Y;
      for (int index = 0; index < this.satiationbars.Count; ++index)
      {
        this.satiationbars[index].Location.Y = num2 + y2 * 0.5f;
        num2 += y2 + 5f * BaseScale;
      }
      this.simpletext.Location.Y -= num2 * 0.5f;
      this.Heading.Location.Y -= num2 * 0.5f;
      for (int index = 0; index < this.satiationbars.Count; ++index)
        this.satiationbars[index].Location.Y -= num2 * 0.5f;
      this.customerframe = new CustomerFrame(new Vector2(PreMutipliedWidth + 20f * BaseScale, num2 + 20f * BaseScale), CustomerFrameColors.Brown, BaseScale);
    }

    public Vector2 getSize() => this.customerframe.VSCale;

    public void UpdateSaleItemDetails()
    {
    }

    public void DrawSaleItems(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.Heading.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      for (int index = 0; index < this.satiationbars.Count; ++index)
        this.satiationbars[index].DrawSatisfactionBarAndText(spritebatch, Offset);
    }
  }
}
