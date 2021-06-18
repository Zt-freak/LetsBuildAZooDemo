// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.AnimationProfile
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;

namespace TinyZoo.Z_AnimalsAndPeople
{
  internal class AnimationProfile
  {
    private float Squishiness;
    private float JumpHeight;
    private float VerticalStretch;
    public float JumpHeightVariance;
    public float JumpLengthTime;
    public float JumpLengthTimeVariance;
    public float MovementPerJump;
    private float JumpGap;
    public float JumpGapVariance;
    public float LongGap;
    public int ProbablityOfLongGap;

    public AnimationProfile(
      float _JumpHeight,
      float _JumpLengthTime,
      float _Hardness,
      float _JumpGap,
      float _VerticalStretch = -1f)
    {
      this.VerticalStretch = (double) _VerticalStretch != -1.0 ? _VerticalStretch : 1f - _Hardness;
      this.JumpGap = _JumpGap;
      this.Squishiness = _Hardness;
      this.JumpLengthTime = _JumpLengthTime;
      this.JumpHeight = _JumpHeight;
    }

    public float GetJumpTime() => (double) this.JumpLengthTimeVariance > 0.0 ? MathStuff.getRandomFloat(this.JumpLengthTime - this.JumpLengthTimeVariance * 0.5f, this.JumpLengthTime + this.JumpLengthTimeVariance * 0.5f) : this.JumpLengthTime;

    public float GetJumpGap()
    {
      if ((double) this.LongGap > 0.0 && TinyZoo.Game1.Rnd.Next(0, 100) < this.ProbablityOfLongGap)
        return this.LongGap;
      return (double) this.JumpLengthTimeVariance > 0.0 ? MathStuff.getRandomFloat(this.JumpGap - this.JumpLengthTimeVariance * 0.5f, this.JumpGap + this.JumpLengthTimeVariance * 0.5f) : this.JumpGap;
    }

    public float GetVerticalSTretchiness() => this.VerticalStretch;

    public float GetSquichiness() => this.Squishiness;

    public float GetJumpHeight() => (double) this.JumpHeightVariance > 0.0 ? MathStuff.getRandomFloat(this.JumpHeightVariance - this.JumpLengthTimeVariance * 0.5f, this.JumpHeight + this.JumpHeightVariance * 0.5f) : this.JumpHeight;
  }
}
