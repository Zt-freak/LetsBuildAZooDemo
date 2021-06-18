// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.beams.BeamManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Effects;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GamePlay.ReclaimedZones;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks;

namespace TinyZoo.GamePlay.beams
{
  internal class BeamManager
  {
    private List<BeamCenter> beams;
    private BeamInfo beaminfo;
    private int BeamIndex;

    public BeamManager(Player player)
    {
      this.beams = new List<BeamCenter>();
      this.beaminfo = new BeamInfo(player);
    }

    public BeamManager(BeamLayout beamlayout, BoxZoneManager boxzonemanager)
    {
      this.beams = new List<BeamCenter>();
      for (int index = 0; index < beamlayout.beamentries.Count; ++index)
        this.beams.Add(new BeamCenter(beamlayout.beamentries[index], index, false));
      this.UpdateBeamManager(1f, boxzonemanager);
    }

    public BeamLayout GetBeamDataForSave()
    {
      BeamLayout beamLayout = new BeamLayout();
      for (int index = 0; index < this.beams.Count; ++index)
        beamLayout.beamentries.Add(new BeamEntry(this.beams[index]));
      return beamLayout;
    }

    public BeamCenter Firebeam(Vector2 Location, bool IsHorizontal, bool PlaySpawnEffect)
    {
      if ((double) Location.Y < (double) TileMath.GetPlaySpaceTop())
        Location.Y = TileMath.GetPlaySpaceRight();
      else if ((double) Location.Y > (double) TileMath.GetPlaySpaceBottom())
        Location.Y = TileMath.GetPlaySpaceBottom();
      if ((double) Location.X > (double) TileMath.GetPlaySpaceRight())
        Location.X = TileMath.GetPlaySpaceRight();
      else if ((double) Location.X < (double) TileMath.GetPlaySpaceLeft())
        Location.X = TileMath.GetPlaySpaceLeft();
      bool flag = false;
      for (int index = 0; index < this.beams.Count; ++index)
      {
        if (!this.beams[index].StillActive())
        {
          this.beams[index].FireBeam(Location, IsHorizontal, this.beaminfo, PlaySpawnEffect);
          return this.beams[index];
        }
      }
      if (flag)
        return (BeamCenter) null;
      this.beams.Add(new BeamCenter(this.BeamIndex));
      this.beams[this.beams.Count - 1].FireBeam(Location, IsHorizontal, this.beaminfo);
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.LaunchBeam);
      return this.beams[this.beams.Count - 1];
    }

    public bool AreAnyBeamsActive()
    {
      for (int index = 0; index < this.beams.Count; ++index)
      {
        if (!this.beams[index].IsLockedInPlace && !this.beams[index].BeamWasHitByHuman)
          return true;
      }
      return false;
    }

    public bool UpdateBeamManager(float DeltaTime, BoxZoneManager boxzonemanager)
    {
      bool flag = false;
      for (int index = 0; index < this.beams.Count; ++index)
      {
        if (this.beams[index].UpdateBeamCenter(DeltaTime, this.beams) && !GameFlags.IsBreakOut)
        {
          boxzonemanager.TryToDivide(this.beams[index]);
          flag = true;
        }
      }
      return flag;
    }

    public bool CheckForEnemyHits(
      Vector2 OldLocation,
      Vector2 NewLocation,
      EnemyRenderer thisenemy)
    {
      for (int index = this.beams.Count - 1; index > -1; --index)
      {
        if (this.beams[index].CheckForEnemyHits(OldLocation, NewLocation, thisenemy))
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.KillPerson);
          this.beams[index].BeamGotHitByHuman();
          DestoyedBeams.BeamsDooms.Add(this.beams[index].vLocation);
          this.beams.RemoveAt(index);
          return true;
        }
      }
      return false;
    }

    public void DrawBeamManager()
    {
      for (int index = 0; index < this.beams.Count; ++index)
        this.beams[index].DrawBeamCenter();
    }
  }
}
