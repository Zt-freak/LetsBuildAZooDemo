// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BarMenu.Build.CatSelection.CetSelectManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System.Collections.Generic;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.CatSelector;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.Z_BarMenu.Build.CatSelection
{
  internal class CetSelectManager
  {
    private Z_CatButtons catButtons;
    private Vector2 catButtonOffset;
    public Vector2 location;
    private bool isActive_Controller;
    private TRC_ButtonDisplay DpadHint;
    private TRC_ButtonDisplay TriggerShiftHint;
    private LerpHandler_Float alphaLerper;

    public CetSelectManager(Player player)
    {
      this.catButtons = new Z_CatButtons(ColourData.Z_Cream, player, 20);
      this.catButtonOffset = new Vector2(85f * TinyZoo.GameFlags.GetTRCButtonScale(), 37f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.catButtonOffset.X = 0.0f;
      this.alphaLerper = new LerpHandler_Float();
      this.DpadHint = new TRC_ButtonDisplay(TinyZoo.GameFlags.GetTRCButtonScale());
      this.DpadHint.SetUpAnimation(new List<ControllerAnim>()
      {
        new ControllerAnim(ControllerButton.DPadLeft, TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, 1f),
        new ControllerAnim(ControllerButton.DpadNeutral, TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, 0.1f),
        new ControllerAnim(ControllerButton.DpadRight, TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, 1f),
        new ControllerAnim(ControllerButton.DpadNeutral, TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, 0.1f)
      });
      this.DpadHint.Position = new Vector2(0.0f, 0.0f);
      this.TriggerShiftHint = new TRC_ButtonDisplay(TinyZoo.GameFlags.GetTRCButtonScale());
      this.SetTriggerShiftHint();
      this.TriggerShiftHint.Position = new Vector2(327f, 0.0f);
      this.TriggerShiftHint.Position.X += this.catButtonOffset.X;
    }

    private void SetTriggerShiftHint(bool isPressed = false)
    {
      if (isPressed)
      {
        this.TriggerShiftHint.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, ControllerButton.RT, ispressed: true);
        this.TriggerShiftHint.SetAlpha(0.7f);
      }
      else
      {
        this.TriggerShiftHint.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, ControllerButton.RT);
        this.TriggerShiftHint.SetAlpha(1f);
      }
    }

    public bool CheckMouseOver(Player player, Vector2 Offset, float ScaleMultiplier)
    {
      ScaleMultiplier = 1f;
      return this.catButtons.CheckMouseOver(player, this.location + this.catButtonOffset + Offset, ScaleMultiplier);
    }

    public CATEGORYTYPE UpdateCetSelectManager(
      Player player,
      float DeltaTime,
      CATEGORYTYPE selectedCategory,
      Vector2 Offset)
    {
      this.alphaLerper.UpdateLerpHandler(DeltaTime);
      this.DpadHint.UpdateTRC_ButtonDisplay(DeltaTime);
      this.TriggerShiftHint.UpdateTRC_ButtonDisplay(DeltaTime);
      return this.catButtons.UpdateZ_CatButtons(player, DeltaTime, this.location + this.catButtonOffset + Offset, selectedCategory);
    }

    public void TryToCycleSelection(DirectionPressed dir) => this.catButtons.TryToCycleSelection(dir);

    public void EnableControllerControls(bool isEnable)
    {
      if (this.isActive_Controller != isEnable)
      {
        this.SetTriggerShiftHint(isEnable);
        if (isEnable)
          this.alphaLerper.SetLerp(false, 0.0f, 1f, 3f);
        else
          this.alphaLerper.SetLerp(false, 1f, 0.0f, 3f);
      }
      this.isActive_Controller = isEnable;
    }

    public void DrawCetSelectManager(Vector2 offset, CATEGORYTYPE selectedCategory)
    {
      if (TinyZoo.GameFlags.IsUsingController)
      {
        this.TriggerShiftHint.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.TRC_Sprites, this.location + offset);
        this.DpadHint.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.TRC_Sprites, this.location + offset, this.alphaLerper.Value);
      }
      this.catButtons.DrawZ_CatButtons(this.location + offset + this.catButtonOffset, selectedCategory);
    }
  }
}
