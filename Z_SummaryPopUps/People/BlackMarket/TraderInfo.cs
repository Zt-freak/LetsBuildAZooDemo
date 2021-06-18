// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.BlackMarket.TraderInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.BlackMarket
{
  internal class TraderInfo
  {
    public CustomerFrame frame;
    private SimpleTextHandler text;
    public Vector2 Location;

    public TraderInfo(Vector2 MainFrameScale)
    {
      this.frame = new CustomerFrame(new Vector2(MainFrameScale.X - AnimalPopUpManager.Space, 100f));
      this.frame.frame.SetAllColours(0.0f, 0.0f, 0.0f);
      float PercentagePfScreenWidth = (float) (((double) this.frame.VSCale.X - (double) AnimalPopUpManager.VerticalBuffer * 1.0) / 1024.0);
      this.text = new SimpleTextHandler(this.GetText(), false, PercentagePfScreenWidth, RenderMath.GetPixelSizeBestMatch(1f), false, false);
      this.text.paragraph.linemaker.SetAllColours(ColourData.BlackMarketPaleBlue);
      this.text.Location.X = this.frame.VSCale.X * -0.5f;
      this.text.Location.X += AnimalPopUpManager.VerticalBuffer;
      this.text.Location.Y = this.frame.VSCale.Y * -0.5f;
      this.text.Location.Y += AnimalPopUpManager.VerticalBuffer;
      this.text.AutoCompleteParagraph();
    }

    private string GetText() => "Black market traders offer:~* BUY Exotic animals illegally sourced.~* BUY Hybrid animals from unlicensed CRISPR laboratories~~Using the black market will elevate your criminal rating.";

    public void DrawTraderInfo(Vector2 Offset)
    {
      Offset += this.Location;
      this.frame.DrawCustomerFrame(Offset, AssetContainer.pointspritebatchTop05);
      this.text.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}
