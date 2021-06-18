// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._02Habitat.CurrentHab.HabitatManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_PenInfo.MainBar;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._02Habitat.CurrentHab
{
  internal class HabitatManager
  {
    public Vector2 location;
    private CustomerFrame customerframe;
    private SimpleBuildingRenderer buildingRenderer;
    private ZGenericText enclosureName;
    private ZGenericText enclosureSize;

    public HabitatManager(
      float width,
      Player player,
      PrisonerInfo animal,
      PrisonZone pz,
      int FloorSpace,
      float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.customerframe = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerframe.AddMiniHeading("Current Enclosure");
      Vector2 vector2_1 = uiScaleHelper.ScaleVector2(Vector2.One * 50f);
      this.buildingRenderer = new SimpleBuildingRenderer(BarButtonIcon.GetTileTypeToPenTileTypeIcon(TileData.GetCellBlockToTileType(pz.CellBLOCKTYPE)));
      this.buildingRenderer.SetSize(vector2_1.X, BaseScale);
      this.enclosureName = new ZGenericText(TileData.GetCellBlockName(pz.CellBLOCKTYPE), BaseScale, false, _UseOnePointFiveFont: true);
      this.enclosureSize = new ZGenericText(string.Format("Size: {0} tiles", (object) FloorSpace), BaseScale, false);
      Vector2 zero = Vector2.Zero;
      zero.Y += this.customerframe.GetMiniHeadingHeight();
      zero.X += defaultBuffer.X;
      this.buildingRenderer.vLocation = zero;
      SimpleBuildingRenderer buildingRenderer1 = this.buildingRenderer;
      buildingRenderer1.vLocation = buildingRenderer1.vLocation + vector2_1 * 0.5f;
      zero.X += vector2_1.X;
      zero.X += defaultBuffer.X;
      Vector2 vector2_2 = zero;
      zero.Y += vector2_1.Y;
      vector2_2.Y += defaultBuffer.Y;
      this.enclosureName.vLocation = vector2_2;
      vector2_2.Y += this.enclosureName.GetSize().Y;
      this.enclosureSize.vLocation = vector2_2;
      vector2_2.Y += this.enclosureSize.GetSize().Y;
      zero.X = width;
      zero.Y = Math.Max(zero.Y, vector2_2.Y);
      this.customerframe.Resize(zero);
      Vector2 vector2_3 = -this.customerframe.VSCale * 0.5f;
      SimpleBuildingRenderer buildingRenderer2 = this.buildingRenderer;
      buildingRenderer2.vLocation = buildingRenderer2.vLocation + vector2_3;
      ZGenericText enclosureName = this.enclosureName;
      enclosureName.vLocation = enclosureName.vLocation + vector2_3;
      ZGenericText enclosureSize = this.enclosureSize;
      enclosureSize.vLocation = enclosureSize.vLocation + vector2_3;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void UpdateHabitatManager()
    {
    }

    public void DrawHabitatManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerframe.DrawCustomerFrame(offset, spriteBatch);
      this.buildingRenderer.DrawSimpleBuildingRenderer(offset, spriteBatch);
      this.enclosureName.DrawZGenericText(offset, spriteBatch);
      this.enclosureSize.DrawZGenericText(offset, spriteBatch);
    }
  }
}
