// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.CategorySelection.CatHeading.CatHeadingManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using SEngine.Localization;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.OverWorld.OverWorldBuildMenu.CategorySelection.CatHeading
{
  internal class CatHeadingManager
  {
    private GameObject textobj;
    private string Name;
    public CATEGORYTYPE category;
    private ButtonRepeater repeater;
    private CatButton FacilitiesTabButton;
    private CatButton EnclosuresTabButton;
    private TRC_ButtonDisplay trcbutton;

    public CatHeadingManager()
    {
      this.textobj = new GameObject();
      this.textobj.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.textobj.vLocation = new Vector2(0.0f, -150f);
      this.textobj.SetAllColours(0.4666667f, 0.3333333f, 0.2235294f);
      this.category = CATEGORYTYPE.Enclosure;
      this.Name = CategoryData.GetCategoryToname(this.category);
      this.repeater = new ButtonRepeater();
      this.FacilitiesTabButton = new CatButton();
      this.FacilitiesTabButton.SetCategory(CATEGORYTYPE.Amenities);
      this.EnclosuresTabButton = new CatButton();
      this.EnclosuresTabButton.SetCategory(CATEGORYTYPE.Enclosure);
      this.FacilitiesTabButton.vLocation = new Vector2(23f, -15f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.EnclosuresTabButton.vLocation = new Vector2(8f, -15f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.trcbutton = new TRC_ButtonDisplay(3f);
      this.trcbutton.SetUpAnimation(new List<ControllerAnim>()
      {
        new ControllerAnim(ControllerButton.LB, TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, 1f),
        new ControllerAnim(ControllerButton.None, TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, 0.1f),
        new ControllerAnim(ControllerButton.RB, TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, 1f),
        new ControllerAnim(ControllerButton.None, TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, 0.1f)
      });
    }

    public bool UpdateCatHeadingManager(Player player, float DeltaTime, Vector2 Offset)
    {
      if (FeatureFlags.BlockPageCycleInBuild)
        return false;
      this.trcbutton.UpdateTRC_ButtonDisplay(DeltaTime);
      if (this.FacilitiesTabButton.UpdateCatButton(player, Offset + this.textobj.vLocation, DeltaTime) && this.category != CATEGORYTYPE.Amenities)
      {
        ThingToBuildManager.placetype = PlaceType.PlacingBuildings;
        this.category = CATEGORYTYPE.Amenities;
        return true;
      }
      if (this.EnclosuresTabButton.UpdateCatButton(player, Offset + this.textobj.vLocation, DeltaTime) && this.category != CATEGORYTYPE.Enclosure)
      {
        ThingToBuildManager.placetype = PlaceType.PlacingCellBlock;
        this.category = CATEGORYTYPE.Enclosure;
        return true;
      }
      DirectionPressed Direction;
      if (this.repeater.UpdateMenuRepeats(DeltaTime, out Direction, false, false, player.inputmap.HeldButtons[9], player.inputmap.HeldButtons[10]))
      {
        if (Direction == DirectionPressed.Left)
        {
          if (this.category > CATEGORYTYPE.Enclosure)
            --this.category;
          else
            this.category = CATEGORYTYPE.Factories;
        }
        else
        {
          ++this.category;
          if (this.category == CATEGORYTYPE.Count)
            this.category = CATEGORYTYPE.Enclosure;
        }
        return true;
      }
      this.Name = CategoryData.GetCategoryToname(this.category);
      return false;
    }

    public void PreDrawCatHeadingManager(Vector2 Offset)
    {
      if (FeatureFlags.BlockPageCycleInBuild)
        return;
      this.FacilitiesTabButton.vLocation = new Vector2(70f, -70f);
      this.FacilitiesTabButton.DrawCatButton(Offset + this.textobj.vLocation, this.category == CATEGORYTYPE.Amenities);
      this.EnclosuresTabButton.vLocation = new Vector2(0.0f, -70f);
      this.EnclosuresTabButton.DrawCatButton(Offset + this.textobj.vLocation, this.category == CATEGORYTYPE.Enclosure);
      if (!TinyZoo.GameFlags.IsUsingController)
        return;
      this.trcbutton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatch03, AssetContainer.TRC_Sprites, this.EnclosuresTabButton.vLocation + Offset + this.textobj.vLocation + new Vector2(60f, 5f * Sengine.ScreenRatioUpwardsMultiplier.Y), 0.6f);
    }

    public void DrawCatHeadingManager(Vector2 Offset)
    {
      this.textobj.vLocation.Y = -260f;
      this.textobj.vLocation.X = -90f;
      float num1 = 1f;
      float x = 30f;
      if (PlayerStats.language != Language.English)
      {
        num1 = TextFunctions.GetStringPercentageReScaledToSpecificLength(this.Name, "XXXXXXXXXX", AssetContainer.springFont, true);
        x = 10f;
      }
      if (PlayerStats.language == Language.French)
      {
        num1 = TextFunctions.GetStringPercentageReScaledToSpecificLength(this.Name, "XXXXXXXXXX", AssetContainer.springFont, true);
        x = 10f;
      }
      TextFunctions.DrawTextWithDropShadow(this.Name, 1f * num1, this.textobj.vLocation + Offset + new Vector2(x, -10f), this.textobj.GetColour(), this.textobj.fAlpha, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, false);
      int num2 = FeatureFlags.BlockPageCycleInBuild ? 1 : 0;
    }
  }
}
