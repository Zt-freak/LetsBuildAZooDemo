// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Architcture.ResearchTree.TreeLines
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_Architcture.ResearchTree
{
  internal class TreeLines
  {
    private GameObject LineOne;
    private Vector2 VSCALEOne;
    private GameObject LineTwo;
    private Vector2 VSCALETwo;
    private GameObject LineThree;
    private Vector2 VSCALEThree;
    private GameObject LineFour;
    private Vector2 VSCALEFour;

    public TreeLines(float Start, float Width, bool SpiltToTwo)
    {
      float num = 3f;
      if ((double) Start == 0.0)
      {
        if (SpiltToTwo)
        {
          Start = 50f * Sengine.ScreenRatioUpwardsMultiplier.Y;
          this.LineOne = new GameObject();
          this.LineOne.DrawRect = TinyZoo.Game1.WhitePixelRect;
          this.LineOne.vLocation.Y = 0.0f;
          this.LineOne.SetDrawOriginToPoint(DrawOriginPosition.CenterRight);
          this.VSCALEOne = new Vector2(Width * 0.5f, num);
          this.LineTwo = new GameObject(this.LineOne);
          this.LineTwo.SetDrawOriginToPoint(DrawOriginPosition.CenterRight);
          this.LineTwo.vLocation.X = Width * -0.5f;
          this.VSCALETwo = new Vector2(num, Start * 2f);
          this.LineThree = new GameObject(this.LineOne);
          this.LineThree.SetDrawOriginToPoint(DrawOriginPosition.CenterRight);
          this.VSCALEThree = new Vector2(Width * 0.5f, num);
          this.LineThree.vLocation = new Vector2((float) (-(double) Width * 0.5), -Start);
          this.LineFour = new GameObject(this.LineOne);
          this.LineFour.SetDrawOriginToPoint(DrawOriginPosition.CenterRight);
          this.VSCALEFour = new Vector2(Width * 0.5f, num);
          this.LineFour.vLocation = new Vector2((float) (-(double) Width * 0.5), Start);
        }
        else
        {
          this.LineOne = new GameObject();
          this.LineOne.DrawRect = TinyZoo.Game1.WhitePixelRect;
          this.LineOne.vLocation.Y = 0.0f;
          this.LineOne.SetDrawOriginToPoint(DrawOriginPosition.CenterRight);
          this.VSCALEOne = new Vector2(Width, num);
        }
      }
      else
      {
        this.LineOne = new GameObject();
        this.LineOne.DrawRect = TinyZoo.Game1.WhitePixelRect;
        this.LineOne.vLocation.Y = 0.0f;
        this.VSCALEOne = new Vector2((float) (-(double) Width * 0.5), num);
        this.LineTwo = new GameObject(this.LineOne);
        this.LineTwo.SetDrawOriginToPoint(DrawOriginPosition.BottomLeft);
        this.LineTwo.vLocation.X = Width * -0.5f;
        this.VSCALETwo = new Vector2(num, Start);
        this.LineThree = new GameObject(this.LineOne);
        this.LineThree.SetDrawOriginToPoint(DrawOriginPosition.CenterRight);
        this.VSCALEThree = new Vector2(Width * 0.5f, num);
        this.LineThree.vLocation = new Vector2((float) (-(double) Width * 0.5), -Start);
      }
    }

    public void UpdateTreeLines()
    {
    }

    public void DrawTreeLines(Vector2 Offset)
    {
      if (this.LineOne == null)
        return;
      this.LineOne.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VSCALEOne * Sengine.ScreenRatioUpwardsMultiplier);
      if (this.LineTwo == null)
        return;
      this.LineTwo.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VSCALETwo);
      this.LineThree.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VSCALEThree * Sengine.ScreenRatioUpwardsMultiplier);
      if (this.LineFour == null)
        return;
      this.LineFour.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VSCALEFour * Sengine.ScreenRatioUpwardsMultiplier);
    }
  }
}
