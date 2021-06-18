// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.BigBrownPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Z_BetaEnd.BetaLock;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_GenericUI
{
  internal class BigBrownPanel
  {
    private GameObjectNineSlice BrownFrame;
    public Vector2 location;
    public Vector2 vScale;
    public BackButton close;
    private MiniHeading miniHeading;
    public Vector2 InternalOffset;
    private float EdgeBuffer;
    private PanelDragger dragger;
    private BackButton PreviousButton;
    public bool HasPreviousButton;
    public bool BlockCloseButton;
    public bool ForceAllowCloseButtonWhenInActive;
    private LerpHandler_Float PreviousPopLerp;
    private bool active = true;
    private float savedalpha;
    private Texture2D mainTexture;
    private GameObjectNineSlice extraBorderFrame;
    private BetaLockedDisplay betaLockedDisplay;
    private float BaseScale;
    private GameObject BlockMask;
    private Vector2 BlockMaskScale;

    public BigBrownPanel()
    {
      this.BrownFrame = new GameObjectNineSlice(new Rectangle(939, 416, 21, 21), 7);
      this.BrownFrame.scale = 3f;
      this.BrownFrame.scale = RenderMath.GetPixelSizeBestMatch(2f);
    }

    public bool Active
    {
      get => this.active;
      set => this.active = value;
    }

    public BigBrownPanel(
      Vector2 _vScale,
      bool HasCloseButton = false,
      string addHeaderText = "",
      float _BaseScale = -1f,
      bool _HasPreviousButton = false,
      PanelFrameType frameType = PanelFrameType.BigBrown)
    {
      this.BaseScale = _BaseScale;
      this.HasPreviousButton = _HasPreviousButton;
      if ((double) this.BaseScale < 0.0)
      {
        this.BaseScale = RenderMath.GetPixelSizeBestMatch(2f);
        this.SetUp(_vScale, HasCloseButton, addHeaderText, RenderMath.GetPixelSizeBestMatch(2f), this.HasPreviousButton, frameType);
      }
      else
        this.SetUp(_vScale, HasCloseButton, addHeaderText, this.BaseScale, this.HasPreviousButton, frameType);
    }

    public void SetNewHeading(string _NewText) => this.miniHeading.SetNewText(_NewText);

    public Rectangle GetDrawRect(PanelFrameType frameType, out int sliceSize)
    {
      sliceSize = 7;
      this.mainTexture = AssetContainer.SpriteSheet;
      switch (frameType)
      {
        case PanelFrameType.BigBrown:
          return new Rectangle(939, 416, 21, 21);
        case PanelFrameType.Black:
          return new Rectangle(992, 578, 21, 21);
        case PanelFrameType.Gold:
          return new Rectangle(909, 766, 21, 21);
        default:
          return new Rectangle(939, 416, 21, 21);
      }
    }

    public Vector2 AddExtraBorderFrame()
    {
      this.extraBorderFrame = new GameObjectNineSlice(new Rectangle(323, 479, 63, 63), 21);
      this.extraBorderFrame.scale = this.BaseScale;
      return new Vector2(15f, 15f);
    }

    public void RemoveExtraBorderFrame() => this.extraBorderFrame = (GameObjectNineSlice) null;

    public void Finalize(Vector2 FullPanelSize, float _EdgeBuffer = 10f)
    {
      this.EdgeBuffer = _EdgeBuffer;
      float num = this.EdgeBuffer * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      float val1 = 0.0f;
      this.vScale = FullPanelSize;
      Vector2 vector2 = new Vector2();
      vector2.Y += this.EdgeBuffer * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.vScale.X += 2f * this.EdgeBuffer * this.BaseScale;
      this.vScale.Y += num;
      if (this.close != null || this.miniHeading != null)
      {
        if (this.close != null)
        {
          val1 = (float) this.close.DrawRect.Height * this.close.scale * Sengine.ScreenRatioUpwardsMultiplier.Y;
          this.close.vLocation.X = this.vScale.X * 0.5f;
          this.close.vLocation.X -= this.EdgeBuffer * this.BaseScale;
          this.close.vLocation.X -= (float) ((double) this.close.scale * (double) this.close.DrawRect.Width * 0.5);
          this.close.vLocation.Y = (float) ((double) this.close.scale * (double) this.close.DrawRect.Height * 0.5) * Sengine.ScreenRatioUpwardsMultiplier.Y;
          this.close.vLocation.Y += num;
          if (this.HasPreviousButton)
          {
            this.PreviousPopLerp = new LerpHandler_Float();
            this.PreviousPopLerp.SetLerp(true, 1f, 1f, 3f);
            this.PreviousButton.vLocation = this.close.vLocation;
            this.PreviousButton.vLocation.X -= this.EdgeBuffer * this.BaseScale;
            this.PreviousButton.vLocation.X -= this.close.scale * (float) this.close.DrawRect.Width;
          }
        }
        if (this.miniHeading != null)
          val1 = Math.Max(val1, this.miniHeading.GetTextHeight() * Sengine.ScreenRatioUpwardsMultiplier.Y);
        vector2.Y += val1;
        vector2.Y += num;
      }
      this.vScale += vector2;
      if (this.close != null || this.miniHeading != null)
      {
        this.InternalOffset.Y = num + val1;
        this.InternalOffset.Y *= -0.5f;
        if (this.miniHeading != null)
        {
          this.miniHeading.vLocation.X = (float) (-(double) this.vScale.X * 0.5);
          this.miniHeading.vLocation.X += this.EdgeBuffer * this.BaseScale;
          this.miniHeading.vLocation.Y = num;
          this.miniHeading.vLocation.Y -= this.vScale.Y * 0.5f;
          if (this.close != null)
            this.miniHeading.vLocation.Y += (float) ((double) this.BaseScale * (double) Sengine.ScreenRatioUpwardsMultiplier.Y * 3.0);
        }
        if (this.close != null)
        {
          this.close.vLocation.Y -= this.vScale.Y * 0.5f;
          if (this.HasPreviousButton)
          {
            this.PreviousButton.vLocation.Y -= this.vScale.Y * 0.5f;
            this.PreviousPopLerp = new LerpHandler_Float();
            this.PreviousPopLerp.SetLerp(true, 1f, 1f, 3f);
          }
        }
      }
      vector2.X = this.vScale.X;
      Vector2 topLeft_ = this.location + this.InternalOffset - 0.5f * this.vScale;
      Vector2 bottomRight_ = topLeft_ + vector2;
      this.dragger = new PanelDragger(topLeft_, bottomRight_, this.BaseScale);
    }

    public void PopPreviousButton() => this.PreviousPopLerp.SetLerp(true, 0.0f, 1f, 3f, true);

    public float GetEdgeBuffer() => this.EdgeBuffer;

    public Vector2 GetEdgeBuffers() => this.EdgeBuffer * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier;

    public Vector2 GetFrameOffsetFromTop(bool IncludeHeaderAndEdgeOffsets = false) => IncludeHeaderAndEdgeOffsets ? new Vector2((float) (-(double) this.vScale.X * 0.5), (float) (-(double) this.vScale.Y * 0.5)) + this.InternalOffset : new Vector2((float) (-(double) this.vScale.X * 0.5), (float) (-(double) this.vScale.Y * 0.5)) - this.InternalOffset + this.GetEdgeBuffers();

    public Vector2 GetMiniHeadingSize(bool IncludeBuffer = true)
    {
      Vector2 vector2 = Vector2.Zero;
      if (IncludeBuffer)
        vector2 = this.GetEdgeBuffers();
      return this.miniHeading.GetSize() + vector2;
    }

    private void SetUp(
      Vector2 _vScale,
      bool HasCloseButton,
      string addHeaderText,
      float BaseScale,
      bool _HasPreviousButton,
      PanelFrameType frameType)
    {
      this.vScale = _vScale;
      int sliceSize;
      this.BrownFrame = new GameObjectNineSlice(this.GetDrawRect(frameType, out sliceSize), sliceSize);
      this.BrownFrame.scale = BaseScale;
      if (HasCloseButton)
      {
        this.close = new BackButton(true, BaseScale: BaseScale);
        this.close.vLocation = new Vector2(-20f, 22f);
        this.close.vLocation.X += this.vScale.X * 0.5f;
        this.close.vLocation.Y -= this.vScale.Y * 0.5f;
      }
      if (_HasPreviousButton)
      {
        this.PreviousButton = new BackButton(true, BaseScale: BaseScale, _IsPrevious: true);
        this.PreviousButton.vLocation = new Vector2(-20f, 22f);
        this.PreviousButton.vLocation.X += this.vScale.X * 0.5f;
        this.PreviousButton.vLocation.Y -= this.vScale.Y * 0.5f;
        if (HasCloseButton)
          this.PreviousButton.vLocation.X -= -20f * BaseScale;
      }
      if (string.IsNullOrEmpty(addHeaderText))
        return;
      this.miniHeading = new MiniHeading(this.vScale, addHeaderText, BigBrownPanel.GetHeaderScale(), BaseScale * 2f);
    }

    public Vector2 GetFinalizedLocation() => this.location + this.InternalOffset;

    public static float GetHeaderScale() => 1.2f;

    public void LockForBeta(bool removeLock = false)
    {
      if (removeLock)
      {
        this.active = true;
        this.betaLockedDisplay = (BetaLockedDisplay) null;
        this.ForceAllowCloseButtonWhenInActive = false;
      }
      else
      {
        this.active = false;
        this.betaLockedDisplay = new BetaLockedDisplay(this.BaseScale, this.vScale.X);
        this.betaLockedDisplay.location.X -= this.betaLockedDisplay.GetSize().X * 0.5f;
        this.ForceAllowCloseButtonWhenInActive = true;
      }
    }

    public bool UpdatePanelCloseButton(
      Player player,
      float DeltaTime,
      Vector2 offset,
      bool AllowCloseOnClickOutsideBox = false)
    {
      if (!this.active && !this.ForceAllowCloseButtonWhenInActive)
        return false;
      offset += this.location + this.InternalOffset;
      if (this.close == null || this.BlockCloseButton)
        return false;
      if (this.close.UpdateBackButton(player, DeltaTime, offset))
        return true;
      return AllowCloseOnClickOutsideBox && (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && !this.CheckCollision(player.player.touchinput.ReleaseTapArray[0], offset);
    }

    public bool UpdatePanelpreviousButton(Player player, float DeltaTime, Vector2 offset)
    {
      if (this.HasPreviousButton)
      {
        offset += this.location + this.InternalOffset;
        this.PreviousPopLerp.UpdateLerpHandler(DeltaTime);
        if (this.HasPreviousButton && (double) this.PreviousPopLerp.Value == 1.0)
          return this.PreviousButton.UpdateBackButton(player, DeltaTime, offset);
      }
      return false;
    }

    public void UpdateDragger(Player player, ref Vector2 localposition, float DeltaTime) => this.UpdateDragger(player, ref localposition, DeltaTime, Vector2.Zero);

    public void UpdateDragger(
      Player player,
      ref Vector2 localposition,
      float DeltaTime,
      Vector2 extraoffset)
    {
      if (this.dragger == null || !this.active || this.close != null && this.close.MouseOver)
        return;
      Vector2 vector2_1 = new Vector2(0.5f * this.vScale.X, (float) (0.0 + 0.5 * (double) this.vScale.Y) - this.InternalOffset.Y);
      Vector2 vector2_2 = new Vector2(Sengine.ReferenceScreenRes.X - 0.5f * this.vScale.X, Sengine.ReferenceScreenRes.Y - 0.5f * this.vScale.Y - this.InternalOffset.Y);
      Vector2 vector2_3 = this.dragger.UpdatePanelDragger(player, extraoffset + localposition, DeltaTime);
      if (vector2_3 != Vector2.Zero)
        localposition += vector2_3;
      if ((double) localposition.X + (double) extraoffset.X > (double) vector2_2.X)
        localposition.X = vector2_2.X - extraoffset.X;
      if ((double) localposition.X + (double) extraoffset.X < (double) vector2_1.X)
        localposition.X = vector2_1.X - extraoffset.X;
      if ((double) localposition.Y + (double) extraoffset.Y > (double) vector2_2.Y)
        localposition.Y = vector2_2.Y - extraoffset.Y;
      if ((double) localposition.Y + (double) extraoffset.Y >= (double) vector2_1.Y)
        return;
      localposition.Y = vector2_1.Y - extraoffset.Y;
    }

    public bool CheckMouseOver(Player player, Vector2 offset) => this.dragger != null && this.dragger.dragging || MathStuff.CheckPointCollision(true, this.location + offset + this.InternalOffset, 1f, this.vScale.X, this.vScale.Y, player.inputmap.PointerLocation);

    public bool CheckCollision(Vector2 point, Vector2 offset) => MathStuff.CheckPointCollision(true, this.location + offset + this.InternalOffset, 1f, this.vScale.X, this.vScale.Y, point);

    public void DrawBigBrownPanel(Vector2 offset) => this.DrawBigBrownPanel(offset, AssetContainer.pointspritebatchTop05);

    public bool CheckMouseOverMask(
      Vector2 offset,
      Player player,
      float TopMaskHeight,
      float BottomMaskHeight)
    {
      offset += this.location + this.InternalOffset;
      return (double) player.inputmap.PointerLocation.Y < (double) offset.Y + ((double) this.vScale.Y * -0.5 + (double) TopMaskHeight) || (double) player.inputmap.PointerLocation.Y > (double) offset.Y + ((double) this.vScale.Y * 0.5 - (double) BottomMaskHeight);
    }

    public Vector2 GetCenter(Vector2 offset) => offset + this.location + this.InternalOffset;

    public Vector2 GetTop(Vector2 offset) => offset + this.location + this.InternalOffset + new Vector2(0.0f, this.vScale.Y * -0.5f);

    public void DrawBigBrownPanelTopMask(
      Vector2 offset,
      SpriteBatch spritebatch,
      float TopMaskHeight,
      float BottomMaskHeight)
    {
      offset += this.location + this.InternalOffset;
      if (this.BlockMask == null)
      {
        this.BlockMask = new GameObject();
        this.BlockMask.DrawRect = TinyZoo.Game1.WhitePixelRect;
        this.BlockMask.SetDrawOriginToCentre();
        this.BlockMaskScale.X = this.vScale.X - this.BrownFrame.scale * 16f;
      }
      this.BlockMask.SetAllColours(ColourData.Z_FrameLightBrown);
      this.BlockMaskScale.Y = TopMaskHeight - this.BrownFrame.scale * 4f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.BlockMask.Draw(spritebatch, AssetContainer.SpriteSheet, offset + new Vector2(0.0f, (float) ((double) this.vScale.Y * -0.5 + (double) TopMaskHeight * 0.5)), this.BlockMaskScale);
      this.BlockMaskScale.Y = BottomMaskHeight - this.BrownFrame.scale * 4f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.BlockMask.Draw(spritebatch, AssetContainer.SpriteSheet, offset + new Vector2(0.0f, (float) ((double) this.vScale.Y * 0.5 + (double) BottomMaskHeight * -0.5)), this.BlockMaskScale);
      if (this.close != null && !this.BlockCloseButton)
        this.close.DrawBackButton(offset, spritebatch);
      if (this.HasPreviousButton)
      {
        this.PreviousButton.scale = this.BaseScale * this.PreviousPopLerp.Value;
        this.PreviousButton.DrawBackButton(offset, spritebatch);
      }
      if (this.miniHeading == null)
        return;
      this.miniHeading.DrawMiniHeading(offset, spritebatch);
    }

    public void DrawBigBrownPanel(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location + this.InternalOffset;
      this.BrownFrame.DrawGameObjectNineSlice(spritebatch, this.mainTexture, offset, this.vScale);
      if (this.close != null && !this.BlockCloseButton)
        this.close.DrawBackButton(offset, spritebatch);
      if (this.HasPreviousButton)
      {
        this.PreviousButton.scale = this.BaseScale * this.PreviousPopLerp.Value;
        this.PreviousButton.DrawBackButton(offset, spritebatch);
      }
      if (this.miniHeading != null)
        this.miniHeading.DrawMiniHeading(offset, spritebatch);
      if (this.extraBorderFrame == null)
        return;
      this.extraBorderFrame.DrawGameObjectNineSlice(spritebatch, AssetContainer.UISheet, offset, this.vScale);
    }

    public void DrawDarkOverlay(Vector2 offset, SpriteBatch spritebatch)
    {
      if (this.active)
        return;
      offset += this.location + this.InternalOffset;
      this.savedalpha = this.BrownFrame.fAlpha;
      this.BrownFrame.SetAlpha(0.6f * this.savedalpha);
      this.BrownFrame.SetAllColours(new Vector3(0.4f, 0.4f, 0.4f));
      this.BrownFrame.DrawGameObjectNineSlice(spritebatch, this.mainTexture, offset, this.vScale);
      this.BrownFrame.SetAlpha(this.savedalpha);
      this.BrownFrame.SetAllColours(new Vector3(1f, 1f, 1f));
      if (this.betaLockedDisplay != null)
        this.betaLockedDisplay.DrawBetaLockedDisplay(offset, spritebatch);
      if (!this.ForceAllowCloseButtonWhenInActive || this.BlockCloseButton || this.close == null)
        return;
      this.close.DrawBackButton(offset, spritebatch);
    }
  }
}
