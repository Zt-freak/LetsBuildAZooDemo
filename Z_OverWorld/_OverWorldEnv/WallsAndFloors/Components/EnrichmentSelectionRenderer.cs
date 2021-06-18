// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.EnrichmentSelectionRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components
{
  internal class EnrichmentSelectionRenderer : RenderComponent
  {
    public PenItem Ref_PenItem;
    private ZooBuildingTopRenderer toprenderer;

    public EnrichmentSelectionRenderer(TileRenderer parent, TileInfo tinfo = null)
      : base(parent, RenderComponentType.EnrichmentSelectionRenderer)
    {
      TileInfo tileInfo = TileData.GetTileInfo(parent.tiletypeonconstruct);
      if (!tileInfo.HasBuildingLayer)
        return;
      this.toprenderer = new ZooBuildingTopRenderer(tileInfo, parent.TileLocation.X, parent.TileLocation.Y, parent.RotationOnConstruct, parent);
    }

    public override bool UpdateRenderComponent(TileRenderer parent, float DeltaTime) => false;

    public override void DrawRenderComponent(
      TileRenderer parent,
      Texture2D drawWIthThis,
      SpriteBatch spritebatch,
      float ALphaMod,
      ref Vector2 ThreadLoc,
      ref Vector2 ThreadScale,
      bool IsTopLayer)
    {
      base.DrawRenderComponent(parent, drawWIthThis, spritebatch, ALphaMod, ref ThreadLoc, ref ThreadScale, IsTopLayer);
      if (Z_GameFlags.SelectedPenItem == null || !Z_GameFlags.SelectedPenItem.IsSelectedInEditView || !Z_GameFlags.SelectedPenItem.Location.CompareMatches(parent.TileLocation))
        return;
      Z_GameFlags.SelectedPenItem.IsSelectedInEditView = false;
      this.toprenderer.vLocation = parent.vLocation;
      this.toprenderer.DrawZooBuildingTopRendererWithoutNight(AssetContainer.PointBlendBatch02);
    }
  }
}
