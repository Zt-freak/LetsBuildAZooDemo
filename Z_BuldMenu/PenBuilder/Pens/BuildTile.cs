// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.PenBuilder.Pens.BuildTile
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_BuldMenu.PenBuilder.Pens
{
  internal class BuildTile : GameObject
  {
    public Vector2Int TileLocation;
    private PerimeterTileStatus thistilestatus;
    private bool Blocked;
    public bool MouseIsBlocked;
    private GameObject LinkIcon;
    public CellCornerType cellcornertype;

    public BuildTile(Vector2Int _TileLocation, PerimeterTileStatus tilestatus)
    {
      this.thistilestatus = tilestatus;
      this.TileLocation = new Vector2Int(_TileLocation);
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToCentre();
      this.vLocation = TileMath.GetTileToWorldSpace(this.TileLocation);
      this.scale = TileMath.TileSize;
      if (tilestatus != PerimeterTileStatus.Commited)
        this.CheckBlocked();
      else
        this.SetGreen();
      if (tilestatus == PerimeterTileStatus.TempWall)
        this.SetAlpha(0.7f);
      if (tilestatus != PerimeterTileStatus.Floor)
        return;
      this.SetGreen();
      this.SetAlpha(0.45f);
    }

    public void SetReady() => this.SetAllColours(1f, 1f, 0.0f);

    public void SetLinkIcon(bool IsClose = false, bool Nullify = false, bool IsMouse = false)
    {
      if (Nullify)
      {
        this.LinkIcon = (GameObject) null;
      }
      else
      {
        this.LinkIcon = new GameObject();
        if (IsClose)
        {
          this.LinkIcon.DrawRect = new Rectangle(70, 48, 24, 22);
        }
        else
        {
          this.LinkIcon.DrawRect = new Rectangle(95, 53, 16, 17);
          if (IsMouse)
            this.LinkIcon.DrawRect = new Rectangle(112, 53, 16, 17);
        }
        this.LinkIcon.SetDrawOriginToCentre();
      }
    }

    public void SetMouseIsBlocked(bool _IsBlocked)
    {
      this.MouseIsBlocked = _IsBlocked;
      this.Blocked = _IsBlocked;
      if (_IsBlocked)
        this.SetRed();
      else
        this.SetGreen();
    }

    private void CheckBlocked()
    {
      this.SetGreen();
      this.Blocked = false;
      if (TileMath.TileIsOverEntryPath(this.TileLocation.X, this.TileLocation.Y))
      {
        this.Blocked = true;
        this.SetRed();
      }
      if (TileMath.TileIsInWorld(this.TileLocation.X, this.TileLocation.Y) && !Z_GameFlags.pathfinder.GetIsBlocked(this.TileLocation.X, this.TileLocation.Y))
        return;
      this.Blocked = true;
      this.SetRed();
    }

    public bool TryAndMove(Vector2Int _TileLocation)
    {
      if (_TileLocation.CompareMatches(this.TileLocation))
        return false;
      this.TileLocation = new Vector2Int(_TileLocation);
      this.vLocation = TileMath.GetTileToWorldSpace(this.TileLocation);
      this.CheckBlocked();
      this.MouseIsBlocked = this.Blocked;
      return true;
    }

    public void SetGreen() => this.SetAllColours(0.0f, 1f, 0.0f);

    public void SetRed() => this.SetAllColours(1f, 0.0f, 0.0f);

    public void RedFlash()
    {
      this.SetGreen();
      this.SetPrimaryColours(0.5f, new Vector3(1f, 0.0f, 0.0f));
    }

    public void UpdateBuildTile(float DeltaTime)
    {
      if (this.thistilestatus == PerimeterTileStatus.AttachedToMouse)
      {
        if (this.Blocked)
          this.ColourCycle(1f, 1f, 0.2f, 0.2f, 0.8f, 0.8f, 0.8f);
        else
          this.ColourCycle(1f, 0.2f, 1f, 0.2f, 0.8f, 0.8f, 0.8f);
        this.SetAlpha(0.5f);
      }
      this.UpdateColours(DeltaTime);
    }

    public void DrawBuildTile(bool IsInPreviewStateForPlacingGate = false)
    {
      this.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
      if (this.LinkIcon == null || IsInPreviewStateForPlacingGate)
        return;
      this.LinkIcon.vLocation = this.vLocation;
      this.LinkIcon.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
    }

    public void DrawBuildTileWithOffset(Vector2 Offset) => this.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, this.vLocation + Offset, this.scale, this.Rotation);
  }
}
