// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.BuyLand.BuyMoreLandFrameAndButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.GenericUI.UXPanels;
using TinyZoo.PlayerDir;

namespace TinyZoo.Z_Manage.BuyLand
{
  internal class BuyMoreLandFrameAndButton
  {
    private ScreenHeading screenherader;
    private GameObject TextColourThing;
    private GameObjectNineSlice framer;
    private TextButton textbutton;
    private Vector2 VScale;
    private ScreenHeading screener;
    private int Cost;
    private bool CanAfford = true;
    private CoinAndString coinandstring;

    public BuyMoreLandFrameAndButton(Player player)
    {
      this.screenherader = new ScreenHeading("NEW ANIMALS!", 100f);
      this.screenherader.header.vLocation = Vector2.Zero;
      Vector3 SecondaryColour;
      this.framer = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.framer.scale = 2f * Sengine.ScreenRationReductionMultiplier.Y;
      this.TextColourThing = new GameObject();
      this.TextColourThing.SetAllColours(SecondaryColour);
      this.textbutton = new TextButton("Expand Park", 60f);
      this.VScale = new Vector2(700f, 200f);
      this.framer.vLocation = new Vector2(512f, 650f);
      this.screener = new ScreenHeading("Land Expansion", 120f);
      this.screener.header.vLocation = Vector2.Zero;
      this.textbutton.vLocation = this.framer.vLocation;
      this.textbutton.vLocation.Y += 40f;
      this.Cost = PlayerStats.LandSize != 0 ? PlayerStats.LandSize * 1000 : 200;
      if (this.Cost > player.Stats.GetCashHeld())
      {
        this.CanAfford = false;
        this.textbutton.SetButtonColour(BTNColour.Red);
      }
      else
        this.CanAfford = true;
      this.coinandstring = new CoinAndString(this.Cost);
      this.coinandstring.SetTextColour(SecondaryColour);
    }

    public bool UpdateBuyMoreLandFrameAndButton(Player player, Vector2 Offset, float DeltaTime)
    {
      if (!this.textbutton.UpdateTextButton(player, Offset, DeltaTime) || !this.CanAfford)
        return false;
      player.Stats.SpendCash(this.Cost, SpendingCashOnThis.BuyLand, player);
      return true;
    }

    public void DrawBuyMoreLandFrameAndButton(Vector2 Offset)
    {
      this.framer.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VScale);
      TextFunctions.DrawJustifiedText("Get more space to build your zoo", RenderMath.GetPixelSizeBestMatch(1f) * 0.5f, this.framer.vLocation + new Vector2(0.0f, -30f), this.TextColourThing.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05);
      this.coinandstring.DrawCoinAndStringSmall(AssetContainer.pointspritebatch03, this.framer.vLocation + Offset);
      this.textbutton.DrawTextButton(Offset);
      this.screener.DrawScreenHeading(this.framer.vLocation + new Vector2(0.0f, (float) (-(double) this.VScale.Y * 0.5)), AssetContainer.pointspritebatchTop05);
    }
  }
}
