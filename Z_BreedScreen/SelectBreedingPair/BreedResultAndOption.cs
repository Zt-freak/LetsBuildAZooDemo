// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.SelectBreedingPair.BreedResultAndOption
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_BreedScreen.SelectBreedingPair.BreedingOptions;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BreedScreen.SelectBreedingPair
{
  internal class BreedResultAndOption
  {
    public Vector2 Location;
    private TextButton textbutton;
    public CustomerFrame customerframe;
    public Parents_AndChild Ref_Info;
    private BreedOptionsAndBar breedOptionsAndBar;
    private ParentsAndChromosome parentsAndChromosome;

    public BreedResultAndOption(
      int Variant,
      AnimalType animaltype,
      bool IsNew,
      Parents_AndChild Info,
      float BaseScale)
    {
      this.Ref_Info = Info;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float num1 = uiScaleHelper.GetDefaultYBuffer() * 0.5f;
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float num2 = 0.0f;
      float num3 = 0.0f;
      float iconSize = 25f;
      float num4 = num3 + num1;
      float num5 = num2 + defaultXbuffer;
      this.parentsAndChromosome = new ParentsAndChromosome(Info, iconSize, BaseScale);
      Vector2 size1 = this.parentsAndChromosome.GetSize();
      this.parentsAndChromosome.location.X = num5 + size1.X * 0.5f;
      float num6 = num5 + (size1.X + defaultXbuffer);
      this.breedOptionsAndBar = new BreedOptionsAndBar(Variant, Info, IsNew, iconSize, BaseScale);
      Vector2 size2 = this.breedOptionsAndBar.GetSize();
      this.breedOptionsAndBar.location.X = num6 + size2.X * 0.5f;
      this.breedOptionsAndBar.location.Y = num4;
      float num7 = num6 + (size2.X + defaultXbuffer * 2f);
      float y = num4 + (size2.Y + num1);
      this.textbutton = new TextButton(BaseScale, "Breed", 40f);
      Vector2 sizeTrue = this.textbutton.GetSize_True();
      this.textbutton.vLocation.X = num7 + sizeTrue.X * 0.5f;
      this.customerframe = new CustomerFrame(new Vector2(num7 + sizeTrue.X + defaultXbuffer, y), true, BaseScale);
      Vector2 vector2 = -this.customerframe.VSCale * 0.5f;
      this.parentsAndChromosome.location.X += vector2.X;
      this.breedOptionsAndBar.location += vector2;
      this.textbutton.vLocation.X += vector2.X;
    }

    public float GetColumnLocationX(int columnIndex)
    {
      Vector2 vector2 = this.customerframe.VSCale * 0.5f;
      if (columnIndex == 0)
        return this.parentsAndChromosome.location.X + vector2.X;
      return columnIndex == 1 ? this.breedOptionsAndBar.location.X + vector2.X : this.textbutton.vLocation.X + vector2.X;
    }

    public bool UpdateBreedResultAndOption(Vector2 Offset, Player player, float DeltaTime)
    {
      Offset += this.Location;
      return this.textbutton.UpdateTextButton(player, Offset, DeltaTime);
    }

    public void DrawBreedResultAndOption(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.parentsAndChromosome.DrawParentsAndChromosome(Offset, spritebatch);
      this.breedOptionsAndBar.DrawBreedOptionsAndBar(Offset, spritebatch);
      this.textbutton.DrawTextButton(Offset, 1f, spritebatch);
    }
  }
}
