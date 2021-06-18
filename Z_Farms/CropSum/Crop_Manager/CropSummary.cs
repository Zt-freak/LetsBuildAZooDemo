// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Farms.CropSum.Crop_Manager.CropSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Farms_;

namespace TinyZoo.Z_Farms.CropSum.Crop_Manager
{
  internal class CropSummary
  {
    private List<string> Descs;
    private List<string> Values;
    private GameObject TextHeader;
    public Vector2 Location;

    public CropSummary(float BaseScale, CROPTYPE croptype, Player player)
    {
      this.TextHeader = new GameObject();
      this.TextHeader.SetAllColours(ColourData.Z_Cream);
      this.TextHeader.scale = BaseScale;
      this.TextHeader.vLocation.X = -100f * BaseScale;
      this.Descs = new List<string>();
      this.Values = new List<string>();
      this.Descs.Add("Used yesterday:");
      this.Values.Add("100");
      this.Descs.Add("Held in store room:");
      this.Values.Add("100");
      this.Descs.Add("Growing in fields:");
      this.Values.Add("100");
      this.Descs.Add("Average growth time:");
      this.Values.Add("100");
      this.Descs.Add("Current Market Value:");
      this.Values.Add("100");
    }

    public void DrawCropSUmmary(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      Offset.Y += this.TextHeader.scale * 11f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      TextFunctions.DrawJustifiedText("Summary", this.TextHeader.scale, Offset + new Vector2(0.0f, this.TextHeader.vLocation.Y), this.TextHeader.GetColour(), this.TextHeader.fAlpha, AssetContainer.SpringFontX1AndHalf, spritebatch);
      Offset.Y += this.TextHeader.scale * 22f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      for (int index = 0; index < this.Descs.Count; ++index)
      {
        TextFunctions.DrawTextWithDropShadow(this.Descs[index], this.TextHeader.scale, this.TextHeader.vLocation + Offset, this.TextHeader.GetColour(), this.TextHeader.fAlpha, AssetContainer.SpringFontX1AndHalf, spritebatch, false);
        TextFunctions.DrawTextWithDropShadow(this.Values[index], this.TextHeader.scale, Offset - this.TextHeader.vLocation, this.TextHeader.GetColour(), this.TextHeader.fAlpha, AssetContainer.SpringFontX1AndHalf, spritebatch, false);
        Offset.Y += this.TextHeader.scale * 11f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      }
    }
  }
}
