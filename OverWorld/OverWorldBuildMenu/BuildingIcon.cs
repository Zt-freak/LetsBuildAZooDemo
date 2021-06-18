// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.BuildingIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Tile_Data;

namespace TinyZoo.OverWorld.OverWorldBuildMenu
{
  internal class BuildingIcon : GameObject
  {
    private GameObject Frame;

    public BuildingIcon(TILETYPE tiletype, float _Scale)
    {
      this.DrawRect = TileStats.GetBuildingIconRectangle(tiletype);
      if (this.DrawRect.Width > 16)
        this.DrawRect = TileData.GetTileInfo(tiletype).GetRect(0, ref this.Rotation);
      this.DrawRect.Width = 16;
      this.DrawRect.Height = 16;
      this.scale = _Scale;
      this.SetDrawOriginToCentre();
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public bool UpdateBuildingIcon(Vector2 Offset, Player player) => (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height, player.player.touchinput.ReleaseTapArray[0]);

    public void DrawBuildingIcon(Vector2 Offset, float MasterAlpha, SpriteBatch spritebatch) => this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset, MasterAlpha);
  }
}
