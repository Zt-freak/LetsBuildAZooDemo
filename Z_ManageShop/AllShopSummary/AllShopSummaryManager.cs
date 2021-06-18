// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.AllShopSummary.AllShopSummaryManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_ManageShop.AllShopSummary.TopSummary;
using TinyZoo.Z_SummaryPopUps.People.Animal;

namespace TinyZoo.Z_ManageShop.AllShopSummary
{
  internal class AllShopSummaryManager
  {
    private List<ShopSummaryRow> shopsummaryows;
    private MainFoodSummary mainfoodsummary;
    private GameObjectNineSlice Frame;
    private Vector2 FrameVScale;
    private string ShopName;

    public AllShopSummaryManager(Player player)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.Frame = new GameObjectNineSlice(new Rectangle(939, 416, 21, 21), 7);
      this.FrameVScale = new Vector2(950f, 650f);
      this.shopsummaryows = new List<ShopSummaryRow>();
      float num = 50f + AnimalPopUpManager.TopAreaBuffer + AnimalPopUpManager.VerticalBuffer;
      for (int index = 0; index < player.shopstatus.shopentries.Count; ++index)
      {
        this.shopsummaryows.Add(new ShopSummaryRow(player.shopstatus.shopentries[index], player, this.FrameVScale, baseScaleForUi));
        this.shopsummaryows[index].Location.Y = num;
        this.shopsummaryows[index].Location.Y += 15f;
        this.shopsummaryows[index].Location.Y += (float) (((double) index + 0.5) * ((double) this.shopsummaryows[index].Height + (double) AnimalPopUpManager.VerticalBuffer));
      }
      this.Frame.vLocation = new Vector2(512f, 384f);
      this.mainfoodsummary = new MainFoodSummary(player, this.FrameVScale);
    }

    public int UpdateAllShopSummaryManager(Player player, float DeltaTime)
    {
      int num = -1;
      Vector2 Offset = Vector2.Zero + this.Frame.vLocation;
      Offset.Y -= this.FrameVScale.Y * 0.5f;
      for (int index = 0; index < this.shopsummaryows.Count; ++index)
      {
        if (this.shopsummaryows[index].UpdateShopSummaryRow(player, Offset, DeltaTime))
        {
          player.livestats.SelectedSHop.tiletype = player.shopstatus.shopentries[index].tiletype;
          player.livestats.SelectedSHop.Location = new Vector2Int(player.shopstatus.shopentries[index].LocationOfThisShop);
          num = index;
        }
      }
      return num;
    }

    public void SetShopName(string _ShopName) => this.ShopName = _ShopName.ToUpper();

    public void DrawAllShopSummaryFrame()
    {
      Vector2 zero = Vector2.Zero;
      this.Frame.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, zero, this.FrameVScale);
      Vector2 vector2 = zero + this.Frame.vLocation;
      vector2.Y -= this.FrameVScale.Y * 0.5f;
      TextFunctions.DrawTextWithDropShadow("MANAGE SHOP: " + this.ShopName, 0.6666f, vector2 + new Vector2((float) ((double) this.FrameVScale.X * -0.5 + 0.5 * (double) AnimalPopUpManager.Space), 8f), new Color(ColourData.Z_Cream), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, false);
    }

    public void DrawAllShopSummaryManager()
    {
      Vector2 zero = Vector2.Zero;
      this.Frame.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, zero, this.FrameVScale);
      Vector2 vector2 = zero + this.Frame.vLocation;
      vector2.Y -= this.FrameVScale.Y * 0.5f;
      TextFunctions.DrawTextWithDropShadow("SHOP SUMMARY", 0.6666f, vector2 + new Vector2((float) ((double) this.FrameVScale.X * -0.5 + 0.5 * (double) AnimalPopUpManager.Space), 8f), new Color(ColourData.Z_Cream), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, false);
      this.mainfoodsummary.DrawMainFoodSummary(vector2);
      TextFunctions.DrawTextWithDropShadow("SHOP LIST", 0.6666f, vector2 + new Vector2((float) ((double) this.FrameVScale.X * -0.5 + 0.5 * (double) AnimalPopUpManager.Space), 35f) + new Vector2(0.0f, this.mainfoodsummary.customerframe.VSCale.Y), new Color(ColourData.Z_Cream), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, false);
      for (int index = 0; index < this.shopsummaryows.Count; ++index)
        this.shopsummaryows[index].DrawShopSummaryRow(vector2);
    }
  }
}
