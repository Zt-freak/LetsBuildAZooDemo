// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.SelectBreedingPair.BreedingOptions.ParentsAndChromosome
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BreedScreen.SelectBreedingPair.BreedingOptions
{
  internal class ParentsAndChromosome
  {
    public Vector2 location;
    private BreedMeIcon Father;
    private BreedMeIcon Mother;
    private float Buffer;

    public ParentsAndChromosome(Parents_AndChild Info, float iconSize, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.Mother = new BreedMeIcon((AnimalsForBreedInfo) null, Info.animaltype, (Player) null, true, Info.FemaleParentVariant, Size_Raw: iconSize, BaseScale: BaseScale);
      this.Father = new BreedMeIcon((AnimalsForBreedInfo) null, Info.animaltype, (Player) null, false, Info.MaleParentVariant, Size_Raw: iconSize, BaseScale: BaseScale);
      this.Buffer = (float) ((double) this.Mother.GetSize().X * 0.5 + (double) uiScaleHelper.GetDefaultXBuffer() * 0.5);
      this.Mother.Location.X -= this.Buffer;
      this.Father.Location.X += this.Buffer;
    }

    public Vector2 GetSize() => new Vector2(this.Mother.GetSize().X + this.Father.GetSize().X + this.Buffer, this.Mother.GetSize().Y);

    public void DrawParentsAndChromosome(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.Father.DrawBreedMeIcon(offset, spriteBatch);
      this.Mother.DrawBreedMeIcon(offset, spriteBatch);
    }
  }
}
