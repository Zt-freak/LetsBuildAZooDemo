// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.Z_NewCostInfo.StatsAndCost.HeadingBlock
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuldMenu.Z_NewCostInfo.StatsAndCost
{
  internal class HeadingBlock
  {
    private CustomerFrame customerframe;
    private SimpleTextHandler simpletext;
    public Vector2 Location;
    private SimpleTextHandler Heading;

    public HeadingBlock(float BAseWidth, string _Heading, string Body, float BaseScale)
    {
      this.Heading = new SimpleTextHandler(_Heading, BAseWidth, _Scale: BaseScale, _UseFontOnePointFive: true, AutoComplete: true);
      this.Heading.SetAllColours(ColourData.Z_Cream);
      float y = this.Heading.GetSize().Y;
      this.Heading.Location.X = BAseWidth * -0.5f;
      float num1 = y + BaseScale * 10f;
      this.simpletext = new SimpleTextHandler(Body, BAseWidth, _Scale: BaseScale, AutoComplete: true);
      this.simpletext.Location.X = BAseWidth * -0.5f;
      this.simpletext.SetAllColours(ColourData.Z_Cream);
      this.simpletext.Location.Y = num1;
      float num2 = num1 + this.simpletext.GetSize().Y + BaseScale * 10f;
      this.customerframe = new CustomerFrame(new Vector2(BAseWidth + 20f * BaseScale, num2 + BaseScale * 20f), CustomerFrameColors.Brown, BaseScale);
      this.Heading.Location.Y -= num2 * 0.5f;
      this.simpletext.Location.Y -= num2 * 0.5f;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void DrawHeadingBlock(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.Heading.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, spritebatch);
    }
  }
}
