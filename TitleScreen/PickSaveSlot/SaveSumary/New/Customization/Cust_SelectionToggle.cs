// Decompiled with JetBrains decompiler
// Type: TinyZoo.TitleScreen.PickSaveSlot.SaveSumary.New.Customization.Cust_SelectionToggle
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.TitleScreen.PickSaveSlot.SaveSumary.New.Customization
{
  internal class Cust_SelectionToggle
  {
    private SelectorBarWithLabels selectorBar;

    public Cust_SelectionToggle(float BaseScale, CustomizationOption customizationoption)
    {
      this.selectorBar = new SelectorBarWithLabels(BaseScale);
      string OptionName;
      int availableOptionCount = CustomizationData.GetAvailableOptionCount(customizationoption, 0, out OptionName);
      int num = CustomizationData.GetDefault(customizationoption);
      for (int ThisOption = 0; ThisOption < availableOptionCount; ++ThisOption)
      {
        CustomizationData.GetAvailableOptionCount(customizationoption, ThisOption, out OptionName);
        if (ThisOption < num)
          this.selectorBar.Add(OptionName, BTNColour.Green);
        else if (ThisOption == num)
          this.selectorBar.Add(OptionName, BTNColour.Grey);
        else
          this.selectorBar.Add(OptionName, BTNColour.Red);
      }
    }

    public void UpdateCust_SelectionToggle(Player player, float DeltaTime, Vector2 Offset) => this.selectorBar.UpdateSelectorBarWithLabels(player, Offset, DeltaTime);

    public void DrawCust_SelectionToggle(Vector2 Offset) => this.selectorBar.DrawSelectorBarWithLabels(AssetContainer.pointspritebatch03, Offset);
  }
}
