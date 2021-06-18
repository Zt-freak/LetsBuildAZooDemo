// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Transfer.TransferScreen.Prisoners.TransButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.OverWorld.Transfer.TransferScreen.Prisoners
{
  internal class TransButton : GameObject
  {
    private Rectangle BaseRect;
    private bool MouseOver;
    public bool IsTick;

    public TransButton()
    {
      this.BaseRect = new Rectangle(917, 484, 30, 30);
      this.DrawRect = this.BaseRect;
      this.SetDrawOriginToCentre();
      this.scale = 2f;
      this.IsTick = false;
    }

    public void SetTick(bool isOn)
    {
      if (isOn)
      {
        this.IsTick = true;
        this.BaseRect = new Rectangle(983, 186, 30, 30);
        this.DrawRect = this.BaseRect;
      }
      else
      {
        this.IsTick = false;
        this.BaseRect = new Rectangle(917, 484, 30, 30);
        this.DrawRect = this.BaseRect;
      }
    }

    public bool UpdateTransButton(
      Player player,
      float DeltaTime,
      Vector2 Offset,
      bool IsControllerSelected)
    {
      if (MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height, player.player.touchinput.MultiTouchTouchLocations[0]))
        this.MouseOver = true;
      else if (MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height, player.inputmap.PointerLocation))
        this.MouseOver = true;
      return MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height, player.player.touchinput.ReleaseTapArray[0]) || IsControllerSelected && GameFlags.IsUsingController && player.inputmap.PressedThisFrame[12];
    }

    public void DrawTransButton(Vector2 Offset)
    {
      if (this.MouseOver)
      {
        this.DrawRect.Y = this.BaseRect.Y + this.BaseRect.Height + 1;
        this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
        this.DrawRect.Y = this.BaseRect.Y;
        this.MouseOver = false;
      }
      else
        this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
    }
  }
}
