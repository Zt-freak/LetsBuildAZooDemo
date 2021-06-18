// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.ControlsHint.R_ControlHint
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.Z_Research_.ControlsHint
{
  internal class R_ControlHint
  {
    public Vector2 location;
    private GameObject textObj;
    private string mouseHoldText;
    private TRC_ButtonDisplay mouseIcon;
    private TRC_ButtonDisplay controllerIcon;
    private bool PositionTextBelow;
    private float IconTextBuffer;

    public R_ControlHint(float BaseScale)
    {
      this.IconTextBuffer = 5f * BaseScale;
      this.mouseIcon = new TRC_ButtonDisplay(BaseScale);
      this.mouseIcon.SetAsStaticButton(ControllerType.NintendoSwitch, ButtonStyle.SuperSmall, ControllerButton.Mouse_LeftHeld);
      this.controllerIcon = new TRC_ButtonDisplay(BaseScale);
      this.controllerIcon.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, ControllerButton.XboxA);
      if (!this.PositionTextBelow)
        this.mouseIcon.Position.X += (float) ((double) this.mouseIcon.DrawRect.Width * (double) this.mouseIcon.scale * 0.5);
      this.mouseHoldText = "Hold to Unlock";
      this.textObj = new GameObject();
      this.textObj.scale = BaseScale;
      this.textObj.SetAllColours(ColourData.Z_Cream);
      if (this.PositionTextBelow)
        this.textObj.vLocation.Y += (float) ((double) this.mouseIcon.DrawRect.Height * (double) this.mouseIcon.scale + (double) SpringFontUtil.MeasureString("X", AssetContainer.springFont).Y * 0.5);
      else
        this.textObj.vLocation.X += this.mouseIcon.Position.X + (float) ((double) this.mouseIcon.DrawRect.Width * (double) this.mouseIcon.scale * 0.5) + this.IconTextBuffer;
    }

    public void UpdateR_ControlHint()
    {
    }

    public Vector2 GetSize()
    {
      float num1 = 0.0f;
      float num2 = 0.0f;
      float y;
      float x;
      if (this.PositionTextBelow)
      {
        Vector2 vector2 = SpringFontUtil.MeasureString(this.mouseHoldText, AssetContainer.springFont) * this.textObj.scale;
        y = num2 + (float) this.mouseIcon.DrawRect.Height * this.mouseIcon.scale * Sengine.ScreenRatioUpwardsMultiplier.Y + vector2.Y;
        x = num1 + vector2.X;
      }
      else
      {
        y = num2 + (float) this.mouseIcon.DrawRect.Height * this.mouseIcon.scale * Sengine.ScreenRatioUpwardsMultiplier.Y;
        x = num1 + (float) this.mouseIcon.DrawRect.Width * this.mouseIcon.scale + SpringFontUtil.MeasureString(this.mouseHoldText, AssetContainer.springFont).X * this.textObj.scale + this.IconTextBuffer;
      }
      return new Vector2(x, y);
    }

    public void DrawR_ControlHint(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (!TinyZoo.GameFlags.IsUsingController)
        this.mouseIcon.DrawTRC_ButtonDisplay(spriteBatch, AssetContainer.TRC_Sprites, offset);
      else
        this.controllerIcon.DrawTRC_ButtonDisplay(spriteBatch, AssetContainer.TRC_Sprites, offset);
      if (this.PositionTextBelow)
        TextFunctions.DrawJustifiedText(this.mouseHoldText, this.textObj.scale, offset + this.textObj.vLocation, this.textObj.GetColour(), this.textObj.fAlpha, AssetContainer.springFont, spriteBatch);
      else
        TextFunctions.DrawTextWithDropShadow(this.mouseHoldText, this.textObj.scale, offset + this.textObj.vLocation, this.textObj.GetColour(), this.textObj.fAlpha, AssetContainer.springFont, spriteBatch, false, false, false, 0.0f, 1);
    }
  }
}
