// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.KeepSellView.KeepSellEditView
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.Z_BuildingInfo.Z_Processing.Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_GenericUI.Z_Scroll;
using TinyZoo.Z_Processing;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.KeepSellView
{
  internal class KeepSellEditView
  {
    public Vector2 location;
    private List<KeepSellRow> rows;
    private UIScaleHelper scaleHelper;
    private Vector2 size;
    private Z_ScrollHelper scrollHelper;

    public KeepSellEditView(
      bool isMeat_ElseCrops,
      Player player,
      float BaseScale,
      float forceThisWidth)
    {
      this.scaleHelper = new UIScaleHelper(BaseScale);
      List<AnimalFoodType> animalFoodTypeList = new List<AnimalFoodType>();
      this.SetUp(!isMeat_ElseCrops ? ProcessedVeg.GetDisplayListOfVegProcessorOutput() : PcessedMeatData.GetConvertableAnimalFoodTypesInDisplayOrder(), player, BaseScale, forceThisWidth);
    }

    public void AddScroll(float maxHeight) => this.scrollHelper = new Z_ScrollHelper(this.size, maxHeight);

    public Vector2 GetSize() => this.size;

    private void SetUp(
      List<AnimalFoodType> foodTypes,
      Player player,
      float BaseScale,
      float forceThisWidth)
    {
      Vector2 defaultBuffer = this.scaleHelper.DefaultBuffer;
      this.rows = new List<KeepSellRow>();
      for (int index = 0; index < foodTypes.Count; ++index)
      {
        KeepSellRow keepSellRow = new KeepSellRow(foodTypes[index], player, BaseScale, forceThisWidth);
        keepSellRow.location.Y = this.size.Y;
        keepSellRow.location += keepSellRow.GetSize() * 0.5f;
        this.size.Y += keepSellRow.GetSize().Y;
        if (index != foodTypes.Count - 1)
          this.size.Y += defaultBuffer.Y;
        this.rows.Add(keepSellRow);
      }
      this.size.X = this.rows[0].GetSize().X;
    }

    public void UpdateKeepSellEditView(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.scrollHelper != null)
      {
        this.scrollHelper.UpdateZ_ScrollHelper(player, offset);
        offset.Y += this.scrollHelper.YscrollOffset;
      }
      for (int index = 0; index < this.rows.Count; ++index)
        this.rows[index].UpdateKeepSellRow(player, DeltaTime, offset);
    }

    public void DrawKeepSellEditView(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.scrollHelper != null)
        offset.Y += this.scrollHelper.YscrollOffset;
      for (int index = 0; index < this.rows.Count; ++index)
      {
        bool flag = false;
        if (this.scrollHelper != null && !this.scrollHelper.CheckIfShouldDrawThis(this.rows[index].location.Y - this.rows[index].GetSize().Y * 0.5f, this.rows[index].location.Y + this.rows[index].GetSize().Y * 0.5f))
          flag = true;
        if (!flag)
          this.rows[index].DrawKeepSellRow(offset, spriteBatch);
      }
    }
  }
}
