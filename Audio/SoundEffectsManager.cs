// Decompiled with JetBrains decompiler
// Type: TinyZoo.Audio.SoundEffectsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using SEngine.Content;
using System;
using System.Collections.Generic;

namespace TinyZoo.Audio
{
  internal class SoundEffectsManager
  {
    internal static float SFXVolume = 1f;
    private static List<SoundEffectInstance> ActiveInstances;
    internal static SoundEffectContainer Menu_GBintro;
    internal static SoundEffectContainer ConfirmClick;
    internal static SoundEffectContainer BackClick;
    internal static SoundEffectContainer MenuOpen;
    internal static SoundEffectContainer MenuClose;
    internal static SoundEffectContainer ConfirmUpgrade;
    internal static SoundEffectContainer DeathChargeDown;
    internal static SoundEffectContainer FieldConnect;
    internal static SoundEffectContainer LaunchBeam;
    internal static SoundEffectContainer ClickSingle;
    internal static SoundEffectContainer ModeSelectButton;
    internal static SoundEffectContainer Gun1;
    internal static SoundEffectContainer Gun2;
    internal static SoundEffectContainer Gun3;
    internal static SoundEffectContainer Rotate;
    internal static SoundEffectContainer BuildSomethingAlt;
    internal static SoundEffectContainer BuildSomething;
    internal static SoundEffectContainer Explode;
    internal static SoundEffectContainer Elect01;
    internal static SoundEffectContainer Elect02;
    internal static SoundEffectContainer Elect03;
    internal static SoundEffectContainer Elect04;
    internal static SoundEffectContainer ForestAmbience;
    internal static SoundEffectContainer Unbuild;
    internal static SoundEffectContainer StampClick;
    internal static SoundEffectContainer[] soundeffectscontainers;
    private static float CLIKTME;
    private static float SingleClickTime;

    internal static void LoadSplashSound()
    {
      SoundEffectsManager.ActiveInstances = new List<SoundEffectInstance>();
      if (DebugFlags.SFXDisabled)
      {
        SoundEffectsManager.SFXVolume = 0.0f;
      }
      else
      {
        SoundEffectsManager.Menu_GBintro = new SoundEffectContainer();
        SoundEffectsManager.Menu_GBintro.soundeffect = Game1.SFXMngr.Load<SoundEffect>(SoundEffectsManager.GetFilePath("SFX/GBIntro"));
      }
    }

    internal static void LoadSFX()
    {
      if (DebugFlags.SFXDisabled)
        SoundEffectsManager.SFXVolume = 0.0f;
      SoundEffectsManager.soundeffectscontainers = new SoundEffectContainer[59];
      for (int index = 0; index < 59; ++index)
        SoundEffectsManager.soundeffectscontainers[index] = new SoundEffectContainer();
      SoundEffectsManager.ConfirmClick = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/ClickOne"), ref SoundEffectsManager.ConfirmClick));
      for (int index = 22; index < 59; ++index)
      {
        SoundEffectsManager.soundeffectscontainers[index] = new SoundEffectContainer();
        ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/Animals/" + (object) (SoundEffectType) index), ref SoundEffectsManager.soundeffectscontainers[index]));
      }
      SoundEffectsManager.BackClick = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/ClickTwo"), ref SoundEffectsManager.BackClick));
      SoundEffectsManager.MenuOpen = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/MenuOpen"), ref SoundEffectsManager.MenuOpen));
      SoundEffectsManager.MenuClose = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/menuClose"), ref SoundEffectsManager.MenuClose));
      SoundEffectsManager.ConfirmUpgrade = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/ConfirmUpgrade"), ref SoundEffectsManager.ConfirmUpgrade));
      SoundEffectsManager.DeathChargeDown = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/DeathChargeDown"), ref SoundEffectsManager.DeathChargeDown));
      SoundEffectsManager.FieldConnect = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/FieldConnect"), ref SoundEffectsManager.FieldConnect));
      SoundEffectsManager.LaunchBeam = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/LaunchBeam"), ref SoundEffectsManager.LaunchBeam));
      SoundEffectsManager.ClickSingle = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/ClickSingle"), ref SoundEffectsManager.ClickSingle));
      SoundEffectsManager.BuildSomethingAlt = new SoundEffectContainer();
      if (Z_DebugFlags.IsBetaVersion)
        ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/BuildSomething"), ref SoundEffectsManager.BuildSomethingAlt));
      else
        ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/BuildSomethingAlt"), ref SoundEffectsManager.BuildSomethingAlt));
      SoundEffectsManager.BuildSomething = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/BuildSomething"), ref SoundEffectsManager.BuildSomething));
      SoundEffectsManager.Explode = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/Explode"), ref SoundEffectsManager.Explode));
      SoundEffectsManager.Elect01 = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/Elect01"), ref SoundEffectsManager.Elect01));
      SoundEffectsManager.Elect02 = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/Elect02"), ref SoundEffectsManager.Elect02));
      SoundEffectsManager.Elect03 = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/Elect03"), ref SoundEffectsManager.Elect03));
      SoundEffectsManager.Elect04 = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/Elect04"), ref SoundEffectsManager.Elect04));
      SoundEffectsManager.Elect02 = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/Elect02"), ref SoundEffectsManager.Elect02));
      SoundEffectsManager.ModeSelectButton = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/ModeSelectButton"), ref SoundEffectsManager.ModeSelectButton));
      SoundEffectsManager.Rotate = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/Rotate"), ref SoundEffectsManager.Rotate));
      SoundEffectsManager.ModeSelectButton = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/ModeSelectButton"), ref SoundEffectsManager.ModeSelectButton));
      SoundEffectsManager.Unbuild = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/Unbuild"), ref SoundEffectsManager.Unbuild));
      SoundEffectsManager.Gun1 = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/Gun1"), ref SoundEffectsManager.Gun1));
      SoundEffectsManager.Gun2 = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/Gun2"), ref SoundEffectsManager.Gun2));
      SoundEffectsManager.Gun3 = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/Gun3"), ref SoundEffectsManager.Gun3));
      SoundEffectsManager.StampClick = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/Stamp"), ref SoundEffectsManager.StampClick));
      SoundEffectsManager.ForestAmbience = new SoundEffectContainer();
      ContentLoadScheduler.AddContent(new ContentLoaderEntry((ContentManager) Game1.SFXMngr, SoundEffectsManager.GetFilePath("SFX/ForestAmbience"), ref SoundEffectsManager.ForestAmbience));
    }

    internal static string GetFilePath(string path)
    {
      int startIndex = path.LastIndexOf('/') + 1;
      path = path.Substring(startIndex);
      if (char.IsDigit(path, 0))
        path = "_" + path;
      return path;
    }

    internal static float GetPitch() => (float) (((double) Game1.Rnd.Next(75, 125) - 100.0) * 0.00999999977648258);

    internal static void PlaySpecificSound(
      SoundEffectType soundeffecttype,
      float Volume = 1f,
      float Pitch = 0.0f,
      float Pan = 0.0f)
    {
      if (DebugFlags.SFXDisabled)
        return;
      if (soundeffecttype >= SoundEffectType.Rabbit_Cute01)
      {
        SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.soundeffectscontainers[(int) soundeffecttype].soundeffect, Volume, Pitch, Pan);
      }
      else
      {
        switch (soundeffecttype)
        {
          case SoundEffectType.Menu_Splash:
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Menu_GBintro.soundeffect, Volume, Pitch, Pan);
            break;
          case SoundEffectType.ConfirmClick:
            if ((double) SoundEffectsManager.CLIKTME > 0.0)
              break;
            SoundEffectsManager.CLIKTME = 0.1f;
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.ConfirmClick.soundeffect, Volume, Pitch, Pan);
            break;
          case SoundEffectType.BackClick:
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.BackClick.soundeffect, Volume, Pitch, Pan);
            break;
          case SoundEffectType.MenuOpen:
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.MenuOpen.soundeffect, Volume, Pitch, Pan);
            break;
          case SoundEffectType.MenuClose:
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.MenuClose.soundeffect, Volume, Pitch, Pan);
            break;
          case SoundEffectType.ConfirmUpgrade:
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.ConfirmUpgrade.soundeffect, Volume, Pitch, Pan);
            break;
          case SoundEffectType.DeathChargeDown:
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.DeathChargeDown.soundeffect, Volume, Pitch, Pan);
            break;
          case SoundEffectType.FieldConnect:
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.FieldConnect.soundeffect, Volume, Pitch, Pan);
            break;
          case SoundEffectType.LaunchBeam:
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.LaunchBeam.soundeffect, Volume, Pitch, Pan);
            break;
          case SoundEffectType.ClickSingle:
            if ((double) SoundEffectsManager.SingleClickTime > 0.0)
              break;
            SoundEffectsManager.SingleClickTime = 0.01f;
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.ClickSingle.soundeffect, Volume, Pitch, Pan);
            break;
          case SoundEffectType.Unbuild:
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Unbuild.soundeffect, Volume, Pitch, Pan);
            break;
          case SoundEffectType.BuildSomething:
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.BuildSomething.soundeffect, Volume * 0.8f, Pitch, Pan);
            break;
          case SoundEffectType.DemolishBuilding:
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.BuildSomething.soundeffect);
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.BuildSomethingAlt.soundeffect, 0.35f, -0.4f, Pan);
            break;
          case SoundEffectType.Explode:
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Explode.soundeffect, Volume, Pitch, Pan);
            break;
          case SoundEffectType.KillPerson:
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Explode.soundeffect, Pitch: 0.5f);
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Explode.soundeffect, Pitch: -0.5f);
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.DeathChargeDown.soundeffect, 0.7f, -0.3f);
            int num1 = Game1.Rnd.Next(0, 4);
            float Pitch1 = (float) Game1.Rnd.Next(0, 10) * 0.1f;
            switch (num1)
            {
              case 0:
                SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Elect01.soundeffect, Pitch: Pitch1, Pan: Pan);
                return;
              case 1:
                SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Elect02.soundeffect, Pitch: Pitch1, Pan: Pan);
                return;
              case 2:
                SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Elect03.soundeffect, Pitch: Pitch1, Pan: Pan);
                return;
              case 3:
                SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Elect04.soundeffect, Pitch: Pitch1, Pan: Pan);
                return;
              default:
                throw new Exception("WRONG RANDOM RANGE");
            }
          case SoundEffectType.BeamConnect:
            int num2 = Game1.Rnd.Next(0, 4);
            float Pitch2 = (float) Game1.Rnd.Next(0, 10) * 0.1f;
            switch (num2)
            {
              case 0:
                SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Elect01.soundeffect, Volume, Pitch2, Pan);
                return;
              case 1:
                SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Elect02.soundeffect, Volume, Pitch2, Pan);
                return;
              case 2:
                SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Elect03.soundeffect, Volume, Pitch2, Pan);
                return;
              case 3:
                SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Elect04.soundeffect, Volume, Pitch2, Pan);
                return;
              default:
                throw new Exception("WRONG RANDOM RANGE");
            }
          case SoundEffectType.ModeSelectButton:
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.ModeSelectButton.soundeffect, Volume, Pitch, Pan);
            break;
          case SoundEffectType.Rotate:
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Rotate.soundeffect, Volume, Pitch, Pan);
            break;
          case SoundEffectType.Gun:
            int num3 = Game1.Rnd.Next(0, 1);
            float Pitch3 = (float) Game1.Rnd.Next(0, 10) * 0.1f;
            switch (num3)
            {
              case 0:
                SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Gun1.soundeffect, Volume, Pitch3, Pan);
                return;
              case 1:
                SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Gun2.soundeffect, Volume, Pitch3, Pan);
                return;
              case 2:
                SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.Gun3.soundeffect, Volume, Pitch3, Pan);
                return;
              default:
                throw new Exception("WRONG RANDOM RANGE");
            }
          case SoundEffectType.Stamp:
            SoundEffectsManager.PlaySoundEffect(SoundEffectsManager.StampClick.soundeffect, Volume, Pitch, Pan);
            break;
        }
      }
    }

    internal static void PlaySoundEffect(SoundEffect thisSound, float Vol = 1f, float Pitch = 0.0f, float Pan = 0.0f)
    {
      if (DebugFlags.SFXDisabled || thisSound == null)
        return;
      SoundEffectInstance instance = thisSound.CreateInstance();
      if (instance == null)
        return;
      instance.Volume = Vol * SoundEffectsManager.SFXVolume;
      instance.Pitch = Pitch;
      instance.Pan = Pan;
      instance.Play();
      SoundEffectsManager.ActiveInstances.Add(instance);
    }

    internal static void CleanSoundEffects(float DeltaTime)
    {
      SoundEffectsManager.SingleClickTime -= DeltaTime;
      SoundEffectsManager.CLIKTME -= DeltaTime;
      for (int index = SoundEffectsManager.ActiveInstances.Count - 1; index >= 0; --index)
      {
        if (SoundEffectsManager.ActiveInstances[index] != null && SoundEffectsManager.ActiveInstances[index].State == SoundState.Stopped)
        {
          SoundEffectsManager.ActiveInstances[index].Dispose();
          SoundEffectsManager.ActiveInstances.RemoveAt(index);
        }
      }
      LoopingSoundHandler.UpdateLoopsounds(DeltaTime);
      AmbienceHandler.UpdateAmbienceHandler(DeltaTime);
    }

    internal static void ClearSoundEffects(float DeltaTime)
    {
      for (int index = SoundEffectsManager.ActiveInstances.Count - 1; index >= 0; --index)
      {
        SoundEffectsManager.ActiveInstances[index].Dispose();
        SoundEffectsManager.ActiveInstances.RemoveAt(index);
      }
    }

    internal static void OnGameDeactivated()
    {
      if (SoundEffectsManager.ActiveInstances == null)
        return;
      for (int index = 0; index < SoundEffectsManager.ActiveInstances.Count; ++index)
      {
        if (SoundEffectsManager.ActiveInstances[index] != null)
          SoundEffectsManager.ActiveInstances[index].Pause();
      }
    }

    internal static void OnGameActivated()
    {
      if (SoundEffectsManager.ActiveInstances == null)
        return;
      for (int index = 0; index < SoundEffectsManager.ActiveInstances.Count; ++index)
      {
        if (SoundEffectsManager.ActiveInstances[index] != null)
          SoundEffectsManager.ActiveInstances[index].Resume();
      }
    }
  }
}
