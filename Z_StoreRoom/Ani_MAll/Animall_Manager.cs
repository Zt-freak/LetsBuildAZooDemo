// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Ani_MAll.Animall_Manager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.Z_StoreRoom.Ani_MAll.CheckOut;
using TinyZoo.Z_StoreRoom.Ani_MAll.History;
using TinyZoo.Z_StoreRoom.Ani_MAll.Scheduled;
using TinyZoo.Z_StoreRoom.Ani_MAll.ShopRight;
using TinyZoo.Z_StoreRoom.Ani_MAll.ShopThings;
using TinyZoo.Z_StoreRoom.Ani_MAll.ShopTop;

namespace TinyZoo.Z_StoreRoom.Ani_MAll
{
  internal class Animall_Manager
  {
    private BackButton CloseButton;
    private ThingsInShopManager StuffInStore;
    private ShopTopManager shoptop;
    private ShopSideBarManager shopsidebar;
    private SideBarButtonTYPE mode;
    private CheckOutManager checkoutmanager;
    private ScheduledManager schedulemanager;
    private HistoryManager historymanager;
    public bool SomethingWasOrdered;

    public Animall_Manager()
    {
      this.CloseButton = new BackButton();
      this.SomethingWasOrdered = false;
      this.StuffInStore = new ThingsInShopManager();
      this.shoptop = new ShopTopManager();
      this.shopsidebar = new ShopSideBarManager();
    }

    public bool UpdateAnimall_Manager(Player player, float DeltaTime)
    {
      SideBarButtonTYPE sideBarButtonType = this.shopsidebar.UpdateShopSideBarManager(player, Vector2.Zero, this.StuffInStore.TotalThings);
      if (sideBarButtonType != this.mode && sideBarButtonType != SideBarButtonTYPE.Count)
      {
        this.mode = sideBarButtonType;
        if (this.mode == SideBarButtonTYPE.CheckOut)
          this.checkoutmanager = new CheckOutManager(this.StuffInStore);
        else if (this.mode == SideBarButtonTYPE.History)
          this.historymanager = new HistoryManager();
        else if (this.mode == SideBarButtonTYPE.MyOrders)
          this.schedulemanager = new ScheduledManager();
      }
      if (this.mode == SideBarButtonTYPE.Main)
        this.StuffInStore.UpdateThingsInShopManager(Vector2.Zero, player, DeltaTime);
      else if (this.mode == SideBarButtonTYPE.CheckOut)
      {
        if (this.checkoutmanager.UpdateCheckOutManager(player, DeltaTime, this.StuffInStore))
        {
          this.SomethingWasOrdered = true;
          this.StuffInStore = new ThingsInShopManager();
        }
      }
      else if (this.mode == SideBarButtonTYPE.MyOrders)
        this.schedulemanager.UpdateScheduledManager();
      else if (this.mode == SideBarButtonTYPE.History)
        this.historymanager.UpdateHistoryManager();
      return this.shoptop.UpdateShopTopManager(player, DeltaTime);
    }

    public void DrawAnimall_Manager()
    {
      if (this.mode == SideBarButtonTYPE.Main)
        this.StuffInStore.DrawThingsInShopManager(Vector2.Zero);
      else if (this.mode == SideBarButtonTYPE.CheckOut)
        this.checkoutmanager.DrawCheckOutManager();
      else if (this.mode == SideBarButtonTYPE.MyOrders)
        this.schedulemanager.DrawScheduledManager();
      else if (this.mode == SideBarButtonTYPE.History)
        this.historymanager.DrawHistoryManager();
      this.shopsidebar.DrawShopSideBarManager(Vector2.Zero);
      this.shoptop.DrawShopTopManager(Vector2.Zero);
    }
  }
}
