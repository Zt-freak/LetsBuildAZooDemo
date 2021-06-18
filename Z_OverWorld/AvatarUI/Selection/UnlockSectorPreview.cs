// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.AvatarUI.Selection.UnlockSectorPreview
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;

namespace TinyZoo.Z_OverWorld.AvatarUI.Selection
{
  internal class UnlockSectorPreview : GameObject
  {
    private float BSCALE;
    private Vector2Int Locked_TopLeft;

    public UnlockSectorPreview() => this.Create();

    private void Create()
    {
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToCentre();
      this.SetAllColours(0.0f, 0.0f, 0.8f);
      this.SetAlpha(0.2f);
      this.BSCALE = (float) (TileMath.SectorSize * 16);
    }

    public UnlockSectorPreview(Vector2Int LockHere)
    {
      this.Locked_TopLeft = new Vector2Int(LockHere.X / TileMath.SectorSize, LockHere.Y / TileMath.SectorSize);
      this.Locked_TopLeft.X *= TileMath.SectorSize;
      this.Locked_TopLeft.Y *= TileMath.SectorSize;
      this.Locked_TopLeft.X += TileMath.SectorSize / 2;
      this.Locked_TopLeft.Y += TileMath.SectorSize / 2;
      this.Create();
      this.SetAlpha(0.45f);
    }

    public void DrawSectorPreviewSet()
    {
      this.scale = this.BSCALE * Sengine.WorldOriginandScale.Z;
      this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, RenderMath.TranslateWorldSpaceToScreenSpace(TileMath.GetTileToWorldSpace(this.Locked_TopLeft)));
    }

    public void DrawSectorPreview(Vector2 Offset, SpriteBatch spriteBatch)
    {
      if (OverWorldManager.overworldstate == OverWOrldState.MainMenu)
      {
        if (SellUIManager.selectedtileandsell != null && SellUIManager.selectedtileandsell.IsSomethingSelected())
          this.SetAlpha(0.05f);
        else
          this.SetAlpha(0.2f);
      }
      else
        this.SetAlpha(0.05f);
      this.scale = this.BSCALE * Sengine.WorldOriginandScale.Z;
      this.Draw(spriteBatch, AssetContainer.SpriteSheet, Offset);
    }
  }
}
