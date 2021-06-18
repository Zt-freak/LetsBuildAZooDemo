// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalNotification.MovingTray
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_AnimalNotification
{
  internal class MovingTray
  {
    public Vector2 location;
    private Vector2 destOffset;
    private Vector2 currOffset;
    private Vector2 start;
    private Vector2 diff;
    private float lerpRatio;
    private bool opened;
    private bool lerping;
    private LerpHandler_Float lerper = new LerpHandler_Float();
    private BigBrownPanel panel;
    private TrayOpenClose button;
    private UIScaleHelper uiScale;
    private Vector2 panelsize;
    private float basescale;

    public bool IsOpen => this.opened;

    public bool Lerping => this.lerping;

    public MovingTray(
      Vector2 panelsize_,
      Vector2 destinationOffset,
      MovingTrayDirection direction,
      float basescale_,
      string headertext = "")
    {
      this.basescale = basescale_;
      this.uiScale = new UIScaleHelper(basescale_);
      this.panelsize = panelsize_;
      this.destOffset = destinationOffset;
      this.button = new TrayOpenClose(this.basescale);
      this.panel = new BigBrownPanel(this.panelsize, true, headertext, basescale_);
      this.panel.Finalize(this.panelsize);
      this.panel.BlockCloseButton = true;
      switch (direction)
      {
        case MovingTrayDirection.Left:
          this.button.location.X = -0.5f * this.panelsize.X - this.uiScale.ScaleX(5f);
          break;
        case MovingTrayDirection.Right:
          this.button.location.X = 0.5f * this.panelsize.X + this.uiScale.ScaleX(5f);
          break;
      }
      this.StartLerp();
    }

    public Vector2 GetSizeWithoutBorder() => this.panelsize;

    public void StartLerp()
    {
      if (this.lerping)
        return;
      this.lerper.SetLerp(true, 0.0f, 1f, 3f, true);
      this.lerpRatio = 0.0f;
      this.lerping = true;
      if (!this.opened)
      {
        this.start = Vector2.Zero;
        this.diff = this.destOffset;
      }
      else
      {
        this.start = this.destOffset;
        this.diff = -this.destOffset;
      }
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      offset += this.currOffset;
      return this.panel.CheckMouseOver(player, offset);
    }

    public void UpdateMovingTray(Player player, float DeltaTime, Vector2 offset)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      if (this.lerping)
      {
        this.lerpRatio = this.lerper.Value;
        if ((double) this.lerpRatio >= 1.0)
        {
          this.lerping = false;
          this.opened = !this.opened;
          this.button.Toggle();
        }
        this.currOffset = this.start + this.lerpRatio * this.diff;
      }
      offset = offset + this.location + this.currOffset;
      if (!this.button.UpdateTrayOpenClose(player, DeltaTime, offset))
        return;
      this.StartLerp();
    }

    public void DrawMovingTray(Vector2 offset, SpriteBatch spritebatch)
    {
      offset = offset + this.location + this.currOffset;
      this.panel.DrawBigBrownPanel(offset);
    }

    public void DrawOpenCloseButton(Vector2 offset, SpriteBatch spritebatch)
    {
      offset = offset + this.location + this.currOffset;
      this.button.DrawTrayOpenClose(offset, spritebatch);
    }

    public Vector2 GetTruePosition() => this.location + this.currOffset;
  }
}
