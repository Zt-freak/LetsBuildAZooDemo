// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Shelter.SheltderedAnimals.Column1Animal
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Shelter.SheltderedAnimals
{
  internal class Column1Animal
  {
    private AnimalInFrame animalInFrame;
    public Vector2 Location;
    private ZGenericText animalName;
    private float width;

    public Column1Animal(AnimalRenderDescriptor _animal, float BaseScale) => this.SetUp(_animal, 25f * BaseScale, BaseScale);

    public Column1Animal(AnimalType _animal, int Variant, float iconSize, float BaseScale = -1f) => this.SetUp(new AnimalRenderDescriptor(_animal, Variant), iconSize, BaseScale);

    private void SetUp(AnimalRenderDescriptor _animal, float iconSize, float BaseScale = -1f)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.animalInFrame = (double) BaseScale == -1.0 ? new AnimalInFrame(_animal.bodyAnimalType, _animal.headAnimalType, _animal.variant, iconSize, HeadVariant: _animal.headVariant) : new AnimalInFrame(_animal.bodyAnimalType, _animal.headAnimalType, _animal.variant, iconSize, 6f * BaseScale, BaseScale, _animal.headVariant);
      this.animalInFrame.Location.X += this.animalInFrame.GetSize().X * 0.5f;
      this.width = this.animalInFrame.GetSize().X;
      this.width += uiScaleHelper.GetDefaultXBuffer();
      string empty = string.Empty;
      this.animalName = new ZGenericText(_animal.headAnimalType != AnimalType.None ? HybridNames.GetAnimalCombinedName(_animal.bodyAnimalType, _animal.headAnimalType) : EnemyData.GetEnemyTypeName(_animal.bodyAnimalType), BaseScale, false, _UseOnePointFiveFont: true);
      this.animalName.vLocation.X = this.width;
      this.animalName.vLocation.Y -= this.animalName.GetSize().Y * 0.5f;
      this.width += this.animalName.GetSize().X;
    }

    public Vector2 GetSize() => new Vector2(this.width, this.animalInFrame.GetSize().Y);

    public void DrawColumn1Animal(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.Location;
      this.animalInFrame.DrawAnimalInFrame(Offset, spriteBatch);
      this.animalName.DrawZGenericText(Offset, spriteBatch);
    }
  }
}
