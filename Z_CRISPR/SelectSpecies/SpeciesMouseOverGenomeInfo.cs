// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CRISPR.SelectSpecies.SpeciesMouseOverGenomeInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_Collection.Animals.DetailFrame;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_CRISPR.SelectSpecies
{
  internal class SpeciesMouseOverGenomeInfo
  {
    public Vector2 location;
    private CustomerFrameMouseOverBox customerFrame;
    private AnimalVariantsAndDNA variants;
    private ZGenericText name;

    public AnimalType refAnimalType { get; private set; }

    public SpeciesMouseOverGenomeInfo(AnimalType animalType, Player player, float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.refAnimalType = animalType;
      string _textToWrite = "Unknown";
      if (player.Stats.GetTotalVaiantsFound(animalType) > 0)
        _textToWrite = EnemyData.GetEnemyTypeName(animalType);
      this.name = new ZGenericText(_textToWrite, BaseScale, false, _UseOnePointFiveFont: true);
      this.variants = new AnimalVariantsAndDNA(animalType, player, BaseScale, true);
      Vector2 vector2_1 = defaultBuffer;
      this.name.vLocation = vector2_1;
      vector2_1.Y += defaultBuffer.Y * 0.5f;
      vector2_1.Y += this.name.GetSize().Y;
      this.variants.location = vector2_1;
      this.customerFrame = new CustomerFrameMouseOverBox(vector2_1 + this.variants.GetSize() + defaultBuffer, BaseScale);
      Vector2 vector2_2 = -this.customerFrame.GetSize() * 0.5f;
      this.variants.location += vector2_2;
      ZGenericText name = this.name;
      name.vLocation = name.vLocation + vector2_2;
    }

    public Vector2 GetSize() => this.customerFrame.GetSize();

    public void DrawSpeciesMouseOverGenomeInfo(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.Active = true;
      this.customerFrame.DrawCustomerFrameMouseOverBoc(offset, spriteBatch);
      this.variants.DrawAnimalVariantsAndDNA(offset, spriteBatch);
      this.name.DrawZGenericText(offset, spriteBatch);
    }
  }
}
