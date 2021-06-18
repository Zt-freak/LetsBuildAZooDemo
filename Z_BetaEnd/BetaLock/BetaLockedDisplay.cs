// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BetaEnd.BetaLock.BetaLockedDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Morality;
using TinyZoo.Z_MoralitySummary;

namespace TinyZoo.Z_BetaEnd.BetaLock
{
  internal class BetaLockedDisplay
  {
    public Vector2 location;
    private MoralityTypeIcon icon;
    private SimpleTextHandler simpleTextHandler;
    private Vector2 size;

    public BetaLockedDisplay(float BaseScale, float frameWidth = -1f)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.size = Vector2.Zero;
      this.icon = new MoralityTypeIcon(MoralityType.Beta, BaseScale);
      this.icon.vLocation.X += this.icon.GetSize().X * 0.5f;
      this.size.X += this.icon.GetSize().X;
      this.size.X += uiScaleHelper.DefaultBuffer.X;
      float width_ = uiScaleHelper.ScaleX(200f);
      if ((double) frameWidth != -1.0)
        width_ = frameWidth - this.size.X - uiScaleHelper.DefaultBuffer.X;
      this.simpleTextHandler = new SimpleTextHandler("Unavailable in Beta", width_, true, BaseScale, true, true);
      this.simpleTextHandler.SetAllColours(ColourData.Z_Cream);
      Vector2 size = this.simpleTextHandler.GetSize(true);
      this.simpleTextHandler.Location.X = this.size.X;
      this.simpleTextHandler.Location.X += size.X * 0.5f;
      this.simpleTextHandler.Location.Y += this.simpleTextHandler.GetHeightOfOneLine() * 0.5f;
      this.simpleTextHandler.Location.Y -= size.Y * 0.5f;
      this.size.X += size.X;
      this.size.X += uiScaleHelper.DefaultBuffer.X;
      this.size.Y = Math.Max(this.icon.GetSize().Y, size.Y);
    }

    public Vector2 GetSize() => this.size;

    public void UpdateBetaLockedDisplay()
    {
    }

    public void DrawBetaLockedDisplay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.icon.DrawMoralityTypeIcon(offset, spriteBatch);
      this.simpleTextHandler.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
