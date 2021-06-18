// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._05Info.Ideal.IdealHabitatManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_PenInfo.MainBar;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._05Info.Ideal
{
  internal class IdealHabitatManager
  {
    public Vector2 location;
    private CustomerFrame customerframe;
    private SimpleBuildingRenderer buildingRenderer;
    private ZGenericText enclosureName;
    private ZGenericText desc;
    private ZGenericText desc2;

    public IdealHabitatManager(PrisonerInfo animal, float width, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.customerframe = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerframe.AddMiniHeading("Ideal Living Conditions");
      Vector2 vector2_1 = uiScaleHelper.ScaleVector2(Vector2.One * 50f);
      CellBlockType typeForThisAnimal = AnimalData.GetBestEnclosureTypeForThisAnimal(animal.intakeperson.animaltype);
      this.buildingRenderer = new SimpleBuildingRenderer(BarButtonIcon.GetTileTypeToPenTileTypeIcon(TileData.GetCellBlockToTileType(typeForThisAnimal)));
      this.buildingRenderer.SetSize(vector2_1.X, BaseScale);
      this.enclosureName = new ZGenericText(TileData.GetCellBlockName(typeForThisAnimal), BaseScale, false, _UseOnePointFiveFont: true);
      float MustHaveAtleastThisMuchSpace = 0.0f;
      this.desc = new ZGenericText("Space (tiles) per animal: " + AnimalData.GetRequiredFloorSpacePerAnimal(animal.intakeperson.animaltype, ref MustHaveAtleastThisMuchSpace).ToString(), BaseScale, false);
      this.desc2 = new ZGenericText("Optimal Group Size: " + (object) AnimalData.GetIdealGroupSize(animal.intakeperson.animaltype), BaseScale, false);
      Vector2 zero = Vector2.Zero;
      zero.Y += this.customerframe.GetMiniHeadingHeight();
      zero.X += defaultBuffer.X;
      this.buildingRenderer.vLocation = zero;
      SimpleBuildingRenderer buildingRenderer1 = this.buildingRenderer;
      buildingRenderer1.vLocation = buildingRenderer1.vLocation + vector2_1 * 0.5f;
      this.enclosureName.vLocation = this.buildingRenderer.vLocation;
      this.enclosureName.vLocation.X += vector2_1.X * 0.5f + defaultBuffer.X;
      this.enclosureName.vLocation.Y -= this.enclosureName.GetSize().Y * 0.5f;
      zero.Y += vector2_1.Y;
      this.desc.vLocation = zero;
      zero.Y += this.desc.GetSize().Y;
      this.desc2.vLocation = zero;
      zero.Y += this.desc2.GetSize().Y;
      zero.Y += defaultBuffer.Y;
      zero.X = width;
      this.customerframe.Resize(zero);
      Vector2 vector2_2 = -this.customerframe.VSCale * 0.5f;
      SimpleBuildingRenderer buildingRenderer2 = this.buildingRenderer;
      buildingRenderer2.vLocation = buildingRenderer2.vLocation + vector2_2;
      ZGenericText enclosureName = this.enclosureName;
      enclosureName.vLocation = enclosureName.vLocation + vector2_2;
      ZGenericText desc = this.desc;
      desc.vLocation = desc.vLocation + vector2_2;
      ZGenericText desc2 = this.desc2;
      desc2.vLocation = desc2.vLocation + vector2_2;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void UpdateIdealHabitatManager()
    {
    }

    public void DrawIdealHabitatManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerframe.DrawCustomerFrame(offset, spriteBatch);
      this.buildingRenderer.DrawSimpleBuildingRenderer(offset, spriteBatch);
      this.enclosureName.DrawZGenericText(offset, spriteBatch);
      this.desc.DrawZGenericText(offset, spriteBatch);
      this.desc2.DrawZGenericText(offset, spriteBatch);
    }
  }
}
