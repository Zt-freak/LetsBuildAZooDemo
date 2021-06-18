// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.Z_Tutorials.Z_EmployZooKeeper
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI.OveWorldUI;
using TinyZoo.OverWorld;
using TinyZoo.PlayerDir;
using TinyZoo.Z_Manage;
using TinyZoo.Z_Manage.MainButtons;

namespace TinyZoo.Tutorials.Z_Tutorials
{
  internal class Z_EmployZooKeeper
  {
    private SmartCharacterBox charactertextbox;
    private int StateCounter;
    private Arrow arrow;
    private Vector2 ArrowLocation;
    private bool Started;

    public Z_EmployZooKeeper(Player player)
    {
      this.charactertextbox = new SmartCharacterBox("To run your zoo, you need more employees! Go to the zoo management panel and select Hire", AnimalType.Administrator, true, Sengine.UltraWideSreenDownardsMultiplier);
      this.charactertextbox.AddNewText(new textBoxPair("We usually only get a few applicants each week, every monday we refresh the list so you can see the latest hopeful candidates!", AnimalType.Administrator));
      this.charactertextbox.AddNewText(new textBoxPair("Eventually you will be able to advertise for the positions you want to fill, but for now we just get a random selection of applicants.", AnimalType.Administrator));
      this.charactertextbox.AddNewText(new textBoxPair("Select one of the zoo keepers to start the interview process!", AnimalType.Administrator));
      player.employees.potentialhires.ForceRandomHiresOnTutorial();
    }

    public bool UpdateZ_EmployZooKeeper(
      ref float SimulationTime,
      ref float DeltaTime,
      Player player)
    {
      if ((double) Z_GameFlags.DayTimer > 30.0)
      {
        SimulationTime = 0.0f;
        if (!this.Started)
        {
          FeatureFlags.BlockPersonInfo = true;
          this.Started = true;
          FeatureFlags.BlockSettings = true;
          FeatureFlags.BlockAlerts = true;
          FeatureFlags.BlockBreeding = true;
          FeatureFlags.BlockBuild = true;
          FeatureFlags.BlockIntake = true;
          FeatureFlags.BlockManage = true;
          this.arrow = new Arrow();
          this.ArrowLocation = new Vector2(1024f - OWCategoryButton.SizeBTN, OverwoldMainButtons.textbuttons[4].Location.Y);
        }
      }
      if (this.Started)
      {
        if (this.StateCounter == 0)
        {
          FeatureFlags.BlockManage = false;
          this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true, DoNotClearInput: true);
          if (TinyZoo.Game1.gamestate == GAMESTATE.OverWorld && OverWorldManager.overworldstate == OverWOrldState.Manage)
          {
            ++this.StateCounter;
            this.arrow = new Arrow();
            this.arrow.InvertPointer();
            FeatureFlags.BlockExitFromManage = true;
            FeatureFlags.DarkenAllButThisInMANAGE = ManageButtonType.Hiring;
            this.arrow.SetAllColours(new Vector3(0.3f, 0.7f, 0.3f));
            this.ArrowLocation = mainButtonsManager.managebuttons[0].Location;
            this.ArrowLocation.X += 110f * Sengine.ScreenRatioUpwardsMultiplier.Y;
          }
        }
        if (this.StateCounter == 1)
        {
          if (Z_ManageManager.currentstate == ManageButtonType.Hiring)
          {
            ++this.StateCounter;
            this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, ForceContinue: true);
          }
        }
        else if (this.StateCounter == 2)
        {
          this.charactertextbox = (SmartCharacterBox) null;
          ++this.StateCounter;
          this.arrow = (Arrow) null;
        }
        else if (this.StateCounter == 3 && player.employees.employees.Count > 0)
        {
          for (int index = 0; index < player.employees.employees.Count; ++index)
          {
            if (player.employees.employees[index].employeetype == EmployeeType.Keeper)
            {
              FeatureFlags.BlockPersonInfo = false;
              FeatureFlags.BlockBuild = false;
              FeatureFlags.BlockExitFromManage = false;
              FeatureFlags.DarkenAllButThisInMANAGE = ManageButtonType.Count;
              FeatureFlags.BlockSettings = false;
              FeatureFlags.BlockIntake = false;
              player.employees.potentialhires.ResetHires(player.employees.employees);
              return true;
            }
          }
        }
      }
      if (this.arrow != null)
        this.arrow.UpdateArrow(DeltaTime);
      return false;
    }

    public void DrawZ_EmployZooKeeper()
    {
      if (this.charactertextbox != null)
        this.charactertextbox.DrawSmartCharacterBox();
      if (this.arrow == null)
        return;
      this.arrow.DrawArrow(this.ArrowLocation);
    }
  }
}
