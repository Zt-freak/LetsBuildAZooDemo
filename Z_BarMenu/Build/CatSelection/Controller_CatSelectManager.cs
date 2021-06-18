// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BarMenu.Build.CatSelection.Controller_CatSelectManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.Buttons;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.Z_BarMenu.Build.CatSelection
{
  internal class Controller_CatSelectManager
  {
    private ButtonRepeater repeater;

    public Controller_CatSelectManager() => this.repeater = new ButtonRepeater();

    public void UpdateController_CatSelectManager(
      Player player,
      float DeltaTime,
      MainBarManager mainbarmanager,
      CetSelectManager catmanager)
    {
      if (player.inputmap.HeldButtons[21])
      {
        catmanager.EnableControllerControls(true);
        mainbarmanager.EnableControllerControls(false);
        DirectionPressed Direction;
        if (!this.repeater.UpdateMenuRepeats(DeltaTime, out Direction, false, false, player.inputmap.HeldButtons[5], player.inputmap.HeldButtons[6]))
          return;
        if (Direction == DirectionPressed.Right)
        {
          catmanager.TryToCycleSelection(DirectionPressed.Right);
        }
        else
        {
          if (Direction != DirectionPressed.Left)
            return;
          catmanager.TryToCycleSelection(DirectionPressed.Left);
        }
      }
      else
      {
        mainbarmanager.EnableControllerControls(true);
        catmanager.EnableControllerControls(false);
      }
    }
  }
}
