// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.ZooBuildingTopRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Tile_Data;
using TinyZoo.Z_DayNight;

namespace TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors
{
  internal class ZooBuildingTopRenderer : AnimatedGameObject
  {
    internal static int BUID = 0;
    public int YLocation;
    public int UID;
    public Texture2D DrawWIthThis;
    public BUILDINGTYPE buildingtype;
    private GameObject Light;
    public TileRenderer RefParent;
    private static Vector2 RescaleVScale = Vector2.One;
    public RenderComponent QuickUseComponent;
    public List<RenderComponent> PostDrawComponents;
    private static Vector2 VSCALE = Vector2.One;
    private static Vector2 ThreadLoc;
    private static Vector2 ThreadScale;

    public ZooBuildingTopRenderer(
      TileInfo tileinf,
      int LocationX,
      int LocationY,
      int RotationClockWise,
      TileRenderer _parent)
    {
      this.Reconstruct(tileinf, LocationX, LocationY, RotationClockWise, _parent);
    }

    public void Reconstruct(
      TileInfo tileinf,
      int LocationX,
      int LocationY,
      int RotationClockWise,
      TileRenderer _parent)
    {
      this.buildingtype = tileinf.buildingtype;
      this.UID = ZooBuildingTopRenderer.BUID;
      ++ZooBuildingTopRenderer.BUID;
      this.YLocation = LocationY;
      this.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(LocationX, LocationY));
      this.DrawRect = tileinf.GetBuildLayer(RotationClockWise).BuildingLayer_Rect;
      this.DrawOrigin = tileinf.GetBuildLayer(RotationClockWise).BuildLayer_DrawOrigin;
      this.DrawWIthThis = tileinf.DrawTexture.texture;
      this.IsAnimating = false;
      if (tileinf.BuildingLayerTotalAnimationFrames > 0)
      {
        this.SetUpSimpleAnimation(tileinf.BuildingLayerTotalAnimationFrames, tileinf.BuildingLayerAnimationFrameRate);
        this.ForceToFrame(TinyZoo.Game1.Rnd.Next(0, tileinf.BuildingLayerTotalAnimationFrames));
      }
      if (tileinf.HasLightsLayer)
      {
        this.Light = new GameObject();
        this.Light.DrawRect = tileinf.GetBuildLayer(RotationClockWise).LightsLayerLayer_Rect;
        this.Light.DrawOrigin = tileinf.GetBuildLayer(RotationClockWise).LightsLayer_DrawOrigin;
        this.Light.vLocation = this.vLocation;
        this.Light.scale = 0.25f;
      }
      if (tileinf.IsFlipped(RotationClockWise))
      {
        this.FlipRender = true;
        if (this.Light != null)
          this.Light.FlipRender = true;
      }
      if (_parent.spawnblocker != null)
        _parent.spawnblocker.TryAddTopSpawner(this, _parent);
      this.SetUpCoponentsAFterLinkingToTile(_parent);
    }

    public void SetUpCoponentsAFterLinkingToTile(TileRenderer _parent)
    {
      if (_parent.rendercomponent == null)
        return;
      for (int index = 0; index < _parent.rendercomponent.Count; ++index)
        _parent.rendercomponent[index].SetUpAfterCreatingTopLayer(this, _parent);
    }

    internal static int SortThese(ZooBuildingTopRenderer a, ZooBuildingTopRenderer b)
    {
      if ((double) a.vLocation.Y < (double) b.vLocation.Y)
        return -1;
      if ((double) a.vLocation.Y > (double) b.vLocation.Y)
        return 1;
      if (a.UID > b.UID)
        return -1;
      return a.UID < b.UID ? 1 : 0;
    }

    public void UpdateZooBuildingTopRenderer(float DeltaTime)
    {
      if (!this.IsAnimating)
        return;
      this.UpdateAnimation(DeltaTime);
    }

    public void TryToAddPostDrawComponent(RenderComponent ThisComponent)
    {
      if (this.PostDrawComponents == null)
        this.PostDrawComponents = new List<RenderComponent>();
      if (this.PostDrawComponents.Contains(ThisComponent))
        throw new Exception("DOuble add post draw component");
      this.PostDrawComponents.Add(ThisComponent);
    }

    public void DrawZooBuildingTopRenderer(ref Vector2 ThreadLoc, ref Vector2 ThreadScale)
    {
      if (this.RefParent != null && this.RefParent.spawnblocker != null)
        return;
      this.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
      if ((double) this.scale != 1.0)
      {
        if (this.QuickUseComponent != null)
        {
          this.QuickUseComponent.DrawRenderComponent(this.RefParent, this.DrawWIthThis, AssetContainer.pointspritebatch01, 1f, ref ThreadLoc, ref ThreadScale, true);
        }
        else
        {
          ZooBuildingTopRenderer.RescaleVScale.X = this.scale;
          ZooBuildingTopRenderer.RescaleVScale.Y = this.scale;
          this.QuickWorldOffsetDraw(AssetContainer.pointspritebatch01, this.DrawWIthThis, ref this.vLocation, ref ZooBuildingTopRenderer.RescaleVScale, this.Rotation, this.DrawRect, this.fAlpha, this.GetColour(), this.scale, true, ref ThreadLoc, ref ThreadScale);
        }
        if (this.PostDrawComponents == null)
          return;
        for (int index = 0; index < this.PostDrawComponents.Count; ++index)
          this.PostDrawComponents[index].DrawRenderComponent(this.RefParent, this.DrawWIthThis, AssetContainer.pointspritebatch01, 1f, ref ThreadLoc, ref ThreadScale, true);
      }
      else
      {
        this.QuickWorldOffsetDraw(AssetContainer.pointspritebatch01, this.DrawWIthThis, ref this.vLocation, ref ZooBuildingTopRenderer.VSCALE, this.Rotation, this.DrawRect, this.fAlpha, this.GetColour(), this.scale, true, ref ThreadLoc, ref ThreadScale);
        if (this.PostDrawComponents != null)
        {
          for (int index = 0; index < this.PostDrawComponents.Count; ++index)
            this.PostDrawComponents[index].DrawRenderComponent(this.RefParent, this.DrawWIthThis, AssetContainer.pointspritebatch01, 1f, ref ThreadLoc, ref ThreadScale, true);
        }
        if (this.Light == null || (double) DayNightManager.NightLerpValue == 0.0 || !Z_GameFlags.HasStartedFirstDay)
          return;
        this.Light.fAlpha = DayNightManager.NightLerpValue * 1f;
        this.Light.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.EnvironmentSheet);
      }
    }

    public void DrawZooBuildingTopRendererWithoutNight(SpriteBatch spritebatch)
    {
      this.QuickWorldOffsetDraw(spritebatch, this.DrawWIthThis, ref this.vLocation, ref ZooBuildingTopRenderer.VSCALE, this.Rotation, this.DrawRect, this.fAlpha, this.GetColour(), this.scale, false, ref ZooBuildingTopRenderer.ThreadLoc, ref ZooBuildingTopRenderer.ThreadScale);
      if (this.Light == null || (double) DayNightManager.NightLerpValue == 0.0 || !Z_GameFlags.HasStartedFirstDay)
        return;
      this.Light.fAlpha = DayNightManager.NightLerpValue * 1f;
      this.Light.WorldOffsetDraw(spritebatch, AssetContainer.EnvironmentSheet);
    }
  }
}
