// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal.Shared.PointerAndText
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Animal.Shared
{
  internal class PointerAndText : GameObject
  {
    private Vector2 VSCALE;
    private ZGenericText textthing;

    public PointerAndText(string Text, float MasterMult, float BaseScale = -1f, float DefHeight = 25f)
    {
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToCentre();
      this.SetAllColours(ColourData.Z_Cream);
      this.textthing = new ZGenericText(Text, BaseScale);
      this.textthing.SetAllColours(ColourData.Z_Cream);
      if ((double) BaseScale == -1.0)
      {
        this.VSCALE = new Vector2(2f, DefHeight) * MasterMult;
        this.textthing.scale = RenderMath.GetPixelSizeBestMatch(1f);
        this.textthing.vLocation.Y = DefHeight * MasterMult;
      }
      else
      {
        this.VSCALE = new Vector2(BaseScale * 2f, DefHeight * BaseScale) * MasterMult;
        this.textthing.scale = BaseScale;
        this.textthing.vLocation.Y = DefHeight * MasterMult * BaseScale;
      }
    }

    public Vector2 GetLineVScale() => this.VSCALE;

    public float GetLineAndTextHeight() => (float) ((double) this.VSCALE.Y + ((double) this.textthing.vLocation.Y - (double) this.VSCALE.Y * 0.5) + (double) this.textthing.GetSize().Y * 0.5);

    public void SetPointerColor(Vector3 color, bool TextToo = true)
    {
      this.SetAllColours(color);
      if (!TextToo)
        return;
      this.textthing.SetAllColours(color);
    }

    public void SetNewText(string text) => this.textthing.textToWrite = text;

    public void DrawPointerAndText(Vector2 Offset, SpriteBatch spriteBatch)
    {
      this.Draw(spriteBatch, AssetContainer.SpriteSheet, Offset, this.VSCALE);
      this.textthing.DrawZGenericText(Offset + this.vLocation, spriteBatch);
    }

    public void DrawPointerAndText(Vector2 Offset) => this.DrawPointerAndText(Offset, AssetContainer.pointspritebatchTop05);
  }
}
