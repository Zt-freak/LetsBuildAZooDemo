// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.ControlHint.MouseHintRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.Z_GenericUI;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.Z_HUD.ControlHint
{
  internal class MouseHintRow
  {
    private GameObject Bar;
    private Vector2 VSCale;
    public Vector2 Location;
    private ZGenericText Text;
    private ZGenericText TextTwo;
    private ControllerButton mousebutton;
    private UIScaleHelper scaleHelper;
    private TRC_ButtonDisplay trcButton;

    public MouseHintRow(
      float BaseScale,
      ControllerButton _mousebutton,
      string _Text,
      string _TextTwo = "")
    {
      this.scaleHelper = new UIScaleHelper(BaseScale);
      Vector2 vector2_1 = this.scaleHelper.DefaultBuffer * 0.5f;
      this.mousebutton = _mousebutton;
      this.Bar = new GameObject();
      this.Bar.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.Bar.SetDrawOriginToPoint(DrawOriginPosition.TopLeft);
      if (_mousebutton != ControllerButton.None)
        this.Bar.SetAllColours(ColourData.Z_Gray);
      else
        this.Bar.SetAllColours(ColourData.Z_Cream);
      this.Text = new ZGenericText(_Text, BaseScale, false);
      this.TextTwo = new ZGenericText(_TextTwo, BaseScale, false);
      this.Text.SetAllColours(ColourData.Z_TextBrown);
      this.TextTwo.SetAllColours(ColourData.Z_TextBrown);
      this.trcButton = new TRC_ButtonDisplay(BaseScale);
      this.trcButton.SetAsStaticButton(ControllerType.NintendoSwitch, ButtonStyle.SuperSmall, this.mousebutton);
      Vector2 vector2_2 = vector2_1;
      this.trcButton.vLocation = vector2_2;
      TRC_ButtonDisplay trcButton = this.trcButton;
      trcButton.vLocation = trcButton.vLocation + this.trcButton.GetSize_Static() * 0.5f * BaseScale;
      if (this.mousebutton != ControllerButton.None)
      {
        vector2_2.X += this.trcButton.GetSize_Static().X * BaseScale;
        vector2_2.X += vector2_1.X;
      }
      this.Text.vLocation = vector2_2;
      this.TextTwo.vLocation.Y = vector2_2.Y + this.Text.GetSize().Y;
      this.TextTwo.vLocation.X = this.Text.vLocation.X;
      vector2_2.X = this.scaleHelper.ScaleX(130f);
      float val1 = this.Text.GetSize().Y + this.TextTwo.GetSize().Y;
      if (this.mousebutton == ControllerButton.None)
        vector2_2.Y += val1;
      else
        vector2_2.Y += Math.Max(val1, this.trcButton.GetSize_Static().Y * BaseScale);
      vector2_2.Y += vector2_1.Y;
      this.VSCale = vector2_2;
      if (this.mousebutton != ControllerButton.Mouse_Movement4Way)
        return;
      this.trcButton.SetAllColours(ColourData.Z_OverworldButtonOrange);
    }

    public Vector2 GetSize() => this.VSCale;

    public void UpdateMouseHintRow()
    {
    }

    public void DrawMouseHintRow(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.Location;
      if (this.mousebutton != ControllerButton.None)
        this.trcButton.DrawTRC_ButtonDisplay(spriteBatch, AssetContainer.TRC_Sprites, Offset);
      this.Text.DrawZGenericText(Offset, spriteBatch);
      this.TextTwo.DrawZGenericText(Offset, spriteBatch);
    }
  }
}
