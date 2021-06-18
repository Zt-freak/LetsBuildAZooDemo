// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Animals.DetailFrame.Animal.AnimalVariantsAndDNA_Column
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Collection.Animals.DetailFrame.Animal
{
  internal class AnimalVariantsAndDNA_Column
  {
    public Vector2 location;
    private ZGenericText header;
    private AnimalVariantsAndDNA variants;

    public AnimalVariantsAndDNA_Column(AnimalType animalType, Player player, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      double defaultXbuffer = (double) uiScaleHelper.GetDefaultXBuffer();
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float num1 = 0.0f + defaultYbuffer;
      this.header = new ZGenericText(string.Format("Variants Found: {0}/{1}", (object) player.Stats.GetTotalVaiantsFound(animalType), (object) 10), BaseScale, false, _UseOnePointFiveFont: true);
      this.header.vLocation.Y = num1;
      float num2 = num1 + this.header.GetSize().Y + defaultYbuffer * 0.5f;
      this.variants = new AnimalVariantsAndDNA(animalType, player, BaseScale);
      this.variants.location.Y = num2;
    }

    public float GetWidth() => this.variants.GetSize().X;

    public void UpdateAnimalVariantsAndDNA_Column(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.variants.UpdateAnimalVariantsAndDNA(player, DeltaTime, offset, true);
    }

    public void DrawAnimalVariantsAndDNA_Column(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.header.DrawZGenericText(offset, spriteBatch);
      this.variants.DrawAnimalVariantsAndDNA(offset, spriteBatch);
    }
  }
}
