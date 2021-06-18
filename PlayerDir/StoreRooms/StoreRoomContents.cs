// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.StoreRooms.StoreRoomContents
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Tile_Data;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_StoreRoom.Shelves;

namespace TinyZoo.PlayerDir.StoreRooms
{
  internal class StoreRoomContents
  {
    public List<StockEntry> stockentries;
    public Vector2Int StoreRoomLocation;

    public StoreRoomContents() => this.stockentries = new List<StockEntry>();

    public int GetCapacity() => TileData.StoreRoomCapacity(TILETYPE.StoreRoom);

    public void PullLocation(Player player)
    {
      for (int _X = 0; _X < player.prisonlayout.layout.BaseTileTypes.GetLength(0); ++_X)
      {
        for (int _Y = 0; _Y < player.prisonlayout.layout.BaseTileTypes.GetLength(1); ++_Y)
        {
          if (TileData.IsAStoreRoom(player.prisonlayout.layout.BaseTileTypes[_X, _Y].tiletype) && !player.prisonlayout.layout.BaseTileTypes[_X, _Y].isChild())
            this.StoreRoomLocation = new Vector2Int(_X, _Y);
        }
      }
    }

    public void UseThis(AnimalFoodType foodtype, float UseThisMuch, TinyZoo.PlayerDir.StoreRooms.StoreRooms storerooms)
    {
      for (int index = this.stockentries.Count - 1; index > -1; --index)
      {
        if (this.stockentries[index].foodtype == foodtype)
        {
          if ((double) this.stockentries[index].GetTotalStock() < (double) UseThisMuch)
          {
            UseThisMuch -= this.stockentries[index].GetTotalStock();
            this.stockentries.RemoveAt(index);
          }
          else
          {
            this.stockentries[index].ModifyStock(-UseThisMuch, storerooms, false);
            break;
          }
        }
      }
    }

    public float GetTotalStockOfThis(AnimalFoodType foodtype)
    {
      float num = 0.0f;
      for (int index = 0; index < this.stockentries.Count; ++index)
      {
        if (this.stockentries[index].foodtype == foodtype)
          num += this.stockentries[index].GetTotalStock();
      }
      return num;
    }

    public int GetRemainingSpace()
    {
      int num = 0;
      for (int index = this.stockentries.Count - 1; index > -1; --index)
        num += (int) Math.Ceiling((double) this.stockentries[index].GetTotalStock());
      return TileData.StoreRoomCapacity(TILETYPE.StoreRoom) - num;
    }

    public void AddStock(AnimalFoodType foodtype, int TotalOrdered, int ShelfLife) => this.stockentries.Add(new StockEntry(foodtype, TotalOrdered, ShelfLife));

    public void StartDay(Player player)
    {
      for (int index = this.stockentries.Count - 1; index > -1; --index)
      {
        if (this.stockentries[index].StartDay(player))
          this.stockentries.RemoveAt(index);
      }
    }

    public List<StoreRoomDataEntry> GetStoreRoomDataEntries(bool Current = true)
    {
      List<StoreRoomDataEntry> storeRoomDataEntryList = new List<StoreRoomDataEntry>();
      for (int index1 = 0; index1 < this.stockentries.Count; ++index1)
      {
        for (int index2 = 0; (double) index2 < Math.Ceiling((double) this.stockentries[index1].GetTotalStock()); ++index2)
          storeRoomDataEntryList.Add(new StoreRoomDataEntry(this.stockentries[index1].foodtype, this.stockentries[index1].GetTotalStock() - (float) index2, this.stockentries[index1].ShelfLifeRemaining));
      }
      return storeRoomDataEntryList;
    }

    public void SaveStoreRoomContents(Writer writer)
    {
      this.StoreRoomLocation.SaveVector2Int(writer);
      writer.WriteInt("s", this.stockentries.Count);
      for (int index = 0; index < this.stockentries.Count; ++index)
        this.stockentries[index].SaveStockEntry(writer);
    }

    public StoreRoomContents(Reader reader)
    {
      this.StoreRoomLocation = new Vector2Int(reader);
      int _out = 0;
      int num = (int) reader.ReadInt("s", ref _out);
      this.stockentries = new List<StockEntry>();
      for (int index = 0; index < _out; ++index)
        this.stockentries.Add(new StockEntry(reader));
    }
  }
}
