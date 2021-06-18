// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.MeatView.ProcessingMeatInfoView
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

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.MeatView
{
  internal class ProcessingMeatInfoView
  {
    public Vector2 location;
    private List<MeatInfoRow> meatRows;
    private Z_ScrollHelper scrollHelper;
    private Vector2 size;
    private Vector2 buffer;

    public ProcessingMeatInfoView(Player player, float BaseScale, float ForcedWidth, bool IsCrops)
    {
      this.buffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.meatRows = new List<MeatInfoRow>();
      List<AnimalFoodType> animalFoodTypeList = !IsCrops ? PcessedMeatData.GetConvertableAnimalFoodTypesInDisplayOrder() : ProcessedVeg.GetDisplayListOfVegProcessorOutput();
      for (int index = 0; index < animalFoodTypeList.Count; ++index)
      {
        MeatInfoRow meatInfoRow = new MeatInfoRow(animalFoodTypeList[index], player, BaseScale, ForcedWidth, IsCrops);
        meatInfoRow.location.Y = this.size.Y;
        meatInfoRow.location.Y += meatInfoRow.GetSize().Y * 0.5f;
        this.size.Y += meatInfoRow.GetSize().Y;
        if (index != animalFoodTypeList.Count - 1)
          this.size.Y += this.buffer.Y;
        this.meatRows.Add(meatInfoRow);
      }
      this.size.X = this.meatRows[0].GetSize().X;
    }

    public Vector2 GetSize() => this.size;

    public void AddScroll(float maxHeight) => this.scrollHelper = new Z_ScrollHelper(this.size, maxHeight, 0.5f);

    public void UpdateProcessingMeatInfoView(Player player, Vector2 offset)
    {
      offset += this.location;
      if (this.scrollHelper != null)
      {
        this.scrollHelper.UpdateZ_ScrollHelper(player, offset + new Vector2((float) (-(double) this.size.X * 0.5), 0.0f));
        offset.Y += this.scrollHelper.YscrollOffset;
      }
      for (int index = 0; index < this.meatRows.Count; ++index)
        this.meatRows[index].UpdateMeatInfoRow();
    }

    public void DrawProcessingMeatInfoView(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.scrollHelper != null)
        offset.Y += this.scrollHelper.YscrollOffset;
      for (int index = 0; index < this.meatRows.Count; ++index)
      {
        bool flag = false;
        float cullBelowThis = 0.0f;
        float cullAboveThis = 0.0f;
        if (this.scrollHelper != null)
        {
          Vector2 size = this.meatRows[index].GetSize();
          float TopLocation = this.meatRows[index].location.Y - size.Y * 0.5f;
          float BottomLocation = this.meatRows[index].location.Y + size.Y * 0.5f;
          if (!this.scrollHelper.CheckIfShouldDrawThis(TopLocation, BottomLocation))
          {
            flag = true;
          }
          else
          {
            cullBelowThis = this.scrollHelper.maxHeight - TopLocation - this.scrollHelper.YscrollOffset + this.buffer.Y;
            cullAboveThis = 0.0f - TopLocation - this.scrollHelper.YscrollOffset - this.buffer.Y;
          }
        }
        if (!flag)
          this.meatRows[index].DrawMeatInfoRow(offset, spriteBatch, cullBelowThis, cullAboveThis);
      }
    }
  }
}
