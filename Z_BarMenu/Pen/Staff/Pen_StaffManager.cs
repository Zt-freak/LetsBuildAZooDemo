// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BarMenu.Pen.Staff.Pen_StaffManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.Z_BarMenu.Pen.Staff
{
  internal class Pen_StaffManager
  {
    private MainBarManager barmanager;

    public Pen_StaffManager(Player player, PrisonZone prisonzone)
    {
      this.barmanager = new MainBarManager(BAR_TYPE.Pen_Staff, TILETYPE.None, false, player);
      this.barmanager.SetUpAnimals(prisonzone.prisonercontainer.prisoners);
      this.barmanager.AddHeading("Animals in this pen");
    }

    public bool IsMouseOverButton(Player player) => this.barmanager.CheckMouseOver(player);

    public bool UpdateStaffManager(Player player, float DeltaTime, PrisonZone prisonzone)
    {
      if (player.inputmap.PressedBackOnController())
        return true;
      bool GoBack;
      BuildingManageButton buildingManageButton = this.barmanager.UpdateMainBarManager(DeltaTime, player, out GoBack);
      if (GoBack)
        return true;
      if (buildingManageButton != BuildingManageButton.Count)
        OverWorldManager.zoopopupHolder.CreateZooPopUps(prisonzone.prisonercontainer.prisoners[this.barmanager.PressedIndex], player);
      return false;
    }

    public void DrawStaffManager(Player player) => this.barmanager.DrawMainBarManager(player);
  }
}
