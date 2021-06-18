// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.Z_NewCostInfo.StatsAndCost.BuildingMoralityRequirement
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Research_.IconGrid.Morality;

namespace TinyZoo.Z_BuldMenu.Z_NewCostInfo.StatsAndCost
{
  internal class BuildingMoralityRequirement
  {
    public Vector2 location;
    private MoralityRequirementDisplay moralityDisplay;

    public BuildingMoralityRequirement(
      TILETYPE building,
      Player player,
      float BaseScale,
      float forcedWidth)
    {
      this.moralityDisplay = new MoralityRequirementDisplay(building, player, BaseScale, forcedWidth, ColourData.Z_FrameMidBrown);
    }

    public Vector2 GetSize() => this.moralityDisplay.GetSize();

    public void DrawBuildingMoralityRequirement(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.moralityDisplay.DrawMoralityRequirementDisplay(offset, spriteBatch);
    }
  }
}
