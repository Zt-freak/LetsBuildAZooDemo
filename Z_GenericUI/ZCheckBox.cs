// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.ZCheckBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_GenericUI
{
  internal class ZCheckBox
  {
    private Rectangle emptyBoxRect;
    private Rectangle tickedBoxRect;
    public GameObject checkBox;
    private bool isTicked;
    public Vector2 location;
    private bool mouseOver;
    private Vector2 extraTouchCollision;
    private bool AutoTickWhenClicked;

    public bool isActive { get; private set; }

    public ZCheckBox(float BaseScale, bool _AutoTickWhenClicked = false)
    {
      this.AutoTickWhenClicked = _AutoTickWhenClicked;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.emptyBoxRect = new Rectangle(670, 416, 18, 18);
      this.tickedBoxRect = new Rectangle(689, 416, 18, 18);
      this.checkBox = new GameObject();
      this.checkBox.DrawRect = this.emptyBoxRect;
      this.checkBox.SetDrawOriginToCentre();
      this.checkBox.scale = BaseScale;
      this.extraTouchCollision = uiScaleHelper.ScaleVector2(new Vector2(15f, 15f));
      this.isActive = true;
    }

    public bool UpdateCheckBox(Player player, Vector2 offset)
    {
      offset += this.location;
      if (!this.isActive)
        return false;
      this.mouseOver = MathStuff.CheckPointCollision(true, offset, 1f, this.extraTouchCollision.X + (float) this.checkBox.DrawRect.Width * this.checkBox.scale, this.extraTouchCollision.Y + (float) this.checkBox.DrawRect.Height * this.checkBox.scale * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
      bool flag = MathStuff.CheckPointCollision(true, offset, 1f, this.extraTouchCollision.X + (float) this.checkBox.DrawRect.Width * this.checkBox.scale, this.extraTouchCollision.Y + (float) this.checkBox.DrawRect.Height * this.checkBox.scale * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]);
      if (this.AutoTickWhenClicked & flag)
        this.SetTicked(!this.isTicked);
      return flag;
    }

    public void SetColorTint(Vector3 color) => this.checkBox.SetAllColours(color);

    public bool GetIsTicked() => this.isTicked;

    public void SetTicked(bool _isTicked)
    {
      this.isTicked = _isTicked;
      if (this.isTicked)
        this.checkBox.DrawRect = this.tickedBoxRect;
      else
        this.checkBox.DrawRect = this.emptyBoxRect;
    }

    public void SetActive(bool _isActive)
    {
      this.isActive = _isActive;
      if (_isActive)
        this.SetColorTint(Color.White.ToVector3());
      else
        this.SetColorTint(Color.DarkGray.ToVector3());
    }

    public Vector2 GetSize() => new Vector2((float) this.checkBox.DrawRect.Width * this.checkBox.scale, (float) this.checkBox.DrawRect.Height * this.checkBox.scale) * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawCheckBox(SpriteBatch spriteBatch, Vector2 offset)
    {
      offset += this.location;
      this.checkBox.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
    }
  }
}
