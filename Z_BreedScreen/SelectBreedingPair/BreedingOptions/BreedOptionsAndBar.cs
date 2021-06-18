// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.SelectBreedingPair.BreedingOptions.BreedOptionsAndBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_BreedScreen.ConfirmBreed;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BreedScreen.SelectBreedingPair.BreedingOptions
{
  internal class BreedOptionsAndBar
  {
    public Vector2 location;
    private AnimalInFrame[] BreedOptions;
    private OffspringPercentageBar offspringposibilityBar;
    private float totalHeight;
    private float totalWidth;

    public BreedOptionsAndBar(
      int Variant,
      Parents_AndChild Info,
      bool IsNew,
      float iconSize,
      float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float num1 = uiScaleHelper.GetDefaultYBuffer() * 0.5f;
      this.totalHeight = 0.0f;
      iconSize *= BaseScale;
      this.BreedOptions = new AnimalInFrame[3];
      this.BreedOptions[0] = new AnimalInFrame(Info.animaltype, AnimalType.None, Info.FemaleParentVariant, iconSize, 6f * BaseScale, BaseScale);
      int num2 = 1;
      if (Info.FemaleParentVariant != Info.MaleParentVariant)
      {
        ++num2;
        this.BreedOptions[1] = new AnimalInFrame(Info.animaltype, AnimalType.None, Info.MaleParentVariant, iconSize, 6f * BaseScale, BaseScale * 2f);
      }
      if (Variant != Info.FemaleParentVariant && Variant != Info.MaleParentVariant)
      {
        ++num2;
        this.BreedOptions[2] = new AnimalInFrame(Info.animaltype, AnimalType.None, Variant, iconSize, 6f * BaseScale, BaseScale * 2f);
        if (IsNew)
          this.BreedOptions[2].SetAnimalGreyedOut(true);
      }
      Vector2 size = this.BreedOptions[0].GetSize();
      float num3 = size.X + defaultXbuffer;
      float num4 = (float) (-(double) num3 * (double) num2 / 2.0 + (double) num3 * 0.5);
      int num5 = 0;
      for (int index = 0; index < this.BreedOptions.Length; ++index)
      {
        if (this.BreedOptions[index] != null)
        {
          this.BreedOptions[index].SetFrameColour(ColourData.ThreeBluesForBabies[index]);
          this.BreedOptions[index].Location = new Vector2(num4 + num3 * (float) num5, size.Y * 0.5f);
          ++num5;
        }
      }
      this.totalHeight += size.Y;
      this.totalHeight += num1;
      this.totalWidth = (float) this.BreedOptions.Length * num3 - defaultXbuffer;
      this.offspringposibilityBar = new OffspringPercentageBar(Info, BaseScale);
      this.offspringposibilityBar.Location.Y = this.totalHeight;
      this.offspringposibilityBar.Location.Y += this.offspringposibilityBar.GetSize().Y * 0.5f;
      this.totalHeight += this.offspringposibilityBar.GetSize().Y;
    }

    public Vector2 GetSize() => new Vector2(this.totalWidth, this.totalHeight);

    public void DrawBreedOptionsAndBar(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.BreedOptions.Length; ++index)
      {
        if (this.BreedOptions[index] != null)
          this.BreedOptions[index].DrawAnimalInFrame(offset, spriteBatch);
      }
      this.offspringposibilityBar.DrawOffspringPercentageBar(offset, spriteBatch);
    }
  }
}
