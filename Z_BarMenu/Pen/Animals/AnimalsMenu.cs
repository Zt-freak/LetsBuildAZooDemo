// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BarMenu.Pen.Animals.AnimalsMenu
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.Z_BarMenu.Pen.Animals
{
  internal class AnimalsMenu
  {
    private MainBarManager barmanager;
    public static bool AnimalListChanged;

    public AnimalsMenu(Player player, PrisonZone prisonzone) => this.SetUp(prisonzone, player);

    private void SetUp(PrisonZone prisonzone, Player player)
    {
      this.barmanager = new MainBarManager(BAR_TYPE.Pen_Animals, TILETYPE.None, false, player);
      this.barmanager.SetUpAnimals(prisonzone.prisonercontainer.prisoners);
      this.barmanager.AddHeading("Animals in this pen");
      AnimalsMenu.AnimalListChanged = false;
    }

    public bool IsMouseOverButton(Player player) => this.barmanager.CheckMouseOver(player);

    public bool UpdateAnimalsMenu(Player player, float DeltaTime, PrisonZone prisonzone)
    {
      if (player.inputmap.PressedBackOnController())
        return true;
      if (AnimalsMenu.AnimalListChanged)
      {
        this.SetUp(prisonzone, player);
        if (prisonzone.prisonercontainer.prisoners.Count == 0)
          return true;
      }
      bool GoBack;
      BuildingManageButton buildingManageButton = this.barmanager.UpdateMainBarManager(DeltaTime, player, out GoBack);
      if (GoBack)
      {
        if (!OverWorldManager.zoopopupHolder.IsNull() && OverWorldManager.zoopopupHolder.GetHasState(POPUPSTATE.Animal))
          OverWorldManager.zoopopupHolder.SetNull();
        return true;
      }
      if (buildingManageButton != BuildingManageButton.Count)
      {
        OverWorldManager.zoopopupHolder.CreateZooPopUps(prisonzone.prisonercontainer.prisoners[this.barmanager.PressedIndex], player);
        this.barmanager.Shrink(TILETYPE.Count);
      }
      if (OverWorldManager.zoopopupHolder.IsNull() && this.barmanager.IsShrunk)
        this.barmanager.UnShrink();
      return false;
    }

    public void DrawAnimalsMenu(Player player) => this.barmanager.DrawMainBarManager(player);
  }
}
