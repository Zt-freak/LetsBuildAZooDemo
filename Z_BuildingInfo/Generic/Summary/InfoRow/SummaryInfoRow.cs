// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Generic.Summary.InfoRow.SummaryInfoRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Processing;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.Z_BuildingInfo.Generic.Summary.InfoRow
{
  internal class SummaryInfoRow
  {
    public Vector2 location;
    private SummaryIcon icon;
    private ZGenericText text_one;
    private ZGenericText text_two;
    private OperationBar operationBar;
    private LittleSummaryButton infoButton;
    private Vector2 size;

    public SummaryInfoType refInfoType { get; private set; }

    public SummaryInfoRow(
      SummaryInfoType infoType,
      int BuildingUID,
      TILETYPE tileType,
      float BaseScale,
      float forcedWidth = -1f)
    {
      this.refInfoType = infoType;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      if ((double) forcedWidth == -1.0)
        forcedWidth = uiScaleHelper.ScaleX(250f);
      bool IsLocked = false;
      if (infoType == SummaryInfoType.FertilizerGenerated)
        IsLocked = true;
      this.icon = new SummaryIcon(this.refInfoType, BaseScale, tileType, IsLocked);
      this.icon.vLocation.X += this.icon.GetSize().X * 0.5f;
      this.size.X += this.icon.GetSize().X;
      this.size.X += defaultBuffer.X;
      string _textToWrite1 = string.Empty;
      string _textToWrite2 = string.Empty;
      bool flag1 = false;
      bool flag2 = false;
      switch (this.refInfoType)
      {
        case SummaryInfoType.ValueEarned:
          _textToWrite1 = "Total Net Value";
          _textToWrite2 = !TileData.IsAnIncinerator(tileType) ? "$?" : "-";
          break;
        case SummaryInfoType.AnimalsBurnt:
          _textToWrite1 = "Animals Burnt";
          _textToWrite2 = "?";
          flag2 = true;
          break;
        case SummaryInfoType.OperationWorkload:
          _textToWrite1 = "Operation Workload";
          _textToWrite2 = "?%";
          flag1 = true;
          flag2 = true;
          break;
        case SummaryInfoType.FertilizerGenerated:
          _textToWrite1 = "Locked";
          _textToWrite2 = "-";
          break;
        case SummaryInfoType.AnimalsOrCropsProcessed:
          _textToWrite1 = "Animals Processed";
          _textToWrite2 = "?";
          flag2 = true;
          break;
        case SummaryInfoType.ProductsProduced:
          switch (tileType)
          {
            case TILETYPE.MeatProcessor:
              _textToWrite1 = "Meat Produced";
              _textToWrite2 = "?";
              flag2 = true;
              break;
            case TILETYPE.FarmProcessor:
              _textToWrite1 = "Veggies Produced";
              _textToWrite2 = "?";
              flag2 = true;
              break;
            default:
              List<AnimalFoodType> outputFromFactory = PcessedMeatData.GetProductOutputFromFactory(tileType);
              for (int index = 0; index < outputFromFactory.Count; ++index)
              {
                if (index > 0)
                  _textToWrite1 += "/";
                _textToWrite1 += AnimalFoodData.GetAnimalFoodTypeToString(outputFromFactory[index]);
              }
              _textToWrite2 = "?";
              break;
          }
          break;
        case SummaryInfoType.ProductsSold:
          _textToWrite1 = "Products Sold";
          _textToWrite2 = "?";
          flag2 = true;
          break;
      }
      Vector2 size = this.size;
      this.text_one = new ZGenericText(_textToWrite1, BaseScale, false, _UseOnePointFiveFont: true);
      this.text_one.vLocation.X = this.size.X;
      this.text_one.vLocation.Y -= this.text_one.GetSize().Y * 0.5f;
      this.size.X += this.text_one.GetSize().X;
      this.text_two = new ZGenericText(_textToWrite2, BaseScale, false, true, true);
      this.text_two.vLocation.X = forcedWidth - defaultBuffer.X * 2f - uiScaleHelper.ScaleX(17f) - defaultBuffer.X;
      this.text_two.vLocation.Y -= this.text_two.GetSize().Y * 0.5f;
      if (flag1)
      {
        this.text_one.vLocation.Y -= this.text_one.GetSize().Y * 0.5f;
        Vector2 vLocation = this.text_one.vLocation;
        vLocation.Y += this.text_one.GetSize().Y;
        this.operationBar = new OperationBar(BaseScale);
        this.operationBar.location = vLocation;
        this.operationBar.SetValueAndColour(MathStuff.getRandomFloat(0.0f, 2f));
      }
      if (flag2)
      {
        this.infoButton = new LittleSummaryButton(LittleSummaryButtonType.BlueInfoCircle, _BaseScale: BaseScale);
        this.infoButton.vLocation.X = forcedWidth - defaultBuffer.X * 2f;
        this.infoButton.vLocation.X -= this.infoButton.GetSize().X * 0.5f;
      }
      float y = this.text_one.GetSize().Y;
      if (this.operationBar != null)
        y += this.operationBar.GetSize().Y;
      this.size.Y = Math.Max(this.icon.GetSize().Y, y);
      this.size.X = forcedWidth;
    }

    public Vector2 GetSize() => this.size;

    public bool UpdateSummaryInfoRow(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      return this.infoButton != null && this.infoButton.UpdateLittleSummaryButton(DeltaTime, player, offset);
    }

    public void DrawSummaryInfoRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.icon.DrawSummaryIcon(offset, spriteBatch);
      this.text_one.DrawZGenericText(offset, spriteBatch);
      this.text_two.DrawZGenericText(offset, spriteBatch);
      if (this.operationBar != null)
        this.operationBar.DrawOperationBar(offset, spriteBatch);
      if (this.infoButton == null)
        return;
      this.infoButton.DrawLittleSummaryButton(offset, spriteBatch);
    }
  }
}
