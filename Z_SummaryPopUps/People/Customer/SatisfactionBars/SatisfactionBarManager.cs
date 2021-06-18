// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars.SatisfactionBarManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_Employees.QuickPick;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars
{
  internal class SatisfactionBarManager
  {
    private List<SatisfactionBarAndText> bars;
    public Vector2 Location;

    public SatisfactionBarManager(SimPerson simperson, bool IsEmployee, float BaseScale = 1f)
    {
      this.bars = new List<SatisfactionBarAndText>();
      if (IsEmployee)
      {
        for (int index = 0; index < 3; ++index)
          this.bars.Add(new SatisfactionBarAndText(simperson, (EmployeeSatisfactionType) index, BaseScale));
      }
      else
      {
        for (int index = 0; index < 6; ++index)
        {
          this.bars.Add(new SatisfactionBarAndText(simperson, (SatisfactionType) index, BaseScale));
          string TEXT = "DESCRIPTION OF STAT";
          switch (index)
          {
            case 0:
              TEXT = "Tired visitors will walk very slowly and need to sit down. While benches let a visitor recover energy, there are other ways to give them a zip in their step.";
              break;
            case 1:
              TEXT = "Build food shops all over the zoo to help keep your visitors from going hungry.";
              break;
            case 2:
              TEXT = "Drinks shops will keep your visitors from getting thirsty.";
              break;
            case 3:
              TEXT = "Visitors often feel the call of nature, but a visitor who drinks a lot or feels sick will hunt out a bathroom even more urgently.";
              break;
            case 4:
              TEXT = "How happy is this visitor with the variety and quality of the animals they have seen.";
              break;
            case 5:
              TEXT = "Is this visitor having fun, beyond just looking at the animals?";
              break;
          }
          this.bars[index].SetMouseOver(TEXT, BaseScale);
        }
      }
      this.SetLocations(BaseScale);
    }

    private void SetLocations(float BaseScale)
    {
      for (int index = 0; index < this.bars.Count; ++index)
        this.bars[index].Location.Y = (float) index * (this.bars[index].GetSize().Y + 5f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y);
    }

    public Vector2 GetBarSize() => this.bars[0].GetSize();

    public Vector2 GetSize(bool FactorInDifferentTextWidths = false)
    {
      float y = (float) ((double) this.bars[this.bars.Count - 1].Location.Y - (double) this.bars[0].Location.Y + (double) this.bars[0].GetHeight() * (double) Sengine.ScreenRatioUpwardsMultiplier.Y);
      float num = 0.0f;
      if (FactorInDifferentTextWidths)
      {
        for (int index = 0; index < this.bars.Count; ++index)
          num = Math.Max(this.bars[index].GetSize(true).X, num);
      }
      else
        num = this.bars[0].GetSize(true).X;
      return new Vector2(num, y);
    }

    public Vector2 GetOffsetFromTopLeft(bool FactorInDifferentTextWidths = false)
    {
      Vector2 zero = Vector2.Zero;
      zero.X = this.GetSize(FactorInDifferentTextWidths).X;
      zero.X -= this.bars[0].GetBarWidth() * 0.5f;
      zero.Y = this.bars[0].GetHeight() * 0.5f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      return zero;
    }

    public void PreviewChange(
      int index,
      float newvalue,
      Vector3 positiveColour,
      Vector3 negativeColour)
    {
      this.bars[index].PreviewChange(newvalue, positiveColour, negativeColour);
    }

    public void ApplyChange(int index) => this.bars[index].ApplyChange();

    public float GetValue(int index) => this.bars[index].Value;

    public void SetValue(int index, float val) => this.bars[index].Value = val;

    public void SetBarColour(int index, Vector3 colour, int layer = 0) => this.bars[index].SetBarColour(colour, layer);

    public SatisfactionBarManager(QuickEmployeeDescription emplyeedescription, float BaseScale = 1f)
    {
      this.bars = new List<SatisfactionBarAndText>();
      for (int index = 0; index < emplyeedescription.StoreEmployeeStatValues.Length; ++index)
      {
        this.bars.Add(new SatisfactionBarAndText(emplyeedescription, (StoreEmployeeStat) index, BaseScale));
        string TEXT = "EMPLOYEE STAT TEXT DESC HERE";
        switch (index)
        {
          case 0:
            TEXT = "The more work an employee does, the more experience they earn. Higher experience means faster work rates and happier customers.";
            break;
          case 1:
            TEXT = "Impolite employees will upset customers, and possibly other staff. It can be a good thing in some jobs however.";
            break;
          case 2:
            TEXT = "Work ethic combined with energy and experience helps define an employee's speed and work quality.";
            break;
          case 3:
            TEXT = "An employee low on energy will serve customers very slowly, schedule a break to pop them up.";
            break;
          case 4:
            TEXT = "Unhappy employees might leave their jobs eventually, but before that their politeness and work ethic will be reduced. Ensure fair and job pay promotions across the zoo to help keep job satisfaction up.";
            break;
        }
        this.bars[index].SetMouseOver(TEXT, BaseScale);
      }
      this.SetLocations(BaseScale);
    }

    public SatisfactionBarManager(PrisonerInfo animal, float BaseScale = 1f)
    {
      this.bars = new List<SatisfactionBarAndText>();
      for (int index = 0; index < 8; ++index)
      {
        this.bars.Add(new SatisfactionBarAndText(animal, (AnimalSatisfactionType) index, BaseScale));
        string TEXT = "I AM SOME TEXT";
        switch (index)
        {
          case 0:
            TEXT = "The overall contentment of this animal.";
            break;
          case 1:
            TEXT = "The bar is full when the animal has had enough to eat.";
            break;
          case 2:
            TEXT = "The bar is full when the animal has had enough to drink.";
            break;
          case 3:
            TEXT = "If the animal's environment is not clean, their hygiene will go down over time. Employ more zoo keepers to keep enclosures clean, and remove dead animals as fast as possible.";
            break;
          case 4:
            TEXT = "The general wellbeing of this animal from a physical perspective.";
            break;
          case 5:
            TEXT = "How closely matched is its enclosure type with the natural habitat of this animal.";
            break;
          case 6:
            TEXT = "Is this animal kept entertained by the things in their enclosure.";
            break;
          case 7:
            TEXT = "While an animal can have enough food, if the food is not perfectly balanced, they will lack the nutrition needed to be optimal.";
            break;
        }
        this.bars[index].SetMouseOver(TEXT, BaseScale);
      }
      this.SetLocations(BaseScale);
    }

    public void UpdateSatisfactionBarManager(Vector2 Offset, Player player)
    {
      Offset += this.Location;
      for (int index = 0; index < this.bars.Count; ++index)
        this.bars[index].UpdateSatisfactionBarAndText(player, Offset);
    }

    public void DrawSatisfactionBarManager(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.Location;
      for (int index = 0; index < this.bars.Count; ++index)
        this.bars[index].DrawSatisfactionBarAndText(spriteBatch, Offset);
    }

    public void DrawSatisfactionBarManager_InverseOrder(Vector2 Offset)
    {
      Offset += this.Location;
      for (int index = 0; index < this.bars.Count; ++index)
        this.bars[index].DrawSatisfactionBarAndText_InverseOrder(AssetContainer.pointspritebatchTop05, Offset);
    }
  }
}
