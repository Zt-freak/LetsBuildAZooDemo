// Decompiled with JetBrains decompiler
// Type: TinyZoo.Audio.Animals.AnimalAudioSet
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.Audio.Animals
{
  internal class AnimalAudioSet
  {
    public int StartingSound;
    public int EndingSound = -1;
    public int MinRepeats;
    public int MaxRepeats;
    public float MinGap;
    public float MaxGap;
    public bool DynamicGap;
    public float PitchMin;
    public float PitchMax;
    public bool PitchIsDynamic;
    public bool HasVariablePitch;

    public AnimalAudioSet(
      SoundEffectType _StartingSound,
      SoundEffectType _EndingSound,
      int _MinRepeats,
      int _MaxRepeats,
      float _MinGap,
      float _MaxGap,
      bool _DynamicGap,
      float _PitchMin = 0.0f,
      float _PitchMax = 0.0f,
      bool _PitchIsDynamic = false)
    {
      this.DynamicGap = _DynamicGap;
      this.StartingSound = (int) _StartingSound;
      this.EndingSound = (int) _EndingSound;
      this.MinRepeats = _MinRepeats;
      this.MaxRepeats = _MaxRepeats;
      this.MinGap = _MinGap;
      this.MaxGap = _MaxGap;
      this.PitchIsDynamic = _PitchIsDynamic;
      this.PitchMin = _PitchMin;
      this.PitchMax = _PitchMax;
      if ((double) this.PitchMin == (double) this.PitchMax)
        return;
      this.HasVariablePitch = true;
    }

    public AnimalAudioSet(SoundEffectType Sound) => this.StartingSound = (int) Sound;
  }
}
