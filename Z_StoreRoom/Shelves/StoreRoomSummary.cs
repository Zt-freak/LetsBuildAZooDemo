// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Shelves.StoreRoomSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Z_StoreRoom.Shelves.ShelfPopUp;

namespace TinyZoo.Z_StoreRoom.Shelves
{
  internal class StoreRoomSummary
  {
    private Vector2 Offset;
    private ShelfPopUpMAnager shelfpopupinfo;
    private ShelfBG shelfbg;
    public ShelfDisplayManager shelfdisplaymanager;
    private StoreRoom_ControllerMatrix controllermatrix;

    public StoreRoomSummary(bool IsCurrenStock, Player player)
    {
      this.shelfdisplaymanager = new ShelfDisplayManager(player);
      this.shelfbg = new ShelfBG();
      this.Offset = new Vector2(100f, 200f);
      this.controllermatrix = new StoreRoom_ControllerMatrix(this.shelfdisplaymanager);
    }

    public string GetCapacityString() => this.shelfdisplaymanager.GetCapacityString();

    public void UpdateStoreRoomSummary(float DeltaTime, Player player)
    {
      if (this.shelfdisplaymanager.UpdateShelfDisplayManager(DeltaTime, player, Vector2.Zero))
        this.shelfpopupinfo = this.shelfdisplaymanager.Selected <= -1 ? (ShelfPopUpMAnager) null : new ShelfPopUpMAnager(this.shelfdisplaymanager.Storeroomshelves[this.shelfdisplaymanager.Selected].RefSTockEntry, player);
      if (this.shelfpopupinfo == null || !this.shelfpopupinfo.UpdaeShelfPopUpMAnager(Vector2.Zero, player, DeltaTime))
        return;
      this.shelfpopupinfo = (ShelfPopUpMAnager) null;
      if (this.shelfdisplaymanager.Selected <= -1)
        return;
      this.shelfdisplaymanager.Deselect();
      this.shelfpopupinfo = (ShelfPopUpMAnager) null;
    }

    public void DrawStoreRoomSummary()
    {
      this.shelfbg.DrawShelfBG(Vector2.Zero, AssetContainer.pointspritebatch03, false);
      this.shelfdisplaymanager.DrawShelfDisplayManager(Vector2.Zero);
      this.shelfbg.DrawShelfBG(Vector2.Zero, AssetContainer.pointspritebatch03, true);
    }

    public void PostDrawStoreRoomSummary()
    {
      if (this.shelfpopupinfo == null)
        return;
      this.shelfpopupinfo.DrawShelfPopUpMAnager(Vector2.Zero, AssetContainer.pointspritebatch03);
    }
  }
}
