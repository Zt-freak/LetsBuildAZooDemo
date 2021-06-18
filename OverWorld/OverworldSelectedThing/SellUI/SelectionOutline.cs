// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectionOutline
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.Graves;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Employees.WorkZonePane;
using TinyZoo.Z_PenInfo;

namespace TinyZoo.OverWorld.OverworldSelectedThing.SellUI
{
  internal class SelectionOutline
  {
    private GameObject HighLight;
    private GameObject TopLeft;
    private GameObject TopRight;
    private GameObject BottomLeft;
    private GameObject BottomRight;
    private List<SelectedPenHightlight> selections;
    public int PrizonZoneUID;

    public SelectionOutline(
      Vector2Int Location,
      TileInfo tileinfo,
      PrisonZone zone,
      GraveYardBlockInfo graveyard,
      TILETYPE tiletype,
      int RotationClockWise,
      WorkZone workzone = null)
    {
      float num = 12f;
      this.TopLeft = new GameObject();
      this.TopLeft.DrawRect = new Rectangle(128, 12, 8, 8);
      this.TopLeft.DrawOrigin = new Vector2(num, num);
      if (workzone != null)
        this.TopLeft.vLocation = TileMath.GetTileToWorldSpace(workzone.TopLeft);
      else
        this.TopLeft.vLocation = TileMath.GetTileToWorldSpace(Location + new Vector2Int(tileinfo.GetIntOrigin(RotationClockWise).X * -1, tileinfo.GetIntOrigin(RotationClockWise).Y * -1));
      this.TopRight = new GameObject();
      this.TopRight.DrawRect = new Rectangle(137, 12, 8, 8);
      this.TopRight.DrawOrigin = new Vector2(8f - num, num);
      if (workzone != null)
        this.TopRight.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(workzone.BottomRight.X, workzone.TopLeft.Y));
      else
        this.TopRight.vLocation = TileMath.GetTileToWorldSpace(Location + new Vector2Int(tileinfo.GetTileWidth(RotationClockWise) - tileinfo.GetIntOrigin(RotationClockWise).X - 1, tileinfo.GetIntOrigin(RotationClockWise).Y * -1));
      this.BottomLeft = new GameObject();
      this.BottomLeft.DrawRect = new Rectangle(128, 21, 8, 8);
      this.BottomLeft.DrawOrigin = new Vector2(num, 8f - num);
      if (workzone != null)
        this.BottomLeft.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(workzone.TopLeft.X, workzone.BottomRight.Y));
      else
        this.BottomLeft.vLocation = TileMath.GetTileToWorldSpace(Location + new Vector2Int(tileinfo.GetIntOrigin(RotationClockWise).X * -1, tileinfo.GetTileHeight(RotationClockWise) - tileinfo.GetIntOrigin(RotationClockWise).Y - 1));
      this.BottomRight = new GameObject();
      this.BottomRight.DrawRect = new Rectangle(137, 21, 8, 8);
      this.BottomRight.DrawOrigin = new Vector2(8f - num, 8f - num);
      if (workzone != null)
        this.BottomRight.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(workzone.BottomRight.X, workzone.BottomRight.Y));
      else
        this.BottomRight.vLocation = TileMath.GetTileToWorldSpace(Location + new Vector2Int(tileinfo.GetTileWidth(RotationClockWise) - tileinfo.GetIntOrigin(RotationClockWise).X - 1, tileinfo.GetTileHeight(RotationClockWise) - tileinfo.GetIntOrigin(RotationClockWise).Y - 1));
      if (zone != null)
      {
        this.PrizonZoneUID = zone.Cell_UID;
        if (zone.IsIerregular)
        {
          Vector2 TopLeft;
          Vector2 BottomRight;
          zone.GetLimits(out TopLeft, out BottomRight);
          this.TopLeft.vLocation = TopLeft;
          this.TopRight.vLocation = TopLeft;
          this.TopRight.vLocation.X = BottomRight.X;
          this.BottomLeft.vLocation = BottomRight;
          this.BottomLeft.vLocation.X = TopLeft.X;
          this.BottomRight.vLocation = BottomRight;
          this.selections = new List<SelectedPenHightlight>();
          for (int index = 0; index < zone.FloorTiles.Count; ++index)
            this.selections.Add(new SelectedPenHightlight(zone.FloorTiles[index]));
          for (int index = 0; index < zone.FenceTiles.Count; ++index)
            this.selections.Add(new SelectedPenHightlight(zone.FenceTiles[index]));
        }
        else
        {
          this.TopRight.vLocation = TileMath.GetTileToWorldSpace(zone.GetTopRight());
          this.TopLeft.vLocation = TileMath.GetTileToWorldSpace(zone.GetTopLeft());
          this.BottomLeft.vLocation = TileMath.GetTileToWorldSpace(zone.GetBottomLeft());
          this.BottomRight.vLocation = TileMath.GetTileToWorldSpace(zone.GetBottomRight());
        }
      }
      else if (graveyard != null && tiletype != TILETYPE.GraveYard_FloorGraveStone)
      {
        this.TopRight.vLocation = TileMath.GetTileToWorldSpace(graveyard.GetTopRight());
        this.TopLeft.vLocation = TileMath.GetTileToWorldSpace(graveyard.GetTopLeft());
        this.BottomLeft.vLocation = TileMath.GetTileToWorldSpace(graveyard.GetBottomLeft());
        this.BottomRight.vLocation = TileMath.GetTileToWorldSpace(graveyard.GetBottomRight());
      }
      else
      {
        if (this.selections != null)
          return;
        this.selections = new List<SelectedPenHightlight>();
        if (workzone == null)
        {
          TileInfo tileInfo = TileData.GetTileInfo(tiletype);
          for (int index1 = 0; index1 < tileInfo.GetTileWidth(RotationClockWise); ++index1)
          {
            for (int index2 = 0; index2 < tileInfo.GetTileHeight(RotationClockWise); ++index2)
              this.selections.Add(new SelectedPenHightlight(new Vector2Int(Location.X + index1 - tileInfo.GetXTileOrigin(RotationClockWise), Location.Y + index2 - tileInfo.GetYTileOrigin(RotationClockWise))));
          }
        }
        else
        {
          for (int x = workzone.TopLeft.X; x < workzone.BottomRight.X; ++x)
          {
            for (int y = workzone.TopLeft.Y; y < workzone.BottomRight.Y; ++y)
              this.selections.Add(new SelectedPenHightlight(new Vector2Int(x, y)));
          }
        }
      }
    }

    public void UpdateSelectionOutline()
    {
    }

    public float GetTopInScreenSpace() => RenderMath.TranslateWorldSpaceToScreenSpace(this.TopLeft.vLocation).Y;

    public float GetLeftInScreenSpace() => RenderMath.TranslateWorldSpaceToScreenSpace(this.TopLeft.vLocation).X;

    public float GetRightInScreenSpace() => RenderMath.TranslateWorldSpaceToScreenSpace(this.BottomRight.vLocation).X;

    public void DrawSelectionOutline()
    {
      if (this.selections != null)
      {
        for (int index = 0; index < this.selections.Count; ++index)
          this.selections[index].DrawSelectedPenHightlight();
      }
      this.TopLeft.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
      this.TopRight.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
      this.BottomLeft.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
      this.BottomRight.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
    }
  }
}
