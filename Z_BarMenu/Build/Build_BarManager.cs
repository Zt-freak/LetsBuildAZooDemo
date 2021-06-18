// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BarMenu.Build.Build_BarManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.OverWorld.OverWorldRenderer;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BarMenu.Build.CatSelection;
using TinyZoo.Z_BarMenu.Pen.BuildMenu;

namespace TinyZoo.Z_BarMenu.Build
{
  internal class Build_BarManager
  {
    private BuildMenuBarManager buildLIST;
    private CetSelectManager catselect;
    public CATEGORYTYPE selectedCategory;
    private Controller_CatSelectManager controllermatrix;

    public Build_BarManager(Player player)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.selectedCategory = CATEGORYTYPE.Enclosure;
      this.buildLIST = new BuildMenuBarManager(player, this.selectedCategory, baseScaleForUi);
      BuildMenuBarManager.barmanager.AddHeading("Build");
      this.catselect = new CetSelectManager(player);
      this.catselect.location = new Vector2(360f * baseScaleForUi, Build_BarManager.GetVerticalCenterForHeading());
      this.controllermatrix = new Controller_CatSelectManager();
    }

    internal static float GetVerticalCenterForHeading() => (float) (768.0 - (double) Z_GameFlags.GetBaseScaleForUI() * 3.5 * 50.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y);

    internal static float GetVerticalCenterForAllIcons() => (float) (768.0 - (double) Z_GameFlags.GetBaseScaleForUI() * 2.0 * 50.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y);

    public bool CheckMouseOver(Player player)
    {
      float ShrinkValue;
      Vector2 shrinkOffset = this.buildLIST.GetShrinkOffset(out ShrinkValue);
      return this.catselect.CheckMouseOver(player, shrinkOffset, 1f - ShrinkValue) || this.buildLIST.CheckMouseOver(player);
    }

    public void ShrinkLerp(TILETYPE SelectedTile, bool IsShrink) => this.buildLIST.ShrinkLerp(SelectedTile, IsShrink);

    public bool UpdateBuild_BarManager(
      Player player,
      float DeltaTime,
      WallsAndFloorsManager wallsandfloors,
      OverworldBuildManager buildmanager_fornewmenu,
      out TILETYPE TiedToBuildThis,
      out bool ChangedCategory)
    {
      this.controllermatrix.UpdateController_CatSelectManager(player, DeltaTime, BuildMenuBarManager.barmanager, this.catselect);
      int num = this.buildLIST.UpdateBuildMenuBarManager(player, DeltaTime, wallsandfloors, buildmanager_fornewmenu, (PrisonZone) null, out TiedToBuildThis) ? 1 : 0;
      ChangedCategory = false;
      Vector2 shrinkOffset = this.buildLIST.GetShrinkOffset(out float _);
      CATEGORYTYPE categorytype = this.catselect.UpdateCetSelectManager(player, DeltaTime, this.selectedCategory, shrinkOffset);
      if (categorytype == CATEGORYTYPE.Count)
        return num != 0;
      this.selectedCategory = categorytype;
      this.buildLIST = new BuildMenuBarManager(player, this.selectedCategory, Z_GameFlags.GetBaseScaleForUI());
      BuildMenuBarManager.barmanager.AddHeading("Build");
      ChangedCategory = true;
      return num != 0;
    }

    public void DrawBuild_BarManager(Player player)
    {
      this.buildLIST.DrawAddDecoManager(player);
      this.catselect.DrawCetSelectManager(this.buildLIST.GetShrinkOffset(out float _), this.selectedCategory);
    }
  }
}
