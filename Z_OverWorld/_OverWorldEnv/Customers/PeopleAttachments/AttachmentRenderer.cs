// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.Customers.PeopleAttachments.AttachmentRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using TinyZoo.OverWorld.OverWorldEnv.Customers;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.Customers.PeopleAttachments
{
  internal class AttachmentRenderer : GameObject
  {
    private bool WillPreDraw;
    public AttachmentInfo REF_attachmentinfo;
    private float SelfAnimationTimer;
    private int LocalFrame;

    public AttachmentRenderer(AttachmentInfo attachmentinfo) => this.REF_attachmentinfo = attachmentinfo;

    public void SetFrame(
      WalkingPerson walkingperson,
      int AnimFrame,
      ref int CurrentLegSlice,
      ref Vector2 Offset,
      bool CharacterAnimationBlocked,
      bool WasCalledFromIdle = false)
    {
      this.FlipRender = false;
      if (!(this.REF_attachmentinfo.HasIdle & CharacterAnimationBlocked))
        this.LocalFrame = AnimFrame;
      CharacterAnimationBlocked |= this.REF_attachmentinfo.BlocksCharacterAnimation;
      switch (walkingperson.directionmoving)
      {
        case DirectionPressed.Up:
          this.DrawRect = this.REF_attachmentinfo.Front;
          this.WillPreDraw = this.REF_attachmentinfo.attachmentlocation != AttachmentLocation.Head;
          this.FlipRender = true;
          if (this.REF_attachmentinfo.WillSliceLegs)
            CurrentLegSlice = this.REF_attachmentinfo.LegSliceUp;
          if (this.REF_attachmentinfo.UsesOffsets)
            Offset = this.REF_attachmentinfo.OffsetUp;
          if (this.REF_attachmentinfo.HasCustomBackRect)
          {
            this.DrawRect = this.REF_attachmentinfo.Up;
            this.FlipRender = false;
          }
          this.DrawOrigin = this.REF_attachmentinfo.RelativeDrawOrigin_Up;
          if (this.REF_attachmentinfo.MoveInCode)
          {
            if (this.REF_attachmentinfo.attachmentlocation == AttachmentLocation.Waist)
              break;
            if (this.REF_attachmentinfo.attachmentlocation == AttachmentLocation.Head)
            {
              if (this.LocalFrame != 1 && this.LocalFrame != 3)
                break;
              --this.DrawOrigin.Y;
              break;
            }
            if (this.LocalFrame != 3)
              break;
            --this.DrawOrigin.Y;
            break;
          }
          if (this.REF_attachmentinfo.HasIdle && !CharacterAnimationBlocked)
            this.SelfAnimationTimer = 0.0f;
          this.DrawRect.X += this.LocalFrame * (this.DrawRect.Width + 1);
          break;
        case DirectionPressed.Right:
          this.DrawRect = this.REF_attachmentinfo.Right;
          this.DrawOrigin = this.REF_attachmentinfo.RelativeDrawOrigin_Right;
          this.WillPreDraw = this.REF_attachmentinfo.attachmentlocation != AttachmentLocation.Head && this.REF_attachmentinfo.attachmentlocation != AttachmentLocation.RightHand && this.REF_attachmentinfo.attachmentlocation != AttachmentLocation.BothHands;
          if (this.REF_attachmentinfo.WillSliceLegs)
            CurrentLegSlice = this.REF_attachmentinfo.LegSliceRight;
          if (this.REF_attachmentinfo.UsesOffsets)
            Offset = this.REF_attachmentinfo.OffsetRight;
          if (this.REF_attachmentinfo.MoveInCode)
          {
            if (this.REF_attachmentinfo.attachmentlocation == AttachmentLocation.Waist)
              break;
            if (this.REF_attachmentinfo.attachmentlocation == AttachmentLocation.Head || this.REF_attachmentinfo.attachmentlocation == AttachmentLocation.BothHands)
            {
              if (this.LocalFrame != 1 && this.LocalFrame != 3)
                break;
              --this.DrawOrigin.Y;
              break;
            }
            if (this.LocalFrame == 1)
            {
              --this.DrawOrigin.Y;
              --this.DrawOrigin.X;
              break;
            }
            if (this.LocalFrame != 3)
              break;
            --this.DrawOrigin.Y;
            ++this.DrawOrigin.X;
            break;
          }
          if (this.REF_attachmentinfo.HasIdle && !CharacterAnimationBlocked)
            this.SelfAnimationTimer = 0.0f;
          this.DrawRect.X += this.LocalFrame * (this.DrawRect.Width + 1);
          break;
        case DirectionPressed.Down:
          this.DrawRect = this.REF_attachmentinfo.Front;
          this.WillPreDraw = this.REF_attachmentinfo.attachmentlocation == AttachmentLocation.Waist;
          this.DrawOrigin = this.REF_attachmentinfo.RelativeDrawOrigin_Down;
          if (this.REF_attachmentinfo.MoveInCode)
          {
            if (this.REF_attachmentinfo.attachmentlocation != AttachmentLocation.Waist)
            {
              if (this.REF_attachmentinfo.attachmentlocation == AttachmentLocation.Head || this.REF_attachmentinfo.attachmentlocation == AttachmentLocation.BothHands)
              {
                if (this.LocalFrame == 1 || this.LocalFrame == 3)
                  --this.DrawOrigin.Y;
              }
              else if (this.REF_attachmentinfo.attachmentlocation == AttachmentLocation.RightHand)
              {
                if (this.LocalFrame == 1)
                {
                  --this.DrawOrigin.Y;
                  --this.DrawOrigin.Y;
                }
              }
              else if (this.LocalFrame == 3)
                --this.DrawOrigin.Y;
            }
          }
          else
          {
            if (this.REF_attachmentinfo.HasIdle && !CharacterAnimationBlocked)
              this.SelfAnimationTimer = 0.0f;
            this.DrawRect.X += this.LocalFrame * (this.DrawRect.Width + 1);
          }
          if (this.REF_attachmentinfo.WillSliceLegs)
            CurrentLegSlice = this.REF_attachmentinfo.LegSliceDown;
          if (!this.REF_attachmentinfo.UsesOffsets)
            break;
          Offset = this.REF_attachmentinfo.OffsetDown;
          break;
        case DirectionPressed.Left:
          this.DrawRect = this.REF_attachmentinfo.Right;
          this.WillPreDraw = this.REF_attachmentinfo.attachmentlocation == AttachmentLocation.RightHand || this.REF_attachmentinfo.attachmentlocation == AttachmentLocation.Waist;
          if ((this.REF_attachmentinfo.attachmentlocation == AttachmentLocation.Head || this.REF_attachmentinfo.attachmentlocation == AttachmentLocation.BothHands) && (this.LocalFrame == 1 || this.LocalFrame == 3))
            --this.DrawOrigin.Y;
          if (this.REF_attachmentinfo.WillSliceLegs)
            CurrentLegSlice = this.REF_attachmentinfo.LegSliceLeft;
          if (this.REF_attachmentinfo.UsesOffsets)
            Offset = this.REF_attachmentinfo.OffsetLeft;
          this.FlipRender = true;
          this.DrawOrigin = this.REF_attachmentinfo.RelativeDrawOrigin_Left;
          if (this.REF_attachmentinfo.MoveInCode)
          {
            if (this.REF_attachmentinfo.attachmentlocation == AttachmentLocation.Waist)
              break;
            if (this.REF_attachmentinfo.attachmentlocation == AttachmentLocation.Head || this.REF_attachmentinfo.attachmentlocation == AttachmentLocation.BothHands)
            {
              if (this.LocalFrame != 1 && this.LocalFrame != 3)
                break;
              --this.DrawOrigin.Y;
              break;
            }
            if (this.LocalFrame == 1)
            {
              --this.DrawOrigin.Y;
              ++this.DrawOrigin.X;
              break;
            }
            if (this.LocalFrame != 3)
              break;
            --this.DrawOrigin.Y;
            --this.DrawOrigin.X;
            break;
          }
          if (this.REF_attachmentinfo.HasIdle && !CharacterAnimationBlocked)
            this.SelfAnimationTimer = 0.0f;
          this.DrawRect.X += this.LocalFrame * (this.DrawRect.Width + 1);
          break;
      }
    }

    public void UpdateAttachmentRenderer(
      WalkingPerson parent,
      float DeltaTime,
      ref int LegSlice,
      ref Vector2 RenderOffset,
      ref bool CharacterAnimationBlocked)
    {
      if (!this.REF_attachmentinfo.HasIdle)
        return;
      this.SelfAnimationTimer += DeltaTime;
      if ((double) this.SelfAnimationTimer <= 0.25)
        return;
      ++this.LocalFrame;
      if (this.LocalFrame > 3)
        this.LocalFrame = 0;
      this.SelfAnimationTimer = 0.0f;
      this.SetFrame(parent, this.LocalFrame, ref LegSlice, ref RenderOffset, CharacterAnimationBlocked);
    }

    public void PreDraw(WalkingPerson parent)
    {
      this.vLocation = parent.vLocation;
      if (!this.WillPreDraw)
        return;
      this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet);
    }

    public void PostDraw(WalkingPerson parent)
    {
      if (this.WillPreDraw)
        return;
      this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet);
    }
  }
}
