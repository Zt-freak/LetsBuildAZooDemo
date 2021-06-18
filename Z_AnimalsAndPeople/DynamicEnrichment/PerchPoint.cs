// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment.PerchPoint
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Z_Animal_Data;

namespace TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment
{
  internal class PerchPoint
  {
    public Vector2 LocationInWorldSpace;
    private GameObject gobject;
    public AnimalRenderMan animalrenderer;
    private float HoldTime;
    private bool HasReachedPerch;
    public JumpToTargetAndBackHandler jumptotargetandback;
    private bool ForceStayOnPerchUntilReleased;
    private bool StartedBehind;
    public bool DrawBehind;
    public AnimatedPerchInfo animatedperchinfo;
    public bool JustLandedOnPerch;
    private bool WillPostDraw;

    public PerchPoint(
      Vector2 _LocInWorldSpace,
      bool _ForceStayOnPerchUntilReleased = false,
      bool _DrawBehind = false)
    {
      this.DrawBehind = _DrawBehind;
      this.ForceStayOnPerchUntilReleased = _ForceStayOnPerchUntilReleased;
      this.gobject = new GameObject();
      this.gobject.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.gobject.SetDrawOriginToCentre();
      this.gobject.scale = 2f;
      this.gobject.vLocation = _LocInWorldSpace;
      this.LocationInWorldSpace = _LocInWorldSpace;
    }

    public void AttachAnimalToPerchPoint(AnimalRenderMan _animalrenderer)
    {
      this.HasReachedPerch = false;
      this.jumptotargetandback = new JumpToTargetAndBackHandler(this.LocationInWorldSpace, _animalrenderer);
      this.StartedBehind = (double) this.LocationInWorldSpace.Y > (double) _animalrenderer.enemyrenderere.vLocation.Y;
      this.animalrenderer = _animalrenderer;
      this.animalrenderer.IsBeingEnriched = true;
      this.animalrenderer.BlockWalkingAndRendering = true;
      this.HoldTime = (float) TinyZoo.Game1.Rnd.Next(5, 35);
      this.HoldTime *= 0.2f;
    }

    public Vector2 GetPerchPointLocationInWorldSpace() => this.LocationInWorldSpace;

    public bool HasAnimal() => this.animalrenderer != null;

    public void ForceRelease()
    {
      this.ForceStayOnPerchUntilReleased = false;
      this.HoldTime = 0.0f;
    }

    public void ForceDetach()
    {
      this.animalrenderer.IsBeingEnriched = false;
      this.animalrenderer.BlockWalkingAndRendering = false;
      this.animalrenderer.enemyrenderere.animator.UnStopJumping();
      this.animalrenderer.LastInteractionPoint_UID = -1;
      this.animalrenderer.enemyrenderere.vLocation = this.jumptotargetandback.OriginalLocation;
      this.jumptotargetandback = (JumpToTargetAndBackHandler) null;
      this.animalrenderer.EnrchmentDelay = 5f;
    }

    public bool UpdatePerchPoint(float DeltaTime)
    {
      if (this.jumptotargetandback != null)
      {
        bool JustArraivedAtTarget;
        if (this.jumptotargetandback.UpdateJumpToTargetAndBackHandler(out JustArraivedAtTarget))
        {
          this.animalrenderer.IsBeingEnriched = false;
          this.animalrenderer.BlockWalkingAndRendering = false;
          this.animalrenderer.EnrchmentDelay = 5f;
          this.animalrenderer = (AnimalRenderMan) null;
          this.jumptotargetandback = (JumpToTargetAndBackHandler) null;
          return true;
        }
        this.animalrenderer.enemyrenderere.vLocation = this.jumptotargetandback.OriginalLocation;
        if (JustArraivedAtTarget)
        {
          this.JustLandedOnPerch = true;
          this.animalrenderer.BlockWalkingAndRendering = true;
          this.animalrenderer.enemyrenderere.animator.StopJumping();
        }
        if (this.jumptotargetandback.attachmentjumpstate == JumpToTargetState.AtTarget && (double) this.HoldTime >= 0.0 && !this.ForceStayOnPerchUntilReleased)
        {
          this.animalrenderer.enemyrenderere.vLocation = this.LocationInWorldSpace;
          this.HoldTime -= DeltaTime;
          if ((double) this.HoldTime <= 0.0)
            this.jumptotargetandback.JumpBack();
        }
      }
      return false;
    }

    public void DrawPerchPoint(bool IsPreDraw)
    {
      if (this.animalrenderer == null || !this.animalrenderer.BlockWalkingAndRendering)
        return;
      if (this.StartedBehind || this.DrawBehind)
      {
        if (IsPreDraw)
        {
          if (this.jumptotargetandback.attachmentjumpstate == JumpToTargetState.JumpingTooAttachmentLoc || this.jumptotargetandback.attachmentjumpstate == JumpToTargetState.WaitingToJump || (this.jumptotargetandback.attachmentjumpstate == JumpToTargetState.JumpingBack || this.jumptotargetandback.attachmentjumpstate == JumpToTargetState.Complete) || this.DrawBehind)
          {
            if (this.jumptotargetandback.attachmentjumpstate == JumpToTargetState.JumpingTooAttachmentLoc && this.animalrenderer.enemyrenderere.animator.IsJumping && this.animalrenderer.enemyrenderere.animator.GoingUpwards)
              this.WillPostDraw = false;
            else if (this.jumptotargetandback.attachmentjumpstate == JumpToTargetState.JumpingBack && this.animalrenderer.enemyrenderere.animator.IsJumping && !this.animalrenderer.enemyrenderere.animator.GoingUpwards)
              this.WillPostDraw = false;
            else if (this.jumptotargetandback.attachmentjumpstate == JumpToTargetState.AtTarget && this.DrawBehind)
              this.WillPostDraw = false;
            if (!this.WillPostDraw)
            {
              if (this.jumptotargetandback.ShouldForceLocatonOnDraw())
                this.animalrenderer.enemyrenderere.vLocation = this.LocationInWorldSpace;
              this.animalrenderer.DrawBlockRenderAnimal();
              return;
            }
          }
        }
        else if (!this.WillPostDraw)
          return;
      }
      else if (IsPreDraw)
        return;
      if (this.jumptotargetandback.ShouldForceLocatonOnDraw())
        this.animalrenderer.enemyrenderere.vLocation = this.LocationInWorldSpace;
      this.animalrenderer.DrawBlockRenderAnimal();
    }

    public void DebugDrawPerchPoint() => this.gobject.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
  }
}
