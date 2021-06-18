// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.DiscordButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.GenericUI
{
  internal class DiscordButton : AnimatedGameObject
  {
    public bool MouseOver;
    private Texture2D sheets;

    public DiscordButton(bool IsDiscord = true)
    {
      this.sheets = AssetContainer.SocialUI;
      this.DrawRect = new Rectangle(0, 144, 67, 70);
      this.SetDrawOriginToCentre();
      this.SetUpSimpleAnimation(5, 0.2f);
      this.scale = RenderMath.GetPixelSizeBestMatch(1f);
      if (IsDiscord)
        return;
      this.sheets = AssetContainer.UISheet;
      this.IsAnimating = false;
      this.DrawRect = new Rectangle(516, 272, 57, 50);
      this.SetDrawOriginToCentre();
    }

    public bool UpdateDiscordButton(float DeltaTime, Player player, Vector2 Offset)
    {
      this.UpdateAnimation(DeltaTime);
      this.MouseOver = MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height, player.inputmap.PointerLocation);
      return (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height, player.player.touchinput.ReleaseTapArray[0]);
    }

    public void DrawDiscordButton(Vector2 Offset)
    {
      this.Draw(AssetContainer.pointspritebatchTop05, this.sheets, Offset);
      if (!this.MouseOver)
        return;
      this.Draw(AssetContainer.pointspritebatchTop05, this.sheets, Offset);
    }
  }
}
