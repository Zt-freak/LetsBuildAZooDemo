// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ControllerLayouts.Controler_OverworldMatrix
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.Buttons;
using TinyZoo.Audio;
using TinyZoo.OverWorld;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.OverWorld.OverWorldStatus;
using TinyZoo.Tutorials;

namespace TinyZoo.Z_ControllerLayouts
{
  internal class Controler_OverworldMatrix
  {
    private ButtonRepeater repeater;
    private int SelectedIndex;

    public Controler_OverworldMatrix()
    {
      this.repeater = new ButtonRepeater();
      this.SelectedIndex = 1;
      this.SelectButton();
    }

    public void UpdateControler_OverworldUI(
      OverWOrldState overworldstate,
      float DeltaTime,
      OverwoldMainButtons overworldbuttons,
      Player player)
    {
      if (overworldstate != OverWOrldState.MainMenu)
        return;
      this.UpdateSideButtons(overworldbuttons, DeltaTime, player);
    }

    private void UpdateSideButtons(
      OverwoldMainButtons overworldbuttons,
      float DeltaTime,
      Player player)
    {
      if ((!overworldbuttons.IsLerpArrayComplete() ? 1 : (OverWorldManager.IsGameIntro ? 1 : 0)) != 0 || GameFlags.IsUsingController && SellUIManager.selectedtileandsell != null)
        return;
      this.IsThisButtonBlocked(this.SelectedIndex);
      DirectionPressed Direction;
      if (!this.repeater.UpdateMenuRepeats(DeltaTime, out Direction, player.inputmap.HeldButtons[3], player.inputmap.HeldButtons[4], player.inputmap.HeldButtons[5] && OverwoldMainButtons.Selected == -1, player.inputmap.HeldButtons[6] && OverwoldMainButtons.Selected == -1))
        return;
      switch (Direction)
      {
        case DirectionPressed.Up:
          int selectedIndex1 = this.SelectedIndex;
          this.SelectedIndex = this.GetNextAvailableIndex(Direction, this.SelectedIndex, overworldbuttons);
          int selectedIndex2 = this.SelectedIndex;
          if (selectedIndex1 == selectedIndex2)
            break;
          this.SelectButton();
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
          if (OverwoldMainButtons.Selected == -1)
            break;
          OverwoldMainButtons.SelectedNeed = 0;
          break;
        case DirectionPressed.Right:
          if (TutorialManager.currenttutorial != TUTORIALTYPE.None || StatsBarHolder.BarLerper != null && (double) StatsBarHolder.BarLerper.TargetValue != -1.0)
            break;
          ++OverwoldMainButtons.SelectedNeed;
          break;
        case DirectionPressed.Down:
          int selectedIndex3 = this.SelectedIndex;
          this.SelectedIndex = this.GetNextAvailableIndex(Direction, this.SelectedIndex, overworldbuttons);
          int selectedIndex4 = this.SelectedIndex;
          if (selectedIndex3 == selectedIndex4)
            break;
          this.SelectButton();
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
          break;
        case DirectionPressed.Left:
          if (TutorialManager.currenttutorial != TUTORIALTYPE.None || StatsBarHolder.BarLerper != null && (double) StatsBarHolder.BarLerper.TargetValue != -1.0 || OverwoldMainButtons.SelectedNeed <= 0)
            break;
          --OverwoldMainButtons.SelectedNeed;
          break;
      }
    }

    private int GetNextAvailableIndex(
      DirectionPressed direction,
      int currentIndex,
      OverwoldMainButtons overworldbuttons,
      bool AllowCycle = false)
    {
      int index = currentIndex;
      switch (direction)
      {
        case DirectionPressed.Up:
          while (index > 0)
          {
            --index;
            if (!FeatureFlags.GetIsThisSubIconBlocked(OverwoldMainButtons.textbuttons[index].buttontype))
              break;
          }
          if (index <= 0)
          {
            index = 0;
            break;
          }
          break;
        case DirectionPressed.Down:
          while (index < OverwoldMainButtons.textbuttons.Length - 1)
          {
            ++index;
            if (!FeatureFlags.GetIsThisSubIconBlocked(OverwoldMainButtons.textbuttons[index].buttontype))
              break;
          }
          break;
      }
      return index;
    }

    private bool IsThisButtonBlocked(int index)
    {
      if (index < 6)
      {
        if (FeatureFlags.GetIsThisSubIconBlocked((OverworldButtons) index))
          return true;
      }
      else if (index == 6 && !OverWorldManager.z_daynightmanager.AllowPressDayNightButton())
        return true;
      return false;
    }

    private void SelectButton()
    {
      OverwoldMainButtons.Selected = this.SelectedIndex;
      OverWorldManager.z_daynightmanager.SetControllerSelected(false);
    }
  }
}
