// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_PenInfo.MainBar.SimpleBuildingRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_PenInfo.MainBar
{
  internal class SimpleBuildingRenderer : GameObject
  {
    public GameObject TopLayer;
    private Texture2D drawtext;

    public SimpleBuildingRenderer(TILETYPE tiletype, int ROtation = 0)
    {
      if (tiletype == TILETYPE.Logo || tiletype == TILETYPE.ZooEntrance_Modern || (tiletype == TILETYPE.ZooEntrance_Deer || tiletype == TILETYPE.ZooEntrance_Cliff))
      {
        switch (tiletype)
        {
          case TILETYPE.Logo:
            this.DrawRect = new Rectangle(1059, 1160, 50, 28);
            this.drawtext = AssetContainer.EnvironmentSheet;
            break;
          case TILETYPE.ZooEntrance_Deer:
            this.DrawRect = new Rectangle(1056, 1124, 50, 35);
            this.drawtext = AssetContainer.EnvironmentSheet;
            break;
          case TILETYPE.ZooEntrance_Cliff:
            this.DrawRect = new Rectangle(1113, 1119, 46, 35);
            this.drawtext = AssetContainer.EnvironmentSheet;
            break;
          case TILETYPE.ZooEntrance_Modern:
            this.DrawRect = new Rectangle(1110, 1155, 46, 28);
            this.drawtext = AssetContainer.EnvironmentSheet;
            break;
        }
      }
      else if (TileData.IsThisACellBlock(tiletype))
      {
        this.DrawRect = TileStats.GetBuildingIconRectangle(tiletype);
        this.drawtext = AssetContainer.SpriteSheet;
      }
      else
      {
        TileInfo tileInfo = TileData.GetTileInfo(tiletype);
        this.DrawRect = tileInfo.GetRect(ROtation, ref this.Rotation);
        if (tileInfo.OverrideDrawOrigin)
          this.DrawOrigin = tileInfo.GetDrawOrigin(ROtation);
        if (tileInfo.HasBuildingLayer)
        {
          this.TopLayer = new GameObject();
          SplitBuildingInfo buildLayer = tileInfo.GetBuildLayer(ROtation);
          this.TopLayer.DrawRect = buildLayer.BuildingLayer_Rect;
          this.TopLayer.DrawOrigin = buildLayer.BuildLayer_DrawOrigin;
        }
        this.drawtext = tileInfo.DrawTexture.texture;
      }
    }

    public void SetSize(float Size, float MaxScale)
    {
      float val1_1 = (float) this.DrawRect.Width;
      float val1_2 = (float) this.DrawRect.Height;
      if (this.TopLayer != null)
      {
        val1_1 = Math.Max(val1_1, (float) this.TopLayer.DrawRect.Width);
        val1_2 = Math.Max(val1_2, (float) this.TopLayer.DrawRect.Height);
      }
      this.scale = Math.Max(val1_1, val1_2 * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.scale = Size / this.scale;
      if ((double) this.scale > (double) MaxScale)
        this.scale = MaxScale;
      if (this.TopLayer != null)
      {
        this.TopLayer.scale = this.scale;
        double num1 = (double) Math.Max(this.DrawOrigin.Y, this.TopLayer.DrawOrigin.Y);
        float num2 = (float) num1 - (float) ((num1 + (double) Math.Max((float) this.DrawRect.Height - this.DrawOrigin.Y, (float) this.TopLayer.DrawRect.Height - this.TopLayer.DrawOrigin.Y)) * 0.5);
        this.DrawOrigin.Y -= num2;
        this.TopLayer.DrawOrigin.Y -= num2;
        double num3 = (double) Math.Max(this.DrawOrigin.X, this.TopLayer.DrawOrigin.X);
        float num4 = (float) num3 - (float) ((num3 + (double) Math.Max((float) this.DrawRect.Width - this.DrawOrigin.X, (float) this.TopLayer.DrawRect.Width - this.TopLayer.DrawOrigin.X)) * 0.5);
        this.DrawOrigin.X -= num4;
        this.TopLayer.DrawOrigin.X -= num4;
      }
      else
        this.SetDrawOriginToCentre();
    }

    public void UpdateSimpleBuildingRenderer()
    {
    }

    public void DrawSimpleBuildingRenderer(
      Vector2 Offset,
      SpriteBatch spritebatch,
      float ScaleMultipier = 1f,
      float alphaMult = 1f)
    {
      this.Draw(spritebatch, this.drawtext, Offset, this.scale * ScaleMultipier, this.fAlpha * alphaMult);
      if (this.TopLayer == null)
        return;
      this.TopLayer.vLocation = this.vLocation;
      this.TopLayer.Draw(spritebatch, this.drawtext, Offset + this.vLocation, this.TopLayer.scale * ScaleMultipier, this.TopLayer.fAlpha * alphaMult);
    }
  }
}
