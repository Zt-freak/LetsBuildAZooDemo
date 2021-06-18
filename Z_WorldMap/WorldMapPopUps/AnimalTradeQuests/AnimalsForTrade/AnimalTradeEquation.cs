// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalsForTrade.AnimalTradeEquation
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalsForTrade
{
  internal class AnimalTradeEquation
  {
    public Vector2 location;
    private BreedMeIcon[] rewardPair;
    private AnimalInFrameGrid animalsInFrameGrid;
    private ZGenericText equalsSign;
    private UIScaleHelper scaleHelper;
    private float width;
    private float height;

    public AnimalTradeEquation(
      float BaseScale,
      AnimalType rewardAnimalPairType,
      params AnimalRenderDescriptor[] animalsNeeded)
    {
      this.scaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = this.scaleHelper.GetDefaultXBuffer();
      this.width = 0.0f;
      this.height = 0.0f;
      Vector2 vector2 = Vector2.Zero;
      if (animalsNeeded.Length != 0)
      {
        List<AnimalRenderDescriptor> list = ((IEnumerable<AnimalRenderDescriptor>) animalsNeeded).ToList<AnimalRenderDescriptor>();
        int numberPerRow = 8;
        int num1 = (int) Math.Ceiling((double) list.Count / (double) numberPerRow);
        if (num1 > 1)
        {
          int num2 = list.Count % numberPerRow;
          if (num2 != 0 && numberPerRow - num2 >= num1)
            numberPerRow -= (numberPerRow - num2) / num1;
        }
        this.animalsInFrameGrid = new AnimalInFrameGrid(BaseScale, numberPerRow, defaultXbuffer, this.scaleHelper.GetDefaultYBuffer(), list, centerJustify: true);
        vector2 = this.animalsInFrameGrid.GetSize();
        this.animalsInFrameGrid.location.X += vector2.X * 0.5f;
        this.width += vector2.X + defaultXbuffer;
        this.height += vector2.Y * 0.5f;
        this.equalsSign = new ZGenericText(" =", BaseScale, _UseOnePointFiveFont: true);
        this.equalsSign.vLocation = new Vector2(this.width, this.height);
        this.width += defaultXbuffer;
      }
      this.rewardPair = new BreedMeIcon[2];
      for (int index = 0; index < this.rewardPair.Length; ++index)
      {
        AnimalRenderDescriptor animal = new AnimalRenderDescriptor(rewardAnimalPairType);
        if (index == 1)
          animal.IsFemale = true;
        this.rewardPair[index] = new BreedMeIcon(animal, BaseScale);
        this.rewardPair[index].Location.X = this.width + this.rewardPair[index].GetOffsetToDrawFromLeft();
        this.rewardPair[index].Location.Y = this.height;
        if (vector2 == Vector2.Zero)
          this.rewardPair[index].Location.Y += this.rewardPair[index].GetSize().Y * 0.5f;
        this.width += this.rewardPair[index].GetSize().X + defaultXbuffer;
      }
      if (vector2 == Vector2.Zero)
        this.height += this.rewardPair[0].GetSize().Y;
      else
        this.height += vector2.Y * 0.5f;
    }

    public Vector2 GetSize() => new Vector2(this.width, this.height);

    public void UpdateAnimalTradeEquation(float DeltaTime, Vector2 offset) => offset += this.location;

    public void DrawAnimalTradeEquation(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.animalsInFrameGrid != null)
        this.animalsInFrameGrid.DrawAnimalInFrameGrid(offset, spriteBatch);
      if (this.equalsSign != null)
        this.equalsSign.DrawZGenericText(offset, spriteBatch);
      for (int index = 0; index < this.rewardPair.Length; ++index)
        this.rewardPair[index].DrawBreedMeIcon(offset, spriteBatch);
    }
  }
}
