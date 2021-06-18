// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment.JumpToTargetAndBackHandler
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment
{
  internal class JumpToTargetAndBackHandler
  {
    public Vector2 OriginalLocation;
    public JumpToTargetState attachmentjumpstate;
    private AnimalRenderMan Refanimalrenderer;
    private Vector2 TargetLocationInWorlsSpace;
    private bool FixFrameGlitchForOrderOfOperations;
    private bool WantsToJumpBack;

    public JumpToTargetAndBackHandler(
      Vector2 _TargetLocationInWorlsSpace,
      AnimalRenderMan _animalrenderer,
      bool _FixFrameGlitchForOrderOfOperations = true)
    {
      this.FixFrameGlitchForOrderOfOperations = _FixFrameGlitchForOrderOfOperations;
      this.TargetLocationInWorlsSpace = _TargetLocationInWorlsSpace;
      this.Refanimalrenderer = _animalrenderer;
      this.OriginalLocation = _animalrenderer.enemyrenderere.vLocation;
      this.attachmentjumpstate = JumpToTargetState.WaitingToJump;
    }

    public void JumpBack() => this.WantsToJumpBack = true;

    public bool UpdateJumpToTargetAndBackHandler(out bool JustArraivedAtTarget)
    {
      JustArraivedAtTarget = false;
      if (this.attachmentjumpstate == JumpToTargetState.WaitingToJump)
      {
        if (!this.Refanimalrenderer.enemyrenderere.animator.IsJumping)
        {
          this.attachmentjumpstate = JumpToTargetState.JumpingTooAttachmentLoc;
          this.Refanimalrenderer.enemyrenderere.animator.JumpHere(this.TargetLocationInWorlsSpace, this.Refanimalrenderer.enemyrenderere.vLocation);
        }
      }
      else if (this.attachmentjumpstate == JumpToTargetState.JumpingTooAttachmentLoc)
      {
        if (!this.Refanimalrenderer.enemyrenderere.animator.IsJumping)
        {
          JustArraivedAtTarget = true;
          this.Refanimalrenderer.enemyrenderere.animator.StopJumping();
          this.attachmentjumpstate = JumpToTargetState.AtTarget;
        }
      }
      else if (this.attachmentjumpstate == JumpToTargetState.AtTarget)
      {
        if (this.WantsToJumpBack)
        {
          this.Refanimalrenderer.enemyrenderere.animator.UnStopJumping();
          this.Refanimalrenderer.enemyrenderere.animator.JumpHere(this.OriginalLocation, this.TargetLocationInWorlsSpace);
          this.attachmentjumpstate = JumpToTargetState.JumpingBack;
          this.FixFrameGlitchForOrderOfOperations = true;
        }
      }
      else if (this.attachmentjumpstate == JumpToTargetState.JumpingBack && !this.Refanimalrenderer.enemyrenderere.animator.IsJumping)
      {
        this.attachmentjumpstate = JumpToTargetState.Complete;
        return true;
      }
      return false;
    }

    public bool ShouldForceLocatonOnDraw()
    {
      if (this.FixFrameGlitchForOrderOfOperations)
      {
        if (this.attachmentjumpstate == JumpToTargetState.JumpingBack)
          return this.Refanimalrenderer.enemyrenderere.animator.IsJumping;
        if (this.attachmentjumpstate == JumpToTargetState.AtTarget || this.attachmentjumpstate == JumpToTargetState.JumpingTooAttachmentLoc && !this.Refanimalrenderer.enemyrenderere.animator.IsJumping)
          return true;
      }
      return false;
    }
  }
}
