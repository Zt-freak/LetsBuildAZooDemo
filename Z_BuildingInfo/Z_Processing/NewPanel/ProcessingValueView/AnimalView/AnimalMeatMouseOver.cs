// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.AnimalView.AnimalMeatMouseOver
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.MeatView;
using TinyZoo.Z_Farms;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.AnimalView
{
  internal class AnimalMeatMouseOver
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private AnimalToMeatProduct animalToMeat;
    private ZGenericText itemName;

    public AnimalType refAnimalType { get; private set; }

    public CROPTYPE refCropType { get; private set; }

    public AnimalFoodType refFoodType { get; private set; }

    public AnimalMeatMouseOver(
      AnimalType animalType,
      Player player,
      float BaseScale,
      CROPTYPE cropType = CROPTYPE.Count,
      bool IncludeName = true,
      AnimalFoodType animalFoodType = AnimalFoodType.Count,
      bool HasDiscovered = true)
    {
      this.refAnimalType = animalType;
      this.refCropType = cropType;
      this.refFoodType = animalFoodType;
      Vector2 vector2_1 = new UIScaleHelper(BaseScale).DefaultBuffer * 0.5f;
      if (IncludeName)
      {
        this.itemName = new ZGenericText(HasDiscovered ? (cropType == CROPTYPE.Count ? (this.refFoodType == AnimalFoodType.Count ? EnemyData.GetEnemyTypeName(animalType) : AnimalFoodData.GetAnimalFoodTypeToString(animalFoodType)) : CropData.GetCropTypeToString(cropType)) : "Unknown", BaseScale, false, _UseOnePointFiveFont: true);
        this.itemName.SetAllColours(ColourData.Z_FrameMidBrown);
      }
      if (this.refFoodType == AnimalFoodType.Count)
      {
        this.animalToMeat = new AnimalToMeatProduct(animalType, player, BaseScale, cropType: cropType);
        this.animalToMeat.SetTextColor(ColourData.Z_FrameMidBrown);
      }
      Vector2 _VSCale = Vector2.Zero + vector2_1;
      if (this.itemName != null)
      {
        this.itemName.vLocation = _VSCale + vector2_1;
        _VSCale.Y += this.itemName.GetSize().Y;
        _VSCale.Y += vector2_1.Y * 2f;
      }
      if (this.animalToMeat != null)
      {
        this.animalToMeat.location = _VSCale;
        this.animalToMeat.location.Y += this.animalToMeat.GetSize().Y * 0.5f;
        _VSCale += this.animalToMeat.GetSize();
        _VSCale.Y += vector2_1.Y;
      }
      else if (this.itemName != null)
        _VSCale.X += this.itemName.GetSize().X + vector2_1.X * 2f;
      _VSCale.X += vector2_1.X;
      this.customerFrame = new CustomerFrame(_VSCale, CustomerFrameColors.CreamWithBorder, BaseScale);
      Vector2 vector2_2 = -this.customerFrame.VSCale * 0.5f;
      if (this.animalToMeat != null)
        this.animalToMeat.location += vector2_2;
      if (this.itemName == null)
        return;
      ZGenericText itemName = this.itemName;
      itemName.vLocation = itemName.vLocation + vector2_2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateAnimalMeatMouseOver()
    {
    }

    public void DrawAnimalMeatMouseOver(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      if (this.animalToMeat != null)
        this.animalToMeat.DrawAnimalToMeatProduct(offset, spriteBatch);
      if (this.itemName == null)
        return;
      this.itemName.DrawZGenericText(offset, spriteBatch);
    }
  }
}
