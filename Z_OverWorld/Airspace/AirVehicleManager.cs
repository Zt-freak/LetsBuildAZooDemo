// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.Airspace.AirVehicleManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TinyZoo.Z_OverWorld.Airspace
{
  internal class AirVehicleManager
  {
    private List<AirVehicle> airvehicles = new List<AirVehicle>();

    public void UpdateAirVehicleManager(float SimulationTime, Player player)
    {
      List<AirVehicle> airVehicleList = new List<AirVehicle>();
      foreach (AirVehicle airvehicle in this.airvehicles)
      {
        if (airvehicle.active && airvehicle.UpdateAirVehicle(SimulationTime, player))
        {
          airvehicle.active = false;
          airVehicleList.Add(airvehicle);
        }
      }
      foreach (AirVehicle airVehicle in airVehicleList)
        this.airvehicles.Remove(airVehicle);
    }

    public bool ThisIsOnOrder(int OrderUID)
    {
      foreach (AirVehicle airvehicle in this.airvehicles)
      {
        if ((airvehicle.vehicletype == AirVehicleType.DeliveryChinook || airvehicle.vehicletype == AirVehicleType.BlackChinook || airvehicle.vehicletype == AirVehicleType.Drone) && airvehicle.ThisIsOnOrder(OrderUID))
          return true;
      }
      return false;
    }

    public bool IsSomethingOnOrderToThisPen(int CellUID)
    {
      foreach (AirVehicle airvehicle in this.airvehicles)
      {
        if ((airvehicle.vehicletype == AirVehicleType.DeliveryChinook || airvehicle.vehicletype == AirVehicleType.BlackChinook || airvehicle.vehicletype == AirVehicleType.Drone) && airvehicle.IsSomethingOnOrderToThisPen(CellUID))
          return true;
      }
      return false;
    }

    public void AddAirVehicle(AirVehicle vehicle) => this.airvehicles.Add(vehicle);

    public void DrawAirVehicleManager()
    {
      foreach (AirVehicle airvehicle in this.airvehicles)
        airvehicle.DrawAirVehicle(AssetContainer.pointspritebatch01, Vector2.Zero);
    }
  }
}
