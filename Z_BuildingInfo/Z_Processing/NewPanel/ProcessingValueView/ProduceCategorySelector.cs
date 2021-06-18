// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.ProduceCategorySelector
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView
{
  internal class ProduceCategorySelector
  {
    public Vector2 location;
    private List<LittleSummaryButton> buttons;
    private Vector2 size;
    private List<ProcessingViewType> buttonTypes;

    public ProduceCategorySelector(
      float BaseScale,
      List<ProcessingViewType> _buttonTypes,
      bool IsCrops)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.buttonTypes = _buttonTypes;
      this.buttons = new List<LittleSummaryButton>();
      bool flag = false;
      for (int index = 0; index < this.buttonTypes.Count; ++index)
      {
        LittleSummaryButtonType _Buttontype = LittleSummaryButtonType.Count;
        switch (this.buttonTypes[index])
        {
          case ProcessingViewType.Queue:
            _Buttontype = LittleSummaryButtonType.Processing_Queue;
            break;
          case ProcessingViewType.AnimalsOrCrops:
            _Buttontype = !IsCrops ? LittleSummaryButtonType.Processing_Animals : LittleSummaryButtonType.Processing_Crops;
            break;
          case ProcessingViewType.Products:
            _Buttontype = !IsCrops ? LittleSummaryButtonType.Processing_Meat : LittleSummaryButtonType.Processing_Veggies;
            break;
          case ProcessingViewType.KeepSell:
            _Buttontype = LittleSummaryButtonType.Processing_StoreRoom;
            if (true)
            {
              flag = true;
              break;
            }
            break;
          case ProcessingViewType.WarehouseMarket:
            _Buttontype = LittleSummaryButtonType.Manage;
            break;
          case ProcessingViewType.WarehouseStock:
            _Buttontype = LittleSummaryButtonType.Processing_StoreRoom;
            break;
        }
        this.buttons.Add(new LittleSummaryButton(_Buttontype, _BaseScale: BaseScale));
        if (flag)
          this.buttons[this.buttons.Count - 1].AddRedLight();
      }
      for (int index = 0; index < this.buttons.Count; ++index)
      {
        this.buttons[index].vLocation.X = this.size.X;
        this.buttons[index].vLocation.X += this.buttons[index].GetSize().X * 0.5f;
        this.size.X += this.buttons[index].GetSize().X + defaultBuffer.X;
      }
      this.size.Y += this.buttons[0].GetSize().Y;
    }

    public Vector2 GetSize() => this.size;

    public ProcessingViewType UpdateProduceCategorySelector(
      Player player,
      float DeltaTime,
      Vector2 offset)
    {
      offset += this.location;
      for (int index = 0; index < this.buttons.Count; ++index)
      {
        if (this.buttons[index].UpdateLittleSummaryButton(DeltaTime, player, offset))
        {
          if (this.buttonTypes[index] == ProcessingViewType.KeepSell)
            this.buttons[(int) this.buttonTypes[index]].RemoveRedLight();
          return this.buttonTypes[index];
        }
      }
      return ProcessingViewType.Count;
    }

    public void DrawProduceCategorySelector(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.buttons.Count; ++index)
        this.buttons[index].DrawLittleSummaryButton(offset, spriteBatch);
    }
  }
}
