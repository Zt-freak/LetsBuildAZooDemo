// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.AvatarUI.Selection.CursorIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_OverWorld.AvatarUI.Selection
{
  internal class CursorIcon : GameObject
  {
    private Vector2 OverrideLocation;
    public TILETYPE tiletype;
    public CURSOR_ACTIONTYPE actiontype;

    public CursorIcon() => this.bActive = false;

    public void SetIcon(CURSOR_ACTIONTYPE _actiontype, Player player, Vector2Int location)
    {
      this.actiontype = _actiontype;
      this.bActive = false;
      if (this.actiontype == CURSOR_ACTIONTYPE.Count)
        return;
      if (this.actiontype != CURSOR_ACTIONTYPE.SelectedBuilding)
        this.SetRect(this.actiontype);
      this.bActive = true;
    }

    private void SetRect(CURSOR_ACTIONTYPE actiontype)
    {
      switch (actiontype)
      {
        case CURSOR_ACTIONTYPE.CollectTrash:
          this.DrawRect = new Rectangle(46, 41, 11, 10);
          break;
        case CURSOR_ACTIONTYPE.BuildThing:
          this.DrawRect = new Rectangle(410, 148, 32, 32);
          break;
        case CURSOR_ACTIONTYPE.RepairBuilding:
          this.DrawRect = new Rectangle(58, 41, 10, 10);
          break;
      }
      this.SetDrawOriginToPoint(DrawOriginPosition.TopLeft);
    }

    public void UpdateCURSOR_ACTIONTYPE()
    {
    }

    public void DrawCursorIcon(Vector2 Location, SpriteBatch spritebatch)
    {
      this.vLocation = Location;
      this.scale = Sengine.WorldOriginandScale.Z * 0.5f;
      if ((double) this.scale < 1.0)
        this.scale = 1f;
      this.Draw(spritebatch, AssetContainer.SpriteSheet);
    }
  }
}
