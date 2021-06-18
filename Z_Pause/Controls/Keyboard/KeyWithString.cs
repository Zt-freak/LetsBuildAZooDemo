// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Pause.Controls.Keyboard.KeyWithString
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SEngine;
using SEngine.Input;
using TinyZoo.Z_GenericUI;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.Z_Pause.Controls.Keyboard
{
  internal class KeyWithString : GameObject
  {
    private ZGenericText TextObject;
    public TRC_ButtonDisplay trcbutton;
    public TRC_ButtonDisplay trcbutton2;

    public KeyWithString(Keys thiskey, float _BaseScale)
    {
      this.scale = _BaseScale * 0.5f;
      this.CreateTextObject();
      string str = PCKeyToString.GetPCKeyToString(thiskey);
      if (thiskey == Keys.Back)
        str = "Back";
      if (thiskey >= Keys.D0 && thiskey <= Keys.D9)
        str = str.Substring(1, 1);
      this.TextObject.textToWrite = str;
      if (thiskey == Keys.Space)
        this.DrawRect = new Rectangle(232, 489, 194, 44);
      else if (str.Length > 2 || thiskey == Keys.Down)
      {
        this.DrawRect = new Rectangle(492, 229, 87, 44);
        this.SetScale(_BaseScale);
      }
      else
        this.DrawRect = new Rectangle(427, 489, 44, 44);
      this.SetDrawOriginToCentre();
    }

    private void CreateTextObject()
    {
      this.TextObject = new ZGenericText(this.scale * 2f, _UseOnePointFiveFont: true);
      this.TextObject.SetAllColours(0.0f, 0.0f, 0.0f);
      this.TextObject.SetAlpha(0.8f);
    }

    public KeyWithString(ControllerButton mousebutton, ControllerButton mousebutton2)
    {
      this.CreateTextObject();
      this.trcbutton = new TRC_ButtonDisplay(3f);
      this.trcbutton.SetAsStaticButton(ControllerType.NintendoSwitch, ButtonStyle.SuperSmall, mousebutton);
      if (mousebutton2 == ControllerButton.None)
        return;
      this.trcbutton2 = new TRC_ButtonDisplay(3f);
      this.trcbutton2.SetAsStaticButton(ControllerType.NintendoSwitch, ButtonStyle.SuperSmall, mousebutton2);
    }

    public void SetScale(float _SCALE)
    {
      this.scale = _SCALE * 0.5f;
      this.TextObject.scale = _SCALE;
      double x = (double) this.TextObject.GetSize().X;
      float num1 = this.GetSize().X - 10f * this.scale;
      double num2 = (double) num1;
      if (x <= num2)
        return;
      this.TextObject.scale /= this.GetSize().X / num1;
    }

    public Vector2 GetSize() => new Vector2(this.GetWidth(), this.GetHeight(true));

    public float GetWidth() => (float) this.DrawRect.Width * this.scale;

    public float GetHeight(bool GetWithMult) => GetWithMult ? (float) this.DrawRect.Height * this.scale * Sengine.ScreenRatioUpwardsMultiplier.Y : (float) this.DrawRect.Height * this.scale;

    public void DrawKeyWithString(Vector2 Offset, SpriteBatch spritebatch)
    {
      if (this.trcbutton != null)
      {
        this.trcbutton.DrawTRC_ButtonDisplay(spritebatch, AssetContainer.TRC_Sprites, Offset);
        if (this.trcbutton2 != null)
          this.trcbutton2.DrawTRC_ButtonDisplay(spritebatch, AssetContainer.TRC_Sprites, Offset);
      }
      else
        this.Draw(spritebatch, AssetContainer.TRC_Sprites, Offset);
      if (this.TextObject == null)
        return;
      this.TextObject.DrawZGenericText(Offset + this.vLocation, spritebatch);
    }
  }
}
