// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps.BuyAnimalActionPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir.BlackMarket;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_WorldMap.WorldMapPopUps.Shelter;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps
{
  internal class BuyAnimalActionPopUp : CustomerActionPopUp
  {
    private ShelterMainFrame shelterMainFrame;
    private BlackMarketDealerHistory dealerHistory;
    private BlackMarketTraderInfo blackMarketTraderInfo;

    public BuyAnimalActionPopUp(Player player, float BaseScale, WalkingPerson walkingPerson)
      : base(BaseScale)
    {
      this.dealerHistory = player.blackmarketstats.GetDealerHistoryForThisBlackMarketDealer(walkingPerson.thispersontype);
      this.blackMarketTraderInfo = walkingPerson.simperson.blackmarkettrader;
      this.shelterMainFrame = new ShelterMainFrame(player, BaseScale, this.blackMarketTraderInfo);
    }

    public override Vector2 GetSize() => this.shelterMainFrame.GetSize();

    public override bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime)
    {
      if (!this.shelterMainFrame.UpdateShelterMainFrame(player, DeltaTime, offset))
        return base.UpdateCustomerActionPopUp(player, offset, DeltaTime);
      int count = player.livestats.AnimalsJustTraded.People.Count;
      this.dealerHistory.OnAnimalBought();
      this.blackMarketTraderInfo.Purchased = true;
      this.ForceCloseEverythingOnClose = true;
      for (int index = 0; index < count; ++index)
        Player.financialrecords.PurchasedFromBlackMrket();
      return true;
    }

    public override void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset) => this.shelterMainFrame.DrawShelterMainFrame(offset, spritebatch);
  }
}
