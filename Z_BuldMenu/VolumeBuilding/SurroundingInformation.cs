// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.VolumeBuilding.SurroundingInformation
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuldMenu.VolumeBuilding
{
  internal class SurroundingInformation
  {
    private bool[,] full;
    public bool IsFarEdge_WillNotBechanged;

    public SurroundingInformation(bool _IsFarEdge_WillNotBechanged = false)
    {
      this.IsFarEdge_WillNotBechanged = _IsFarEdge_WillNotBechanged;
      this.full = new bool[3, 3];
    }

    public void UnBlockAll() => this.full = new bool[3, 3];

    public void UnBlockThis(int X, int Y) => this.full[X, Y] = false;

    public void BlockThis(int X, int Y) => this.full[X, Y] = true;

    public void SimpleSetUpFromPlayer(
      Vector2Int CenterLocation,
      TILETYPE Lookforthis,
      Player player,
      bool IsFloor = true)
    {
      this.UnBlockAll();
      for (int index1 = -1; index1 < 2; ++index1)
      {
        for (int index2 = -1; index2 < 2; ++index2)
        {
          int index3 = CenterLocation.X + index1;
          int index4 = CenterLocation.Y + index2;
          if (index3 > -1 && index4 > -1 && (index3 < player.prisonlayout.layout.FloorTileTypes.GetLength(0) && index4 < player.prisonlayout.layout.FloorTileTypes.GetLength(1)) && (player.prisonlayout.layout.FloorTileTypes[index3, index4].tiletype == Lookforthis || player.prisonlayout.layout.FloorTileTypes[index3, index4].UnderFloorTiletype == Lookforthis))
            this.BlockThis(index1 + 1, index2 + 1);
        }
      }
    }

    public void ApplyRenderingToSelf(
      WallsAndFloorsManager wallsandfloors,
      Player player,
      bool IsFloor,
      Vector2Int LocationInWorldSpace)
    {
      TileRenderContainer tileRenderContainer = new TileRenderContainer(LocationInWorldSpace.X, LocationInWorldSpace.Y);
      tileRenderContainer.SetSurrounder(this);
      tileRenderContainer.SetTile(player.prisonlayout.layout.FloorTileTypes[LocationInWorldSpace.X, LocationInWorldSpace.Y].tiletype, LocationInWorldSpace, player);
      tileRenderContainer.SetRenderTileFromSurround(wallsandfloors, player);
    }

    public void ApplyRenderingToSelfAndAllAround(
      List<VolumeRow> volumerows,
      Vector2Int Location,
      int MinY,
      WallsAndFloorsManager wallsandfloors,
      Player player,
      bool IsDeleting,
      TILETYPE tiletype,
      bool IsFloor)
    {
      for (int index1 = 0; index1 < 3; ++index1)
      {
        for (int index2 = 0; index2 < 3; ++index2)
        {
          if (index2 == 1 && index1 == 1)
            volumerows[Location.Y - MinY].SetRenderTileFromSurround(Location.X, wallsandfloors, player, tiletype, IsFloor);
          else if (this.full[index2, index1])
          {
            int num1 = 0;
            switch (index2)
            {
              case 0:
                num1 = -1;
                break;
              case 2:
                num1 = 1;
                break;
            }
            int num2 = 0;
            switch (index1)
            {
              case 0:
                num2 = -1;
                break;
              case 2:
                num2 = 1;
                break;
            }
            if (Location.Y + num2 - MinY > -1 && Location.Y + num2 - MinY < volumerows.Count)
            {
              if (!IsDeleting)
                volumerows[Location.Y + num2 - MinY].BlockSurroundingTile(Location.X + num1, num1 * -1, num2 * -1);
              volumerows[Location.Y + num2 - MinY].SetRenderTileFromSurround(Location.X + num1, wallsandfloors, player, tiletype, IsFloor);
            }
          }
        }
      }
    }

    public void ModifyEdge(bool IsBlock, int RelativeX, int RelativeY) => this.full[RelativeX + 1, RelativeY + 1] = IsBlock;

    public EdgeRotationTYPE GetEdgeType()
    {
      SurroundingInformation.RowStaus rowStatus1 = this.GetRowStatus(0);
      SurroundingInformation.RowStaus rowStatus2 = this.GetRowStatus(1);
      SurroundingInformation.RowStaus rowStatus3 = this.GetRowStatus(2);
      if (rowStatus1 == SurroundingInformation.RowStaus.None && rowStatus2 == SurroundingInformation.RowStaus.Mid)
        ;
      if (rowStatus1 == SurroundingInformation.RowStaus.All && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.All)
        return EdgeRotationTYPE.Open;
      if ((rowStatus1 == SurroundingInformation.RowStaus.None || rowStatus1 == SurroundingInformation.RowStaus.Left || (rowStatus1 == SurroundingInformation.RowStaus.Right || rowStatus1 == SurroundingInformation.RowStaus.LeftAndRight)) && (rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.All))
        return EdgeRotationTYPE.TopStraight_GapAbove;
      if ((rowStatus1 == SurroundingInformation.RowStaus.LeftMid || rowStatus1 == SurroundingInformation.RowStaus.All) && rowStatus2 == SurroundingInformation.RowStaus.LeftMid && (rowStatus3 == SurroundingInformation.RowStaus.LeftMid || rowStatus3 == SurroundingInformation.RowStaus.All))
        return EdgeRotationTYPE.SideStraight_GapRight;
      if ((rowStatus1 == SurroundingInformation.RowStaus.MidRight || rowStatus1 == SurroundingInformation.RowStaus.All) && rowStatus2 == SurroundingInformation.RowStaus.MidRight && (rowStatus3 == SurroundingInformation.RowStaus.MidRight || rowStatus3 == SurroundingInformation.RowStaus.All))
        return EdgeRotationTYPE.SideStraight_GapLeft;
      if (rowStatus1 == SurroundingInformation.RowStaus.All && rowStatus2 == SurroundingInformation.RowStaus.All && (rowStatus3 == SurroundingInformation.RowStaus.None || rowStatus3 == SurroundingInformation.RowStaus.LeftAndRight || (rowStatus3 == SurroundingInformation.RowStaus.Left || rowStatus3 == SurroundingInformation.RowStaus.Right)))
        return EdgeRotationTYPE.BottomStraight_GapBelow;
      if ((rowStatus1 == SurroundingInformation.RowStaus.None || rowStatus1 == SurroundingInformation.RowStaus.Left || (rowStatus1 == SurroundingInformation.RowStaus.Right || rowStatus1 == SurroundingInformation.RowStaus.LeftAndRight)) && (rowStatus2 == SurroundingInformation.RowStaus.MidRight && (rowStatus3 == SurroundingInformation.RowStaus.MidRight || rowStatus3 == SurroundingInformation.RowStaus.All)))
        return EdgeRotationTYPE.TopLeftCorner;
      if ((rowStatus1 == SurroundingInformation.RowStaus.None || rowStatus1 == SurroundingInformation.RowStaus.Right || (rowStatus1 == SurroundingInformation.RowStaus.Left || rowStatus1 == SurroundingInformation.RowStaus.LeftAndRight)) && (rowStatus2 == SurroundingInformation.RowStaus.LeftMid && (rowStatus3 == SurroundingInformation.RowStaus.LeftMid || rowStatus3 == SurroundingInformation.RowStaus.All)))
        return EdgeRotationTYPE.TopRightCorner;
      if ((rowStatus1 == SurroundingInformation.RowStaus.MidRight || rowStatus1 == SurroundingInformation.RowStaus.All) && rowStatus2 == SurroundingInformation.RowStaus.MidRight && (rowStatus3 == SurroundingInformation.RowStaus.None || rowStatus3 == SurroundingInformation.RowStaus.Right || (rowStatus3 == SurroundingInformation.RowStaus.Left || rowStatus3 == SurroundingInformation.RowStaus.LeftAndRight)))
        return EdgeRotationTYPE.BottomLeftCorner;
      if ((rowStatus1 == SurroundingInformation.RowStaus.LeftMid || rowStatus1 == SurroundingInformation.RowStaus.All) && rowStatus2 == SurroundingInformation.RowStaus.LeftMid && (rowStatus3 == SurroundingInformation.RowStaus.None || rowStatus3 == SurroundingInformation.RowStaus.Left || (rowStatus3 == SurroundingInformation.RowStaus.Right || rowStatus3 == SurroundingInformation.RowStaus.LeftAndRight)))
        return EdgeRotationTYPE.BottomRightCorner;
      if (rowStatus1 == SurroundingInformation.RowStaus.All && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.LeftMid)
        return EdgeRotationTYPE.TopLeftInnerCorner;
      if (rowStatus1 == SurroundingInformation.RowStaus.All && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.MidRight)
        return EdgeRotationTYPE.TopRightInnerCorner;
      if (rowStatus1 == SurroundingInformation.RowStaus.LeftMid && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.All)
        return EdgeRotationTYPE.BottomLeftInnerCorner;
      if (rowStatus1 == SurroundingInformation.RowStaus.MidRight && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.All)
        return EdgeRotationTYPE.BottomRightInnerCorner;
      if ((rowStatus1 == SurroundingInformation.RowStaus.None || rowStatus1 == SurroundingInformation.RowStaus.Left || (rowStatus1 == SurroundingInformation.RowStaus.Right || rowStatus1 == SurroundingInformation.RowStaus.LeftAndRight)) && rowStatus2 == SurroundingInformation.RowStaus.All && (rowStatus3 == SurroundingInformation.RowStaus.None || rowStatus3 == SurroundingInformation.RowStaus.LeftAndRight || (rowStatus3 == SurroundingInformation.RowStaus.Left || rowStatus3 == SurroundingInformation.RowStaus.Right)))
        return EdgeRotationTYPE.Single_StraightHorizontal;
      if ((rowStatus1 == SurroundingInformation.RowStaus.Mid || rowStatus1 == SurroundingInformation.RowStaus.LeftMid || (rowStatus1 == SurroundingInformation.RowStaus.MidRight || rowStatus1 == SurroundingInformation.RowStaus.All)) && rowStatus2 == SurroundingInformation.RowStaus.Mid && (rowStatus3 == SurroundingInformation.RowStaus.Mid || rowStatus3 == SurroundingInformation.RowStaus.LeftMid || (rowStatus3 == SurroundingInformation.RowStaus.MidRight || rowStatus3 == SurroundingInformation.RowStaus.All)))
        return EdgeRotationTYPE.Single_StraightVertical;
      if ((rowStatus1 == SurroundingInformation.RowStaus.None || rowStatus1 == SurroundingInformation.RowStaus.Right || (rowStatus1 == SurroundingInformation.RowStaus.Left || rowStatus1 == SurroundingInformation.RowStaus.LeftAndRight)) && rowStatus2 == SurroundingInformation.RowStaus.LeftMid && (rowStatus3 == SurroundingInformation.RowStaus.None || rowStatus3 == SurroundingInformation.RowStaus.Left || (rowStatus3 == SurroundingInformation.RowStaus.LeftAndRight || rowStatus3 == SurroundingInformation.RowStaus.Right)))
        return EdgeRotationTYPE.Single_HorizontalEnd_NoRight;
      if ((rowStatus1 == SurroundingInformation.RowStaus.None || rowStatus1 == SurroundingInformation.RowStaus.Right || (rowStatus1 == SurroundingInformation.RowStaus.LeftAndRight || rowStatus1 == SurroundingInformation.RowStaus.Left)) && rowStatus2 == SurroundingInformation.RowStaus.MidRight && (rowStatus3 == SurroundingInformation.RowStaus.None || rowStatus3 == SurroundingInformation.RowStaus.Right || (rowStatus3 == SurroundingInformation.RowStaus.LeftAndRight || rowStatus3 == SurroundingInformation.RowStaus.Left)))
        return EdgeRotationTYPE.Single_HorizontalEnd_NoLeft;
      if ((rowStatus1 == SurroundingInformation.RowStaus.None || rowStatus1 == SurroundingInformation.RowStaus.Left || (rowStatus1 == SurroundingInformation.RowStaus.Right || rowStatus1 == SurroundingInformation.RowStaus.LeftAndRight)) && rowStatus2 == SurroundingInformation.RowStaus.Mid && (rowStatus3 == SurroundingInformation.RowStaus.Mid || rowStatus3 == SurroundingInformation.RowStaus.All || (rowStatus3 == SurroundingInformation.RowStaus.LeftMid || rowStatus3 == SurroundingInformation.RowStaus.MidRight)))
        return EdgeRotationTYPE.Single_VerticalEnd_NoTop;
      if ((rowStatus1 == SurroundingInformation.RowStaus.Mid || rowStatus1 == SurroundingInformation.RowStaus.All || (rowStatus1 == SurroundingInformation.RowStaus.LeftMid || rowStatus1 == SurroundingInformation.RowStaus.MidRight)) && rowStatus2 == SurroundingInformation.RowStaus.Mid && (rowStatus3 == SurroundingInformation.RowStaus.None || rowStatus3 == SurroundingInformation.RowStaus.Right || (rowStatus3 == SurroundingInformation.RowStaus.Left || rowStatus3 == SurroundingInformation.RowStaus.LeftAndRight)))
        return EdgeRotationTYPE.Single_VerticalEnd_NoBottom;
      if ((rowStatus1 == SurroundingInformation.RowStaus.None || rowStatus1 == SurroundingInformation.RowStaus.Left || (rowStatus1 == SurroundingInformation.RowStaus.Right || rowStatus1 == SurroundingInformation.RowStaus.LeftAndRight)) && rowStatus2 == SurroundingInformation.RowStaus.Mid && (rowStatus3 == SurroundingInformation.RowStaus.None || rowStatus3 == SurroundingInformation.RowStaus.LeftAndRight || (rowStatus3 == SurroundingInformation.RowStaus.Left || rowStatus3 == SurroundingInformation.RowStaus.Right)))
        return EdgeRotationTYPE.SIngle_Surrounded;
      if (rowStatus1 == SurroundingInformation.RowStaus.None && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.MidRight)
        return EdgeRotationTYPE.SingleTopStraight_BottomInnerCornerLeft;
      if (rowStatus1 == SurroundingInformation.RowStaus.None && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.LeftMid)
        return EdgeRotationTYPE.SingleTopStraight_BottomInnerCornerRight;
      if (rowStatus1 == SurroundingInformation.RowStaus.None && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.MidRight)
        return EdgeRotationTYPE.SingleBottomStraight_TopInnerCornerRight;
      if (rowStatus1 == SurroundingInformation.RowStaus.None && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.LeftMid)
        return EdgeRotationTYPE.SingleBottomStraight_TopInnerCornerLeft;
      if ((rowStatus1 == SurroundingInformation.RowStaus.MidRight || rowStatus1 == SurroundingInformation.RowStaus.All) && rowStatus2 == SurroundingInformation.RowStaus.MidRight && (rowStatus3 == SurroundingInformation.RowStaus.Mid || rowStatus3 == SurroundingInformation.RowStaus.LeftMid))
        return EdgeRotationTYPE.SingleInnerCorner_LeftSideStraight_OposingCornerDown;
      if ((rowStatus1 == SurroundingInformation.RowStaus.Mid || rowStatus1 == SurroundingInformation.RowStaus.LeftMid) && rowStatus2 == SurroundingInformation.RowStaus.MidRight && (rowStatus3 == SurroundingInformation.RowStaus.MidRight || rowStatus3 == SurroundingInformation.RowStaus.All))
        return EdgeRotationTYPE.SingleInnerCorner_LeftSideStraight_OposingCornerUp;
      if ((rowStatus1 == SurroundingInformation.RowStaus.LeftMid || rowStatus1 == SurroundingInformation.RowStaus.All) && rowStatus2 == SurroundingInformation.RowStaus.LeftMid && (rowStatus3 == SurroundingInformation.RowStaus.Mid || rowStatus3 == SurroundingInformation.RowStaus.MidRight))
        return EdgeRotationTYPE.SingleInnerCorner_RightSideStraight_OposingCornerDown;
      if ((rowStatus1 == SurroundingInformation.RowStaus.Mid || rowStatus1 == SurroundingInformation.RowStaus.MidRight) && rowStatus2 == SurroundingInformation.RowStaus.LeftMid && (rowStatus3 == SurroundingInformation.RowStaus.LeftMid || rowStatus3 == SurroundingInformation.RowStaus.All))
        return EdgeRotationTYPE.SingleInnerCorner_RightSideStraight_OposingCornerUp;
      if ((rowStatus1 == SurroundingInformation.RowStaus.None || rowStatus1 == SurroundingInformation.RowStaus.LeftAndRight || (rowStatus1 == SurroundingInformation.RowStaus.Left || rowStatus1 == SurroundingInformation.RowStaus.Right)) && (rowStatus2 == SurroundingInformation.RowStaus.MidRight && (rowStatus3 == SurroundingInformation.RowStaus.Mid || rowStatus3 == SurroundingInformation.RowStaus.LeftMid)))
        return EdgeRotationTYPE.SingleCorner_CornerTopLeft_OpposingLink;
      if ((rowStatus1 == SurroundingInformation.RowStaus.None || rowStatus1 == SurroundingInformation.RowStaus.LeftAndRight || (rowStatus1 == SurroundingInformation.RowStaus.Right || rowStatus1 == SurroundingInformation.RowStaus.Left)) && (rowStatus2 == SurroundingInformation.RowStaus.LeftMid && (rowStatus3 == SurroundingInformation.RowStaus.Mid || rowStatus3 == SurroundingInformation.RowStaus.MidRight)))
        return EdgeRotationTYPE.SingleCorner_CornerTopRight_OpposingLink;
      if ((rowStatus1 == SurroundingInformation.RowStaus.Mid || rowStatus1 == SurroundingInformation.RowStaus.MidRight) && rowStatus2 == SurroundingInformation.RowStaus.LeftMid && (rowStatus3 == SurroundingInformation.RowStaus.None || rowStatus3 == SurroundingInformation.RowStaus.Right | rowStatus3 == SurroundingInformation.RowStaus.LeftAndRight | rowStatus3 == SurroundingInformation.RowStaus.Left))
        return EdgeRotationTYPE.SingleCorner_CornerBottomRight_OpposingLink;
      if ((rowStatus1 == SurroundingInformation.RowStaus.Mid || rowStatus1 == SurroundingInformation.RowStaus.LeftMid) && rowStatus2 == SurroundingInformation.RowStaus.MidRight && (rowStatus3 == SurroundingInformation.RowStaus.None || rowStatus3 == SurroundingInformation.RowStaus.Right | rowStatus3 == SurroundingInformation.RowStaus.LeftAndRight | rowStatus3 == SurroundingInformation.RowStaus.Left))
        return EdgeRotationTYPE.SingleCorner_CornerBottomLeft_OpposingLink;
      if (rowStatus1 == SurroundingInformation.RowStaus.Mid && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.Mid)
        return EdgeRotationTYPE.InnerCorner_All;
      if (rowStatus1 == SurroundingInformation.RowStaus.Mid && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.LeftMid)
        return EdgeRotationTYPE.InnerCorner_0_1_2;
      if (rowStatus1 == SurroundingInformation.RowStaus.LeftMid && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.Mid)
        return EdgeRotationTYPE.InnerCorner_1_2_3;
      if (rowStatus1 == SurroundingInformation.RowStaus.MidRight && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.Mid)
        return EdgeRotationTYPE.InnerCorner_2_3_0;
      if (rowStatus1 == SurroundingInformation.RowStaus.Mid && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.MidRight)
        return EdgeRotationTYPE.InnerCorner_3_0_1;
      if (rowStatus1 == SurroundingInformation.RowStaus.Mid && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.All)
        return EdgeRotationTYPE.InnerCorner_0_1;
      if (rowStatus1 == SurroundingInformation.RowStaus.MidRight && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.LeftMid)
        return EdgeRotationTYPE.InnerCorner_0_2;
      if (rowStatus1 == SurroundingInformation.RowStaus.MidRight && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.MidRight)
        return EdgeRotationTYPE.InnerCorner_0_3;
      if (rowStatus1 == SurroundingInformation.RowStaus.LeftMid && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.LeftMid)
        return EdgeRotationTYPE.InnerCorner_1_2;
      if (rowStatus1 == SurroundingInformation.RowStaus.LeftMid && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.MidRight)
        return EdgeRotationTYPE.InnerCorner_1_3;
      if (rowStatus1 == SurroundingInformation.RowStaus.All && rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.Mid)
        return EdgeRotationTYPE.InnerCorner_2_3;
      if ((rowStatus1 == SurroundingInformation.RowStaus.Mid || rowStatus1 == SurroundingInformation.RowStaus.LeftMid) && rowStatus2 == SurroundingInformation.RowStaus.MidRight && (rowStatus3 == SurroundingInformation.RowStaus.Mid || rowStatus3 == SurroundingInformation.RowStaus.LeftMid))
        return EdgeRotationTYPE.Single_SideLeftGap_OpposingDoubleJunction;
      if ((rowStatus1 == SurroundingInformation.RowStaus.MidRight || rowStatus1 == SurroundingInformation.RowStaus.Mid) && rowStatus2 == SurroundingInformation.RowStaus.LeftMid && (rowStatus3 == SurroundingInformation.RowStaus.Mid || rowStatus3 == SurroundingInformation.RowStaus.MidRight))
        return EdgeRotationTYPE.Single_SideRightGap_OpposingDoubleJunction;
      if ((rowStatus1 == SurroundingInformation.RowStaus.None || rowStatus1 == SurroundingInformation.RowStaus.LeftAndRight || (rowStatus1 == SurroundingInformation.RowStaus.Left || rowStatus1 == SurroundingInformation.RowStaus.Right)) && (rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.Mid))
        return EdgeRotationTYPE.Single_Horizontal_TopGap_OpposingDoubleJunction;
      if (rowStatus1 == SurroundingInformation.RowStaus.Mid && rowStatus2 == SurroundingInformation.RowStaus.All && (rowStatus3 == SurroundingInformation.RowStaus.None || rowStatus3 == SurroundingInformation.RowStaus.LeftAndRight || (rowStatus3 == SurroundingInformation.RowStaus.Left || rowStatus3 == SurroundingInformation.RowStaus.Right)))
        return EdgeRotationTYPE.Single_Horizontal_BottomGap_OpposingDoubleJunction;
      if (rowStatus1 == SurroundingInformation.RowStaus.MidRight && rowStatus2 == SurroundingInformation.RowStaus.All && (rowStatus3 == SurroundingInformation.RowStaus.None || rowStatus3 == SurroundingInformation.RowStaus.Left || (rowStatus3 == SurroundingInformation.RowStaus.Right || rowStatus3 == SurroundingInformation.RowStaus.LeftAndRight)))
        return EdgeRotationTYPE.Single_BottomStraight_GapBelow_TopCornerLeft;
      if (rowStatus1 == SurroundingInformation.RowStaus.LeftMid && rowStatus2 == SurroundingInformation.RowStaus.All && (rowStatus3 == SurroundingInformation.RowStaus.None || rowStatus3 == SurroundingInformation.RowStaus.Left || (rowStatus3 == SurroundingInformation.RowStaus.Right || rowStatus3 == SurroundingInformation.RowStaus.LeftAndRight)))
        return EdgeRotationTYPE.Single_BottomStraight_GapBelow_TopCornerRight;
      if ((rowStatus1 == SurroundingInformation.RowStaus.None || rowStatus1 == SurroundingInformation.RowStaus.Right || (rowStatus1 == SurroundingInformation.RowStaus.Left || rowStatus1 == SurroundingInformation.RowStaus.LeftAndRight)) && (rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.MidRight))
        return EdgeRotationTYPE.Single_TopStraight_GapAbove_LowerCornerLeft;
      if ((rowStatus1 == SurroundingInformation.RowStaus.None || rowStatus1 == SurroundingInformation.RowStaus.Right || (rowStatus1 == SurroundingInformation.RowStaus.Left || rowStatus1 == SurroundingInformation.RowStaus.LeftAndRight)) && (rowStatus2 == SurroundingInformation.RowStaus.All && rowStatus3 == SurroundingInformation.RowStaus.LeftMid))
        return EdgeRotationTYPE.Single_TopStraight_GapAbove_LowerCornerRight;
      throw new Exception("hsdf");
    }

    private SurroundingInformation.RowStaus GetRowStatus(int Row)
    {
      if (this.full[0, Row] && this.full[1, Row] && this.full[2, Row])
        return SurroundingInformation.RowStaus.All;
      if (this.full[0, Row] && !this.full[1, Row] && !this.full[2, Row])
        return Row == 1 ? SurroundingInformation.RowStaus.LeftMid : SurroundingInformation.RowStaus.Left;
      if (this.full[0, Row] && this.full[1, Row] && !this.full[2, Row])
        return SurroundingInformation.RowStaus.LeftMid;
      if (!this.full[0, Row] && this.full[1, Row] && this.full[2, Row])
        return SurroundingInformation.RowStaus.MidRight;
      if (!this.full[0, Row] && this.full[1, Row] && !this.full[2, Row])
        return SurroundingInformation.RowStaus.Mid;
      if (!this.full[0, Row] && !this.full[1, Row] && this.full[2, Row])
        return Row == 1 ? SurroundingInformation.RowStaus.MidRight : SurroundingInformation.RowStaus.Right;
      if (this.full[0, Row] && !this.full[1, Row] && this.full[2, Row])
        return Row == 1 ? SurroundingInformation.RowStaus.All : SurroundingInformation.RowStaus.LeftAndRight;
      if (this.full[0, Row] || this.full[1, Row] || this.full[2, Row])
        throw new Exception("YOU SCREWED UP");
      return Row == 1 ? SurroundingInformation.RowStaus.Mid : SurroundingInformation.RowStaus.None;
    }

    private enum RowStaus
    {
      All,
      Left,
      LeftMid,
      MidRight,
      Mid,
      Right,
      LeftAndRight,
      None,
    }
  }
}
