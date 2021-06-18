// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.AllShopSummary.Row.C04_profits
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI.UXPanels;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood;
using TinyZoo.Z_SummaryPopUps.Generic;

namespace TinyZoo.Z_ManageShop.AllShopSummary.Row
{
  internal class C04_profits
  {
    private TopTextMini toptextmini;
    private CoinAndString coinandstring;
    private SplitNineSlice splitnineslice;
    public Vector2 Location;

    public C04_profits(float HeightForText, float BaseScale, ShopEntry shopentry)
    {
      this.toptextmini = new TopTextMini("Profits", BaseScale, HeightForText, false, true);
      this.toptextmini.CenterJustify = true;
      this.coinandstring = new CoinAndString(shopentry.shopstockstatus[0].GetProfits(TimeSegmentType.Last7Days, RevType.Profit));
      this.coinandstring.Location.Y = 13f;
      this.splitnineslice = new SplitNineSlice(new Vector2(90f, 50f * Sengine.ScreenRatioUpwardsMultiplier.Y), 23f * Sengine.ScreenRatioUpwardsMultiplier.Y, IsBlock: true);
      this.splitnineslice.FrameTop.SetAllColours(ColourData.Z_FrameGreenPale);
      this.splitnineslice.FrameBottom.SetAllColours(ColourData.Z_FrameGreenDarker);
    }

    public void DrawColumn(Vector2 Offset)
    {
      Offset += this.Location;
      this.splitnineslice.DrawSplitNineSlice(Offset, AssetContainer.pointspritebatchTop05);
      this.toptextmini.DrawTopTextMini(Offset);
      this.coinandstring.DrawCoinAndStringSmall(AssetContainer.pointspritebatchTop05, Offset);
    }
  }
}
