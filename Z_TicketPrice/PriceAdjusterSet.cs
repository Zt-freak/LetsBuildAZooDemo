// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TicketPrice.PriceAdjusterSet
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer;

namespace TinyZoo.Z_TicketPrice
{
  internal class PriceAdjusterSet
  {
    private GameObjectNineSlice Framer;
    public PriceAdjuster priceaduster;
    public TextButton Confirm;
    public Vector2 Location;
    private GameObject TEXTCLR;
    private bool BlockClicks;

    public PriceAdjusterSet(int Min, int Max, int StartingValue)
    {
      float num = 1f;
      if (DebugFlags.IsPCVersion)
        num = 0.75f;
      this.priceaduster = new PriceAdjuster(Min, Max, StartingValue, _OverallMultiplier: (Sengine.ScreenRationReductionMultiplier.Y * num));
      this.priceaduster.Location = new Vector2(512f, 450f);
      Vector3 SecondaryColour;
      this.Framer = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.Framer.scale = 2f;
      this.TEXTCLR = new GameObject();
      this.TEXTCLR.SetAllColours(SecondaryColour);
    }

    public void MakeNewButton(string TEXT, bool _BlockClicks = false, float Lebgth = 60f)
    {
      this.BlockClicks = _BlockClicks;
      this.Confirm = new TextButton(TEXT, Lebgth);
      this.Confirm.vLocation.X = 768f;
    }

    public bool UpdatePriceAdjusterSet(
      Player player,
      float DeltaTime,
      Vector2 Offset,
      bool CentralVersion = false)
    {
      if (CentralVersion)
        this.priceaduster.Location.X = 512f;
      if (this.Confirm != null && !this.BlockClicks && this.Confirm.UpdateTextButton(player, Offset + new Vector2(0.0f, this.priceaduster.Location.Y), DeltaTime))
        return true;
      this.priceaduster.UpdatePriceAdjuster(player, Offset, DeltaTime);
      return false;
    }

    public void DrawPriceAdjusterSet(Vector2 Offset)
    {
      this.Framer.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset + new Vector2(512f, this.priceaduster.Location.Y), new Vector2(800f, 150f));
      this.priceaduster.DrawPriceAdjuster(Offset);
      this.priceaduster.Location.X = 350f;
      if (this.Confirm == null)
        return;
      this.Confirm.DrawTextButton(Offset + new Vector2(0.0f, this.priceaduster.Location.Y));
    }

    public void DrawPriceAdjusterSetCentral(
      Vector2 Offset,
      string MainString,
      string SecondaryString,
      Color Secondarycolour)
    {
      this.Framer.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset + new Vector2(512f, this.priceaduster.Location.Y), new Vector2(450f, 130f));
      TextFunctions.DrawJustifiedText(MainString, 1f, Offset + this.Framer.vLocation + new Vector2(0.0f, -30f) + new Vector2(512f, this.priceaduster.Location.Y), this.TEXTCLR.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03);
      this.priceaduster.DrawPriceAdjuster(Offset);
      if (this.Confirm != null)
        this.Confirm.DrawTextButton(Offset + new Vector2(0.0f, this.priceaduster.Location.Y));
      TextFunctions.DrawJustifiedText(SecondaryString, 1f, Offset + this.Framer.vLocation + new Vector2(0.0f, 45f) + new Vector2(512f, this.priceaduster.Location.Y), Secondarycolour, 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03);
    }
  }
}
