// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.HUD.StatusArea.Survivors
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.GamePlay.HUD.StatusArea
{
  internal class Survivors
  {
    private GameObject Guy;
    private LerpHandler_Float ProgressLerper;

    public Survivors()
    {
      this.ProgressLerper = new LerpHandler_Float();
      this.ProgressLerper.SetLerp(true, -1f, -1f, 3f);
      this.Guy = new GameObject();
      this.Guy.DrawRect = new Rectangle(96, 225, 5, 8);
      this.Guy.SetDrawOriginToCentre();
      this.Guy.scale = RenderMath.GetPixelSizeBestMatch(1f);
    }

    public void UpdateSurvivors(float DeltaTime)
    {
      if (FeatureFlags.ShowGamePlayPeopleToSave)
      {
        if ((double) this.ProgressLerper.TargetValue != 0.0)
          this.ProgressLerper.SetLerp(false, -1f, 0.0f, 3f);
      }
      else if ((double) this.ProgressLerper.TargetValue != -1.0)
        this.ProgressLerper.SetLerp(false, -1f, -1f, 3f);
      this.ProgressLerper.UpdateLerpHandler(DeltaTime);
    }

    public void DrawSurvivors(Vector2 Offset)
    {
      Offset.Y += this.ProgressLerper.Value * 150f;
      this.Guy.scale = 4f;
      this.Guy.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset);
      TextFunctions.DrawTextWithDropShadow("x " + (object) GameFlags.EnemyCount + "/" + (object) GameFlags.EnemyCountAtStart, 3f, Offset + new Vector2(20f, -10f), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03, false);
    }
  }
}
