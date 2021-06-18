// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment.TrampolineBouncer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment
{
  internal class TrampolineBouncer
  {
    public AnimalRenderMan animalrenderer;
    private Vector2 OriginalLocation;
    private int Bounces;
    private PerchPoint perchpoint;
    private bool IsBouncing_SkipPerchPoint;

    public TrampolineBouncer(AnimalRenderMan _animalrenderer, Vector2 TileRendererLocation)
    {
      this.perchpoint = new PerchPoint(TileRendererLocation + new Vector2(-8f, -14f), true);
      this.animalrenderer = _animalrenderer;
      this.Bounces = Game1.Rnd.Next(4, 30);
      this.perchpoint.AttachAnimalToPerchPoint(this.animalrenderer);
    }

    public void ForceDetach() => this.perchpoint.ForceDetach();

    public bool UpdateTrampolineBouncer(float DeltaTime, out bool StartedJump)
    {
      StartedJump = false;
      if (!this.IsBouncing_SkipPerchPoint && this.perchpoint.UpdatePerchPoint(DeltaTime))
        return true;
      if (this.perchpoint.jumptotargetandback.attachmentjumpstate == JumpToTargetState.AtTarget)
      {
        if (!this.IsBouncing_SkipPerchPoint)
        {
          this.animalrenderer.enemyrenderere.animator.UnStopJumping();
          this.IsBouncing_SkipPerchPoint = true;
        }
        if ((double) this.animalrenderer.enemyrenderere.animator.PositionalOffset.Y == 0.0)
        {
          if (this.Bounces > 0)
          {
            StartedJump = true;
            this.animalrenderer.enemyrenderere.animator.TryToJumpForAttack(new Vector2(0.0f, -50f));
            --this.Bounces;
          }
          else
          {
            this.IsBouncing_SkipPerchPoint = false;
            this.perchpoint.ForceRelease();
          }
        }
      }
      return false;
    }

    public void DrawTrampolineBouncer(bool IsPreDraw)
    {
      this.perchpoint.DrawPerchPoint(IsPreDraw);
      if (!Z_DebugFlags.DrawWhiteStuffOnPerch)
        return;
      this.perchpoint.DebugDrawPerchPoint();
    }
  }
}
