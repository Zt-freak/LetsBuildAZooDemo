// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Animals.DetailFrame.Animal.AnimalVariantMouseOverHintBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_Collection.Animals.DetailFrame.Animal.AnimalVariant.MouseOverBox;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Collection.Animals.DetailFrame.Animal
{
  internal class AnimalVariantMouseOverHintBox
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private CustomerFrame outlineFrame;
    private SimpleTextHandler zooLocationText;
    private SimpleTextHandler genomeText;
    private BreedingParentsWithPercentage breedingParents;

    public AnimalVariantMouseOverHintBox(
      AnimalType animalType,
      Player player,
      int Variant,
      float BaseScale,
      bool IsDNAMouseOver = false)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float y1 = defaultYbuffer;
      float x1 = defaultXbuffer;
      int totalVaiantsFound = player.Stats.GetTotalVaiantsFound(animalType);
      bool flag1 = player.Stats.GetTotalOfThisVariantFound(animalType, Variant) > 0;
      bool flag2 = totalVaiantsFound >= 10;
      float y2;
      float x2;
      if (IsDNAMouseOver)
      {
        string empty = string.Empty;
        this.genomeText = new SimpleTextHandler(!flag2 ? "Discover all the variants to unlock this genome. Genomes can be used in the CRISPR splicer." : "You have mapped this genome. Use it in the CRIPSR splicer to create hybrid animals.", 200f * BaseScale, _Scale: BaseScale, AutoComplete: true);
        this.genomeText.SetAllColours(ColourData.Z_Cream);
        this.genomeText.Location = new Vector2(x1, y1);
        y2 = y1 + (this.genomeText.GetHeightOfParagraph() + defaultYbuffer);
        x2 = x1 + (this.genomeText.GetSize(true).X + defaultXbuffer);
      }
      else if (Variant == 0 || !flag1)
      {
        string TextToWrite = string.Empty;
        if (Variant == 0)
        {
          CityName obtainThisAnimal = QuestData.GetQuestLocationToObtainThisAnimal(animalType);
          TextToWrite = obtainThisAnimal != CityName.Count ? string.Format("Obtained from:~{0} Zoo", (object) QuestData.GetCityName(obtainThisAnimal)) : "Obtained from: Unknown";
        }
        else if (!flag1)
          TextToWrite = "Breed more to discover this variant.";
        this.zooLocationText = new SimpleTextHandler(TextToWrite, 100f * BaseScale, _Scale: BaseScale, AutoComplete: true);
        this.zooLocationText.SetAllColours(ColourData.Z_Cream);
        this.zooLocationText.Location = new Vector2(x1, y1);
        y2 = y1 + (this.zooLocationText.GetHeightOfParagraph() + defaultYbuffer);
        x2 = x1 + (this.zooLocationText.GetSize(true).X + defaultXbuffer);
      }
      else
      {
        this.breedingParents = new BreedingParentsWithPercentage(animalType, Variant, BaseScale);
        this.breedingParents.location = new Vector2(x1, y1);
        x2 = x1 + (this.breedingParents.GetSize().X + defaultXbuffer);
        y2 = y1 + (this.breedingParents.GetSize().Y + defaultXbuffer);
      }
      Vector2 vector2_1 = uiScaleHelper.ScaleVector2(Vector2.One * 4f);
      this.customerFrame = new CustomerFrame(new Vector2(x2, y2) - vector2_1, ColourData.Z_FrameLightBrown, BaseScale);
      this.outlineFrame = new CustomerFrame(this.customerFrame.VSCale + vector2_1, ColourData.Z_Cream, BaseScale);
      Vector2 vector2_2 = -this.outlineFrame.VSCale * 0.5f;
      if (this.zooLocationText != null)
        this.zooLocationText.Location += vector2_2;
      if (this.breedingParents != null)
        this.breedingParents.location += vector2_2;
      if (this.genomeText == null)
        return;
      this.genomeText.Location += vector2_2;
    }

    public Vector2 GetSize() => this.outlineFrame.VSCale;

    public void UpdateAnimalVariantMouseOverHintBox()
    {
    }

    public void DrawAnimalVariantMouseOverHintBox(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.outlineFrame.DrawCustomerFrame(offset, spriteBatch);
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      if (this.zooLocationText != null)
        this.zooLocationText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      if (this.breedingParents != null)
        this.breedingParents.DrawBreedingParentsWithPercentage(offset, spriteBatch);
      if (this.genomeText == null)
        return;
      this.genomeText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
