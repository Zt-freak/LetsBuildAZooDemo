// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo.SubElements.BuildingRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.ChangeBuildingSkin;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo.SubElements
{
  internal class BuildingRow
  {
    public Vector2 location;
    private List<BuildingOnAButton> buildingIcons;
    private float BaseScale;
    private float width;

    public BuildingRow(float _BaseScale, List<TILETYPE> buildingsTypes, int _bufferRaw = 5)
    {
      this.BaseScale = _BaseScale;
      Vector2 vector2 = new UIScaleHelper(this.BaseScale).ScaleVector2(Vector2.One * (float) _bufferRaw);
      this.buildingIcons = new List<BuildingOnAButton>();
      float num = 35f;
      for (int index = 0; index < buildingsTypes.Count; ++index)
      {
        BuildingOnAButton buildingOnAbutton = new BuildingOnAButton(buildingsTypes[index], this.BaseScale, rawsizeX_: num, rawsizeY_: num, _DrawQuestionMark: (buildingsTypes[index] == TILETYPE.None));
        Vector2 size = buildingOnAbutton.GetSize();
        buildingOnAbutton.location.X = this.width;
        buildingOnAbutton.location.X += size.X * 0.5f;
        this.width += size.X + vector2.X;
        this.buildingIcons.Add(buildingOnAbutton);
      }
    }

    public Vector2 GetSize() => new Vector2(this.width, this.buildingIcons[0].GetSize().Y);

    public void UpdateBuildingRow()
    {
    }

    public void DrawBuildingRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.buildingIcons.Count; ++index)
        this.buildingIcons[index].DrawZGenericButton(spriteBatch, offset);
    }
  }
}
