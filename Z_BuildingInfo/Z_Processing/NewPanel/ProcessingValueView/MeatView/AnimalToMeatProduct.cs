// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.MeatView.AnimalToMeatProduct
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.Z_BuildingInfo.Z_Processing.Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Processing;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.MeatView
{
  internal class AnimalToMeatProduct
  {
    public Vector2 location;
    private List<MeatWithNumber> foodIcons;
    private AnimalInFrame animalNotInFrame;
    private List<ZGenericText> symbols;
    private Vector2 size;

    public AnimalToMeatProduct(
      AnimalType animal,
      Player player,
      float BaseScale,
      bool SetAvailableState = true,
      CROPTYPE cropType = CROPTYPE.Count)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.animalNotInFrame = new AnimalInFrame(animal, AnimalType.None, TargetSize: uiScaleHelper.ScaleX(35f), FrameEdgeBuffer: uiScaleHelper.ScaleX(6f), BaseScale: BaseScale, HeadVariant: 0, croptype: cropType, DrawGrownPlant: true);
      this.animalNotInFrame.Location.X += this.animalNotInFrame.GetSize().X * 0.5f;
      this.size.X += this.animalNotInFrame.GetSize().X;
      this.size.X += defaultBuffer.X * 0.5f;
      this.symbols = new List<ZGenericText>();
      AnimalProductionList animalProductionList = cropType == CROPTYPE.Count ? PcessedMeatData.GetAnmalToBaseMeatType(animal) : ProcessedVeg.GetVegetableToOutput(cropType);
      this.foodIcons = new List<MeatWithNumber>();
      for (int index = 0; index < animalProductionList.animalfoodtypes.Length; ++index)
      {
        string _textToWrite = "+";
        if (index == 0)
          _textToWrite = "=";
        ZGenericText zgenericText = new ZGenericText(_textToWrite, BaseScale, _UseOnePointFiveFont: true);
        zgenericText.vLocation.X = this.size.X;
        zgenericText.vLocation.X += zgenericText.GetSize().X * 0.5f;
        this.size.X += zgenericText.GetSize().X;
        this.size.X += defaultBuffer.X * 0.5f;
        MeatWithNumber meatWithNumber = new MeatWithNumber(animalProductionList.animalfoodtypes[index], BaseScale);
        if (SetAvailableState)
          meatWithNumber.SmartSetIfUndiscoveredOrUnavailable(player);
        meatWithNumber.location.X = this.size.X;
        this.size.X += meatWithNumber.GetSize().X;
        this.size.X += defaultBuffer.X * 0.5f;
        this.foodIcons.Add(meatWithNumber);
        this.symbols.Add(zgenericText);
      }
      for (int index = 0; index < this.foodIcons.Count; ++index)
        this.size.Y = Math.Max(this.foodIcons[index].GetSize().Y, this.size.Y);
      this.size.Y = Math.Max(this.animalNotInFrame.GetSize().Y, this.size.Y);
    }

    public void SetTextColor(Vector3 color)
    {
      for (int index = 0; index < this.symbols.Count; ++index)
        this.symbols[index].SetAllColours(color);
    }

    public Vector2 GetSize() => this.size;

    public void DrawAnimalToMeatProduct(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.animalNotInFrame.JustDrawAnimal(offset, spriteBatch);
      for (int index = 0; index < this.foodIcons.Count; ++index)
        this.foodIcons[index].DrawMeatWithNumber(offset, spriteBatch);
      for (int index = 0; index < this.symbols.Count; ++index)
        this.symbols[index].DrawZGenericText(offset, spriteBatch);
    }
  }
}
