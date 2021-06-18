// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BarMenu.Pen.ViewDeco.ViewDecoManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.OverWorld.OverWorldRenderer;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.Z_BarMenu.Pen.ViewDeco
{
  internal class ViewDecoManager
  {
    private MainBarManager barmanager;
    private DecorationInfo decoinfo;

    public ViewDecoManager(Player player, PrisonZone prisonzone)
    {
      this.barmanager = new MainBarManager(BAR_TYPE.Pen_ViewItems, TILETYPE.None, false, player);
      this.barmanager.SetUpForEditEnclosure(player, prisonzone);
    }

    public bool IsMouseOverButton(Player player) => this.barmanager.CheckMouseOver(player);

    public bool UpdateViewDecoManager(
      Player player,
      float DeltaTime,
      PrisonZone prisonzone,
      WallsAndFloorsManager wallsandfloors,
      OverworldBuildManager buildmanager,
      OverWorldEnvironmentManager overworldenvironment)
    {
      if (this.decoinfo == null)
      {
        bool GoBack;
        BuildingManageButton buildingManageButton = this.barmanager.UpdateMainBarManager(DeltaTime, player, out GoBack);
        if (GoBack)
          return true;
        if (buildingManageButton != BuildingManageButton.Count)
          this.decoinfo = new DecorationInfo(player, prisonzone.penItems.items[this.barmanager.PressedIndex]);
      }
      if (this.decoinfo != null && this.decoinfo.UpdateDecorationInfo(DeltaTime, player, wallsandfloors, prisonzone, buildmanager, overworldenvironment))
      {
        this.decoinfo = (DecorationInfo) null;
        this.barmanager = new MainBarManager(BAR_TYPE.Pen_ViewItems, TILETYPE.None, false, player);
        this.barmanager.SetUpForEditEnclosure(player, prisonzone);
      }
      return false;
    }

    public void DrawViewDecoManager(Player player)
    {
      if (this.decoinfo == null)
        this.barmanager.DrawMainBarManager(player);
      if (this.decoinfo == null)
        return;
      this.decoinfo.DrawDecorationInfo(player);
    }
  }
}
