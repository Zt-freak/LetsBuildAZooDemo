// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.PenBuilder.Pens.EntranceArrow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_BuldMenu.PenBuilder.Pens
{
  internal class EntranceArrow : GameObject
  {
    public EntranceSpriteType entrancespritetype;
    internal static Rectangle BlockRect = new Rectangle(78, 31, 16, 16);
    internal static Rectangle DestroyRect = new Rectangle(53, 307, 16, 16);
    public Vector2Int TempWorldLocation;
    private bool WillBlockDraw;

    public EntranceArrow(int Rotation)
    {
      this.entrancespritetype = EntranceSpriteType.EntryArrow;
      this.bActive = true;
      switch (Rotation)
      {
        case -1:
          this.DrawRect = new Rectangle(111, 12, 16, 16);
          this.SetDrawOriginToCentre();
          this.SetAllColours(1f, 0.0f, 1f);
          break;
        case 0:
          this.DrawRect = new Rectangle(33, 53, 16, 16);
          break;
        case 1:
          this.DrawRect = new Rectangle(0, 53, 15, 16);
          break;
        case 2:
          this.DrawRect = new Rectangle(16, 53, 16, 16);
          break;
        default:
          this.DrawRect = new Rectangle(50, 53, 15, 16);
          break;
      }
      this.SetDrawOriginToCentre();
    }

    public void BlockEntrance(bool _WillBlockDraw)
    {
      this.WillBlockDraw = _WillBlockDraw;
      this.SetAllColours(1f, 0.0f, 0.0f);
    }

    public void UnblockEntrance()
    {
      this.WillBlockDraw = false;
      this.SetAllColours(1f, 1f, 1f);
    }

    public void SetAsBlocked()
    {
      this.entrancespritetype = EntranceSpriteType.Block;
      this.DrawRect = EntranceArrow.BlockRect;
      this.SetDrawOriginToCentre();
    }

    public void DrawEntrance(Vector2 Offset, SpriteBatch spritebatch)
    {
      if (this.WillBlockDraw)
        return;
      this.WorldOffsetDraw(spritebatch, AssetContainer.SpriteSheet, this.vLocation + Offset, this.scale, 0.0f);
    }

    public void DrawEntrance(SpriteBatch spritebatch) => this.WorldOffsetDraw(spritebatch, AssetContainer.SpriteSheet, this.vLocation, this.scale, 0.0f);
  }
}
