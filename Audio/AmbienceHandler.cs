// Decompiled with JetBrains decompiler
// Type: TinyZoo.Audio.AmbienceHandler
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Audio
{
  internal class AmbienceHandler
  {
    internal static int[] Animals = new int[56];
    internal static float TotalAnimals;
    internal static float TimeToNextSound = 5f;
    internal static bool CollectSoundInfo;
    private static List<AnimalAudioControler> animalsounds = new List<AnimalAudioControler>();
    private static SmartSFXHolder NatureSmall;
    private static float WorldVolume;

    public static void UpdateAmbienceHandler(float DeltaTime)
    {
      float num1 = MathHelper.Clamp(Sengine.WorldOriginandScale.Z / 4f, 0.05f, 1f);
      bool UpdateVolume = false;
      if ((double) AmbienceHandler.WorldVolume != (double) num1)
      {
        UpdateVolume = true;
        AmbienceHandler.WorldVolume = num1;
      }
      if (TinyZoo.Game1.gamestate != GAMESTATE.OverWorld)
        return;
      if (AmbienceHandler.NatureSmall == null)
        AmbienceHandler.NatureSmall = AmbienceHandler.PlayThisAmbienceType(AmbienceType.LotsOfNature);
      else if (UpdateVolume)
        AmbienceHandler.NatureSmall.SetVolume(AmbienceHandler.WorldVolume);
      if (AmbienceHandler.CollectSoundInfo && (double) AmbienceHandler.TotalAnimals > 0.0)
      {
        int num2 = TinyZoo.Game1.Rnd.Next(0, (int) AmbienceHandler.TotalAnimals);
        for (int index = 0; index < AmbienceHandler.Animals.Length; ++index)
        {
          if (AmbienceHandler.Animals[index] > 0)
          {
            num2 -= AmbienceHandler.Animals[index];
            if (num2 <= 0)
            {
              AmbienceHandler.PlayAnimalSound((AnimalType) index);
              AmbienceHandler.CollectSoundInfo = false;
              break;
            }
          }
        }
        AmbienceHandler.CollectSoundInfo = false;
      }
      else
      {
        AmbienceHandler.TimeToNextSound -= DeltaTime;
        if ((double) AmbienceHandler.TimeToNextSound <= 0.0)
        {
          AmbienceHandler.TimeToNextSound = MathStuff.getRandomFloat(5f, 10f);
          AmbienceHandler.CollectSoundInfo = true;
        }
      }
      for (int index = 0; index < AmbienceHandler.animalsounds.Count; ++index)
        AmbienceHandler.animalsounds[index].UpdateAnimalAudioControler(DeltaTime, UpdateVolume, AmbienceHandler.WorldVolume);
    }

    private static SmartSFXHolder PlayThisAmbienceType(AmbienceType ambienceType)
    {
      switch (ambienceType)
      {
        case AmbienceType.SparceNature:
        case AmbienceType.MediumNature:
        case AmbienceType.LotsOfNature:
          return LoopingSoundHandler.PlayLoopongSoundEffects(SoundEffectType.ForestAmbience, SoundEffectType.ForestAmbience.ToString());
        default:
          return (SmartSFXHolder) null;
      }
    }

    private static void PlayAnimalSound(AnimalType animalsoundtoplay)
    {
      for (int index = 0; index < AmbienceHandler.animalsounds.Count; ++index)
      {
        if (!AmbienceHandler.animalsounds[index].IsActive)
        {
          AmbienceHandler.animalsounds[index].StartSound(animalsoundtoplay, AmbienceHandler.WorldVolume);
          return;
        }
      }
      AmbienceHandler.animalsounds.Add(new AnimalAudioControler());
      AmbienceHandler.animalsounds[AmbienceHandler.animalsounds.Count - 1].StartSound(animalsoundtoplay, AmbienceHandler.WorldVolume);
    }
  }
}
