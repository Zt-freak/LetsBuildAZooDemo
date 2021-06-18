// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ActiveBreedPairManage.Predict.Animal_Bar_Button
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BreedScreen.ActiveBreedPairManage.Nursing;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Pregnancy;

namespace TinyZoo.Z_BreedScreen.ActiveBreedPairManage.Predict
{
  internal class Animal_Bar_Button
  {
    private TextButton textButton;
    private AnimalInFrame animalframe;
    private float totalWidth;
    private PregnancyBar pregnancybar;
    private NursingBar nursingBar;
    public Vector2 location;

    public Animal_Bar_Button(
      string TextButtonString,
      Player player,
      ActiveBreed babyBreed = null,
      PrisonerInfo MotherForPregnancy = null,
      Parents_AndChild parents_and_child_FORNURSING = null,
      float baseScale = 1f)
    {
      float defaultXbuffer = new UIScaleHelper(baseScale).GetDefaultXBuffer();
      this.totalWidth = 0.0f;
      float TargetSize = 25f * baseScale;
      if (babyBreed != null || parents_and_child_FORNURSING != null && parents_and_child_FORNURSING.HeldBaby != null)
      {
        this.animalframe = babyBreed == null ? new AnimalInFrame(parents_and_child_FORNURSING.HeldBaby.intakeperson.animaltype, AnimalType.None, parents_and_child_FORNURSING.HeldBaby.intakeperson.CLIndex, TargetSize, 6f * baseScale, baseScale) : new AnimalInFrame(babyBreed.animalType, AnimalType.None, babyBreed.ChildType, TargetSize, 6f * baseScale, baseScale);
        this.animalframe.Location.X = this.totalWidth + this.animalframe.GetSize().X * 0.5f;
        this.totalWidth += this.animalframe.GetSize().X;
        this.totalWidth += defaultXbuffer;
      }
      float num1 = 90f;
      if (MotherForPregnancy != null)
      {
        this.totalWidth += defaultXbuffer * 0.5f;
        this.pregnancybar = new PregnancyBar(MotherForPregnancy, player, baseScale, BarWidth: num1);
        this.pregnancybar.Location.X = this.totalWidth + this.pregnancybar.GetWidth() * 0.5f;
        this.totalWidth += this.pregnancybar.GetWidth();
        this.totalWidth += defaultXbuffer;
        this.totalWidth += defaultXbuffer * 0.5f;
      }
      else if (parents_and_child_FORNURSING != null)
      {
        this.totalWidth += defaultXbuffer * 0.5f;
        this.nursingBar = new NursingBar(parents_and_child_FORNURSING, player, baseScale, num1);
        this.nursingBar.Location.X = this.totalWidth + this.nursingBar.GetSize().X * 0.5f;
        this.totalWidth += this.nursingBar.GetSize().X;
        this.totalWidth += defaultXbuffer;
        this.totalWidth += defaultXbuffer * 0.5f;
      }
      this.textButton = new TextButton(baseScale, TextButtonString, 40f);
      this.textButton.vLocation.X = this.totalWidth + this.textButton.GetSize_True().X * 0.5f;
      this.totalWidth += this.textButton.GetSize_True().X;
      float num2 = (float) (-(double) this.totalWidth * 0.5);
      if (this.animalframe != null)
        this.animalframe.Location.X += num2;
      if (this.pregnancybar != null)
        this.pregnancybar.Location.X += num2;
      if (this.nursingBar != null)
        this.nursingBar.Location.X += num2;
      this.textButton.vLocation.X += num2;
    }

    public float GetHeight()
    {
      double y = (double) this.textButton.GetSize_True().Y;
      float num1 = 0.0f;
      if (this.pregnancybar != null)
        num1 = this.pregnancybar.GetHeight();
      else if (this.nursingBar != null)
        num1 = this.nursingBar.GetSize().Y;
      float val2 = 0.0f;
      if (this.animalframe != null)
        val2 = this.animalframe.GetSize().Y;
      double num2 = (double) num1;
      return Math.Max(Math.Max((float) y, (float) num2), val2);
    }

    public bool UpdateAnimal_Bar_Button(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      return this.textButton.UpdateTextButton(player, offset, DeltaTime);
    }

    public void DrawAnimal_Bar_Button(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.location;
      if (this.animalframe != null)
        this.animalframe.DrawAnimalInFrame(Offset);
      if (this.textButton != null)
        this.textButton.DrawTextButton(Offset, 1f, spritebatch);
      if (this.pregnancybar != null)
        this.pregnancybar.DrawPregnancyBar(Offset, spritebatch);
      if (this.nursingBar == null)
        return;
      this.nursingBar.DrawNursingBar(Offset, spritebatch);
    }
  }
}
