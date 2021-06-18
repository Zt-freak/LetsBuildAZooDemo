// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.AllShopSummary.ShopSummaryRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageShop.AllShopSummary.Row;
using TinyZoo.Z_SummaryPopUps.People.Animal;

namespace TinyZoo.Z_ManageShop.AllShopSummary
{
  internal class ShopSummaryRow
  {
    private SplitNineSlice splitnineslice;
    public Vector2 Location;
    public float Height;
    private C01_Shop c01_Shop;
    private C02_Type c02_Type;
    private C03_Appeal c03_Appeal;
    private C04_profits c04_profits;
    private C05_Visitors c05_Visitors;
    private C05_Visitors c06_Served;
    private C07_ExtraFood c07_ExtraFood;
    private C08_Staff c08_Staff;
    private C09_Stock c09_Stock;
    public C10_Buttons c10_Buttons;

    public ShopSummaryRow(
      ShopEntry shopentry,
      Player player,
      Vector2 VscaleOfParent,
      float BaseScale)
    {
      this.Height = 50f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.splitnineslice = new SplitNineSlice(new Vector2(VscaleOfParent.X - AnimalPopUpManager.Space, this.Height), 23f * Sengine.ScreenRatioUpwardsMultiplier.Y, true);
      float HeightForText = this.Height * -0.25f;
      float num1 = 16f;
      float num2 = (float) (((double) VscaleOfParent.X - (double) AnimalPopUpManager.Space) * -0.5);
      this.c01_Shop = new C01_Shop(HeightForText, shopentry.tiletype, player, num1, BaseScale);
      this.c02_Type = new C02_Type(HeightForText, shopentry.tiletype, num1, BaseScale);
      this.c03_Appeal = new C03_Appeal(HeightForText, shopentry, num1, BaseScale, player);
      this.c04_profits = new C04_profits(HeightForText, BaseScale, shopentry);
      this.c05_Visitors = new C05_Visitors(HeightForText, num1, true, BaseScale, shopentry);
      this.c06_Served = new C05_Visitors(HeightForText, num1, false, BaseScale, shopentry);
      this.c07_ExtraFood = new C07_ExtraFood(HeightForText, num1, BaseScale, shopentry);
      this.c08_Staff = new C08_Staff(HeightForText, num1, BaseScale);
      this.c09_Stock = new C09_Stock(HeightForText, num1, BaseScale);
      this.c10_Buttons = new C10_Buttons(HeightForText, BaseScale);
      this.c01_Shop.Location.X = num2 + 30f;
      float num3 = num2 + 90f;
      this.c02_Type.Location.X = num3;
      float num4 = num3 + 60f;
      this.c03_Appeal.Location.X = num4;
      float num5 = num4 + 90f;
      this.c04_profits.Location.X = num5;
      float num6 = num5 + 90f;
      this.c05_Visitors.Location.X = num6;
      float num7 = num6 + 90f;
      this.c06_Served.Location.X = num7;
      float num8 = num7 + 90f;
      this.c07_ExtraFood.Location.X = num8;
      float num9 = num8 + 90f;
      this.c08_Staff.Location.X = num9;
      float num10 = num9 + 90f;
      this.c09_Stock.Location.X = num10;
      this.c10_Buttons.Location.X = num10 + 90f;
    }

    public bool UpdateShopSummaryRow(Player player, Vector2 Offset, float DeltaTime)
    {
      Offset += this.Location;
      return false;
    }

    public void SetExtraIngredient(float Perc) => this.c07_ExtraFood.SetExtraIngredient(Perc);

    public Vector2 GetSize() => this.splitnineslice.GetSize();

    public void DrawShopSummaryRow(Vector2 Offset)
    {
      Offset += this.Location;
      this.splitnineslice.DrawSplitNineSlice(Offset, AssetContainer.pointspritebatchTop05);
      this.c01_Shop.DrawColumn(Offset);
      this.c02_Type.DrawColumn(Offset);
      this.c03_Appeal.DrawColumn(Offset);
      this.c04_profits.DrawColumn(Offset);
      this.c05_Visitors.DrawColumn(Offset);
      this.c06_Served.DrawColumn(Offset);
      this.c07_ExtraFood.DrawColumn(Offset);
      this.c08_Staff.DrawColumn(Offset);
    }
  }
}
