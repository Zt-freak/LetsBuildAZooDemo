// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Animals.DetailFrame.AnimalDetailFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Collection.Animals.DetailFrame.Animal;
using TinyZoo.Z_Collection.Animals.Hybrid;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Collection.Animals.DetailFrame
{
  internal class AnimalDetailFrame
  {
    public Vector2 location;
    private SpeciesDescriptionColumn speciesDescription;
    private AnimalVariantsAndDNA_Column animalsAndDNA;
    private DominantHybridsSummaryDisplay dominantHybridDisplay;
    private bool ViewingFullHybridDisplay;
    private HybridFullDisplay hybridFulLDisplay;
    private float BaseScale;
    private AnimalType refAnimalType;
    private Vector2 refframeSize;

    public AnimalDetailFrame(
      AnimalType animalType,
      Player player,
      float _BaseScale,
      bool IsBaseTypeUnlocked,
      Vector2 frameSize)
    {
      this.BaseScale = _BaseScale;
      this.refAnimalType = animalType;
      this.refframeSize = frameSize;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      double defaultYbuffer = (double) uiScaleHelper.GetDefaultYBuffer();
      float x1 = defaultXbuffer;
      float y = (float) defaultYbuffer;
      float maxWidth = uiScaleHelper.ScaleX(170f);
      this.speciesDescription = new SpeciesDescriptionColumn(animalType, player, this.BaseScale, maxWidth, IsBaseTypeUnlocked);
      this.speciesDescription.location += new Vector2(x1, y);
      float num = x1 + maxWidth;
      this.animalsAndDNA = new AnimalVariantsAndDNA_Column(animalType, player, this.BaseScale);
      float width = this.animalsAndDNA.GetWidth();
      this.animalsAndDNA.location.X += num;
      float x2 = num + width + uiScaleHelper.ScaleX(75f);
      this.dominantHybridDisplay = new DominantHybridsSummaryDisplay(animalType, player, this.BaseScale);
      this.dominantHybridDisplay.location += new Vector2(x2, y);
    }

    public void UpdateAnimalDetailFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.ViewingFullHybridDisplay)
      {
        if (!this.hybridFulLDisplay.UpdateHybridFullDisplay(player, DeltaTime, offset))
          return;
        this.ViewingFullHybridDisplay = false;
      }
      else
      {
        if (this.dominantHybridDisplay.UpdateDominantHybridsSummaryDisplay(player, DeltaTime, offset))
        {
          this.hybridFulLDisplay = new HybridFullDisplay(this.refAnimalType, player, this.BaseScale, this.refframeSize);
          this.hybridFulLDisplay.location = new Vector2(0.0f, 0.0f);
          this.ViewingFullHybridDisplay = true;
        }
        this.animalsAndDNA.UpdateAnimalVariantsAndDNA_Column(player, DeltaTime, offset);
      }
    }

    public void DrawAnimalDetailFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.ViewingFullHybridDisplay)
      {
        this.hybridFulLDisplay.DrawHybridFullDisplay(offset, spriteBatch);
      }
      else
      {
        this.speciesDescription.DrawSpeciesDescriptionColumn(offset, spriteBatch);
        this.dominantHybridDisplay.DrawDominantHybridsSummaryDisplay(offset, spriteBatch);
        this.animalsAndDNA.DrawAnimalVariantsAndDNA_Column(offset, spriteBatch);
      }
    }
  }
}
