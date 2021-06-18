// Decompiled with JetBrains decompiler
// Type: TinyZoo.Audio.Animals.AnimalAudioData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Audio.Animals
{
  internal class AnimalAudioData
  {
    private static AnimalAudioInfo[] info;

    internal static AnimalAudioInfo GetAnimalAudioInfo(AnimalType animal)
    {
      if (AnimalAudioData.info == null)
        AnimalAudioData.info = new AnimalAudioInfo[56];
      if (AnimalAudioData.info[(int) animal] == null)
      {
        switch (animal)
        {
          case AnimalType.Rabbit:
            AnimalAudioData.info[(int) animal] = new AnimalAudioInfo();
            AnimalAudioData.info[(int) animal].AddSet(new AnimalAudioSet(SoundEffectType.Rabbit_LessCute01, SoundEffectType.Rabbit_LessCute03, 2, 5, 0.3f, 0.5f, false));
            AnimalAudioData.info[(int) animal].AddSet(new AnimalAudioSet(SoundEffectType.Rabbit_Cute01, SoundEffectType.Rabbit_Cute03, 2, 5, 0.3f, 0.5f, false));
            AnimalAudioData.info[(int) animal].AddSet(new AnimalAudioSet(SoundEffectType.Rabbit_Tiny, SoundEffectType.Rabbit_Tiny2, 3, 6, 0.3f, 0.5f, false, _PitchMax: 0.3f));
            break;
          case AnimalType.Goose:
            AnimalAudioData.info[(int) animal] = new AnimalAudioInfo();
            AnimalAudioData.info[(int) animal].AddSet(new AnimalAudioSet(SoundEffectType.Goose_Buck01, SoundEffectType.Goose_Buck06, 1, 4, 1.4f, 2f, true));
            AnimalAudioData.info[(int) animal].AddSet(new AnimalAudioSet(SoundEffectType.Goose_Call01, SoundEffectType.Goose_Call06, 1, 4, 1f, 1.4f, true));
            AnimalAudioData.info[(int) animal].AddSet(new AnimalAudioSet(SoundEffectType.Goose_Honk01, SoundEffectType.Goose_Honk11, 3, 8, 0.21f, 0.35f, false, _PitchMax: 0.3f));
            AnimalAudioData.info[(int) animal].AddSet(AnimalAudioData.info[(int) animal].animalaudiosets[AnimalAudioData.info[(int) animal].animalaudiosets.Count - 1]);
            AnimalAudioData.info[(int) animal].AddSet(new AnimalAudioSet(SoundEffectType.Goose_SmallHonk01, SoundEffectType.Goose_SmallHonk06, 3, 8, 0.1f, 0.15f, false));
            break;
        }
      }
      return AnimalAudioData.info[(int) animal];
    }
  }
}
