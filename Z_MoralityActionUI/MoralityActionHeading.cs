// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralityActionUI.MoralityActionHeading
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_MoralitySummary;

namespace TinyZoo.Z_MoralityActionUI
{
  internal class MoralityActionHeading
  {
    private float basescale;
    public Vector2 location;
    private string str;
    private Vector2 strLoc;
    private Vector2 strSize;
    private GoodEvilIcon icon;
    private bool isGood;
    private bool isLocked;
    private UIScaleHelper uiScale;

    public MoralityActionHeading(bool isGood_, bool isLocked_, string text_, float basescale_)
    {
      this.isGood = isGood_;
      this.basescale = basescale_;
      this.isLocked = isLocked_;
      this.uiScale = new UIScaleHelper(this.basescale);
      this.str = text_;
      this.icon = new GoodEvilIcon(this.isGood, this.isLocked, true);
      this.icon.SetDrawOriginToCentre();
      this.strSize = 2f * this.uiScale.ScaleVector2(AssetContainer.springFont.MeasureString(this.str));
      this.strLoc.X = (float) (0.5 * (double) this.icon.GetSize().X + 0.5 * (double) this.strSize.X) + this.uiScale.ScaleX(5f);
      this.strLoc.Y += this.uiScale.ScaleY(2f);
      Vector2 vector2 = new Vector2();
      vector2.X = (float) (-0.5 * ((double) this.strSize.X + (double) this.uiScale.ScaleX(5f)));
      this.icon.vLocation = vector2;
      this.strLoc += vector2;
    }

    public Vector2 GetSize() => new Vector2()
    {
      X = this.icon.GetSize().X + this.strSize.X,
      Y = this.strSize.Y
    };

    public void DrawMoralityActionHeading(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.icon.DrawGoodEvilIcon(offset, spritebatch);
      TextFunctions.DrawJustifiedText(this.str, 2f * this.basescale, offset + this.strLoc, new Color(ColourData.Z_Cream), 1f, AssetContainer.springFont, spritebatch);
    }
  }
}
