// Decompiled with JetBrains decompiler
// Type: TinyZoo.Audio.LoopingSoundHandler
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace TinyZoo.Audio
{
  internal class LoopingSoundHandler
  {
    private static List<SmartSFXHolder> ActiveInstances = new List<SmartSFXHolder>();

    internal static void FadeoutAndRemoveThisSound(
      string UIDName,
      float TimeForFade,
      bool ForceStartVol = true,
      float StartVol = 1f,
      float TargetVol = 0.0f)
    {
      for (int index = 0; index < LoopingSoundHandler.ActiveInstances.Count; ++index)
      {
        if (LoopingSoundHandler.ActiveInstances[index].UID == UIDName)
          LoopingSoundHandler.ActiveInstances[index].tryToFadeOutAndRemove(TimeForFade, ForceStartVol, StartVol, TargetVol);
      }
    }

    internal static void FadeoutAndRemoveAllSounds(
      float TimeForFade,
      bool ForceStartVol = false,
      float StartVol = 1f,
      float TargetVol = 0.0f)
    {
      for (int index = 0; index < LoopingSoundHandler.ActiveInstances.Count; ++index)
        LoopingSoundHandler.ActiveInstances[index].tryToFadeOutAndRemove(TimeForFade, ForceStartVol, StartVol, TargetVol);
    }

    internal static bool HasThisSound(string UIDName)
    {
      for (int index = 0; index < LoopingSoundHandler.ActiveInstances.Count; ++index)
      {
        if (LoopingSoundHandler.ActiveInstances[index].UID == UIDName)
          return true;
      }
      return false;
    }

    internal static SmartSFXHolder GetSoundByUID(string UIDName)
    {
      for (int index = 0; index < LoopingSoundHandler.ActiveInstances.Count; ++index)
      {
        if (LoopingSoundHandler.ActiveInstances[index].UID == UIDName)
          return LoopingSoundHandler.ActiveInstances[index];
      }
      return (SmartSFXHolder) null;
    }

    internal static void StopAndRemoveThisSound(string UIDName)
    {
      for (int index = LoopingSoundHandler.ActiveInstances.Count - 1; index > -1; --index)
      {
        if (LoopingSoundHandler.ActiveInstances[index].UID == UIDName)
        {
          LoopingSoundHandler.ActiveInstances[index].tryToFadeOutAndRemove(0.0f, false, 0.0f);
          LoopingSoundHandler.ActiveInstances.RemoveAt(index);
        }
      }
    }

    internal static SmartSFXHolder PlaySpecificSoundEffect(
      SoundEffect soundeffect,
      string UIDName,
      float Pitch = 0.0f,
      float Vol = 1f,
      bool WillRestart = false,
      bool WillLoop = true,
      bool ForceCreateNew = false)
    {
      return DebugFlags.SFXDisabled ? (SmartSFXHolder) null : LoopingSoundHandler.PlayThisSound(soundeffect, UIDName, WillLoop, Vol, Pitch, WillRestart, ForceCreateNew);
    }

    internal static SmartSFXHolder PlayLoopongSoundEffects(
      SoundEffectType sfxtype,
      string UIDName,
      float Pitch = 0.0f,
      float Vol = 1f,
      bool WillRestart = false,
      bool WillLoop = true)
    {
      if (DebugFlags.SFXDisabled)
        return (SmartSFXHolder) null;
      return sfxtype == SoundEffectType.ForestAmbience ? LoopingSoundHandler.PlayThisSound(SoundEffectsManager.ForestAmbience.soundeffect, UIDName, Vol: Vol, Pitch: Pitch, WillRestart: WillRestart) : (SmartSFXHolder) null;
    }

    internal static void PauseAllSounds()
    {
      for (int index = 0; index < LoopingSoundHandler.ActiveInstances.Count; ++index)
        LoopingSoundHandler.ActiveInstances[index].TryToPause();
    }

    internal static void resumeAllSounds()
    {
      for (int index = 0; index < LoopingSoundHandler.ActiveInstances.Count; ++index)
        LoopingSoundHandler.ActiveInstances[index].TryToUnPause();
    }

    internal static void FadeInThisSound(
      string UIDName,
      float TimeForFade,
      bool ForceStartVol = true,
      float StartVol = 0.0f,
      float TargetVol = 1f)
    {
      for (int index = 0; index < LoopingSoundHandler.ActiveInstances.Count; ++index)
      {
        if (LoopingSoundHandler.ActiveInstances[index].UID == UIDName)
        {
          LoopingSoundHandler.ActiveInstances[index].TryToUnPause();
          LoopingSoundHandler.ActiveInstances[index].FadeIn(TimeForFade, ForceStartVol, StartVol * SoundEffectsManager.SFXVolume, TargetVol * SoundEffectsManager.SFXVolume);
        }
      }
    }

    private static SmartSFXHolder PlayThisSound(
      SoundEffect thisSound,
      string UIDName,
      bool Looping = true,
      float Vol = 1f,
      float Pitch = 0.0f,
      bool WillRestart = false,
      bool ForceMakeNew = false)
    {
      if (DebugFlags.SFXDisabled)
        return (SmartSFXHolder) null;
      if (thisSound != null)
      {
        bool flag = false;
        for (int index = LoopingSoundHandler.ActiveInstances.Count - 1; index > -1; --index)
        {
          if (LoopingSoundHandler.ActiveInstances[index].UID == UIDName)
          {
            if (ForceMakeNew)
            {
              SoundEffectInstance instance = thisSound.CreateInstance();
              if (instance != null)
              {
                instance.Volume = Vol * SoundEffectsManager.SFXVolume;
                instance.Pitch = Pitch;
                instance.IsLooped = Looping;
                instance.Play();
                SmartSFXHolder smartSfxHolder = new SmartSFXHolder(UIDName, Vol, Pitch);
                smartSfxHolder.AddSound(instance);
                LoopingSoundHandler.ActiveInstances[index] = smartSfxHolder;
                return smartSfxHolder;
              }
            }
            else
            {
              if (WillRestart)
                LoopingSoundHandler.ActiveInstances[index].ResetAndPlay(Looping);
              return LoopingSoundHandler.ActiveInstances[index];
            }
          }
        }
        if (!flag)
        {
          SoundEffectInstance instance = thisSound.CreateInstance();
          if (instance != null)
          {
            instance.Volume = Vol * SoundEffectsManager.SFXVolume;
            instance.Pitch = Pitch;
            instance.IsLooped = Looping;
            instance.Play();
            SmartSFXHolder smartSfxHolder = new SmartSFXHolder(UIDName, Vol, Pitch);
            smartSfxHolder.AddSound(instance);
            LoopingSoundHandler.ActiveInstances.Add(smartSfxHolder);
            return smartSfxHolder;
          }
        }
      }
      return (SmartSFXHolder) null;
    }

    internal static void UpdateLoopsounds(float DeltaTime)
    {
      for (int index = LoopingSoundHandler.ActiveInstances.Count - 1; index > -1; --index)
      {
        if (LoopingSoundHandler.ActiveInstances[index].UpdateSound(DeltaTime))
          LoopingSoundHandler.ActiveInstances.RemoveAt(index);
      }
    }
  }
}
