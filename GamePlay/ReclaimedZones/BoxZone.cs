// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.ReclaimedZones.BoxZone
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.BoxZones;
using TinyZoo.GamePlay.HUD;

namespace TinyZoo.GamePlay.ReclaimedZones
{
  internal class BoxZone
  {
    private GameObject BoxEr;
    public Vector2 TopLeft;
    public Vector2 BottomRight;
    public bool IsDestroyed;
    public bool HasPerson;
    public bool TempHasPerson;
    private ClearMessage clearmessage;
    private bool IsClear;
    private ClearParticles clearparticles;
    private bool WaitingToLaunchParticles;

    public BoxZone(Vector2 _TopLeft, Vector2 _BottomRight)
    {
      this.BoxEr = new GameObject();
      this.BoxEr.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.BoxEr.SetAlpha(0.2f);
      this.TopLeft = _TopLeft;
      this.BottomRight = _BottomRight;
      this.SetClear();
    }

    private void SetClear()
    {
      this.BoxEr.SetAlpha(0.8f);
      this.BoxEr.SetAllColours(0.1f, 0.8f, 0.1f);
      if (TinyZoo.Game1.gamestate == GAMESTATE.GamePlay)
        this.clearmessage = new ClearMessage((this.BottomRight - this.TopLeft) * 0.5f + this.TopLeft);
      this.IsClear = true;
      if (TinyZoo.Game1.gamestate == GAMESTATE.GamePlay)
        this.clearparticles = new ClearParticles(this.TopLeft, this.BottomRight);
      if (TinyZoo.Game1.gamestate != GAMESTATE.GamePlay)
        return;
      this.WaitingToLaunchParticles = true;
    }

    public void LaunchProgressParticles()
    {
      if (!this.WaitingToLaunchParticles)
        return;
      if (this.IsClear)
      {
        this.WaitingToLaunchParticles = false;
        ProgressBar.AddProgressParticles(this.TopLeft, this.BottomRight);
      }
      else
        this.WaitingToLaunchParticles = false;
    }

    public void ForceSetSafeAfterDeath()
    {
      this.SetClear();
      this.WaitingToLaunchParticles = false;
      ProgressBar.AddProgressParticles(this.TopLeft, this.BottomRight);
    }

    public void SetDestroyed() => this.IsDestroyed = true;

    public bool IsThisInThisZone(Vector2 location) => (double) location.X >= (double) this.TopLeft.X && (double) location.Y >= (double) this.TopLeft.Y && ((double) location.X <= (double) this.BottomRight.X && (double) location.Y <= (double) this.BottomRight.Y);

    public void SetHasPerson()
    {
      this.IsClear = false;
      this.BoxEr.SetAlpha(0.2f);
      this.clearmessage = (ClearMessage) null;
      this.HasPerson = true;
      this.BoxEr.SetAllColours(0.8f, 0.2f, 0.2f);
    }

    public void UpdateBoxZone(float DeltaTime)
    {
      if (this.clearmessage != null)
        this.clearmessage.UpdateClearMessage(DeltaTime);
      if (!this.IsClear || this.clearparticles == null)
        return;
      this.clearparticles.UpdateClearParticles(DeltaTime);
    }

    public void DrawBoxZone()
    {
      if (this.IsClear)
      {
        if (this.clearparticles != null)
          this.clearparticles.DrawClearParticles();
        this.BoxEr.SetAlpha(0.3f);
        this.BoxEr.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet, this.TopLeft, (this.BottomRight - this.TopLeft) * Sengine.ScreenRationReductionMultiplier, 0.0f);
        this.BoxEr.SetAlpha(0.2f);
        this.BoxEr.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, this.TopLeft, (this.BottomRight - this.TopLeft) * Sengine.ScreenRationReductionMultiplier, 0.0f);
        if (this.clearmessage == null)
          return;
        this.clearmessage.vLocation = (this.BottomRight - this.TopLeft) * 0.5f + this.TopLeft;
        this.clearmessage.DrawClearMessage();
      }
      else
      {
        if (GameFlags.BountyMode)
          return;
        this.BoxEr.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet, this.TopLeft, (this.BottomRight - this.TopLeft) * Sengine.ScreenRationReductionMultiplier, 0.0f);
      }
    }
  }
}
