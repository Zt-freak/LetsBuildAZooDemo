// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.AllShopSummary.TopSummary.ShopSummaryEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_ManageShop.AllShopSummary.TopSummary
{
  internal class ShopSummaryEntry
  {
    private ShopSummaryIcon shopsummaryicon;
    public Vector2 Location;
    private GameObject textObj;

    public ShopSummaryEntry(SummaryEntryType summary)
    {
      this.shopsummaryicon = new ShopSummaryIcon(summary);
      this.shopsummaryicon.vLocation.X = -30f;
      this.textObj = new GameObject();
      this.textObj.SetAllColours(ColourData.Z_Cream);
      this.textObj.scale = RenderMath.GetPixelSizeBestMatch(1f);
    }

    public void UpdateShopSummaryEntry()
    {
    }

    public void DrawShopSummaryEntry(Vector2 Offset)
    {
      Offset += this.Location;
      this.shopsummaryicon.DrawShopSummaryIcon(Offset);
      TextFunctions.DrawJustifiedText("100", this.textObj.scale, this.textObj.vLocation + Offset, this.textObj.GetColour(), this.textObj.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05);
      TextFunctions.DrawJustifiedText("Food Shop", this.textObj.scale, this.shopsummaryicon.vLocation + Offset + new Vector2(0.0f, 20f), this.textObj.GetColour(), this.textObj.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
    }
  }
}
