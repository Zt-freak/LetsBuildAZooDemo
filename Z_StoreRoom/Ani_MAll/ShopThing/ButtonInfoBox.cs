// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Ani_MAll.ShopThing.ButtonInfoBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_StoreRoom.Ani_MAll.ShopThing
{
  internal class ButtonInfoBox : GameObject
  {
    private Vector2 VSCALE;
    private ZGenericText topText;
    private ZGenericText bottomText;
    private ZGenericText redNumberText;
    private float BaseScale;

    public ButtonInfoBox(float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToCentre();
      this.SetAllColours(0.9372549f, 0.7019608f, 0.6f);
      string _textToWrite = "days";
      this.redNumberText = new ZGenericText("999", this.BaseScale, _UseOnePointFiveFont: true);
      this.redNumberText.SetAllColours(0.8392157f, 0.3568628f, 0.2980392f);
      this.topText = new ZGenericText("X", this.BaseScale);
      this.bottomText = new ZGenericText(_textToWrite, this.BaseScale, false);
      this.topText.SetAllColours(ColourData.Z_DarkTextGray);
      this.bottomText.SetAllColours(ColourData.Z_DarkTextGray);
      float num = uiScaleHelper.ScaleY(3f);
      Vector2 zero = Vector2.Zero;
      zero.Y += num;
      this.topText.vLocation.Y = zero.Y;
      this.topText.vLocation.Y += this.topText.GetSize().Y * 0.5f;
      zero.Y += this.topText.GetSize().Y;
      this.redNumberText.vLocation.Y = zero.Y;
      this.redNumberText.vLocation.Y += this.redNumberText.GetSize().Y * 0.5f;
      this.redNumberText.vLocation.Y -= uiScaleHelper.ScaleY(2f);
      this.bottomText.vLocation.Y = zero.Y;
      this.redNumberText.vLocation.X -= uiScaleHelper.ScaleX(15f);
      zero.Y += this.bottomText.GetSize().Y;
      zero.Y += num;
      zero.X = uiScaleHelper.ScaleX(110f);
      this.VSCALE = zero;
      Vector2 vector2 = -this.VSCALE * 0.5f;
      this.redNumberText.vLocation.Y += vector2.Y;
      this.bottomText.vLocation.Y += vector2.Y;
      this.topText.vLocation.Y += vector2.Y;
    }

    public Vector2 GetSize() => this.VSCALE;

    public void SetAsDeleiveryTime(int _Days)
    {
      this.topText.textToWrite = "Delivery Time:";
      this.redNumberText.textToWrite = string.Concat((object) _Days);
    }

    public void SetAsShelfLifeTime(int _Days)
    {
      this.topText.textToWrite = "Shelf Life:";
      this.redNumberText.textToWrite = string.Concat((object) _Days);
    }

    public void DrawButtonInfoBox(Vector2 Offset, SpriteBatch DrawWithThis)
    {
      this.Draw(DrawWithThis, AssetContainer.SpriteSheet, Offset, this.VSCALE);
      Offset += this.vLocation;
      this.topText.DrawZGenericText(Offset, DrawWithThis);
      this.bottomText.DrawZGenericText(Offset, DrawWithThis);
      this.redNumberText.DrawZGenericText(Offset, DrawWithThis);
    }
  }
}
