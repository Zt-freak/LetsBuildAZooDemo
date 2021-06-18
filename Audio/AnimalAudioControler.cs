// Decompiled with JetBrains decompiler
// Type: TinyZoo.Audio.AnimalAudioControler
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.Audio.Animals;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Audio
{
  internal class AnimalAudioControler
  {
    private static int UID;
    public bool IsActive;
    private AnimalAudioSet animalaudioset;
    private float DefaultGap = 10f;
    private float Timer;
    private float Pitch;
    private string SOUND_ID;
    private int PlayThisMany;
    private SmartSFXHolder currentsound;

    public AnimalAudioControler()
    {
      this.SOUND_ID = "an" + (object) AnimalAudioControler.UID;
      ++AnimalAudioControler.UID;
    }

    public void StartSound(AnimalType animaltype, float WorldVolume)
    {
      if (animaltype != AnimalType.Rabbit && animaltype != AnimalType.Goose)
        return;
      this.IsActive = true;
      this.Timer = 0.0f;
      this.animalaudioset = AnimalAudioData.GetAnimalAudioInfo(animaltype).GetAudioSet();
      this.Pitch = !this.animalaudioset.HasVariablePitch ? this.animalaudioset.PitchMin : MathStuff.getRandomFloat(this.animalaudioset.PitchMin, this.animalaudioset.PitchMax);
      this.PlayThisMany = 0;
      if (this.animalaudioset.MaxRepeats > 0)
      {
        this.DefaultGap = MathStuff.getRandomFloat(this.animalaudioset.MinGap, this.animalaudioset.MaxGap);
        this.PlayThisMany = TinyZoo.Game1.Rnd.Next(this.animalaudioset.MinRepeats - 1, this.animalaudioset.MaxRepeats - 1);
      }
      this.currentsound = this.animalaudioset.EndingSound <= -1 ? LoopingSoundHandler.PlaySpecificSoundEffect(SoundEffectsManager.soundeffectscontainers[this.animalaudioset.StartingSound].soundeffect, this.SOUND_ID, this.Pitch, WorldVolume, true, false, true) : LoopingSoundHandler.PlaySpecificSoundEffect(SoundEffectsManager.soundeffectscontainers[TinyZoo.Game1.Rnd.Next(this.animalaudioset.StartingSound, this.animalaudioset.EndingSound + 1)].soundeffect, this.SOUND_ID, this.Pitch, WorldVolume, true, false, true);
      if (this.currentsound == null)
        return;
      this.IsActive = false;
    }

    public void UpdateAnimalAudioControler(float DeltaTime, bool UpdateVolume, float WorldVolume)
    {
      if (!this.IsActive)
        return;
      if (this.currentsound == null)
      {
        this.IsActive = false;
      }
      else
      {
        if (this.PlayThisMany > 0)
        {
          this.Timer += DeltaTime;
          if ((double) this.Timer >= (double) this.DefaultGap)
          {
            --this.PlayThisMany;
            this.Timer = 0.0f;
            this.currentsound = this.animalaudioset.EndingSound <= -1 ? LoopingSoundHandler.PlaySpecificSoundEffect(SoundEffectsManager.soundeffectscontainers[this.animalaudioset.StartingSound].soundeffect, this.SOUND_ID, this.Pitch, WorldVolume, true, false) : LoopingSoundHandler.PlaySpecificSoundEffect(SoundEffectsManager.soundeffectscontainers[TinyZoo.Game1.Rnd.Next(this.animalaudioset.StartingSound, this.animalaudioset.EndingSound)].soundeffect, this.SOUND_ID, this.Pitch, WorldVolume, true, false, true);
            if (this.animalaudioset.DynamicGap)
              this.DefaultGap = MathStuff.getRandomFloat(this.animalaudioset.MinGap, this.animalaudioset.MaxGap);
            if (this.animalaudioset.HasVariablePitch && this.animalaudioset.PitchIsDynamic)
              this.Pitch = MathStuff.getRandomFloat(this.animalaudioset.PitchMin, this.animalaudioset.PitchMax);
          }
        }
        if (UpdateVolume)
          this.currentsound.SetVolume(WorldVolume);
        if (this.PlayThisMany > 0 || !this.currentsound.HasFinished())
          return;
        this.IsActive = false;
      }
    }
  }
}
