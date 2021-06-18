// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ConfirmBreed.PotentialBabies
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BreedScreen.ConfirmBreed
{
  internal class PotentialBabies
  {
    private List<BabyAndPercent> babies;
    public Vector2 Location;
    private ZGenericText Heading;
    private OffspringPercentageBar offspringposibilityBar;
    public CustomerFrame customerFrame;

    public PotentialBabies(Parents_AndChild Pand, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float num1 = 0.0f + defaultYbuffer;
      this.Heading = new ZGenericText("Possible Offspring:", BaseScale, _UseOnePointFiveFont: true);
      float y1 = this.Heading.GetSize().Y;
      this.Heading.vLocation.Y = num1 + y1 * 0.5f;
      this.Heading.SetAllColours(ColourData.Z_Cream);
      float num2 = num1 + y1 + defaultYbuffer;
      this.offspringposibilityBar = new OffspringPercentageBar(Pand, BaseScale);
      this.offspringposibilityBar.Location.Y = num2 + this.offspringposibilityBar.GetSize().Y * 0.5f;
      float num3 = num2 + this.offspringposibilityBar.GetSize().Y + defaultYbuffer;
      this.babies = new List<BabyAndPercent>();
      this.babies.Add(new BabyAndPercent(Pand.FemaleParentVariant, Pand.animaltype, BaseScale));
      this.babies[0].Percent = 100;
      this.babies[0].SetFrameColour(ColourData.ThreeBluesForBabies[0]);
      if (Pand.FemaleParentVariant != Pand.MaleParentVariant)
      {
        this.babies.Add(new BabyAndPercent(Pand.MaleParentVariant, Pand.animaltype, BaseScale));
        this.babies[0].Percent = 50;
        this.babies[1].Percent = 50;
        this.babies[1].SetFrameColour(ColourData.ThreeBluesForBabies[1]);
      }
      if (Pand.ChildVariant != Pand.FemaleParentVariant && Pand.ChildVariant != Pand.MaleParentVariant)
      {
        this.babies.Add(new BabyAndPercent(Pand.ChildVariant, Pand.animaltype, BaseScale));
        int num4 = 100 - Pand.PercentChanceOfThisChild;
        if (this.babies.Count == 3)
        {
          int num5 = num4 / 2;
          this.babies[0].Percent = num5;
          this.babies[1].Percent = num5;
          this.babies[2].Percent = Pand.PercentChanceOfThisChild;
          if (num5 * 2 + Pand.PercentChanceOfThisChild != 100)
            ++this.babies[2].Percent;
          this.babies[1].SetFrameColour(ColourData.ThreeBluesForBabies[1]);
          this.babies[2].SetFrameColour(ColourData.ThreeBluesForBabies[2]);
        }
        else
        {
          this.babies[0].Percent = num4;
          this.babies[1].Percent = Pand.PercentChanceOfThisChild;
          this.babies[1].SetFrameColour(ColourData.ThreeBluesForBabies[2]);
        }
      }
      if (this.babies.Count == 2)
      {
        float num4 = (float) ((double) this.babies[0].GetSize().X * 0.5 + (double) defaultXbuffer * 0.5);
        this.babies[0].Location.X = -num4;
        this.babies[1].Location.X = num4;
      }
      else if (this.babies.Count == 3)
      {
        float num4 = this.babies[0].GetSize().X + defaultXbuffer;
        this.babies[0].Location.X = -num4;
        this.babies[2].Location.X = num4;
      }
      for (int index = 0; index < this.babies.Count; ++index)
        this.babies[index].Location.Y = num3 + this.babies[0].GetHeight() * 0.5f + this.babies[0].GetTextOffset();
      float y2 = num3 + (this.babies[0].GetHeight() + this.babies[0].GetTextOffset() * 0.5f) + defaultYbuffer;
      this.customerFrame = new CustomerFrame(new Vector2(uiScaleHelper.ScaleX(235f), y2), CustomerFrameColors.Brown, BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.Heading.vLocation.Y += vector2.Y;
      this.offspringposibilityBar.Location.Y += vector2.Y;
      for (int index = 0; index < this.babies.Count; ++index)
        this.babies[index].Location.Y += vector2.Y;
    }

    public void DrawPotentialBabies(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.customerFrame.DrawCustomerFrame(Offset, spritebatch);
      this.Heading.DrawZGenericText(Offset, spritebatch);
      this.offspringposibilityBar.DrawOffspringPercentageBar(Offset, spritebatch);
      for (int index = 0; index < this.babies.Count; ++index)
        this.babies[index].DrawBabyAndPercent(Offset, spritebatch);
    }
  }
}
