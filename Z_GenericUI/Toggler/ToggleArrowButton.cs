// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.Toggler.ToggleArrowButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_GenericUI.Toggler
{
  internal class ToggleArrowButton
  {
    public Vector2 location;
    private GameObject toggleArrow;
    private MouseoverHandler mouseOverHandler;

    public ToggleArrowButton(float BaseScale, bool IsFacingRight = true)
    {
      this.toggleArrow = new GameObject();
      this.toggleArrow.DrawRect = new Rectangle(908, 440, 22, 22);
      this.toggleArrow.scale = BaseScale;
      this.toggleArrow.SetDrawOriginToCentre();
      if (!IsFacingRight)
        this.toggleArrow.FlipRender = true;
      this.mouseOverHandler = new MouseoverHandler(this.GetSize(), BaseScale);
    }

    public Vector2 GetSize() => new Vector2((float) this.toggleArrow.DrawRect.Width, (float) this.toggleArrow.DrawRect.Height) * this.toggleArrow.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public bool UpdateToggleArrowButton(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.mouseOverHandler.UpdateMouseoverHandler(player, offset, DeltaTime);
      return MathStuff.CheckPointCollision(true, offset, 1f, this.GetSize().X, this.GetSize().Y, player.player.touchinput.ReleaseTapArray[0]);
    }

    public void DrawToggleArrowButton(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.toggleArrow.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
      this.mouseOverHandler.DrawMouseOverHandler(spriteBatch, offset);
    }
  }
}
