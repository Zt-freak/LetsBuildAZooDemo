// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Animals.DetailFrame.AnimalVariantsAndDNA
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_BreedResult.VariantDiscovered;
using TinyZoo.Z_Collection.Animals.DetailFrame.Animal;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Collection.Animals.DetailFrame
{
  internal class AnimalVariantsAndDNA
  {
    public Vector2 location;
    private AnimalInFrameGrid animalsInFrames;
    private DNAIcon dnaIcon;
    private float IconBuffer;
    private ZGenericText header;
    private WishBone wishBone;
    private AnimalVariantMouseOverHintBox mouseoverHintBox;
    private int currentMouseOverVariantIndex;
    private AnimalType refAnimalType;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    private ZGenericText percentage;
    private ZGenericText percentageText;
    private float additionalTextSize;

    public AnimalVariantsAndDNA(
      AnimalType animalType,
      Player player,
      float _BaseScale,
      bool AddPercentageTextAtTheRight = false)
    {
      this.refAnimalType = animalType;
      this.BaseScale = _BaseScale;
      this.currentMouseOverVariantIndex = -1;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      float defaultXbuffer = this.scaleHelper.GetDefaultXBuffer();
      float defaultYbuffer = this.scaleHelper.GetDefaultYBuffer();
      float num1 = 0.0f;
      List<AnimalRenderDescriptor> animals = new List<AnimalRenderDescriptor>();
      for (int index = 0; index < 10; ++index)
        animals.Add(new AnimalRenderDescriptor(animalType, index, _IsUnlocked: (player.Stats.GetTotalOfThisVariantFound(animalType, index) > 0)));
      this.animalsInFrames = new AnimalInFrameGrid(this.BaseScale, 5, defaultXbuffer, defaultYbuffer, animals);
      Vector2 size1 = this.animalsInFrames.GetSize();
      this.animalsInFrames.location.Y = num1;
      this.dnaIcon = new DNAIcon(this.BaseScale, AddFrame: true);
      Vector2 size2 = this.dnaIcon.GetSize();
      this.IconBuffer = this.scaleHelper.ScaleX(15f);
      this.dnaIcon.vLocation.X += size1.X + size2.X * 0.5f + this.IconBuffer;
      this.dnaIcon.vLocation.Y += this.animalsInFrames.location.Y + size1.Y * 0.5f;
      this.dnaIcon.SetUpSimpleAnimation();
      this.wishBone = new WishBone(this.BaseScale);
      this.wishBone.vLocation = this.dnaIcon.vLocation;
      this.wishBone.vLocation.X -= size2.X * 0.5f;
      if (!AddPercentageTextAtTheRight)
        return;
      this.percentage = new ZGenericText(((int) Math.Floor((double) player.Stats.GetTotalVaiantsFound(animalType) / 10.0 * 100.0)).ToString() + "%", this.BaseScale, _UseOnePointFiveFont: true);
      this.percentageText = new ZGenericText("Discovered", this.BaseScale);
      this.percentage.vLocation = this.dnaIcon.vLocation;
      this.percentage.vLocation.X += this.dnaIcon.GetSize().X * 0.5f;
      float num2 = Math.Max(this.percentageText.GetSize().X, this.percentage.GetSize().X);
      this.additionalTextSize = defaultXbuffer + num2 * 0.5f;
      this.percentage.vLocation.X += this.additionalTextSize;
      this.additionalTextSize += num2 * 0.5f;
      this.percentageText.vLocation = this.percentage.vLocation;
      this.percentage.vLocation.Y -= this.percentage.GetSize().Y * 0.5f;
      this.percentageText.vLocation.Y += this.percentageText.GetSize().Y * 0.5f;
    }

    public Vector2 GetSize() => this.animalsInFrames.GetSize() + new Vector2(this.IconBuffer + this.dnaIcon.GetSize().X + this.additionalTextSize, 0.0f);

    public void UpdateAnimalVariantsAndDNA(
      Player player,
      float DeltaTime,
      Vector2 offset,
      bool UpdateForMouseOverBox = false)
    {
      offset += this.location;
      if (UpdateForMouseOverBox)
      {
        int num = this.animalsInFrames.UpdateForMouseOver(player, offset);
        bool MouseOvered = false;
        this.dnaIcon.UpdateDNAIcon(player, offset, DeltaTime, out MouseOvered);
        if (((num == -1 ? 0 : (this.currentMouseOverVariantIndex != num ? 1 : 0)) | (MouseOvered ? 1 : 0)) != 0)
        {
          Vector2 vector2 = Vector2.Zero;
          if (MouseOvered)
          {
            this.mouseoverHintBox = new AnimalVariantMouseOverHintBox(this.refAnimalType, player, -1, this.BaseScale, true);
            vector2 = new Vector2(0.0f, (float) ((double) this.dnaIcon.GetSize().Y * 0.5 + (double) this.mouseoverHintBox.GetSize().Y * 0.5));
            vector2.Y += this.scaleHelper.ScaleY(10f);
            this.mouseoverHintBox.location = this.dnaIcon.vLocation;
          }
          else
          {
            this.mouseoverHintBox = new AnimalVariantMouseOverHintBox(this.refAnimalType, player, num, this.BaseScale);
            this.currentMouseOverVariantIndex = num;
            vector2 = new Vector2(0.0f, (float) ((double) this.animalsInFrames.animalFrames[num].GetSize().Y * 0.5 + (double) this.mouseoverHintBox.GetSize().Y * 0.5));
            vector2.Y += this.scaleHelper.ScaleY(10f);
            this.mouseoverHintBox.location = this.animalsInFrames.animalFrames[num].Location;
          }
          if (MouseOvered || num < 5)
            this.mouseoverHintBox.location -= vector2;
          else if ((double) this.mouseoverHintBox.location.Y + (double) vector2.Y + (double) offset.Y + (double) this.mouseoverHintBox.GetSize().Y * 0.5 > 768.0)
            this.mouseoverHintBox.location -= vector2;
          else
            this.mouseoverHintBox.location += vector2;
        }
        else
        {
          if (num != -1)
            return;
          this.mouseoverHintBox = (AnimalVariantMouseOverHintBox) null;
          this.currentMouseOverVariantIndex = -1;
        }
      }
      else
        this.dnaIcon.UpdateAnimation(DeltaTime);
    }

    public void DrawAnimalVariantsAndDNA(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.header != null)
        this.header.DrawZGenericText(offset, spriteBatch);
      this.wishBone.DrawWishBone(offset, spriteBatch);
      this.animalsInFrames.DrawAnimalInFrameGrid(offset, spriteBatch);
      this.dnaIcon.DrawDNAIcon(offset, spriteBatch);
      if (this.mouseoverHintBox != null)
        this.mouseoverHintBox.DrawAnimalVariantMouseOverHintBox(offset, spriteBatch);
      if (this.percentage != null)
        this.percentage.DrawZGenericText(offset, spriteBatch);
      if (this.percentageText == null)
        return;
      this.percentageText.DrawZGenericText(offset, spriteBatch);
    }
  }
}
