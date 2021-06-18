// Decompiled with JetBrains decompiler
// Type: TinyZoo.Settings.SetDrone
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Objects;

namespace TinyZoo.Settings
{
  internal class SetDrone : GameObject
  {
    private DualSinOscillator oscialltor;
    private LerpHandler_Float lerper;
    private bool exiting;

    public SetDrone()
    {
      this.DrawRect = new Rectangle(325, 719, 142, 62);
      this.SetDrawOriginToCentre();
      this.oscialltor = new DualSinOscillator(0.4f, 0.2f);
      this.scale = 3f;
      this.lerper = new LerpHandler_Float();
    }

    public void UpdateDrone(float DeltaTime)
    {
      if (this.exiting && (double) this.lerper.TargetValue != -1.0)
        this.lerper.SetLerp(false, 0.0f, -1f, 3f, true);
      else if (!this.exiting && (double) this.lerper.TargetValue != 0.0)
        this.lerper.SetLerp(false, 0.0f, 0.0f, 3f, true);
      this.exiting = false;
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.oscialltor.UpdateDualSinOscillator(DeltaTime);
    }

    public void Exit() => this.exiting = true;

    public void DrawDrone(Vector2 Offset)
    {
      Offset.X += this.lerper.Value * 612f;
      this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + this.oscialltor.CurrentOffset * 10f);
    }
  }
}
