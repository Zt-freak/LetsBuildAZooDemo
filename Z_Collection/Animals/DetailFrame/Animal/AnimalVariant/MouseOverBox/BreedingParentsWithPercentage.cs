// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Animals.DetailFrame.Animal.AnimalVariant.MouseOverBox.BreedingParentsWithPercentage
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_Breeding;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Collection.Animals.DetailFrame.Animal.AnimalVariant.MouseOverBox
{
  internal class BreedingParentsWithPercentage
  {
    public Vector2 location;
    private ZGenericText descText;
    private AnimalInFrame parentOne;
    private AnimalInFrame parentTwo;
    private ZGenericText percentage;
    private SimpleTextHandler percentageHeader;
    private float width;
    private float height;

    public BreedingParentsWithPercentage(AnimalType animalType, int TargetVariant, float BaseScale)
    {
      int Mother;
      int Father;
      int PercentChance;
      BreedData.GetParentsAndPercent(animalType, TargetVariant, out Mother, out Father, out PercentChance);
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float num = uiScaleHelper.GetDefaultYBuffer() * 0.5f;
      this.width = 0.0f;
      this.height = 0.0f;
      this.descText = new ZGenericText("Parents", BaseScale, false);
      this.descText.vLocation = new Vector2(this.width, this.height);
      this.height += this.descText.GetSize().Y + num;
      this.parentOne = new AnimalInFrame(animalType, AnimalType.None, Mother, 25f * BaseScale, 6f * BaseScale, BaseScale);
      this.parentOne.Location = new Vector2(this.width, this.height) + this.parentOne.GetSize() * 0.5f;
      this.width += this.parentOne.GetSize().X + defaultXbuffer;
      this.parentTwo = new AnimalInFrame(animalType, AnimalType.None, Father, 25f * BaseScale, 6f * BaseScale, BaseScale);
      this.parentTwo.Location = new Vector2(this.width, this.height) + this.parentTwo.GetSize() * 0.5f;
      this.width += this.parentTwo.GetSize().X + defaultXbuffer;
      float y1 = 0.0f;
      this.percentageHeader = new SimpleTextHandler("Offspring~Chance", uiScaleHelper.ScaleX(100f), true, BaseScale, AutoComplete: true);
      this.percentageHeader.SetAllColours(ColourData.Z_Cream);
      float x = this.percentageHeader.GetSize(true).X;
      this.width += x * 0.5f;
      this.percentageHeader.Location = new Vector2(this.width, y1);
      this.percentageHeader.Location.Y += this.percentageHeader.GetHeightOfOneLine() * 0.5f;
      float y2 = y1 + (this.percentageHeader.GetHeightOfParagraph() + num);
      this.percentage = new ZGenericText(PercentChance.ToString() + "%", BaseScale, _UseOnePointFiveFont: true);
      this.percentage.vLocation = new Vector2(this.width, y2);
      this.percentage.vLocation.Y += this.percentage.GetSize().Y * 0.5f;
      this.width += x * 0.5f;
      this.height += this.parentOne.GetSize().Y;
    }

    public Vector2 GetSize() => new Vector2(this.width, this.height);

    public void DrawBreedingParentsWithPercentage(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.descText.DrawZGenericText(offset, spriteBatch);
      this.parentOne.DrawAnimalInFrame(offset, spriteBatch);
      this.parentTwo.DrawAnimalInFrame(offset, spriteBatch);
      this.percentageHeader.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.percentage.DrawZGenericText(offset, spriteBatch);
    }
  }
}
