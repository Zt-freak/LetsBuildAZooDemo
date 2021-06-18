// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.ReclaimedZones.BoxZoneManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.beams;

namespace TinyZoo.GamePlay.ReclaimedZones
{
  internal class BoxZoneManager
  {
    public List<BoxZone> zones;

    public BoxZoneManager()
    {
      GameFlags.CurrentReclamedZones = 0M;
      GameFlags.FullZoneSize = (Decimal) (TileMath.GetPlaySpaceRight() - TileMath.GetPlaySpaceLeft()) * (Decimal) (TileMath.GetPlaySpaceBottom() - TileMath.GetPlaySpaceTop());
      this.zones = new List<BoxZone>();
      this.zones.Add(new BoxZone(new Vector2(TileMath.GetPlaySpaceLeft(), TileMath.GetPlaySpaceTop()), new Vector2(TileMath.GetPlaySpaceRight(), TileMath.GetPlaySpaceBottom())));
    }

    public void TryToDivide(BeamCenter beamcentre)
    {
      int index1 = -1;
      for (int index2 = 0; index2 < this.zones.Count; ++index2)
      {
        if (this.zones[index2].IsThisInThisZone(beamcentre.vLocation))
          index1 = index2;
      }
      if (index1 != -1)
      {
        BoxZone zone = this.zones[index1];
        this.zones.RemoveAt(index1);
        zone.SetDestroyed();
        if (beamcentre.IsHorizontal)
        {
          this.zones.Add(new BoxZone(zone.TopLeft, new Vector2(zone.BottomRight.X, beamcentre.vLocation.Y)));
          this.zones.Add(new BoxZone(new Vector2(zone.TopLeft.X, beamcentre.vLocation.Y), zone.BottomRight));
        }
        else
        {
          this.zones.Add(new BoxZone(zone.TopLeft, new Vector2(beamcentre.vLocation.X, zone.BottomRight.Y)));
          this.zones.Add(new BoxZone(new Vector2(beamcentre.vLocation.X, zone.TopLeft.Y), zone.BottomRight));
        }
      }
      this.CountProgress();
    }

    public void CountProgress()
    {
      GameFlags.CurrentReclamedZones = 0M;
      for (int index = 0; index < this.zones.Count; ++index)
      {
        if (!this.zones[index].HasPerson && !this.zones[index].IsDestroyed)
        {
          float num1 = this.zones[index].BottomRight.X - this.zones[index].TopLeft.X;
          float num2 = this.zones[index].BottomRight.Y - this.zones[index].TopLeft.Y;
          GameFlags.CurrentReclamedZones += (Decimal) (num1 * num2);
        }
      }
    }

    public void Addedline(Vector2 Start, Vector2 End)
    {
    }

    public BoxZone GetMyZone(ref Vector2 Location)
    {
      for (int index = 0; index < this.zones.Count; ++index)
      {
        if (this.zones[index].IsThisInThisZone(Location))
          return this.zones[index];
      }
      Location = this.zones[0].TopLeft + (this.zones[0].BottomRight - this.zones[0].TopLeft) * MathStuff.getRandomFloat(0.1f, 0.9f);
      return this.zones[0];
    }

    public void SetAllTempHasPersonCopy()
    {
      for (int index = 0; index < this.zones.Count; ++index)
      {
        this.zones[index].TempHasPerson = this.zones[index].HasPerson;
        this.zones[index].HasPerson = false;
      }
    }

    public void LaunchProgressParticles()
    {
      for (int index = 0; index < this.zones.Count; ++index)
      {
        if (!this.zones[index].IsDestroyed)
          this.zones[index].LaunchProgressParticles();
      }
    }

    public void RevallidateAgainstTemp()
    {
      for (int index = 0; index < this.zones.Count; ++index)
      {
        if (!this.zones[index].IsDestroyed && this.zones[index].HasPerson != this.zones[index].TempHasPerson && !this.zones[index].HasPerson)
          this.zones[index].ForceSetSafeAfterDeath();
      }
    }

    public void UpdateBoxZoneManager(float DeltaTime)
    {
      for (int index = 0; index < this.zones.Count; ++index)
        this.zones[index].UpdateBoxZone(DeltaTime);
    }

    public void DrawBoxZoneManager()
    {
      if (TinyZoo.Game1.gamestate == GAMESTATE.OverWorld || TinyZoo.Game1.gamestate == GAMESTATE.ManageShop)
        return;
      for (int index = 0; index < this.zones.Count; ++index)
        this.zones[index].DrawBoxZone();
    }
  }
}
