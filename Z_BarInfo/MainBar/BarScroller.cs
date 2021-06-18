// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BarInfo.MainBar.BarScroller
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_BarInfo.MainBar.Scroll;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.Z_BarInfo.MainBar
{
  internal class BarScroller
  {
    private Z_ScrollButton Left;
    private Z_ScrollButton Right;
    private int TotalPages;
    private int TargetPage;
    public LerpHandler_Float pagelerper;
    private PENControllerHint LBumper;
    private PENControllerHint Rbumper;
    private LerpHandler_Float alphaLerper;
    private bool enableControllerHint;
    private float BaseScale;

    public BarScroller(int _TotalPages, float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.TotalPages = _TotalPages;
      this.Left = new Z_ScrollButton(true);
      this.Left.vLocation = new Vector2(30f, 668f);
      this.Right = new Z_ScrollButton(false);
      this.Right.vLocation = new Vector2(994f, 668f);
      this.TargetPage = 0;
      this.pagelerper = new LerpHandler_Float();
      this.LBumper = new PENControllerHint(false, 0);
      this.Rbumper = new PENControllerHint(true, 8);
      this.alphaLerper = new LerpHandler_Float();
    }

    public bool MouseOverlapping(Player player, Vector2 Offset, ref Vector2 PageOffset)
    {
      PageOffset.X = this.pagelerper.Value * 1024f;
      return this.Left.MouseOverlapping(player, Offset) || this.Right.MouseOverlapping(player, Offset);
    }

    public int GetCurrentPage() => this.TargetPage;

    public void ForceToThisPage(int pageIndex)
    {
      if (pageIndex == this.TargetPage)
        return;
      this.TryChangePage(pageIndex > this.TargetPage);
    }

    public bool TryChangePage(bool Forward)
    {
      if (this.pagelerper.IsComplete())
      {
        if (Forward)
        {
          if (this.TargetPage < this.TotalPages)
          {
            ++this.TargetPage;
            this.pagelerper.SetLerp(false, 0.0f, (float) -this.TargetPage, 3f, true);
            this.alphaLerper.SetLerp(false, 1f, 0.0f, 3f, true);
            return true;
          }
        }
        else if (this.TargetPage > 0)
        {
          --this.TargetPage;
          this.pagelerper.SetLerp(false, 0.0f, (float) -this.TargetPage, 3f, true);
          this.alphaLerper.SetLerp(false, 1f, 0.0f, 3f, true);
          return true;
        }
      }
      return false;
    }

    public void EnableControllerHint(bool isEnable)
    {
      if (this.enableControllerHint != isEnable)
      {
        if (isEnable)
          this.alphaLerper.SetLerp(false, 0.0f, 1f, 3f, true);
        else
          this.alphaLerper.SetLerp(false, 1f, 0.0f, 3f, true);
      }
      this.enableControllerHint = isEnable;
    }

    public void UpdateBarScroller(
      Player player,
      float DeltaTime,
      ref Vector2 PageOffset,
      float ShrinkValue)
    {
      this.LBumper.UpdatePENControllerHint(DeltaTime);
      this.Rbumper.UpdatePENControllerHint(DeltaTime);
      this.alphaLerper.UpdateLerpHandler(DeltaTime);
      this.pagelerper.UpdateLerpHandler(DeltaTime);
      if (this.pagelerper.IsComplete())
        this.alphaLerper.SetLerp(false, 0.0f, 1f, 3f);
      PageOffset.X = this.pagelerper.Value * 1024f;
      if (this.Left.UpdateZ_ScrollButton(Vector2.Zero, player, DeltaTime, this.pagelerper.IsComplete()))
        this.TryChangePage(false);
      if (!this.Right.UpdateZ_ScrollButton(new Vector2((float) ((1.0 - (double) ShrinkValue) * -1124.0) * this.BaseScale, 0.0f), player, DeltaTime, this.pagelerper.IsComplete()))
        return;
      this.TryChangePage(true);
    }

    public void DrawBarScroller(SpriteBatch spritebatch, ref Vector2 PageOffset, float ShrinkValue)
    {
      PageOffset.X = this.pagelerper.Value * 1024f;
      if (this.TargetPage > 0)
      {
        if (GameFlags.IsUsingController)
        {
          this.LBumper.DrawPENControllerHint(Vector2.Zero, this.alphaLerper.Value);
        }
        else
        {
          this.Left.DrawZ_ScrollButton(spritebatch);
          this.Left.SetAlpha(this.alphaLerper.Value);
        }
      }
      if (this.TargetPage >= this.TotalPages)
        return;
      if (GameFlags.IsUsingController)
      {
        this.Rbumper.DrawPENControllerHint(new Vector2((float) ((1.0 - (double) ShrinkValue) * -1124.0) * this.BaseScale, 0.0f), this.alphaLerper.Value);
      }
      else
      {
        this.Right.DrawZ_ScrollButton(new Vector2((float) ((1.0 - (double) ShrinkValue) * -1124.0) * this.BaseScale, 0.0f), spritebatch);
        this.Right.SetAlpha(this.alphaLerper.Value);
      }
    }
  }
}
