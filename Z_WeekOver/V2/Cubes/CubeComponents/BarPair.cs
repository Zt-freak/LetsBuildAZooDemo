// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents.BarPair
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents
{
  internal class BarPair
  {
    public ContentsBar LeftBar;
    public ContentsBar RightBar;
    private Vector2 Location;
    private GameObject DivisionLine;
    private Vector2 VScale;

    public BarPair(float BaseScale, float LFullness, float RFullness, Vector3 Colour)
    {
      this.LeftBar = new ContentsBar(BaseScale, "Previous", Colour, true, true);
      this.RightBar = new ContentsBar(BaseScale, "This Week", Colour, true, true);
      this.LeftBar.vLocation.X = (float) (-(double) this.LeftBar.VScale.X * 0.5);
      this.RightBar.vLocation.X = this.RightBar.VScale.X * 0.5f;
      this.LeftBar.vLocation.X += -15f * BaseScale;
      this.RightBar.vLocation.X += 15f * BaseScale;
      this.LeftBar.SetFinalFullness(LFullness);
      this.RightBar.SetFinalFullness(RFullness);
      this.Location = new Vector2(0.0f, 60f * BaseScale);
      this.Location.X = (float) (30.0 * -(double) BaseScale);
      this.DivisionLine = new GameObject();
      this.DivisionLine.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.DivisionLine.SetDrawOriginToCentre();
      this.DivisionLine.vLocation.Y += BaseScale * 10f;
      this.DivisionLine.SetAllColours(Colour);
      this.VScale = new Vector2((float) ((double) this.LeftBar.VScale.X * 2.0 + 30.0 * (double) BaseScale), BaseScale * 5f);
    }

    public void SetBarValueText(string TT, bool IsLeftBar = false)
    {
      if (IsLeftBar)
        this.LeftBar.SetBarValueText(TT);
      else
        this.RightBar.SetBarValueText(TT);
    }

    public void UpdateBarPair()
    {
    }

    public void DrawBarPair(Vector2 Offset, SpriteBatch spritebatch, float ALPHAMult = 1f)
    {
      Offset += this.Location;
      this.LeftBar.DrawContentsBar(spritebatch, Offset, ALPHAMult);
      this.RightBar.DrawContentsBar(spritebatch, Offset, ALPHAMult);
      this.DivisionLine.Draw(spritebatch, AssetContainer.SpriteSheet, Offset, this.VScale);
    }
  }
}
