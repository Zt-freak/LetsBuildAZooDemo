// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.StoreRoom.AnimalStuff.SummaryPanel.AnimalFoodSummaryPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_BalanceSystems.FoodDayCalc;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.StoreRoom.AnimalStuff.SummaryPanel
{
  internal class AnimalFoodSummaryPanel
  {
    public Vector2 Location;
    private BigBrownPanel mainpanel;
    private List<PenRow> penrows;
    private float BaseScale;
    private float Height;
    private float MaskValue;
    private float ExtraValueForTopRow;
    private CustomerFrame customerFrame;

    public AnimalFoodSummaryPanel(Player player, float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.MaskValue = 70f * this.BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.penrows = new List<PenRow>();
      this.mainpanel = new BigBrownPanel(Vector2.Zero, true, "Animal Food Supplies", this.BaseScale);
      this.Height = this.BaseScale * 10f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      float Width = 615f;
      double height = (double) this.Height;
      for (int index = 0; index < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index)
        player.prisonlayout.cellblockcontainer.prisonzones[index].prisonercontainer.SetUpTempAnimals(player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID, player.prisonlayout.cellblockcontainer.prisonzones[index].CellBLOCKTYPE, (Player) null);
      FoodDaysRemainingCalculator.CalculateFoodDaysRemainingCalculator(player);
      for (int index = 0; index < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index)
      {
        this.penrows.Add(new PenRow(player.prisonlayout.cellblockcontainer.prisonzones[index], this.BaseScale, ref this.Height, player, index == 0, Width));
        this.Height += 10f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
        if (index == 0)
          this.ExtraValueForTopRow = this.Height;
      }
      for (int index = 0; index < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index)
        this.penrows[index].Location.Y = this.Height * -0.5f;
      this.customerFrame = new CustomerFrame(new Vector2(uiScaleHelper.ScaleX(Width), Math.Min(this.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, 500f)), true, this.BaseScale);
      this.mainpanel.Finalize(this.customerFrame.VSCale);
    }

    public void GetScrollLimits(ref float MinMouseY)
    {
      MinMouseY = this.Height * -1f;
      MinMouseY -= this.ExtraValueForTopRow;
      MinMouseY += this.mainpanel.vScale.Y;
      if ((double) this.Height * (double) Sengine.ScreenRatioUpwardsMultiplier.Y < 500.0)
        MinMouseY = 0.0f;
      if ((double) MinMouseY <= (double) this.BaseScale * 125.0)
        return;
      MinMouseY = this.BaseScale * 125f;
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.Location;
      return this.mainpanel.CheckMouseOver(player, offset);
    }

    public bool UpdateAnimalFoodSummaryPanel(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool GoToDiet,
      out bool OrderFood,
      out PrisonZone prisonZone,
      out TempAnimalInfo lookAtThisAnimal,
      ref float YPos,
      float MouseScrollOffset)
    {
      offset += this.Location;
      this.mainpanel.UpdateDragger(player, ref this.Location, DeltaTime);
      GoToDiet = false;
      OrderFood = false;
      prisonZone = (PrisonZone) null;
      lookAtThisAnimal = (TempAnimalInfo) null;
      if (this.mainpanel.CheckMouseOverMask(offset, player, this.MaskValue, this.MaskValue))
      {
        for (int index = 0; index < this.penrows.Count; ++index)
          this.penrows[index].UpdateLerpersOnly(DeltaTime);
      }
      else
      {
        float MinDraw = this.mainpanel.vScale.Y * -0.5f + offset.Y;
        float MaxDraw = this.mainpanel.vScale.Y * 0.5f + offset.Y + this.mainpanel.InternalOffset.Y;
        Vector2 Offset = this.mainpanel.GetTop(offset) - new Vector2(0.0f, -this.MaskValue);
        Offset.Y += MouseScrollOffset;
        for (int index = 0; index < this.penrows.Count; ++index)
        {
          int PressedThis = this.penrows[index].UpdatePenRow(Offset, player, DeltaTime, ref YPos, MaxDraw, MinDraw);
          if (PressedThis > -1)
          {
            if (this.penrows[index].GoToDiet)
              GoToDiet = true;
            else
              OrderFood = true;
            prisonZone = this.penrows[index].REF_prison;
            lookAtThisAnimal = this.penrows[index].GetClickedAnimal(PressedThis);
            return true;
          }
        }
      }
      return this.mainpanel.UpdatePanelCloseButton(player, DeltaTime, offset);
    }

    public void DrawAnimalFoodSummaryPanel(
      Vector2 offset,
      float MouseScrollOffset,
      SpriteBatch spriteBatch,
      ref float YPos)
    {
      offset += this.Location;
      this.mainpanel.DrawBigBrownPanel(offset, spriteBatch);
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      Vector2 offset1 = offset;
      float MinDraw = this.mainpanel.vScale.Y * -0.5f + offset.Y;
      float MaxDraw = this.mainpanel.vScale.Y * 0.5f + offset.Y + this.mainpanel.InternalOffset.Y;
      Vector2 Offset = this.mainpanel.GetTop(offset1) - new Vector2(0.0f, -this.MaskValue);
      Offset.Y += MouseScrollOffset;
      for (int index = 0; index < this.penrows.Count; ++index)
        this.penrows[index].DrawPenRow(Offset, spriteBatch, ref YPos, MaxDraw, MinDraw);
      this.mainpanel.DrawBigBrownPanelTopMask(offset1, spriteBatch, this.MaskValue, this.MaskValue);
    }
  }
}
