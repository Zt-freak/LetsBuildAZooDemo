// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.UIShake
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;

namespace TinyZoo.Z_GenericUI
{
  internal class UIShake
  {
    private double xval;
    private double yval;
    private LerpHandler_Float amplerper;
    private double speed;
    private UIScaleHelper scalehelper;
    private float ampMultiplier;
    private float maxAmplitude;

    public UIShake(float basescale, float duration, float maxAmplitude_, float speed_ = 1f)
    {
      this.scalehelper = new UIScaleHelper(basescale);
      this.speed = (double) speed_;
      this.maxAmplitude = maxAmplitude_;
      this.amplerper = new LerpHandler_Float();
      this.xval = TinyZoo.Game1.Rnd.NextDouble() * Math.PI;
      this.yval = TinyZoo.Game1.Rnd.NextDouble() * Math.PI;
      this.amplerper = new LerpHandler_Float();
      this.amplerper.SetLerp(true, 1.570796f, 3.141593f, 1f / duration);
    }

    public bool IsDone => this.amplerper.IsComplete();

    public Vector2 UpdateUIShake(float DeltaTime)
    {
      this.xval += (double) DeltaTime * Math.PI * this.speed;
      this.yval += (double) DeltaTime * Math.PI * this.speed;
      this.amplerper.UpdateLerpHandler(DeltaTime);
      this.ampMultiplier = (float) Math.Sin((double) this.amplerper.Value) * this.maxAmplitude;
      return this.scalehelper.ScaleVector2(new Vector2((float) Math.Sin(this.xval), (float) Math.Sin(this.yval))) * this.ampMultiplier;
    }
  }
}
