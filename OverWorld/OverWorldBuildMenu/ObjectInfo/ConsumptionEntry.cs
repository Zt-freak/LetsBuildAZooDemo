// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.ObjectInfo.ConsumptionEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Tile_Data;

namespace TinyZoo.OverWorld.OverWorldBuildMenu.ObjectInfo
{
  internal class ConsumptionEntry
  {
    private GameObject Icon;
    private GameObject Text;
    public Vector2 Location;
    private string texttodraw;

    public ConsumptionEntry(ProductionType poductintype, int Value)
    {
      this.Icon = new GameObject();
      this.Icon.DrawRect = TileStats.GetProductionTypeToRectangle(poductintype);
      this.Icon.SetDrawOriginToCentre();
      this.Icon.scale = 1f;
      this.Text = new GameObject();
      this.Text.scale = 3f;
      if (Value < 0)
      {
        this.Text.SetAllColours(ColourData.FernRed);
        this.texttodraw = string.Concat((object) Value);
      }
      else
      {
        this.Text.SetAllColours(ColourData.FernGreen);
        this.texttodraw = "+" + (object) Value;
      }
      this.Icon.vLocation.X = -20f;
      this.Text.vLocation.X = 20f;
    }

    public void DrawConsumptionEntry(Vector2 Offset)
    {
      this.Text.vLocation.Y = -9f;
      this.Icon.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + this.Location);
      TextFunctions.DrawTextWithDropShadow(this.texttodraw, this.Text.scale, this.Text.vLocation + Offset + this.Location, this.Text.GetColour(), this.Text.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false);
    }
  }
}
