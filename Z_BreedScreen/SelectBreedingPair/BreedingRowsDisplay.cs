// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.SelectBreedingPair.BreedingRowsDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BreedScreen.SelectBreedingPair
{
  internal class BreedingRowsDisplay
  {
    public Vector2 location;
    public CustomerFrame customerFrame;
    private SimpleTextHandler ParentsHeader;
    private SimpleTextHandler OffspringHeader;
    private List<BreedResultAndOption> breedresults;

    public BreedingRowsDisplay(AnimalsForBreedInfo selectedanimal, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float num1 = 0.0f;
      float x = 0.0f + defaultXbuffer;
      float y = num1 + defaultYbuffer * 1.5f;
      this.ParentsHeader = new SimpleTextHandler("Parents", true, 0.2f, BaseScale, true, true);
      this.ParentsHeader.SetAllColours(ColourData.Z_Cream);
      this.OffspringHeader = new SimpleTextHandler("Potential~Offspring", true, 0.2f, BaseScale, true, true);
      this.OffspringHeader.SetAllColours(ColourData.Z_Cream);
      this.ParentsHeader.Location = new Vector2(x, y);
      this.OffspringHeader.Location = new Vector2(x, y);
      this.ParentsHeader.Location.Y += (this.OffspringHeader.GetHeightOfParagraph() - this.ParentsHeader.GetHeightOfParagraph()) * 0.5f;
      float num2 = y + this.OffspringHeader.GetHeightOfParagraph() * 0.5f + defaultYbuffer;
      this.breedresults = new List<BreedResultAndOption>();
      int num3 = 0;
      int num4 = 5;
      for (int index = 0; index < selectedanimal.PossibleChildVariants.Length; ++index)
      {
        if (selectedanimal.PossibleChildVariants[index] != null)
        {
          this.breedresults.Add(new BreedResultAndOption(selectedanimal.PossibleChildVariants[index].ChildVariant, selectedanimal.animaltype, selectedanimal.IsNew[index], selectedanimal.PossibleChildVariants[index], BaseScale));
          ++num3;
        }
      }
      if (this.breedresults.Count == 0)
      {
        for (int _MaleParentVariant = 0; _MaleParentVariant < selectedanimal.Males_UID.Length; ++_MaleParentVariant)
        {
          for (int _FemaleParentVariant = 0; _FemaleParentVariant < selectedanimal.Females_UID.Length; ++_FemaleParentVariant)
          {
            if (selectedanimal.Males_UID[_MaleParentVariant] > -1 && selectedanimal.Females_UID[_FemaleParentVariant] > -1 && this.breedresults.Count == 0)
            {
              this.breedresults.Add(new BreedResultAndOption(0, selectedanimal.animaltype, false, new Parents_AndChild(_FemaleParentVariant, _MaleParentVariant, selectedanimal.Females_UID[_FemaleParentVariant], selectedanimal.Males_UID[_MaleParentVariant], 100, selectedanimal.animaltype, 0), BaseScale));
              break;
            }
            if (this.breedresults.Count > 0)
              break;
          }
        }
      }
      for (int index = 0; index < this.breedresults.Count; ++index)
      {
        this.breedresults[index].Location.Y += num2;
        this.breedresults[index].Location.Y += this.breedresults[index].customerframe.VSCale.Y * 0.5f;
        this.breedresults[index].Location.Y += (float) index * ((float) num4 + this.breedresults[index].customerframe.VSCale.Y);
      }
      if (this.breedresults.Count <= 0)
        throw new Exception("NO BREEDING PAIRS FOUND, CURRENTLY NOT HANDLED");
      float num5 = x + this.breedresults[0].customerframe.VSCale.X;
      float num6 = num2 + ((float) this.breedresults.Count * this.breedresults[0].customerframe.VSCale.Y + (float) ((this.breedresults.Count - 1) * num4));
      this.customerFrame = new CustomerFrame(new Vector2(num5 + defaultXbuffer, num6 + defaultYbuffer), CustomerFrameColors.Brown, BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.ParentsHeader.Location += vector2;
      this.ParentsHeader.Location.X += this.breedresults[0].GetColumnLocationX(0);
      this.OffspringHeader.Location += vector2;
      this.OffspringHeader.Location.X += this.breedresults[0].GetColumnLocationX(1);
      for (int index = 0; index < this.breedresults.Count; ++index)
        this.breedresults[index].Location.Y += vector2.Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public Parents_AndChild UpdateBreedingRowsDisplay(
      Player player,
      float DeltaTime,
      Vector2 offset)
    {
      offset += this.location;
      for (int index = 0; index < this.breedresults.Count; ++index)
      {
        if (this.breedresults[index].UpdateBreedResultAndOption(offset, player, DeltaTime))
          return this.breedresults[index].Ref_Info;
      }
      return (Parents_AndChild) null;
    }

    public void DrawBreedingRowsDisplay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      for (int index = 0; index < this.breedresults.Count; ++index)
        this.breedresults[index].DrawBreedResultAndOption(offset, spriteBatch);
      this.ParentsHeader.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.OffspringHeader.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
