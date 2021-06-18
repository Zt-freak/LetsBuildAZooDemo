// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Z_StoreRoomViewManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.Z_StoreRoom.Ani_MAll;

namespace TinyZoo.Z_StoreRoom
{
  internal class Z_StoreRoomViewManager
  {
    private StoreBGManager storeBGManager;
    private Animall_Manager animallmanager;
    private StoreRoomViewMode viewmode;
    private StoreRoomViewManager storroomviewmanager;

    public Z_StoreRoomViewManager(Player player)
    {
      this.viewmode = StoreRoomViewMode.Room;
      this.storeBGManager = new StoreBGManager(IsAutumnal: true);
      this.storroomviewmanager = new StoreRoomViewManager(player);
      if (Z_GameFlags.StoreRoomGoToThisFood == AnimalFoodType.Count && !Z_GameFlags.ForceToShopOnEnteringStoreRoom)
        return;
      Z_GameFlags.ForceToShopOnEnteringStoreRoom = false;
      this.animallmanager = new Animall_Manager();
      this.viewmode = StoreRoomViewMode.Shop;
    }

    public void UpdateZ_StoreRoomViewManager(Player player, float DeltaTime)
    {
      this.storeBGManager.UpdateStoreBGManager(DeltaTime);
      if (this.viewmode == StoreRoomViewMode.Room)
      {
        if (!this.storroomviewmanager.UpdateStoreRoomViewManager(player, DeltaTime))
          return;
        if (this.storroomviewmanager.GoToOrdering)
        {
          this.animallmanager = new Animall_Manager();
          this.viewmode = StoreRoomViewMode.Shop;
        }
        else if (Z_GameFlags.CameToStoreRoomFromManagePen)
        {
          Game1.screenfade.BeginFade(true);
          Game1.SetNextGameState(GAMESTATE.ManagePen);
        }
        else
        {
          Game1.screenfade.BeginFade(true);
          Game1.SetNextGameState(GAMESTATE.OverWorld);
        }
      }
      else
      {
        if (this.viewmode != StoreRoomViewMode.Shop || !this.animallmanager.UpdateAnimall_Manager(player, DeltaTime))
          return;
        if (this.animallmanager.SomethingWasOrdered)
          this.storroomviewmanager.ResetOders(player);
        this.viewmode = StoreRoomViewMode.Room;
      }
    }

    public void DrawZ_StoreRoomViewManager()
    {
      this.storeBGManager.DrawStoreBGManager(Vector2.Zero);
      if (this.viewmode == StoreRoomViewMode.Room)
      {
        this.storroomviewmanager.DrawStoreRoomViewManager();
      }
      else
      {
        if (this.viewmode != StoreRoomViewMode.Shop)
          return;
        this.animallmanager.DrawAnimall_Manager();
      }
    }
  }
}
