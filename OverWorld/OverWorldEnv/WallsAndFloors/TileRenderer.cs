// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.TileRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_DayNight;
using TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components;
using TinyZoo.Z_Trailer;

namespace TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors
{
  internal class TileRenderer : GameObject
  {
    private bool UsesRotation;
    public TileInfo Refinfo;
    public ZooBuildingTopRenderer RefTopRenderer;
    public LayoutEntry Ref_layoutentry;
    public int XWidth = 1;
    public int YHeight = 1;
    public int XOrigin;
    public int YOrigin;
    public Vector2Int TileLocation;
    private bool IsAChild;
    public Texture2D drawWIthThis;
    public bool Is17PixFloor;
    public TILETYPE tiletypeonconstruct;
    public int RotationOnConstruct;
    public List<RenderComponent> rendercomponent;
    public bool HasDrawn;
    private bool WillTint;
    public SpawnBlockComponent spawnblocker;
    private static Vector2 VSCALE = Vector2.One;

    public TileRenderer(
      LayoutEntry layourentry,
      int XL,
      int YL,
      bool _IsAChild,
      bool IsConstructionPreview = false,
      bool IsFloor_Trailer = false,
      bool IsUnderfloor = false)
    {
      this.Reconstruct(layourentry, XL, YL, _IsAChild, IsConstructionPreview, IsFloor_Trailer, IsUnderfloor);
    }

    public bool IsStillBuilding()
    {
      for (int index = 0; index < this.rendercomponent.Count; ++index)
      {
        if (this.rendercomponent[index].componenttype == RenderComponentType.SpawnAnimator || this.rendercomponent[index].componenttype == RenderComponentType.SpawnAnimBuilding || this.rendercomponent[index].componenttype == RenderComponentType.SpawnAnimNature)
          return true;
      }
      return false;
    }

    public void Reconstruct(
      LayoutEntry layourentry,
      int XL,
      int YL,
      bool _IsAChild,
      bool IsConstructionPreview = false,
      bool IsFloor_Trailer = false,
      bool IsUnderfloor = false)
    {
      int tiletype = (int) layourentry.tiletype;
      this.WillTint = TileData.WillTintAtNight(layourentry.tiletype);
      int num1 = _IsAChild ? 1 : 0;
      this.bActive = true;
      this.RotationOnConstruct = layourentry.RotationClockWise;
      this.tiletypeonconstruct = layourentry.tiletype;
      this.IsAChild = _IsAChild;
      this.Ref_layoutentry = layourentry;
      if (layourentry != null)
      {
        this.Ref_layoutentry = new LayoutEntry(layourentry.tiletype);
        this.Ref_layoutentry.RotationClockWise = layourentry.RotationClockWise;
        if (layourentry.isChild())
          this.Ref_layoutentry.SetChild(layourentry.GetParentLocation(), layourentry.tiletype);
        else
          this.Ref_layoutentry.UnsetChild();
      }
      this.TileLocation = new Vector2Int(XL, YL);
      if (!this.IsAChild)
      {
        this.Refinfo = TileData.GetTileInfo(layourentry.tiletype);
        this.Is17PixFloor = CategoryData.IsThisACroppedFloor(layourentry.tiletype);
        this.drawWIthThis = this.Refinfo.DrawTexture.texture;
        if (this.Refinfo.IsRepeatingLargeTexture)
        {
          this.DrawRect = this.Refinfo.GetRect(layourentry.RotationClockWise, ref this.Rotation);
          int num2 = this.DrawRect.Width / 16;
          int num3 = this.DrawRect.Height / 16;
          this.DrawRect.X = XL % num2;
          this.DrawRect.X *= 16;
          this.DrawRect.Y = YL % num2;
          this.DrawRect.Y *= 16;
          this.DrawRect.Width = 16;
          this.DrawRect.Height = 16;
          this.XWidth = 1;
          this.YHeight = 1;
          this.SetDrawOriginToCentre();
        }
        else
        {
          this.DrawRect = this.Refinfo.GetRect(layourentry.RotationClockWise, ref this.Rotation);
          this.UsesRotation = layourentry.RotationClockWise > 0;
          this.SetDrawOriginToCentre();
          if (this.Is17PixFloor && this.DrawRect.Height == 17)
          {
            this.XOrigin = 0;
            this.YOrigin = 0;
            this.DrawOrigin.Y = 8f;
            this.XWidth = 1;
            this.YHeight = 1;
            this.Is17PixFloor = true;
            if (!IsUnderfloor)
              this.scale = 1.01f;
            if (this.TileLocation.Y == TileMath.GetOverWorldMapSize_YSize() - 6)
              this.DrawRect.Height = 15;
          }
          else
          {
            if ((double) this.DrawOrigin.X % 16.0 != 8.0)
            {
              this.DrawOrigin.X += 8f;
              this.XOrigin = (int) ((double) this.DrawOrigin.X / 16.0);
            }
            if ((double) this.DrawOrigin.Y % 16.0 != 8.0)
            {
              this.DrawOrigin.Y += 8f;
              this.YOrigin = (int) ((double) this.DrawOrigin.Y / 16.0);
            }
            if (this.DrawRect.Width == 48)
              this.XOrigin = 1;
            if (this.DrawRect.Height == 48)
              this.YOrigin = 1;
            if (this.DrawRect.Width == 80)
              this.XOrigin = 2;
            this.XWidth = this.DrawRect.Width / 16;
            this.YHeight = this.DrawRect.Height / 16;
            if (!IsUnderfloor && TileData.IsThisFloorAVolume(layourentry.tiletype))
              this.scale = 1.01f;
          }
        }
        if (TileData.IsThisACellBlock(layourentry.tiletype))
          this.scale = 1.005f;
        if (this.Refinfo.IsFlipped(layourentry.RotationClockWise))
          this.FlipRender = true;
        if (this.Refinfo.OverrideDrawOrigin)
        {
          this.XOrigin = this.Refinfo.GetIntOrigin(this.RotationOnConstruct).X;
          this.YOrigin = this.Refinfo.GetIntOrigin(this.RotationOnConstruct).Y;
          this.DrawOrigin = this.Refinfo.GetDrawOrigin(this.RotationOnConstruct);
        }
        this.vLocation = new Vector2((float) (XL * 16), (float) YL * 16f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      }
      this.rendercomponent = ComponentData.GetRenderComponent(layourentry.tiletype, this, IsConstructionPreview);
      if (TileData.GetTileInfo(layourentry.tiletype).buildingtype == BUILDINGTYPE.Wall && TileData.isWallNotCorner(layourentry.tiletype))
      {
        if (layourentry.RotationClockWise == 1 || layourentry.RotationClockWise == 3)
        {
          if (XL % 2 == 1)
            this.DrawRect.X += 16;
        }
        else if (YL % 2 == 1)
          this.DrawRect.Y += 16;
      }
      if (!TrailerDemoFlags.AutoReveal || this.IsAChild || (this.tiletypeonconstruct == TILETYPE.DefaultFence_InnerCorner || this.tiletypeonconstruct == TILETYPE.DefaultFence_WallCorner || (this.tiletypeonconstruct == TILETYPE.DefaultFence_WallSide || this.tiletypeonconstruct == TILETYPE.Logo) || YL >= 225) && TrailerDemoFlags.RenderRoadAndFences)
        return;
      this.spawnblocker = new SpawnBlockComponent(XL, YL, IsFloor_Trailer, IsUnderfloor, this.tiletypeonconstruct);
      if (!IsFloor_Trailer && !IsUnderfloor)
      {
        if (!TrailerDemoFlags.HasTrailerFlag || !TrailerDemoFlags.PlayScaffoldAnimations)
          return;
        this.DoBuildingSpawnAnimation();
      }
      else
      {
        if (!TrailerDemoFlags.HasTrailerFlag || !TrailerDemoFlags.PlayScaffoldAnimations)
          return;
        this.DoNatureSpawnAnimation();
      }
    }

    public void RemoveComponentByType(RenderComponentType componenttype)
    {
      if (this.rendercomponent == null)
        return;
      for (int index = this.rendercomponent.Count - 1; index > -1; --index)
      {
        if (this.rendercomponent[index].componenttype == componenttype)
          this.rendercomponent.RemoveAt(index);
      }
    }

    public void DoPenSpawnAnim(float Delay, TILETYPE underfloor, bool IsGate)
    {
      if (this.rendercomponent == null)
        this.rendercomponent = new List<RenderComponent>();
      this.rendercomponent.Add((RenderComponent) new SpawnAnimator(this, Delay, underfloor, IsGate));
    }

    public void DoNatureSpawnAnimation()
    {
      if (this.rendercomponent == null)
        this.rendercomponent = new List<RenderComponent>();
      this.rendercomponent.Add((RenderComponent) new SpawnAnimNature(this));
    }

    public void DoBuildingSpawnAnimation()
    {
      if (this.rendercomponent == null)
        this.rendercomponent = new List<RenderComponent>();
      this.rendercomponent.Add((RenderComponent) new SpawnAnimBuilding(this));
    }

    public void SetLocation(int XL, int YL)
    {
      this.TileLocation = new Vector2Int(XL, YL);
      this.vLocation = new Vector2((float) (XL * 16), (float) YL * 16f * Sengine.ScreenRatioUpwardsMultiplier.Y);
    }

    public EnclosureGate OpenGate()
    {
      for (int index = 0; index < this.rendercomponent.Count; ++index)
      {
        if (this.rendercomponent[index].componenttype == RenderComponentType.EnclosureGate)
        {
          (this.rendercomponent[index] as EnclosureGate).OpenGateNow();
          return this.rendercomponent[index] as EnclosureGate;
        }
      }
      return (EnclosureGate) null;
    }

    public EnclosureGate GetGate()
    {
      if (this.rendercomponent == null)
        return (EnclosureGate) null;
      for (int index = 0; index < this.rendercomponent.Count; ++index)
      {
        if (this.rendercomponent[index].componenttype == RenderComponentType.EnclosureGate)
          return this.rendercomponent[index] as EnclosureGate;
      }
      return (EnclosureGate) null;
    }

    public void CreateFarmSign(Player player, CROPTYPE croptype, int PrisonUID)
    {
      FarmSign farmSign = (FarmSign) null;
      if (this.rendercomponent == null)
        this.rendercomponent = new List<RenderComponent>();
      for (int index = 0; index < this.rendercomponent.Count; ++index)
      {
        if (this.rendercomponent[index].componenttype == RenderComponentType.AnimalsOnOrderSign)
          farmSign = this.rendercomponent[index] as FarmSign;
      }
      if (farmSign == null)
      {
        farmSign = new FarmSign(this, PrisonUID, croptype);
        this.rendercomponent.Add((RenderComponent) farmSign);
        this.rendercomponent.Add((RenderComponent) new SpawnAnimBuilding(this));
      }
      farmSign.SetUpOrderStatus(player, PrisonUID);
    }

    public void CreateOrderSign(Player player, int TotalEntries, int PrisonUID)
    {
      AnimalsOnOrderSign animalsOnOrderSign = (AnimalsOnOrderSign) null;
      if (this.rendercomponent == null)
        this.rendercomponent = new List<RenderComponent>();
      for (int index = 0; index < this.rendercomponent.Count; ++index)
      {
        if (this.rendercomponent[index].componenttype == RenderComponentType.AnimalsOnOrderSign)
          animalsOnOrderSign = this.rendercomponent[index] as AnimalsOnOrderSign;
      }
      if (animalsOnOrderSign == null)
      {
        animalsOnOrderSign = new AnimalsOnOrderSign(this, PrisonUID);
        Z_GameFlags.animalsonordersigns.Add(animalsOnOrderSign);
        this.rendercomponent.Add((RenderComponent) animalsOnOrderSign);
      }
      animalsOnOrderSign.SetUpOrderStatus(player, TotalEntries, PrisonUID);
    }

    public void UpdateTileRenderer(float DeltaTime)
    {
      if (this.spawnblocker != null)
      {
        if (!this.spawnblocker.UpdateSpawnBlocker(DeltaTime))
          return;
        this.spawnblocker = (SpawnBlockComponent) null;
      }
      this.HasDrawn = false;
      if (this.rendercomponent == null)
        return;
      for (int index = this.rendercomponent.Count - 1; index > -1; --index)
      {
        if (this.rendercomponent[index].UpdateRenderComponent(this, DeltaTime))
        {
          this.rendercomponent.RemoveAt(index);
          if (this.rendercomponent.Count == 0)
            this.rendercomponent = (List<RenderComponent>) null;
        }
      }
    }

    internal static bool GetIsBlocked(Vector2Int Location, TileRenderer[,] tilesasarray) => !TileMath.TileIsInBuildablePartOfWorld(Location.X, Location.Y) || tilesasarray[Location.X, Location.Y] != null && (tilesasarray[Location.X, Location.Y].Ref_layoutentry.isChild() || tilesasarray[Location.X, Location.Y].Ref_layoutentry.tiletype != TILETYPE.None && tilesasarray[Location.X, Location.Y].Refinfo.buildingtype != BUILDINGTYPE.MoonPlant && tilesasarray[Location.X, Location.Y].Refinfo.buildingtype != BUILDINGTYPE.PrisonWall);

    public void DrawTileRenderer(
      SpriteBatch spritebatch,
      ref Vector2 ThreadLoc,
      ref Vector2 ThreadScale,
      float ALphaMod = 1f)
    {
      if (this.spawnblocker != null || this.IsAChild || this.HasDrawn)
        return;
      this.HasDrawn = true;
      if (this.rendercomponent != null)
      {
        for (int index = 0; index < this.rendercomponent.Count; ++index)
          this.rendercomponent[index].DrawRenderComponent(this, this.drawWIthThis, spritebatch, ALphaMod, ref ThreadLoc, ref ThreadScale, false);
      }
      else
      {
        if (this.WillTint)
          this.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
        this.QuickWorldOffsetDraw(spritebatch, this.drawWIthThis, ref this.vLocation, ref TileRenderer.VSCALE, this.Rotation, this.DrawRect, this.fAlpha * ALphaMod, this.GetColour(), this.scale, false, ref ThreadLoc, ref ThreadScale);
      }
    }

    public void ThreadDrawTileRenderer(
      SpriteBatch spritebatch,
      ref Vector2 Vloc,
      ref Vector2 ThreadLoc,
      ref Vector2 ThreadScale,
      float ALphaMod = 1f)
    {
      if (this.IsAChild || this.HasDrawn)
        return;
      this.HasDrawn = true;
      if (this.rendercomponent != null)
      {
        for (int index = 0; index < this.rendercomponent.Count; ++index)
          this.rendercomponent[index].DrawRenderComponent(this, this.drawWIthThis, spritebatch, ALphaMod, ref ThreadLoc, ref ThreadScale, false);
      }
      else
      {
        if (this.WillTint)
          this.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
        Vloc.X = this.vLocation.X;
        Vloc.Y = this.vLocation.Y;
        this.QuickWorldOffsetDraw(spritebatch, this.drawWIthThis, ref Vloc, ref TileRenderer.VSCALE, this.Rotation, this.DrawRect, this.fAlpha * ALphaMod, this.GetColour(), this.scale, false, ref ThreadLoc, ref ThreadScale);
      }
    }
  }
}
