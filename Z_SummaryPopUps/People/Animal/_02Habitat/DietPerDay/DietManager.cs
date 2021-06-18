// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._02Habitat.DietPerDay.DietManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._02Habitat.DietPerDay
{
  internal class DietManager
  {
    private CustomerFrame customerframe;
    private MiniHeading miniheading;
    private TextButton ManageDiet;
    private int PenUID;

    public DietManager(float width, PrisonZone pz, float MasterMult)
    {
      this.PenUID = pz.Cell_UID;
      this.miniheading = new MiniHeading(this.customerframe.VSCale, "Current Diet:", MasterMult);
      this.ManageDiet = new TextButton("Manage", 50f);
    }

    public void UpdateDietManager(
      Vector2 TopCenter,
      Player player,
      float DeltaTime,
      ref bool PrssedClose)
    {
      if (!this.ManageDiet.UpdateTextButton(player, TopCenter, DeltaTime))
        return;
      PrssedClose = true;
      Z_GameFlags.ForceToNewScreen = ForceToNewScreen.DietManagement;
      Z_GameFlags.SelectedPrisonZoneUID = this.PenUID;
      Z_GameFlags.SelectedPrisonZoneisFarm = false;
    }

    public void DrawDietManager(Vector2 TopCenter)
    {
      this.customerframe.DrawCustomerFrame(TopCenter, AssetContainer.pointspritebatchTop05);
      this.ManageDiet.DrawTextButton(TopCenter, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}
