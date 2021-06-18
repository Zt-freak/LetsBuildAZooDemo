// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.AllShopSummary.Row.C05_Visitors
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood;
using TinyZoo.Z_SummaryPopUps.Generic;

namespace TinyZoo.Z_ManageShop.AllShopSummary.Row
{
  internal class C05_Visitors
  {
    private TopTextMini toptextmini;
    private TopTextMini toptextminiTwo;
    private SplitNineSlice splitnineslice;
    public Vector2 Location;

    public C05_Visitors(
      float HeightForText,
      float SecondaryHeight,
      bool IsVisitors,
      float BaseScale,
      ShopEntry shopentry)
    {
      string WriteMe1 = "Visitors Yesterday";
      string str = "134";
      string WriteMe2;
      if (!IsVisitors)
      {
        WriteMe1 = "Served";
        str = "50%";
        float profits1 = (float) shopentry.shopstockstatus[0].GetProfits(TimeSegmentType.Last7Days, RevType.CustomersServed);
        float profits2 = (float) shopentry.shopstockstatus[0].GetProfits(TimeSegmentType.Last7Days, RevType.AllCustomers);
        WriteMe2 = (double) profits2 != 0.0 ? Math.Round((double) profits1 / (double) profits2 * 100.0).ToString() + "%" : "-";
      }
      else
        WriteMe2 = string.Concat((object) shopentry.shopstockstatus[0].GetProfits(TimeSegmentType.Last7Days, RevType.AllCustomers));
      this.toptextmini = new TopTextMini(WriteMe1, BaseScale, HeightForText, false, true);
      this.toptextmini.SetAsSplit();
      this.toptextmini.CenterJustify = true;
      this.toptextminiTwo = new TopTextMini(WriteMe2, BaseScale, SecondaryHeight);
      this.toptextminiTwo.CenterJustify = true;
      this.splitnineslice = new SplitNineSlice(new Vector2(90f, 50f * Sengine.ScreenRatioUpwardsMultiplier.Y), 23f * Sengine.ScreenRatioUpwardsMultiplier.Y, IsBlock: true);
      this.splitnineslice.FrameTop.SetAllColours(ColourData.Z_FrameBluePale);
      this.splitnineslice.FrameBottom.SetAllColours(ColourData.Z_FrameBlueDarker);
    }

    public void DrawColumn(Vector2 Offset)
    {
      Offset += this.Location;
      this.splitnineslice.DrawSplitNineSlice(Offset, AssetContainer.pointspritebatchTop05);
      this.toptextmini.DrawTopTextMini(Offset);
      this.toptextminiTwo.DrawTopTextMini(Offset);
    }
  }
}
