// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.StockAdjustBits.StockStatus
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.Z_ManageShop.Shop_Data;

namespace TinyZoo.Z_ManageShop.StockAdjustBits
{
  internal class StockStatus
  {
    private TextButton STockButton;
    private TextButton CurrentPrice;

    public StockStatus(int Stoock, int MaxStock, int price, ShopStatEntry shopstat)
    {
      this.STockButton = new TextButton("Stock: " + (object) Stoock + "/" + (object) MaxStock, 70f);
      this.STockButton.vLocation.X = 200f;
      if (Stoock == 0)
        this.STockButton.SetButtonColour(BTNColour.Red);
      this.CurrentPrice = new TextButton("Price $" + (object) price, 50f);
      int num1 = (int) ((double) shopstat.IdealCost * 0.150000005960464);
      if (price < shopstat.IdealCost - num1)
        this.CurrentPrice.SetButtonColour(BTNColour.PaleYellow);
      else if (price > shopstat.IdealCost + num1)
        this.CurrentPrice.SetButtonColour(BTNColour.PaleYellow);
      int num2 = num1 * 2;
      if (price < shopstat.IdealCost - num2)
      {
        this.CurrentPrice.SetButtonColour(BTNColour.Red);
      }
      else
      {
        if (price <= shopstat.IdealCost + num2)
          return;
        this.CurrentPrice.SetButtonColour(BTNColour.Red);
      }
    }

    public bool UpdateStockStatus(
      Player player,
      float DeltaTime,
      Vector2 Offest,
      out bool PressedPrice)
    {
      PressedPrice = false;
      if (this.CurrentPrice.UpdateTextButton(player, Offest, DeltaTime))
      {
        PressedPrice = true;
        return true;
      }
      return this.STockButton.UpdateTextButton(player, Offest, DeltaTime);
    }

    public void DrawStockStatus(Vector2 Offest)
    {
      this.STockButton.DrawTextButton(Offest);
      this.CurrentPrice.DrawTextButton(Offest);
    }
  }
}
