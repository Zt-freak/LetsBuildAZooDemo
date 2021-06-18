// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.Emp_Summary.Hiring.ExtraDragBarAttachment
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_Employees.Emp_Summary.Hiring
{
  internal class ExtraDragBarAttachment
  {
    public Vector2 location;
    public Vector2 VSCALEOutSide;
    private Vector2 VSCALEInside;
    private GameObject FRAME;
    private GameObject Inside;

    public ExtraDragBarAttachment(float BaseScale, float width)
    {
      this.FRAME = new GameObject();
      this.FRAME.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.FRAME.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      Vector3 SecondaryColour;
      StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour);
      this.FRAME.SetAllColours(SecondaryColour);
      this.VSCALEOutSide = new Vector2(width, 30f * BaseScale);
      this.VSCALEInside = this.VSCALEOutSide - new Vector2(8f * BaseScale, 8f * Sengine.ScreenRatioUpwardsMultiplier.Y * BaseScale);
      this.Inside = new GameObject(this.FRAME);
      this.Inside.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
    }

    public void DrawExtraDragBarAttachment(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.FRAME.Draw(spriteBatch, AssetContainer.SpriteSheet, offset, this.VSCALEOutSide);
      this.Inside.Draw(spriteBatch, AssetContainer.SpriteSheet, offset, this.VSCALEInside);
    }
  }
}
