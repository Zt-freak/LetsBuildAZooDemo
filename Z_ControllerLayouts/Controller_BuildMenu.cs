// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ControllerLayouts.Controller_BuildMenu
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.Buttons;
using SEngine.Input;
using TinyZoo.OverWorld.OverWorldBuildMenu;
using TinyZoo.Z_BuldMenu.CatSelector;
using TinyZoo.Z_BuldMenu.IconGrid;

namespace TinyZoo.Z_ControllerLayouts
{
  internal class Controller_BuildMenu
  {
    private ButtonRepeater CategoryRepeater;
    private ControllerGridNavMatrix gridmatrix;
    private int Selected;
    private bool ForceSelected;

    public Controller_BuildMenu()
    {
      this.CategoryRepeater = new ButtonRepeater();
      this.SelectNewIconTab(BIconAndCost.PerRow, BIconAndCost.Total, 0);
    }

    public void SelectNewIconTab(int _TotalPerRow, int _Total, int ForceSelectThis = -1)
    {
      this.Selected = ForceSelectThis;
      this.gridmatrix = new ControllerGridNavMatrix(_TotalPerRow, _Total, ForceSelectThis);
      this.ForceSelected = false;
    }

    public void SelectedBuildIcon(int SelectedThis) => this.gridmatrix.SelectedIndex = SelectedThis;

    public void UpdateController_BuildMenu(
      Player player,
      float DeltaTime,
      Z_CatSelect catselect,
      Z_IconPanel z_iconpanel)
    {
      DirectionPressed Direction;
      if (this.CategoryRepeater.UpdateMenuRepeats(DeltaTime, out Direction, false, false, player.inputmap.HeldButtons[9], player.inputmap.HeldButtons[10]))
        catselect.TryToCycleSelection(Direction);
      if (GameFlags.IsUsingController && !this.ForceSelected)
      {
        this.ForceSelected = true;
        if (this.gridmatrix.SelectedIndex == -1)
          this.gridmatrix.SelectedIndex = 0;
        player.inputmap.QuickCenter();
        z_iconpanel.TryToSelectFromController(this.gridmatrix.SelectedIndex);
      }
      if (!this.gridmatrix.UpdateGridNavigatorMatrix(player.inputmap.HeldButtons[4], player.inputmap.HeldButtons[3], player.inputmap.HeldButtons[5], player.inputmap.HeldButtons[6], DeltaTime, out DirectionPressed _))
        return;
      z_iconpanel.TryToSelectFromController(this.gridmatrix.SelectedIndex);
      player.inputmap.QuickCenter();
    }
  }
}
