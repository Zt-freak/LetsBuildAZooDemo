// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.BlackMarket.BlackMarketDealer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Z_SummaryPopUps.People.Animal;

namespace TinyZoo.Z_SummaryPopUps.People.BlackMarket
{
  internal class BlackMarketDealer
  {
    private GameObjectNineSlice Frame;
    private AnimalBuyFrame animalframe;
    private SimpleTextHandler text;
    private TextButton Button;
    private PersonAndTip prsonandtip;
    private TraderInfo traderinfo;
    private TrustRating trustrating;
    private Vector2 HeadingLoc;
    private WalkingPerson REF_person;

    public BlackMarketDealer(Vector2 MainFrameScale, WalkingPerson person)
    {
      this.REF_person = person;
      this.prsonandtip = new PersonAndTip(person, MainFrameScale);
      this.prsonandtip.Location.Y = this.prsonandtip.frame.VSCale.Y * 0.5f;
      this.prsonandtip.Location.Y += AnimalPopUpManager.Space;
      this.trustrating = new TrustRating(MainFrameScale);
      this.trustrating.Location = this.prsonandtip.Location;
      this.trustrating.Location.Y += this.prsonandtip.frame.VSCale.Y * 0.5f;
      this.trustrating.Location.Y += this.trustrating.frame.VSCale.Y * 0.5f;
      this.trustrating.Location.Y += AnimalPopUpManager.VerticalBuffer;
      this.traderinfo = new TraderInfo(MainFrameScale);
      this.traderinfo.Location = this.trustrating.Location;
      this.traderinfo.Location.Y += this.trustrating.frame.VSCale.Y * 0.5f;
      this.traderinfo.Location.Y += this.traderinfo.frame.VSCale.Y * 0.5f;
      this.traderinfo.Location.Y += AnimalPopUpManager.VerticalBuffer;
      this.animalframe = new AnimalBuyFrame(MainFrameScale, this.REF_person.simperson.blackmarkettrader.Body, this.REF_person.simperson.blackmarkettrader.Head, this.REF_person.simperson.blackmarkettrader.Purchased, this.REF_person.simperson.blackmarkettrader.BodyVariant, this.REF_person.simperson.blackmarkettrader.HeadVariant);
      this.animalframe.Locations.Y = 350f;
      this.HeadingLoc = new Vector2(MainFrameScale.X * -0.5f + AnimalPopUpManager.VerticalBuffer, 17f);
    }

    public bool UpdateBlackMarketDealer(
      Vector2 Location,
      Player player,
      float DeltaTime,
      Vector2 VSCALE)
    {
      if (!this.animalframe.UpdateAnimalBuyFrame(Location, player, DeltaTime))
        return false;
      if (player.livestats.AnimalsJustTraded == null)
      {
        player.livestats.AnimalsJustTraded = new WaveInfo(new IntakeInfo());
        this.REF_person.simperson.blackmarkettrader.Purchased = true;
      }
      player.livestats.AnimalsJustTraded.People.Add(new IntakePerson(this.animalframe.Body, Variant: this.animalframe.BodyVariant, _HeadType: this.animalframe.Head, _HeadVariant: this.animalframe.HeadVariant));
      this.animalframe.SetPurchased();
      FeatureFlags.NewAnimalGot = true;
      Player.financialrecords.PurchasedFromBlackMrket();
      return true;
    }

    public void DrawBlackMarketDealer(Vector2 Location, Vector2 VSCALE)
    {
      TextFunctions.DrawTextWithDropShadow("BLACK MARKET TRADER", RenderMath.GetPixelSizeBestMatch(1f) * 0.5f, Location + this.HeadingLoc, new Color(211, 211, 222), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, false);
      this.animalframe.DrawAnimalBuyFrame(Location);
      this.trustrating.DrawTrustRating(Location);
      this.prsonandtip.DrawPersonAndTip(Location);
      this.traderinfo.DrawTraderInfo(Location);
    }
  }
}
