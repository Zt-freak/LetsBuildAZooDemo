// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Shelves.ShelfDisplayManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;

namespace TinyZoo.Z_StoreRoom.Shelves
{
  internal class ShelfDisplayManager
  {
    public GameObjectNineSlice InnerFrameHole;
    public Vector2 InnerFrameHoleVScale;
    public List<StoreRoomShelf> Storeroomshelves;
    public int Selected;
    public int TotalSpaceUsed;
    public int TotalStoreRoomCapacity;
    public Vector2 TopLeft = new Vector2(30f, 200f);

    public ShelfDisplayManager(Player player)
    {
      this.Selected = -1;
      this.InnerFrameHole = new GameObjectNineSlice(new Rectangle(948, 528, 21, 21), 7);
      this.InnerFrameHole.scale = 2f;
      this.Storeroomshelves = new List<StoreRoomShelf>();
      float num1 = 20f;
      int num2 = 0;
      this.TotalStoreRoomCapacity += player.storerooms.StorRoomcontents.GetCapacity();
      for (int index = 0; index < player.storerooms.StorRoomcontents.stockentries.Count; ++index)
      {
        this.Storeroomshelves.Add(new StoreRoomShelf(player.storerooms.StorRoomcontents.stockentries[index]));
        this.TotalSpaceUsed += (int) Math.Ceiling((double) player.storerooms.StorRoomcontents.stockentries[index].GetTotalStock());
      }
      int _RemainingSpace = num2 + (this.TotalStoreRoomCapacity - this.TotalSpaceUsed);
      if (_RemainingSpace > 0)
        this.Storeroomshelves.Add(new StoreRoomShelf(_RemainingSpace));
      int num3 = 5;
      int num4 = 0;
      int num5 = 0;
      for (int index = 0; index < this.Storeroomshelves.Count; ++index)
      {
        this.Storeroomshelves[index].vLocation = new Vector2((float) num5 * this.Storeroomshelves[index].scale * (float) this.Storeroomshelves[index].DrawRect.Width, (float) num4 * this.Storeroomshelves[index].scale * (float) this.Storeroomshelves[index].DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y);
        this.Storeroomshelves[index].vLocation.X += num1 * (float) num5;
        this.Storeroomshelves[index].vLocation.Y += num1 * (float) num4;
        ++num5;
        if (num5 >= num3)
        {
          num5 = 0;
          ++num4;
        }
        this.Storeroomshelves[index].vLocation.X += this.Storeroomshelves[index].scale * 0.5f * (float) this.Storeroomshelves[index].DrawRect.Width;
        this.Storeroomshelves[index].vLocation.X += num1 * 0.5f;
        this.Storeroomshelves[index].vLocation.Y += num1 * 0.5f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.Storeroomshelves[index].vLocation.Y += (float) (50.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y - 50.0);
      }
      if (this.Storeroomshelves.Count > 0)
      {
        this.InnerFrameHoleVScale = new Vector2(this.Storeroomshelves[0].scale * (float) this.Storeroomshelves[0].DrawRect.Width * (float) num3, 800f);
        this.InnerFrameHoleVScale.X += num1 * (float) num3;
        this.InnerFrameHole.vLocation = new Vector2(this.InnerFrameHoleVScale.X * 0.5f, 350f);
      }
      else
      {
        this.InnerFrameHoleVScale = new Vector2(533f, 800f);
        this.InnerFrameHole.vLocation = new Vector2(this.InnerFrameHoleVScale.X * 0.5f, 350f);
      }
    }

    public string GetCapacityString() => "Capacity: " + (object) this.TotalSpaceUsed + "/" + (object) this.TotalStoreRoomCapacity;

    public void Deselect()
    {
      this.Storeroomshelves[this.Selected].Select(false);
      this.Selected = -1;
    }

    public bool UpdateShelfDisplayManager(float DeltaTime, Player player, Vector2 Offset)
    {
      Offset += this.TopLeft;
      bool flag = false;
      for (int index = 0; index < this.Storeroomshelves.Count; ++index)
      {
        if (this.Storeroomshelves[index].UpdateStoreRoomShelf(DeltaTime, player, Offset))
        {
          if (this.Selected > -1)
            this.Storeroomshelves[this.Selected].Select(false);
          if (this.Selected != index)
          {
            flag = true;
            this.Selected = index;
            this.Storeroomshelves[this.Selected].Select(true);
          }
          else
          {
            this.Selected = -1;
            flag = true;
          }
        }
      }
      return flag;
    }

    public void DrawShelfDisplayManager(Vector2 Offset)
    {
      Offset += this.TopLeft;
      this.InnerFrameHole.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.InnerFrameHoleVScale);
      for (int index = 0; index < this.Storeroomshelves.Count; ++index)
        this.Storeroomshelves[index].DrawStoreRoomShelf(Offset);
    }
  }
}
