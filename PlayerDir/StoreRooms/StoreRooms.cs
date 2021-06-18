// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.StoreRooms.StoreRooms
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.Tile_Data;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.PlayerDir.StoreRooms
{
  internal class StoreRooms
  {
    public StoreRoomContents StorRoomcontents;
    private Orders orders;
    public float[] YesterdaysUse;
    public float[] TodaysUse;
    public bool HasBuiltStoreRoom;
    public bool HasRunOutOfSomethingThatIsNeededByAnimals;

    public StoreRooms()
    {
      this.YesterdaysUse = new float[88];
      this.TodaysUse = new float[88];
      this.StorRoomcontents = new StoreRoomContents();
      this.orders = new Orders();
    }

    public int GetDeliveryTime(AnimalFoodType thisfoodtype) => this.orders.GetDeliveryTime(thisfoodtype);

    public int GetTotalOfTheseOnOrder(AnimalFoodType thisfoodtype) => this.orders.GetTotalOfTheseOnOrder(thisfoodtype);

    public void InstantAddStock(AnimalFoodType animalFoodType, int Total) => this.StorRoomcontents.AddStock(animalFoodType, Total, AnimalFoodData.GetAnimalFoodInfo(animalFoodType).ShelfLife);

    public void OrderAThing(AnimalFoodType animalFoodType, int Total, int _DaysUntilDelivery) => this.orders.AddNewORder(new OrderEntry(animalFoodType, _DaysUntilDelivery, Total));

    public void StartNewDay()
    {
      this.YesterdaysUse = this.TodaysUse;
      this.TodaysUse = new float[88];
      this.orders.StartNewDay(this.StorRoomcontents);
    }

    public void AddStoreRoom(StoreRoomContents storeroom, Vector2Int Location)
    {
      Z_GameFlags.HasBuiltStoreRoom = true;
      this.StorRoomcontents.StoreRoomLocation = new Vector2Int(Location);
      this.HasBuiltStoreRoom = true;
    }

    public void RemoveStoreRoom(Vector2Int Location, TILETYPE buildingtype)
    {
    }

    public void UseThis(AnimalFoodType animalFoodType, float UseThisMuch) => this.StorRoomcontents.UseThis(animalFoodType, UseThisMuch, this);

    public float GetTotalStockOfThis(AnimalFoodType animalFoodType) => 0.0f + this.StorRoomcontents.GetTotalStockOfThis(animalFoodType);

    public StoreRoomContents GetStoreRoom(Vector2Int Location) => this.StorRoomcontents;

    public string GetStuffOnOrderString(out string RightString) => this.orders.GetStuffOnOrderString(out RightString);

    public StoreRooms(Reader reader)
    {
      int num1 = (int) reader.ReadBool("s", ref this.HasBuiltStoreRoom);
      this.StorRoomcontents = new StoreRoomContents(reader);
      this.orders = new Orders(reader);
      int _out = 0;
      int num2 = (int) reader.ReadInt("f", ref _out);
      this.YesterdaysUse = new float[_out];
      this.TodaysUse = new float[_out];
      for (int index = 0; index < this.YesterdaysUse.Length; ++index)
      {
        int num3 = (int) reader.ReadFloat("f", ref this.YesterdaysUse[index]);
      }
    }

    public void SaveStoreRooms(Writer writer)
    {
      writer.WriteBool("s", this.HasBuiltStoreRoom);
      this.StorRoomcontents.SaveStoreRoomContents(writer);
      this.orders.SaveOrders(writer);
      writer.WriteInt("s", this.YesterdaysUse.Length);
      for (int index = 0; index < this.YesterdaysUse.Length; ++index)
        writer.WriteFloat("f", this.YesterdaysUse[index]);
    }
  }
}
