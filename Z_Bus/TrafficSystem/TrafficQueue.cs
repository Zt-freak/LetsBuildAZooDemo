// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.TrafficSystem.TrafficQueue
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;

namespace TinyZoo.Z_Bus.TrafficSystem
{
  internal class TrafficQueue
  {
    private List<Z_BusObject> Traffic;

    public TrafficQueue() => this.Traffic = new List<Z_BusObject>();

    public void AddVehicleToRoad(Z_BusObject busobject)
    {
      if (this.Traffic.Contains(busobject))
        throw new Exception("CANNOT BE IN THE SAME LIST TWICE - WILL FULL ON BREAK THE GAME - as truck will be queuing behind itself.");
      this.Traffic.Insert(0, busobject);
    }

    public void UpdateTrafficQueue()
    {
      for (int index = 0; index < this.Traffic.Count; ++index)
      {
        if (index < this.Traffic.Count - 1)
          this.Traffic[index].SetLimit(this.Traffic[index + 1].GetBackOfBus());
        else
          this.Traffic[index].SetLimit(1E+08f);
      }
      for (int index = this.Traffic.Count - 1; index > -1; --index)
      {
        if (this.Traffic[index].drivestate == DriveState.None)
          this.Traffic.RemoveAt(index);
      }
    }

    public void DrawTrafficQueue()
    {
    }
  }
}
