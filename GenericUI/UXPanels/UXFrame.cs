// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.UXPanels.UXFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.GenericUI.UXPanels
{
  internal class UXFrame : GameObject
  {
    public GameObject BrightFrame;
    private GameObject MiddleWhite;
    private Rectangle MouseOverFrame;
    private Rectangle BaseRect;

    public bool IsMouseOver { get; private set; }

    public UXFrame(float _Scale, bool SmallFrame = false, bool VerySmall = false)
    {
      this.scale = _Scale;
      this.BaseRect = new Rectangle(0, 71, 138, 17);
      this.MouseOverFrame = this.BaseRect;
      if (SmallFrame)
      {
        this.BaseRect = new Rectangle(708, 416, 76, 17);
        this.MouseOverFrame = this.BaseRect;
      }
      else if (VerySmall)
      {
        this.BaseRect = new Rectangle(319, 87, 25, 26);
        this.MouseOverFrame = new Rectangle(276, 128, 25, 26);
      }
      this.DrawRect = this.BaseRect;
      this.SetDrawOriginToCentre();
      this.BrightFrame = new GameObject((GameObject) this);
      this.BrightFrame.DrawRect.X += this.DrawRect.Width + 1;
      this.BrightFrame.SetAlpha(0.0f);
      this.MiddleWhite = new GameObject((GameObject) this);
      this.MiddleWhite.DrawRect.X += 2 * (this.DrawRect.Width + 1);
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void UpdateUXFrame(float DeltaTime) => this.BrightFrame.UpdateColours(DeltaTime);

    public void DoFlash() => this.BrightFrame.SetAlpha(false, 0.5f, 1f, 0.0f);

    public bool CheckTaps(Player player, Vector2 Offset, bool UpdateForMouseOver = false)
    {
      bool flag = MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.BaseRect.Width, (float) this.BaseRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
      if (UpdateForMouseOver)
      {
        int num = flag ? 1 : 0;
        this.IsMouseOver = flag;
      }
      return (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && flag;
    }

    public void DrawUXFrame(Vector2 Offset) => this.DrawUXFrame(Offset, AssetContainer.pointspritebatchTop05);

    public void DrawUXFrame(Vector2 Offset, SpriteBatch spritebatch)
    {
      if (this.IsMouseOver)
        this.DrawRect = this.MouseOverFrame;
      else
        this.DrawRect = this.BaseRect;
      this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
      if ((double) this.BrightFrame.fAlpha <= 0.0)
        return;
      this.BrightFrame.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
    }

    public void FlashDraw(Vector2 Offset)
    {
      if ((double) this.BrightFrame.fAlpha <= 0.0)
        return;
      this.MiddleWhite.fAlpha = this.BrightFrame.fAlpha * 0.4f;
      this.MiddleWhite.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
    }
  }
}
