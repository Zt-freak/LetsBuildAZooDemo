// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.WorkZonePane.CheckBoxWithString
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Employees.WorkZonePane
{
  internal class CheckBoxWithString
  {
    private string TextToWrite;
    private ZCheckBox checkbox;
    private GameObject TextObject;
    public Vector2 Location;
    private Vector2 InternalOffsetToCenter;
    private float basescale;

    public CheckBoxWithString(
      string _TextToWrite,
      bool isChecked,
      float BaseScale,
      bool CenterJustify = false)
    {
      this.basescale = BaseScale;
      this.TextToWrite = _TextToWrite;
      this.checkbox = new ZCheckBox(BaseScale);
      this.checkbox.SetTicked(isChecked);
      this.TextObject = new GameObject();
      this.TextObject.scale = BaseScale;
      this.TextObject.vLocation.X = BaseScale * -20f;
      this.InternalOffsetToCenter = this.GetSize();
      this.TextObject.vLocation.Y -= BaseScale * 3f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      if (CenterJustify)
      {
        this.InternalOffsetToCenter.Y = 0.0f;
        this.InternalOffsetToCenter.X *= 0.5f;
        this.InternalOffsetToCenter.X -= (float) ((double) this.checkbox.checkBox.DrawRect.Width * (double) this.checkbox.checkBox.scale * 0.5);
      }
      else
      {
        this.InternalOffsetToCenter.Y = 0.0f;
        this.InternalOffsetToCenter.X = 0.0f;
      }
    }

    public void SetTextColour(Vector3 colour) => this.TextObject.SetAllColours(colour);

    public Vector2 GetBoxSize() => new UIScaleHelper(this.basescale).ScaleVector2(new Vector2((float) this.checkbox.checkBox.DrawRect.Width, (float) this.checkbox.checkBox.DrawRect.Height));

    public Vector2 GetSize()
    {
      Vector2 vector2 = AssetContainer.springFont.MeasureString(this.TextToWrite) * this.TextObject.scale;
      vector2.X += -this.TextObject.vLocation.X;
      vector2.X += (float) ((double) this.checkbox.checkBox.DrawRect.Width * (double) this.checkbox.checkBox.scale * 0.5);
      vector2.Y = Math.Max((float) this.checkbox.checkBox.DrawRect.Height * this.checkbox.checkBox.scale * Sengine.ScreenRatioUpwardsMultiplier.Y, vector2.Y);
      return vector2;
    }

    public bool IsTicked() => this.checkbox.GetIsTicked();

    public bool UpdateCheckBoxWithString(Player player, Vector2 Offset)
    {
      Offset += this.Location + this.InternalOffsetToCenter;
      if (!this.checkbox.UpdateCheckBox(player, Offset))
        return false;
      this.checkbox.SetTicked(!this.checkbox.GetIsTicked());
      return true;
    }

    public void DrawCheckBoxWithString(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location + this.InternalOffsetToCenter;
      TextFunctions.DrawTextWithDropShadow(this.TextToWrite, this.TextObject.scale, this.TextObject.vLocation + Offset, this.TextObject.GetColour(), this.TextObject.fAlpha, AssetContainer.springFont, spritebatch, false, true);
      this.checkbox.DrawCheckBox(spritebatch, Offset);
    }
  }
}
