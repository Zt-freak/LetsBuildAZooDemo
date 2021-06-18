// Decompiled with JetBrains decompiler
// Type: TinyZoo.TitleScreen.PickSaveSlot.SaveSumary.New.Customization.GameCustomizationScreen
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI;

namespace TinyZoo.TitleScreen.PickSaveSlot.SaveSumary.New.Customization
{
  internal class GameCustomizationScreen
  {
    public bool Active;
    public bool IsCustomGame;
    public int[] Customizations;
    private List<CustomizationEntry> customisationoptions;
    private BackButton close;
    private float BaseScale;

    public GameCustomizationScreen(float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.close = new BackButton(true, BaseScale: this.BaseScale);
      this.customisationoptions = new List<CustomizationEntry>();
      float RowHeight = 60f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      int num1 = 0;
      float num2 = 45f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      for (int index = 0; index < 34; ++index)
      {
        if (index == 0)
        {
          this.customisationoptions.Add(new CustomizationEntry((CustomizationOption) index, this.BaseScale, RowHeight, false, "Starting Bonuses"));
          this.customisationoptions[this.customisationoptions.Count - 1].Location = new Vector2(512f, (float) ((double) RowHeight * (double) index + (double) num1 * (double) num2));
          this.customisationoptions[this.customisationoptions.Count - 1].Location.Y += 60f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
          ++num1;
        }
        else if (index == 2)
        {
          this.customisationoptions.Add(new CustomizationEntry((CustomizationOption) index, this.BaseScale, RowHeight, false, "Financial"));
          this.customisationoptions[this.customisationoptions.Count - 1].Location = new Vector2(512f, (float) ((double) RowHeight * (double) index + (double) num1 * (double) num2));
          this.customisationoptions[this.customisationoptions.Count - 1].Location.Y += 60f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
          ++num1;
        }
        else if (index == 14)
        {
          this.customisationoptions.Add(new CustomizationEntry((CustomizationOption) index, this.BaseScale, RowHeight, false, "Staff"));
          this.customisationoptions[this.customisationoptions.Count - 1].Location = new Vector2(512f, (float) ((double) RowHeight * (double) index + (double) num1 * (double) num2));
          this.customisationoptions[this.customisationoptions.Count - 1].Location.Y += 60f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
          ++num1;
        }
        else if (index == 19)
        {
          this.customisationoptions.Add(new CustomizationEntry((CustomizationOption) index, this.BaseScale, RowHeight, false, "Financial"));
          this.customisationoptions[this.customisationoptions.Count - 1].Location = new Vector2(512f, (float) ((double) RowHeight * (double) index + (double) num1 * (double) num2));
          this.customisationoptions[this.customisationoptions.Count - 1].Location.Y += 60f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
          ++num1;
        }
        else if (index == 23)
        {
          this.customisationoptions.Add(new CustomizationEntry((CustomizationOption) index, this.BaseScale, RowHeight, false, "Guests"));
          this.customisationoptions[this.customisationoptions.Count - 1].Location = new Vector2(512f, (float) ((double) RowHeight * (double) index + (double) num1 * (double) num2));
          this.customisationoptions[this.customisationoptions.Count - 1].Location.Y += 60f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
          ++num1;
        }
        else if (index == 27)
        {
          this.customisationoptions.Add(new CustomizationEntry((CustomizationOption) index, this.BaseScale, RowHeight, false, "Farms"));
          this.customisationoptions[this.customisationoptions.Count - 1].Location = new Vector2(512f, (float) ((double) RowHeight * (double) index + (double) num1 * (double) num2));
          this.customisationoptions[this.customisationoptions.Count - 1].Location.Y += 60f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
          ++num1;
        }
        else if (index == 31)
        {
          this.customisationoptions.Add(new CustomizationEntry((CustomizationOption) index, this.BaseScale, RowHeight, false, "General"));
          this.customisationoptions[this.customisationoptions.Count - 1].Location = new Vector2(512f, (float) ((double) RowHeight * (double) index + (double) num1 * (double) num2));
          this.customisationoptions[this.customisationoptions.Count - 1].Location.Y += 60f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
          ++num1;
        }
        this.customisationoptions.Add(new CustomizationEntry((CustomizationOption) index, this.BaseScale, RowHeight, (num1 + index) % 2 == 0));
        this.customisationoptions[this.customisationoptions.Count - 1].Location = new Vector2(512f, (float) ((double) RowHeight * (double) index + (double) num1 * (double) num2));
        this.customisationoptions[this.customisationoptions.Count - 1].Location.Y += 60f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      }
    }

    public void Activate()
    {
      this.Active = true;
      this.close = new BackButton(BaseScale: this.BaseScale);
    }

    public void UpdateGameCustomizationScreen(Vector2 Offset, Player player, float DeltaTime)
    {
      if (!this.Active)
        return;
      if (player.inputmap.ReleasedThisFrame[7])
        this.Active = false;
      else if (this.close.UpdateBackButton(player, DeltaTime))
      {
        this.Active = false;
      }
      else
      {
        for (int index = 0; index < this.customisationoptions.Count; ++index)
          this.customisationoptions[index].UpdateCustomizationEntry(Offset, player, DeltaTime);
      }
    }

    public void DrawGameCustomizationScreen(Vector2 Offset)
    {
      if (!this.Active)
        return;
      for (int index = 0; index < this.customisationoptions.Count; ++index)
        this.customisationoptions[index].DrawCustomizationEntry(Offset);
      this.close.DrawBackButton(Offset);
    }
  }
}
