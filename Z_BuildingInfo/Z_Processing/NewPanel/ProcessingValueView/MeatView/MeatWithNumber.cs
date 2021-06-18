// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.MeatView.MeatWithNumber
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Processing;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_StoreRoom.Shelves;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.MeatView
{
  internal class MeatWithNumber
  {
    public Vector2 location;
    private AnimalFoodIcon animalFoodIcon;
    private ZGenericText numberText;
    private Vector2 size;
    private ZGenericText perKg_subText;
    private ZGenericText nameText;
    private ZGenericText smallNameText;

    public MeatWithNumber(
      AnimalFoodType foodType,
      float BaseScale,
      int Number = -1,
      bool IsPerKg = false,
      bool AddNameTextOnRight = false,
      bool ForceConsistentSize = false,
      bool AddSmallNameBelow = false)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 vector2_1 = uiScaleHelper.DefaultBuffer * 0.5f;
      this.animalFoodIcon = new AnimalFoodIcon(foodType, 1f, BaseScale);
      if (Number > -1)
      {
        this.numberText = new ZGenericText("x" + (object) Number, BaseScale, false, _UseOnePointFiveFont: true);
        if (IsPerKg)
          this.perKg_subText = new ZGenericText("per Kg", BaseScale, false);
      }
      if (AddNameTextOnRight)
        this.nameText = new ZGenericText(AnimalFoodData.GetAnimalFoodTypeToString(foodType), BaseScale, false, _UseOnePointFiveFont: true);
      if (AddSmallNameBelow)
        this.smallNameText = new ZGenericText(AnimalFoodData.GetAnimalFoodTypeToString(foodType), BaseScale);
      Vector2 vector2_2 = this.animalFoodIcon.GetSize_IconOnly();
      if (ForceConsistentSize)
        vector2_2 = uiScaleHelper.ScaleVector2(new Vector2(40f, 30f));
      this.animalFoodIcon.vLocation.X += vector2_2.X * 0.5f;
      this.size = vector2_2;
      this.size.X += vector2_1.X;
      if (this.nameText != null)
      {
        this.nameText.vLocation.X = this.size.X;
        this.nameText.vLocation.Y -= this.nameText.GetSize().Y * 0.5f;
        this.size.X += this.nameText.GetSize().X;
      }
      if (this.numberText != null)
      {
        this.numberText.vLocation.X += this.size.X;
        this.numberText.vLocation.Y -= this.numberText.GetSize().Y * 0.5f;
        if (this.perKg_subText != null)
        {
          this.perKg_subText.vLocation = this.numberText.vLocation;
          this.perKg_subText.vLocation.Y += this.numberText.GetSize().Y;
          this.size.X += Math.Max(this.numberText.GetSize().X, this.perKg_subText.GetSize().X);
        }
        else
          this.size.X += this.numberText.GetSize().X;
      }
      if (this.smallNameText == null)
        return;
      this.size.Y = uiScaleHelper.ScaleY(20f);
      this.smallNameText.vLocation = this.size;
      this.smallNameText.vLocation.X -= this.animalFoodIcon.GetSize_IconOnly().X * 0.5f;
      this.size.Y += this.smallNameText.GetSize().Y;
    }

    public void SetIsUndiscovered() => this.animalFoodIcon.SetIsUndiscovered();

    public void SetIsUnavailable() => this.animalFoodIcon.SetIsUnavailable();

    public void SmartSetIfUndiscoveredOrUnavailable(Player player)
    {
      int numberBuilt;
      if (PcessedMeatData.HasPlayerResearchedBuildingToProcessThisProduce(this.animalFoodIcon.refFoodType, player, out numberBuilt))
      {
        if (numberBuilt == -1 || numberBuilt != 0)
          return;
        this.animalFoodIcon.SetIsUnavailable();
      }
      else
      {
        this.animalFoodIcon.SetIsUndiscovered();
        if (this.nameText == null)
          return;
        this.nameText.textToWrite = "Unknown";
      }
    }

    public Vector2 GetSize() => this.size;

    public void DrawMeatWithNumber(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.animalFoodIcon.DrawAnimalFoodIcon(offset, false, spriteBatch);
      if (this.numberText != null)
      {
        this.numberText.DrawZGenericText(offset, spriteBatch);
        if (this.perKg_subText != null)
          this.perKg_subText.DrawZGenericText(offset, spriteBatch);
      }
      if (this.nameText != null)
        this.nameText.DrawZGenericText(offset, spriteBatch);
      if (this.smallNameText == null)
        return;
      this.smallNameText.DrawZGenericText(offset, spriteBatch);
    }
  }
}
