// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.HoldingCells.HoldingCellInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GamePlay.ReclaimedZones;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.Tile_Data;

namespace TinyZoo.PlayerDir.Layout.HoldingCells
{
  internal class HoldingCellInfo
  {
    public Vector2Int HoldingCellRoot;
    public PrisonerContainer prisonercontainer;
    public bool PeopleChanged;

    public HoldingCellInfo(Vector2Int Location)
    {
      this.prisonercontainer = new PrisonerContainer();
      this.HoldingCellRoot = new Vector2Int(Location);
    }

    public void TransferPrisonersToHoldingCell_FromIntake(List<IntakePerson> People, Player player) => throw new Exception("NOT IN GAME - IF USED remember to add food");

    public void StickPrisonerInThisCell(PrisonerInfo prisoner) => throw new Exception("NOT IN GAME - IF USED remember to add food");

    public bool HasThisAlienSomewhere(AnimalType enemytype)
    {
      for (int index = 0; index < this.prisonercontainer.prisoners.Count; ++index)
      {
        if (this.prisonercontainer.prisoners[index].intakeperson.animaltype == enemytype)
          return true;
      }
      return false;
    }

    public PrisonerInfo TryToRemoveThisEnemy(IntakePerson intakeperson)
    {
      for (int index = this.prisonercontainer.prisoners.Count - 1; index > -1; --index)
      {
        if (this.prisonercontainer.prisoners[index].intakeperson == intakeperson)
        {
          PrisonerInfo prisoner = this.prisonercontainer.prisoners[index];
          this.prisonercontainer.prisoners.RemoveAt(index);
          GameFlags.prisonersJustChangedInHoldingCell = true;
          this.prisonercontainer.ThisWasTehCellBlockThatChanged = true;
          this.PeopleChanged = true;
          return prisoner;
        }
      }
      return (PrisonerInfo) null;
    }

    public EnemyManager GetEnemyRenderer(BoxZoneManager boxzonemanager)
    {
      for (int index = 0; index < this.prisonercontainer.prisoners.Count; ++index)
        this.prisonercontainer.prisoners[index].Location = this.GetRandomInmateLocationInWorldSpace();
      return new EnemyManager(this.prisonercontainer, boxzonemanager, (PrisonZone) null);
    }

    public bool IsThisLocationTheSame(Vector2Int _HoldingCellRoot) => this.HoldingCellRoot.CompareMatches(_HoldingCellRoot);

    public bool HoldingCellIshere(Vector2Int location) => location.CompareMatches(this.HoldingCellRoot);

    public void SetMapLimits()
    {
      TileInfo tileInfo = TileData.GetTileInfo(TILETYPE.HoldingCell);
      TileMath.SetPlaySpaceLeft((float) ((this.HoldingCellRoot.X - tileInfo.GetXTileOrigin(-1)) * 16 + 16));
      TileMath.SetPlaySpaceTop((float) ((this.HoldingCellRoot.Y - tileInfo.GetYTileOrigin(-1)) * 16));
      TileMath.SetPlaySpaceRight((float) ((this.HoldingCellRoot.X - tileInfo.GetXTileOrigin(-1) + tileInfo.GetTileWidth(-1)) * 16 - 32));
      TileMath.SetPlaySpaceBottom((float) ((this.HoldingCellRoot.Y - tileInfo.GetYTileOrigin(-1) + tileInfo.GetTileHeight(-1)) * 16 - 32));
    }

    public Vector2 GetRandomInmateLocationInWorldSpace()
    {
      Vector2 tileToWorldSpace = TileMath.GetTileToWorldSpace(this.HoldingCellRoot);
      TileInfo tileInfo = TileData.GetTileInfo(TILETYPE.HoldingCell);
      float num1 = (float) (tileInfo.GetXTileOrigin(-1) * -16);
      float num2 = (float) (tileInfo.GetTileWidth(-1) * 16) + num1;
      float num3 = num1 + 16f;
      float num4 = num2 - 16f;
      float num5 = -32f;
      float num6 = 32f;
      tileToWorldSpace.X += (float) TinyZoo.Game1.Rnd.Next((int) num5, (int) num6);
      float num7 = (float) (tileInfo.GetYTileOrigin(-1) * -16);
      float num8 = (float) (tileInfo.GetTileHeight(-1) * 16) + num7;
      num3 = num7 + 16f;
      num4 = num8 - 16f;
      float num9 = -16f;
      float num10 = 16f;
      tileToWorldSpace.Y += (float) TinyZoo.Game1.Rnd.Next((int) num9, (int) num10);
      return tileToWorldSpace;
    }

    public int GetFreeTransferSlots() => GameFlags.MaxHoldngCell - this.prisonercontainer.prisoners.Count;

    public HoldingCellInfo(Reader reader, int VersionNumberForLoad)
    {
      this.HoldingCellRoot = new Vector2Int(reader);
      this.prisonercontainer = new PrisonerContainer(reader, CellBlockType.HoldingCell, VersionNumberForLoad);
    }

    public void SaveHoldingCellInfo(Writer writer)
    {
      this.HoldingCellRoot.SaveVector2Int(writer);
      this.prisonercontainer.SavePrisonContainer(writer);
    }
  }
}
