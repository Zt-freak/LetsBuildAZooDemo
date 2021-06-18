// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.SpecificTuts.ProductionTypeInformation
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldBuildMenu;
using TinyZoo.Tile_Data;

namespace TinyZoo.Tutorials.SpecificTuts
{
  internal class ProductionTypeInformation
  {
    private SmartCharacterBox charactertextbox;
    private BlackOut blackout;
    private float Delay;
    private BuildingIcon buildingicon;
    private SmartCharacterBox charactertextboxbanker;
    private bool Exiting;

    public ProductionTypeInformation(ProductionType productiontype, Player player)
    {
      this.Delay = 0.3f;
      this.blackout = new BlackOut();
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmUpgrade, 0.4f, 1f);
      this.blackout.SetAlpha(false, 0.3f, 0.0f, 0.8f);
      this.blackout.SetAllColours(ColourData.IconOrange);
      float _Scale = 7f * Sengine.UltraWideSreenDownardsMultiplier;
      switch (productiontype)
      {
        case ProductionType.Power:
          this.buildingicon = new BuildingIcon(TILETYPE.PowerStation, _Scale);
          break;
        case ProductionType.LifeSupport:
          this.buildingicon = new BuildingIcon(TILETYPE.LifeSupport, _Scale);
          break;
        case ProductionType.Water:
          this.buildingicon = new BuildingIcon(TILETYPE.Water, _Scale);
          break;
        case ProductionType.Farm:
          this.buildingicon = new BuildingIcon(TILETYPE.Farm, _Scale);
          break;
        case ProductionType.FoodKitchen:
          this.buildingicon = new BuildingIcon(TILETYPE.KitchenZone, _Scale);
          break;
        case ProductionType.Janitor:
          this.buildingicon = new BuildingIcon(TILETYPE.Janitor, _Scale);
          break;
        case ProductionType.Medicine:
          this.buildingicon = new BuildingIcon(TILETYPE.Medical, _Scale);
          break;
        case ProductionType.Security:
          this.buildingicon = new BuildingIcon(TILETYPE.Security, _Scale);
          break;
        case ProductionType.Bank:
          this.buildingicon = new BuildingIcon(TILETYPE.Bank, _Scale);
          break;
        case ProductionType.Research:
          this.buildingicon = new BuildingIcon(TILETYPE.Research_PrisonPlanet, _Scale);
          break;
      }
      string productionTypeToString = TileStats.GetProductionTypeToString(productiontype);
      string str1 = productionTypeToString;
      float consumptionValue = player.livestats.consumptionstatus.ConsumptionValues[(int) productiontype];
      float generationValue = player.livestats.consumptionstatus.GenerationValues[(int) productiontype];
      string str2;
      if ((double) consumptionValue <= (double) generationValue)
      {
        str2 = productionTypeToString + "~You have enough of this in your prison!~";
      }
      else
      {
        this.blackout.SetAllColours(ColourData.FernRed);
        str2 = productionTypeToString + "~Your Prison needs more " + str1 + " to keep your current inmates happy.~";
      }
      this.charactertextbox = new SmartCharacterBox(str2 + "You require: " + (object) (int) consumptionValue + ", and are generating " + (object) (int) generationValue + ".", AnimalType.Administrator);
      this.buildingicon.vLocation = new Vector2(512f, 300f);
      this.buildingicon.SetAlpha(false, 0.3f, 0.0f, 1f);
      int EarningsWithoutMod;
      int AllMoneyIncludingWrongCell;
      int dailyEanings = player.prisonlayout.GetDailyEanings(true, out EarningsWithoutMod, out AllMoneyIncludingWrongCell, player);
      string FirstText = "You are earning $" + (object) dailyEanings + " for every rotation of the prison planet.";
      if (dailyEanings < EarningsWithoutMod && EarningsWithoutMod < AllMoneyIncludingWrongCell)
        FirstText = "You are earning $" + (object) dailyEanings + " for every rotation of the prison planet.~But if you build all the correct structures and move inmates to their correct habitats you could be earning $" + (object) AllMoneyIncludingWrongCell + ".";
      else if (dailyEanings < EarningsWithoutMod)
        FirstText = "You are earning $" + (object) dailyEanings + " for every rotation of the prison planet.~But if you build all the correct structures you could be earning $" + (object) EarningsWithoutMod + ".";
      else if (dailyEanings < AllMoneyIncludingWrongCell)
        FirstText = "You are earning $" + (object) dailyEanings + " for every rotation of the prison planet.~But if you put all inmates in their correct habitats, you could be earning $" + (object) AllMoneyIncludingWrongCell + ".";
      this.charactertextboxbanker = new SmartCharacterBox(FirstText, AnimalType.ShopKeeper);
      this.charactertextboxbanker.SetNewHeight(600f);
      if ((double) consumptionValue > (double) generationValue)
        this.charactertextbox.SetRed();
      if (dailyEanings >= EarningsWithoutMod)
        return;
      this.charactertextboxbanker.SetRed();
    }

    public bool UpdateProductionTypeInformation(ref float DeltaTime, Player player)
    {
      this.buildingicon.UpdateColours(DeltaTime);
      this.blackout.UpdateColours(DeltaTime);
      bool ForceContinue = false;
      if (((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 || player.inputmap.PressedThisFrame[7]) && (double) this.Delay <= 0.0)
      {
        ForceContinue = true;
        player.inputmap.PressedThisFrame[7] = false;
      }
      this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, !ForceContinue, ForceContinue);
      if (!this.Exiting && this.charactertextbox.Exiting)
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmUpgrade, 0.1f, -0.1f);
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
        this.Exiting = true;
      }
      if ((double) this.Delay > 0.0)
        this.Delay -= DeltaTime;
      else if (this.charactertextboxbanker.UpdateSmartCharacterBox(DeltaTime, player, !this.charactertextbox.Exiting, this.charactertextbox.Exiting))
      {
        player.inputmap.ClearAllInput(player);
        return true;
      }
      if (this.charactertextbox.Exiting && (double) this.buildingicon.fTargetAlpha != 0.0)
      {
        this.buildingicon.SetAlpha(true, 0.2f, 1f, 0.0f);
        this.blackout.SetAlpha(true, 0.2f, 1f, 0.0f);
      }
      player.inputmap.ClearAllInput(player);
      DeltaTime = 0.0f;
      return false;
    }

    public void DrawProductionTypeInformation()
    {
      this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      this.charactertextbox.DrawSmartCharacterBox();
      this.charactertextboxbanker.DrawSmartCharacterBox();
      this.buildingicon.DrawBuildingIcon(Vector2.Zero, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}
