// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tile_Data.TileInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Utils;

namespace TinyZoo.Tile_Data
{
  internal class TileInfo
  {
    private Rectangle BaseRect;
    public bool HasFullRotation;
    public int VariantCount;
    private TileInfo[] tileinfobyrotation;
    public TextureHolder DrawTexture;
    public BUILDINGTYPE buildingtype;
    public bool IsRepeatingLargeTexture;
    public int PrivacyBlock;
    public int SoundVolume;
    public bool HasBuildingLayer;
    public DoorInfo doorinfo;
    public bool HasLightsLayer;
    public int BuildingLayerTotalAnimationFrames;
    public float BuildingLayerAnimationFrameRate;
    public bool CustomerCanLeaveNavToReachPurchasePoint;
    private Rectangle[] BaseRectsForVariants;
    public bool XFlipped;
    public bool OverrideDrawOrigin;
    public Vector2 baseDrawOrigin;
    private int FootPrintWidth = -1;
    private int FootPrintHeight = -1;
    private int FootPrintOriginX = -1;
    private int FootPrintOriginY = -1;
    private SplitBuildingInfo[] splitbuildinginfo;
    private List<Vector2Int> FootprintHoles = new List<Vector2Int>();
    public List<Vector2Int> ShopPurchasePoints;
    public CATEGORYTYPE categorytype;
    public Vector2Int ShopKeeperLocation;
    public Vector2Int TradesmansEntrance;
    public Vector2 ShopKeeperLocationMicroOffset;
    public bool HasNoCollision;
    public bool HasFence;
    public List<int> FanceBlocks;
    private List<Vector3Int> ShopEntryPoints;
    public int BaseFrame = -1;
    public float BaseFrameRate;

    public TileInfo(
      Rectangle _BaseRect,
      BUILDINGTYPE _buildingtype,
      bool _HasFullRotation,
      int _Variants = 1,
      int LockToThisRotation = -1,
      TextureHolder _DrawTexture = null,
      bool _IsRepeatingLargeTexture = false,
      int TotalRotations = -1)
    {
      this.IsRepeatingLargeTexture = _IsRepeatingLargeTexture;
      this.DrawTexture = _DrawTexture == null ? AssetContainer.SpriteSheetSheetHolder : _DrawTexture;
      this.buildingtype = _buildingtype;
      this.VariantCount = _Variants;
      this.BaseRect = _BaseRect;
      this.HasFullRotation = _HasFullRotation;
      if (LockToThisRotation > -1)
      {
        this.tileinfobyrotation = TotalRotations <= -1 ? new TileInfo[4] : new TileInfo[TotalRotations];
        this.tileinfobyrotation[LockToThisRotation] = this;
      }
      if (this.VariantCount <= 1)
        return;
      this.BaseRectsForVariants = new Rectangle[this.VariantCount];
    }

    public bool HasWall(int Rotation) => this.tileinfobyrotation != null ? this.tileinfobyrotation[Rotation].FanceBlocks != null && this.FanceBlocks.Count > 0 : this.FanceBlocks != null && this.FanceBlocks.Count > 0;

    public void BlockWall(int LocX, int LocY, int Rotation)
    {
      if (this.tileinfobyrotation != null)
        Z_GameFlags.pathfinder.CreateWall(LocX, LocY, false, this.tileinfobyrotation[Rotation].FanceBlocks.Contains(0), this.tileinfobyrotation[Rotation].FanceBlocks.Contains(1), this.tileinfobyrotation[Rotation].FanceBlocks.Contains(2), this.tileinfobyrotation[Rotation].FanceBlocks.Contains(3));
      else
        Z_GameFlags.pathfinder.CreateWall(LocX, LocY, false, this.FanceBlocks.Contains(0), this.FanceBlocks.Contains(1), this.FanceBlocks.Contains(2), this.FanceBlocks.Contains(3));
    }

    public void SetFence(int Rotation, int BlocksThis, bool IsRecursive = false)
    {
      if (this.tileinfobyrotation != null && !IsRecursive)
      {
        this.tileinfobyrotation[Rotation].SetFence(Rotation, BlocksThis, true);
      }
      else
      {
        if (this.FanceBlocks == null)
          this.FanceBlocks = new List<int>();
        this.FanceBlocks.Add(BlocksThis);
      }
    }

    public void AddShopKeeperLocation(
      Vector2Int ShopKeeperStands,
      Vector2 MicroOffset,
      Vector2Int TradesmansEntrance,
      int Rotation)
    {
      this.tileinfobyrotation[Rotation].ShopKeeperLocation = ShopKeeperStands;
      this.tileinfobyrotation[Rotation].ShopKeeperLocationMicroOffset = MicroOffset;
      if (TradesmansEntrance == null)
        return;
      this.tileinfobyrotation[Rotation].TradesmansEntrance = new Vector2Int(TradesmansEntrance);
    }

    public void SetCustomerCanLeaveNavToReachPurchasePoint()
    {
      this.CustomerCanLeaveNavToReachPurchasePoint = true;
      if (this.tileinfobyrotation == null)
        return;
      for (int index = 0; index < this.tileinfobyrotation.Length; ++index)
        this.tileinfobyrotation[index].CustomerCanLeaveNavToReachPurchasePoint = true;
    }

    public int GetMaximumRotations() => this.tileinfobyrotation == null ? 0 : this.tileinfobyrotation.Length;

    public List<Vector2Int> GetFootPrintHoles(int _Rotation) => _Rotation > -1 && this.tileinfobyrotation != null && this.tileinfobyrotation.Length >= _Rotation ? this.tileinfobyrotation[_Rotation].GetFootPrintHoles(-1) : this.FootprintHoles;

    public void SetBuildingLayerAnimation(
      int _BuildingLayerTotalAnimationFrames,
      float _BuildingLayerAnimationFrameRate)
    {
      this.BuildingLayerTotalAnimationFrames = _BuildingLayerTotalAnimationFrames;
      this.BuildingLayerAnimationFrameRate = _BuildingLayerAnimationFrameRate;
    }

    public SplitBuildingInfo GetBuildLayer(int RotationClockWise)
    {
      if (!this.HasBuildingLayer)
        return (SplitBuildingInfo) null;
      return this.splitbuildinginfo[RotationClockWise] != null ? this.splitbuildinginfo[RotationClockWise] : this.splitbuildinginfo[0];
    }

    public void SetOverrideDrawOrigin(Vector2 _baseDrawOrigin, int RotationClockWise)
    {
      this.OverrideDrawOrigin = true;
      this.tileinfobyrotation[RotationClockWise].OverrideDrawOrigin = true;
      this.tileinfobyrotation[RotationClockWise].baseDrawOrigin = _baseDrawOrigin;
    }

    public Vector2Int GetIntOrigin(int RotationClockWise) => new Vector2Int()
    {
      X = this.GetXTileOrigin(RotationClockWise),
      Y = this.GetYTileOrigin(RotationClockWise)
    };

    public Vector2 GetDrawOrigin(int RotationClockWise) => this.tileinfobyrotation[RotationClockWise].baseDrawOrigin;

    public void SetCustomFootPrint(
      int _Width,
      int _Height,
      int _Xorigin,
      int Yorigin,
      int RotationClockWise = 0)
    {
      this.tileinfobyrotation[RotationClockWise].FootPrintWidth = _Width;
      this.tileinfobyrotation[RotationClockWise].FootPrintWidth = _Width;
      this.tileinfobyrotation[RotationClockWise].FootPrintHeight = _Height;
      this.tileinfobyrotation[RotationClockWise].FootPrintOriginX = _Xorigin;
      this.tileinfobyrotation[RotationClockWise].FootPrintOriginY = Yorigin;
      this.SetOverrideDrawOrigin(new Vector2((float) (_Xorigin * 16 + 8), (float) (Yorigin * 16 + 8)), RotationClockWise);
    }

    public void CutHoleInFootPrint(Vector2Int HoleLocation, int RotationClockWise = 0) => this.tileinfobyrotation[RotationClockWise].FootprintHoles.Add(HoleLocation);

    public void AddShopEntry(Vector2Int entry, int Rotation)
    {
      if (this.ShopEntryPoints == null)
        this.ShopEntryPoints = new List<Vector3Int>();
      this.ShopEntryPoints.Add(new Vector3Int(entry.X, entry.Y, Rotation));
    }

    public void AddShopPurchasePoint(Vector2Int entry, int Rotation)
    {
      if (Rotation == -1)
      {
        if (this.ShopPurchasePoints == null)
          this.ShopPurchasePoints = new List<Vector2Int>();
        this.ShopPurchasePoints.Add(new Vector2Int(entry.X, entry.Y));
      }
      else
        this.tileinfobyrotation[Rotation].AddShopPurchasePoint(entry, -1);
    }

    public bool HasEntrance() => this.ShopEntryPoints != null && this.tileinfobyrotation != null;

    public Vector2Int GetPurchasingLocation(int RotationClockWise)
    {
      if (this.tileinfobyrotation[RotationClockWise].ShopPurchasePoints != null && this.tileinfobyrotation[RotationClockWise].ShopPurchasePoints.Count > 0)
        return this.tileinfobyrotation[RotationClockWise].ShopPurchasePoints[0];
      return this.tileinfobyrotation[RotationClockWise] == null ? (Vector2Int) null : this.GetEntrances(RotationClockWise)[0];
    }

    public Vector2Int GetTradesmansEntrance(int RotationClockWise) => this.tileinfobyrotation[RotationClockWise].TradesmansEntrance != null ? this.tileinfobyrotation[RotationClockWise].TradesmansEntrance : (Vector2Int) null;

    public Vector2Int GetShopKeeperLocation(
      int RotationClockWise,
      ref Vector2 TargetOffsetForJob)
    {
      TargetOffsetForJob = this.tileinfobyrotation[RotationClockWise].ShopKeeperLocationMicroOffset;
      return this.tileinfobyrotation[RotationClockWise].ShopKeeperLocation;
    }

    public List<Vector2Int> GetEntrances(int RotationClockWise)
    {
      if (this.ShopEntryPoints == null)
        return (List<Vector2Int>) null;
      List<Vector2Int> vector2IntList = new List<Vector2Int>();
      for (int index = 0; index < this.ShopEntryPoints.Count; ++index)
      {
        if (RotationClockWise == this.ShopEntryPoints[index].Z)
          vector2IntList.Add(new Vector2Int(this.ShopEntryPoints[index].X, this.ShopEntryPoints[index].Y));
      }
      return vector2IntList;
    }

    public void SetPrivacy(int _PrivacyRating) => this.PrivacyBlock = _PrivacyRating;

    public void SetVolume(int _SoundVolume) => this.SoundVolume = _SoundVolume;

    public void AddBuilding(
      Rectangle _Buildrect,
      Vector2 _BuildDrawOrigin,
      int ThisRotationClockWise = 0)
    {
      this.AddBuilding(_Buildrect, _BuildDrawOrigin, new Rectangle(), Vector2.Zero, ThisRotationClockWise);
    }

    public void AddBuilding(
      Rectangle _Buildrect,
      Vector2 _BuildDrawOrigin,
      Rectangle CloseDoorRect,
      Vector2 ClosedDoorOrigin,
      int ThisRotationClockWise = 0)
    {
      if (this.splitbuildinginfo == null)
        this.splitbuildinginfo = new SplitBuildingInfo[4];
      this.splitbuildinginfo[ThisRotationClockWise] = new SplitBuildingInfo(_Buildrect, _BuildDrawOrigin, CloseDoorRect, ClosedDoorOrigin);
      this.HasBuildingLayer = true;
    }

    public void AddBuildingLights(
      Rectangle _Buildrect,
      Vector2 _BuildDrawOrigin,
      int ThisRotationClockWise = 0)
    {
      this.splitbuildinginfo[ThisRotationClockWise].LightsLayerLayer_Rect = _Buildrect;
      this.splitbuildinginfo[ThisRotationClockWise].LightsLayer_DrawOrigin = _BuildDrawOrigin;
      this.HasLightsLayer = true;
    }

    public int GetXTileOrigin(int Rotation)
    {
      if (Rotation > -1 && this.tileinfobyrotation != null && this.tileinfobyrotation.Length > Rotation)
        return this.tileinfobyrotation[Rotation].GetXTileOrigin(-1);
      return this.FootPrintOriginX > -1 ? this.FootPrintOriginX : this.BaseRect.Width / 16 / 2;
    }

    public int GetYTileOrigin(int Rotation)
    {
      if (Rotation > -1 && this.tileinfobyrotation != null && this.tileinfobyrotation.Length > Rotation)
        return this.tileinfobyrotation[Rotation].GetYTileOrigin(-1);
      if (this.FootPrintOriginY > -1)
        return this.FootPrintOriginY;
      return this.BaseRect.Height == 64 ? 2 : this.BaseRect.Height / 16 / 2;
    }

    public Rectangle GetAnyRectangle() => this.BaseRect;

    public int GetTileWidth(int Rotation)
    {
      if (Rotation > -1 && this.tileinfobyrotation != null && this.tileinfobyrotation.Length > Rotation)
        return this.tileinfobyrotation[Rotation].GetTileWidth(-1);
      return this.FootPrintWidth > -1 ? this.FootPrintWidth : (int) ((double) this.BaseRect.Width / (double) TileMath.TileSize);
    }

    public int GetTileHeight(int Rotation)
    {
      if (Rotation > -1 && this.tileinfobyrotation != null && this.tileinfobyrotation.Length > Rotation)
        return this.tileinfobyrotation[Rotation].GetTileHeight(-1);
      return this.FootPrintHeight > -1 ? this.FootPrintHeight : (int) ((double) this.BaseRect.Height / (double) TileMath.TileSize);
    }

    public bool IsFlipped(int ThisRotation) => this.tileinfobyrotation != null && ThisRotation < this.tileinfobyrotation.Length ? this.tileinfobyrotation[ThisRotation].XFlipped : this.XFlipped;

    public Rectangle GetRect(int ThisRotation, ref float Rotation, int VariantIndex = -1)
    {
      if (this.VariantCount > 1)
      {
        if (VariantIndex == -1)
          VariantIndex = TinyZoo.Game1.Rnd.Next(0, this.VariantCount);
      }
      else
        VariantIndex = 0;
      Rectangle rectangle = new Rectangle(this.BaseRect.X, this.BaseRect.Y, this.BaseRect.Width, this.BaseRect.Height);
      if (this.BaseRectsForVariants != null && this.BaseRectsForVariants[VariantIndex].Width > 0)
        rectangle = this.BaseRectsForVariants[VariantIndex];
      else
        rectangle.X += VariantIndex * (rectangle.Width + 1);
      if (this.tileinfobyrotation != null && this.tileinfobyrotation[ThisRotation] != null)
      {
        rectangle = new Rectangle(this.tileinfobyrotation[ThisRotation].BaseRect.X, this.tileinfobyrotation[ThisRotation].BaseRect.Y, this.tileinfobyrotation[ThisRotation].BaseRect.Width, this.tileinfobyrotation[ThisRotation].BaseRect.Height);
        rectangle.X += VariantIndex * (rectangle.Width + 1);
        return rectangle;
      }
      if (this.HasFullRotation)
        Rotation = 1.570796f * (float) ThisRotation;
      return rectangle;
    }

    public void SetWillFlipSprite(int Rotation) => this.tileinfobyrotation[Rotation].XFlipped = true;

    public void AddRotationVariant(Rectangle _BaseRect, int _Variants = 1, int LockToThisRotation = -1)
    {
      TileInfo tileInfo = new TileInfo(_BaseRect, this.buildingtype, false, _Variants);
      this.tileinfobyrotation[LockToThisRotation] = tileInfo;
    }

    public void SetUpBaseAnimation(int _BaseFrames, float _BaseFrameRate)
    {
      this.BaseFrame = _BaseFrames;
      this.BaseFrameRate = _BaseFrameRate;
    }

    public void AddVariant(Rectangle _BaseRect, int ThisVariant) => this.BaseRectsForVariants[ThisVariant] = _BaseRect;

    public bool IsSomethingOverlappingWaterPump(int Rotation, int Xloc, int YLoc)
    {
      int tileWidth = this.GetTileWidth(Rotation);
      int tileHeight = this.GetTileHeight(Rotation);
      Xloc -= this.GetXTileOrigin(Rotation);
      YLoc -= this.GetYTileOrigin(Rotation);
      for (int index1 = 0; index1 < tileWidth; ++index1)
      {
        for (int index2 = 0; index2 < tileHeight; ++index2)
        {
          if (OverWorldManager.heatmapmanager.GetHasWaterAccess(index1 + Xloc, index2 + YLoc))
            return true;
        }
      }
      return false;
    }
  }
}
