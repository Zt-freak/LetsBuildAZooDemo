// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BarMenu.Pen.AddDeco.AddDecoManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.OverWorld.OverWorldRenderer;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BarMenu.Pen.BuildMenu;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.Z_BarMenu.Pen.AddDeco
{
  internal class AddDecoManager
  {
    private BuildMenuBarManager buildmenumanager;
    private MainBarManager barmanager;
    private CATEGORYTYPE cattype = CATEGORYTYPE.Count;

    public AddDecoManager(Player player) => this.barmanager = new MainBarManager(BAR_TYPE.Pen_MainEditPen, TILETYPE.None, false, player);

    public bool IsMouseOverButton(Player player) => this.barmanager.CheckMouseOver(player);

    public bool UpdateAddDecoManager(
      Player player,
      float DeltaTime,
      WallsAndFloorsManager wallsandfloors,
      OverworldBuildManager buildmanager,
      PrisonZone prisonzone)
    {
      if (this.buildmenumanager == null)
      {
        bool GoBack;
        BuildingManageButton buildingManageButton = this.barmanager.UpdateMainBarManager(DeltaTime, player, out GoBack);
        if (GoBack)
          return true;
        if (buildingManageButton != BuildingManageButton.Count)
        {
          this.cattype = CATEGORYTYPE.Count;
          string HEadingName = "NA";
          switch (buildingManageButton - 9)
          {
            case BuildingManageButton.StoreRoom:
              this.cattype = CATEGORYTYPE.Pen_Water;
              OverWorldManager.heatmapmanager.DoubleCheckWaterSetUp(player);
              HEadingName = "Water";
              break;
            case BuildingManageButton.StoreRoomShop:
              this.cattype = CATEGORYTYPE.Pen_Enrichment;
              HEadingName = "Enrichment";
              break;
            case BuildingManageButton.Move:
              this.cattype = CATEGORYTYPE.Pen_Shelter;
              HEadingName = "Shelter";
              break;
            case BuildingManageButton.Destroy:
              this.cattype = CATEGORYTYPE.Pen_Deco;
              HEadingName = "Decorate";
              break;
          }
          this.buildmenumanager = new BuildMenuBarManager(player, this.cattype, Z_GameFlags.GetBaseScaleForUI());
          BuildMenuBarManager.barmanager.AddHeading(HEadingName);
          return false;
        }
      }
      if (this.buildmenumanager != null && this.buildmenumanager.UpdateBuildMenuBarManager(player, DeltaTime, wallsandfloors, buildmanager, prisonzone, out TILETYPE _))
      {
        player.inputmap.ReleasedThisFrame[7] = false;
        this.buildmenumanager = (BuildMenuBarManager) null;
        this.barmanager = new MainBarManager(BAR_TYPE.Pen_MainEditPen, TILETYPE.None, false, player);
      }
      return false;
    }

    public void DrawAddDecoManager(Player player)
    {
      if (this.buildmenumanager != null)
        this.buildmenumanager.DrawAddDecoManager(player);
      else
        this.barmanager.DrawMainBarManager(player);
    }
  }
}
