// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Farms.CropSum.SeedPicker.SeedIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_Farms.CropSum.SeedPicker
{
  internal class SeedIcon : GameObject
  {
    private Texture2D drawWithThis;

    public SeedIcon(CROPTYPE seed, float BaseScale, float TargetSize = 25f, bool DrawGrownTileVersion = false)
    {
      if (DrawGrownTileVersion)
      {
        TileInfo tileInfo = TileData.GetTileInfo(CropData.GetCropToTileType(seed, PlantState.Fruited));
        int RotationClockWise = 0;
        this.drawWithThis = tileInfo.DrawTexture.texture;
        SplitBuildingInfo buildLayer = tileInfo.GetBuildLayer(RotationClockWise);
        this.DrawRect = buildLayer.BuildingLayer_Rect;
        this.DrawOrigin = buildLayer.BuildLayer_DrawOrigin;
        this.scale = BaseScale;
        Vector2 size = this.GetSize();
        float num = Math.Max(size.X, size.Y);
        this.scale = TargetSize / num;
      }
      else
      {
        this.DrawRect = CropData.GetCropTypeToSeedPacketRectangle(seed);
        this.drawWithThis = AssetContainer.AnimalSheet;
        this.scale = BaseScale;
      }
      this.SetDrawOriginToCentre();
    }

    public void Darken()
    {
      this.SetAlpha(0.7f);
      this.SetAllColours(Color.Gray.ToVector3());
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawSeedIcon(SpriteBatch spritebatch, Vector2 Offset, float ScaleMultiplier = 1f) => this.Draw(spritebatch, this.drawWithThis, Offset, this.scale * ScaleMultiplier, this.fAlpha);
  }
}
