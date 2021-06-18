// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.BuyLand.CurrentLandGrid
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.PlayerDir;
using TinyZoo.Z_Manage.BuyLand.DIagram;

namespace TinyZoo.Z_Manage.BuyLand
{
  internal class CurrentLandGrid
  {
    private GameObject Sign;
    public bool AtMax;
    private DiagramFloorTile[,] floortile;
    private GameObject Road;

    public CurrentLandGrid(Player player)
    {
      this.Road = new GameObject();
      this.Road.DrawRect = new Rectangle(879, 337, 16, 12);
      this.Road.scale = 2f;
      this.Road.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.Road.DrawOrigin.Y -= 10f;
      this.Sign = new GameObject();
      this.Sign.DrawRect = new Rectangle(946, 301, 16, 11);
      this.Sign.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.AtMax = false;
      this.floortile = new DiagramFloorTile[8, 6];
      for (int index1 = 0; index1 < this.floortile.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.floortile.GetLength(1); ++index2)
          this.floortile[index1, index2] = new DiagramFloorTile(TileTypeForDiagram.Unpurchased);
      }
      this.floortile[3, 5] = new DiagramFloorTile(TileTypeForDiagram.Owned);
      this.floortile[4, 5] = new DiagramFloorTile(TileTypeForDiagram.Owned);
      this.floortile[3, 4] = new DiagramFloorTile(TileTypeForDiagram.Owned);
      this.floortile[4, 4] = new DiagramFloorTile(TileTypeForDiagram.Owned);
      this.floortile[2, 5] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
      this.floortile[5, 5] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
      this.floortile[2, 4] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
      this.floortile[5, 4] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
      if (PlayerStats.LandSize > 0)
      {
        this.floortile[2, 5] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[5, 5] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[2, 4] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[5, 4] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[2, 3] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[3, 3] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[4, 3] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[5, 3] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
      }
      if (PlayerStats.LandSize > 1)
      {
        this.floortile[2, 3] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[3, 3] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[4, 3] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[5, 3] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[1, 3] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[1, 4] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[1, 5] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[6, 3] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[6, 4] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[6, 5] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
      }
      if (PlayerStats.LandSize > 2)
      {
        this.floortile[1, 3] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[1, 4] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[1, 5] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[6, 3] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[6, 4] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[6, 5] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[1, 2] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[2, 2] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[3, 2] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[4, 2] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[5, 2] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[6, 2] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
      }
      if (PlayerStats.LandSize > 3)
      {
        this.floortile[1, 2] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[2, 2] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[3, 2] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[4, 2] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[5, 2] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[6, 2] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[0, 2] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[0, 3] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[0, 4] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[0, 5] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[7, 2] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[7, 3] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[7, 4] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[7, 5] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
      }
      if (PlayerStats.LandSize > 4)
      {
        this.floortile[0, 2] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[0, 3] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[0, 4] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[0, 5] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[7, 2] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[7, 3] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[7, 4] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[7, 5] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[0, 1] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[1, 1] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[2, 1] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[3, 1] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[4, 1] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[5, 1] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[6, 1] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[7, 1] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
      }
      if (PlayerStats.LandSize > 5)
      {
        this.floortile[0, 1] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[1, 1] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[2, 1] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[3, 1] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[4, 1] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[5, 1] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[6, 1] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[7, 1] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[0, 0] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[1, 0] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[2, 0] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[3, 0] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[4, 0] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[5, 0] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[6, 0] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
        this.floortile[7, 0] = new DiagramFloorTile(TileTypeForDiagram.BuyLand);
      }
      else if (PlayerStats.LandSize > 6)
      {
        this.floortile[0, 0] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[1, 0] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[2, 0] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[3, 0] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[4, 0] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[5, 0] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[6, 0] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        this.floortile[7, 0] = new DiagramFloorTile(TileTypeForDiagram.Owned);
        if (PlayerStats.LandSize > 7)
          this.AtMax = true;
      }
      for (int index1 = 0; index1 < this.floortile.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.floortile.GetLength(1); ++index2)
        {
          this.floortile[index1, index2].vLocation = new Vector2((float) (index1 * 32), (float) (index2 * 32));
          DiagramFloorTile diagramFloorTile = this.floortile[index1, index2];
          diagramFloorTile.vLocation = diagramFloorTile.vLocation + new Vector2(16f);
          this.floortile[index1, index2].vLocation.X += 384f;
        }
      }
    }

    public void DrawCurrentLandGrid(Vector2 Offset)
    {
      Offset += new Vector2(0.0f, 250f);
      for (int index1 = 0; index1 < this.floortile.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.floortile.GetLength(1); ++index2)
          this.floortile[index1, index2].DrawDiagramFloorTile(Offset);
      }
      for (int index = 0; index < this.floortile.GetLength(0); ++index)
        this.Road.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + this.floortile[index, this.floortile.GetLength(1) - 1].vLocation);
      this.Sign.scale = 2f;
      this.Sign.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + this.floortile[4, this.floortile.GetLength(1) - 1].vLocation + new Vector2(-16f, 19f * Sengine.ScreenRatioUpwardsMultiplier.Y));
    }
  }
}
