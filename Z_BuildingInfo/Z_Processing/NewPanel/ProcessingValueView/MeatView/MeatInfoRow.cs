// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.MeatView.MeatInfoRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.Z_BuildingInfo.Z_Processing.Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Processing;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.MeatView
{
  internal class MeatInfoRow
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private MiniHeading miniHeading;
    private List<AnimalToMeatProduct> animalsToMeats;
    private Vector2 frameSize;

    public MeatInfoRow(
      AnimalFoodType meatType,
      Player player,
      float BaseScale,
      float ForcedWidth,
      bool IsCrops)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.customerFrame = new CustomerFrame(Vector2.Zero, CustomerFrameColors.DarkBrown, BaseScale);
      this.miniHeading = new MiniHeading(Vector2.Zero, AnimalFoodData.GetAnimalFoodTypeToString(meatType), 1f, BaseScale);
      this.animalsToMeats = new List<AnimalToMeatProduct>();
      List<AnimalType> animalTypeList = new List<AnimalType>();
      List<CROPTYPE> croptypeList = new List<CROPTYPE>();
      if (IsCrops)
      {
        for (int index = 0; index < 18; ++index)
        {
          if (index != 0 && ((IEnumerable<AnimalFoodType>) ProcessedVeg.GetVegetableToOutput((CROPTYPE) index).animalfoodtypes).Contains<AnimalFoodType>(meatType))
            croptypeList.Add((CROPTYPE) index);
        }
      }
      else
      {
        for (int index = 0; index < 56; ++index)
        {
          if (((IEnumerable<AnimalFoodType>) PcessedMeatData.GetAnmalToBaseMeatType((AnimalType) index).animalfoodtypes).Contains<AnimalFoodType>(meatType))
            animalTypeList.Add((AnimalType) index);
        }
      }
      this.frameSize = Vector2.Zero;
      this.frameSize.Y += defaultBuffer.Y + this.miniHeading.GetTextHeight(true);
      this.frameSize.X = defaultBuffer.X;
      if (IsCrops)
      {
        for (int index = 0; index < croptypeList.Count; ++index)
          this.animalsToMeats.Add(new AnimalToMeatProduct(AnimalType.None, player, BaseScale, cropType: croptypeList[index]));
      }
      else
      {
        for (int index = 0; index < animalTypeList.Count; ++index)
          this.animalsToMeats.Add(new AnimalToMeatProduct(animalTypeList[index], player, BaseScale));
      }
      foreach (AnimalToMeatProduct animalsToMeat in this.animalsToMeats)
      {
        animalsToMeat.location = this.frameSize;
        animalsToMeat.location.Y += animalsToMeat.GetSize().Y * 0.5f;
        this.frameSize.Y += animalsToMeat.GetSize().Y;
        this.frameSize.Y += defaultBuffer.Y;
      }
      this.frameSize.X = ForcedWidth;
      this.customerFrame.Resize(this.frameSize);
      this.miniHeading.SetTextPosition(this.frameSize);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      for (int index = 0; index < this.animalsToMeats.Count; ++index)
        this.animalsToMeats[index].location += vector2;
    }

    public Vector2 GetSize() => this.frameSize;

    public void UpdateMeatInfoRow()
    {
    }

    public void DrawMeatInfoRow(
      Vector2 offset,
      SpriteBatch spriteBatch,
      float cullBelowThis,
      float cullAboveThis)
    {
      offset += this.location;
      bool flag1 = false;
      if ((double) this.frameSize.Y > (double) cullBelowThis && 0.0 < (double) cullAboveThis)
      {
        this.customerFrame.VSCale.Y = cullBelowThis - cullAboveThis;
        this.customerFrame.location.Y = (float) (-(double) this.frameSize.Y * 0.5 + (double) this.customerFrame.VSCale.Y * 0.5) + cullAboveThis;
        flag1 = true;
      }
      else if ((double) this.frameSize.Y > (double) cullBelowThis)
      {
        this.customerFrame.VSCale.Y = cullBelowThis;
        this.customerFrame.location.Y = (float) (-(double) this.frameSize.Y * 0.5 + (double) this.customerFrame.VSCale.Y * 0.5);
      }
      else if (0.0 < (double) cullAboveThis)
      {
        this.customerFrame.VSCale.Y = this.frameSize.Y - cullAboveThis;
        this.customerFrame.location.Y = (float) ((double) this.frameSize.Y * 0.5 - (double) this.customerFrame.VSCale.Y * 0.5);
        flag1 = true;
      }
      else
      {
        this.customerFrame.VSCale.Y = this.frameSize.Y;
        this.customerFrame.location.Y = 0.0f;
      }
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      if (!flag1)
        this.miniHeading.DrawMiniHeading(offset, spriteBatch);
      for (int index = 0; index < this.animalsToMeats.Count; ++index)
      {
        bool flag2 = false;
        double num1 = (double) this.animalsToMeats[index].location.Y + (double) this.frameSize.Y * 0.5;
        float num2 = (float) (num1 - (double) this.animalsToMeats[index].GetSize().Y * 0.5);
        double num3 = num1 + (double) this.animalsToMeats[index].GetSize().Y * 0.5;
        if ((double) num2 > (double) cullBelowThis)
          flag2 = true;
        double num4 = (double) cullAboveThis;
        if (num3 < num4)
          flag2 = true;
        if (!flag2)
          this.animalsToMeats[index].DrawAnimalToMeatProduct(offset, spriteBatch);
      }
    }
  }
}
