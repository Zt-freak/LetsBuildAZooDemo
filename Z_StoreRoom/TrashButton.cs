// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.TrashButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_StoreRoom
{
  internal class TrashButton : GameObject
  {
    public TrashButton()
    {
      this.DrawRect = new Rectangle(855, 463, 28, 28);
      this.SetDrawOriginToCentre();
      this.scale = 1f;
    }

    public bool UpdateTrashButton(Vector2 Offset, Player player) => MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]);

    public void DrawTrashButton(Vector2 Offset, SpriteBatch spritebatch) => this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
  }
}
