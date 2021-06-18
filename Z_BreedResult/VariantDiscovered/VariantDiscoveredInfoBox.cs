// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedResult.VariantDiscovered.VariantDiscoveredInfoBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BreedResult.VariantDiscovered
{
  internal class VariantDiscoveredInfoBox
  {
    private SimpleTextHandler description;
    private AnimalInFrame animalInFrame;
    private CustomerFrame frame;
    private string nameStr;
    private Vector2 nameLoc;
    private Vector2 nameSize;
    public Vector2 location;
    private static Color cream = new Color(ColourData.Z_Cream.X, ColourData.Z_Cream.Y, ColourData.Z_Cream.Z);
    private static float descriptionScreenWidthFraction = 0.15f;
    private Vector2 framescale;
    private float basescale;
    private string descriptionStr;

    public VariantDiscoveredInfoBox(
      AnimalType type,
      int variant,
      float basescale_,
      string descriptionString = "")
    {
      this.basescale = basescale_;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.basescale);
      this.descriptionStr = descriptionString;
      if (string.IsNullOrEmpty(this.descriptionStr))
        this.descriptionStr = "You have discovered a new " + type.ToString() + " variant! Its genome is unlocked.";
      float num1 = uiScaleHelper.ScaleY(15f);
      float num2 = uiScaleHelper.ScaleY(13f);
      this.nameStr = type.ToString();
      this.nameSize = uiScaleHelper.ScaleVector2(AssetContainer.SpringFontX1AndHalf.MeasureString(this.nameStr));
      this.description = new SimpleTextHandler(this.descriptionStr, false, VariantDiscoveredInfoBox.descriptionScreenWidthFraction * this.basescale, this.basescale, false, false);
      float TargetSize = 50f * this.basescale;
      this.animalInFrame = new AnimalInFrame(type, AnimalType.None, variant, TargetSize, BaseScale: (2f * this.basescale));
      this.framescale.X = uiScaleHelper.ScaleX(180f);
      this.framescale.Y = uiScaleHelper.ScaleY(140f);
      this.animalInFrame.Location.Y -= (float) (0.5 * ((double) this.framescale.Y - (double) TargetSize * (double) Sengine.ScreenRatioUpwardsMultiplier.Y));
      this.animalInFrame.Location.Y += uiScaleHelper.ScaleY(10f);
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.nameLoc = this.animalInFrame.Location;
      this.nameLoc.Y += 0.5f * this.animalInFrame.FrameVSCALE.Y * Sengine.ScreenRatioUpwardsMultiplier.Y + num1;
      this.description.Location = this.nameLoc;
      this.description.Location.Y += num2;
      this.description.Location.X -= 0.5f * VariantDiscoveredInfoBox.descriptionScreenWidthFraction * this.basescale * Sengine.ReferenceScreenRes.X;
      this.description.SetAllColours(ColourData.Z_Cream);
      this.description.AutoCompleteParagraph();
    }

    public Vector2 GetSize() => this.framescale;

    public void UpdateVariantDiscoveredInfoBox(float DeltaTime) => this.description.UpdateSimpleTextHandler(DeltaTime);

    public void DrawVariantDiscoveredInfoBox(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      TextFunctions.DrawJustifiedText(this.nameStr, 1f * this.basescale, offset + this.nameLoc, VariantDiscoveredInfoBox.cream, 1f, AssetContainer.SpringFontX1AndHalf, spritebatch);
      this.description.DrawSimpleTextHandler(offset, 1f, spritebatch);
      this.animalInFrame.DrawAnimalInFrame(offset, spritebatch);
    }
  }
}
