// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Shared.Grid.PlusInFrameButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Collection.Shared.Grid
{
  internal class PlusInFrameButton
  {
    public Vector2 location;
    private GameObject plus;
    private MouseoverHandler mouseOverHighlight;

    public PlusInFrameButton(float BaseScale) => this.Create(BaseScale);

    private void Create(float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.plus = new GameObject();
      this.plus.DrawRect = new Rectangle(920, 327, 22, 22);
      this.plus.scale = BaseScale;
      this.plus.SetDrawOriginToCentre();
      this.mouseOverHighlight = new MouseoverHandler(uiScaleHelper.ScaleX((float) this.plus.DrawRect.Width), uiScaleHelper.ScaleY((float) this.plus.DrawRect.Height), BaseScale);
    }

    public bool UpdatePlusInFrameButton(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.mouseOverHighlight.UpdateMouseoverHandler(player, offset, DeltaTime);
      return MathStuff.CheckPointCollision(true, offset, this.plus.scale, (float) this.plus.DrawRect.Width, (float) this.plus.DrawRect.Height, player.player.touchinput.ReleaseTapArray[0]);
    }

    public void DrawPlusInFrameButton(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.plus.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
      this.mouseOverHighlight.DrawMouseOverHandler(spriteBatch, offset);
    }
  }
}
