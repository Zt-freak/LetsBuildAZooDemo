// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.HUD.ProgressBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.BoxZones;
using TinyZoo.GamePlay.HUD.StatusArea;

namespace TinyZoo.GamePlay.HUD
{
  internal class ProgressBar
  {
    private UIBar uibar;
    private float LastFullness;
    private float TargetFullness;
    private float TransitionStart;
    private GameObject Cleaar;
    private LerpHandler_Float ProgressLerper;
    private List<BoxParticle> boxparticles;
    private static HintParticlesManager hintparticlemanager;

    public ProgressBar()
    {
      this.uibar = new UIBar(new Vector2(128f, 16f), 4f, TinyZoo.Game1.WhitePixelRect, true);
      this.uibar.SetUpGreenBar(false);
      this.LastFullness = 0.0f;
      this.ProgressLerper = new LerpHandler_Float();
      this.ProgressLerper.SetLerp(true, -1f, -1f, 3f);
      this.Cleaar = new GameObject();
      this.Cleaar.DrawRect = new Rectangle(228, 225, 45, 11);
      this.Cleaar.vLocation.X = -60f;
      this.Cleaar.scale = 2f;
      this.Cleaar.SetDrawOriginToCentre();
      this.boxparticles = new List<BoxParticle>();
      float num = 6.4f;
      for (int index = 0; index < 20; ++index)
        this.boxparticles.Add(new BoxParticle(new Vector2(num * (float) index, -6f), new Vector2(num * (float) (index + 1), 3f)));
      ProgressBar.hintparticlemanager = new HintParticlesManager();
    }

    public void UpdateProgressBar(float DeltaTime)
    {
      if (FeatureFlags.ShowGamePlayProgressBar)
      {
        if ((double) this.ProgressLerper.TargetValue != 0.0)
          this.ProgressLerper.SetLerp(false, -1f, 0.0f, 3f);
      }
      else if ((double) this.ProgressLerper.TargetValue != -1.0)
        this.ProgressLerper.SetLerp(false, -1f, -1f, 3f);
      this.ProgressLerper.UpdateLerpHandler(DeltaTime);
      float num = MathHelper.Clamp((float) (GameFlags.CurrentReclamedZones / GameFlags.FullZoneSize * (1M / (Decimal) GameFlags.TargetPercent)), 0.0f, 1f);
      if ((double) this.TargetFullness != (double) num)
        this.TargetFullness = num;
      if ((double) this.TargetFullness > (double) this.LastFullness && (double) this.ProgressLerper.Value == 0.0)
      {
        this.LastFullness += DeltaTime * 0.3f;
        this.uibar.SetWhiteFlash(0.3f);
        if ((double) this.LastFullness > (double) this.TargetFullness)
          this.LastFullness = this.TargetFullness;
      }
      this.uibar.SetFullness(this.LastFullness);
      this.uibar.UpdateUIBar(DeltaTime);
      for (int index = 0; index < 20; ++index)
        this.boxparticles[index].UpdateBoxParticle(DeltaTime);
      ProgressBar.hintparticlemanager.UpdateHintParticlesManager(DeltaTime);
    }

    internal static void AddProgressParticles(Vector2 _TopLeft, Vector2 _BottomRight) => ProgressBar.hintparticlemanager.AddParticles(_TopLeft, _BottomRight);

    public void DrawProgressBar(Vector2 Offset)
    {
      Offset.Y += this.ProgressLerper.Value * 150f;
      this.uibar.DrawUI_Bar(AssetContainer.pointspritebatch03, Offset, AssetContainer.SpriteSheet);
      for (int index = 0; index < (int) (20.0 * (double) this.LastFullness); ++index)
        this.boxparticles[index].BarDrawBoxParticle(Offset + this.uibar.vLocation);
      this.Cleaar.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset);
      ProgressBar.hintparticlemanager.DrawHintParticlesManager(Offset + this.uibar.vLocation + new Vector2(3f, 0.0f));
    }
  }
}
