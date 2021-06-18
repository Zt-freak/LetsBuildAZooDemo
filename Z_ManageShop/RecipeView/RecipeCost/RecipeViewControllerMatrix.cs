// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.RecipeView.RecipeCost.RecipeViewControllerMatrix
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.Buttons;
using TinyZoo.Z_ManageShop.RecipeView.NewLayout;

namespace TinyZoo.Z_ManageShop.RecipeView.RecipeCost
{
  internal class RecipeViewControllerMatrix
  {
    private ButtonRepeater repeater;
    public int SelectedIndex;
    private int Total;
    private bool HasExtraIngredient;

    public RecipeViewControllerMatrix(int Entries, bool _HasExtraIngredient)
    {
      this.HasExtraIngredient = _HasExtraIngredient;
      this.repeater = new ButtonRepeater();
      this.Total = Entries;
      if (!this.HasExtraIngredient)
        return;
      ++this.Total;
    }

    public void UpdateExBar(
      Player player,
      float DeltaTime,
      ExtraIngredient extraingredient,
      FoodViewSet foodset)
    {
      float Movement = (float) ((double) player.inputmap.Movementstick.X * (double) DeltaTime * 1.0);
      if (extraingredient != null)
        extraingredient.OverrideFoodSlider(player, Movement);
      else
        foodset.OverrideFoodSlider(player, Movement, DeltaTime);
    }

    public bool ExtraIngredientSelected() => this.SelectedIndex == 0 && this.HasExtraIngredient;

    public bool IsSelected(int Index)
    {
      if (this.SelectedIndex == Index && !this.HasExtraIngredient)
        return true;
      return this.SelectedIndex == Index + 1 && this.HasExtraIngredient;
    }

    public void UpdateRecipeViewControllerMatrix(Player player, float DeltaTime)
    {
      if (!GameFlags.IsUsingController)
        return;
      Z_GameFlags.TempBlockVirtualMouse = true;
      DirectionPressed Direction;
      if (!this.repeater.UpdateMenuRepeats(DeltaTime, out Direction, player.inputmap.HeldButtons[16], player.inputmap.HeldButtons[17], false, false))
        return;
      switch (Direction)
      {
        case DirectionPressed.Up:
          if (this.SelectedIndex <= 0)
            break;
          --this.SelectedIndex;
          break;
        case DirectionPressed.Down:
          if (this.SelectedIndex >= this.Total - 1)
            break;
          ++this.SelectedIndex;
          break;
      }
    }
  }
}
