// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Shelter.SheltderedAnimals.CollumnXBuy
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Shelter.SheltderedAnimals
{
  internal class CollumnXBuy
  {
    private TextButton textbutton;
    public Vector2 Location;
    private ZGenericText toptextmini;
    private ZGenericText Middletextmini;
    private int COST;
    private bool Purchased;
    private bool isBlackMarket;

    public CollumnXBuy(
      int _COST,
      float HeightForText,
      float MidTextHeight,
      Player player,
      float BaseScale,
      bool _isBlackMarket = false,
      bool isAlreadyPurchased = false)
    {
      this.isBlackMarket = _isBlackMarket;
      this.COST = _COST;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      double defaultXbuffer = (double) uiScaleHelper.GetDefaultXBuffer();
      this.toptextmini = new ZGenericText("Price", BaseScale, _UseOnePointFiveFont: true);
      this.toptextmini.vLocation.Y = HeightForText;
      this.Middletextmini = new ZGenericText("$" + (object) this.COST, BaseScale, _UseOnePointFiveFont: true);
      this.Middletextmini.vLocation.Y = MidTextHeight;
      string TextToDraw = "Rescue!";
      float Length = 55f;
      if (this.isBlackMarket)
        TextToDraw = "Purchase";
      this.textbutton = new TextButton(BaseScale, TextToDraw, Length);
      this.textbutton.vLocation.Y = MidTextHeight;
      this.textbutton.vLocation.X = uiScaleHelper.ScaleX(35f) + this.textbutton.GetSize_True().X * 0.5f;
      this.UpdateButtonColourBasedOnPlayerCash(player);
      if (!isAlreadyPurchased)
        return;
      this.SetAsPurchased();
    }

    private void UpdateButtonColourBasedOnPlayerCash(Player player)
    {
      if (this.Purchased)
        return;
      if (player.Stats.GetCashHeld() < this.COST)
        this.textbutton.SetButtonColour(BTNColour.Red);
      else
        this.textbutton.SetButtonColour(BTNColour.Green);
    }

    public bool UpdateCollumnXBuy(Player player, Vector2 Offset, float DeltaTime)
    {
      Offset += this.Location;
      this.UpdateButtonColourBasedOnPlayerCash(player);
      if (this.textbutton.UpdateTextButton(player, Offset, DeltaTime) && !this.Purchased)
      {
        SpendingCashOnThis spendingonthis = SpendingCashOnThis.BuyAnimals_Shelter;
        if (this.isBlackMarket)
          spendingonthis = SpendingCashOnThis.BuyAnimals_BlackMarket;
        if (player.Stats.SpendCash(this.COST, spendingonthis, player))
        {
          this.SetAsPurchased();
          return true;
        }
      }
      return false;
    }

    private void SetAsPurchased()
    {
      this.textbutton.SetButtonColour(BTNColour.Blue);
      this.textbutton.SetText("Purchased");
      this.Purchased = true;
    }

    public void DrawCollumnXBuy(Vector2 Offset) => this.DrawCollumnXBuy(Offset, AssetContainer.pointspritebatch03);

    public void DrawCollumnXBuy(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.Location;
      this.textbutton.DrawTextButton(Offset, 1f, spriteBatch);
      this.toptextmini.DrawZGenericText(Offset, spriteBatch);
      this.Middletextmini.DrawZGenericText(Offset, spriteBatch);
    }
  }
}
