// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.InfoBox.AnimalLocationInfoBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_Collection.Animals.DetailFrame;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.InfoBox
{
  internal class AnimalLocationInfoBox
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private SimpleTextHandler paraDesc;
    private ZGenericText animalTypeHeader;
    private AnimalVariantsAndDNA animalVariants;

    public AnimalLocationInfoBox(
      AnimalType animalType,
      int variantNeeded,
      Player player,
      float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float y1 = defaultYbuffer;
      float x = defaultXbuffer;
      this.animalTypeHeader = new ZGenericText(EnemyData.GetEnemyTypeName(animalType), BaseScale, false, _UseOnePointFiveFont: true);
      this.animalTypeHeader.vLocation = new Vector2(x, y1);
      float y2 = y1 + (this.animalTypeHeader.GetSize().Y + defaultYbuffer);
      this.animalVariants = new AnimalVariantsAndDNA(animalType, player, BaseScale);
      this.animalVariants.location = new Vector2(x, y2);
      float y3 = y2 + (this.animalVariants.GetSize().Y + defaultYbuffer);
      this.customerFrame = new CustomerFrame(new Vector2(x + (this.animalVariants.GetSize().X + defaultXbuffer), y3), CustomerFrameColors.DarkBrown, BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      ZGenericText animalTypeHeader = this.animalTypeHeader;
      animalTypeHeader.vLocation = animalTypeHeader.vLocation + vector2;
      this.animalVariants.location += vector2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateAnimalLocationInfoBox(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.animalVariants.UpdateAnimalVariantsAndDNA(player, DeltaTime, offset, true);
    }

    public void DrawAnimalLocationInfoBox(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.animalTypeHeader.DrawZGenericText(offset, spriteBatch);
      this.animalVariants.DrawAnimalVariantsAndDNA(offset, spriteBatch);
    }
  }
}
