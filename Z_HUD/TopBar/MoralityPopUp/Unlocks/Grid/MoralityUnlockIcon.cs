// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks.Grid.MoralityUnlockIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Morality;

namespace TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks.Grid
{
  internal class MoralityUnlockIcon : GameObject
  {
    private MouseoverHandler mouseOverHandler;
    private SlashCross slash;
    private float BaseScale;

    public MoralityUnlockIcon(MoralityUnlock moralityUnlockType, float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.DrawRect = MoralityUnlocksData.GetIconRectangle(moralityUnlockType);
      this.scale = this.BaseScale;
      if (this.DrawRect == Rectangle.Empty)
      {
        this.DrawRect = TinyZoo.Game1.WhitePixelRect;
        this.scale = 32f * this.BaseScale;
      }
      this.SetDrawOriginToCentre();
      this.mouseOverHandler = new MouseoverHandler(this.GetSize(), this.BaseScale);
    }

    public void AddSlash() => this.slash = new SlashCross(this.BaseScale);

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public bool UpdateMoralityUnlockIcon(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.vLocation;
      this.mouseOverHandler.UpdateMouseoverHandler(player, offset, DeltaTime);
      return this.mouseOverHandler.Clicked;
    }

    public void DrawMoralityUnlockIcon(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
      if (this.slash != null)
        this.slash.DrawSlashCross(offset + this.vLocation, spriteBatch);
      this.mouseOverHandler.DrawMouseOverHandler(spriteBatch, offset);
    }
  }
}
