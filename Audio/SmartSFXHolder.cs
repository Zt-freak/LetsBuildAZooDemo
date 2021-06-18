// Decompiled with JetBrains decompiler
// Type: TinyZoo.Audio.SmartSFXHolder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace TinyZoo.Audio
{
  internal class SmartSFXHolder
  {
    private SoundEffectInstance thisInstance;
    public string UID;
    public SoundEffectType thissound;
    private float StartingVol;
    private float StartingPitch;
    private bool Fading;
    private float TargetVlume;
    private float StartingVolume;
    private float TransitionTimer;
    private float TransitionTime;
    private bool FadingOutForRemove;
    public bool isPaused;

    public SmartSFXHolder(string _UID, float _StartingVol, float _StartingPitch)
    {
      this.StartingVol = _StartingVol;
      this.StartingPitch = _StartingPitch;
      this.UID = _UID;
    }

    public void TryToPause()
    {
      if ((double) this.thisInstance.Volume <= 0.0)
        return;
      this.thisInstance.Pause();
      this.isPaused = true;
    }

    public void TryToUnPause()
    {
      if (!this.isPaused)
        return;
      this.isPaused = false;
      this.thisInstance.Resume();
    }

    public void SetPan(float Pan) => this.thisInstance.Pan = Pan;

    public void SetVolume(float Volume, bool WillRestart = true)
    {
      if ((this.thisInstance.State == SoundState.Paused || this.thisInstance.State == SoundState.Stopped) && WillRestart)
        this.thisInstance.Play();
      Volume = MathHelper.Clamp(Volume, 0.0f, 1f);
      this.TargetVlume = Volume * SoundEffectsManager.SFXVolume;
      this.thisInstance.Volume = this.TargetVlume;
      this.StartingVolume = this.TargetVlume;
    }

    public void tryToFadeOutAndRemove(
      float FadeTime,
      bool ForceStartVol = true,
      float StartVol = 1f,
      float TargetVol = 0.0f)
    {
      if (ForceStartVol)
        this.thisInstance.Volume = StartVol;
      this.FadingOutForRemove = true;
      this.Fading = true;
      this.TransitionTime = FadeTime;
      this.StartingVolume = this.thisInstance.Volume;
      this.TransitionTimer = 0.0f;
      this.TargetVlume = TargetVol;
      if ((double) FadeTime != 0.0 || (double) this.TargetVlume != 0.0)
        return;
      this.thisInstance.Stop();
    }

    public void FadeIn(float FadeTime, bool ForceStartVol = true, float StartVol = 0.0f, float TargetVol = 1f)
    {
      if (ForceStartVol)
        this.thisInstance.Volume = StartVol;
      this.FadingOutForRemove = false;
      this.Fading = true;
      this.TransitionTime = FadeTime;
      this.StartingVolume = this.thisInstance.Volume;
      this.TransitionTimer = 0.0f;
      this.TargetVlume = TargetVol;
    }

    public void ResetAndPlay(bool WillLoop)
    {
      this.thisInstance.Stop();
      this.thisInstance.Volume = this.StartingVol;
      this.thisInstance.Pitch = this.StartingPitch;
      this.Fading = false;
      this.thisInstance.Play();
    }

    public void AddSound(SoundEffectInstance instance) => this.thisInstance = instance;

    public bool HasFinished() => this.thisInstance.State == SoundState.Stopped;

    public bool UpdateSound(float DeltaTime)
    {
      if (this.Fading && (double) this.TransitionTimer < (double) this.TransitionTime)
      {
        this.TransitionTimer += DeltaTime;
        if ((double) this.TransitionTimer >= (double) this.TransitionTime)
        {
          this.thisInstance.Volume = this.TargetVlume;
          this.Fading = false;
          if (this.FadingOutForRemove && (double) this.TargetVlume == 0.0)
          {
            this.thisInstance.Stop();
            return true;
          }
        }
        else
          this.thisInstance.Volume = (float) (((double) this.StartingVolume + ((double) this.TargetVlume - (double) this.StartingVolume)) * ((double) this.TransitionTimer / (double) this.TransitionTime));
      }
      return false;
    }
  }
}
