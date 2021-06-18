// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.ManageEmployeeMain.HiringSummary.SpinningProgressIconWithText
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_ManageEmployees.ManageEmployeeMain.HiringSummary
{
  internal class SpinningProgressIconWithText
  {
    public Vector2 location;
    private SpinningInProgressIcon icon;
    private ZGenericText text;
    private Vector2 size;

    public SpinningProgressIconWithText(float BaseScale, string TextToWrite)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.icon = new SpinningInProgressIcon(BaseScale);
      this.icon.vLocation.X += this.icon.GetSize().X * 0.5f;
      this.size.X = this.icon.GetSize().X;
      this.size.X += uiScaleHelper.DefaultBuffer.X;
      this.text = new ZGenericText(TextToWrite, BaseScale, false);
      this.text.vLocation.X = this.size.X;
      this.text.vLocation.Y -= this.text.GetSize().Y * 0.5f;
      this.size.X += this.text.GetSize().X;
      this.size.Y = Math.Max(this.icon.GetSize().Y, this.text.GetSize().Y);
    }

    public Vector2 GetSize() => this.size;

    public void UpdateSpinningProgressIconWithText(float DeltaTime) => this.icon.UpdateSpinningInProgressIcon(DeltaTime);

    public void DrawSpinningProgressIconWithText(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.icon.DrawSpinningInProgressIcon(offset, spriteBatch);
      this.text.DrawZGenericText(offset, spriteBatch);
    }
  }
}
