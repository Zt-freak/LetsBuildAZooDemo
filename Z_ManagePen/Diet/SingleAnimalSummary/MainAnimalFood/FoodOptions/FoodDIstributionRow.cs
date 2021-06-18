// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodOptions.FoodDIstributionRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodOptions
{
  internal class FoodDIstributionRow
  {
    private SplitNineSlice splitnineslice;
    public Vector2 Location;
    private Column1_Icon column1;
    private Column2_Name column2;
    public Column3_Amount column3;
    private Column4_Stock coulmn4;
    public Column5_Cost column5;
    private Column6_Restock column6;
    private FoodSet REF_foodset;
    private int _Index;
    private AnimalFoodEntry REF_animalfoodentry;
    private float FullDailyFoodRquirement;

    public FoodDIstributionRow(
      AnimalFoodEntry animalfoodentry,
      Vector2 VscaleOfParent,
      float CurrentFoodPerDayOneAnimal,
      int Index,
      FoodSet _REF_foodset,
      float _FullDailyFoodRquirement,
      Player player,
      int TotalAnimals,
      int TotalBabies,
      float BaseScale)
    {
      this.FullDailyFoodRquirement = _FullDailyFoodRquirement;
      this.REF_animalfoodentry = animalfoodentry;
      this._Index = Index;
      this.REF_foodset = _REF_foodset;
      float y = 50f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.splitnineslice = new SplitNineSlice(new Vector2(450f * BaseScale, y), 15f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      float HeightForText = y * -0.5f;
      float HeightForMiddleText = 0.0f;
      this.column1 = new Column1_Icon(HeightForText, Index + 1, animalfoodentry.foodtype, BaseScale);
      this.column1.Location.X = this.splitnineslice.VScaleTop.X * -0.5f;
      this.column1.Location.X += 5f;
      this.column2 = new Column2_Name(HeightForText, animalfoodentry.foodtype, HeightForMiddleText, BaseScale);
      this.column2.Location.X = this.column1.Location.X + 50f * BaseScale;
      this.column3 = new Column3_Amount(HeightForText, CurrentFoodPerDayOneAnimal, this.FullDailyFoodRquirement, BaseScale);
      this.column3.Location.X = this.column2.Location.X + 70f * BaseScale;
      this.coulmn4 = new Column4_Stock(HeightForText, HeightForMiddleText, animalfoodentry.foodtype, player, BaseScale);
      this.coulmn4.Location.X = this.column3.Location.X + 140f * BaseScale;
      this.column5 = new Column5_Cost(HeightForText, HeightForMiddleText, animalfoodentry.foodtype, CurrentFoodPerDayOneAnimal, TotalAnimals, TotalBabies, BaseScale);
      this.column5.Location.X = this.coulmn4.Location.X + 70f * BaseScale;
      this.coulmn4.SetUpStock(player, false);
      this.column6 = new Column6_Restock(HeightForText, (float) player.storerooms.GetTotalOfTheseOnOrder(animalfoodentry.foodtype), BaseScale, HeightForMiddleText, player.storerooms.GetDeliveryTime(animalfoodentry.foodtype));
      this.column6.Location.X = this.column5.Location.X + 50f * BaseScale;
    }

    public bool UpdateFoodRow(
      Vector2 Offset,
      Player player,
      float DeltaTime,
      bool IsAtMax,
      int CurrentTotal)
    {
      Offset += this.Location;
      int num = this.column3.UpdateColumn3_Amount(player, DeltaTime, Offset, IsAtMax, CurrentTotal) ? 1 : 0;
      if (this.column6.UpdateColumn6_Restock(player, Offset, DeltaTime) && TinyZoo.Game1.GetNextGameState() != GAMESTATE.ManageStoreRoomSetUp && player.storerooms.HasBuiltStoreRoom)
      {
        player.livestats.SelectedSHop = new LiveSlectedShop(player.storerooms.StorRoomcontents.StoreRoomLocation, TILETYPE.StoreRoom);
        TinyZoo.Game1.SetNextGameState(GAMESTATE.ManageStoreRoomSetUp);
        Z_GameFlags.CameToStoreRoomFromManagePen = true;
        Z_GameFlags.StoreRoomGoToThisFood = this.REF_animalfoodentry.foodtype;
        TinyZoo.Game1.screenfade.BeginFade(true);
      }
      if (num == 0)
        return num != 0;
      this.REF_foodset.FoodUnitsPerDay[this._Index] = (float) this.column3.priceadjuster.CurrentValue * 0.01f * this.FullDailyFoodRquirement;
      this.column5.SetNewCost(this.REF_foodset.FoodUnitsPerDay[this._Index]);
      this.coulmn4.SetUpStock(player, true);
      return num != 0;
    }

    public void DrawFoodRow(Vector2 Offset)
    {
      Offset += this.Location;
      this.splitnineslice.DrawSplitNineSlice(Offset, AssetContainer.pointspritebatchTop05);
      this.column1.DrawColumn1_Icon(Offset);
      this.column2.DrawColumn2_Name(Offset);
      this.column3.DrawColumn3_Amount(Offset);
      this.coulmn4.DrawColumn4_Stock(Offset);
      this.column5.DrawColumn5_Cost(Offset);
      this.column6.DrawColumn6_Restock(Offset);
    }
  }
}
