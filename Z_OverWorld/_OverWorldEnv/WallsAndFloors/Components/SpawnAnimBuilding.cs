// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.SpawnAnimBuilding
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Tile_Data;
using TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.Sub;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components
{
  internal class SpawnAnimBuilding : RenderComponent
  {
    private List<Scaffold> scaffold;
    private List<TopBuilider> topbuilders;

    public SpawnAnimBuilding(TileRenderer parent)
      : base(parent, RenderComponentType.SpawnAnimBuilding)
    {
      TileInfo tileInfo = TileData.GetTileInfo(parent.tiletypeonconstruct);
      this.scaffold = new List<Scaffold>();
      float xtileOrigin = (float) tileInfo.GetXTileOrigin(parent.RotationOnConstruct);
      tileInfo.GetYTileOrigin(parent.RotationOnConstruct);
      int tileHeight = tileInfo.GetTileHeight(parent.RotationOnConstruct);
      int tileWidth = tileInfo.GetTileWidth(parent.RotationOnConstruct);
      this.topbuilders = new List<TopBuilider>();
      for (int index1 = 0; index1 < tileHeight; ++index1)
      {
        for (int index2 = 0; index2 < tileWidth; ++index2)
        {
          Scaffold scaffold = new Scaffold(parent.RefTopRenderer != null);
          scaffold.vLocation = parent.vLocation;
          scaffold.vLocation.X += (float) (index2 * 16);
          scaffold.vLocation.X -= xtileOrigin * 16f;
          scaffold.vLocation.Y -= (float) ((tileHeight - 1 - index1) * 16) * Sengine.ScreenRatioUpwardsMultiplier.Y;
          scaffold.IsBottomRow = index1 == tileHeight - 1;
          this.scaffold.Add(scaffold);
        }
      }
      this.AddTopRenderer(parent, tileInfo, tileWidth);
    }

    public void AddTopRenderer(TileRenderer parent, TileInfo info, int FootprintWidth)
    {
      if (parent.RefTopRenderer == null)
        return;
      this.topbuilders = new List<TopBuilider>();
      for (int ArrayIndex = 0; ArrayIndex < info.GetTileWidth(parent.RotationOnConstruct); ++ArrayIndex)
        this.topbuilders.Add(new TopBuilider(parent.RefTopRenderer, ArrayIndex, FootprintWidth));
      parent.RefTopRenderer.scale = 0.0f;
      parent.RefTopRenderer.QuickUseComponent = (RenderComponent) this;
      parent.RefTopRenderer.RefParent = parent;
    }

    public override bool UpdateRenderComponent(TileRenderer parent, float DeltaTime)
    {
      if (TrailerDemoFlags.HasTrailerFlag)
        DeltaTime *= TrailerDemoFlags.ScaffoldAnimationSpeedMult;
      if (OverWorldManager.overworldstate != OverWOrldState.QuickPickEmployee)
      {
        bool TopStillActive = false;
        if (this.topbuilders != null)
        {
          for (int index = 0; index < this.topbuilders.Count; ++index)
            TopStillActive |= this.topbuilders[index].UpdateTopBuilider(DeltaTime);
        }
        bool flag = false;
        for (int index = 0; index < this.scaffold.Count; ++index)
          flag |= this.scaffold[index].UpdateScaffold(DeltaTime, TopStillActive);
        ZooBuildingTopRenderer refTopRenderer = parent.RefTopRenderer;
        if (!flag)
        {
          if (parent.RefTopRenderer != null)
          {
            parent.RefTopRenderer.scale = 1f;
            parent.RefTopRenderer.QuickUseComponent = (RenderComponent) null;
          }
          return true;
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
      bool flag = false;
      for (int index1 = 0; index1 < this.scaffold.Count; ++index1)
      {
        if (this.scaffold[index1].IsBottomRow && !flag)
        {
          flag = true;
          base.DrawRenderComponent(parent, drawWIthThis, spritebatch, ALphaMod, ref ThreadLoc, ref ThreadScale, IsTopLayer);
          if (this.topbuilders != null)
          {
            for (int index2 = 0; index2 < this.topbuilders.Count; ++index2)
              this.topbuilders[index2].DrawTopBuilider();
          }
          if (parent.RefTopRenderer != null)
            parent.RefTopRenderer.scale = 0.0f;
        }
        this.scaffold[index1].DrawScaffold(spritebatch);
      }
    }
  }
}
