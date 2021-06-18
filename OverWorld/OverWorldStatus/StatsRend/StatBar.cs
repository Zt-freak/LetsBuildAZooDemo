// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldStatus.StatsRend.StatBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.Tile_Data;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.OverWorld.OverWorldStatus.StatsRend
{
  internal class StatBar
  {
    private GameObject Icon;
    private SimpleUIBar simpleUIBar;
    private string ResourceName;
    public Vector2 Location;
    private GameObject HEading;
    private string STRING_generating;
    private string STRING_Using;
    private GameObject Frame;
    private GameObject GenText;
    private Vector2 FrameScale;
    private TRC_ButtonDisplay contbuton;
    public ProductionType productiontype;

    public StatBar(ProductionType _productiontype)
    {
      this.contbuton = new TRC_ButtonDisplay(2f);
      this.contbuton.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, ControllerButton.XboxA);
      this.productiontype = _productiontype;
      this.Icon = new GameObject();
      this.Icon.DrawRect = TileStats.GetProductionTypeToRectangle(this.productiontype);
      this.Icon.SetDrawOriginToCentre();
      this.Icon.scale = 0.5f;
      this.simpleUIBar = new SimpleUIBar(45f);
      this.ResourceName = TileStats.GetProductionTypeToString(this.productiontype);
      this.HEading = new GameObject();
      this.HEading.scale = 2f;
      this.GenText = new GameObject();
      this.GenText.scale = 2f;
      this.Frame = new GameObject();
      this.Frame.DrawRect = new Rectangle(84, 278, 30, 30);
      this.Frame.SetDrawOriginToCentre();
      this.FrameScale = new Vector2(2f, 2f);
      this.Icon.vLocation.Y = -30f;
      this.Icon.vLocation.X = -35f;
      this.Icon.scale = 0.5f;
      this.STRING_generating = "Gen";
      this.STRING_Using = "using";
      this.simpleUIBar = (SimpleUIBar) null;
    }

    public bool UpdateStatBar(Player player, Vector2 Offset, float DeltaTime, int ArrayIndex)
    {
      if ((double) player.livestats.consumptionstatus.ConsumptionValues[(int) this.productiontype] != 0.0 || (double) player.livestats.consumptionstatus.GenerationValues[(int) this.productiontype] != 0.0)
      {
        float consumptionValue = player.livestats.consumptionstatus.ConsumptionValues[(int) this.productiontype];
        float generationValue = player.livestats.consumptionstatus.GenerationValues[(int) this.productiontype];
        if ((double) consumptionValue > (double) generationValue)
        {
          this.GenText.SetAllColours(ColourData.IconYellow);
          this.Icon.SetAllColours(ColourData.IconYellow);
          float TimeLength = 0.25f;
          if (TinyZoo.GameFlags.NoStrobe)
            TimeLength = 1f;
          this.Frame.ColourCycle(TimeLength, ColourData.FernRed.X, ColourData.FernRed.Y, ColourData.FernRed.Z, 1f, 1f, 1f);
          this.Frame.UpdateColours(DeltaTime);
          if (this.simpleUIBar != null)
            this.simpleUIBar.SetFullness(1f, generationValue / consumptionValue, true);
          this.STRING_generating = Math.Round(((double) generationValue / (double) consumptionValue - 1.0) * 100.0).ToString() + "%";
          this.STRING_generating = string.Concat((object) (int) ((double) generationValue - (double) consumptionValue));
        }
        else
        {
          this.Frame.SetAllColours(1f, 1f, 1f);
          this.GenText.SetAllColours(1f, 1f, 1f);
          this.Icon.SetAllColours(1f, 1f, 1f);
          if ((double) generationValue > 0.0)
          {
            float REd = consumptionValue / generationValue;
            if ((double) REd <= 1.0 && (double) REd > 0.949999988079071)
              REd = 0.95f;
            if (this.simpleUIBar != null)
              this.simpleUIBar.SetFullness(REd, 1f, false);
            if ((double) consumptionValue == 0.0)
            {
              this.STRING_generating = "100%";
            }
            else
            {
              int num = (int) Math.Round(((double) generationValue / (double) consumptionValue - 1.0) * 100.0);
              if (num > 999)
                num = 999;
              this.STRING_generating = num.ToString() + "%";
            }
            this.STRING_generating = "+" + (object) (int) ((double) generationValue - (double) consumptionValue);
          }
          else
          {
            this.STRING_generating = "100%";
            this.simpleUIBar.SetFullness(0.0f, 1f, false);
          }
          this.GenText.SetAllColours(ColourData.FernGreen);
          this.Icon.SetAllColours(ColourData.FernGreen);
        }
      }
      else
      {
        if (this.simpleUIBar != null)
          this.simpleUIBar.SetFullness(0.0f, 0.0f, false);
        this.STRING_generating = "0%";
      }
      Offset += this.Location;
      if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && (!TinyZoo.GameFlags.IsUsingController || !player.inputmap.PressedThisFrame[15]))
        return MathStuff.CheckPointCollision(true, Offset, this.FrameScale.X, (float) this.Frame.DrawRect.Width, (float) this.Frame.DrawRect.Height, player.player.touchinput.ReleaseTapArray[0]);
      return TinyZoo.GameFlags.IsUsingController && OverwoldMainButtons.Selected == -1 && (OverwoldMainButtons.SelectedNeed == ArrayIndex + 1 && player.inputmap.PressedThisFrame[0]);
    }

    public void SetForSummary(int WantsThisMuch) => this.STRING_generating = string.Concat((object) WantsThisMuch);

    public void DrawStatBar(Vector2 Offset, int ArrayIndex)
    {
      Offset += this.Location;
      this.Frame.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.FrameScale * Sengine.ScreenRatioUpwardsMultiplier);
      if (this.simpleUIBar != null)
      {
        this.Icon.vLocation.X = -14f;
        this.Icon.vLocation.Y = -17f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.Icon.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
        this.simpleUIBar.DrawSimpleUIBar(Offset, AssetContainer.pointspritebatchTop05);
      }
      this.Icon.vLocation.X = 0.0f;
      this.Icon.vLocation.Y = -4f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.Icon.scale = 1f;
      this.Icon.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
      TextFunctions.DrawTextWithDropShadow(this.STRING_generating, this.GenText.scale, Offset + new Vector2(-23f, (float) (5.0 + 7.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y)), this.GenText.GetColour(), this.HEading.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false);
      if (!TinyZoo.GameFlags.IsUsingController || OverwoldMainButtons.Selected != -1 || OverwoldMainButtons.SelectedNeed != ArrayIndex + 1)
        return;
      this.contbuton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.TRC_Sprites, Offset + new Vector2(-20f, -20f));
    }
  }
}
