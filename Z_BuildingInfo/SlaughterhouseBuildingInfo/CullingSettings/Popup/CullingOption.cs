// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.Popup.CullingOption
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.Rows;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Employee;

namespace TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.Popup
{
  internal class CullingOption
  {
    public Vector2 location;
    private Vector2 size;
    private ZCheckBox checkBox;
    private ZGenericText heading;
    private SliderWithAverage dragBar;
    private ZGenericText dragBarNumber;
    private bool SomethingChanged;
    private float MinValue;
    private float MaxValue;

    public CullingType refCullingType { get; private set; }

    public CullingOption(CullingType cullingType, float BaseScale)
    {
      this.refCullingType = cullingType;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.checkBox = new ZCheckBox(BaseScale);
      string _textToWrite = string.Empty;
      bool flag = false;
      string averageText = "TODO";
      switch (cullingType)
      {
        case CullingType.None:
          _textToWrite = "None";
          break;
        case CullingType.Weight:
          _textToWrite = "Cull By Weight";
          flag = true;
          this.MinValue = 0.0f;
          this.MaxValue = 100f;
          averageText = "Average Weight";
          break;
        case CullingType.Age:
          _textToWrite = "Cull By Age";
          flag = true;
          this.MinValue = 0.0f;
          this.MaxValue = 100f;
          averageText = "Average Age";
          break;
      }
      this.heading = new ZGenericText(_textToWrite, BaseScale, false, _UseOnePointFiveFont: true);
      if (flag)
      {
        float initial0to1 = 0.5f;
        this.dragBar = new SliderWithAverage(BaseScale, initial0to1, uiScaleHelper.ScaleX(100f), average0to1: 0.5f, averageText: averageText);
        this.dragBarNumber = new ZGenericText(BaseScale, _UseOnePointFiveFont: true);
      }
      this.checkBox.location += this.checkBox.GetSize() * 0.5f;
      this.size.X += this.checkBox.GetSize().X;
      this.size.Y += this.checkBox.GetSize().Y * 0.5f;
      this.size.X += defaultBuffer.X;
      this.heading.vLocation = this.size;
      this.heading.vLocation.Y -= this.heading.GetSize().Y * 0.5f;
      this.size.Y = Math.Max(this.checkBox.GetSize().Y, this.heading.GetSize().Y);
      if (this.dragBar == null)
        return;
      this.size.X += uiScaleHelper.ScaleX(5f);
      this.dragBarNumber.vLocation = this.size;
      this.size.X += uiScaleHelper.ScaleX(35f);
      this.dragBar.location = this.size;
      this.dragBar.location += this.dragBar.GetSize() * 0.5f;
      this.dragBarNumber.vLocation.Y = this.dragBar.location.Y;
      this.size.Y += this.dragBar.GetSize().Y;
      this.SetString();
    }

    public Vector2 GetSize() => this.size;

    public void SetIsActive(bool isActive)
    {
      this.checkBox.SetTicked(isActive);
      if (this.dragBar == null)
        return;
      this.dragBar.SetIsActive(isActive);
    }

    public void CommitChangesToSave()
    {
      if (!this.SomethingChanged)
        return;
      double barSetValue = (double) this.GetBarSetValue();
    }

    public bool UpdateCullingOption(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.dragBar != null)
      {
        if (this.dragBar.UpdateSliderWithAverage(player, offset, DeltaTime))
          this.SomethingChanged = true;
        this.SetString();
      }
      if (!this.checkBox.UpdateCheckBox(player, offset))
        return false;
      this.SomethingChanged = true;
      return true;
    }

    private float GetBarSetValue() => (float) (int) Math.Floor((double) this.MinValue + (double) this.dragBar.Value * ((double) this.MaxValue - (double) this.MinValue));

    private void SetString()
    {
      float barSetValue = this.GetBarSetValue();
      if (this.refCullingType == CullingType.Weight)
      {
        this.dragBarNumber.textToWrite = barSetValue.ToString() + "kg";
      }
      else
      {
        if (this.refCullingType != CullingType.Age)
          return;
        this.dragBarNumber.textToWrite = barSetValue.ToString() + " Days";
      }
    }

    public void DrawCullingOption(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.checkBox.DrawCheckBox(spriteBatch, offset);
      this.heading.DrawZGenericText(offset, spriteBatch);
      if (this.dragBar == null)
        return;
      this.dragBar.DrawSliderWithAverage(spriteBatch, offset);
      this.dragBarNumber.DrawZGenericText(offset, spriteBatch);
    }
  }
}
