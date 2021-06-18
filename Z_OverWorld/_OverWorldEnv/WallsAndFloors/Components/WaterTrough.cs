// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.WaterTrough
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_DayNight;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components
{
  internal class WaterTrough : RenderComponent
  {
    private Rectangle FullRect;
    private Rectangle EmptyRect;
    private List<Vector2Int> Water;
    private bool HasSetTopRect;

    public WaterTrough(TileRenderer parent)
      : base(parent, RenderComponentType.WaterTrough)
    {
      this.HasSetTopRect = false;
      TileInfo tileInfo = TileData.GetTileInfo(TileData.GetWaterTroughToEmptyTrough(parent.tiletypeonconstruct));
      TileRenderer tileRenderer = new TileRenderer(new LayoutEntry(TileData.GetWaterTroughToEmptyTrough(parent.tiletypeonconstruct)), parent.TileLocation.X, parent.TileLocation.Y, false, true);
      int x = parent.TileLocation.X;
      int y = parent.TileLocation.Y;
      int rotationOnConstruct = parent.RotationOnConstruct;
      TileRenderer _parent = tileRenderer;
      this.EmptyRect = new ZooBuildingTopRenderer(tileInfo, x, y, rotationOnConstruct, _parent).DrawRect;
      this.Water = new List<Vector2Int>();
      for (int index1 = 0; index1 < parent.XWidth; ++index1)
      {
        for (int index2 = 0; index2 < parent.YHeight; ++index2)
        {
          this.Water.Add(new Vector2Int(index1 - parent.XOrigin, index2 - parent.YOrigin));
          this.Water[this.Water.Count - 1].X += parent.TileLocation.X;
          this.Water[this.Water.Count - 1].Y += parent.TileLocation.Y;
        }
      }
    }

    public override void SetUpAfterCreatingTopLayer(
      ZooBuildingTopRenderer toplayer,
      TileRenderer parent)
    {
      if (parent.RefTopRenderer == null)
        return;
      this.HasSetTopRect = true;
      this.FullRect = toplayer.DrawRect;
      parent.RefTopRenderer.DrawRect = this.EmptyRect;
      for (int index = 0; index < this.Water.Count; ++index)
      {
        if (OverWorldManager.heatmapmanager.GetHasWaterAccess(this.Water[index].X, this.Water[index].Y))
          parent.RefTopRenderer.DrawRect = this.FullRect;
      }
    }

    public override void StartNewDay(TileRenderer parent, Player player)
    {
    }

    public override bool UpdateRenderComponent(TileRenderer parent, float DeltaTime)
    {
      if (parent.RefTopRenderer != null && Z_GameFlags.DidSomethingWithWater)
      {
        parent.RefTopRenderer.DrawRect = this.EmptyRect;
        for (int index = 0; index < this.Water.Count; ++index)
        {
          if (OverWorldManager.heatmapmanager.GetHasWaterAccess(this.Water[index].X, this.Water[index].Y))
            parent.RefTopRenderer.DrawRect = this.FullRect;
        }
      }
      return false;
    }

    public override void DrawRenderComponent(
      TileRenderer parent,
      Texture2D drawWIthThis,
      SpriteBatch spritebatch,
      float ALphaMod,
      ref Vector2 ThreadLoc,
      ref Vector2 ThreadScale,
      bool IsTopLayer)
    {
      if (IsTopLayer)
        return;
      parent.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
      parent.WorldOffsetDraw(spritebatch, drawWIthThis, 1f);
    }
  }
}
