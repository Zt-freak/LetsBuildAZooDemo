// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.ScreenHeading
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.GenericUI
{
  internal class ScreenHeading
  {
    public StringInBox header;

    public ScreenHeading(
      string ThisHeading,
      float Length = 50f,
      bool DrawFromCanter = true,
      float ExtraScaleMult = 1f,
      float BaseScale = -1f,
      bool UseSmallerOnePointFiveFont = false)
    {
      if ((double) BaseScale > -1.0)
      {
        this.header = new StringInBox(BaseScale, ThisHeading, (float) ((double) Length * (double) BaseScale * 6.0), DrawFromCanter, !UseSmallerOnePointFiveFont, true, UseSmallerOnePointFiveFont);
        this.header.vLocation = new Vector2((float) (20.0 + (double) this.header.VScale.X * 0.5), 50f);
      }
      else
      {
        float num = 4f;
        if (DebugFlags.IsPCVersion)
          num = 3f * Sengine.ScreenRationReductionMultiplier.Y;
        float _BaseScale = num * ExtraScaleMult;
        this.header = new StringInBox(ThisHeading, _BaseScale, Length, DrawFromCanter, true);
        this.header.vLocation = new Vector2((float) (20.0 + (double) this.header.GetVScale_Depricated().X * 0.5), 50f);
      }
      this.header.SetAsButtonFrame(BTNColour.PaleYellow);
      if (DebugFlags.IsPCVersion)
        this.header.Frame.scale = 1f;
      if ((double) BaseScale <= -1.0)
        return;
      this.header.Frame.scale = BaseScale;
    }

    public void SetNewString(string NewText) => this.header.SetText(NewText);

    public void UpdateScreenHeading()
    {
    }

    public void DrawScreenHeading(Vector2 Offset, SpriteBatch spritebatch) => this.header.DrawStringInBox(Offset, spritebatch);
  }
}
