// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.HUD.StatusArea.BeamCounter
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.beams;

namespace TinyZoo.GamePlay.HUD.StatusArea
{
  internal class BeamCounter
  {
    private GameObject beamThing;
    private float FlashTimer;
    private LerpHandler_Float ProgressLerper;

    public BeamCounter()
    {
      this.ProgressLerper = new LerpHandler_Float();
      this.ProgressLerper.SetLerp(true, -1f, -1f, 3f);
      this.beamThing = new GameObject();
      this.beamThing.DrawRect = BeamCenter.BeamRect;
      this.beamThing.SetDrawOriginToCentre();
      this.beamThing.scale = 2f;
    }

    public void UpdateBeamCounter(float DeltaTime)
    {
      if (FeatureFlags.ShowGamePlayBeams)
      {
        if ((double) this.ProgressLerper.TargetValue != 0.0)
          this.ProgressLerper.SetLerp(false, -1f, 0.0f, 3f);
      }
      else if ((double) this.ProgressLerper.TargetValue != -1.0)
        this.ProgressLerper.SetLerp(false, -1f, -1f, 3f);
      this.ProgressLerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.FlashTimer < 0.5)
      {
        this.FlashTimer += DeltaTime;
        if ((double) this.FlashTimer < 0.5)
          return;
        this.beamThing.DrawRect = BeamCenter.BeamRect;
        this.beamThing.DrawRect.X += 6;
      }
      else
      {
        if ((double) this.FlashTimer >= 0.699999988079071)
          return;
        this.FlashTimer += DeltaTime;
        if ((double) this.FlashTimer < 0.699999988079071)
          return;
        this.beamThing.DrawRect = BeamCenter.BeamRect;
        this.FlashTimer = 0.0f;
      }
    }

    public void DrawBeamCounter(Vector2 Offset)
    {
      Offset.Y += this.ProgressLerper.Value * 150f;
      this.beamThing.scale = 4f;
      this.beamThing.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset);
      if (GameFlags.IsArcadeMode && GameFlags.DifficultyIsEasy)
        TextFunctions.DrawTextWithDropShadow("--", 3f, Offset + new Vector2(20f, -10f), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03, false);
      else if (!GameFlags.IsArcadeMode && GameFlags.BountyMode)
        TextFunctions.DrawTextWithDropShadow("--", 3f, Offset + new Vector2(20f, -10f), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03, false);
      else
        TextFunctions.DrawTextWithDropShadow("x " + (object) GameFlags.CurrentBeamInventory + "/" + (object) GameFlags.BeamInventoryAtStart, 3f, Offset + new Vector2(20f, -10f), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03, false);
    }
  }
}
