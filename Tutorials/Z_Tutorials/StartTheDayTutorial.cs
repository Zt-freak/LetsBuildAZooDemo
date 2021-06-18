// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.Z_Tutorials.StartTheDayTutorial
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_Bus;
using TinyZoo.Z_DayNight;

namespace TinyZoo.Tutorials.Z_Tutorials
{
  internal class StartTheDayTutorial
  {
    private SmartCharacterBox charactertextbox;
    private Arrow arrow;
    private Vector2 ArrowLocation;
    private int StateCounter;
    private static bool StartedDay;
    internal static bool DoPanNow;

    public StartTheDayTutorial()
    {
      FeatureFlags.BlockIntake = true;
      FeatureFlags.BlockSettings = true;
      FeatureFlags.BlockPremiumStore_DEPRICATED = true;
      FeatureFlags.BlockBuild = true;
      FeatureFlags.BlockBreeding = true;
      FeatureFlags.BlockAllUI = false;
      FeatureFlags.BlockCash = true;
      FeatureFlags.BlockDayUI = false;
      FeatureFlags.BlockPersonInfo = true;
      this.StateCounter = 0;
      this.charactertextbox = new SmartCharacterBox("Now you have some animals! Let's earn some money!", AnimalType.Administrator, _ScaleMult: Sengine.UltraWideSreenDownardsMultiplier);
      this.charactertextbox.AddNewText(new textBoxPair("Use the button to open the zoo for the day.~Our bus will bring in some customers.", AnimalType.Administrator));
      StartTheDayTutorial.StartedDay = false;
    }

    internal static void PressedStartDay() => StartTheDayTutorial.StartedDay = true;

    public bool UpdateStartTheDay(float DeltaTime, Player player, ref float SimulationTime)
    {
      if (OverWorldManager.overworldstate == OverWOrldState.MainMenu)
      {
        if (this.StateCounter < 2)
        {
          this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player);
          if (this.charactertextbox.ThisLine == 1)
          {
            ++this.StateCounter;
            this.arrow = new Arrow(true);
            this.ArrowLocation = DayNightManager.BTNLocation + new Vector2(0.0f, -50f);
          }
        }
        else if (this.StateCounter == 2 && this.charactertextbox != null && this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, !StartTheDayTutorial.StartedDay, StartTheDayTutorial.StartedDay))
          this.charactertextbox = (SmartCharacterBox) null;
        if (this.arrow != null)
          this.arrow.UpdateArrow(DeltaTime);
        if (this.StateCounter == 2 && StartTheDayTutorial.StartedDay && CustomerManager.PeopleOutAndAbout > 0)
        {
          StartTheDayTutorial.DoPanNow = false;
          this.StateCounter = 3;
          this.charactertextbox = new SmartCharacterBox("The more animals you have, the more people will come to the zoo and buy tickets!", AnimalType.Administrator, _ScaleMult: Sengine.UltraWideSreenDownardsMultiplier);
          this.charactertextbox.AddNewText(new textBoxPair("Rarer animals allow you increase your ticket prices, but it's fine to make your zoo in any way you want to!", AnimalType.Administrator));
        }
        if (StartTheDayTutorial.StartedDay && this.arrow != null)
          this.arrow = (Arrow) null;
        if (StartTheDayTutorial.DoPanNow)
        {
          Vector2 gateLocation = TileMath.GetGateLocation();
          OverWorldEnvironmentManager.overworldcam.DoPan(new Vector3(Z_BusManager.busses[0].vLocation.X + 0.0f, gateLocation.Y, 1f), 0.8f, true, true, false);
        }
        if (this.StateCounter == 3)
        {
          SimulationTime = 0.0f;
          if (this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player))
          {
            FeatureFlags.BlockIntake = false;
            FeatureFlags.BlockSettings = false;
            FeatureFlags.BlockPremiumStore_DEPRICATED = false;
            FeatureFlags.BlockBuild = false;
            FeatureFlags.BlockBreeding = false;
            FeatureFlags.BlockBuild = true;
            FeatureFlags.BlockBreeding = true;
            FeatureFlags.BlockAllUI = false;
            FeatureFlags.BlockCash = false;
            FeatureFlags.BlockPersonInfo = false;
            FeatureFlags.BlockSpeedUp = false;
            return true;
          }
        }
      }
      return false;
    }

    public void DrawStartTheDay()
    {
      if (OverWorldManager.overworldstate != OverWOrldState.MainMenu)
        return;
      if (this.charactertextbox != null)
        this.charactertextbox.DrawSmartCharacterBox();
      if (this.arrow == null)
        return;
      this.arrow.Rotation = 1.570796f;
      this.ArrowLocation = DayNightManager.BTNLocation + new Vector2(0.0f, -30f);
      this.arrow.DrawArrow(this.ArrowLocation);
    }
  }
}
