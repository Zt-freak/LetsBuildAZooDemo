// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.BlackMarket.TrustRating
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
  internal class TrustRating
  {
    public CustomerFrame frame;
    private SimpleTextHandler text;
    public Vector2 Location;
    private StarBar starbar;

    public TrustRating(Vector2 MainFrameScale)
    {
      this.frame = new CustomerFrame(new Vector2(MainFrameScale.X - AnimalPopUpManager.Space, 50f));
      this.frame.frame.SetAllColours(0.0f, 0.0f, 0.0f);
      this.text = new SimpleTextHandler("Trust Rating", false, (float) (((double) this.frame.VSCale.X - (double) AnimalPopUpManager.VerticalBuffer * 1.0) / 1024.0), RenderMath.GetPixelSizeBestMatch(1f), false, false);
      this.text.paragraph.linemaker.SetAllColours(ColourData.BlackMarketPaleBlue);
      this.text.Location.X = this.frame.VSCale.X * -0.5f;
      this.text.Location.X += AnimalPopUpManager.VerticalBuffer;
      this.starbar = new StarBar(0.7f);
      this.starbar.Location.X = 0.0f;
      this.text.AutoCompleteParagraph();
    }

    public void DrawTrustRating(Vector2 Offset)
    {
      Offset += this.Location;
      this.frame.DrawCustomerFrame(Offset, AssetContainer.pointspritebatchTop05);
      this.text.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatchTop05);
      this.starbar.DrawStarBar(Offset, AssetContainer.pointspritebatchTop05);
    }
  }
}
