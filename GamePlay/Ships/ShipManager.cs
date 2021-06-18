// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Ships.ShipManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.GamePlay.beams;
using TinyZoo.GamePlay.ReclaimedZones;

namespace TinyZoo.GamePlay.Ships
{
  internal class ShipManager
  {
    public List<Ship> ships;

    public ShipManager(int TotalPlayers)
    {
      this.ships = new List<Ship>();
      this.ships.Add(new Ship(0));
    }

    public void UpdateShipManager(
      Player[] players,
      float DeltaTime,
      BeamManager beammanager,
      bool IsGoingToNextLevel,
      BoxZoneManager boxzonemanager)
    {
      if (!IsGoingToNextLevel)
      {
        for (int index = 0; index < this.ships.Count; ++index)
          this.ships[index].UpdateShip(DeltaTime, players, beammanager, out bool _, boxzonemanager);
      }
      else
      {
        for (int index = 0; index < this.ships.Count; ++index)
          this.ships[index].UpdateDuringGameOver(DeltaTime);
      }
    }

    public void DrawShipManager()
    {
      for (int index = 0; index < this.ships.Count; ++index)
        this.ships[index].DrawShip();
    }
  }
}
