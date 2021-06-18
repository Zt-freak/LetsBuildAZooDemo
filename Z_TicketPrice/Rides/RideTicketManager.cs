// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TicketPrice.Rides.RideTicketManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_TicketPrice.Rides
{
  internal class RideTicketManager
  {
    private RideTicketPanel panel;

    public RideTicketManager(Player player, TILETYPE tiletype)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      if (TileData.IsATicketedRide(tiletype))
      {
        int typeToTicketType = (int) TicketData.GetTileTypeToTicketType(tiletype);
      }
      this.panel = new RideTicketPanel(player, tiletype, baseScaleForUi);
      this.panel.location = Sengine.HalfReferenceScreenRes;
    }

    public bool CheckMouseOver(Player player) => this.panel.CheckMouseOver(player, Vector2.Zero);

    public bool UpdateRideTicketManager(Player player, float DeltaTime) => (0 | (this.panel.UpdateRideTicketPanel(player, Vector2.Zero, DeltaTime) ? 1 : 0)) != 0;

    public void DrawRideTicketManager() => this.panel.DrawRideTicketPanel(AssetContainer.pointspritebatchTop05, Vector2.Zero);
  }
}
