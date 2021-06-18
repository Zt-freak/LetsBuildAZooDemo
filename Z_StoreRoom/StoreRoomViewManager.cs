// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.StoreRoomViewManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_StoreRoom.Shelves;
using TinyZoo.Z_StoreRoom.StuffOnOrder;

namespace TinyZoo.Z_StoreRoom
{
  internal class StoreRoomViewManager
  {
    private ScreenHeading screenheading;
    private BackButton backbutton;
    private StoreRoomSummary storeroom;
    private TextButton OrderStuff;
    public bool GoToOrdering;
    private StuffOnOrderManager stuffonorder;

    public StoreRoomViewManager(Player player)
    {
      this.backbutton = new BackButton();
      this.OrderStuff = new TextButton("GO TO SHOP", 60f, OverAllMultiplier: 0.6666f, _UseRoundaboutFont: true);
      this.OrderStuff.SetButtonGreen();
      this.OrderStuff.stringinabox.Frame.scale *= 0.5f;
      this.OrderStuff.vLocation = new Vector2(800f, 100f);
      this.screenheading = new ScreenHeading("Store Room".ToUpper());
      player.storerooms.GetStoreRoom(player.livestats.SelectedSHop.Location);
      this.storeroom = new StoreRoomSummary(true, player);
      this.stuffonorder = new StuffOnOrderManager(player, this.storeroom.shelfdisplaymanager.InnerFrameHole.vLocation + this.storeroom.shelfdisplaymanager.TopLeft, this.storeroom.shelfdisplaymanager.InnerFrameHoleVScale);
      this.OrderStuff.vLocation.X = this.stuffonorder.InnerFrameHole.vLocation.X;
      int roomGoToThisFood = (int) Z_GameFlags.StoreRoomGoToThisFood;
    }

    public void ResetOders(Player player) => this.stuffonorder = new StuffOnOrderManager(player, this.storeroom.shelfdisplaymanager.InnerFrameHole.vLocation + this.storeroom.shelfdisplaymanager.TopLeft, this.storeroom.shelfdisplaymanager.InnerFrameHoleVScale);

    public bool UpdateStoreRoomViewManager(Player player, float DeltaTime)
    {
      this.storeroom.UpdateStoreRoomSummary(DeltaTime, player);
      if (this.OrderStuff.UpdateTextButton(player, Vector2.Zero, DeltaTime))
      {
        this.GoToOrdering = true;
        return true;
      }
      if (!this.backbutton.UpdateBackButton(player, DeltaTime))
        return false;
      this.GoToOrdering = false;
      return true;
    }

    public void DrawStoreRoomViewManager()
    {
      this.storeroom.DrawStoreRoomSummary();
      this.stuffonorder.DrawStuffOnOrderManager(Vector2.Zero);
      this.storeroom.PostDrawStoreRoomSummary();
      this.backbutton.DrawBackButton(Vector2.Zero);
      this.OrderStuff.DrawTextButton(Vector2.Zero);
      TextFunctions.DrawJustifiedText(this.storeroom.GetCapacityString(), RenderMath.GetPixelSizeBestMatch(1f) * 0.5f, this.OrderStuff.vLocation + new Vector2(0.0f, -40f), new Color(ColourData.Z_Cream), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03);
      this.screenheading.DrawScreenHeading(Vector2.Zero, AssetContainer.pointspritebatch03);
    }
  }
}
