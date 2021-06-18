// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.BuyLand.BuyLandManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components;
using TinyZoo.PlayerDir;
using TinyZoo.Z_Data;

namespace TinyZoo.Z_Manage.BuyLand
{
  internal class BuyLandManager
  {
    private ScreenHeading screenhead;
    private BuyMoreLandFrameAndButton buymorelandbutton;
    private bool Exiting;
    private CurrentLandGrid currentlandgrid;

    public BuyLandManager(Player player)
    {
      this.currentlandgrid = new CurrentLandGrid(player);
      string ThisHeading = "BUY LAND";
      int num = this.currentlandgrid.AtMax ? 1 : 0;
      this.screenhead = new ScreenHeading(ThisHeading, 70f);
      this.buymorelandbutton = new BuyMoreLandFrameAndButton(player);
    }

    public void UpdateBuyLandManager(float DeltaTime, Player player, Vector2 Offset)
    {
      if (this.currentlandgrid.AtMax || !this.buymorelandbutton.UpdateBuyMoreLandFrameAndButton(player, Offset, DeltaTime) || (this.Exiting || PlayerStats.LandSize >= 12))
        return;
      this.Exiting = true;
      ++PlayerStats.LandSize;
      ZMapSetUp.BuyMoreLand(player);
      Game1.screenfade.BeginFade(true);
      ParkGate.Reset();
      Game1.SetNextGameState(GAMESTATE.OverWorldSetUp);
      player.OldSaveThisPlayer();
    }

    public void DrawBuyLandManager(Vector2 Offset)
    {
      if (this.screenhead != null)
        this.screenhead.DrawScreenHeading(Vector2.Zero, AssetContainer.pointspritebatch03);
      this.currentlandgrid.DrawCurrentLandGrid(Offset);
      this.buymorelandbutton.DrawBuyMoreLandFrameAndButton(Offset);
    }
  }
}
