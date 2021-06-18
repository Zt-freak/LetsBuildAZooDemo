// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.Z_Scroll.Z_ScrollHelper
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;

namespace TinyZoo.Z_GenericUI.Z_Scroll
{
  internal class Z_ScrollHelper
  {
    private Vector2 sizeOfContents;
    private bool scrollOnlyIfInZone;
    private float scrollSpeedMult;

    public float YscrollOffset { get; private set; }

    public bool PointerInZone { get; private set; }

    public float maxHeight { get; private set; }

    public Z_ScrollHelper(
      Vector2 _sizeOfContents,
      float _maxHeight,
      float _scrollSpeedMult = 0.25f,
      bool _scrollOnlyIfInZone = true)
    {
      this.sizeOfContents = _sizeOfContents;
      this.maxHeight = _maxHeight;
      this.scrollOnlyIfInZone = _scrollOnlyIfInZone;
      this.scrollSpeedMult = _scrollSpeedMult;
    }

    public void UpdateZ_ScrollHelper(Player player, Vector2 offset_TopLeft)
    {
      this.PointerInZone = true;
      if ((double) this.maxHeight > (double) this.sizeOfContents.Y)
        return;
      if (this.scrollOnlyIfInZone)
        this.PointerInZone = MathStuff.CheckPointCollision(false, offset_TopLeft, 1f, this.sizeOfContents.X, this.maxHeight, player.inputmap.PointerLocation);
      if (!this.PointerInZone)
        return;
      this.YscrollOffset += player.inputmap.momentumwheel.MovementThisFrame * this.scrollSpeedMult;
      this.YscrollOffset = Math.Min(this.YscrollOffset, 0.0f);
      this.YscrollOffset = Math.Max(this.YscrollOffset, -this.sizeOfContents.Y + this.maxHeight);
    }

    public bool CheckIfShouldDrawThis(float TopLocation, float BottomLocation) => (double) BottomLocation + (double) this.YscrollOffset >= 0.0 && (double) TopLocation + (double) this.YscrollOffset <= (double) this.maxHeight;
  }
}
