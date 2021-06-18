// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.StoreIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.OverWorld.Store_Local.Entries;

namespace TinyZoo.OverWorld
{
  internal class StoreIcon : GameObject
  {
    private GameObject Centre;

    public StoreIcon(StoreEntryType icontype, float _Scale = 1f)
    {
      this.scale = RenderMath.GetPixelSizeBestMatch(_Scale);
      this.Centre = new GameObject();
      switch (icontype)
      {
        case StoreEntryType.BasicBeam:
          this.DrawRect = new Rectangle(24, 309, 24, 24);
          break;
        case StoreEntryType.BeamSpeed:
          this.DrawRect = new Rectangle(72, 309, 24, 24);
          break;
        case StoreEntryType.BeamL2:
          this.DrawRect = new Rectangle(96, 309, 24, 24);
          break;
        case StoreEntryType.InstaBeam:
          this.DrawRect = new Rectangle(48, 309, 24, 24);
          break;
        case StoreEntryType.BeamSpeedL2:
          this.DrawRect = new Rectangle(0, 309, 24, 24);
          break;
      }
      this.SetDrawOriginToCentre();
      this.Centre.DrawRect = this.DrawRect;
      this.Centre.DrawRect.Y += 25;
      this.Centre.SetDrawOriginToCentre();
    }

    public void UpdateStoreIcon()
    {
    }

    public void DrawStoreIcon(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
      this.Centre.scale = this.scale;
      this.Centre.Draw(spritebatch, AssetContainer.SpriteSheet, Offset + this.vLocation);
    }

    public void DrawStoreIcon(Vector2 Offset) => this.DrawStoreIcon(Offset, AssetContainer.pointspritebatchTop05);
  }
}
