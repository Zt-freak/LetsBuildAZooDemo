// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Pause.Elements.PhotoModeRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.Z_Pause.Elements
{
  internal class PhotoModeRow
  {
    public Vector2 location;
    private ZGenericText text;
    private LittleSummaryButton littlesummaryButton;
    private Vector2 size;

    public PhotoModeRow(float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.text = new ZGenericText("Camera Mode", BaseScale, false, _UseOnePointFiveFont: true);
      this.littlesummaryButton = new LittleSummaryButton(LittleSummaryButtonType.Camera, _BaseScale: BaseScale);
      this.size = Vector2.Zero;
      this.text.vLocation.X = this.size.X;
      this.text.vLocation.Y -= this.text.GetSize().Y * 0.5f;
      this.size.X += this.text.GetSize().X;
      this.size.X += defaultBuffer.X;
      this.littlesummaryButton.vLocation.X = this.size.X;
      this.littlesummaryButton.vLocation.X += this.littlesummaryButton.GetSize().X * 0.5f;
      this.size.X += this.littlesummaryButton.GetSize().X;
      this.size.Y = Math.Max(this.text.GetSize().Y, this.littlesummaryButton.GetSize().Y);
    }

    public Vector2 GetSize() => this.size;

    public bool UpdatePhotoModeRow(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      return this.littlesummaryButton.UpdateLittleSummaryButton(DeltaTime, player, offset);
    }

    public void DrawPhotoModeRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.text.DrawZGenericText(offset, spriteBatch);
      this.littlesummaryButton.DrawLittleSummaryButton(offset, spriteBatch);
    }
  }
}
